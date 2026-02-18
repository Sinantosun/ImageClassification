using ImageClassification.ML.Predict.Base;

namespace ImageClassification.ML.Predict.Middlewares
{
    public class CustomErrorMiddleware(RequestDelegate _next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                var response = BaseResult<object>.Fail();
                await context.Response.WriteAsJsonAsync(response);  
            }
        }
    }
}
