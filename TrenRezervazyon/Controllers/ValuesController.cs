using Microsoft.AspNetCore.Mvc;
using TrenRezervasyon;

namespace TrenRezervasyonAPI.Controllers
{
    [ApiController]
    [Route("api/rezervasyon")]
    public class RezervasyonController : ControllerBase
    {
        [HttpPost("yap")]
        public IActionResult YapRezervasyon([FromBody] RezervasyonIstegi istek)
        {
            var rezervasyonSonucu = RezervasyonuYap(istek);

            if (rezervasyonSonucu.RezervasyonYapilabilir)
            {
                return Ok(rezervasyonSonucu);
            }
            else
            {
                return BadRequest(rezervasyonSonucu);
            }
        }

        private RezervasyonSonucu RezervasyonuYap(RezervasyonIstegi istek)
        {
            var sonuc = new RezervasyonSonucu();
            var kalanKisiSayisi = istek.RezervasyonYapilacakKisiSayisi;

            for (int i = 0; i < istek.Tren.Vagonlar.Count; i++)
            {
                var vagon = istek.Tren.Vagonlar[i];
                var kapasite = vagon.Kapasite;
                var doluKoltukAdet = vagon.DoluKoltukAdet;

                if (kalanKisiSayisi <= 0)
                    break;

                if (doluKoltukAdet < kapasite * 0.7)
                {
                    sonuc.RezervasyonYapilabilir = true;

                    var bosKoltukSayisi = (int)(kapasite * 0.7 - doluKoltukAdet);
                    var rezerveEdilenKisiSayisi = Math.Min(bosKoltukSayisi, kalanKisiSayisi);

                    sonuc.YerlesimAyrinti.Add(new YerlesimAyrinti
                    {
                        VagonAdi = vagon.Ad,
                        KisiSayisi = rezerveEdilenKisiSayisi
                    });

                    doluKoltukAdet += rezerveEdilenKisiSayisi;
                    kalanKisiSayisi -= rezerveEdilenKisiSayisi;
                }
            }

            if (kalanKisiSayisi > 0)
                sonuc.RezervasyonYapilabilir = false;

            return sonuc;
        }



    }

}
