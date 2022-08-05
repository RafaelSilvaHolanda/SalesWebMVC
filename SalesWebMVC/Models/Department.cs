using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.Models {
    public class Department {
        [Key]
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"[a-zA-Z\s]{5,30}", ErrorMessage = "Somente Letras de 5 a 15 caracteres")]
        public string Name { get; set; }

       

    }
}
