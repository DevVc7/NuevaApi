using AutoMapper;
using Domain;

namespace Application.Questions.Dto.Profiles
{
    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            CreateMap<Question, QuestionDto>().ReverseMap();
            CreateMap<Question, QuestionSaveDto>().ReverseMap();
        }
    }
}
