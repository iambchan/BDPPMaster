using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace BDPPMaster.Controllers
{
    public class WebApiController : ApiController
    {
        public IHttpActionResult GetPlayerInfo()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}