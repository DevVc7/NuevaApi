using Microsoft.ML;
using Microsoft.ML.Trainers;
using System;
using System.Collections.Generic;
using System.IO;

namespace MLModel
{
    public class ModelTrainer
    {
        private readonly MLContext _mlContext;
        private ITransformer? _trainedModel;

        public ModelTrainer()
        {
            _mlContext = new MLContext(seed: 0);
        }

        public void Train(IEnumerable<ModelInput> trainingData)
        {
            IDataView trainingDataView = _mlContext.Data.LoadFromEnumerable(trainingData);

            // El pipeline de entrenamiento ahora hará lo siguiente:
            // 1. Convertir la característica de texto 'Difficulty' en un vector numérico (One-Hot Encoding).
            // 2. Combinar todas las características en una sola columna llamada 'Features'.
            // 3. Usar un algoritmo de regresión (LightGbm) para entrenar el modelo, prediciendo 'Label' (el puntaje) a partir de 'Features'.

            var pipeline = _mlContext.Transforms.Categorical.OneHotEncoding(inputColumnName: "Difficulty", outputColumnName: "DifficultyEncoded")
                .Append(_mlContext.Transforms.Concatenate("Features",
                    "UserId",
                    "QuestionId",
                    "DifficultyEncoded",
                    "UserHistoricalScore",
                    "QuestionHistoricalScore"))
                .Append(_mlContext.Regression.Trainers.LightGbm(labelColumnName: "Label", featureColumnName: "Features"));

            _trainedModel = pipeline.Fit(trainingDataView);
        }

        public void Save(string modelPath)
        {
            if (_trainedModel == null)
            {
                throw new InvalidOperationException("El modelo debe ser entrenado antes de guardarlo.");
            }

            _mlContext.Model.Save(_trainedModel, null, modelPath);
        }
    }
}
