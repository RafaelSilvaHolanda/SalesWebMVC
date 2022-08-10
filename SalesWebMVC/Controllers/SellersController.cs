using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMVC.Services;
using SalesWebMVC.Models;
using SalesWebMVC.Models.ViewModels;


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
            
            if (id == null) {
                return NotFound();
            }
            var obj = _sellerService.FindSellerById(id.Value);

            if (obj == null) {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id) {

            _sellerService.TryRemoveSeller(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id) {

            if (id == null) {
                return NotFound();
            }
            var obj = _sellerService.FindSellerById(id.Value);

            if (obj == null) {
                return NotFound();
            }
            return View(obj);
        }
    }
}
