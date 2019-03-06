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

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
