using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpeculatorVersionOne.Models
{
    public partial class Proizvod
    {
        public Proizvod()
        {
            ZeljeniProizvodi = new HashSet<ZeljeniProizvod>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProizvodId { get; set; }

        [Required]
        [Display(Name = "Naziv")]
        [DataType(DataType.Text)]
        [StringLength(25, MinimumLength = 2)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        public string NazivProizvoda { get; set; }

        [Required]
        [Display(Name = "Iznos")]
        [DataType(DataType.Currency)]
        public decimal CenaProizvoda { get; set; }

        public ICollection<ZeljeniProizvod> ZeljeniProizvodi { get; set; }
    }
}
