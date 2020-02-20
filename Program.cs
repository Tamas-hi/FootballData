using System;
using System.Collections.Generic;

namespace FootballData
{
    public class Program
    {
        static int tableWidth = 100;
        public static void Main(string[] args)
        {   
            PrintRow("Team Name", "Wins", "Draws", "Losses", "Points");

            //GetStandings();
            GetFixturesByRounds();
            //GetRounds();
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
            var rootFixtures = new Service().GetFixtures(2, round); // 2018/2019 Premier League - first round
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
