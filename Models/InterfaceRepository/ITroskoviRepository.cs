using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeculatorVersionOne.Models
{
    public interface ITroskoviRepository
    {

        Trosak DodajNoviTrosak(Trosak trosak);
        // dodaj novu kupovinu
        Kupovina DodajNovuKupovinu(int trosakId, string korisnikId);
        ICollection<Trosak> VratiTroskove(string id);

        //void ObrisiTrosak(int id);

    }
}
