using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpeculatorVersionOne.Models
{
    public partial class Prihod
    {
        public Prihod()
        {
            Prilivi = new HashSet<Priliv>();
        }

        //kolone u bazi
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PrihodId { get; set; }

        [Required]
        [Display(Name = "Naziv")]
        [DataType(DataType.Text)]
        [StringLength(25, MinimumLength = 2)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        public string NazivPrihoda { get; set; }

        [Required]
        [Display(Name = "Iznos")]
        [DataType(DataType.Currency)]
        public decimal IznosPrihoda { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime VremePrihoda { get; set; }

        public ICollection<Priliv> Prilivi { get; set; }
    }
}
