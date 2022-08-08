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
    public class AdminController : ApiController
    {
        [Route("api/Admin/AddBottle")]
        public IHttpActionResult PostBottle([FromBody] Bottle bottle)
        {
            try
            {
                bool isAddedToDb = BLLAdmin.AddBottleAction(bottle);
                if (!isAddedToDb)
                {
                    return Content(HttpStatusCode.NotFound, $"Failed to Add bottle, please check your fields and try again.");
                }

                return Ok("Bottle add to db Successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("api/Admin/DeleteBottle/{barcode:int}")]
        public IHttpActionResult DeleteBottle(int barcode) {
            try
            {
                bool isDeleted = BLLAdmin.DeleteBottleAction(barcode);
                if (!isDeleted)
                {
                    return Content(HttpStatusCode.NotFound, $"Failed to Delete bottle, please check your fields and try again.");
                }

                return Ok("Bottle Deleted from db Successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("api/Admin/DeleteUser/{userId:int}")]
        public IHttpActionResult DeleteUser(int userId)
        {
            try
            {
                bool isDeleted = BLLAdmin.DeleteUserAction(userId);
                if (!isDeleted)
                {
                    return Content(HttpStatusCode.NotFound, $"Failed to Delete user, please check your fields and try again.");
                }

                return Ok("User Deleted from db Successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("api/Admin/UpdateBottle/{bracodeToUpdate:int}")]
        public IHttpActionResult PutUpdateBottle(int bracodeToUpdate, [FromUri]double price)
        {
            try
            {
                bool isUpdated = BLLAdmin.UpdateBottleAction(price, bracodeToUpdate);
                if (!isUpdated)
                {
                    return Content(HttpStatusCode.NotFound, $"Failed to update bottle, please check your fields and try again.");
                }

                return Ok("Bottle updated Successfully in db");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        //get all users controller
        [Route("api/Admin/Users")]
        public IHttpActionResult Get()
        {
            try
            {
                IEnumerable<UserSummary> usersSummary = BLLAdmin.GetAllUsersAction();
                if (usersSummary == null)
                {
                    return Content(HttpStatusCode.NotFound, $"No users was found!!");
                }

                return Ok(usersSummary);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
