using System;
using System.Collections.Generic;
//using System.Drawing;
using System.Linq;
using System.Web;

namespace BDPPMaster.Models
{
    public class Player
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string screenName { get; set; }
        public string bdLoginName { get; set; }
        public string email { get; set; }
        public string rfid { get; set; }

        public int playerId { get; set; }
        public int gamesWon { get; set; }
        public int gamesPlayed { get; set; }
        public TimeSpan timePlayed { get; set; }

        //public Image profileImage { get; set; } //if images are stored as bytes in the database

        public double percentWon(){
            return ((double) gamesWon / gamesPlayed);
    }

    }
     

}