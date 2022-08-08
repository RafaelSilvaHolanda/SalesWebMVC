using SalesWebMVC.Data;
using SalesWebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.Services {
    public class SellerService {

        private readonly SalesWebMvcContext _context;
        public SellerService(SalesWebMvcContext context) {
            _context = context;
        }

        public List<Seller> GetSellersFromDB() {
            return _context.Seller.ToList();
        }

        public void SaveInDatabase(Seller seller) {
            _context.Add(seller);
            _context.SaveChanges();
        }
    }
}
