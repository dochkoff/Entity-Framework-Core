using System;
using System.Xml.Serialization;
using CarDealer.Models;

namespace CarDealer.DTOs.Import
{
    [XmlType("Sale")]
    public class ImportSaleDTO
    {
        [XmlElement("carId")]
        public int CarId { get; set; }

        [XmlElement("customerId")]
        public int CustomerId { get; set; }

        [XmlElement("discount")]
        public decimal Discount { get; set; }
    }
}

