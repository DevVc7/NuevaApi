using Application.Preguntas.Services.Interfaces;
using Infraestructure.Repositories.Interfaces;
using Microsoft.ML;
using MLModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Preguntas.Services
{
    public class PredictionService : IPredictionService
    {
        private readonly IRespuestaUsuarioRepositorio _respuestaUsuarioRepositorio;
        private readonly IPreguntaRepositorio _preguntaRepositorio;
        private readonly MLContext _mlContext;
        private static ITransformer? _model;
        private static readonly string _modelPath = Path.Combine(System.AppContext.BaseDirectory, "model.zip");

        public PredictionService(IRespuestaUsuarioRepositorio respuestaUsuarioRepositorio, IPreguntaRepositorio preguntaRepositorio)
        {
            _respuestaUsuarioRepositorio = respuestaUsuarioRepositorio;
            _preguntaRepositorio = preguntaRepositorio;
            _mlContext = new MLContext(seed: 0);
        }

        private async Task TrainModelAsync()
        {
            var allAnswers = await _respuestaUsuarioRepositorio.GetAllAsync();
            var trainingData = allAnswers.Select(a => new ModelInput
            {
                UserId = a.IdUsuario,
                QuestionId = a.IdPregunta,
                Label = (float)a.PuntajeObtenido
            }).ToList();

            if (!trainingData.Any()) return;

            var trainer = new ModelTrainer();
            trainer.Train(trainingData);
            trainer.Save(_modelPath);
        }

        private async Task LoadModelAsync()
        {
            if (_model == null)
            {
                if (!File.Exists(_modelPath))
                {
                    await TrainModelAsync();
                }
                _model = _mlContext.Model.Load(_modelPath, out _);
            }
        }

        public async Task<int> GetNextQuestion(int userId, int courseId)
        {
            await LoadModelAsync();

            var predictionEngine = _mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(_model);

            var answeredQuestions = (await _respuestaUsuarioRepositorio.FindAllByUserAsync(userId, courseId))
                                    .Select(r => r.IdPregunta).ToHashSet();

            var allQuestionsInCourse = await _preguntaRepositorio.FindAllByCourseAsync(courseId);

            var candidateQuestions = allQuestionsInCourse.Where(q => !answeredQuestions.Contains(q.IdPregunta)).ToList();

            if (!candidateQuestions.Any())
            {
                // User has answered all questions in the course
                return 0; 
            }

            var bestQuestion = candidateQuestions
                .Select(q => new
                {
                    QuestionId = q.IdPregunta,
                    PredictedScore = predictionEngine.Predict(new ModelInput { UserId = userId, QuestionId = q.IdPregunta }).Score
                })
                .OrderByDescending(p => p.PredictedScore)
                .FirstOrDefault();

            return bestQuestion?.QuestionId ?? 0;
        }
    }
}
