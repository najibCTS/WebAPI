using System;
using System.ComponentModel.DataAnnotations;

namespace Posp.Models
{
    public class Pomaster
    {
        private string pono;
        [Required]
        [Display(Name = "Product No")]
        public string PONO { get { return pono; } set { pono = value.Trim(); } }
        [Display(Name = "Date")]
        public DateTime? PODATE { get; set; }
        [Required]
        [Display(Name = "Supplier No")]
        public string SUPLNO { get; set; }
    }
}