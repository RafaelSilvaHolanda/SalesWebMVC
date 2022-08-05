using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace SalesWebMVC.Models {
    public class Seller {
        [Key]
        public int Id { get;private set; }

        [Required]
        [RegularExpression(@"[a-zA-Z\s]{5,30}", ErrorMessage = "O nome deve conter somente Letras de 5 a 30 caracteres")]
        public string Name { get; set; }
        
        [Required]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Digite um email válido")]
        public string Email { get; set; }
        
        [Required]
        [RegularExpression(@"/^\d{4}/(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])$/", ErrorMessage = "Digite uma data no formato YYYY/MM/DD")]
        public DateTime BirthDate { get; set; }
        
        [Required]
        [Range(1000, double.MaxValue, ErrorMessage = "O Salário precisa ser acima de 1000.00 reais")]
        public double Salary { get; set; }

        public Department Departament { get; set; }
        public List<SalesRecord> Sales { get; private set; } = new List<SalesRecord>();

        public Seller() {
                
        }

        public Seller(int id, string name, string email, DateTime birthDate, double salary, Department departament) {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            Salary = salary;
            Departament = departament;
        }

        public void AddSales(SalesRecord sale) {
            Sales.Add(sale);
        }

        public void RemoveSale(SalesRecord sale) {
            Sales.Remove(sale);
        }

        public double TotalSales(DateTime initialDate,DateTime finalDate) {
            return Sales.Where(sr => (sr.Date >= initialDate && sr.Date <= finalDate)).Sum(sr => sr.Amount);
        }
    }
}
