using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using WikiClientLibrary;
using WikiClientLibrary.Client;
using WikiClientLibrary.Sites;
using WikiClientLibrary.Wikibase;

namespace FootballData
{
    public class Program
    {
        static int tableWidth = 150;
        public static string Identity = null;
        MatchData wiki = new MatchData();
        MatchData API = new MatchData();


        public static void Main(string[] args)
        {
            PrintRow("Wikidata", "Football API");

            Console.Write("Enter Wikidata Entity ID: ");
            string WikiId = Console.ReadLine();
            MainAsync(WikiId).Wait();


            //Console.Write("Enter Football API league ID: ");
            // string APILeagueId = Console.ReadLine();
            //GetEntityData(int.Parse(APILeagueId));
        }

        private static async Task MainAsync(string id) // Wiki
        {
            MatchData wiki = new MatchData();
            var client = new WikiClient
            {
                ClientUserAgent = "Hargitomi"
            };

            var site = new WikiSite(client, "https://www.wikidata.org/w/api.php");
            await site.Initialization;
            try
            {
                await site.LoginAsync("Hargitomi", "Toloszekes4489");
            }
            catch (WikiClientException ex)
            {
                Console.WriteLine(ex.Message);
            }


            var entity = new Entity(site, id);
            await entity.RefreshAsync(EntityQueryOptions.FetchClaims);
            Dictionary<string, string> IdNamePairs = new Dictionary<string, string>();
            var data = new Service().GetEntityNames();

            string keys = "";

            foreach (dynamic claim in entity.Claims)
            {
                try
                {
                    //IdNamePairs.TryAdd(claim.MainSnak.PropertyId, claim.MainSnak.RawDataValue.value.amount);

                    IdNamePairs.TryAdd(claim.ToString().Substring(0, claim.ToString().IndexOf("=")).Trim(), claim.MainSnak.DataValue.ToString());
                }
                catch (Exception e) { }
                try
                {
                    if (!(claim.ToString().Contains("-")))
                        keys += claim.ToString() + "|";
                }
                catch (Exception e)
                {

                }
            }


            StringBuilder sb = new StringBuilder();
            foreach (char c in keys)
            {
                if (!(c == '=') && !(c == ' '))
                {
                    sb.Append(c);
                }
            }
            keys = sb.ToString();
            keys = keys.Replace("Q", "|Q");

            var allKeys = keys.Split('|');

            var noDuplicates = new HashSet<string>(allKeys);

            var noDuplicatesKeys = string.Join("|", noDuplicates);

            noDuplicatesKeys = noDuplicatesKeys.Remove(noDuplicatesKeys.Length - 1);

            Dictionary<string, string> asdIdNamePairs = new Dictionary<string, string>();
            dynamic datax = new Service().GetEntityNames(noDuplicatesKeys);
            try
            {
                foreach (var x in datax)
                {
                    foreach (var y in x)
                    {
                        if (y is JObject)
                            foreach (var z in y)
                            {
                                foreach (var w in z)
                                {
                                    try
                                    {
                                        asdIdNamePairs.Add(w.id.ToString().Trim(), w.labels.en.value.ToString().Trim());
                                    }
                                    catch (Exception e)
                                    {

                                    }
                                }
                            }
                    }
                }
            }
            catch (Exception e) { }

            /* foreach(KeyValuePair<string, string> kvp in IdNamePairs)
             {
                 Console.WriteLine(kvp.Key);
             }*/

            var temp = IdNamePairs["P3450"];
            wiki.LeagueName = asdIdNamePairs[temp.ToString()];
            Console.WriteLine(wiki.LeagueName);
            temp = IdNamePairs["P2348"];
            wiki.LeagueSeason = asdIdNamePairs[temp.ToString()];
            Console.WriteLine(wiki.LeagueSeason);
            temp = IdNamePairs["P17"];
            wiki.Country = asdIdNamePairs[temp.ToString()];
            Console.WriteLine(wiki.Country);
            temp = IdNamePairs["P1346"];
            wiki.Winner = asdIdNamePairs[temp.ToString()];
            Console.WriteLine(wiki.Winner);
            temp = IdNamePairs["P2882"];
            wiki.Relegated = asdIdNamePairs[temp.ToString()];
            Console.WriteLine(wiki.Relegated);

        }


        private static string GetEntityData(int league_id) //Football API
        {
            var rootLeague = new Service().GetLeagues(league_id); // a liga
            var apiLeague = rootLeague.api;
            var leagues = apiLeague.leagues.First();

            PropertyInfo[] properties = typeof(League).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                Console.WriteLine(property.Name + "\t" + property.GetValue(leagues)); // csak a kiválasztott liga
            }
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

        public static Dictionary<string, string> GetQPsFromWikidata(string id)
        {
            Dictionary<string, string> IdNamePairs = new Dictionary<string, string>();

            dynamic result = new Service().GetIdentifiers(id);

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

        public static string GetAllProperties(object obj)
        {
            return string.Join(" ", obj.GetType()
                                        .GetProperties()
                                        .Select(prop => prop.GetValue(obj)));
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

        public static List<string> GetRounds()
        {
            var rootRounds = new Service().GetRounds(2);
            var apiRounds = rootRounds.api;
            var round = apiRounds.fixtures;

            return round;
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
    }
}
