
using Application.Estudiantes.Dtos.Grados;
using AutoMapper;
using Domain;

namespace Application.Estudiantes.Dtos.Profiles
{
    public class GradoProfile : Profile
    {
        public GradoProfile()
        {
            CreateMap<Grado, GradoDto>().ReverseMap();
            CreateMap<Grado, GradoSaveDto>().ReverseMap();

        }
    }
}
