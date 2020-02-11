using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpeculatorVersionOne.Models;

namespace SpeculatorVersionOne.ViewModels
{
    public class PrihodEditViewModel : BaseViewModel
    {
        public Prihod prihod;

        public PrihodEditViewModel()
        {
            prihod = new Prihod();
        }
        

    }
}
