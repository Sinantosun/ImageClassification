using ImageClassification.ML.Predict.Base;

public class RequestSizeMiddleware
{
    private readonly RequestDelegate _next;
    private const long _maxBytes = 5_000_000;

    public RequestSizeMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.ContentLength is long length && length > _maxBytes)
        {
            double mb = Math.Round(length / 1024d / 1024d, 2);

            var response = BaseResult<string>.Fail($"Dosya boyutu ({mb} MB) çok büyük. Maximum 5 MB dosya seçiniz.");
            context.Response.StatusCode = StatusCodes.Status413PayloadTooLarge;
            await context.Response.WriteAsJsonAsync(response);
            return;
        }

        await _next(context);
    }
}
