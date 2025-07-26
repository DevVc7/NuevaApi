namespace Application.Questions.Dto
{
    public class QuestionSaveDto
    {
        public string? Subject { get; set; }
        public string? Type { get; set; } 
        public string? Content { get; set; }
        public string? Options { get; set; }
        public string? CorrectAnswer { get; set; }
        public decimal Points { get; set; }
        public byte? Difficulty { get; set; }
    }
}
