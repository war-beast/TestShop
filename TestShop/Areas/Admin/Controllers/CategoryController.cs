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
            return View(categories);
        }

        public ActionResult Create()
        {
            return PartialView("_Create");
        }

        public ActionResult Edit(int? id)
        {
            int identity = 0;
            if (id.HasValue)
                identity = id.Value;

            var category = unitOfWork.Categories.Get(identity);
            return PartialView("_Edit", category);
        }

        public ActionResult Delete(int? id)
        {
            int identity = 0;
            if (id.HasValue)
                identity = id.Value;

            var category = unitOfWork.Categories.Get(identity);
            return PartialView("_Delete", category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteRecord(Category category)
        {
            unitOfWork.Categories.Delete(category.Id);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}