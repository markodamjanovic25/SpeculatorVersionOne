using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeculatorVersionOne.Models
{
    public class SQLListaZeljaRepository : IListaZeljaRepository
    {
        private readonly SpeculatorContext context;

        public SQLListaZeljaRepository(SpeculatorContext context)
        {
            this.context = context;
        }

        public Proizvod DodajNoviProizvod(Proizvod proizvod)
        {
            Proizvod noviProizvod = new Proizvod();
            context.Proizvodi.Add(proizvod);
            context.SaveChanges();
            return noviProizvod;
        }

        public ZeljeniProizvod DodajNoviZeljeniProizvod(int proizvodId, string korisnikId)
        {
            ZeljeniProizvod zp = new ZeljeniProizvod();
            zp.ProizvodId = proizvodId;
            zp.KorisnikId = korisnikId;
            context.ZeljeniProizvodi.Add(zp);
            context.SaveChanges();
            return zp;
        }

        public ICollection<Proizvod> VratiProizvode(string id)
        {
            var proizvodi = context.Proizvodi;
            var zeljeniProizvodi = context.ZeljeniProizvodi;

            var listaZelja = (from p in proizvodi
                              join zp in zeljeniProizvodi
                              on p.ProizvodId equals zp.ProizvodId
                              where zp.KorisnikId == id
                              select p).ToList();

            return listaZelja;
        }
    }
}
