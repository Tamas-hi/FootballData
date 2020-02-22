using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WikiClientLibrary;
using WikiClientLibrary.Client;
using WikiClientLibrary.Pages;
using WikiClientLibrary.Pages.Queries.Properties;
using WikiClientLibrary.Sites;

namespace FootballData
{
    public class Program
    {
        static int tableWidth = 100;
        public static void Main(string[] args)
        {   
           // PrintRow("Team Name", "Wins", "Draws", "Losses", "Points");

            //GetStandings();
            //GetFixturesByRounds();
            //GetRounds();
            Dictionary<dynamic, dynamic> IdNamePairs = new Dictionary<dynamic, dynamic>();

            dynamic result = new Service().GetIdentifiers();

            foreach (Match match in Regex.Matches(result.ToString(), @"(?<!\w)Q\d\w+"))
            {
                if (!IdNamePairs.ContainsKey(match.Value))
                {
                    IdNamePairs.Add(match.Value, null);
                }
            }

            foreach (Match match in Regex.Matches(result.ToString(), @"(?<!\w)P\d\w+"))
            {
                if (!IdNamePairs.ContainsKey(match.Value))
                {
                    if(IdNamePairs.Count < 50)
                    IdNamePairs.Add(match.Value, null);
                }
            }
            
            var x = string.Format("{0}", string.Join("|", IdNamePairs.Keys));
            

            var entityNames = new Service().GetEntityNames(x);

            IdNamePairs.Clear();
            foreach(var data in entityNames)
            {
                foreach(var y in data)
                {
                    if(y is JObject)
                    foreach(var z in y)
                    {
                      foreach(var w in z)
                      {
                                IdNamePairs[w.id] = w.labels.en.value;
                      }
                    }
                }
            }

            foreach(KeyValuePair<dynamic, dynamic> pairs in IdNamePairs)
            {
                Console.WriteLine(pairs.Key + " " + pairs.Value);
            }
        }

        public static List<string> GetRounds()
        {
            var rootRounds = new Service().GetRounds(2);
            var apiRounds = rootRounds.api;
            var round = apiRounds.fixtures;

            return round;
        }

        public static void GetStandings()
        {
            var rootStandings = new Service().GetStandings(2); // 2018/2019 Premier League
            var apiStandings = rootStandings.api;
            var standings = apiStandings.standings;
            foreach (var s in standings)
            {
                foreach (var t in s)
                {
                    Console.WriteLine(t.teamName + "\t\t\t" + t.all.win + "\t\t" + t.all.draw + "\t\t" + t.all.lose + "\t\t" + "\t" + t.points);
                }
            }
        }

        public static void GetFixturesByRounds()
        {
            var allRounds = GetRounds();
            var round = allRounds[37];
            var rootFixtures = new Service().GetFixturesByRound(2, round); // 2018/2019 Premier League - last round
            var apiFixtures = rootFixtures.api;
            var fixtures = apiFixtures.fixtures;

            foreach (var f in fixtures)
            {
                Console.WriteLine(f.homeTeam.team_name + " - " + f.awayTeam.team_name + "\t\t" + f.score.fulltime);
            }
        }

        static void PrintRow(params string[] columns)
        {
            int width = (tableWidth - columns.Length) / columns.Length;
            string row = "|";

            foreach (string column in columns)
            {
                row += AlignCentre(column, width) + "|";
            }

            Console.WriteLine(row);
        }

        static string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }
    }
}
