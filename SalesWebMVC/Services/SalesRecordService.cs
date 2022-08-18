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

       

        public async Task<List<Department>> GetGroupedSales(DateTime? minDate, DateTime? maxDate){
            var salesRecords = await GetSalesFromDb(minDate, maxDate).ToListAsync();
            var departments = OrganizeByDepartment(salesRecords);

            return departments;
        }
        private static List<Department> OrganizeByDepartment(List<SalesRecord> salesRecords) {                      

             return salesRecords.Select(x => x.Seller.Department).Distinct().ToList();
             
        }




    }
}
