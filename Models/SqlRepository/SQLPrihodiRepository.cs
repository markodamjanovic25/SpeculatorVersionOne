using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeculatorVersionOne.Models
{
    public class SQLPrihodiRepository : IPrihodiRepository
    {
        private readonly SpeculatorContext context;

        public SQLPrihodiRepository(SpeculatorContext context)
        {
            this.context = context;
        }

        //
        //dodavanje novog prihoda u bazu
        public Prihod DodajNoviPrihod(Prihod prihod)
        {
            prihod.VremePrihoda = DateTime.Now;
            context.Prihodi.Add(prihod);
            context.SaveChanges();
            return prihod;
        }

        public Priliv DodajNoviPriliv(int prihodId, string korisnikId)
        {
            Priliv priliv = new Priliv();
            priliv.PrihodId = prihodId;
            priliv.KorisnikId = korisnikId;
            context.Prilivi.Add(priliv);
            context.SaveChanges();
            return priliv;
        }

        //
        //dodavanje novog priliva u bazu



        //
        //prikaz prihoda
        public ICollection<Prihod> VratiPrihode(string id)
        {
            var prihodi = context.Prihodi;
            var prilivi = context.Prilivi;

            var prihodiZaPrikaz = (from prh in prihodi
                           join prl in prilivi
                           on prh.PrihodId equals prl.PrihodId
                           where prl.KorisnikId == id
                           select prh).ToList();
            return prihodiZaPrikaz;
        }
    }
}
