using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
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
            ViewBag.Title = "Home Page";

            return View();
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

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
