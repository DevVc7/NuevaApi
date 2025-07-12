using Application.Studens.Dtos.Students;
using Application.Studens.Dtos.StudentSubje;
using AutoMapper;
using Domain;

namespace Application.Studens.Dtos.Profiles
{
    public class StudentSubjectsProfile : Profile
    {
        public StudentSubjectsProfile()
        {
            CreateMap<StudentSubjects, StudentSubjectsDto>().ReverseMap();
            CreateMap<StudentSubjects, StudentSubjectsSaveDto>().ReverseMap();

        }
    }
}
