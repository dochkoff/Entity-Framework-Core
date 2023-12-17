namespace Medicines.DataProcessor
{
    using Medicines.Data;
    using Medicines.Data.Models;
    using Medicines.Data.Models.Enums;
    using Medicines.DataProcessor.ImportDtos;
    using Medicines.Helpers;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Text;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid Data!";
        private const string SuccessfullyImportedPharmacy = "Successfully imported pharmacy - {0} with {1} medicines.";
        private const string SuccessfullyImportedPatient = "Successfully imported patient - {0} with {1} medicines.";


        public static string ImportPatients(MedicinesContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();
            int counter = 0;
            ICollection<Patient> patients = new List<Patient>();

            var patientDtos = JsonConvert.DeserializeObject<IEnumerable<ImportPatientDto>>(jsonString);

            foreach (var patientDto in patientDtos)
            {
                if (!IsValid(patientDto) || !Enum.IsDefined(typeof(AgeGroup), patientDto.AgeGroup) || !Enum.IsDefined(typeof(Gender), patientDto.Gender))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var patientToAdd = new Patient
                {
                    FullName = patientDto.FullName,
                    AgeGroup = (AgeGroup)patientDto.AgeGroup,
                    Gender = (Gender)patientDto.Gender,
                    PatientsMedicines = new List<PatientMedicine>() // Initialize as a list
                };

                foreach (var medicineId in patientDto.Medicines)
                {
                    if (patientToAdd.PatientsMedicines.Any(x => x.MedicineId == medicineId))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    PatientMedicine patientMedicine = new PatientMedicine()
                    {
                        Patient = patientToAdd,
                        MedicineId = medicineId,
                    };

                    patientToAdd.PatientsMedicines.Add(patientMedicine);
                }

                counter += patientToAdd.PatientsMedicines.Count;
                patients.Add(patientToAdd);
                sb.AppendLine(string.Format(SuccessfullyImportedPatient, patientToAdd.FullName, patientToAdd.PatientsMedicines.Count));

            }

            context.Patients.AddRange(patients);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }


        public static string ImportPharmacies(MedicinesContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();

            var pharmacyDtos = XmlSerializationHelper.Deserialize<ImportPharmacyDto[]>(xmlString, "Pharmacies");

            List<Pharmacy> pharmacies = new List<Pharmacy>();

            foreach (var pharmacyDto in pharmacyDtos)
            {
                if (!IsValid(pharmacyDto) || !bool.TryParse(pharmacyDto.IsNonStop, out bool isNonStop))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var pharmacyToAdd = new Pharmacy
                {
                    Name = pharmacyDto.Name,
                    PhoneNumber = pharmacyDto.PhoneNumber,
                    IsNonStop = isNonStop,
                    Medicines = new List<Medicine>()
                };

                foreach (var medicineDto in pharmacyDto.Medicines)
                {
                    if (!IsValid(medicineDto))
                    {
                        sb.AppendLine(ErrorMessage);
                    }
                    else
                    {
                        DateTime productionDate = DateTime.ParseExact(medicineDto.ProductionDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                        DateTime expiryDate = DateTime.ParseExact(medicineDto.ExpiryDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                        if (productionDate >= expiryDate)
                        {
                            sb.AppendLine(ErrorMessage);
                            continue;
                        }

                        var medicine = new Medicine
                        {
                            Category = (Category)medicineDto.Category,
                            Name = medicineDto.Name,
                            Price = medicineDto.Price,
                            ProductionDate = productionDate,
                            ExpiryDate = expiryDate,
                            Producer = medicineDto.Producer
                        };

                        if (pharmacyToAdd.Medicines.Any(m => m.Name == medicineDto.Name && m.Producer == medicineDto.Producer))
                        {
                            sb.AppendLine(ErrorMessage);
                            continue;
                        }
                        else
                        {
                            pharmacyToAdd.Medicines.Add(medicine);
                        }
                    }
                }

                pharmacies.Add(pharmacyToAdd);
                sb.AppendLine(string.Format(SuccessfullyImportedPharmacy, pharmacyToAdd.Name, pharmacyToAdd.Medicines.Count));
            }

            context.Pharmacies.AddRange(pharmacies);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}
