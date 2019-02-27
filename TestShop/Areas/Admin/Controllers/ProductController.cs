using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestShop.Areas.Admin.Models;
using TestShop.Models;
using TestShop.Repositories;

namespace TestShop.Areas.Admin.Controllers
{
    [Authorize(Users = "test@shop.ru")]
    public class ProductController : Controller
    {
        UnitOfWork unitOfWork;

        public ProductController()
        {
            unitOfWork = new UnitOfWork();
        }

        public ActionResult Index(int? id)
        {
            int identity = 0;
            if (id.HasValue)
                identity = id.Value;

            var category = unitOfWork.Categories.Get(identity);
            var products = category == null ? new List<Product>() : category.Products;
            var model = new ProductListViewModel { Id = id, CategoryName = category?.Name, Products = products };
            return View(model);
        }

        public ActionResult Details(int? id)
        {
            int identity = 0;
            if (id.HasValue)
                identity = id.Value;

            var product = unitOfWork.Products.Get(identity);
            return PartialView("_Details", product);
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}