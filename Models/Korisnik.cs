using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpeculatorVersionOne.Models
{
    public partial class Korisnik : IdentityUser<string>
    {
        public Korisnik()
        {
            Kupovine = new HashSet<Kupovina>();
            Prilivi = new HashSet<Priliv>();
            ZeljeniProizvodi = new HashSet<ZeljeniProizvod>();
        }
     
        
        

        [Required]
        [DataType(DataType.Text)]
        [StringLength(25, MinimumLength = 2)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        public string Ime { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(25, MinimumLength = 2)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        public string Prezime { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [MinLength(8)]
        public string Lozinka { get; set; }

        [Required]
        [Display(Name = "Stanje")]
        [DataType(DataType.Currency)]
        public decimal StanjeNaRacunu { get; set; }

        public ICollection<Kupovina> Kupovine { get; set; }
        public ICollection<Priliv> Prilivi { get; set; }
        public ICollection<ZeljeniProizvod> ZeljeniProizvodi { get; set; }
    }
}
