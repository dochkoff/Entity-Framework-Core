using System.Xml.Serialization;

namespace ProductShop.DTOs.Export
{
    [XmlType("SoldProducts")]
    public class ExportSoldProductsWithCountDTO
    {
        [XmlElement("count")]
        public int Count { get; set; }

        [XmlArray("products")]
        public virtual ExportSoldProductsDTO[] SoldProducts { get; set; } = null!;
    }
}