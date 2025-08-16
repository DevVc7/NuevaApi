namespace Application.Preguntas.Services.Interfaces
{
    public interface IPredictionService
    {
        Task<int> GetNextQuestion(int userId, int courseId);
    }
}
