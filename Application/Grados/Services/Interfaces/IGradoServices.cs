using Application.Grados.Dtos;
using Domain;

namespace Application.Grados.Services.Interfaces;

public interface IGradoServices
{
    Task<IReadOnlyList<GradoDto>> FetchAll();
    Task<GradoDto> FetchById(int id);
    Task<OperationResult<Grado>> Save(SaveGradoDto data);
    Task<OperationResult<Grado>> Update(int id, SaveGradoDto data);
    Task<OperationResult<Grado>> Delete(int id);
}
