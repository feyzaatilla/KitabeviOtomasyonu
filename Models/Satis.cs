namespace KitabeviApp.Models
{
    public class Satis
    {
        public int Id { get; set; }
        public DateTime SatisTarihi { get; set; } = DateTime.Now;
        public decimal ToplamTutar { get; set; }
        public string OdemeDurumu { get; set; } = "Ödendi";

        // Hangi müşteri aldı?
        public int MusteriId { get; set; }
        public Musteri? Musteri { get; set; }

        // Bu satışta hangi kitaplar var?
        public List<SatisDetay> SatisDetaylari { get; set; } = new List<SatisDetay>();
    }
}
