namespace Application.Studens.Dtos.StudentSubje
{
    public class StudentSubjectsSaveDto
    {
        public Guid SubjectId { get; set; }
        public Guid StudentId { get; set; }
        public int Progress { get; set; } = 0; 
    }
}
