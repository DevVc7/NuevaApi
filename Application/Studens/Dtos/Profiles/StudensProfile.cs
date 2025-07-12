using Application.Studens.Dtos.Students;
using AutoMapper;
using Domain;

namespace Application.Studens.Dtos.Profiles
{
    public class StudensProfile : Profile
    {
        public StudensProfile() 
        {
            CreateMap<Student , StudentsDto>().ReverseMap();
            CreateMap<Student , StudentsSaveDto>().ReverseMap();

        }
    }
}
