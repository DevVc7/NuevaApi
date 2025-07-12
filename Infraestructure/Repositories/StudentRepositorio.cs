using Domain;
using Infraestructure.Contexts;
using Infraestructure.Core.Repositories;
using Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Infraestructure.Repositories
{
    public class StudentRepositorio : CurdCoreRespository<Student, Guid>, IStudentRepositorio
    {
        private readonly ApplicationDbContext _dbContext;
        public StudentRepositorio(ApplicationDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async override Task<Student?> FindByIdAsync(Guid id)
        {
            return await _dbContext.Set<Student>().Where(t => t.Id == id).Include(t => t.User).Include(t => t.StudentSubjects).ThenInclude(ss => ss.Subject).FirstOrDefaultAsync();
        }

        public override async Task<IReadOnlyList<Student>> FindAllAsync()
        {
            var response = await _dbContext.Set<Student>().Include(t => t.User).Include(t => t.StudentSubjects).ThenInclude(ss => ss.Subject).ToListAsync();

            return response;
        }


        public async Task<StudentsMeta<Student>> BusquedaPaginado()
        {

            var estudiantes = await _dbContext.Set<Student>().Include(s => s.User).Include(s => s.StudentSubjects).ThenInclude(ss => ss.Subject).ToListAsync();

            var totalStudents = estudiantes.Count;

            var activeStudents = estudiantes.Count(e => e.LastActive == "Hoy");

            // Promedio de progreso por estudiante (promediando cada uno)
            var averageProgress = totalStudents > 0
                ? (int)Math.Round(estudiantes.Average(e => e.Progress))
                : 0;

            var grades = estudiantes
                .Where(e => !string.IsNullOrWhiteSpace(e.Grade))
                .Select(e => e.Grade)
                .Distinct()
                .ToList();

            var response = new StudentsMeta<Student>
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
