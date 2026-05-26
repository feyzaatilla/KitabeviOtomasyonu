using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KitabeviApp.Models;

namespace KitabeviApp.Controllers
{
    public class KitapController : Controller
    {
        private readonly AppDbContext _db;

        public KitapController(AppDbContext db)
        {
            _db = db;
        }

        // Kitap listesi
        public async Task<IActionResult> Index()
        {
            var kitaplar = await _db.Kitaplar.ToListAsync();
            return View(kitaplar);
        }

        // Ekleme formu - GET
        public IActionResult Ekle()
        {
            return View();
        }

        // Ekleme kaydet - POST
        [HttpPost]
        public async Task<IActionResult> Ekle(Kitap kitap)
        {
            if (ModelState.IsValid)
            {
                _db.Kitaplar.Add(kitap);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(kitap);
        }

        // Düzenleme formu - GET
        public async Task<IActionResult> Duzenle(int id)
        {
            var kitap = await _db.Kitaplar.FindAsync(id);
            if (kitap == null) return NotFound();
            return View(kitap);
        }

        // Düzenleme kaydet - POST
        [HttpPost]
        public async Task<IActionResult> Duzenle(Kitap kitap)
        {
            if (ModelState.IsValid)
            {
                _db.Kitaplar.Update(kitap);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(kitap);
        }

        // Silme
        public async Task<IActionResult> Sil(int id)
        {
            var kitap = await _db.Kitaplar.FindAsync(id);
            if (kitap == null) return NotFound();
            _db.Kitaplar.Remove(kitap);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}