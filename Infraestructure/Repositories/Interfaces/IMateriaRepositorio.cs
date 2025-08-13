using Domain;
using Infraestructure.Core.Repositories.Interfaces;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IMateriaRepositorio : ICurdCoreRespository<Materia, int>
    {
        Task<Materia> FechtNameMateria(string name);
    }
}
