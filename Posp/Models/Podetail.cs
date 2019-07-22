using System;
using System.ComponentModel.DataAnnotations;

namespace Posp.Models
{
    public class Podetail
    {
        private string pono;
        private string itcode;
        [Required]
        [Display(Name = "Product No")]
        public string PONO { get { return pono; } set { pono = value.Trim(); } }
        [Required]
        [Display(Name = "Item Code")]
        public string ITCODE { get { return itcode; } set { itcode = value.Trim(); } }
        [Display(Name = "Quantity")]
        public int? QTY { get; set; }
    }
}