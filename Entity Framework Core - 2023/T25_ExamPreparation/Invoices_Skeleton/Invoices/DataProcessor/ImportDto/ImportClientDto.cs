using System;
using Invoices.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Invoices.DataProcessor.ImportDto
{
    [XmlType("Client")]
    public class ImportClientDto
    {
        [Required]
        [MinLength(10)]
        [MaxLength(25)]
        [XmlElement()]
        public string Name { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(15)]
        [XmlElement("NumberVat")]
        public string NumberVat { get; set; }

        [XmlArray("Addresses")]
        public ImportAdressDto[] Addresses { get; set; }
    }
}

//•	Name – text with length [10…25] (required)
//•	NumberVat – text with length [10…15] (required)
//•	Invoices – collection of type Invoicе
//•	Addresses – collection of type Address