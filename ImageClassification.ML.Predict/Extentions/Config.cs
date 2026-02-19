using ImageClassification.ML.Predict.Base;
using ImageClassification.ML.Predict.Models;
using ImageClassification.ML.Predict.Services;
using ImageClassification.Shared.Models;
using ImageClassification.Shared.Settings;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.ML;

namespace ImageClassification.ML.Predict.Extentions
{
    public static class Config
    {
        public static void RegisterProjectExtentions(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("DefaultPolicy", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });
            services.AddRateLimiter(cfg =>
            {
                cfg.AddFixedWindowLimiter("Fixed", opts =>
                {
                    opts.QueueLimit = 0;
                    opts.PermitLimit = 50;
                    opts.Window = TimeSpan.FromMinutes(5);
                });
                cfg.OnRejected = async (context, token) =>
                {
                    context.HttpContext.Response.ContentType = "application/json";
                    context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                    var response = BaseResult<object>.Fail("Çok fazla yaptınız lütfen biraz bekleyip tekrar deneyin.");
                    await context.HttpContext.Response.WriteAsJsonAsync(response);
                };
            });
            var path = Path.Combine(ModelStorageConfig.ModelDirectory, "model.zip");
            services.AddPredictionEnginePool<ModelInput, ModelOutput>().FromFile(path, watchForChanges: true); //watchForChanges production da önerilmez.
            services.AddSingleton<IMLImageService, MLImageService>();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }
    }
}
