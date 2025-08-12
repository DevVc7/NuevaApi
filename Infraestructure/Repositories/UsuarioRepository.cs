using Domain;
using Infraestructure.Contexts;
using Infraestructure.Core.Repositories;
using Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class UsuarioRepository : CurdCoreRespository<User, int>, IUsuarioRepositorio
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User?> FindByEmailAsync(string email)
        {
            return await _context.Set<User>()
                .Where(t => t.Correo.ToUpper().Equals(email.ToUpper()))
                .FirstOrDefaultAsync();
        }

        

        public async Task<User?> FindByPersonaAsync(string idPersona)
        {
            return await _context.Set<User>()
                .FirstOrDefaultAsync();
        }

        public async override Task<User?> FindByIdAsync(int id)
        {
            return await _context.Set<User>()
                .Include(t => t.Escuela)
                .Where(t => t.IdUsuario == id)
                .FirstOrDefaultAsync();
        }
    }
}
