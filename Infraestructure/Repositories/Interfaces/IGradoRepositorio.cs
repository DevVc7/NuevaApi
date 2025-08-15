using Domain;
using Infraestructure.Core.Repositories;
using Infraestructure.Core.Repositories.Interfaces;

namespace Infraestructure.Repositories.Interfaces;

public interface IGradoRepositorio : ICurdCoreRespository<Grado, int>
{
    Task<Grado> FechtNombreGrado(string nombre);
}
