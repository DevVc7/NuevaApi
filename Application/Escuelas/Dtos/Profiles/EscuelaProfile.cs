using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Escuelas.Dtos.Profiles
{
    public class EscuelaProfile : Profile
    {
        public EscuelaProfile() 
        {
            CreateMap<Escuela , EscuelaDto>().ReverseMap();
            CreateMap<Escuela, EscuelaSaveDto>().ReverseMap();
        }
    }
}
