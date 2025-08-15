using Application.Grados.Dtos;
using Application.Grados.Services.Interfaces;
using Domain;
using Infraestructure.Repositories.Interfaces;

namespace Application.Grados.Services;

public class GradoServices : IGradoServices
{
    private readonly IGradoRepositorio _gradoRepositorio;

    public GradoServices(IGradoRepositorio gradoRepositorio)
    {
        _gradoRepositorio = gradoRepositorio;
    }

    public async Task<IReadOnlyList<GradoDto>> FetchAll()
    {
        var grados = await _gradoRepositorio.FindAllAsync();
        return grados.Select(g => new GradoDto
        {
            IdGrado = g.IdGrado,
            Titulo = g.Titulo,
            Estado = g.Estado
        }).ToList();
    }

    public async Task<GradoDto> FetchById(int id)
    {
        var g = await _gradoRepositorio.FindByIdAsync(id);
        return new GradoDto
        {
            IdGrado = g.IdGrado,
            Titulo = g.Titulo,
            Estado = g.Estado
        };
    }

    public Task<OperationResult<Grado>> Save(SaveGradoDto data)
    {
        throw new NotImplementedException();
    }

    public Task<OperationResult<Grado>> Update(int id, SaveGradoDto data)
    {
        throw new NotImplementedException();
    }

    public Task<OperationResult<Grado>> Delete(int id)
    {
        throw new NotImplementedException();
    }
}
