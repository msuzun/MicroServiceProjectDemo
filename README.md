
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

```
{
  "Routes": [
    {
      "DownstreamPathTemplate": "/api/product/{everything}",
      "DownstreamScheme": "http",
      "DownstreamHostAndPorts": [
        {
          "Host": "localhost",
          "Port": 5001
        }
      ],
      "AuthenticationOptions": {
        "AuthenticationProviderKey": "JwtBearer",
        "AllowedScopes": []
      },
      "UpstreamPathTemplate": "/product/{everything}",
      "UpstreamHttpMethod": [ "GET", "POST", "PUT", "DELETE" ]
    },
    {
      "DownstreamPathTemplate": "/api/order/{everything}",
      "DownstreamScheme": "http",
      "DownstreamHostAndPorts": [
        {
          "Host": "localhost",
          "Port": 5003
        }
      ],
      "AuthenticationOptions": {
        "AuthenticationProviderKey": "JwtBearer",
        "AllowedScopes": []
      },
      "UpstreamPathTemplate": "/order/{everything}",
      "UpstreamHttpMethod": [ "GET", "POST", "PUT", "DELETE" ]
    },
    {
      "DownstreamPathTemplate": "/api/customer/{everything}",
      "DownstreamScheme": "http",
      "DownstreamHostAndPorts": [
        {
          "Host": "localhost",
          "Port": 5005
        }
      ],
      "AuthenticationOptions": {
        "AuthenticationProviderKey": "JwtBearer",
        "AllowedScopes": []
      },
      "UpstreamPathTemplate": "/customer/{everything}",
      "UpstreamHttpMethod": [ "GET", "POST", "PUT", "DELETE" ]
    },
    {
      "DownstreamPathTemplate": "/api/orderstatus/{everything}",
      "DownstreamScheme": "http",
      "DownstreamHostAndPorts": [
        {
          "Host": "localhost",
          "Port": 5007
        }
      ],
      "AuthenticationOptions": {
        "AuthenticationProviderKey": "JwtBearer",
        "AllowedScopes": []
      },
      "UpstreamPathTemplate": "/orderstatus/{everything}",
      "UpstreamHttpMethod": [ "GET", "POST", "PUT", "DELETE" ]
    },
    {
      "DownstreamPathTemplate": "/api/auth/{everything}",
      "DownstreamScheme": "http",
      "DownstreamHostAndPorts": [
        {
          "Host": "localhost",
          "Port": 5009
        }
      ],
      "UpstreamPathTemplate": "/auth/{everything}",
      "UpstreamHttpMethod": [ "POST" ]
    }
  ],
  "GlobalConfiguration": {
    "BaseUrl": "http://localhost:5020"
  }
}
```


---

## **Testlerin Çalıştırılması**

Testleri çalıştırmak için aşağıdaki komutu kullanabilirsiniz:

```bash
dotnet test
```

---


# MicroServiceProject.ProductService ve MicroServiceProject.OrderService için Test Dokümantasyonu

Bu doküman, **MicroServiceProject.ProductService** ve **MicroServiceProject.OrderService** için yazılan testleri detaylandırır. Test yapısı, test türleri ve testlerin nasıl çalıştırılacağı hakkında bilgi içerir.

---

## **1. ProductService Test Dokümantasyonu**

### **Test Kategorileri**

1. **Birim Testleri (Unit Tests)**:
   - İş mantığının doğru çalıştığından emin olmak için her bir command ve query handler için yazılan testler.

2. **Entegrasyon Testleri (Integration Tests)**:
   - ProductService'in bağımlılıkları (örneğin, veritabanı) ile etkileşimini test eder.

### **Birim Testleri**

#### **Commands**

- `CreateProductCommandHandlerTests`
  - `Handle_ValidCommand_ShouldCreateProduct`

- `UpdateProductCommandHandlerTests`
  - `Handle_ValidCommand_ShouldUpdateProduct`

- `DeleteProductCommandHandlerTests`
  - `Handle_ValidCommand_ShouldDeleteProduct`

#### **Queries**

- `GetProductsQueryHandlerTests`
  - `Handle_ShouldReturnProductsList`

- `GetProductByIdQueryHandlerTests`
  - `Handle_ValidId_ShouldReturnProduct`

### **Entegrasyon Testleri**

- `ProductServiceIntegrationTests`
  - `Product` varlığı üzerindeki CRUD işlemleri için veritabanı entegrasyonunu test eder.

### **ProductService Testlerinin Çalıştırılması**

Tüm ProductService testlerini çalıştırmak için:
```bash
cd MicroServiceProject.ProductService.Tests
dotnet test
```

Belirli bir test sınıfını çalıştırmak için:
```bash
dotnet test --filter FullyQualifiedName~MicroServiceProject.ProductService.Tests.Commands.CreateProductCommandHandlerTests
```

---

## **2. OrderService Test Dokümantasyonu**

### **Test Kategorileri**

1. **Birim Testleri (Unit Tests)**:
   - Her bir command ve query handler için yazılan testler.

2. **Entegrasyon Testleri (Integration Tests)**:
   - Sipariş oluşturma sırasında stok doğrulama için OrderService ile ProductService'in etkileşimini test eder.

### **Birim Testleri**

#### **Commands**

- `CreateOrderCommandHandlerTests`
  - `Handle_ValidCommand_ShouldCreateOrder`

- `UpdateOrderCommandHandlerTests`
  - `Handle_ValidCommand_ShouldUpdateOrderStatus`

#### **Queries**

- `GetOrdersQueryHandlerTests`
  - `Handle_ShouldReturnOrdersList`

- `GetOrderByIdQueryHandlerTests`
  - `Handle_ValidId_ShouldReturnOrder`

### **Entegrasyon Testleri**

- `OrderServiceIntegrationTests`
  - Sipariş oluştururken ProductService'den stok doğrulamasını test eder.

### **OrderService Testlerinin Çalıştırılması**

Tüm OrderService testlerini çalıştırmak için:
```bash
cd MicroServiceProject.OrderService.Tests
dotnet test
```

Belirli bir test sınıfını çalıştırmak için:
```bash
dotnet test --filter FullyQualifiedName~MicroServiceProject.OrderService.Tests.Commands.CreateOrderCommandHandlerTests
```

---

## **Testler için Genel İpuçları**

1. Gerekli tüm test bağımlılıklarının yüklü olduğundan emin olun:
   ```bash
   dotnet restore
   ```

2. Test sonuçlarını kaydetmek için `--logger` parametresini kullanın:
   ```bash
   dotnet test --logger "trx;LogFileName=TestResults.trx"
   ```

3. Entegrasyon testleri için, üretim verilerini değiştirmemek adına **In-Memory Database** kullanmayı tercih edin.

---

## **Sonuç**

Bu proje, bir e-ticaret platformu için modern bir microservice mimarisi sağlar. Her microservice bağımsız olarak geliştirilmiş ve çalıştırılabilir. API Gateway, tüm servisleri tek bir noktadan yönetirken, kimlik doğrulama **AuthAPI** ile sağlanmaktadır. Frontend tarafında React ile kullanıcı dostu bir arayüz sunulmuştur.

---

Herhangi bir sorun veya geliştirme sürecinde destek için benimle iletişime geçebilirsiniz! 😊
MUHAMMET ŞEVKİ UZUN
05380860840
