using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestShop.Models;

namespace TestShop.Areas.Admin.Models
{
    public class ProductListViewModel
    {
        public int? Id { get; set; }
        public string CategoryName { get; set; } = "";
        public ICollection<Product> Products { get; set; }
    }
}