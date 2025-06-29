# OrderService API

Basit bir e-ticaret sipariş yönetim sistemi API'si.  
Ürünler, müşteriler, siparişler ve sipariş kalemlerini yönetir.

---

## Özellikler

- Yeni sipariş oluşturma (stok kontrolü dahil)  
- Kullanıcıya ait tüm siparişleri listeleme  
- Sipariş detaylarını getirme  
- Sipariş silme  
- Veritabanı olarak InMemoryDatabase kullanır, migration gerektirmez  
- Seed edilmiş örnek veri ile çalışır (örnek ürün ve müşteri verileri)

---

## Gereksinimler

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

---

## Proje Kurulumu ve Çalıştırma

1. Depoyu klonlayın:

   ```bash
   git clone https://github.com/yourusername/OrderService.git
   cd OrderService
2.Projeyi çalıştırın
 ```bash
dotnet run
