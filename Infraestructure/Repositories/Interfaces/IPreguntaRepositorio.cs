using Domain;
using Domain.View;
using Infraestructure.Core.Repositories.Interfaces;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IPreguntaRepositorio : ICurdCoreRespository<Pregunta, int>
    {
        Task<QuestionDataResponse> BusquedaPaginado();
        Task<IReadOnlyList<Pregunta>> FindAllMateriaAsync(int id);
        Task<IReadOnlyList<Pregunta>> FindAllQuestionMateria(PreguntaView view);
        // -- 1. Aprendizaje Adaptativo
        Task<Pregunta?> FindEasiestQuestionAsync(int idCurso);
        Task<Pregunta?> FindNextDifficultyAsync(int idLeccion, string dificultadActual);
        Task<Pregunta?> FindPreviousDifficultyAsync(int idLeccion, string dificultadActual);
        Task<Pregunta?> FindNextLessonEasiestQuestionAsync(int idCurso, int idLeccionActual);
        Task<IReadOnlyList<Pregunta>> FindAllByCourseAsync(int idCurso);
        Task<Pregunta?> FindAdaptiveQuestionByIdAsync(int id);

        // reporte

        Task<List<ReporteUsuarioDto>> GetReporteAsync();
        Task<List<ReporteUsuario>> GetReporteByUserAsync(int idUsuario);
    }
}
