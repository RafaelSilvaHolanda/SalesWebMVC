using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Data;
using SalesWebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMVC.Services.Exceptions;
namespace SalesWebMVC.Services {
    public class SellerService {

        private readonly SalesWebMvcContext _context;
        public SellerService(SalesWebMvcContext context) {
            _context = context;
        }

        public List<Seller> GetSellersFromDB() {
            return _context.Seller.Include(x=>x.Department).ToList();
        }

        public void SaveInDatabase(Seller seller) {
            _context.Add(seller);
            _context.SaveChanges();
        }

        public Seller FindSellerById(int id) {
            return _context.Seller.Include(x=> x.Department).FirstOrDefault(x => x.Id == id);
        }


        public void TryRemoveSeller(int id) {
            Seller seller = FindSellerById(id);

            if (seller != null) {
                RemoveSellerFromDatabase(seller);
            }
           
        }

        private void RemoveSellerFromDatabase(Seller seller) {
            _context.Remove(seller);
            _context.SaveChanges();
        }

       
        public void TryUpdateSeller(Seller seller) {

            if (!SellerInDatabase(seller.Id)) {
                throw new NotFoundException("O vendedor não está cadastrado");
            }
            UpdateDatabase(seller);
            
        }

        public Boolean SellerInDatabase(int id) {
            return _context.Seller.Any(x => x.Id == id);
        }

        private void UpdateDatabase(Seller seller) {
            try {
                _context.Update(seller);
                _context.SaveChanges();
            }catch (DbUpdateConcurrencyException e) {
                throw new DbConcurrencyException(e.Message);
            }
            
        }

        

    }




}
