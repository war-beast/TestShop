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
    [Authorize(Users = "test@shop.ru")]
    [RoutePrefix("api/Admin")]
    public class AdminAPIController : ApiController
    {
        UnitOfWork unitOfWork;

        public AdminAPIController()
        {
            unitOfWork = new UnitOfWork();
        }

        [HttpPost]
        [Route("CreateCategory")]
        public async Task<IHttpActionResult> CreateCategory([FromBody] Category category)
        {
            string retVal = "ok";

            if (!ModelState.IsValid)
                return BadRequest("Ошибка при заполнении полей формы.");

            await Task.Run(() => {
                unitOfWork.Categories.Create(category);
                unitOfWork.Save();
            });

            return Ok(retVal);
        }

        [HttpPost]
        [Route("EditCategory")]
        public async Task<IHttpActionResult> EditCategory([FromBody] Category category)
        {
            string retVal = "ok";

            if (!ModelState.IsValid)
                return BadRequest("Ошибка при заполнении полей формы.");

            await Task.Run(() => {
                unitOfWork.Categories.Update(category);
                unitOfWork.Save();
            });

            return Ok(retVal);
        }

        [HttpPost]
        [Route("CreateProduct")]
        public async Task<IHttpActionResult> CreateProduct([FromBody] Product product)
        {
            string retVal = "ok";

            if (!ModelState.IsValid)
                return BadRequest("Ошибка при заполнении полей формы.");

            await Task.Run(() => {
                unitOfWork.Products.Create(product);
                unitOfWork.Save();
            });

            return Ok(retVal);
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
