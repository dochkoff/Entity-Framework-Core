using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Invoices.DataProcessor.ImportDto
{
    [XmlType("Address")]
    public class ImportAdressDto
    {
        [Required]
        [MinLength(10)]
        [MaxLength(20)]
        [XmlElement("StreetName")]
        public string StreetName { get; set; } = null!;

        [Required]
        [XmlElement("StreetNumber")]
        public int StreetNumber { get; set; }

        [Required]
        [XmlElement("PostCode")]
        public string PostCode { get; set; } = null!;

        [Required]
        [MinLength(5)]
        [MaxLength(15)]
        [XmlElement("City")]
        public string City { get; set; } = null!;

        [Required]
        [MinLength(5)]
        [MaxLength(15)]
        [XmlElement("Country")]
        public string Country { get; set; } = null!;
    }
}

//•	StreetName – text with length [10…20] (required)
//•	StreetNumber – integer (required)
//•	PostCode – text (required)
//•	City – text with length [5…15] (required)
//•	Country – text with length [5…15] (required)