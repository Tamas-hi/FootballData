using System;
using System.Collections.Generic;

namespace FootballData
{
    public class Program
    {
        static int tableWidth = 100;
        public static void Main(string[] args)
        {
            var rootTeams = new Service().GetTeams(524); // 2019/2020 Premier League
            var apiTeams = rootTeams.api;
            var teams = apiTeams.teams;
            /*foreach (var t in teams)
            {
                Console.WriteLine(t.name + t.country);
            }*/
            
            PrintRow("Team Name", "Wins", "Draws", "Losses", "Points");
            var rootStandings = new Service().GetStandings(524); // 2018/2019 Premier League
            var apiStandings = rootStandings.api;
            var standings = apiStandings.standings;
            foreach(var s in standings)
            {
               foreach(var t in s)
                {
                    Console.WriteLine(t.teamName + "\t\t\t" + t.all.win + "\t\t" + t.all.draw + "\t\t" + t.all.lose + "\t\t" + "\t" + t.points);
                }
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
