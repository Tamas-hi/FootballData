using System;
using System.Collections.Generic;
using System.Text;

namespace FootballData
{
    public class Team
    {
        public int team_id { get; set; }
        public string name { get; set; }
        public string teamName { get; set; }
        public string code { get; set; }
        public string logo { get; set; }
        public string country { get; set; }
        public bool is_national { get; set; }
        public int founded { get; set; }
        public string venue_name { get; set; }
        public string venue_surface { get; set; }
        public string venue_address { get; set; }
        public string venue_city { get; set; }
        public int venue_capacity { get; set; }
        public int rank { get; set; }
        public int points { get; set; }
        public AllApi all { get; set; }
    }

    public class AllApi
    {
        public int matchsPlayed { get; set; }
        public int win { get; set; }
        public int draw { get; set; }
        public int lose { get; set; }
        public int goalsFor { get; set; }
        public int goalsAgainst { get; set; }
    }

    public class TeamApi
    {
        public int results { get; set; }
        public List<Team> teams { get; set; }
    }

    public class RootTeamObject
    {
        public TeamApi api { get; set; }
    }
}
