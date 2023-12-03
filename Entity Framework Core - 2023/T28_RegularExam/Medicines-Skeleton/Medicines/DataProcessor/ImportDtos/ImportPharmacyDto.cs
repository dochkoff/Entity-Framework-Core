using System;
using Medicines.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Medicines.DataProcessor.ImportDtos
{
    [XmlType("Pharmacy")]
    public class ImportPharmacyDto
    {
        [Required]
        [XmlAttribute("non-stop")]
        public string IsNonStop { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        //[StringLength(14)] may be unnecessary
        [RegularExpression(@"^\(\d{3}\) \d{3}-\d{4}$")]
        public string PhoneNumber { get; set; }

        [XmlArray("Medicines")]
        public ImportMedicineDto[] Medicines { get; set; }
    }
}

