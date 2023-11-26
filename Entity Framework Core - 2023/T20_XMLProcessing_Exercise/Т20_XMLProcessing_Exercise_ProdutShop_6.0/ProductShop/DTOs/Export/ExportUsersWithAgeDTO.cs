using System;
using System.Xml.Serialization;

namespace ProductShop.DTOs.Export
{
    [XmlType("User")]
    public class ExportUsersWithAgeDTO
    {
        [XmlElement("firstName")]
        public string FirstName { get; set; } = null!;

        [XmlElement("lastName")]
        public string LastName { get; set; } = null!;

        [XmlElement("age")]
        public int? Age { get; set; } = null!;

        [XmlElement("SoldProducts")]
        public virtual ExportSoldProductsWithCountDTO ProductsWithCount { get; set; } = null!;
    }
}