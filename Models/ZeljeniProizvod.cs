using System;
using System.Collections.Generic;

namespace SpeculatorVersionOne.Models
{
    public partial class ZeljeniProizvod
    {
        public int ProizvodId { get; set; }
        public string KorisnikId { get; set; }

        public Korisnik Korisnik { get; set; }
        public Proizvod Proizvod { get; set; }
    }
}
