using ImageClassification.ML.Trainer.Services;

var service = new TrainingService();
Console.WriteLine("Train Başladı.");
service.TrainModel();
Console.WriteLine("Eğitim tamamlandı zip dosyası hazırlandı..");

