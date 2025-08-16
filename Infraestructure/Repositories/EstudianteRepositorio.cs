using Domain;
using Domain.View;
using Infraestructure.Contexts;
using Infraestructure.Core.Repositories;
using Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Infraestructure.Repositories
{
    public class EstudianteRepositorio : CurdCoreRespository<Estudiante, int>, IEstudianteRepositorio
    {
        private readonly ApplicationDbContext _dbContext;
        public EstudianteRepositorio(ApplicationDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async override Task<Estudiante?> FindByIdAsync(int id)
        {
            return await _dbContext.Set<Estudiante>().
                Include(t => t.Usuario).
                Include(t => t.Grado).
                FirstOrDefaultAsync(t => t.IdUsuario == id);
        }

        public override async Task<IReadOnlyList<Estudiante>> FindAllAsync()
        {
            var response = await _dbContext.Set<Estudiante>().
                Include(t => t.Usuario).
                Include(t => t.Grado).
                ToListAsync();

            return response;
        }


        public async Task<StudentsMeta<Estudiante>> BusquedaPaginado()
        {

            var estudiantes = await _dbContext.Set<Estudiante>().
                Include(s => s.Usuario).
                Include(s => s.Grado).
                Where(s => s.Usuario.Estado).
                ToListAsync();

            var totalStudents = estudiantes.Count;
            var activeStudents = 0;
            var averageProgress =  0;


            // Promedio de progreso por estudiante (promediando cada uno)
            //var activeStudents = estudiantes.Count(e => e.LastActive == "Hoy");
            //var averageProgress = totalStudents > 0 ? (int)Math.Round(estudiantes.Average(e => e.Progress)) : 0;

            var grades = estudiantes
                .Where(e => !string.IsNullOrWhiteSpace(e.Grado.Titulo))
                .Select(e => e.Grado.Titulo)
                .Distinct()
                .ToList();

            var response = new StudentsMeta<Estudiante>
            {
                TotalStudents = totalStudents,
                ActiveStudents = activeStudents,
                AverageProgress = averageProgress,
                Grades = grades,
                Students = estudiantes,
            };

            return response;
        }
    }
}
