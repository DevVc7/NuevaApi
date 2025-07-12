using Domain;
using Infraestructure.Core.Repositories.Interfaces;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IUsuarioRepositorio : ICurdCoreRespository<User, Guid>
    {
        Task<User?> FindByEmailAsync(string email);
        Task<User?> FindByPersonaAsync(string idPersona);
    }
}
