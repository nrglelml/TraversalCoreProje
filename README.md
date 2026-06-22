# 🌍 TraversalCoreProje

ASP.NET Core 8 MVC ile geliştirilmiş, çok katmanlı mimari üzerine kurulu kapsamlı bir **turizm ve rezervasyon yönetim sistemi**. Kullanıcılar tur rotalarını inceleyip rezervasyon oluştururken, adminler ve rehberler sistemi yönetebilmektedir. 

---

## 🗂️ Proje Yapısı

```
TraversalCoreProje/
├── EntityLayer/           # Veritabanı entity sınıfları
├── DataAccessLayer/       # EF Core repository ve migration'lar
├── BusinessLayer/         # Servis ve iş mantığı katmanı
├── DTOLayer/              # Veri transfer nesneleri (DTO)
├── TraversalCoreProje/    # Ana MVC web projesi
├── SignalRApi/            # SignalR + PostgreSQL API projesi
└── SignalRConsume/        # SignalR istemci (MVC) projesi
```

---

## 🛠️ Kullanılan Teknolojiler

| Teknoloji | Versiyon | Kullanım Amacı |
|---|---|---|
| ASP.NET Core MVC | 8.0 | Web framework |
| Entity Framework Core | 8.x | ORM, veritabanı işlemleri |
| ASP.NET Core Identity | 8.x | Kullanıcı kimlik doğrulama ve rol yönetimi |
| AutoMapper | 12.x | Entity-DTO dönüşümleri |
| FluentValidation | 12.x | Model doğrulama |
| SignalR | 8.x | Gerçek zamanlı ziyaretçi istatistikleri |
| PostgreSQL + Npgsql | 16 | SignalR API veritabanı |
| SQL Server | - | Ana uygulama veritabanı |
| Serilog | - | Dosya tabanlı loglama |
| EPPlus / ClosedXML | - | Excel rapor oluşturma |
| RapidAPI (Booking.com) | - | Otel arama entegrasyonu |

---

## ✨ Özellikler

### 🌐 Kullanıcı (Ziyaretçi) Tarafı

- Ana sayfa: popüler rotalar, rehberler, testimonial, bülten aboneliği
- Tur rotaları listeleme ve detay sayfası (Cover image, açıklama, program detayları)
- Rehberlerimiz sayfası
- Hakkımızda sayfası (dinamik `Abouts` + `About2s` tabloları)
- İletişim sayfası (Google Maps embed, form ile mesaj gönderme)
- Yorum sistemi (giriş yapmış kullanıcılar yorum yapabilir, herkes görebilir)
- Rezervasyon yap butonu

### 👤 Üye (Member) Paneli

- Profil sayfası (fotoğraf yükleme, bilgi güncelleme)
- Tur rotaları listeleme ve detay görüntüleme
- Rezervasyon oluşturma
- Mevcut ve geçmiş rezervasyonlarını görüntüleme
- Dashboard (son rotalar, istatistikler, profil bilgisi, rehber listesi)

### 🛡️ Admin Paneli

- **Destinasyon yönetimi:** Tur rotası ekleme, güncelleme, silme (soft delete), detay görüntüleme
- **Rehber yönetimi:** Rehber ekleme, düzenleme, aktif/pasif yapma
- **Yorum yönetimi:** Tüm yorumları listeleme, silme
- **Duyuru yönetimi:** Duyuru ekleme, güncelleme, silme (FluentValidation ile)
- **Şehir yönetimi (AJAX):** Dinamik şehir CRUD işlemleri
- **Rezervasyon yönetimi:** Onaylama, reddetme, geçmiş olarak işaretleme
- **Kullanıcı yorum sayfası:** Belirli kullanıcının tüm yorumlarını görüntüleme
- **İletişim yönetimi:** Siteye gelen mesajların yönetimi
- **Excel raporlama:** EPPlus ve ClosedXML ile destinasyon Excel raporları
- **Booking.com entegrasyonu:** RapidAPI üzerinden otel arama (şehir → otel listesi)
- **Ziyaretçi istatistikleri API:** HTTP Client ile SignalR API entegrasyonu

### 📊 SignalR Ziyaretçi İstatistikleri

- Gerçek zamanlı ziyaretçi verisi (şehir bazlı, tarih bazlı)
- PostgreSQL `crosstab` ile pivot tablo
- Google Charts ile sütun grafik görselleştirmesi
- WebSocket bağlantısı ile anlık güncelleme

