using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BDPPMaster.Models
{
    public class Player
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string screenName { get; set; }
        public int gamesWon { get; set; }
        public int gamesPlayed { get; set; }
        public TimeSpan timePlayed { get; set; }


        public double percentWon(){
            return ((double) gamesWon / gamesPlayed);
    }

    }
     

}