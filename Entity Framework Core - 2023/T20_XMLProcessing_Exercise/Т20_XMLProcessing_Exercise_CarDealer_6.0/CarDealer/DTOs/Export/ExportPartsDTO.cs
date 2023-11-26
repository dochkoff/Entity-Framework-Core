using System;
using System.Xml.Serialization;

namespace CarDealer.DTOs.Export
{
    [XmlType("parts")]
    public class ExportPartsDTO
    {
        [XmlElement("part")]
        public PartDTO[] Parts { get; set; }
    }
}


