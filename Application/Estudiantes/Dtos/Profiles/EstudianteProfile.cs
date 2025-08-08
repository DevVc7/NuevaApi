
using Application.Estudiant.Dtos.Students;
using AutoMapper;
using Domain;

namespace Application.Estudiantes.Dtos.Profiles
{
    public class EstudianteProfile : Profile
    {
        public EstudianteProfile() 
        {
            CreateMap<Estudiante , EstudianteDto>().ReverseMap();
            CreateMap<Estudiante , EstudianteSaveDto>().ReverseMap();
        }
    }
}
