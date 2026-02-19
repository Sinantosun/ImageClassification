using ImageClassification.ML.Trainer.Services;

var service = new TrainingService();
Console.Write("Eğitim İçin Lütfen Veri Setinin Yolunu Yapıştırın: ");
string path = Console.ReadLine();
if (string.IsNullOrEmpty(path))
{
    Console.WriteLine("Lütfen veri setinin yolunu yapıştırın.");
    return;
}
service.TrainModel(path);

