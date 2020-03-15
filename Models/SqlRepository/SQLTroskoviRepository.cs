using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace SpeculatorVersionOne.Models
{
    public class SQLTroskoviRepository : ITroskoviRepository
    {
        private readonly SpeculatorContext context;

        public SQLTroskoviRepository(SpeculatorContext context)
        {
            this.context = context;
        }

        public Trosak DodajNoviTrosak(Trosak trosak)
        {
            trosak.VremeTroska = DateTime.Now;
            context.Troskovi.Add(trosak);
            context.SaveChanges();
            return trosak;
        }
        public Kupovina DodajNovuKupovinu(int trosakId, string korisnikId)
        {
            Kupovina kupovina = new Kupovina();
            kupovina.TrosakId = trosakId;
            kupovina.KorisnikId = korisnikId;
            context.Kupovine.Add(kupovina);
            context.SaveChanges();
            return kupovina;
        }

        public ICollection<Trosak> VratiTroskove(string id)
        {

            var troskovi = context.Troskovi;
            var kupovine = context.Kupovine;


            var troskoviZaPrikaz = (from t in troskovi
                                    join k in kupovine
                                    on t.TrosakId equals k.TrosakId
                                    where k.KorisnikId == id
                                    select t).ToList();
            return troskoviZaPrikaz;
        }
    }
}