---

## 🏗️ Mimari Yapı

### Katmanlı Mimari

```
Presentation Layer (TraversalCoreProje MVC)
        ↓
Business Layer (Manager sınıfları, FluentValidation)
        ↓
Data Access Layer (EF Core Generic Repository)
        ↓
Entity Layer (POCO sınıfları)
        ↓
DTOLayer (ViewModel/DTO sınıfları)
```

### Generic Repository Pattern

```csharp
public interface IGenericDal<T>
{
    void Insert(T t);
    void Update(T t);
    void Delete(T t);
    T GetByID(int id);
    List<T> GetList();
}
```

### Dependency Injection (Extensions.cs)

Tüm servis kayıtları `BusinessLayer/Container/Extensions.cs` dosyasında merkezi olarak yönetilmektedir.

---

## 🗄️ Veritabanı Şeması (SQL Server)

| Tablo | Açıklama |
|---|---|
| `Destinations` | Tur rotaları (şehir, açıklama, resimler, fiyat, kapasite, rehber ilişkisi) |
| `Guides` | Rehber bilgileri (isim, açıklama, resim, sosyal medya linkleri) |
| `Comments` | Kullanıcı yorumları (AppUser FK, Destination FK) |
| `Reservations` | Rezervasyonlar (kullanıcı, destinasyon, kişi sayısı, durum) |
| `Contacts` | Şirket iletişim bilgileri (mail, adres, telefon, harita) |
| `ContactUs` | Kullanıcıların gönderdiği mesajlar |
| `Announcements` | Duyurular |
| `Newsletters` | Bülten aboneleri |
| `Abouts` | Hakkımızda içeriği |
| `About2s` | Hakkımızda alt grid içeriği |
| `AspNetUsers` | Identity kullanıcı tablosu (AppUser) |
| `AspNetRoles` | Identity rol tablosu (Admin, Member) |

---

## 🔐 Kimlik Doğrulama ve Yetkilendirme

- **ASP.NET Core Identity** tabanlı kullanıcı yönetimi
- **Rol bazlı yetkilendirme:** `Admin` ve `Member` rolleri
- Giriş sonrası **rol kontrolüyle otomatik yönlendirme:**
  - `Admin` rolü → `/Admin/Dashboard/Index`
  - Diğer kullanıcılar → `/Member/Dashboard/Index`
- Yetkisiz erişimde özel `/ErrorPage/AccessDenied` sayfası (403)
- Özel `CustomIdentityValidator` ile Türkçe hata mesajları
- Cookie tabanlı oturum yönetimi

---

## 🔗 Area Yapısı

```
Areas/
├── Admin/
│   ├── Controllers/
│   │   ├── BaseAdminController.cs   ← [Authorize(Roles = "Admin")]
│   │   ├── DestinationController.cs
│   │   ├── GuideController.cs
│   │   ├── CommentController.cs
│   │   ├── AnnouncementController.cs
│   │   ├── ReservationController.cs
│   │   ├── ExcelController.cs
│   │   ├── BookingHotelSearchController.cs
│   │   └── VisitorApiController.cs
│   └── Views/
└── Member/
    ├── Controllers/
    │   ├── BaseMemberController.cs  ← [Authorize]
    │   ├── DashboardController.cs
    │   ├── ProfileController.cs
    │   ├── ReservationController.cs
    │   └── DestinationController.cs
    └── Views/
```

---

## 🖼️ ViewComponent Listesi

| ViewComponent | Açıklama |
|---|---|
| `_CommentList` | Destinasyon yorumlarını listeler (kullanıcı avatarı ile) |
| `_AddComment` | Yorum ekleme formu (giriş durumuna göre gösterir) |
| `_ProfileInformation` | Üye profil bilgisi özeti |
| `_GuideList` | Rehber listesi (dashboard) |
| `_LastDestinations` | Son eklenen 4 tur rotası |
| `_PopularDestinations` | Popüler rotalar |
| `_Statistics` | İstatistik kartları |
| `_DashboardBanner` | Dashboard banner alanı |
| `_SliderPartial` | Ana sayfa slider |
| `_Testimonial` | Müşteri yorumları carousel |
| `_SubAbout` | Alt hakkımızda bölümü |
| `_Feature` | Özellikler bölümü |
| `_AdminDashboardHeader` | Admin profil header |
| `_AdminGuideList` | Admin rehber istatistikleri |
| `_Cards1Statistic` | İstatistik kartı 1 |
| `_Cards2Statistics` | İstatistik kartı 2 |
| `_DestinationStatistic` | Destinasyon istatistikleri |
| `_TotalRevenue` | Toplam gelir grafiği |

