using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMVC.Data;
using SalesWebMVC.Models;
using Microsoft.EntityFrameworkCore;
namespace SalesWebMVC.Services {
    public class SalesRecordService {
        private readonly SalesWebMvcContext _context;

        public SalesRecordService(SalesWebMvcContext context) {
            _context = context;
        }



        public async Task<List<SalesRecord>> FindSalesByDateAsync(DateTime? minDate, DateTime? maxDate) {

            return await GetSalesFromDb(minDate, maxDate).ToListAsync();
        }


        private IQueryable<SalesRecord> GetSalesFromDb(DateTime? minDate, DateTime? maxDate) {
            var salesRecords = _context.SalesRecord.Select(x => x);

            if (minDate.HasValue) {
                salesRecords = salesRecords.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue) {
                salesRecords = salesRecords.Where(x => x.Date <= maxDate.Value);
            }

            return salesRecords.Include(x => x.Seller).Include(x => x.Seller.Department).OrderByDescending(x => x.Date);
        }

       

        public async Task<List<Department>> FindSalesAgroupedAsync(DateTime? minDate, DateTime? maxDate){
            var salesRecords = await GetSalesFromDb(minDate, maxDate).ToListAsync();
            var departments = OrganizeByDepartment(salesRecords);

                return await departments;
        }
        private async Task<List<Department>> OrganizeByDepartment(List<SalesRecord> saleRecords) {

            var departmentReference = new List<Department>();

            var allDepartments = await _context.Department.Select(x => x).ToListAsync();

            foreach (var department in allDepartments) {

                foreach (var sale in saleRecords) {
                    if (department.Name == sale.Seller.Department.Name) {
                        departmentReference.Add(sale.Seller.Department);
                        break;
                    }
                }
            }
            return departmentReference;
        }




    }
}
