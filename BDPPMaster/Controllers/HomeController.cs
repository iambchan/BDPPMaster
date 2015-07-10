using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BDPPMaster.Models;
using BDPPMaster.Helpers;
using Microsoft.AspNet.SignalR;
using BDPPMaster.Hubs;
using System.Globalization;

namespace BDPPMaster.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Rankings()
        {
            ViewBag.Message = "Rankings";
            //get users from db




            return View();
        }
        [ChildActionOnly]
        public ActionResult mostGamesWon()
        {
            //
            // var model = repository.GetThingByParameter(parameter1);
            // var partialViewModel = new PartialViewModel(model);

            List<Player> players = new List<Player>();

            Player p1 = new Player();
            p1.ScreenName = "p1";
            p1.GamesPlayed = 10;
            p1.GamesWon = 7;

            Player p2 = new Player();
            p2.ScreenName = "p2";
            p2.GamesPlayed = 12;
            p2.GamesWon = 7;

            Player p3 = new Player();
            p3.ScreenName = "p3";
            p3.GamesPlayed = 0;
            p3.GamesWon = 7;

            Player p4 = new Player();
            p4.ScreenName = "p4";
            p4.GamesPlayed = 13;
            p4.GamesWon = 7;

            players.Add(p1);
            players.Add(p2);
            players.Add(p3);
            players.Add(p4);
            return PartialView("_mostGamesWon", players);
        }
        [ChildActionOnly]
        public ActionResult percentGamesWon()
        {
            //
            // var model = repository.GetThingByParameter(parameter1);
            // var partialViewModel = new PartialViewModel(model);

            List<Player> players = new List<Player>();

            Player p1 = new Player();
            p1.ScreenName = "p1";
            p1.GamesPlayed = 10;
            p1.GamesWon = 7;

            Player p2 = new Player();
            p2.ScreenName = "p2";
            p2.GamesPlayed = 12;
            p2.GamesWon = 7;

            Player p3 = new Player();
            p3.ScreenName = "p3";
            p3.GamesPlayed = 0;
            p3.GamesWon = 7;

            Player p4 = new Player();
            p4.ScreenName = "p4";
            p4.GamesPlayed = 13;
            p4.GamesWon = 7;

            players.Add(p1);
            players.Add(p2);
            players.Add(p3);
            players.Add(p4);
            return PartialView("_percentGamesWon", players);
        }
        [ChildActionOnly]
        public ActionResult numGamesPlayed()
        {
            //
            // var model = repository.GetThingByParameter(parameter1);
            // var partialViewModel = new PartialViewModel(model);

            List<Player> players = new List<Player>();

            Player p1 = new Player();
            p1.ScreenName = "p1";
            p1.GamesPlayed = 10;
            p1.GamesWon = 7;

            Player p2 = new Player();
            p2.ScreenName = "p2";
            p2.GamesPlayed = 12;
            p2.GamesWon = 7;

            Player p3 = new Player();
            p3.ScreenName = "p3";
            p3.GamesPlayed = 0;
            p3.GamesWon = 7;

            Player p4 = new Player();
            p4.ScreenName = "p4";
            p4.GamesPlayed = 13;
            p4.GamesWon = 7;

            players.Add(p1);
            players.Add(p2);
            players.Add(p3);
            players.Add(p4);
            return PartialView("_numGamesPlayed", players);
        }
        [ChildActionOnly]
        public ActionResult timePlayed()
        {
            //
            // var model = repository.GetThingByParameter(parameter1);
            // var partialViewModel = new PartialViewModel(model);

            List<Player> players = new List<Player>();
            Player p1 = new Player();
            p1.ScreenName = "p1";
            p1.GamesPlayed = 10;
            p1.GamesWon = 7;

            Player p2 = new Player();
            p2.ScreenName = "p2";
            p2.GamesPlayed = 12;
            p2.GamesWon = 7;
            Player p3 = new Player();
            p3.ScreenName = "p3";
            p3.GamesPlayed = 0;
            p3.GamesWon = 7;

            Player p4 = new Player();
            p4.ScreenName = "p4";
            p4.GamesPlayed = 13;
            p4.GamesWon = 7;

            players.Add(p1);
            players.Add(p2);
            players.Add(p3);
            players.Add(p4);
            return PartialView("_timePlayed", players);
        }
        public ActionResult PlayerBoard()
        {
            ViewBag.Message = "Player Board";

            return View();
        }

        public ActionResult GameSetup()
        {
            ViewBag.Message = "Game setup";

            return View();
        }

        // Todo pass in team ids 
        public ActionResult ScoreBoard(int idp1, string imagep1, string namep1, int idp2, string imagep2, string namep2)
        //public ActionResult ScoreBoard()    
        {
            ViewBag.Message = "ScoreBoard";
            List<Player> players = new List<Player>() { new Player() { PlayerId = idp1, ImageNameWithExt = imagep1, ScreenName = namep1 }, new Player() { PlayerId = idp2, ImageNameWithExt = imagep2, ScreenName = namep2 } };
            return View(players);
        }

        public ActionResult GameOptions()
        {
            List<Player> players = DBHelper.GetAllPlayers();
            return View(players);
        }

        public ActionResult AddPlayer(string rfid)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<PPHub>();
            context.Clients.All.addPlayer(rfid);
            return Content("hi cassandra");
        }
        public ActionResult EndGame(string ScreenLoginName1, string ScreenLoginName2, string StartDateTimeString, string EndDateTimeString, int Team1_Score = 0, int Team2_Score = 0)
        {
            var player1_Id = DBHelper.GetPlayerIdByScreenLoginNames(ScreenLoginName1, ScreenLoginName1);
            var player2_Id = DBHelper.GetPlayerIdByScreenLoginNames(ScreenLoginName2, ScreenLoginName2);

            var team1_Id = DBHelper.CreateNewTeam(player1_Id);
            var team2_Id = DBHelper.CreateNewTeam(player2_Id);

            DateTime dt = new DateTime();

            DateTime StartDateTime = dt;
            DateTime EndDateTime = dt;

            var game_Id = DBHelper.CreateNewGame(team1_Id, team2_Id, Team1_Score, Team2_Score, StartDateTime, EndDateTime);


            return View("Index");

        }

    }
}