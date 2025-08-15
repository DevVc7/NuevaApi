using Application.Materias.Dtos.SaveDtos;
using AutoMapper;
using Domain;

namespace Application.Materias.Dtos.Profiles
{
    public class MateriaProfile: Profile
    {
        public MateriaProfile() 
        {
            CreateMap<Materia , MateriaDto>().ReverseMap();
            CreateMap<Materia , MateriaSaveDto>().ReverseMap();
            
            
            CreateMap<Curso , CursoDto>().ReverseMap();
            CreateMap<Curso , CursoSaveDto>().ReverseMap();

            CreateMap<Leccion, LeccionDto>().ReverseMap();
            CreateMap<Leccion, LeccionSaveDto>().ReverseMap();
        }
    }
}
