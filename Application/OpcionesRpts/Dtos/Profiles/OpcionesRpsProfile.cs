using AutoMapper;
using Domain;

namespace Application.OpcionesRpts.Dtos.Profiles
{
    public class OpcionesRpsProfile : Profile
    {
        public OpcionesRpsProfile()
        {
            CreateMap<OpcionesRpt , OpcionSaveDto>().ReverseMap();

        }
    }
}