---

## 🏨 Booking.com API Entegrasyonu (RapidAPI)

Admin panelinde otel arama özelliği, iki aşamalı bir akışla çalışır:

1. **Şehir Arama:** `searchDestination` endpoint'i ile şehir adından `dest_id` bulunur
2. **Otel Listeleme:** `searchHotels` endpoint'i ile otel listesi (isim, resim, fiyat, puan) çekilir

---

## ⚡ SignalR Gerçek Zamanlı İstatistik

`SignalRApi` projesi, PostgreSQL üzerinde çalışan bağımsız bir API sunucusudur:

- `POST /api/Visitor` → Yeni ziyaretçi verisi ekler
- `GET /api/Visitor` → Ziyaretçi listesini döner
- `/VisitorHub` → SignalR Hub endpoint'i

`SignalRConsume` projesi bu Hub'a bağlanarak Google Charts ile gerçek zamanlı sütun grafik çizer.

---

## 📦 NuGet Paketleri

### TraversalCoreProje (Ana Proje)
```
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools
Microsoft.AspNetCore.Identity.EntityFrameworkCore
AutoMapper
FluentValidation.AspNetCore
Serilog.AspNetCore
Serilog.Sinks.File
EPPlus
ClosedXML
Newtonsoft.Json
```

### SignalRApi
```
Npgsql.EntityFrameworkCore.PostgreSQL
Microsoft.AspNetCore.SignalR
```

---

## 🚀 Kurulum

### Gereksinimler
- .NET 8 SDK
- SQL Server (ana uygulama)
- PostgreSQL 16 (SignalR API)
- Visual Studio 2022

### Adımlar

1. Repoyu klonla:
```bash
git clone https://github.com/nrglelml/TraversalCoreProje.git
cd TraversalCoreProje
```

2. `TraversalCoreProje/appsettings.json` dosyasında SQL Server bağlantı string'ini güncelle:
```json
"ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=TraversalDB;Trusted_Connection=True;"
}
```

3. `SignalRApi/appsettings.json` dosyasında PostgreSQL bağlantı string'ini güncelle:
```json
"ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=SignalRDB;Username=postgres;Password=sifren"
}
```

4. Migration'ları uygula:
```bash
# Ana proje (Package Manager Console - DataAccessLayer seçili)
Update-Database

# SignalRApi (SignalRApi projesi seçili)
Update-Database
```

5. PostgreSQL'de `tablefunc` extension'ını etkinleştir:
```sql
CREATE EXTENSION IF NOT EXISTS tablefunc;
```

6. Admin rolü oluştur ve kullanıcıya ata:
```sql
INSERT INTO AspNetRoles (Id, Name, NormalizedName, ConcurrencyStamp)
VALUES (NEWID(), 'Admin', 'ADMIN', NEWID())

INSERT INTO AspNetUserRoles (UserId, RoleId)
VALUES ('kullanici_id', 'admin_rol_id')
```

7. Solution'ı çalıştır:
   - `TraversalCoreProje` → Ana web uygulaması
   - `SignalRApi` → SignalR API sunucusu (port 5023)
   - `SignalRConsume` → SignalR istemcisi

---

## 📁 Resim Klasörleri

```
wwwroot/
├── destinationImages/   # Tur rotası görselleri
├── guideImages/         # Rehber profil fotoğrafları
├── userImages/          # Kullanıcı profil fotoğrafları
└── project_logo.png     # Site logosu / favicon
```

---

## 📜 Lisans

Bu proje Murat Yücedağ Traversal ASP.Net Core eğitimi ile birlikte geliştirilmiştir.  
Youtube kursu: [Traversal Rezervasyon Asp.Net Core 5.0 Mini Proje ]([https://www.youtube.com/playlist?list=PLKnjBHu2xXNMK5MBogdXmsXVi3K_eEZT5](https://www.youtube.com/playlist?list=PLKnjBHu2xXNMK5MBogdXmsXVi3K_eEZT5))

---

## 👩‍💻 Geliştirici

**Nurgül** — [@nrglelml](https://github.com/nrglelml)
