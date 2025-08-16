using Domain;
using Infraestructure.Contexts;
using Infraestructure.Core.Repositories;
using Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class RespuestaUsuarioRepositorio : CurdCoreRespository<RespuestaUsuario, int>, IRespuestaUsuarioRepositorio
    {
        private readonly ApplicationDbContext _dbContext;
        public RespuestaUsuarioRepositorio(ApplicationDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public override async Task<IReadOnlyList<RespuestaUsuario>> FindAllAsync()
        {
            var response = await _dbContext.Set<RespuestaUsuario>().Include(t => t.OpcionesRpt).ToListAsync();

            return response;
        }

        public async Task<IReadOnlyList<RespuestaUsuario>> FindAllAsyncMateria(int id)
        {
            var response = await _dbContext.Set<RespuestaUsuario>().Include(t => t.Pregunta).Include(t => t.OpcionesRpt).Where(t => t.Pregunta.IdMateria == id).ToListAsync();

            return response;
        }

        public override async Task<RespuestaUsuario?> FindByIdAsync(int id)
        {
            var response = await _dbContext.Set<RespuestaUsuario>().Include(t => t.OpcionesRpt).FirstOrDefaultAsync(t => t.IdRespuesta == id);

            return response;
        }
        public async Task<RespuestaUsuario?> FindByIdUsuarioAsync(int idUsuario , int idPregunta)
        {
            var response = await _dbContext.Set<RespuestaUsuario>().FirstOrDefaultAsync(t => t.IdUsuario == idUsuario && t.IdPregunta == idPregunta);

            return response;
        }

        public async Task<bool> HasHistoryAsync(int idUsuario, int idCurso)
        {
            return await _dbContext.Set<RespuestaUsuario>()
                .Include(r => r.Pregunta)
                .AnyAsync(r => r.IdUsuario == idUsuario && r.Pregunta.idCurso == idCurso);
        }

        public async Task<RespuestaUsuario?> FindLastAnswerAsync(int idUsuario, int idCurso)
        {
            return await _dbContext.Set<RespuestaUsuario>()
                .Include(r => r.Pregunta)
                .Where(r => r.IdUsuario == idUsuario && r.Pregunta.idCurso == idCurso)
                .OrderByDescending(r => r.FechaRespuesta)
                .FirstOrDefaultAsync();
        }

        public async Task<RespuestaUsuario?> FindLastAnswerForQuestionAsync(int idUsuario, int idPregunta)
        {
            return await _dbContext.Set<RespuestaUsuario>()
                .Where(r => r.IdUsuario == idUsuario && r.IdPregunta == idPregunta)
                .OrderByDescending(r => r.FechaRespuesta)
                .FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<RespuestaUsuario>> GetAllAsync()
        {
            return await _dbContext.Set<RespuestaUsuario>().ToListAsync();
        }

        public async Task<IReadOnlyList<RespuestaUsuario>> FindAllByUserAsync(int idUsuario, int idCurso)
        {
            var questionIdsInCourse = await _dbContext.Set<Pregunta>()
                .Where(p => p.idCurso == idCurso)
                .Select(p => p.IdPregunta)
                .ToListAsync();

            return await _dbContext.Set<RespuestaUsuario>()
                .Where(r => r.IdUsuario == idUsuario && questionIdsInCourse.Contains(r.IdPregunta))
                .ToListAsync();
        }

        public async Task<int> DeleteByUserIdAndCourseIdAsync(int userId, int courseId)
        {
            // 1. Encontrar los Ids de las preguntas del curso.
            var questionIds = await _dbContext.Set<Pregunta>()
                                            .Where(p => p.idCurso == courseId)
                                            .Select(p => p.IdPregunta)
                                            .ToListAsync();

            if (!questionIds.Any()) return 0;

            // 2. Encontrar las respuestas del usuario para esas preguntas.
            var answersToDelete = await _dbContext.Set<RespuestaUsuario>()
                                                .Where(r => r.IdUsuario == userId && questionIds.Contains(r.IdPregunta))
                                                .ToListAsync();

            if (!answersToDelete.Any()) return 0;

            // 3. Eliminar las respuestas y guardar los cambios.
            _dbContext.Set<RespuestaUsuario>().RemoveRange(answersToDelete);
            return await _dbContext.SaveChangesAsync();
        }
    }
}
