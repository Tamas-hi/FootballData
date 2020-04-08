using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
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
        public static MatchData wiki = new MatchData();
        public static MatchData API = new MatchData();
        public static int allGoals = 0;
        public static bool flag = false;

        public static void Main(string[] args)
        {
            PrintRow("Wikidata", "Football API");
            wiki.Teams = new Dictionary<int, string>();
            wiki.Relegated = new List<string>();
            Console.Write("Enter Wikidata Entity ID: ");
            string WikiId = Console.ReadLine();
            // átadjuk a megadott ID-t
            MainAsync(WikiId).Wait();


            /* Console.Write("Enter Football API league ID: ");
             API.Teams = new List<string>();
             API.Relegated = new List<string>();
             string APILeagueId = Console.ReadLine();
             GetLeagueFromAPI(int.Parse(APILeagueId));*/


            MatchData(wiki, API);
        }

        private static async Task MainAsync(string id) // Wiki
        {
            var client = new WikiClient
            {
                ClientUserAgent = "Hargitomi"
            };

            // belépünk az oldalra
            var site = new WikiSite(client, "https://www.wikidata.org/w/api.php");
            await site.Initialization;
            try
            {
                await site.LoginAsync("Hargitomi", "jelszo123");
            }
            catch (WikiClientException ex)
            {
                Console.WriteLine(ex.Message);
            }

            var entity = new Entity(site, id);
            await entity.RefreshAsync(EntityQueryOptions.FetchClaims);

            // Claimek közül az egyes (P)ropertykhez tartozó DataValue.value érték
            var IdValuePairs = new List<KeyValuePair<string, string>>();
            HashSet<string> keys = new HashSet<string>();

            foreach (var claim in entity.Claims)
            {
                flag = false;
                try
                {
                    if (claim.MainSnak.PropertyId.Equals("P1923"))
                    {
                        foreach (var qualifierSnak in claim.Qualifiers)
                        {
                            dynamic json = JsonConvert.DeserializeObject(qualifierSnak.RawDataValue.ToString());
                            if (json.value.amount != null)
                            {
                                if (flag == false)
                                {
                                    IdValuePairs.Add(new KeyValuePair<string, string>(claim.MainSnak.PropertyId, claim.MainSnak.DataValue.ToString()));
                                    IdValuePairs.Add(new KeyValuePair<string, string>(claim.MainSnak.DataValue.ToString(), (string)json.value.amount));
                                    keys.Add(claim.MainSnak.DataValue.ToString());
                                    flag = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        IdValuePairs.Add(new KeyValuePair<string, string>(claim.MainSnak.PropertyId, claim.MainSnak.DataValue.ToString()));
                        keys.Add(claim.MainSnak.PropertyId);
                        if (claim.MainSnak.DataValue.ToString().StartsWith('Q') || claim.MainSnak.DataValue.ToString().StartsWith('P'))
                            keys.Add(claim.MainSnak.DataValue.ToString());
                    }
                }
                catch (Exception e) { }
            }

            foreach (var claim in entity.Claims)
            {
                try
                {
                    dynamic json = JsonConvert.DeserializeObject(claim.MainSnak.RawDataValue.ToString());
                    if (json.value.amount != null)
                    {
                        IdValuePairs.Add(new KeyValuePair<string, string>(claim.MainSnak.PropertyId, (string)json.value.amount));
                        keys.Add(claim.MainSnak.PropertyId);
                    }
                }
                catch (Exception e)
                {

                }
            }
            // Összes P és Q | jellel elválasztva       
            string allKeys = "";
            foreach (var element in keys)
            {
                var addedElement = element + "|";
                allKeys += addedElement;
            }
            allKeys = allKeys.Remove(allKeys.Length - 1); // az utolsó | jelet eltávolítjuk
            // Összes P és Q, illetve a hozzájuk tartozó label
            Dictionary<string, string> RealIdNamePairs = new Dictionary<string, string>();


            var datax = new Service().GetEntityNames(allKeys);
            try
            {
                foreach (var x in datax)
                {
                    foreach (var y in x)
                    {
                        foreach (var z in y)
                        {
                            foreach (var w in z)
                            {
                                try
                                {
                                    RealIdNamePairs.Add(w.id.ToString().Trim(), w.labels.en.value.ToString().Trim());
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

            /*foreach(KeyValuePair<string, string> kvp in IdValuePairs) {
                 Console.WriteLine(kvp.Key + " " + kvp.Value);
            }*/

            var temp = IdValuePairs.ToLookup(kvp => kvp.Key, kvp => kvp.Value);


            foreach (string key in temp["P3450"])
            {
                wiki.LeagueName = RealIdNamePairs[key];
            }
            // Console.WriteLine(wiki.LeagueName);

            foreach (string key in temp["P17"])
            {
                wiki.Country = RealIdNamePairs[key];
            }
            // Console.WriteLine(wiki.Country);

            foreach (string key in temp["P580"])
            {
                wiki.SeasonStart = key;
            }
            //  Console.WriteLine(wiki.SeasonStart);

            foreach (string key in temp["P582"])
            {
                wiki.SeasonEnd = key;
            }
            //  Console.WriteLine(wiki.SeasonEnd);

            foreach (string key in temp["P2348"])
            {
                wiki.LeagueSeason = RealIdNamePairs[key];
            }
            //  Console.WriteLine(wiki.LeagueSeason);

            foreach (string key in temp["P1132"])
            {
                wiki.numberOfTeams = key;
            }
            //  Console.WriteLine(wiki.numberOfTeams);

            foreach (string key in temp["P1923"])
            {
                foreach (string subKey in temp[key])
                {
                        // teamName
                        string teamName = RealIdNamePairs[key];
                        // ranking
                        string ranking = subKey;
                        ranking.Remove(0, 1);
                        int rank = int.Parse(ranking);
                        wiki.Teams.Add(rank, teamName);
                }

            }

            foreach(KeyValuePair<int, string> kvp in wiki.Teams)
            {
                Console.WriteLine(kvp.Key + " " + kvp.Value);
            }
            foreach (var team in wiki.Teams)
            {
                //     Console.WriteLine(team);
            }

            foreach (string key in temp["P1346"])
            {
                wiki.Winner = RealIdNamePairs[key];
            }
            //Console.WriteLine(wiki.Winner);

            foreach (string key in temp["P2882"])
            {
                wiki.Relegated.Add(RealIdNamePairs[key]);
            }
            foreach (var team in wiki.Relegated)
            {
                // Console.WriteLine(team);
            }

            foreach (string key in temp["P1350"])
            {
                wiki.numberOfMatches = key;
            }
            // Console.WriteLine(wiki.numberOfMatches);

            foreach (string key in temp["P1351"])
            {
                wiki.numberOfGoals = key;
            }
            // Console.WriteLine(wiki.numberOfGoals);
        }



        private static void GetLeagueFromAPI(int league_id) // Football API
        {
            var rootLeague = new Service().GetLeagues(league_id);
            var apiLeague = rootLeague.api;
            var leagues = apiLeague.leagues;
            API.LeagueName = leagues.First().name;
            API.LeagueSeason = leagues.First().season.ToString();
            API.Country = leagues.First().country;
            API.SeasonStart = leagues.First().season_start;
            API.SeasonEnd = leagues.First().season_end;

            var rootStanding = new Service().GetStandings(league_id);
            var apiStanding = rootStanding.api;
            var standings = apiStanding.standings;
            foreach (var standing in standings)
            {
                API.Winner = standing.First().teamName;
                var lastTeam = standing[standing.Count - 1];
                var lastButOneTeam = standing[standing.Count - 2];
                var lastButOneButOneTeam = standing[standing.Count - 3];
                API.Relegated.Add(lastTeam.teamName);
                API.Relegated.Add(lastButOneTeam.teamName);
                API.Relegated.Add(lastButOneButOneTeam.teamName);

                foreach (var team in standing)
                {
                   // API.Teams.Add(team.teamName);
                }
            }
            API.numberOfTeams = API.Teams.Count().ToString();
            API.numberOfMatches = GetFixtures(league_id).ToString();
            API.numberOfGoals = allGoals.ToString();
        }

        private static void MatchData(MatchData wiki, MatchData API)
        {
            LevenshteinDistance(wiki, API);
        }

        /// <summary>
        /// Compute the distance between two strings.
        /// </summary>
        public static int LevenshteinDistance<T>(T self, T to, params string[] ignore) where T : class
        {
            if (self != null && to != null)
            {
                Type type = typeof(T);
                List<string> ignoreList = new List<string>(ignore);
                foreach (PropertyInfo pi in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    if (!ignoreList.Contains(pi.Name))
                    {
                        object selfValue = type.GetProperty(pi.Name).GetValue(self, null); // wiki property érték
                        object toValue = type.GetProperty(pi.Name).GetValue(to, null); // API property érték


                        object selfType = type.GetProperty(pi.Name).PropertyType; // a property típusa

                        int n;
                        int m;
                        // listák elemeinek összehasonlítása
                        if ((Type)selfType == typeof(List<string>))
                        {
                            List<string> selfTeams = (List<string>)selfValue;
                            List<string> toTeams = (List<string>)toValue;

                            for (int a = 0; a < selfTeams.Count; a++)
                            {
                                n = selfTeams[a].Length;
                                selfTeams.Sort();
                                m = toTeams[a].Length;
                                toTeams.Sort();
                                int[,] d = new int[n + 1, m + 1];

                                // Step 1
                                if (n == 0)
                                {
                                    return m;
                                }

                                if (m == 0)
                                {
                                    return n;
                                }

                                // Step 2
                                for (int i = 0; i <= n; d[i, 0] = i++)
                                {
                                }

                                for (int j = 0; j <= m; d[0, j] = j++)
                                {
                                }

                                // Step 3
                                for (int i = 1; i <= n; i++)
                                {
                                    //Step 4
                                    for (int j = 1; j <= m; j++)
                                    {
                                        // Step 5
                                        int cost = (toValue.ToString().Trim()[j - 1] == selfValue.ToString().Trim()[i - 1]) ? 0 : 1;

                                        // Step 6
                                        d[i, j] = Math.Min(
                                            Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                                            d[i - 1, j - 1] + cost);
                                    }
                                }
                                // Step 7
                                Console.WriteLine(selfTeams[a] + " " + toTeams[a] + " " + (d[n, m]));
                                //return d[n, m];                      
                            }
                        }

                        // nem lista elemek összehasonlítása
                        else
                        {
                            n = selfValue.ToString().Length;
                            m = toValue.ToString().Length;
                            int[,] d = new int[n + 1, m + 1];

                            // Step 1
                            if (n == 0)
                            {
                                return m;
                            }

                            if (m == 0)
                            {
                                return n;
                            }

                            // Step 2
                            for (int i = 0; i <= n; d[i, 0] = i++)
                            {
                            }

                            for (int j = 0; j <= m; d[0, j] = j++)
                            {
                            }

                            // Step 3
                            for (int i = 1; i <= n; i++)
                            {
                                //Step 4
                                for (int j = 1; j <= m; j++)
                                {
                                    // Step 5
                                    int cost = (toValue.ToString().Trim()[j - 1] == selfValue.ToString().Trim()[i - 1]) ? 0 : 1;

                                    // Step 6
                                    d[i, j] = Math.Min(
                                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                                        d[i - 1, j - 1] + cost);
                                }
                            }
                            // Step 7
                            Console.WriteLine(selfValue + " " + toValue + " " + (d[n, m]));
                            //return d[n, m];
                        }
                    }
                    else
                    {
                        return 100;
                    }
                }
                return 100;
            }
            else
            {
                return 100;
            }
        }




        private static string GetEntityData(int league_id)
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

        public static int GetFixtures(int league_id)
        {
            var rootFixtures = new Service().GetFixtures(league_id); // 2018/2019 Premier League - last round
            var apiFixtures = rootFixtures.api;
            var fixtures = apiFixtures.fixtures;

            foreach (var fixture in fixtures)
            {
                var oneMatchGoals = fixture.goalsHomeTeam + fixture.goalsAwayTeam;
                allGoals += oneMatchGoals;

            }

            return fixtures.Count();
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
