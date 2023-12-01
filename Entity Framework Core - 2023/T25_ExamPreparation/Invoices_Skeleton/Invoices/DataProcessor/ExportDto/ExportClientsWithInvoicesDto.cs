using System;
using Invoices.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Invoices.DataProcessor.ExportDto
{
    [XmlType("Client")]
    public class ExportClientsWithInvoicesDto
    {
        [XmlAttribute("InvoicesCount")]
        public int InvoicesCount { get; set; }

        [XmlElement]
        public string ClientName { get; set; }

        [XmlElement]
        public string VatNumber { get; set; }

        [XmlArray]
        public ExportInvoicesDto[] Invoices { get; set; }
    }
}

