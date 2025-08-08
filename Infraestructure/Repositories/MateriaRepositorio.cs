using Domain;
using Infraestructure.Contexts;
using Infraestructure.Core.Repositories;
using Infraestructure.Repositories.Interfaces;

namespace Infraestructure.Repositories
{
    public class MateriaRepositorio : CurdCoreRespository<Materia, int>, IMateriaRepositorio
    {
        private readonly ApplicationDbContext _dbContext;
        public MateriaRepositorio(ApplicationDbContext context) : base(context)
        {
            _dbContext = context;
        }
    }
}
