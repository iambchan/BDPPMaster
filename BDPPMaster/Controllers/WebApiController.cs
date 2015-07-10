using BDPPMaster.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;

namespace BDPPMaster.Controllers
{
    public class WebApiController : ApiController
    {
        //required, all of: FirstName, LastName, ScreenName, BDLoginName, Email
        public IHttpActionResult CreateNewPlayer(string FirstName, string LastName, string ScreenName, string BDLoginName, string Email, string RFID)
        {
            if (!ModelState.IsValid) { return BadRequest(); }

            var missingItems = new StringBuilder();
            #region Validation (builds missingItems)
            if (FirstName == null)
            {
                if (missingItems.Length > 0) { missingItems.Append(", "); }
                missingItems.Append("First Name");
            }
            if (LastName == null)
            {
                if (missingItems.Length > 0) { missingItems.Append(", "); }
                missingItems.Append("Last Name");
            }
            if (BDLoginName == null)
            {
                if (missingItems.Length > 0) { missingItems.Append(", "); }
                missingItems.Append("BD Domain Login Name");
            }
            if (ScreenName == null)
            {
                if (missingItems.Length > 0) { missingItems.Append(", "); }
                missingItems.Append("Screen Name");
            }
            if (Email == null)
            {
                if (missingItems.Length > 0) { missingItems.Append(", "); }
                missingItems.Append("Email");
            }
            #endregion
            if (missingItems.Length > 0) { return BadRequest(String.Format("Missing the following information: {0}.", missingItems.ToString())); }
            
            var newPlayer = DBHelper.CreateNewPlayer(FirstName, LastName, ScreenName, BDLoginName, Email, RFID);
            return Ok(newPlayer);
        }

        //required, one of: FirstName, LastName, ScreenName, BDLoginName, Email
        public IHttpActionResult GetPlayerInfo(string FirstName, string LastName, string ScreenName, string BDLoginName, string Email, string RFID)
        {
            if (!ModelState.IsValid) { return BadRequest(); }
            if (FirstName == null && LastName == null && ScreenName == null && BDLoginName == null && Email == null && RFID == null) { return BadRequest("Please fill in at least one field"); }

            var player = DBHelper.GetPlayerInfo(FirstName, LastName, ScreenName, BDLoginName, Email, RFID);
            return Ok(player);
        }
    }
}