using Domain;
using Domain.View;
using Infraestructure.Contexts;
using Infraestructure.Core.Repositories;
using Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Infraestructure.Repositories
{
    public class PreguntaRepositorio : CurdCoreRespository<Pregunta, int>, IPreguntaRepositorio
    {
        private readonly ApplicationDbContext _context;
        public PreguntaRepositorio(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<QuestionDataResponse> BusquedaPaginado()
        {
            var query = _context.Set<Pregunta>()
                .Include(q => q.Grado)
                .Include(q => q.Materia)
                .Include(q => q.OpcionesRpt)
                .Where(q => q.Estado)
                .OrderByDescending(t => t.IdPregunta);

            var subjectCounts = await query
                .GroupBy(q => q.Materia.Descripcion)
                .Select(g => new{ Materia = g.Key, Count = g.Count()})
                .ToListAsync();

            int matematica = subjectCounts.FirstOrDefault(s => s.Materia == "Matematica")?.Count ?? 0;
            int comunicacion = subjectCounts.FirstOrDefault(s => s.Materia == "Comunicacion")?.Count ?? 0;

            var subjects = new Subjects
            {
                Comunicacion = comunicacion,
                Matematica = matematica
            };

            var difficulties = await query
                .Select(q => q.Dificultad)
                .Distinct()
                .ToListAsync();

            var questions = await query.ToListAsync();

            return new QuestionDataResponse
            {
                TotalQuestions = questions.Count,
                Subjects = subjects,
                Difficulties = difficulties,
                Questions = questions
            };

        }

        public async override Task<IReadOnlyList<Pregunta>> FindAllAsync()
        {
            var response = await _context.Set<Pregunta>().Include(s => s.Grado).ToListAsync();

            return response;
        }

        public override Task<Pregunta?> FindByIdAsync(int id)
        {
            var response = _context.Set<Pregunta>()
                .Include(t => t.Materia)
                .Include(s => s.Grado)
                .Include(op => op.OpcionesRpt)
                .FirstOrDefaultAsync(t => t.IdPregunta == id);
            return response;    
        }


        public async Task<IReadOnlyList<Pregunta>> FindAllMateriaAsync(int id)
        {
            var response = await _context.Set<Pregunta>()
                .Include(s => s.Materia).ThenInclude(c => c.Cursos)
                .Where(t => t.IdMateria == id && t.Estado ).ToListAsync();

            return response;
        }

        public async Task<IReadOnlyList<Pregunta>> FindAllQuestionMateria(PreguntaView view)
        {
            var response = await _context.Set<Pregunta>()
                .Include(q => q.Grado)
                .Include(q => q.Materia)
                .Include(q => q.OpcionesRpt)
                .Where(t => t.IdMateria == view.IdMateria && t.idCurso == view.IdCurso && t.IdGrado == view.IdGrado && t.Estado).ToListAsync();

            return response;
        }


        private string MapDifficulty(byte? level)
        {
            return level switch
            {
                1 => "facil",
                2 => "medio",
                3 => "dificil",
                _ => "desconocido"
            };
        }


        private int ParseCorrectAnswer(string? answer)
        {
            if (int.TryParse(answer, out var index))
                return index;

            // En caso de true/false, se podría usar: "true" => 1, "false" => 0
            if (answer?.ToLower() == "true") return 1;
            if (answer?.ToLower() == "false") return 0;

            return -1; // no válido
        }

        private List<string> ParseOptions(string? rawOptions)
        {
            if (string.IsNullOrWhiteSpace(rawOptions)) return new List<string>();

            rawOptions = rawOptions.Trim();

            // Si ya parece JSON (empieza con [ y termina con ])
            if (rawOptions.StartsWith("[") && rawOptions.EndsWith("]"))
            {
                try
                {
                    return JsonSerializer.Deserialize<List<string>>(rawOptions) ?? new List<string>();
                }
                catch
                {
                    // Si falla, continúa al siguiente intento
                }
            }

            // Si es texto plano separado por comas, lo convertimos a lista
            return rawOptions
                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                .Select(o => o.Trim())
                .ToList();
        }

        public async Task<Pregunta?> FindEasiestQuestionAsync(int idCurso)
        {
            // Encuentra la pregunta con la dificultad "Facil" en la primera lección del curso.
            return await _context.Set<Pregunta>()
                .Where(q => q.idCurso == idCurso && q.Dificultad == "Facil")
                .OrderBy(q => q.IdLeccion)
                .FirstOrDefaultAsync();
        }

        public async Task<Pregunta?> FindNextDifficultyAsync(int idLeccion, string dificultadActual)
        {
            // Lógica para ordenar las dificultades (esto puede variar, aquí se usa un orden fijo).
            var ordenDificultad = new Dictionary<string, int>
            {
                { "Facil", 1 }, { "Medio", 2 }, { "Dificil", 3 }
            };

            var dificultadSiguiente = ordenDificultad.FirstOrDefault(d => d.Value == ordenDificultad[dificultadActual] + 1).Key;

            return await _context.Set<Pregunta>()
                .Where(q => q.IdLeccion == idLeccion && q.Dificultad == dificultadSiguiente)
                .FirstOrDefaultAsync();
        }

        public async Task<Pregunta?> FindPreviousDifficultyAsync(int idLeccion, string dificultadActual)
        {
            var ordenDificultad = new Dictionary<string, int>
            {
                { "Facil", 1 }, { "Medio", 2 }, { "Dificil", 3 }
            };

            var dificultadAnterior = ordenDificultad.FirstOrDefault(d => d.Value == ordenDificultad[dificultadActual] - 1).Key;

            return await _context.Set<Pregunta>()
                .Where(q => q.IdLeccion == idLeccion && q.Dificultad == dificultadAnterior)
                .FirstOrDefaultAsync();
        }

        public async Task<Pregunta?> FindNextLessonEasiestQuestionAsync(int idCurso, int idLeccionActual)
        {
            return await _context.Set<Pregunta>()
                .Where(q => q.idCurso == idCurso && q.IdLeccion > idLeccionActual)
                .OrderBy(q => q.IdLeccion)
                .ThenBy(q => q.Dificultad)
                .FirstOrDefaultAsync();
        }
    }
}
