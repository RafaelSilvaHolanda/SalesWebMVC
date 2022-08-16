using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMVC.Services;
using SalesWebMVC.Models;
using SalesWebMVC.Models.ViewModels;
using SalesWebMVC.Services.Exceptions;
using System.Diagnostics;

namespace SalesWebMVC.Controllers {
    public class SellersController : Controller {

        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;
        public SellersController(SellerService sellerService, DepartmentService departmentService) {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        public async Task<IActionResult> Index() {
            var sellers = await _sellerService.GetSellersFromDbAsync();
            return View(sellers);
        }

        public async Task<IActionResult> Create() {
            var departments = await _departmentService.GetDepartmentsFromDbAsync();
            var viewModel = new SellerFormViewModel(departments);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller) {
            if (ModelState.IsValid) {
                await _sellerService.SaveInDatabaseAsync(seller);
                return RedirectToAction(nameof(Index));
            }
            var departments = await _departmentService.GetDepartmentsFromDbAsync();
            return View(new SellerFormViewModel(seller, departments));

        }

        public async Task<IActionResult> Delete(int? id) {

            if (!(await ValidSeller(id))) {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            var seller = await _sellerService.FindSellerByIdAsync(id.Value);

            return View(seller);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id) {

            
            return await TryDeletingSeller(id);
        }


        public async Task<IActionResult> TryDeletingSeller(int id) {
            
            try{
                await _sellerService.TryRemoveSellerAsync(id);
            } catch (IntegrityException e) {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }catch (Exception e) {
                return RedirectToAction(nameof(Error), new { message = "Unexpected Error" });
            }
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Details(int? id) {

            if (!(await ValidSeller(id))) {
                return RedirectToAction(nameof(Error), new {message =  "Id not found"});
            }
            var seller = await _sellerService.FindSellerByIdAsync(id.Value);
            return View(seller);
        }

        public async Task<IActionResult> Edit(int? id) {

            if (!(await ValidSeller(id))){
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            var seller = await _sellerService.FindSellerByIdAsync(id.Value);
            var departments = await _departmentService.GetDepartmentsFromDbAsync();
            return View(new SellerFormViewModel(seller, departments));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Seller seller) {
            if (ModelState.IsValid) {

                return await TryUpdatingSellerPage(seller);

            }
            List<Department> departments = await _departmentService.GetDepartmentsFromDbAsync();            
            return View(new SellerFormViewModel(seller, departments));

        }

        private async Task<IActionResult> TryUpdatingSellerPage(Seller seller) {
            try {
                await _sellerService.TryUpdateSellerAsync(seller);

            } catch (ApplicationException e) {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            } catch (Exception) {
                return RedirectToAction(nameof(Error), new { message = "Unexpected Error" });
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task<Boolean> ValidSeller(int? id) {
            Boolean valid = false;

            if (id != null) {
                if (await _sellerService.SellerInDatabaseAsync(id.Value)) {
                    valid = true;
                }
            }
            return valid;
        }

        public IActionResult Error(string message) {
            var viewModel = new ErrorViewModel(Activity.Current?.Id ?? HttpContext.TraceIdentifier,message);

            return View(viewModel);
        }
    }
}
