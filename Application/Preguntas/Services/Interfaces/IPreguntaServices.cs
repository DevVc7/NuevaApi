using Application.Core.Services.Interfaces;
using Application.Preguntas.Dto;
using Domain;
using Domain.View;

namespace Application.Preguntas.Services.Interfaces
{
    public interface IPreguntaServices : ICurdCoreService<PreguntaDto, QuestionRequestDto, int>
    {
        Task<QuestionDataResponse> BusquedaPaginado();
    }
}
