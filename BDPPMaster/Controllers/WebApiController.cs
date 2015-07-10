using BDPPMaster.Helpers;
using BDPPMaster.Models;
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
        #region CREATE
        //required, all of: FirstName, LastName, ScreenName, BDLoginName, Email
        [Route("api/Create/Player")]
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
        //ScreenLoginName is ScreenName or BDLoginName
        [Route("api/Create/Team")]
        public IHttpActionResult CreateNewTeam(string ScreenLoginName1, string ScreenLoginName2)
        {
            if (!ModelState.IsValid) { return BadRequest(); }
            var player1_Id = DBHelper.GetPlayerIdByScreenLoginNames(ScreenLoginName1, ScreenLoginName1);
            var teamId = 0;

            if (ScreenLoginName2 != null)
            {
                var player2_Id = DBHelper.GetPlayerIdByScreenLoginNames(ScreenLoginName2, ScreenLoginName2);
                teamId = DBHelper.CreateNewTeam(player1_Id, player2_Id);
            }
            else {
                teamId = DBHelper.CreateNewTeam(player1_Id);
            }
            return Ok(teamId);
        }
        [Route("api/Create/Game/{TeamId1:int}/{TeamId2:int}")]
        public IHttpActionResult CreateNewGame(int TeamId1, int TeamId2)
        {
            if (!ModelState.IsValid) { return BadRequest(); }
            var gameId = DBHelper.CreateNewGame(TeamId1, TeamId2);
            return Ok(gameId);
        }
        #endregion

        #region GET
        //required, one of: FirstName, LastName, ScreenName, BDLoginName, Email
        [Route("api/Get/Player")]
        public IHttpActionResult GetPlayerInfo(string FirstName, string LastName, string ScreenName, string BDLoginName, string Email, string RFID)
        {
            if (!ModelState.IsValid) { return BadRequest(); }
            if (FirstName == null && LastName == null && ScreenName == null && BDLoginName == null && Email == null && RFID == null) { return BadRequest("Please fill in at least one field"); }
            var player = DBHelper.GetPlayerInfo(FirstName, LastName, ScreenName, BDLoginName, Email, RFID);
            return Ok(player);
        }
        [Route("api/Get/Team/{TeamId:int}")]
        public IHttpActionResult GetTeamInfo(int TeamId)
        {
            if (!ModelState.IsValid) { return BadRequest(); }
            var team = DBHelper.GetTeamPlayersInfo(TeamId);
            return Ok(team);
        }
        [Route("api/Get/Game/{GameId:int}")]
        public IHttpActionResult GetGameInfo(int GameId)
        {
            if (!ModelState.IsValid) { return BadRequest(); }
            var team = DBHelper.GetTeamPlayersInfo(GameId);
            return Ok(team);
        }


        #endregion
    }
}