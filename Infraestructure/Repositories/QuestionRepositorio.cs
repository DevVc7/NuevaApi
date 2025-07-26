using Domain;
using Domain.View;
using Infraestructure.Contexts;
using Infraestructure.Core.Repositories;
using Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infraestructure.Repositories
{
    public class QuestionRepositorio : CurdCoreRespository<Question, Guid>, IQuestionRepositorio
    {
        private readonly ApplicationDbContext _context;
        public QuestionRepositorio(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<QuestionDataResponse> BusquedaPaginado()
        {
            var questions = await _context.Set<Question>()
            .Include(q => q.Subjects)
            .ToListAsync();

            var questionList = questions.Select(q => new QuestionFrontendDto
            {
                Id = q.Id,
                Question = q.Content,
                Subject = q.Subjects != null ? q.Subjects.Name.ToLower() : "sin materia",
                Topic =  "",
                Difficulty = MapDifficulty(q.Difficulty),
                Grade =  "",
                Options = ParseOptions(q.Options),
                CorrectAnswer = ParseCorrectAnswer(q.CorrectAnswer),
                Explanation =  "",
                Hint = ""
            }).ToList();

            var subjects = questionList
                .GroupBy(q => q.Subject)
                .ToDictionary(g => g.Key, g => g.Count());

            var difficulties = questionList
                .Select(q => q.Difficulty)
                .Distinct()
                .ToList();

            return new QuestionDataResponse
            {
                TotalQuestions = questionList.Count,
                Subjects = subjects,
                Difficulties = difficulties,
                Questions = questionList
            };
        }

        public async override Task<IReadOnlyList<Question>> FindAllAsync()
        {
            var response = await _context.Set<Question>().Include(s => s.Subjects).ToListAsync();

            return response;
        }

        public override Task<Question?> FindByIdAsync(Guid id)
        {
            var response = _context.Set<Question>().Where(t => t.Id == id).Include(s => s.Subjects).FirstOrDefaultAsync();
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




    }
}
