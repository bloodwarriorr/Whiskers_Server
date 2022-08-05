using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BAL;
using DAL;
namespace Whiskers_Server.Controllers
{
   
    public class UserController : ApiController
    {
        
        public IHttpActionResult Post([FromBody]User user)
        {
            try
            {
                bool isSignedUp= BLLUsers.RegisterUser(user);
                if (!isSignedUp)
                {
                    return Content(HttpStatusCode.NotFound, $"User failed to create, please check your fields and try again.");
                }

                return Ok("User created Successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("api/User/login")]
        public IHttpActionResult Post([FromBody] UserLoginDetails value)
        {

            try
            {
                User returnedUser = BLLUsers.LoginUser(value);
                if (returnedUser == null)
                {
                    return Content(HttpStatusCode.NotFound, $"User with email={value.Email} was not found in DB! ");
                }

                return Ok(returnedUser);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("api/User/{userId:int}")]
        public IHttpActionResult Get(int userId)
        {
            try
            {
                IEnumerable<Order> orders = BLLUsers.GetOrdersByUser(userId);
                if (orders == null)
                {
                    return Content(HttpStatusCode.NotFound, $"user {userId} don't have any orders yet!");
                }

                return Ok(orders);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("api/User/order")]
        public IHttpActionResult PostOrder([FromBody]NewOrder order)
        {
            
            try
            {
                bool result = BLLUsers.BuyCart(order);
                if (!result)
                {
                    return Content(HttpStatusCode.NotFound, $"Cant add an order, check your fields!");
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}
