using System.ComponentModel.DataAnnotations;

namespace Posp.Models
{
    public class Supplier
    {
        private string supNo;
        [Required]
        [Display(Name = "Supplier No")]
        public string SUPLNO
        {
            get => supNo;
            set => supNo = value.Trim();
        }
        [Required]
        [Display(Name = "Name")]
        public string SUPLNAME { get; set; }
        [Display(Name = "Address")]
        public string SUPLADDR { get; set; }
    }
}