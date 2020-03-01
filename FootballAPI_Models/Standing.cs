using System;
using System.Collections.Generic;
using System.Text;

namespace FootballData
{
    public class StandingApi
    {
        public int results { get; set; }
        public List<List<Team>> standings { get; set; }
        public All api { get; set; }
    }

    public class All
    {
        public Dictionary<string, int> all;
    }

    public class RootStandingObject
    {
        public StandingApi api { get; set; }
    }
}
