using BDPPMaster.Helpers;
using BDPPMaster.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using Phidgets;

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
        [Route("api/Create/Game/")]
        public IHttpActionResult CreateNewGame(int Team1_Id, int Team2_Id, int Team1_Score, int Team2_Score, DateTime StartDateTime, DateTime EndDateTime)
        {
            if (!ModelState.IsValid) { return BadRequest(); }
            var gameId = DBHelper.CreateNewGame(Team1_Id, Team2_Id, Team1_Score, Team2_Score, StartDateTime, EndDateTime);
            return Ok(gameId);
        }
        //[Route("api/Create/Game/{TeamId1:int}/{TeamId2:int}")]
        //public IHttpActionResult CreateNewGame(int TeamId1, int TeamId2)
        //{
        //    if (!ModelState.IsValid) { return BadRequest(); }
        //    var gameId = DBHelper.CreateNewGame(TeamId1, TeamId2);
        //    return Ok(gameId);
        //}
        [Route("api/Create/Game/ByPlayerScreenLoginName")]
        public IHttpActionResult CreateNewGameByPlayersScreenLoginName(string ScreenLoginName1, string ScreenLoginName2, int Team1_Score, int Team2_Score, DateTime StartDateTime, DateTime EndDateTime)
        {
            if (!ModelState.IsValid) { return BadRequest(); }
            var player1_Id = DBHelper.GetPlayerIdByScreenLoginNames(ScreenLoginName1, ScreenLoginName1);
            var player2_Id = DBHelper.GetPlayerIdByScreenLoginNames(ScreenLoginName2, ScreenLoginName2);

            var team1_Id = DBHelper.CreateNewTeam(player1_Id);
            var team2_Id = DBHelper.CreateNewTeam(player2_Id);

            var game_Id = DBHelper.CreateNewGame(team1_Id, team2_Id, Team1_Score, Team2_Score, StartDateTime, EndDateTime);
            return Ok(game_Id);
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
            if (player == null) { return NotFound(); }
            return Ok(player);
        }


        [Route("api/Get/Player/Id")]
        public IHttpActionResult GetPlayerIdByScreenLoginNames(string ScreenName, string BDLoginName)
        {
            if (!ModelState.IsValid) { return BadRequest(); }
            var playerId = DBHelper.GetPlayerIdByScreenLoginNames(ScreenName, BDLoginName);
            return Ok(playerId);
        }
        [Route("api/Get/Player/All")]
        public IHttpActionResult GetAllPlayers()
        {
            if (!ModelState.IsValid) { return BadRequest(); }
            var players = DBHelper.GetAllPlayers();
            if (players == null) { return NotFound(); }
            return Ok(players);
        }
        [Route("api/Get/Player/Top/{Num:int}")]
        public IHttpActionResult GetPlayersTop(int Num) 
        {
            if (!ModelState.IsValid) { return BadRequest(); }
            var players = DBHelper.GetPlayersTop(Num);
            if (players == null) { return NotFound(); }
            return Ok(players);
        }
        [Route("api/Get/Player/MostGames")]
        public IHttpActionResult GetPlayersMostGames()
        {
            if (!ModelState.IsValid) { return BadRequest(); }
            var playerIds = DBHelper.GetPlayersMostGames();
            if (playerIds == null) { return NotFound(); }
            return Ok(playerIds);
        }
        [Route("api/Get/Team/{TeamId:int}")]
        public IHttpActionResult GetTeamInfo(int TeamId)
        {
            if (!ModelState.IsValid) { return BadRequest(); }
            var team = DBHelper.GetTeamPlayersInfo(TeamId);
            if (team == null) { return NotFound(); }
            return Ok(team);
        }
        [Route("api/Get/Game/{GameId:int}")]
        public IHttpActionResult GetGameInfo(int GameId)
        {
            if (!ModelState.IsValid) { return BadRequest(); }
            var game = DBHelper.GetGameInfo(GameId);
            if (game == null) { return NotFound(); }
            return Ok(game);
        }
        #endregion
    }
}