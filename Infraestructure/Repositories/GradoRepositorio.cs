using Domain;
using Infraestructure.Contexts;
using Infraestructure.Core.Repositories;
using Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories;

public class GradoRepositorio : CurdCoreRespository<Grado, int>, IGradoRepositorio
{
    private readonly ApplicationDbContext _dbContext;
    public GradoRepositorio(ApplicationDbContext context) : base(context)
    {
        _dbContext = context;
    }

    public override async Task<IReadOnlyList<Grado>> FindAllAsync()
    {
        return await _dbContext.Set<Grado>()
            .Where(t => t.Estado)
            .ToListAsync();
    }

    public override async Task<Grado?> FindByIdAsync(int id)
    {
        return await _dbContext.Set<Grado>()
            .FirstOrDefaultAsync(t => t.IdGrado == id);
    }

    public async Task<Grado> FechtNombreGrado(string nombre)
    {
        return await _dbContext.Set<Grado>().FirstOrDefaultAsync(t => t.Titulo.ToLower().Trim().Equals(nombre));
    }
}
