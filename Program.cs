using System;
using System.Collections.Generic;

namespace FootballData
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var teams = new Service().GetAsync();
            foreach(var team in teams)
            {
                Console.WriteLine(team.name + team.founded);
            }
        }
    }
}
