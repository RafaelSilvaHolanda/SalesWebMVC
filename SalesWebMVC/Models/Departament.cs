using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.Models {
    public class Departament {
        [Key]
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"[a-zA-Z]{5,15}", ErrorMessage = "Somente Letras de 5 a 15 caracteres")]
        public string Name { get; set; }

       

    }
}
