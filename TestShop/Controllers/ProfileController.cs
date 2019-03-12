using Microsoft.AspNet.Identity.Owin;
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
    [Authorize]
    public class ProfileController : AsyncController
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

        public ProfileController()
        {
            unitOfWork = new UnitOfWork();
        }

        // GET: Profile
        public async Task<ActionResult> Index()
        {
            List<Order> orders = new List<Order>();
            var user = await UserManager.FindByNameAsync(User.Identity.Name);
            var customer = unitOfWork.Customer.GetAll().FirstOrDefault(cst => cst.UserId == user.Id);
            if (customer != null) {
                var userOrders = unitOfWork.Order.Find(order => order.CustomerId == customer.Id);
                orders.AddRange(userOrders);
            }

            return View(orders);
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}