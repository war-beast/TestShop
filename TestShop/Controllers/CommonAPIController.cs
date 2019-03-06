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
        UnitOfWork unitOfWork;

        public CommonAPIController()
        {
            unitOfWork = new UnitOfWork();
        }

        [HttpPost]
        [Route("SortTable")]
        public IHttpActionResult SortTable([FromBody] FilterViewModels filter)
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
            return Ok(productList);
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
