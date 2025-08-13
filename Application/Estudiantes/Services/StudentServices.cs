using Application.Auth.Services.Interfaces;
using Application.Estudiant.Dtos.Students;
using Application.Exceptions;
using Application.Studens.Services.Interface;
using AutoMapper;
using Domain;
using Domain.View;
using Infraestructure.Repositories.Interfaces;

namespace Application.Estudiantes.Services
{
    public class StudentServices : IStudentServices
    {
        private readonly IMapper _mapper;
        private readonly IEstudianteRepositorio _estudianteRepositorio;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IRolUsuarioRepositorio _rolUsuarioRepositorio;
        private readonly IRolRepositorio _rolRepositorio;
        private readonly IJwtServices _securityService;

        public StudentServices(IMapper mapper, IRolUsuarioRepositorio rolUsuarioRepositorio, IEstudianteRepositorio estudianteRepositorio, IRolRepositorio rolRepositorio ,IUsuarioRepositorio usuarioRepositorio, IJwtServices securityService)
        {
            _mapper = mapper;
            _estudianteRepositorio = estudianteRepositorio;
            _rolRepositorio = rolRepositorio;
            _rolUsuarioRepositorio = rolUsuarioRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
            _securityService = securityService;
        }

        public async Task<StudentsMeta<Estudiante>> BusquedaPaginado()
        {
           var response =  await _estudianteRepositorio.BusquedaPaginado();

            return response;
        }

        public async Task<OperationResult<EstudianteDto>> CreateAsync(EstudianteSaveDto saveDto)
        {
            var user = _mapper.Map<User>(saveDto.UsuarioSave);

            var email = await _usuarioRepositorio.FindByEmailAsync(user.Correo);

            if (email != null) throw new NotFoundCoreException("Correo ya Registrado");

            user.Estado = true;
            user.CreatedAt = DateTime.Now;
            user.Password = _securityService.HashPassword(user.Correo, user.Password);
            user.NombreCompleto = $"{user.Nombres} {user.Apellidos}";

            await _usuarioRepositorio.SaveAsync(user);


            var rol = await _rolRepositorio.FillName("student");

            RolUser rol_usuario = new()
            {
                IdRol = rol.IdRol,
                IdUsuario = user.IdUsuario,
                Estado = true,
                CreatedAt = DateTime.Now
            };

            await _rolUsuarioRepositorio.SaveAsync(rol_usuario);


            var estudiante = _mapper.Map<Estudiante>(saveDto);

            estudiante.IdUsuario = user.IdUsuario;
            estudiante.CreatedAt = DateTime.Now;
            estudiante.IdSeccion = null;

            await _estudianteRepositorio.SaveAsync(estudiante);


            return new OperationResult<EstudianteDto>()
            {
                State = true,
                Data = _mapper.Map<EstudianteDto>(estudiante),
                Message = "Estudiante creado con exito"
            };
        }

        public async Task<OperationResult<EstudianteDto>> DisabledAsync(int id)
        {
            var estudiante = await _estudianteRepositorio.FindByIdAsync(id);

            if (estudiante == null) throw new NotFoundCoreException("estudiante no encontrado para el id " + id);

            var user = await _usuarioRepositorio.FindByIdAsync(estudiante.IdUsuario);

            user.Estado = !user.Estado;

            await _usuarioRepositorio.SaveAsync(user);


            return new OperationResult<EstudianteDto>()
            {
                State = true,
                Data = _mapper.Map<EstudianteDto>(estudiante),
                Message = "Estudiante deshabilitado con exito"
            };
        }

        public async Task<OperationResult<EstudianteDto>> EditAsync(int id, EstudianteSaveDto saveDto)
        {
            var student = await _estudianteRepositorio.FindByIdAsync(id);

            if (student == null) throw new NotFoundCoreException("estudiante no encontrado para el id " + id);

            User usuario = await _usuarioRepositorio.FindByIdAsync(student.IdUsuario);

            usuario.UpdatedAt = DateTime.Now;
            usuario.NombreCompleto = saveDto.UsuarioSave.Nombres;
            usuario.Correo = saveDto.UsuarioSave.Correo;
            usuario.Password = _securityService.HashPassword(saveDto.UsuarioSave.Correo, "student123");

            await _usuarioRepositorio.SaveAsync(usuario);

            student.UpdatedAt = DateTime.Now;
            student.Usuario = usuario;
            student.Descripcion = saveDto.Descripcion;
            student.CodEstudiante = saveDto.CodEstudiante;
            student.IdGrado = saveDto.IdGrado;
            
            await _estudianteRepositorio.SaveAsync(student);

            var update = _mapper.Map<EstudianteDto>(student);

            return new OperationResult<EstudianteDto>()
            {
                State = true,
                Data = update,
                Message = "Estudiante actualizado con exito"
            };

        }

        public async Task<IReadOnlyList<EstudianteDto>> FindAllAsync()
        {
            var response = await _estudianteRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<EstudianteDto>>(response);
        }

        public async Task<EstudianteDto> FindByIdAsync(int id)
        {
            var response = await _estudianteRepositorio.FindByIdAsync(id);

            return _mapper.Map<EstudianteDto>(response);
        }
    }
}
