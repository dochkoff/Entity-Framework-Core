namespace Medicines.DataProcessor
{
    using System.Globalization;
    using Medicines.Data;
    using Medicines.DataProcessor.ExportDtos;
    using Medicines.Helpers;
    using Newtonsoft.Json;

    public class Serializer
    {
        public static string ExportPatientsWithTheirMedicines(MedicinesContext context, string date)
        {
            if (!DateTime.TryParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
            {
                return "Invalid date format!";
            }

            var patients = context.Patients
                .Where(p => p.PatientsMedicines.Any(pm => pm.Medicine.ProductionDate > parsedDate))
                .Select(p => new ExportPatientXmlDto()
                {
                    Gender = p.Gender.ToString().ToLower(),
                    Name = p.FullName,
                    AgeGroup = p.AgeGroup.ToString(),
                    Medicines = p.PatientsMedicines
                        .Where(pm => pm.Medicine.ProductionDate > parsedDate)
                        .OrderByDescending(pm => pm.Medicine.ExpiryDate)
                        .ThenBy(pm => pm.Medicine.Price)
                        .Select(pm => new ExportMedicinesXmlDto()
                        {
                            Category = pm.Medicine.Category.ToString().ToLower(),
                            Name = pm.Medicine.Name,
                            Price = pm.Medicine.Price.ToString("F2", CultureInfo.InvariantCulture),
                            Producer = pm.Medicine.Producer,
                            BestBefore = pm.Medicine.ExpiryDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                        }).ToArray()
                })
                .OrderByDescending(p => p.Medicines.Count())
                .ThenBy(p => p.Name)
                .ToArray();

            return XmlSerializationHelper.Serialize(patients, "Patients");
        }

        public static string ExportMedicinesFromDesiredCategoryInNonStopPharmacies(MedicinesContext context, int medicineCategory)
        {
            var medicines = context.Medicines
                .Where(m => (int)m.Category == medicineCategory && m.Pharmacy.IsNonStop)
                .OrderBy(m => m.Price)
                .ThenBy(m => m.Name)
                .Select(m => new ExportMedicinesJsonDto()
                {
                    Name = m.Name,
                    Price = m.Price.ToString("F2"),
                    Pharmacy = new ExportPharmacyJsonDto()
                    {
                        Name = m.Pharmacy.Name,
                        PhoneNumber = m.Pharmacy.PhoneNumber
                    }

                })
                .ToList();

            return JsonConvert.SerializeObject(medicines, Formatting.Indented);

        }
    }
}

