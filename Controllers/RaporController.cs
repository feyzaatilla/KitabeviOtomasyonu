using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KitabeviApp.Models;

namespace KitabeviApp.Controllers
{
    public class RaporController : Controller
    {
        private readonly AppDbContext _db;

        public RaporController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            // En çok satan kitaplar
            var enCokSatan = await _db.SatisDetaylari
                .Include(sd => sd.Kitap)
                .GroupBy(sd => sd.Kitap!.Adi)
                .Select(g => new
                {
                    KitapAdi = g.Key,
                    ToplamAdet = g.Sum(sd => sd.Adet),
                    ToplamGelir = g.Sum(sd => sd.Adet * sd.BirimFiyat)
                })
                .OrderByDescending(x => x.ToplamAdet)
                .Take(10)
                .ToListAsync();

            // Toplam gelir
            var toplamGelir = await _db.Satislar.SumAsync(s => s.ToplamTutar);

            // Toplam satış sayısı
            var toplamSatis = await _db.Satislar.CountAsync();

            // Toplam müşteri sayısı
            var toplamMusteri = await _db.Musteriler.CountAsync();

            // Stoku az olan kitaplar (5'ten az)
            var azStok = await _db.Kitaplar
                .Where(k => k.StokAdedi < 5)
                .ToListAsync();

            ViewBag.EnCokSatan = enCokSatan;
            ViewBag.ToplamGelir = toplamGelir;
            ViewBag.ToplamSatis = toplamSatis;
            ViewBag.ToplamMusteri = toplamMusteri;
            ViewBag.AzStok = azStok;

            return View();
        }
    }
}