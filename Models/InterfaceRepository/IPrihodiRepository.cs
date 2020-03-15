using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeculatorVersionOne.Models
{
    public interface IPrihodiRepository
    {
        Prihod DodajNoviPrihod(Prihod prihod);
        Priliv DodajNoviPriliv(int prihodId, string korisnikId);
        ICollection<Prihod> VratiPrihode(string id);
    }
}
