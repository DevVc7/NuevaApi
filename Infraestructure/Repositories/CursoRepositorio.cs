using Domain;
using Infraestructure.Contexts;
using Infraestructure.Core.Repositories;
using Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class CursoRepositorio : CurdCoreRespository<Curso, int>, ICursoRepositorio
    {
        private readonly ApplicationDbContext _dbContext;
        public CursoRepositorio(ApplicationDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<IReadOnlyList<Curso>> FindByIdMateriaAsync(int id)
        {
            var response = await _dbContext.Set<Curso>().
                 Where(t =>  t.IdMateria == id  && t.Estado).
                 ToListAsync();

            return response;
        }

        public async override Task<Curso?> FindByIdAsync(int id)
        {
            var response = await _dbContext.Set<Curso>().
                 Include(t => t.Leccion).
                 FirstOrDefaultAsync(t => t.IdCurso == id);

            return response;
        }

        public async Task<Curso> FechtNameCurso(string name)
        {
            var response = await _dbContext.Set<Curso>().FirstOrDefaultAsync(t => t.Nombre.ToLower().Trim().Equals(name));

            return response;
        }
    }
}
