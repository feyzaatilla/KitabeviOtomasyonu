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