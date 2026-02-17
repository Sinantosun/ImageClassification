using Microsoft.ML.Data;

namespace ImageClassification.ML.Predict.Models
{
    public class ModelOutput
    {
        [ColumnName("PredictedLabel")]
        public string Prediction { get; set; }

        public float[] Score { get; set; }
    }
}
