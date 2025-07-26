using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.View
{
    public class QuestionsMeta<T>
    {
        public int TotalQuestions { get; set; }
        public Dictionary<string, int> Subjects { get; set; } = new();
        public List<string> Difficulties { get; set; } = new();
        public List<T> Questions { get; set; } = new();
    }

    public class QuestionFrontendDto
    {
        public Guid? Id { get; set; }
        public string Question { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Topic { get; set; } = string.Empty;
        public string Difficulty { get; set; } = string.Empty;
        public string Grade { get; set; } = string.Empty;
        public List<string> Options { get; set; } = new();
        public int CorrectAnswer { get; set; }
        public string Explanation { get; set; } = string.Empty;
        public string Hint { get; set; } = string.Empty;
    }

    public class QuestionDataResponse
    {
        public int TotalQuestions { get; set; }
        public Dictionary<string, int> Subjects { get; set; } = new();
        public List<string> Difficulties { get; set; } = new();
        public List<QuestionFrontendDto> Questions { get; set; } = new();
    }
}
