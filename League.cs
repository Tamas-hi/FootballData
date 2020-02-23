using System;
using System.Collections.Generic;
using System.Text;

namespace FootballData
{

    public class Fixtures
    {
        public bool events { get; set; }
        public bool lineups { get; set; }
        public bool statistics { get; set; }
        public bool players_statistics { get; set; }
    }

    public class Coverage
    {
        public bool standings { get; set; }
        public Fixtures fixtures { get; set; }
        public bool players { get; set; }
        public bool topScorers { get; set; }
        public bool predictions { get; set; }
        public bool odds { get; set; }
    }

    public class League
    {
        public int league_id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string country { get; set; }
        public string country_code { get; set; }
        public int season { get; set; }
        public string season_start { get; set; }
        public string season_end { get; set; }
        public string logo { get; set; }
        public string flag { get; set; }
        public int standings { get; set; }
        public int is_current { get; set; }
        public Coverage coverage { get; set; }
    }

    public class LeagueApi
    {
        public int results { get; set; }
        public List<League> leagues { get; set; }
    }

    public class RootLeagueObject
    {
        public LeagueApi api { get; set; }
    }
}
