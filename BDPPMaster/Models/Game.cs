using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BDPPMaster.Models
{
    public class Game
    {
        public int GameId { get; set; }
        public int Team1_Score { get; set; }
        public int Team2_Score { get; set; }
        public Team Team1 { get; set; }
        public Team Team2 { get; set; }

        DateTime Start { get; set; }
        DateTime End { get; set; }
    }
}