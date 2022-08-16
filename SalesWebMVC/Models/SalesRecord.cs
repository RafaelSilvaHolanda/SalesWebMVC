using System;

using System.ComponentModel.DataAnnotations;

using SalesWebMVC.Models.Enums;
namespace SalesWebMVC.Models {
    public class SalesRecord {
        [Key]
        public int Id { get;private set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; private set; }

        [Required]
        [RegularExpression(@"[0-9,]")]
        public double Amount { get; private set; }
        [Required]
        public SaleStatus Status { get; set; }
        [Required]
        public Seller Seller { get; set; }

        public SalesRecord() {

        }

        public SalesRecord(DateTime date, double amount, SaleStatus status, Seller seller) {
            Date = date;
            Amount = amount;
            Status = status;
            Seller = seller;
        }

    }
}
