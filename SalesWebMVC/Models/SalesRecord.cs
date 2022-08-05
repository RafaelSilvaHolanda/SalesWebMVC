using System;

using System.ComponentModel.DataAnnotations;

using SalesWebMVC.Models.Enums;
namespace SalesWebMVC.Models {
    public class SalesRecord {
        [Key]
        public int Id { get;private set; }

        [Required]
        [RegularExpression(@"/^\d{4}/(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])$/", ErrorMessage = "Digite uma data no formato YYYY/MM/DD")]
        public DateTime Date { get; private set; }

        [Required]
        [RegularExpression(@"[0-9.]")]
        public double Amount { get; private set; }
        [Required]
        public SaleStatus Status { get; set; }

        public Seller Seller { get; set; }

        public SalesRecord() {

        }

        public SalesRecord(int id, DateTime date, double amount, SaleStatus status, Seller seller) {
            Id = id;
            Date = date;
            Amount = amount;
            Status = status;
            Seller = seller;
        }

    }
}
