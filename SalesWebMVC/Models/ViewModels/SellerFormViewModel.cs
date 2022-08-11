using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.Models.ViewModels {
    public class SellerFormViewModel {
        public Seller Seller { get; set; }

        public ICollection<Department> Departments { get; private set; }

        public SellerFormViewModel(ICollection<Department> departments) {
            Departments = departments;
        }

        public SellerFormViewModel(Seller seller, ICollection<Department> departments) {
            Seller = seller;
            Departments = departments;
        }

        public SellerFormViewModel() {

        }
    }
}
