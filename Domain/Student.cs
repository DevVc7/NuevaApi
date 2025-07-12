using System.Text.Json.Serialization;

namespace Domain
{
    public class Student
    {
		public Guid Id { get; set; }
		public string? Grade { get; set; }
        public int Progress { get; set; }
        public string? LastActive { get; set; }
        public string? Code { get; set; }
        public string? Note { get; set; }
        public Guid UsersId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public virtual User? User { get; set; }

        public virtual ICollection<StudentSubjects>? StudentSubjects { get; set; }

    }
}