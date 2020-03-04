using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
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
        static int tableWidth = 150;
        public static string Identity = null;
        public static void Main(string[] args)
        {
            // init 
            //var rootWikiLeague = new Service().GetIdentifiers();
            // var rootApiLeague = new Service().GetLeagues(2);
            PrintRow("Wikidata", "Football API");

            Console.Write("Enter Wikidata league name: ");
            string wikiLeagueName = Console.ReadLine();
            wikiLeagueName = wikiLeagueName.Replace(' ', '_');
            GetEntityData(wikiLeagueName, "wiki");


            Console.Write("Enter Football API league name: ");
            string APILeagueName = Console.ReadLine();
            string APISeasonNumber = new String(APILeagueName.Where(Char.IsDigit).ToArray()).Substring(0, 4);
            APILeagueName = new string(APILeagueName.Where(ch => !Char.IsDigit(ch)).ToArray()).Trim();
            GetEntityData(APILeagueName, "api", APISeasonNumber);



            /*var firstRow = "League name with season:" + "\t\t\t" + GetLeagueFromWiki(0) + "\t\t\t\t\t" + GetLeagueFromAPI(0);
            var secondRow = "Description:" + "\t\t\t\t\t" + GetLeagueFromWiki(1) + "\t\t\t" + "No description in FootballAPI";
            var thirdRow = "Sports season or league or competition:" + "\t\t" + GetLeagueFromWiki(2) + "\t\t\t\t\t\t" + GetLeagueFromAPI(2);
            var fourthRow = "Follows:" + "\t\t\t\t\t" + GetLeagueFromWiki(3) + "\t\t\t\t" + "Previous season can be found with a specific league ID (37)";
            var fifthRow = "Followed By:" + "\t\t\t\t\t" + GetLeagueFromWiki(4) + "\t\t\t\t" + "Next season can be found with a specific league ID (524)";
            var sixthRow = "Country: " + "\t\t\t\t\t" + GetLeagueFromWiki(5) + "\t\t\t\t\t" + GetLeagueFromAPI(5);
            var seventhRow = "Edition: " + "\t\t\t\t\t\t" + GetLeagueFromWiki(6) + "\t\t\t\t\t" + "No edition number in FootballAPI";
            var eighthRow = "Sport: " + "\t\t\t\t\t\t" + GetLeagueFromWiki(7) + "\t\t\t\t" + "No sport type in FootballAPI";
            var ninethRow = "Start time: " + "\t\t\t\t\t" + GetLeagueFromWiki(8) + "\t\t\t\t" + GetLeagueFromAPI(8);
            var tenthRow = "End time: " + "\t\t\t\t\t" + GetLeagueFromWiki(9) + "\t\t\t\t" + GetLeagueFromAPI(9);
            var eleventhRow = "Time period: " + "\t\t\t\t\t" + GetLeagueFromWiki(10) + "\t\t\t\t" + GetLeagueFromAPI(8).Year + "-" + GetLeagueFromAPI(9).Year;
            var twelvethRow = "Organizer:" + "\t\t\t\t\t" + GetLeagueFromWiki(2) + "\t\t\t\t\t" + GetLeagueFromAPI(2);
            var thirteenthRow = "Number of participants:" + "\t\t\t\t" + GetLeagueFromWiki(11) + "\t\t\t\t\t\t\t" + GetLeagueFromAPI(11);
            Console.WriteLine(firstRow);
            Console.WriteLine(secondRow);
            Console.WriteLine(thirdRow);
            Console.WriteLine(fourthRow);
            Console.WriteLine(fifthRow);
            Console.WriteLine(sixthRow);
            Console.WriteLine(seventhRow);
            Console.WriteLine(eighthRow);
            Console.WriteLine(ninethRow);
            Console.WriteLine(tenthRow);
            Console.WriteLine(eleventhRow);
            Console.WriteLine(twelvethRow);
            Console.WriteLine(thirteenthRow);*/
            //GetStandings();
            //GetFixturesByRounds();
            //GetRounds();
            //GetQPsFromWikidata();
        }

        private static string GetEntityIdBySearch(string wikiTeamName) // wiki
        {
            var rootEntity = new Service().GetEntityIdBySearch(wikiTeamName);
            var entity = rootEntity.Search;
            foreach (var property in entity)
            {
                return property.Title; // label ha a név kell
            }
            return null;
        }

        private static int GetLeagueIdBySearch(string wikiLeagueName) // Football API
        {
            var rootLeague = new Service().FindLeagueByIdAndSeason(wikiLeagueName);
            var apiLeague = rootLeague.api;
            var leagues = apiLeague.leagues;
            return leagues.First().league_id;
        }

        public static string GetAllProperties(object obj)
        {
            return string.Join(" ", obj.GetType()
                                        .GetProperties()
                                        .Select(prop => prop.GetValue(obj)));
        }

        private static string GetEntityData(string wikiTeamName, string value, string ApiSeasonNumber = null) // wiki || Football API
        {
            if (value == "wiki")
            {
                Identity = GetEntityIdBySearch(wikiTeamName);
                var rootEntity = new Service().GetEntityObject(Identity);
                // Console.WriteLine(rootEntity.Entities.ID.Labels.En.Value);
            }
            else if (value == "api")
            {
                var league_id = GetLeagueIdBySearch(wikiTeamName);
                var rootLeague = new Service().GetLeagues(league_id);
                var apiLeague = rootLeague.api;
                var leagues = apiLeague.leagues;
                //Console.WriteLine(leagues.First().league_id);
                bool firstOccurence = true;

                var rootEntity = new Service().GetLeaguesFromSeason(ApiSeasonNumber);
                foreach (var league in rootEntity.api.leagues)
                {
                    if (league.league_id == leagues.First().league_id)
                    {

                        PropertyInfo[] properties = typeof(League).GetProperties();

                        if (firstOccurence)
                        {
                            foreach (PropertyInfo property in properties)
                            {
                                Console.WriteLine(property.Name + "\t" + property.GetValue(league));
                            }
                            firstOccurence = false;
                        }
                    }
                }
            }


            /*var rootLeague = new Service().GetIdentifiers();
            var IdNamePairs = GetQPsFromWikidata();
            foreach (var entityList in rootLeague)
            {
                foreach (var entity in entityList)
                {
                    foreach (var data in entity)
                    {
                        foreach (var subData in data)
                        {
                            if (chooser == 0)
                                return subData.labels.en.value;
                            if (chooser == 1)
                                return subData.descriptions.en.value;
                            if (chooser == 2 || chooser == 3 || chooser == 4)
                            {
                                foreach (var claim in subData.claims.P3450)
                                {
                                    if (chooser == 2)
                                    {
                                        dynamic ID = claim.mainsnak.datavalue.value.id;
                                        var ProcessedData = IdNamePairs[ID];
                                        return ProcessedData;
                                    }
                                    if (chooser == 3)
                                    {
                                        foreach (var qualifier in claim.qualifiers.P155)
                                        {
                                            dynamic ID = qualifier.datavalue.value.id;
                                            var ProcessedData = IdNamePairs[ID];
                                            return ProcessedData;
                                        }
                                    }
                                    if (chooser == 4)
                                    {
                                        foreach (var qualifier in claim.qualifiers.P156)
                                        {
                                            dynamic ID2 = qualifier.datavalue.value.id;
                                            var ProcessedData2 = IdNamePairs[ID2];
                                            return ProcessedData2;
                                        }
                                    }
                                }
                            }
                            if (chooser == 5)
                            {
                                foreach (var claim in subData.claims.P17)
                                {
                                    dynamic ID = claim.mainsnak.datavalue.value.id;
                                    var ProcessedData = IdNamePairs[ID];
                                    return ProcessedData;
                                }
                            }
                            if (chooser == 6)
                            {
                                foreach (var claim in subData.claims.P393)
                                {
                                    dynamic ID = claim.mainsnak.datavalue.value;
                                    return ID;
                                }
                            }
                            if (chooser == 7)
                            {
                                foreach (var claim in subData.claims.P641)
                                {
                                    dynamic ID = claim.mainsnak.datavalue.value.id;
                                    var ProcessedData = IdNamePairs[ID];
                                    return ProcessedData;
                                }
                            }
                            if (chooser == 8)
                            {
                                foreach (var claim in subData.claims.P580)
                                {
                                    dynamic ID = claim.mainsnak.datavalue.value.time;
                                    return ID;
                                }
                            }

                            if (chooser == 9)
                            {
                                foreach (var claim in subData.claims.P582)
                                {
                                    dynamic ID = claim.mainsnak.datavalue.value.time;
                                    return ID;
                                }
                            }
                            if (chooser == 10)
                            {
                                foreach (var claim in subData.claims.P2348)
                                {
                                    dynamic ID = claim.mainsnak.datavalue.value.id;
                                    var ProcessedData = IdNamePairs[ID];
                                    return ProcessedData;
                                }
                            }
                            if (chooser == 11)
                            {
                                foreach (var claim in subData.claims.P1132)
                                {
                                    dynamic ID = claim.mainsnak.datavalue.value.amount;
                                    return ID;
                                }
                            }

                        }
                    }
                }
            }*/
            return null;
        }

        private static dynamic GetLeagueFromAPI(int chooser)
        {
            var rootLeague = new Service().GetLeagues(2);
            var apiLeague = rootLeague.api;
            var leagues = apiLeague.leagues;
            if (chooser == 0)
                return leagues.First().season + " " + leagues.First().name;
            if (chooser == 2)
                return leagues.First().name;
            if (chooser == 5)
            {
                return leagues.First().country;
            }
            if (chooser == 8)
            {
                DateTime parsedDate = DateTime.Parse(leagues.First().season_start);
                return parsedDate;
            }
            if (chooser == 9)
            {
                DateTime parsedDate = DateTime.Parse(leagues.First().season_end);
                return parsedDate;
            }

            if (chooser == 11)
            {
                var rootTeam = new Service().GetTeams(2);
                var apiTeam = rootTeam.api;
                var teams = apiTeam.teams;
                return teams.Count();
            }
            return null;
        }

        public static Dictionary<dynamic, dynamic> GetQPsFromWikidata()
        {
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
                    if (IdNamePairs.Count < 50)
                        IdNamePairs.Add(match.Value, null);
                }
            }

            var x = string.Format("{0}", string.Join("|", IdNamePairs.Keys));
            //Console.WriteLine(x);

            var entityNames = new Service().GetEntityNames(x);

            IdNamePairs.Clear();
            foreach (var data in entityNames)
            {
                foreach (var y in data)
                {
                    if (y is JObject)
                        foreach (var z in y)
                        {
                            foreach (var w in z)
                            {
                                IdNamePairs[w.id] = w.labels.en.value;
                            }
                        }
                }
            }

            return IdNamePairs;
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
            string row = "                                    |";

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
