
# MicroServiceProject

Bu proje, bir e-ticaret platformunun temel işlevselliklerini sağlamak için **Microservice Mimarisi** ile geliştirilmiştir. Proje, aşağıdaki dört mikroservisten oluşmaktadır:

- **ProductService**: Ürün yönetimi (ekleme, güncelleme, silme ve listeleme).
- **OrderService**: Sipariş oluşturma ve sipariş detaylarını görüntüleme.
- **CustomerService**: Müşteri yönetimi.
- **OrderStatusService**: Sipariş durumlarını güncelleme.

Ayrıca, bu servisleri yöneten bir **API Gateway** ve kimlik doğrulama için bir **AuthAPI** bulunmaktadır.

---

## **Kullanılan Teknolojiler ve Araçlar**

1. **Backend**:
   - **.NET Core 8**: Tüm servislerin geliştirilmesi.
   - **Entity Framework Core**: Veritabanı işlemleri.
   - **MediatR**: CQRS ve bağımsız işlemleri yönetmek için.
   - **FluentValidation**: Giriş doğrulama.
   - **Elasticsearch ve Serilog**: Loglama.

2. **Frontend**:
   - **React**: Kullanıcı arayüzü.
   - **Tailwind CSS**: Stiller.

3. **Diğer Araçlar**:
   - **Ocelot**: API Gateway.
   - **JWT**: Kimlik doğrulama.
   - **Docker (opsiyonel)**: Servislerin konteynerize edilmesi.
   - **xUnit ve Moq**: Unit testler.

---

## **Mimari**

### **1. Microservice Mimarisi**
Her bir microservice bağımsız olarak geliştirilmiş ve çalıştırılabilir. Bu servisler arasında iletişim, HTTP protokolü üzerinden gerçekleşmektedir.

- **CQRS Pattern**:
  - Command ve Query'ler ayrı sınıflar olarak tanımlanmıştır.
  - MediatR ile bu işlemler yönlendirilmiştir.
  
- **Repository ve Unit of Work Pattern**:
  - Veritabanı işlemleri için soyutlama sağlanmıştır.

### **2. API Gateway**
- **Ocelot** kullanılarak microservice'ler tek bir noktadan yönetilmektedir.
- Kimlik doğrulama işlemleri de API Gateway üzerinden yapılmaktadır.

### **3. AuthAPI**
- JWT ile kimlik doğrulama ve yetkilendirme işlemlerini yönetir.

---

## **Projenin Yapısı**

```plaintext
MicroServiceProject
│
├── MicroServiceProject.ProductService
│   ├── App
│   │   ├── Commands
│   │   ├── Queries
│   │   └── Validators
│   ├── Controllers
│   ├── Models
│   ├── Repositories
│   └── Startup.cs
│
├── MicroServiceProject.OrderService
│   ├── App
│   ├── Controllers
│   ├── Models
│   ├── Repositories
│   └── Startup.cs
│
├── MicroServiceProject.CustomerService
├── MicroServiceProject.OrderStatusService
│
├── MicroServiceProject.AuthAPI
├── MicroServiceProject.ApiGateway
│
├── microserviceprojectui
│   ├── src
│   │   ├── components
│   │   └── utils
│   └── package.json
│
└── MicroServiceProject.Tests
    ├── ProductService.Tests
    ├── OrderService.Tests
```

---

## **Projenin Çalıştırılması**

### **1. Gerekli Bağımlılıkların Yüklenmesi**

- **Backend** için gerekli NuGet paketlerini yüklemek:
  ```bash
  dotnet restore
  ```

- **Frontend** için gerekli npm paketlerini yüklemek:
  ```bash
  cd microserviceprojectui
  npm install
  ```

### **2. Veritabanını Hazırlama**

Her microservice için gerekli veritabanı migrasyonlarını uygulayın:

```bash
cd MicroServiceProject.ProductService
dotnet ef database update
```

Diğer servisler için de aynı komut uygulanır.

### **3. Servisleri Çalıştırma**

Her bir microservice'i ayrı terminal pencerelerinde çalıştırabilirsiniz:

```bash
cd MicroServiceProject.ProductService
dotnet run
```

**OrderService**, **CustomerService**, **OrderStatusService**, **AuthAPI**, ve **ApiGateway** için de aynı işlemi yapın.

### **4. Frontend'i Çalıştırma**

React kullanıcı arayüzünü çalıştırmak için:

```bash
cd microserviceprojectui
npm start
```

---

## **API Gateway Yapılandırması**

`ocelot.json` dosyasında her microservice için routing yapılandırması bulunmaktadır.


---

## **Testlerin Çalıştırılması**

Testleri çalıştırmak için aşağıdaki komutu kullanabilirsiniz:

```bash
dotnet test
```

---

## **Sonuç**

Bu proje, bir e-ticaret platformu için modern bir microservice mimarisi sağlar. Her microservice bağımsız olarak geliştirilmiş ve çalıştırılabilir. API Gateway, tüm servisleri tek bir noktadan yönetirken, kimlik doğrulama **AuthAPI** ile sağlanmaktadır. Frontend tarafında React ile kullanıcı dostu bir arayüz sunulmuştur.

---

Herhangi bir sorun veya geliştirme sürecinde destek için benimle iletişime geçebilirsiniz! 😊
MUHAMMET ŞEVKİ UZUN
05380860840
