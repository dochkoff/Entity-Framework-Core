using System;
using Invoices.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Invoices.DataProcessor.ExportDto
{
    public class ExportProductsMostClientsDto
    {
        public string Name { get; set; }

        public double Price { get; set; }

        public CategoryType Category { get; set; }

        public ExportClientDto[] Clients { get; set; }
    }
}

