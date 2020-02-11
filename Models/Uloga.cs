using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeculatorVersionOne.Models
{
    public class Uloga : IdentityRole<string>
    {
        public Uloga()
        {

        }

        public Uloga(string name)
        {
            Name = name;
        }
    }
}
