using Microsoft.AspNetCore.Mvc;
using KitabeviApp.Models;

namespace KitabeviApp.Controllers
{
    public class KitapController : Controller
    {
        // Geçici test verisi — veritabanı olmadan
        public IActionResult Index()
        {
            var kitaplar = new List<Kitap>
            {
                new Kitap { Id=1, Adi="Suç ve Ceza", Yazar="Dostoyevski", Fiyat=85, StokAdedi=10 },
                new Kitap { Id=2, Adi="1984", Yazar="George Orwell", Fiyat=75, StokAdedi=3 },
                new Kitap { Id=3, Adi="Simyacı", Yazar="Paulo Coelho", Fiyat=65, StokAdedi=8 }
            };
            return View(kitaplar);
        }

        public IActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Ekle(Kitap kitap)
        {
            return RedirectToAction("Index");
        }
    }
} 
