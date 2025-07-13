using Application.Auth.Services.Interfaces;
using Application.Exceptions;
using Application.Studens.Dtos.Students;
using Application.Studens.Dtos.StudentSubje;
using Application.Studens.Dtos.Subjec;
using Application.Studens.Services.Interface;
using Application.Usuarios.Dto;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Studens.Services
{
    public class StudentServices : IStudentServices
    {
        private readonly IMapper _mapper;
        private readonly IStudentRepositorio _studentRepositorio;
        private readonly ISubjectRepositorio _subjectRepositorio;
        private readonly IStudentSubjectsRepositorio _studentSubjectsRepositorio;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IJwtServices _securityService;

        public StudentServices(IMapper mapper, IStudentRepositorio studentRepositorio, ISubjectRepositorio subjectRepositorio, IStudentSubjectsRepositorio studentSubjectsRepositorio, IUsuarioRepositorio usuarioRepositorio, IJwtServices securityService)
        {
            _mapper = mapper;
            _studentRepositorio = studentRepositorio;
            _subjectRepositorio = subjectRepositorio;
            _studentSubjectsRepositorio = studentSubjectsRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
            _securityService = securityService;
        }

        public async Task<StudentsMeta<Student>> BusquedaPaginado()
        {
           var response =  await _studentRepositorio.BusquedaPaginado();

            return response;
        }

        public async Task<OperationResult<StudentsDto>> CreateAsync(StudentsSaveDto saveDto)
        {
            
            var email = await _usuarioRepositorio.FindByEmailAsync(saveDto.User.Email);

            if (email != null) new NotFoundCoreException("Email ya Registrado");

            var user = _mapper.Map<User>(saveDto.User);

            user.Role = "student";
            user.CreateAt = DateTime.Now;
            user.UpdatedAt = DateTime.Now;
            user.Password = _securityService.HashPassword(user.Email, user.Password);

            await _usuarioRepositorio.SaveAsync(user);

            var new_user = _mapper.Map<UserDto>(user);

            var students = _mapper.Map<Student>(saveDto);
            
            students.Progress = 0;
            students.LastActive = "Hoy";
            students.UsersId = new_user.Id;
            students.CreatedAt = DateTime.Now;
            students.UpdatedAt = DateTime.Now;
            students.User = user;

            await _studentRepositorio.SaveAsync(students);


            var new_student = _mapper.Map<StudentsDto>(students);


            var nombresMaterias = new[] { "Matemática", "Comunicación" };

             foreach (var nombre in nombresMaterias)
            {
                var materia = await _subjectRepositorio.GetSubjectsName(nombre);

                if (materia != null)
                {
                    var entity = new StudentSubjects
                    {
                        Progress = 0,
                        StudentId = students.Id,     
                        SubjectId = materia.Id,     
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };


                    await _studentSubjectsRepositorio.SaveAsync(entity);
                }
            }


            return new OperationResult<StudentsDto>()
            {
                State = true,
                Data = new_student,
                Message = "student creado con exito"
            };
        }

        public Task<OperationResult<StudentsDto>> DisabledAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResult<StudentsDto>> EditAsync(Guid id, StudentsSaveDto saveDto)
        {
            Student? student = await _studentRepositorio.FindByIdAsync(id);

            if (student == null) throw new NotFoundCoreException("estudiante no encontrado para el id " + id);

            User usuario = await _usuarioRepositorio.FindByIdAsync(student.UsersId);

            usuario.UpdatedAt = DateTime.Now;
            usuario.Name = saveDto.User.Name;
            usuario.Role = "student";

            await _usuarioRepositorio.SaveAsync(usuario);

            student.UpdatedAt = DateTime.Now;
            student.User = usuario;

            _mapper.Map(saveDto, student);

            await _studentRepositorio.SaveAsync(student);

            var update = _mapper.Map<StudentsDto>(student);


            return new OperationResult<StudentsDto>()
            {
                State = true,
                Data = update,
                Message = "student actualizado con exito"
            };

        }

        public async Task<IReadOnlyList<StudentsDto>> FindAllAsync()
        {
            var response = await _studentRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<StudentsDto>>(response);
        }

        public async Task<StudentsDto> FindByIdAsync(Guid id)
        {
            var response = await _studentRepositorio.FindByIdAsync(id);

            return _mapper.Map<StudentsDto>(response);
        }
    }
}
