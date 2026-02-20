Kendi Yapay Zekanızı Yazın: .NET ile Görsel Sınıflandırma

Bu proje, ML.NET kullanarak görsel sınıflandırma modeli eğitmek ve eğitilen modeli bir Web API üzerinden tahmin yapmak için geliştirilmiştir. 3 katmanlı mimari ile tasarlanan bu repo, model eğitimi ile tahmin sürecini uçtan uca göstermektedir.

------------------------------------------------------------------------

📌 Kullanılan Teknolojiler

-   .NET 10
-   ML.NET
-   ML.NET Vision
-   TensorFlow (SciSharp.TensorFlow.Redist)
-   ASP.NET Core Web API
-   Dependency Injection
-   PredictionEnginePool

------------------------------------------------------------------------
Proje Mimarisi

Proje, sorumlulukların net ayrılması için üç ana parçadan oluşur:

🔹 ImageClassification.Shared: Ortak modeller (ModelInput) ve konfigürasyonlar (ModelStorageConfig) burada bulunur.

🔹 ImageClassification.ML.Trainer: Console uygulamasıdır. Data klasörünü tarar, alt klasörleri etiket (label) olarak kullanır ve MobilenetV2 (Transfer Learning) ile modeli eğitip .zip olarak kaydeder.

🔹 ImageClassification.ML.Predict: Web API projesidir. PredictionEnginePool kullanarak yüksek performanslı tahminler gerçekleştirir.

------------------------------------------------------------------------
Kullanılan Teknolojiler
🔹 .NET 10
🔹 ML.NET Vision
🔹 TensorFlow	Arka plan hesaplama motoru (SciSharp)
🔹ASP.NET Core
🔹DI & Pool	PredictionEnginePool

------------------------------------------------------------------------

🧠 Model Eğitimi

Klasör yapısı şu şekilde olmalıdır:

Data/
├── kedi/
├── kopek/
└── araba/

Model, klasör isimlerini otomatik olarak etiket (label) olarak kabul eder. Eğitim sonrasında model dosyası merkezi bir dizine kaydedilir.

------------------------------------------------------------------------

🔍 Tahmin Süreci

Endpoint:

POST /api/classification/predict

Form-data ile “file” parametresi gönderilir.

Response formatı:

{ “payload”: { “prediction”: “kedi”, “score”: 92.35 }, “errors”: null }

------------------------------------------------------------------------

🛡️ Güvenlik & Doğrulama

ImageValidator sınıfı ile şu kontroller yapılır:

🔹 Dosya varlığı kontrolü.

🔹 Uzantı kontrolü (.jpg, .jpeg, .png).

🔹 5MB maksimum dosya boyutu limiti.

🔹 %85 altındaki güven skorlarında "Tanınamadı" uyarısı.

------------------------------------------------------------------------

⚠️ Önemli Not: Gerçek dünya uygulamalarında yüksek doğruluk için; daha büyük veri setleri, farklı ışık koşulları, çeşitli açılar ve arka planlar içeren veriler kullanılmalıdır.

------------------------------------------------------------------------

Blog Yazısı

Projenin detaylı anlatımı için blog yazımı inceleyebilirsiniz: https://sinantosun.com/blog-detayi/mlnet-gorsel-siniflandirma
