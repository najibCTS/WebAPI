using System.ComponentModel.DataAnnotations;

namespace Posp.Models
{
    public class Item
    {
        private string itCode;
        [Required]
        [Display(Name = "Item Code")]
        public string ITCODE { get { return itCode; } set { itCode = value.Trim(); } }
        [Required]
        [Display(Name = "Description")]
        public string ITDESC { get; set; }
        [Display(Name = "Rate")]
        public decimal? ITRATE { get; set; }
    }
}