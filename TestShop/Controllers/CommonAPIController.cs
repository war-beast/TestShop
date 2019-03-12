using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TestShop.Models;
using TestShop.Repositories;

namespace TestShop.Controllers
{
    [RoutePrefix("api/Common")]
    public class CommonAPIController : ApiController
    {
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

        UnitOfWork unitOfWork;

        public CommonAPIController()
        {
            unitOfWork = new UnitOfWork();
        }

        [Authorize]
        [HttpPost]
        [Route("CreateCategory")]
        public async Task<IHttpActionResult> SendShopingCard([FromBody] ShopingCardViewModel model)
        {
            string retVal = "";
            var customer = new Customer();

            var user = await UserManager.FindByNameAsync(User.Identity.Name);
            customer = unitOfWork.Customer.GetAll().FirstOrDefault(cst => cst.UserId == user.Id);
            if (customer == null)
            {
                unitOfWork.Customer.Create(new Customer { UserId = user.Id });
                unitOfWork.Save();
                customer = unitOfWork.Customer.GetAll().FirstOrDefault(cst => cst.UserId == user.Id);
            }

            List<OrderItem> orderItems = new List<OrderItem>(model.Items.Count);
            decimal totalSum = 0;
            foreach(var item in model.Items)
            {
                var prod = unitOfWork.Products.Get(item.Id);
                orderItems.Add(new OrderItem { Product = prod, Count = item.Count });
                totalSum += item.Count * prod.Price;
            }

            var order = new Order {
                Customer = customer,
                Address = model.Adress == "" ? "Самовывоз" : model.Adress,
                Date = DateTime.Now,
                OrderItems = orderItems,
                State = "",
                Sum = totalSum };

            unitOfWork.Order.Create(order);
            try { 
            unitOfWork.Save();}
            catch (Exception)
            {
                retVal = "Ошибка обработки формы.";
                return BadRequest(retVal);
            }

            return Ok(retVal);
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
