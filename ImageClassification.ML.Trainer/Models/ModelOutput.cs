namespace ImageClassification.ML.Trainer.Models
{
    public class ModelOutput
    {
        public string Prediction { get; set; } // Tahmin edilen kategori adı
        public float[] Score { get; set; } // Olasılık değerleri
    }
}
