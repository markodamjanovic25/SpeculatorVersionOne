using System;
using System.Collections.Generic;

namespace SpeculatorVersionOne.Models
{
    public partial class Kupovina
    {
        public int TrosakId { get; set; }
        public string KorisnikId { get; set; }

        public Korisnik Korisnik { get; set; }
        public Trosak Trosak { get; set; }
    }
}
