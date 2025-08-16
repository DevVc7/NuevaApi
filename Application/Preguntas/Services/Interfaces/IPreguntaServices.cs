using Application.Core.Services.Interfaces;
using Application.Preguntas.Dto;
using Domain;
using Domain.View;

namespace Application.Preguntas.Services.Interfaces
{
    public interface IPreguntaServices : ICurdCoreService<PreguntaDto, QuestionRequestDto, int>
    {
        Task<QuestionDataResponse> BusquedaPaginado();
        Task<IReadOnlyList<PreguntaDto>> FindAllMateriaAsync(int id);
        Task<IReadOnlyList<PreguntaDto>> FindAllQuestionMateria(PreguntaView view);
        Task<OperationResult<RespuestaUsuarioDto>> SaveRespuesta(RespuestaUsuarioSaveDto saveDto);
        Task<IReadOnlyList<RespuestaUsuarioDto>> FinPreguntaAsync(int id);
        
        // -- Funciones para el sistema de aprendizaje adaptativo --
        Task<PreguntaDto> GetFirstAdaptiveQuestionAsync(int idUsuario, int idCurso);
        Task<PreguntaDto?> GetNextAdaptiveQuestionAsync(int idUsuario, int idPreguntaAnterior);
        Task<bool> ResetAdaptiveTestAsync(int idUsuario, int idCurso);
    }
}
