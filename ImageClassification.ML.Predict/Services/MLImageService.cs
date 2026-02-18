using ImageClassification.ML.Predict.Base;
using ImageClassification.ML.Predict.Models;
using Microsoft.Extensions.ML;

namespace ImageClassification.ML.Predict.Services
{
    public class MLImageService : IMLImageService
    {
        private readonly PredictionEnginePool<ModelInput, ModelOutput> _predictionEnginePool;

        public MLImageService(PredictionEnginePool<ModelInput, ModelOutput> predictionEnginePool)
        {
            _predictionEnginePool = predictionEnginePool;
        }
        public async Task<BaseResult<PredictionResult>> PredictAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BaseResult<PredictionResult>.Fail("Lütfen Dosya Seçiniz.");

            byte[] imageBytes = await GetImageBytes(file);

            var input = new ModelInput { Image = imageBytes };
            var prediction = _predictionEnginePool.Predict(input);

            var maxScore = (prediction.Score.Max() * 100).ToString("0.00");
            float score = float.Parse(maxScore);
            if (score == 0)
            {
                var result = Result("Bu görseli Hiç tanıyamadım..", score);
                return BaseResult<PredictionResult>.Success(result);
            }
            else if (score < 85f)
            {
                var result = Result($"Bu görseli tanıyamadım.. Ama %{score} İhtimalle Bu Bir {prediction.Prediction} Olabilir.", score);
                return BaseResult<PredictionResult>.Success(result);
            }
            else
            {
                var result = Result(prediction.Prediction, score);
                return BaseResult<PredictionResult>.Success(result);
            }
        }

        private async Task<byte[]> GetImageBytes(IFormFile file)
        {
            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            return ms.ToArray();
        }
        private PredictionResult Result(string prediction, float score)
        {
            return new PredictionResult
            {
                Prediction = prediction,
                Score = score
            };
        }
    }
}
