using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpeculatorVersionOne.Models
{
    public partial class Trosak
    {
        public Trosak()
        {
            Kupovine = new HashSet<Kupovina>();
        }

        //kolone u bazi
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TrosakId { get; set; }

        [Required]
        [Display(Name = "Naziv")]
        [DataType(DataType.Text)]
        [StringLength(25, MinimumLength = 2)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        public string NazivTroska { get; set; }

        [Required]
        [Display(Name = "Iznos")]
        [DataType(DataType.Currency)]
        public decimal IznosTroska { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime VremeTroska { get; set; }

        public ICollection<Kupovina> Kupovine { get; set; }
    }
}
