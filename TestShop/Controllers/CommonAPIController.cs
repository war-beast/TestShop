using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var customer = await GetCustomer();
            List<OrderItem> orderItems = new List<OrderItem>(model.Items.Count);
            decimal totalSum = 0;

            if(model.Items == null) {
                return BadRequest("Ошибка отправки заказа. Пустой список товаров.");
            }

            foreach (var item in model.Items)
            {
                var prod = unitOfWork.Products.Get(item.Id);
                orderItems.Add(new OrderItem { Product = prod, Count = item.Count });
                totalSum += item.Count * prod.Price;
            }

            var order = new Order
            {
                Customer = customer,
                Address = model.Adress == "" ? "Самовывоз" : model.Adress,
                Date = DateTime.Now,
                OrderItems = orderItems,
                State = "",
                Sum = totalSum
            };
            unitOfWork.Order.Create(order);

            try
            {
                unitOfWork.Save();
            }
            catch (Exception)
            {
                retVal = "На сервере произошла ошибка при обработке заказа.";
                return BadRequest(retVal);
            }

            return Ok(retVal);
        }

        #region Вспомогательные методы
        private async Task<Customer> GetCustomer()
        {
            var user = await UserManager.FindByNameAsync(User.Identity.Name);
            var customer = unitOfWork.Customer.GetAll().FirstOrDefault(cst => cst.UserId == user.Id);
            if (customer == null)
            {
                unitOfWork.Customer.Create(new Customer { UserId = user.Id });
                unitOfWork.Save();
                customer = unitOfWork.Customer.GetAll().FirstOrDefault(cst => cst.UserId == user.Id);
            }

            return customer;
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
