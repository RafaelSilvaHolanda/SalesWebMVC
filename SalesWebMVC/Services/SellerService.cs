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

        public async Task<List<Seller>> GetSellersFromDbAsync() {
            return await _context.Seller.Include(x=>x.Department).ToListAsync();
        }

        public async Task SaveInDatabaseAsync(Seller seller) {
            _context.Add(seller);
            await _context.SaveChangesAsync();
        }

        public async Task<Seller> FindSellerByIdAsync(int id) {
            return await _context.Seller.Include(x=> x.Department).FirstOrDefaultAsync(x => x.Id == id);
        }


        public async Task TryRemoveSellerAsync(int id) {

            var seller = await FindSellerByIdAsync(id);

            if (seller != null) {
                await RemoveSellerFromDatabaseAsync(seller);
            }
           
        }

        private async Task RemoveSellerFromDatabaseAsync(Seller seller) {
            _context.Remove(seller);
            await _context.SaveChangesAsync();
        }

       
        public async Task TryUpdateSellerAsync(Seller seller) {

            if (!(await SellerInDatabaseAsync(seller.Id))) {
                throw new NotFoundException("O vendedor não está cadastrado");
            }
            await UpdateDatabaseAsync(seller);
            
        }

        public async Task<Boolean> SellerInDatabaseAsync(int id) {
            return await _context.Seller.AnyAsync(x => x.Id == id);
        }

        private async Task UpdateDatabaseAsync(Seller seller) {
            try {
                _context.Update(seller);
                await _context.SaveChangesAsync();
            }catch (DbUpdateConcurrencyException e) {
                throw new DbConcurrencyException(e.Message);
            }
            
        }

        

    }




}
