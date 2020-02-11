using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpeculatorVersionOne.Models;

namespace SpeculatorVersionOne.ViewModels
{
    public class ListaZeljaViewModel : BaseViewModel
    {

        public Proizvod proizvod;
        public List<Proizvod> proizvodi;

        public ListaZeljaViewModel()
        {
            proizvod = new Proizvod();
            proizvodi = new List<Proizvod>();
        }

    }
}
