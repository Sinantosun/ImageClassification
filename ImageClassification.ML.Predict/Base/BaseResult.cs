using System.Text.Json.Serialization;

namespace ImageClassification.ML.Predict.Base
{
    public class BaseResult<T>
    {
        public T? Payload { get; set; }
        public List<Error>? Errors { get; set; }

        [JsonIgnore]
        public bool IsSuccess => Errors == null || !Errors.Any();

        [JsonIgnore]
        public bool IsFailure => !IsSuccess;

        public static BaseResult<T> Success(T payload)
        {
            return new BaseResult<T> { Payload = payload };
        }
        public static BaseResult<string> Success()
        {
            return new BaseResult<string> { Payload = "İşlem Tamamlandı...!" };
        }
        public static BaseResult<T> Fail(string errorMessage = "Beklenmeyen bir hata oluştu")
        {
            return new BaseResult<T>
            {
                Errors = [new Error { ErrorMessage = errorMessage }]
            };
        }
    }
    public class Error
    {
        public string ErrorMessage { get; set; }
    }
}
