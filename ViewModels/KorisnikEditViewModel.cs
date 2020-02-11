using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpeculatorVersionOne.Models;

namespace SpeculatorVersionOne.ViewModels
{
    public class KorisnikEditViewModel : BaseViewModel
    {
        public Korisnik korisnik;

        public KorisnikEditViewModel()
        {
            korisnik = new Korisnik();
        }
    }
}
