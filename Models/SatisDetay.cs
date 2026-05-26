namespace KitabeviApp.Models
{
    public class SatisDetay
    {
        public int Id { get; set; }
        public int Adet { get; set; }
        public decimal BirimFiyat { get; set; }

        // Hangi satışa ait?
        public int SatisId { get; set; }
        public Satis? Satis { get; set; }

        // Hangi kitap?
        public int KitapId { get; set; }
        public Kitap? Kitap { get; set; }
    }
}
