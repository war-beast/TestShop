﻿using System;
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

        [HttpPost]
        public ActionResult SortTable(FilterViewModels filter)
        {
            var productList = unitOfWork.Products.Find(pr => pr.Price >= filter.MinPrice && pr.Price <= filter.MaxPrice);
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
