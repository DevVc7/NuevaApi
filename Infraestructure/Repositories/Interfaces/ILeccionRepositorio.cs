using Domain;
using Infraestructure.Core.Repositories.Interfaces;

namespace Infraestructure.Repositories.Interfaces
{
    public interface ILeccionRepositorio : ICurdCoreRespository<Leccion, int>
    {
        Task<IReadOnlyList<Leccion>> FindByIdCursoAsync(int id);
    }
}
