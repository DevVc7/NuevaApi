using Application.Materias.Dtos.SaveDtos;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
    }
}
