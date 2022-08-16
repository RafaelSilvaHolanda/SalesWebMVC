using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMVC.Data;
using SalesWebMVC.Models;
using Microsoft.EntityFrameworkCore;
namespace SalesWebMVC.Services {
    public class SalesRecordService {
        private readonly SalesWebMvcContext  _context;

        public SalesRecordService(SalesWebMvcContext context) {
            _context = context;
        }

        public  async Task<List<SalesRecord>> FindSalesByDateAsync(DateTime? minDate, DateTime? maxDate) {

            var salesRecords = _context.SalesRecord.Select(x => x);

            if (minDate.HasValue) {
                salesRecords = salesRecords.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue) {
                salesRecords = salesRecords.Where(x => x.Date <= maxDate.Value);
            }

            return await salesRecords.Include(x => x.Seller).Include(x => x.Seller.Department).OrderByDescending(x => x.Date).ToListAsync();

        } 
    }
}
