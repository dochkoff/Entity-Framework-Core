using System;
using Invoices.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Invoices.DataProcessor.ImportDto
{
    public class ImportInvoiceDto
    {
        [Required]
        [Range(1_000_000_000, 1_500_000_000)]
        public int Number { get; set; }

        [Required]
        public string IssueDate { get; set; } = null!;

        [Required]
        public string DueDate { get; set; } = null!;

        [Required]
        public decimal Amount { get; set; }

        [Required]
        [EnumDataType(typeof(CurrencyType))]
        public CurrencyType CurrencyType { get; set; }

        [Required]
        public int ClientId { get; set; }
    }
}

//•	Number – integer in range  [1,000,000,000…1,500,000,000] (required)
//•	IssueDate – DateTime (required)
//•	DueDate – DateTime (required)
//•	Amount – decimal (required)
//•	CurrencyType – enumeration of type CurrencyType, with possible values (BGN, EUR, USD) (required)
//•	ClientId – integer, foreign key (required)


//"Number": 1427940691,
//    "IssueDate": "2022-08-29T00:00:00",
//    "DueDate": "2022-10-28T00:00:00",
//    "Amount": 913.13,
//    "CurrencyType": 1,
//    "ClientId": 1