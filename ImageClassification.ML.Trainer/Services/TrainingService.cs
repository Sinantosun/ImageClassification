using ImageClassification.Shared.Models;
using ImageClassification.Shared.Settings;
using Microsoft.ML;
using Microsoft.ML.Vision;

namespace ImageClassification.ML.Trainer.Services
{
    public class TrainingService
    {
        public void TrainModel(string path)
        {
            try
            {
                var mlContext = new MLContext();

                if (!Directory.Exists(path))
                {
                    Console.WriteLine("Verilen klasör bulunamadı.");
                    return;
                }

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
                string outputPath = ModelStorageConfig.ModelDirectory;
                mlContext.Model.Save(model, data.Schema, Path.Combine(outputPath, "model.zip"));
                Console.WriteLine("Eğitim tamamlandı zip dosyası hazırlandı..");
            }
            catch (Exception)
            {
                Console.WriteLine("Eğitim aşamasında bir hata oluştu.");
            }
        }
        private IDataView LoadDataFromFreeText(MLContext mlContext, string folderPath)
        {
            var images = new List<ModelInput>();

            var directories = Directory.GetDirectories(folderPath);

            foreach (var directory in directories)
            {
                var label = Path.GetFileName(directory);

                var files = Directory.GetFiles(directory, "*.*").Where(file => file.EndsWith(".jpg") || file.EndsWith(".png") || file.EndsWith(".jpeg"));

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
