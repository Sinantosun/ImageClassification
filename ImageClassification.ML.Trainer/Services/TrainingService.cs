using ImageClassification.ML.Trainer.Models;
using Microsoft.ML;
using Microsoft.ML.Vision;

namespace ImageClassification.ML.Trainer.Services
{
    public class TrainingService
    {
        public void TrainModel()
        {
            var mlContext = new MLContext();

            var path = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "Data");
            var data = LoadDataFromFreeText(mlContext, path);

            var pipeline = mlContext.Transforms.Conversion.MapValueToKey("LabelAsKey", "Label")
            .Append(mlContext.MulticlassClassification.Trainers.ImageClassification(new ImageClassificationTrainer.Options
            {
                FeatureColumnName = "Image",
                LabelColumnName = "LabelAsKey",
                Arch = ImageClassificationTrainer.Architecture.MobilenetV2,
            }))
            .Append(mlContext.Transforms.Conversion
           .MapKeyToValue(
               outputColumnName: "PredictedLabel",
               inputColumnName: "PredictedLabel"));

            var model = pipeline.Fit(data);

            mlContext.Model.Save(model, data.Schema, "model.zip");
        }
        private IDataView LoadDataFromFreeText(MLContext mlContext, string folderPath)
        {
            var images = new List<ModelInput>();

            var directories = Directory.GetDirectories(folderPath);

            foreach (var directory in directories)
            {
                var label = Path.GetFileName(directory);

                var files = Directory.GetFiles(directory, "*.*")
                                     .Where(file => file.EndsWith(".jpg") || file.EndsWith(".png") || file.EndsWith(".jpeg"));

                foreach (var file in files)
                {
                    images.Add(new ModelInput
                    {
                        ImagePath = file,
                        Label = label,
                        Image = File.ReadAllBytes(file)
                    });
                }
            }

            return mlContext.Data.LoadFromEnumerable(images);
        }
    }
}
