namespace TrenRezervasyon
{
    public class Tren
    {
        public string Ad { get; set; }
        public List<Vagon> Vagonlar { get; set; }
    }

    public class Vagon
    {
        public string Ad { get; set; }
        public int Kapasite { get; set; }
        public int DoluKoltukAdet { get; set; }
    }

    public class RezervasyonIstegi
    {
        public Tren Tren { get; set; }
        public int RezervasyonYapilacakKisiSayisi { get; set; }
        public bool KisilerFarkliVagonlaraYerlestirilebilir { get; set; }
      
    }


    public class RezervasyonSonucu
    {
        public bool RezervasyonYapilabilir { get; set; }
        public List<YerlesimAyrinti> YerlesimAyrinti { get; set; }

        public RezervasyonSonucu()
        {
            YerlesimAyrinti = new List<YerlesimAyrinti>();
        }
    }

    public class YerlesimAyrinti
    {
        public string VagonAdi { get; set; }
        public int KisiSayisi { get; set; }
    }
}
