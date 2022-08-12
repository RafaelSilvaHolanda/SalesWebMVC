using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace SalesWebMVC.Models {
    public class Seller {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage ="É necessário preencher o {0}")]
        [RegularExpression(@"[a-zA-Z\s]{5,30}", ErrorMessage = "O nome deve conter somente Letras de 5 a 30 caracteres")]
        public string Name { get; set; }

        [Display(Name="E-mail")]
        [Required(ErrorMessage = "É necessário preencher o {0}")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Digite um email válido")]
        public string Email { get; set; }
        
        [Display(Name = "Data de aniversário")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "É necessário preencher a {0}")]
        public DateTime BirthDate { get; set; }
        
        [Display(Name="Salário")]
        [Required(ErrorMessage = "É necessário preencher o {0}")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        [Range(1000, double.MaxValue, ErrorMessage = "O Salário precisa ser acima de 1000.00 reais")]
        public double Salary { get; set; }

        [Display(Name="Departamento")]
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
        public List<SalesRecord> Sales { get; private set; } = new List<SalesRecord>();

        public Seller() {            
        }

        public Seller(string name, string email, DateTime birthDate, double salary, Department departament) {
            Name = name;
            Email = email;
            BirthDate = birthDate;
            Salary = salary;
            Department = departament;
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
