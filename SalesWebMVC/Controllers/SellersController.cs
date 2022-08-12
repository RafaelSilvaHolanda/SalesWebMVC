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

        public IActionResult Index() {
            var sellers = _sellerService.GetSellersFromDB();
            return View(sellers);
        }

        public IActionResult Create() {
            var departments = _departmentService.GetDepartmentsFromDB();
            var viewModel = new SellerFormViewModel(departments);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SellerFormViewModel form) {
            if (ModelState.IsValid) {
                _sellerService.SaveInDatabase(form.Seller);
                return RedirectToAction(nameof(Index));
            }
            return View(form);

        }

        public IActionResult Delete(int? id) {

            if (!ValidSeller(id)) {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            var seller = _sellerService.FindSellerById(id.Value);

            return View(seller);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id) {

            _sellerService.TryRemoveSeller(id);
            return RedirectToAction(nameof(Index));
        }



        public IActionResult Details(int? id) {

            if (!ValidSeller(id)) {
                return RedirectToAction(nameof(Error), new {message =  "Id not found"});
            }
            var seller = _sellerService.FindSellerById(id.Value);
            return View(seller);
        }

        public IActionResult Edit(int? id) {

            if (!ValidSeller(id)) {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            var seller = _sellerService.FindSellerById(id.Value);
            var departments = _departmentService.GetDepartmentsFromDB();
            return View(new SellerFormViewModel(seller, departments));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Seller seller) {
            if (ModelState.IsValid) {

                return TryUpdatingSellerPage(seller);

            }
            var departments = _departmentService.GetDepartmentsFromDB();            
            return View(new SellerFormViewModel(seller, departments));

        }

        private IActionResult TryUpdatingSellerPage(Seller seller) {
            try {
                _sellerService.TryUpdateSeller(seller);

            } catch (ApplicationException e) {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            } catch (Exception) {
                return RedirectToAction(nameof(Error), new { message = "Unexpected Error" });
            }
            return RedirectToAction(nameof(Index));
        }

        private Boolean ValidSeller(int? id) {
            Boolean valid = false;

            if (id != null) {
                if (_sellerService.SellerInDatabase(id.Value)) {
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
