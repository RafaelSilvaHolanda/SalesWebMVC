using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMVC.Models;

namespace SalesWebMVC.Controllers {
    public class DepartmentsController : Controller {
        public IActionResult Index() {
            var departaments = new List<Departament>();
            departaments.Add(new Departament(1, "Eletronics"));
            departaments.Add(new Departament(2, "Fashion"));
            return View(departaments);
        }
    }
}
