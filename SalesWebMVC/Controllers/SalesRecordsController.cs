using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMVC.Services;
using SalesWebMVC.Models;
namespace SalesWebMVC.Controllers {
    public class SalesRecordsController : Controller {

        private readonly SalesRecordService _salesRecordService;

        public SalesRecordsController(SalesRecordService salesRecordService) {
            _salesRecordService = salesRecordService;
        }

        public IActionResult Index() {
            return View();
        }
        public async Task<IActionResult> SimpleSearch(DateTime? minDate, DateTime? maxDate) {
            var sales = await _salesRecordService.FindSalesByDateAsync(minDate, maxDate);
            SetDateOrDefault(minDate, maxDate);

            return View(sales);
        }
        public async Task<IActionResult> GroupSearch(DateTime? minDate, DateTime? maxDate) {
            var sales = await _salesRecordService.GetGroupedSales(minDate, maxDate);
            SetDateOrDefault(minDate, maxDate);
            return View(sales);
        }

        private void SetDateOrDefault(DateTime? minDate, DateTime? maxDate) {
            if (!minDate.HasValue) {
                minDate = new DateTime(2018, 1, 1);
            }
            if (!maxDate.HasValue) {
                maxDate = DateTime.Now;
            }
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");

        }

    }
}
