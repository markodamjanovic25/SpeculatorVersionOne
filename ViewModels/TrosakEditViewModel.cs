using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpeculatorVersionOne.Models;

namespace SpeculatorVersionOne.ViewModels
{
    public class TrosakEditViewModel : BaseViewModel
    {
        public Trosak trosak;

        public TrosakEditViewModel()
        {
            trosak = new Trosak();
        }
    }
}
