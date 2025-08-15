using Domain;
using Infraestructure.Contexts;
using Infraestructure.Core.Repositories;
using Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class LeccionRepositorio : CurdCoreRespository<Leccion, int>, ILeccionRepositorio
    {
        private readonly ApplicationDbContext _dbContext;
        public LeccionRepositorio(ApplicationDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<IReadOnlyList<Leccion>> FindByIdCursoAsync(int id)
        {
            var response = await _dbContext.Set<Leccion>().
                 Where(t => t.IdCurso == id && t.Estado).
                 ToListAsync();

            return response;
        }
    }
}
