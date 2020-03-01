using System.Collections.Generic;

namespace FootballData
{
    public class RootApi
    {
        public int results { get; set; }
        public List<string> fixtures { get; set; }
    }

    public class RootRoundObject
    {
        public RootApi api { get; set; }
    }
}