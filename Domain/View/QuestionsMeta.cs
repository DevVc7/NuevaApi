using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.View
{
    public class QuestionDataResponse
    {
        public int TotalQuestions { get; set; }
        public Subjects? Subjects { get; set; }
        public IList<string>? Difficulties { get; set; }
        public IList<Pregunta>? Questions { get; set; }
    }

    public class Subjects
    {
        public int Matematica { get; set; }
        public int Comunicacion { get; set; }

    }
}
