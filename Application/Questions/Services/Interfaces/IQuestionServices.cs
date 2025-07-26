using Application.Core.Services.Interfaces;
using Application.Questions.Dto;
using Domain;
using Domain.View;

namespace Application.Questions.Services.Interfaces
{
    public interface IQuestionServices : ICurdCoreService<QuestionDto, QuestionSaveDto, Guid>
    {
        Task<QuestionDataResponse> BusquedaPaginado();
    }
}
