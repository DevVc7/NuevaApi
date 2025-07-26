namespace Domain
{
    public class Question
    {
        public Guid? Id { get; set; }
        public Guid? SubjectId { get; set; }
        public Guid? SubcategoryId { get; set; }
        public string? Type { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string? Options { get; set; }
        public string? CorrectAnswer { get; set; }
        public decimal? Points { get; set; }
        public byte? Difficulty { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Subjects? Subjects { get; set; }
    }
}
