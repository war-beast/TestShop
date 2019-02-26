using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestShop.Repositories;

namespace TestShop.Areas.Admin.Controllers
{
    [Authorize(Users = "test@shop.ru")]
    public class CategoryController : Controller
    {
        UnitOfWork unitOfWork;

        public CategoryController()
        {
            unitOfWork = new UnitOfWork();
        }

        // GET: Admin/Category
        public ActionResult Index()
        {
            var categories = unitOfWork.Categories.GetAll();
            var list = categories.ToList();
            return View(categories);
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}