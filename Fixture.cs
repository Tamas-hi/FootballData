using System;
using System.Collections.Generic;
using System.Text;

namespace FootballData
{
    public class HomeTeam
    {
        public int team_id { get; set; }
        public string team_name { get; set; }
        public string logo { get; set; }
    }

    public class AwayTeam
    {
        public int team_id { get; set; }
        public string team_name { get; set; }
        public string logo { get; set; }
    }

    public class Score
    {
        public string halftime { get; set; }
        public string fulltime { get; set; }
        public object extratime { get; set; }
        public object penalty { get; set; }
    }

    public class Fixture
    {
        public int fixture_id { get; set; }
        public int league_id { get; set; }
        public DateTime event_date { get; set; }
        public int event_timestamp { get; set; }
        public int firstHalfStart { get; set; }
        public int secondHalfStart { get; set; }
        public string round { get; set; }
        public string status { get; set; }
        public string statusShort { get; set; }
        public int elapsed { get; set; }
        public string venue { get; set; }
        public string referee { get; set; }
        public HomeTeam homeTeam { get; set; }
        public AwayTeam awayTeam { get; set; }
        public int goalsHomeTeam { get; set; }
        public int goalsAwayTeam { get; set; }
        public Score score { get; set; }
    }

    public class FixtureApi
    {
        public int results { get; set; }
        public List<Fixture> fixtures { get; set; }
    }

    public class RootFixtureObject
    {
        public FixtureApi api { get; set; }
    }
}
