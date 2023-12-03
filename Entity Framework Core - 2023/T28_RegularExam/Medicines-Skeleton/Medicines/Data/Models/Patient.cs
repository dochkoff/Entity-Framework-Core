using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Medicines.Data.Models.Enums;

namespace Medicines.Data.Models
{
    public class Patient
    {
        public Patient()
        {
            PatientsMedicines = new List<PatientMedicine>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }

        [Required]
        public AgeGroup AgeGroup { get; set; }

        [Required]
        public Gender Gender { get; set; }

        public virtual ICollection<PatientMedicine> PatientsMedicines { get; set; }
    }
}

