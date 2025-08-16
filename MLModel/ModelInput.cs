using Microsoft.ML.Data;

namespace MLModel
{
    public class ModelInput
    {
        [LoadColumn(0)]
        public float UserId { get; set; }

        [LoadColumn(1)]
        public float QuestionId { get; set; }

        [LoadColumn(2)]
        public string Difficulty { get; set; }

        [LoadColumn(3)]
        public float UserHistoricalScore { get; set; }

        [LoadColumn(4)]
        public float QuestionHistoricalScore { get; set; }

        [LoadColumn(5)]
        public float Label { get; set; } // PuntajeObtenido
    }
}
