using ImageClassification.ML.Predict.Extentions;
using ImageClassification.ML.Predict.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterProjectExtentions();
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors("DefaultPolicy");
app.UseRateLimiter();
app.UseMiddleware<CustomErrorMiddleware>();
app.UseMiddleware<RequestSizeMiddleware>();

app.UseAuthorization();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers().RequireRateLimiting("Fixed");
app.Run();
