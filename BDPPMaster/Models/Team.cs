using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BDPPMaster.Models
{
    public class Team
    {
        public int TeamId { get; set; }
        public int Player1_id { get; set; }
        public int Player2_id { get; set; }
    }
}