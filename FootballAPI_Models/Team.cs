using System;
using System.Collections.Generic;
using System.Text;

namespace FootballData
{
    public class Team
    {
        public string team_id { get; set; }
        public string teamName { get; set; }
        public string name { get; set; }
        public int founded { get; set; }
        public string venue_name { get; set; }
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
