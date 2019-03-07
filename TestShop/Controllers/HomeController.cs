using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TestShop.Models;
using TestShop.Repositories;

namespace TestShop.Controllers
{
    public class HomeController : AsyncController
    {
        UnitOfWork unitOfWork;

        public HomeController()
        {
            unitOfWork = new UnitOfWork();
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Тестовый интернет-магазин";

            var categories = unitOfWork.Categories.GetAll();

            return View(categories);
        }

        public ActionResult SortingList()
        {
            var sort = new SortType().GetSortTypes();
            return PartialView("_SortingList", sort);
        }

        public ActionResult CatList()
        {
            var categories = unitOfWork.Categories.GetAll();
            return PartialView("_CatList", categories);
        }

        public ActionResult CatNavigation()
        {
            var categories = unitOfWork.Categories.GetAll();
            return PartialView("_CatNavigation", categories);
        }

        public ActionResult SortedProducts()
        {
            var productList = unitOfWork.Products.GetAll();
            return PartialView("_ProductList", productList);
        }

        [HttpPost]
        public ActionResult SortedProducts(FilterViewModels filter)
        {
            IEnumerable<Product> productList = new List<Product>();
            var allProducts = unitOfWork.Products.GetAll();

            foreach (var category in filter.Categories)
            {
                var query = allProducts.Where(prod => prod.Price >= filter.MinPrice && prod.Price <= filter.MaxPrice && prod.CategoryId == category.Id);
                productList = productList.Concat(query);
                var list = new List<Product>();
                list.AddRange(productList);
            }


            switch (filter.Sort)
            {
                case 1:
                    productList = productList.OrderBy(pr => pr.Price);
                    break;
                case 2:
                    productList = productList.OrderBy(pr => pr.Rating);
                    break;
            }
            return PartialView("_ProductList", productList);
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
