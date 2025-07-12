using Domain;
using Infraestructure.Contexts;
using Infraestructure.Core.Repositories;
using Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class SubjectRepositorio : CurdCoreRespository<Subjects, string>, ISubjectRepositorio
    {
        private readonly ApplicationDbContext _dbContext;
        public SubjectRepositorio(ApplicationDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<Subjects> GetSubjectsName(string name)
        {
            var response = await _dbContext.Set<Subjects>().Where( t => t.Name.Equals(name)).FirstOrDefaultAsync();

            return response;
        }
    }
}
