using Domain;
using Infraestructure.Contexts;
using Infraestructure.Core.Repositories;
using Infraestructure.Repositories.Interfaces;

namespace Infraestructure.Repositories
{
    public class EscuelaRepositorio : CurdCoreRespository<Escuela, int>, IEscuelaRepositorio
    {
        private readonly ApplicationDbContext _dbContext;
        public EscuelaRepositorio(ApplicationDbContext context) : base(context)
        {
            _dbContext = context;
        }
    }
}
