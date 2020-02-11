using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpeculatorVersionOne.Models;

namespace SpeculatorVersionOne.ViewModels
{
    public class PrihodiViewModel : BaseViewModel
    {
        public Prihod prihod;
        public List<Prihod> prihodi;

        public PrihodiViewModel()
        {
            prihod = new Prihod();
            prihodi = new List<Prihod>();
        }
        

    }
}
