using Domain;
using Infraestructure.Contexts;
using Infraestructure.Core.Repositories;
using Infraestructure.Repositories.Interfaces;

namespace Infraestructure.Repositories
{
    public class PersonaRepositorio : CurdCoreRespository<Person, int>, IPersonaRepositorio
    {
        public PersonaRepositorio(ApplicationDbContext context) : base(context)
        {
        }
    }
}
