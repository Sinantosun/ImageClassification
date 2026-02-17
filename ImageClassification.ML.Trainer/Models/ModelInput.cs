namespace ImageClassification.ML.Trainer.Models
{
    public class ModelInput
    {
        public byte[] Image { get; set; }
        public string Label { get; set; }
        public string ImagePath { get; set; }
    }
}
