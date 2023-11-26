using System;
using System.Xml.Serialization;

namespace ProductShop.DTOs.Export
{
    [XmlType("Users")]
    public class ExportUsersWithCount
    {
        [XmlElement("count")]
        public int Count { get; set; }

        [XmlArray("users")]
        public ExportUsersWithAgeDTO[] UsersWithAge { get; set; } = null!;
    }
}