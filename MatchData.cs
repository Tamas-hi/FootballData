using System;
using System.Collections.Generic;
using System.Text;

namespace FootballData
{
    public class MatchData
    {
        public string LeagueName { get; set; }
        public string LeagueSeason { get; set; }
        public string Country { get; set; }
        public string SeasonStart { get; set; }
        public string SeasonEnd { get; set; }
        public string numberOfTeams { get; set; }
        public Dictionary<int, string> Teams { get; set; }
        public string Winner { get; set; }
        public List<string> Relegated { get; set; }
        public string numberOfMatches { get; set; }
        public string numberOfGoals { get; set; }

    }
}
