using Domain;
using Infraestructure.Core.Repositories.Interfaces;

namespace Infraestructure.Repositories.Interfaces
{
    public interface ISubjectRepositorio : ICurdCoreRespository<Subjects, string>
    {
        Task<Subjects> GetSubjectsName(string name);
    }
}
