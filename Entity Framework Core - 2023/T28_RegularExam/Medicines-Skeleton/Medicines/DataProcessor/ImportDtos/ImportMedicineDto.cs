using System;
using Medicines.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Medicines.Data.Models.Enums;

namespace Medicines.DataProcessor.ImportDtos
{
    [XmlType("Medicine")]
    public class ImportMedicineDto
    {
        [Required]
        [XmlAttribute("category")]
        [Range(0, 4)]
        public int Category { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(150)]
        [XmlElement("Name")]
        public string Name { get; set; }

        [Required]
        [Range(0.01, 1000.00)]
        [XmlElement("Price")]
        public decimal Price { get; set; }

        [Required]
        [XmlElement("ProductionDate")]
        public string ProductionDate { get; set; }

        [Required]
        [XmlElement("ExpiryDate")]
        public string ExpiryDate { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        [XmlElement("Producer")]
        public string Producer { get; set; } = null!;
    }
}

