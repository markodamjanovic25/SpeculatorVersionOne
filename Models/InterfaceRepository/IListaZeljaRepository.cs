using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpeculatorVersionOne.Models;

namespace SpeculatorVersionOne.Models
{
    public interface IListaZeljaRepository
    {
        Proizvod DodajNoviProizvod(Proizvod proizvod);
        ZeljeniProizvod DodajNoviZeljeniProizvod(int proizvodId, string korisnikId);
        ICollection<Proizvod> VratiProizvode(string id);
    }
}
