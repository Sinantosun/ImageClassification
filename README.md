🚀 Kendi Yapay Zekanızı Yazın: .NET ile Görsel Sınıflandırma

Bu proje, ML.NET kullanarak görsel sınıflandırma modeli eğitmek ve
eğitilen modeli bir Web API üzerinden tahmin yapmak için
geliştirilmiştir.

Proje, 3 katmanlı bir mimari ile tasarlanmıştır ve model eğitimi ile
tahmin sürecini uçtan uca göstermektedir.

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

🏗️ Proje Mimarisi

ImageClassification 
  │ 
  ├── ImageClassification.Shared 
  ├── ImageClassification.ML.Trainer 
  └── ImageClassification.ML.Predict

🔹 ImageClassification.Shared

Ortak modeller ve konfigürasyonlar burada bulunur. - ModelInput -
ModelStorageConfig

🔹 ImageClassification.ML.Trainer

Console uygulamasıdır. Model eğitimi bu proje üzerinden yapılır. - Data
klasörünü tarar - Alt klasörleri label olarak kullanır - ML.NET pipeline
oluşturur - Modeli .zip formatında kaydeder

🔹 ImageClassification.ML.Predict

Web API projesidir. Eğitilen modeli yükler ve tahmin işlemini
gerçekleştirir.

Özellikler: - Dependency Injection ile model yükleme -
PredictionEnginePool kullanımı - BaseResult pattern ile standart API
response - Dosya validasyonu (ImageValidator) - 5 MB dosya limiti - JPG
/ JPEG / PNG desteği

------------------------------------------------------------------------

🧠 Model Eğitimi

Klasör yapısı şu şekilde olmalıdır:

Data/ │ ├── kedi/ ├── kopek/ └── araba/

Her klasör bir sınıfı temsil eder. Model, klasör isimlerini otomatik
olarak etiket (label) olarak kullanır.

Kullanılan mimari: - MobilenetV2 - Transfer Learning

Model eğitildikten sonra model.zip olarak kaydedilir.

------------------------------------------------------------------------

🔍 Tahmin Süreci

Endpoint:

POST /api/classification/predict

Form-data ile “file” parametresi gönderilir.

Response formatı:

{ “payload”: { “prediction”: “kedi”, “score”: 92.35 }, “errors”: null }

------------------------------------------------------------------------

🛡️ Güvenlik & Doğrulama

ImageValidator sınıfı: - Dosya boş mu kontrol eder - Uzantı kontrolü
yapar (.jpg, .jpeg, .png) - Maksimum 5MB boyut limiti uygular

------------------------------------------------------------------------

⚠️ Önemli Not

Gerçek dünya uygulamalarında daha yüksek doğruluk için:
-   Daha büyük veri setleri
-   Farklı açılar
-   Farklı ışık koşulları
-   Çeşitli arka planlar

kullanılmalıdır.

Blog Yazısı

Projenin detaylı anlatımı için blog yazımı inceleyebilirsiniz: 
