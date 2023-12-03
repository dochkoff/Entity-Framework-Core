using System;
using System.Xml.Serialization;
using Medicines.Data.Models.Enums;

namespace Medicines.DataProcessor.ExportDtos
{
    [XmlType("Medicine")]
    public class ExportMedicinesXmlDto
    {
        [XmlAttribute("Category")]
        public string Category { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Price")]
        public string Price { get; set; }

        [XmlElement("Producer")]
        public string Producer { get; set; }

        [XmlElement("BestBefore")]
        public string BestBefore { get; set; }
    }
}

