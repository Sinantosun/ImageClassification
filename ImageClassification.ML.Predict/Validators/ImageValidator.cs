using ImageClassification.ML.Predict.Base;
using ImageClassification.ML.Predict.Models;

namespace ImageClassification.ML.Predict.Validators
{
    public class ImageValidator
    {
        public BaseResult<PredictionResult> Validate(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BaseResult<PredictionResult>.Fail("Lütfen Dosya Seçiniz.");

            string[] extentions = { ".jpg", ".jpeg", ".png" };
            string fileExt = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!extentions.Contains(fileExt))
                return BaseResult<PredictionResult>.Fail("Lütfen JPG JPEG ve png uzantılı dosyalar seçiniz.");

            long maxFileSize = 5 * 1024 * 1024;
            if (file.Length > maxFileSize)
                return BaseResult<PredictionResult>.Fail("Dosya boyutu en fazla 5 MB olabilir.");

            var result = new PredictionResult();
            return BaseResult<PredictionResult>.Success(result);
        }
    }
}
