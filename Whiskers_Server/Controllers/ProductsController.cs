using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DAL;
using BAL;
namespace Whiskers_Server.Controllers
{
    public class ProductsController : ApiController
    {
        //get all bottles
        [Route("api/Products/Bottles")]
        public IHttpActionResult Get()
        {
            try
            {
                IEnumerable<Bottle> bottles = BLLProducts.GetAllBottlesAction();
                if (bottles == null)
                {
                    return Content(HttpStatusCode.NotFound, $"No bottles were found!!");
                }

                return Ok(bottles);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //get top five bottles
        [Route("api/Products/TopFiveBottles")]
        public IHttpActionResult GetTopFiveBottles()
        {
            try
            {
                IEnumerable<TopRatedBottle> bottles = BLLProducts.GetTopFiveBottlesAction();
                if (bottles == null)
                {
                    return Content(HttpStatusCode.NotFound, $"There are no bottles at the db!!");
                }

                return Ok(bottles);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
