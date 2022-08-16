using SalesWebMVC.Data;
using SalesWebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace SalesWebMVC.Services {
    public class DepartmentService {

        private readonly SalesWebMvcContext _context;
        public DepartmentService(SalesWebMvcContext context) {
            _context = context;
        }

        public async Task<List<Department>> GetDepartmentsFromDbAsync() {
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();
        }

        public void SaveInDatabase(Department department) {
            _context.Add(department);
            _context.SaveChanges();
        }
    }
}
