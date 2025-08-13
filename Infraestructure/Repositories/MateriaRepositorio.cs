using Domain;
using Infraestructure.Contexts;
using Infraestructure.Core.Repositories;
using Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class MateriaRepositorio : CurdCoreRespository<Materia, int>, IMateriaRepositorio
    {
        private readonly ApplicationDbContext _dbContext;
        public MateriaRepositorio(ApplicationDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<Materia> FechtNameMateria(string name)
        {
            var response = await _dbContext.Set<Materia>().FirstOrDefaultAsync(t => t.Descripcion.ToLower().Trim().Equals(name));

            return response;
        }

        public override async Task<IReadOnlyList<Materia>> FindAllAsync()
        {
            var response = await _dbContext.Set<Materia>().
                Include(t => t.Cursos).
                Where(t => t.Estado).ToListAsync();

            return response;
        }

        public override async Task<Materia?> FindByIdAsync(int id)
        {
            var response = await _dbContext.Set<Materia>().
                Include(t => t.Cursos).
                FirstOrDefaultAsync(t => t.IdMateria == id);

            return response;
        }
    }
}
