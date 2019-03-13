using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TestShop.Areas.Admin.Models;
using TestShop.Areas.Admin.Utils;
using TestShop.Models;
using TestShop.Repositories;

namespace TestShop.Areas.Admin.Controllers
{
    [Authorize(Users = "test@shop.ru")]
    public class OrderController : AsyncController
    {
        UnitOfWork unitOfWork;
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public OrderController()
        {
            unitOfWork = new UnitOfWork();
        }

        // GET: Admin/Order
        public async Task<ActionResult> Index()
        {
            List<OrderViewModel> orders = new List<OrderViewModel>();
            var allOrders = unitOfWork.Order.GetAll().OrderByDescending(ord => ord.Id);

            if (allOrders.Count() > 0)
            {
                for (int idx = 0; idx < allOrders.Count(); idx++)
                {
                    var order = allOrders.ElementAt(idx);
                    var userId = order.Customer.UserId;
                    var user = await UserManager.FindByIdAsync(userId);
                    orders.Add(new OrderViewModel { CustomerEmail = user.Email, Order = order });
                }
            }

            return View(orders);
        }

        public ActionResult EditStatus(int? id)
        {
            int identity = 0;
            if (id.HasValue)
                identity = id.Value;

            var order = unitOfWork.Order.Get(identity);
            ViewBag.StatusList = OrderState.GelList();

            return PartialView("_EditStatus", order);
        }

        public ActionResult Products(int? id)
        {
            int identity = 0;
            if (id.HasValue)
                identity = id.Value;

            var order = unitOfWork.Order.Get(identity);
            foreach (var item in order.OrderItems)
            {
                item.Product = unitOfWork.OrderItem.Get(item.Id).Product;
            }

            return PartialView("_Products", order);
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}