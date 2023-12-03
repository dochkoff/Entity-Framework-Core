using System;
using System.ComponentModel.DataAnnotations;

namespace Medicines.DataProcessor.ImportDtos
{
    public class ImportPatientDto
    {
        [Required]
        [MinLength(5)]
        [MaxLength(100)]
        public string FullName { get; set; }

        [Required]
        [Range(0, 2)]
        public int AgeGroup { get; set; }

        [Required]
        [Range(0, 1)]
        public int Gender { get; set; }


        public List<int> Medicines { get; set; }
    }
}

