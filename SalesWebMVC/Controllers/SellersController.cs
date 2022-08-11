using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMVC.Services;
using SalesWebMVC.Models;
using SalesWebMVC.Models.ViewModels;
using SalesWebMVC.Services.Exceptions;

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
                return NotFound();
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

            return View();
        }

        public IActionResult Edit(int? id) {

            if (!ValidSeller(id)) {
                return NotFound();
            }

            var seller = _sellerService.FindSellerById(id.Value);
            var departments = _departmentService.GetDepartmentsFromDB();
            return View(new SellerFormViewModel(seller, departments));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Seller seller) {
            if (ModelState.IsValid) {

                return UpdatingSeller(seller);

            }
            var departments = _departmentService.GetDepartmentsFromDB();            
            return View(new SellerFormViewModel(seller, departments));

        }

        private IActionResult UpdatingSeller(Seller seller) {
            try {
                _sellerService.TryUpdateSeller(seller);

            } catch (DbConcurrencyException e) {
                return NotFound();
            } catch (NotFoundException e) {
                return BadRequest();
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
    }
}
