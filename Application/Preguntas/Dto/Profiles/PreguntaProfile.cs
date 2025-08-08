using AutoMapper;
using Domain;

namespace Application.Preguntas.Dto.Profiles
{
    public class PreguntaProfile : Profile
    {
        public PreguntaProfile()
        {
            CreateMap<Pregunta, PreguntaDto>().ReverseMap();
            CreateMap<Pregunta, PreguntaSaveDto>().ReverseMap();
        }
    }
}
