using System;
using Medicines.Data.Models;

namespace Medicines.DataProcessor.ExportDtos
{
    public class ExportMedicinesJsonDto
    {
        public string Name { get; set; }
        public string Price { get; set; }
        public ExportPharmacyJsonDto Pharmacy { get; set; }
    }
}

