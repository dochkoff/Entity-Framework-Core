using System;
using System.Xml.Serialization;
using Medicines.Data.Models.Enums;

namespace Medicines.DataProcessor.ExportDtos
{
    [XmlType("Patient")]
    public class ExportPatientXmlDto
    {
        [XmlAttribute("Gender")]
        public string Gender { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("AgeGroup")]
        public string AgeGroup { get; set; }

        [XmlArray("Medicines")]
        public ExportMedicinesXmlDto[] Medicines { get; set; }
    }
}

