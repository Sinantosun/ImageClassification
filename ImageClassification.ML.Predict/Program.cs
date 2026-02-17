using ImageClassification.ML.Predict.Models;
using Microsoft.Extensions.ML;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("DefaultPolicy", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var path = Path.Combine(Directory.GetCurrentDirectory(), "MLModel", "model.zip");

builder.Services.AddPredictionEnginePool<ModelInput, ModelOutput>().FromFile(path, watchForChanges: true);
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseCors("DefaultPolicy");

if (app.Environment.IsDevelopment())
{

    app.MapOpenApi();
    app.UseSwagger();

    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
