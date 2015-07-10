using System;
using System.Collections.Generic;
//using System.Drawing;
using System.Linq;
using System.Web;

namespace BDPPMaster.Models
{
    public class Player
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ScreenName { get; set; }
        public string BDLoginName { get; set; }
        public string Email { get; set; }
        public string RFID { get; set; }

        public int PlayerId { get; set; }
        public int GamesWon { get; set; }
        public int GamesPlayed { get; set; }
        public TimeSpan TimePlayed { get; set; }

        //public Image profileImage { get; set; } //if images are stored as bytes in the database

        public double PercentWon(){
            return ((double) GamesWon / GamesPlayed);
    }

    }
     

}