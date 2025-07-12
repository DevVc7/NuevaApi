using Domain;

namespace Application.Studens.Dtos.StudentSubje
{
    public class StudentSubjectsDto
    {
        public Guid Id { get; set; }
        public Guid SubjectId { get; set; }
        public Guid StudentId { get; set; }
        public int Progress { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public virtual Subjects? Subject { get; set; }
    }
}
