using Domain;
using Infraestructure.Contexts;
using Infraestructure.Core.Repositories;
using Infraestructure.Repositories.Interfaces;

namespace Infraestructure.Repositories
{
    public class OpcionRepositorio : CurdCoreRespository<OpcionesRpt, int>, IOpcionRepositorio
    {
        public OpcionRepositorio(ApplicationDbContext context) : base(context)
        {
        }
    }
}
