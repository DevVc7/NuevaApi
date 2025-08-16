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
    }
}
