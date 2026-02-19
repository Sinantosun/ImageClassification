using ImageClassification.ML.Predict.Models;
using ImageClassification.ML.Predict.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

[ApiController]
[Route("api/[controller]")]
public class ClassificationController(IMLImageService _service) : ControllerBase
{
    [RequestFormLimits(MultipartBodyLengthLimit = 5000000)]
    [HttpPost("predict")]
    public async Task<IActionResult> Predict(IFormFile? file)
    {
        var result = await _service.PredictAsync(file);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}