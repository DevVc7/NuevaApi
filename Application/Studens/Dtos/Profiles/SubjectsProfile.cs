using Application.Studens.Dtos.Subjec;
using AutoMapper;
using Domain;

namespace Application.Studens.Dtos.Profiles
{
    public class SubjectsProfile : Profile
    {
        public SubjectsProfile()
        {
            CreateMap<Subjects, SubjectsDto>().ReverseMap();
            CreateMap<Subjects, SubjectsSaveDto>().ReverseMap();

        }
    }
}
