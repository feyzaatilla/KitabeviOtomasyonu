using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KitabeviApp.Models;

namespace KitabeviApp.Controllers
{
    public class SatisController : Controller
    {
        private readonly AppDbContext _db;

        public SatisController(AppDbContext db)
        {
            _db = db;
        }

        // Satış listesi
        public async Task<IActionResult> Index()
        {
            var satislar = await _db.Satislar
                .Include(s => s.Musteri)
                .Include(s => s.SatisDetaylari)
                    .ThenInclude(sd => sd.Kitap)
                .ToListAsync();
            return View(satislar);
        }

        // Yeni satış formu - GET
        public async Task<IActionResult> Ekle()
        {
            ViewBag.Musteriler = await _db.Musteriler.ToListAsync();
            ViewBag.Kitaplar = await _db.Kitaplar.ToListAsync();
            return View();
        }

        // Yeni satış kaydet - POST
        [HttpPost]
        public async Task<IActionResult> Ekle(int MusteriId, int KitapId, int Adet)
        {
            var kitap = await _db.Kitaplar.FindAsync(KitapId);
            if (kitap == null) return NotFound();

            // Stok kontrolü
            if (kitap.StokAdedi < Adet)
            {
                TempData["Hata"] = "Yeterli stok yok!";
                return RedirectToAction("Ekle");
            }

            // Satış oluştur
            var satis = new Satis
            {
                MusteriId = MusteriId,
                ToplamTutar = kitap.Fiyat * Adet,
                OdemeDurumu = "Ödendi"
            };

            _db.Satislar.Add(satis);
            await _db.SaveChangesAsync();

            // Satış detayı ekle
            var detay = new SatisDetay
            {
                SatisId = satis.Id,
                KitapId = KitapId,
                Adet = Adet,
                BirimFiyat = kitap.Fiyat
            };

            _db.SatisDetaylari.Add(detay);

            // Stoku azalt
            kitap.StokAdedi -= Adet;
            _db.Kitaplar.Update(kitap);

            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
