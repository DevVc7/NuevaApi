using Application.Auth.Dto;
using Application.Auth.Services.Interfaces;
using Application.Exceptions;
using Application.Usuarios.Dto;
using Application.Usuarios.Services.Interface;
using AutoMapper;
using Azure.Core;
using Domain;
using Infraestructure.Repositories;
using Infraestructure.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;

namespace Application.Usuarios.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IRolRepositorio _rolRepositorio;
        private readonly IRolUsuarioRepositorio _rolUsuarioRepositorio;
        private readonly IEstudianteRepositorio _estudianteRepositorio;
        private readonly IEscuelaRepositorio _escuelaRepositorio;
        private readonly IJwtServices _securityService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserService> _logger;
        public UserService(
            IMapper mapper,
            IEscuelaRepositorio escuelaRepositorio,
            IRolUsuarioRepositorio rolUsuarioRepositorio , 
            IRolRepositorio rolRepositorio ,
            IEstudianteRepositorio estudianteRepositorio,
            IUsuarioRepositorio usuarioRepositorio, 
            IJwtServices securityService, 
            IConfiguration configuration, 
            ILogger<UserService> logger)
        {
            _mapper = mapper;
            _escuelaRepositorio = escuelaRepositorio;
            _rolRepositorio = rolRepositorio;
            _rolUsuarioRepositorio = rolUsuarioRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
            _securityService = securityService;
            _configuration = configuration;
            _logger = logger;
            _estudianteRepositorio = estudianteRepositorio;
        }

        public async Task<OperationResult<UserDto>> CreateAsync(UserRolSaveDto saveDto)
        {
            var user = _mapper.Map<User>(saveDto.User);
            var rol = _mapper.Map<Rol>(saveDto.Rol);

            var email = await _usuarioRepositorio.FindByEmailAsync(user.Correo);

            if (email != null) throw new NotFoundCoreException("Correo ya Registrado");

            user.Estado = true;
            user.CreatedAt = DateTime.Now;
            user.Password = _securityService.HashPassword(user.Correo, user.Password);
            user.NombreCompleto = $"{user.Nombres} {user.Apellidos}";
            
            await _usuarioRepositorio.SaveAsync(user);

            rol.CreatedAt = DateTime.Now;
            rol.Estado = true;

            await _rolRepositorio.SaveAsync(rol);

            var rol_usuario = new RolUser();

            rol_usuario.IdRol = rol.IdRol;
            rol_usuario.IdUsuario = user.IdUsuario;
            rol_usuario.Estado = true;
            rol_usuario.CreatedAt = DateTime.Now;

            await _rolUsuarioRepositorio.SaveAsync(rol_usuario);


            return new OperationResult<UserDto>()
            {
                State = true,
                Data = _mapper.Map<UserDto>(user),
                Message = "Usuario creado con exito"
            };
        }

        public async Task<OperationResult<UserDto>> DisabledAsync(int id)
        {
            var usuario = await _usuarioRepositorio.FindByIdAsync(id);
            
            if (usuario == null)
            {
                _logger.LogWarning("Usuario no encontrado para el id " + id);
                throw new NotImplementedException();
            }


            var usuarioSave = await _usuarioRepositorio.SaveAsync(usuario);

            var dUser = _mapper.Map<UserDto>(usuarioSave);

            return new OperationResult<UserDto>()
            {
                State = true,
                Data = dUser,
                Message = "Usuario  con exito"
            };
        }

        public async Task<OperationResult<UserDto>> EditAsync(int id, UserRolSaveDto saveDto)
        {
            User? user = await _usuarioRepositorio.FindByIdAsync(id);

            if (user == null) throw new NotFoundCoreException("Usuario no Registrado con ese id");

            user.Estado = true;
            user.CreatedAt = DateTime.Now;
            user.Password = user.Password;
            user.NombreCompleto = saveDto.User.NombreCompleto;
            user.Photo = saveDto.User.Photo;
            user.Biografia = saveDto.User.Biografia;
            user.IdEscuela = saveDto.User.IdEscuela;

            await _usuarioRepositorio.SaveAsync(user);

            return new OperationResult<UserDto>()
            {
                State = true,
                Data = _mapper.Map<UserDto>(user),
                Message = "Usuario actualizado con exito"
            };

        }

        public async Task<IReadOnlyList<UserDto>> FindAllAsync()
        {
            var response = await _usuarioRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<UserDto>>(response);
        }

        public async Task<UserDto> FindByIdAsync(int id)
        {
            var response = await _usuarioRepositorio.FindByIdAsync(id);

            return  _mapper.Map<UserDto>(response);
        }

        public async Task<LoginDto> LoginAsync(LoginRequest userAuthDto)
        {

            User? user = await _usuarioRepositorio.FindByEmailAsync(userAuthDto.Email);
            

            if (user is null) throw new NotFoundCoreException("Usuario no registrado");
            
            bool isCorrect = _securityService.VerifyHashedPassword(user.Correo, user.Password, userAuthDto.Password);

            if (!isCorrect) throw new NotFoundCoreException("La contraseña no es correcta");

            string jwtSecretKey = _configuration.GetSection("security:JwtSecretKey").Get<string>();
            
            var user_securiti = _securityService.JwtSecurity(jwtSecretKey);

            var infoestudiante = await _estudianteRepositorio.FindByIdUsuarioAsync(user.IdUsuario);

            var rol_user = await _rolUsuarioRepositorio.FindByIdAsync(user.IdUsuario);

            var view_user = new UserView()
            {
                Id = user.IdUsuario,
                Name = user.NombreCompleto,
                Email = user.Correo,
                Type = rol_user?.Rol.Descripcion,
                Grade = infoestudiante?.IdGrado ?? 0,
                Rol = rol_user?.Rol.Descripcion
            };
            
            byte[] randomBytes = RandomNumberGenerator.GetBytes(64);
            
            


            LoginDto userSecurity = new()
            {
                AccessToken = user_securiti.Token,
                RefreshToken = user_securiti.Token,
                User = view_user,

            };

            return userSecurity;
        }


        
       
    }
}
