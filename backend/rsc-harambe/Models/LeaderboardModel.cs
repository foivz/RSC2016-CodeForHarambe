using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rsc_harambe.Models
{
    public class LeaderboardModel
    {
        public string name { get; set; }
        public int gameCount { get; set; }
        public int totalPoints { get; set; }
    }
}