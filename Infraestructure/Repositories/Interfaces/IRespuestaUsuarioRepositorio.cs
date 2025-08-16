using Domain;
using Infraestructure.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IRespuestaUsuarioRepositorio : ICurdCoreRespository<RespuestaUsuario, int>
    {
        Task<int> DeleteByUserIdAndCourseIdAsync(int userId, int courseId);
        Task<RespuestaUsuario?> FindByIdUsuarioAsync(int idUsuario, int idPregunta);
        Task<IReadOnlyList<RespuestaUsuario>> FindAllAsyncMateria(int id);
        // -- 1. Aprendizaje Adaptativo
        Task<bool> HasHistoryAsync(int idUsuario, int idCurso);
        Task<RespuestaUsuario?> FindLastAnswerAsync(int idUsuario, int idCurso);
        Task<RespuestaUsuario?> FindLastAnswerForQuestionAsync(int idUsuario, int idPregunta);
        Task<IReadOnlyList<RespuestaUsuario>> GetAllAsync();
        Task<IReadOnlyList<RespuestaUsuario>> FindAllByUserAsync(int idUsuario, int idCurso);
    }
}
