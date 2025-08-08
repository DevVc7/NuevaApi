using Domain;
using Infraestructure.Contexts;
using Infraestructure.Core.Repositories;
using Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class RolUsuarioRepositorio : CurdCoreRespository<RolUser, int>, IRolUsuarioRepositorio
    {
        private readonly ApplicationDbContext _dbContext;
        public RolUsuarioRepositorio(ApplicationDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public override Task<RolUser?> FindByIdAsync(int id)
        {
            var response = _dbContext.Set<RolUser>().
                    Include(t => t.Rol).
                    Include(t => t.Usuario).
                    FirstOrDefaultAsync(e => e.IdUsuario == id);

            return response;
        }


    }
}
