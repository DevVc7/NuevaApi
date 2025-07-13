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
        private readonly IPersonaRepositorio _personaRepositorio;
        private readonly IJwtServices _securityService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserService> _logger;
        public UserService(IMapper mapper, IPersonaRepositorio personaRepositorio,IUsuarioRepositorio usuarioRepositorio, IJwtServices securityService, IConfiguration configuration, ILogger<UserService> logger)
        {
            _mapper = mapper;
            _personaRepositorio = personaRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
            _securityService = securityService;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<OperationResult<UserDto>> CreateAsync(UserSaveDto saveDto)
        {
            var VEmail = await _usuarioRepositorio.FindByEmailAsync(saveDto.Email);

            if (VEmail != null) {
                
                return new OperationResult<UserDto>()
                {
                    State = false,
                    Data = null,
                    Message = "Esta Correo ya esta registrado"
                };
            }

            User user = _mapper.Map<User>(saveDto);

            user.CreateAt = DateTime.Now;
            user.UpdatedAt = DateTime.Now;
            user.Password = _securityService.HashPassword(saveDto.Email, user.Password);
            
            await _usuarioRepositorio.SaveAsync(user);
            
            var newUser =  _mapper.Map<UserDto>(user);

            return new OperationResult<UserDto>()
            {
                State = true,
                Data = newUser,
                Message = "Usuario creado con exito"
            };
        }

        public async Task<OperationResult<UserDto>> DisabledAsync(Guid id)
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

        public async Task<OperationResult<UserDto>> EditAsync(Guid id, UserSaveDto saveDto)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<UserDto>> FindAllAsync()
        {
            var response = await _usuarioRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<UserDto>>(response);
        }

        public async Task<UserDto> FindByIdAsync(Guid id)
        {
            var response = await _usuarioRepositorio.FindByIdAsync(id);

            return  _mapper.Map<UserDto>(response);
        }

        public async Task<LoginDto> LoginAsync(LoginRequest userAuthDto)
        {

            var response = await _usuarioRepositorio.FindAllAsync();

            if(response.Count == 0)
            {
                User? us = new User()
                {
                    Email = userAuthDto.Email,
                    Password = _securityService.HashPassword(userAuthDto.Email, userAuthDto.Password),
                    Role = "admin",
                    Name = "Admin",
                    CreateAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                await _usuarioRepositorio.SaveAsync(us);
            }


            User? user = await _usuarioRepositorio.FindByEmailAsync(userAuthDto.Email);
            

            if (user is null) throw new NotFoundCoreException("Usuario no registrado");
            
            bool isCorrect = _securityService.VerifyHashedPassword(user.Email, user.Password, userAuthDto.Password);

            if (!isCorrect) throw new NotFoundCoreException("La contraseña no es correcta");

            string jwtSecretKey = _configuration.GetSection("security:JwtSecretKey").Get<string>();
            
            var user_securiti = _securityService.JwtSecurity(jwtSecretKey);

            var responde_user = _mapper.Map<UserDto>(user);
            
            byte[] randomBytes = RandomNumberGenerator.GetBytes(64);
            
            


            LoginDto userSecurity = new()
            {
                AccessToken = user_securiti.Token,
                RefreshToken = user_securiti.Token,
                User = responde_user,

            };

            return userSecurity;
        }

       
    }
}
