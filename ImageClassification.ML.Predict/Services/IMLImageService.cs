using ImageClassification.ML.Predict.Base;
using ImageClassification.ML.Predict.Models;

namespace ImageClassification.ML.Predict.Services
{
    public interface IMLImageService
    {
        Task<BaseResult<PredictionResult>> PredictAsync(IFormFile file);
    }
}
