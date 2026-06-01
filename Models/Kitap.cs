namespace KitabeviApp.Models
{
    public class Kitap
    {
        public int Id { get; set; }
        public string Adi { get; set; } = string.Empty;
        public string Yazar { get; set;} = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public decimal Fiyat { get; set;}
        public int StokAdedi { get; set; }
        public DateTime EklenmeTarihi { get; set; } = DateTime.Now;
        public string? FotoğrafYolu { get; set; } = string.Empty;
    }
}
