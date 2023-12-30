using System;
using System.ComponentModel.DataAnnotations;

namespace Rental.Core.Models
{
    public class PropertyModel
    {
        [Required(ErrorMessage = "Полето {0} е задължително")]
        [MaxLength(200)]
        [Display(Name = "Адрес")]
        public string Location { get; set; } = null!;

        [Required(ErrorMessage = "Полето {0} е задължително")]
        [Display(Name = "Площ")]
        public decimal Area { get; set; }

        [Display(Name = "Цена")]
        public decimal? Price { get; set; }
    }
}

