using Microsoft.EntityFrameworkCore;

namespace KitabeviApp.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Kitap> Kitaplar { get; set; }
        public DbSet<Musteri> Musteriler { get; set; }
        public DbSet<Satis> Satislar { get; set; }
        public DbSet<SatisDetay> SatisDetaylari { get; set; }
    }
}

