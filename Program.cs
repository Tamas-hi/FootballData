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
        public static List<Tuple<string, string, int>> distanceList = new List<Tuple<string, string, int>>();

        public static void Main(string[] args)
        {
            PrintRow("Wikidata", "Football API");
            wiki.Teams = new List<KeyValuePair<int, Team>>(); // ranking - team
            wiki.Relegated = new List<string>();
            Console.Write("Enter Wikidata Entity ID: ");
            string WikiId = Console.ReadLine();
            // átadjuk a megadott ID-t
            MainAsync(WikiId).Wait();


            Console.Write("Enter Football API league ID: ");
            API.Teams = new List<KeyValuePair<int, Team>>();
            API.Relegated = new List<string>();
            string APILeagueId = Console.ReadLine();
            GetLeagueFromAPI(int.Parse(APILeagueId));


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
                                    IdValuePairs.Add(new KeyValuePair<string, string>(claim.MainSnak.PropertyId, claim.MainSnak.DataValue.ToString())); // property - csapat ID
                                    IdValuePairs.Add(new KeyValuePair<string, string>(claim.MainSnak.DataValue.ToString(), (string)json.value.amount)); // csapat ID - qualifier
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
                   // Console.WriteLine(rank);
                    Team team = new Team();
                    team.teamName = teamName;
                    wiki.Teams.Add(new KeyValuePair<int, Team>(rank, team));
                }

            }

            foreach (var team in wiki.Teams)
            {
                // Console.WriteLine(kvp.Key + " " + kvp.Value);
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
                int i = 1;
                foreach (var team in standing)
                {
                    // addedTeam.all.goalsFor = team.all.goalsFor;
                    //Console.WriteLine(team.points + " " + team.venue_name);
                    Team addedTeam = new Team();
                    addedTeam.teamName = team.teamName;
                    addedTeam.rank = team.rank;
                    API.Teams.Add(new KeyValuePair<int, Team>(team.rank, addedTeam));
                }
            }
            API.numberOfTeams = API.Teams.Count().ToString();
            API.numberOfMatches = GetFixtures(league_id).ToString();
            API.numberOfGoals = allGoals.ToString();
        }

        private static void MatchData(MatchData wiki, MatchData API)
        {
            LevenshteinDistance(wiki, API);
            var result = distanceList;//GroupBy(item => item.Item1)
                                      //.Select(g => g.OrderBy(t => t.Item3).First()).ToList();

            

            foreach (var x in result)
            {
                Console.WriteLine(x);
            }

        }



        /// <summary>
        /// Compute the distance between two strings.
        /// </summary>
        public static int LevenshteinDistance<T>(T self, T to, params string[] ignore) where T : class
        {
            List<string> selfPropertyValues = new List<string>();
            List<string> toPropertyValues = new List<string>();
            int n, m;
            if (self != null && to != null)
            {
                Type type = typeof(T);

                List<string> ignoreList = new List<string>(ignore);
                foreach (PropertyInfo pi in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    object selfValue = type.GetProperty(pi.Name).GetValue(self, null); // wiki property érték
                    object toValue = type.GetProperty(pi.Name).GetValue(to, null); // API property érték

                    object selfType = type.GetProperty(pi.Name).PropertyType; // a property típusa


                    if((Type)selfType == typeof(List<KeyValuePair<int, Team>>))
                    {
                        foreach(KeyValuePair<int, Team> kvp in (List<KeyValuePair<int,Team>>)selfValue)
                        {
                            foreach(KeyValuePair<int, Team> kvp1 in (List<KeyValuePair<int, Team>>)toValue)
                            {
                                if(kvp.Key == kvp1.Key)
                                {
                                    var tuple = Tuple.Create(kvp.Value.teamName, kvp1.Value.teamName, 0);
                                    distanceList.Add(tuple);
                                }
                            }
                        }
                    }


                    if ((Type)selfType == typeof(List<string>))
                    {
                        foreach (var team in (List<string>)selfValue)
                        {
                            selfPropertyValues.Add(team);
                        }

                        foreach (var team in (List<string>)toValue)
                        {
                            toPropertyValues.Add(team);
                        }
                    }

                    else
                    {
                        selfPropertyValues.Add(selfValue.ToString());
                        toPropertyValues.Add(toValue.ToString());
                    }
                }
            }

            /*for (int i = 0; i < selfPropertyValues.Count; i++)
            {
                for (int j = 0; j < toPropertyValues.Count; j++)
                {
                    n = selfPropertyValues[i].Length;
                    m = toPropertyValues[j].Length;

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
                    for (int a = 0; a <= n; d[a, 0] = a++)
                    {
                    }

                    for (int b = 0; b <= m; d[0, b] = b++)
                    {
                    }

                    // Step 3
                    for (int a = 1; a <= n; a++)
                    {
                        //Step 4
                        for (int b = 1; b <= m; b++)
                        {
                            // Step 5
                            int cost = ((toPropertyValues[j])[b - 1]) == ((selfPropertyValues[i])[a - 1]) ? 0 : 1;

                            // Step 6
                            d[a, b] = Math.Min(
                                Math.Min(d[a - 1, b] + 1, d[a, b - 1] + 1),
                                d[a - 1, b - 1] + cost);
                        }
                    }
                    // Step 7

                    var tuple = Tuple.Create(selfPropertyValues[i], toPropertyValues[j], d[n, m]);
                    distanceList.Add(tuple);
                    //return d[n, m];             
                }
            }*/
            return 100;
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
