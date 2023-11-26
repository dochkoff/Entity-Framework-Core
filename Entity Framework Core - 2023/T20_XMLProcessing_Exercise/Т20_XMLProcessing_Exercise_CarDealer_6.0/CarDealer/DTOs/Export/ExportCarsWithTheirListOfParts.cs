using System;
using System.Xml.Serialization;

namespace CarDealer.DTOs.Export
{
    [XmlType("car")]
    public class ExportCarsWithTheirListOfParts
    {
        [XmlAttribute("make")]
        public string Make { get; set; }

        [XmlAttribute("model")]
        public string Model { get; set; }

        [XmlAttribute("traveled-distance")]
        public long TraveledDistance { get; set; }

        [XmlElement("parts")]
        public ExportPartsDTO Parts { get; set; } // Change to ExportPartsDTO instead of ExportPartsDTO[]
    }
}


