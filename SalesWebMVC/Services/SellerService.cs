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

        public Seller FindSellerById(int id) {
            return _context.Seller.FirstOrDefault(x => x.Id == id);
        }

        public Boolean TryRemoveSeller(int id) {
            Seller seller = FindSellerById(id);
            var sellerFound = false;

            if (seller != null) {
                RemoveSellerFromDatabase(seller);
                sellerFound = true;
            }
            return sellerFound;
        }

        private void RemoveSellerFromDatabase(Seller seller) {
            _context.Remove(seller);
            _context.SaveChanges();
        }
    }


}
