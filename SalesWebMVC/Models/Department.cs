using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.Models {
    public class Department {
        [Key]
        public int Id { get; private set; }

        [Required]
        [RegularExpression(@"[a-zA-Z\s]{5,30}", ErrorMessage = "Nome deve possuir somente Letras de 5 a 30 caracteres")]
        public string Name { get; set; }

        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();

        public Department() {

        }

        public Department(string name) {           
            Name = name;            
        }

        public void AddSeller(Seller seller) {
            Sellers.Add(seller);
        }

        public void RemoveSeller(Seller seller) {
            Sellers.Remove(seller);
        }

        public double TotalSales(DateTime initialDate, DateTime finalDate) {
            return Sellers.Sum(seller => seller.TotalSales(initialDate,finalDate));
        }
    }
}
