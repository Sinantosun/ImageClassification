using ImageClassification.ML.Predict.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ML;

[ApiController]
[Route("api/[controller]")]
public class ClassificationController : ControllerBase
{
    private readonly PredictionEnginePool<ModelInput, ModelOutput> _predictionEnginePool;

    public ClassificationController(PredictionEnginePool<ModelInput, ModelOutput> predictionEnginePool)
    {
        _predictionEnginePool = predictionEnginePool;
    }

    [HttpPost("classify")]
    public async Task<IActionResult> Classify(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Lütfen bir resim yükleyin.");

        using var ms = new MemoryStream();
        await file.CopyToAsync(ms);
        byte[] imageBytes = ms.ToArray();

        var input = new ModelInput { Image = imageBytes };
        var prediction = _predictionEnginePool.Predict(input);
        var maxScore = (prediction.Score.Max() * 100).ToString("0.00");
        if (float.Parse(maxScore) < 85f)
        {
            return Ok(new
            {
                prediction = $"Bu görseli tanıyamadım.. Ama %{float.Parse(maxScore)} İhtimalle Bu Bir {prediction.Prediction} Olabilir.",
                score = float.Parse(maxScore)
            });
        }
        else
        {
            return Ok(new
            {
                prediction = prediction.Prediction,
                score = float.Parse(maxScore)
            });
        }
            
    }
}