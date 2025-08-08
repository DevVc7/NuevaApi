using AutoMapper;
using Domain;

namespace Application.Roles.Dtos.Profiles
{
    public class RolProfile : Profile
    {
        public RolProfile() { 
        
            CreateMap<Rol , RolDto>().ReverseMap();
            CreateMap<Rol , RolSaveDto>().ReverseMap();

        }
    }
}
