using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KitabeviApp.Models;

namespace KitabeviApp.Controllers
{
    public class MusteriController : Controller
    {
        private readonly AppDbContext _db;

        public MusteriController(AppDbContext db)
        {
            _db = db;
        }

        // Müşteri listesi
        public async Task<IActionResult> Index()
        {
            var musteriler = await _db.Musteriler.ToListAsync();
            return View(musteriler);
        }

        // Ekleme formu - GET
        public IActionResult Ekle()
        {
            return View();
        }

        // Ekleme kaydet - POST
        [HttpPost]
        public async Task<IActionResult> Ekle(Musteri musteri)
        {
            if (ModelState.IsValid)
            {
                _db.Musteriler.Add(musteri);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(musteri);
        }

        // Düzenleme formu - GET
        public async Task<IActionResult> Duzenle(int id)
        {
            var musteri = await _db.Musteriler.FindAsync(id);
            if (musteri == null) return NotFound();
            return View(musteri);
        }

        // Düzenleme kaydet - POST
        [HttpPost]
        public async Task<IActionResult> Duzenle(Musteri musteri)
        {
            if (ModelState.IsValid)
            {
                _db.Musteriler.Update(musteri);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(musteri);
        }

        // Silme
        public async Task<IActionResult> Sil(int id)
        {
            var musteri = await _db.Musteriler.FindAsync(id);
            if (musteri == null) return NotFound();
            _db.Musteriler.Remove(musteri);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
