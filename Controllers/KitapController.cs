using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KitabeviApp.Models;

namespace KitabeviApp.Controllers
{
    public class KitapController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public KitapController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var kitaplar = await _db.Kitaplar.ToListAsync();
            return View(kitaplar);
        }

        public IActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Ekle(Kitap kitap, IFormFile? FotoğrafDosyası)
        {
            if (FotoğrafDosyası != null && FotoğrafDosyası.Length > 0)
            {
                var dosyaAdı = Guid.NewGuid().ToString() + Path.GetExtension(FotoğrafDosyası.FileName);
                var kayıtYolu = Path.Combine(_env.WebRootPath, "uploads", dosyaAdı);
                using (var stream = new FileStream(kayıtYolu, FileMode.Create))
                {
                    await FotoğrafDosyası.CopyToAsync(stream);
                }
                kitap.FotoğrafYolu = "/uploads/" + dosyaAdı;
            }

            if (ModelState.IsValid)
            {
                _db.Kitaplar.Add(kitap);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(kitap);
        }

        public async Task<IActionResult> Duzenle(int id)
        {
            var kitap = await _db.Kitaplar.FindAsync(id);
            if (kitap == null) return NotFound();
            return View(kitap);
        }

        [HttpPost]
        public async Task<IActionResult> Duzenle(Kitap kitap, IFormFile? FotoğrafDosyası)
        {
            if (FotoğrafDosyası != null && FotoğrafDosyası.Length > 0)
            {
                var dosyaAdı = Guid.NewGuid().ToString() + Path.GetExtension(FotoğrafDosyası.FileName);
                var kayıtYolu = Path.Combine(_env.WebRootPath, "uploads", dosyaAdı);
                using (var stream = new FileStream(kayıtYolu, FileMode.Create))
                {
                    await FotoğrafDosyası.CopyToAsync(stream);
                }
                kitap.FotoğrafYolu = "/uploads/" + dosyaAdı;
            }

            if (ModelState.IsValid)
            {
                _db.Kitaplar.Update(kitap);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(kitap);
        }

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