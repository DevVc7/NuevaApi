namespace Domain.View
{
    public class StudentsMeta<T>
    {
        public int TotalStudents { get; set; }
        public int ActiveStudents { get; set; }
        public int AverageProgress { get; set; }
        public IList<string>? Grades { get; set; }
        public ICollection<T>? Students { get; set; }
    }
}
