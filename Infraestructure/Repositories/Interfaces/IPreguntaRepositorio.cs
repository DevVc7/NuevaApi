using Domain;
using Domain.View;
using Infraestructure.Core.Repositories.Interfaces;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IPreguntaRepositorio : ICurdCoreRespository<Pregunta, int>
    {
        Task<QuestionDataResponse> BusquedaPaginado();
        Task<IReadOnlyList<Pregunta>> FindAllMateriaAsync(int id);
    }
}
