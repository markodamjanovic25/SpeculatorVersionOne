using System;
using System.Collections.Generic;

namespace SpeculatorVersionOne.Models
{
    public partial class Priliv
    {
        public int PrihodId { get; set; }
        public string KorisnikId { get; set; }

        public Korisnik Korisnik { get; set; }
        public Prihod Prihod { get; set; }
    }
}
