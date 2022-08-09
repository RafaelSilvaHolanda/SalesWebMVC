using SalesWebMVC.Data;
using SalesWebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.Services {
    public class DepartmentService {

        private readonly SalesWebMvcContext _context;
        public DepartmentService(SalesWebMvcContext context) {
            _context = context;
        }

        public List<Department> GetDepartmentsFromDB() {
            return _context.Department.OrderBy(x => x.Name).ToList();
        }

        public void SaveInDatabase(Department department) {
            _context.Add(department);
            _context.SaveChanges();
        }
    }
}
