using SpeculatorVersionOne.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeculatorVersionOne.ViewModels
{
    public class TroskoviViewModel : BaseViewModel
    {
        public Trosak trosak;
        public List<Trosak> troskovi;

        public TroskoviViewModel()
        {
            trosak = new Trosak();
            troskovi = new List<Trosak>();
        }

    }
}
