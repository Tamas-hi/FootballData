using DiffMatchPatch;
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
        public static int tableWidth = 150; // megjelenítéshez 
        public static MatchData wiki = new MatchData(); // wikidata példány
        public static MatchData API = new MatchData(); //  Football API példány
        public static int allGoals = 0; // összes gólok száma a ligában
        public static bool flag = false; 
        public static string Identity = null;
        public static List<Tuple<string, string, int>> distanceList = new List<Tuple<string, string, int>>();
        public static List<Tuple<Team, Team, int>> distanceListTeams = new List<Tuple<Team, Team, int>>();
        public static Dictionary<string, string> RealIdNamePairs = new Dictionary<string, string>();
        public static Dictionary<string, string> TeamAndVenue = new Dictionary<string, string>();

        public static void Main(string[] args)
        {
            PrintRow("Wikidata", "Football API");
            wiki.Teams = new List<Team>();
            wiki.Relegated = new List<Team>();
            Console.Write("Enter Wikidata Entity ID: ");
            string WikiId = Console.ReadLine();
            // átadjuk a megadott ID-t
            MainAsync(WikiId).Wait();


            Console.Write("Enter Football API league ID: ");
            API.Teams = new List<Team>();
            API.Relegated = new List<Team>();
            string APILeagueId = Console.ReadLine();
            GetLeagueFromAPI(int.Parse(APILeagueId));


            MatchData(wiki, API);
        }

        private static async Task MainAsync(string id) // Wiki belépés
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

            var entity = new Entity(site, id); // a lekért entitás
            await entity.RefreshAsync(EntityQueryOptions.FetchClaims);

            // Claimek közül az egyes (P)ropertykhez tartozó DataValue.value érték
            var IdValuePairs = new List<KeyValuePair<string, string>>();
            HashSet<string> keys = new HashSet<string>();
            List<string> teamIdsList = new List<string>();

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
                                    teamIdsList.Add(claim.MainSnak.DataValue.ToString()); // kell P571 (alapítás), P115 (home_venue), 
                                    IdValuePairs.Add(new KeyValuePair<string, string>(claim.MainSnak.DataValue.ToString(), (string)json.value.amount)); // csapat ID - ranking
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
                        IdValuePairs.Add(new KeyValuePair<string, string>(claim.MainSnak.PropertyId, (string)json.value.amount)); //property - amount érték (ahol csak szám van, pl numberOfGoals)
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


            getNames(allKeys);

            Dictionary<string, int> TeamAndFounded = new Dictionary<string, int>();

            foreach (var element in teamIdsList)
            {
                var teamDetails = new Service().GetEntityClaims(element, "P571");

                if(teamDetails == null)
                {
                    continue;
                }

                try
                {
                    foreach (var x in teamDetails)
                    {
                        foreach (var y in x)
                        {
                            foreach (var z in y)
                            {
                                foreach (var w in z)
                                {
                                    foreach (var a in w)
                                    {
                                        string founded = a.mainsnak.datavalue.value.time;
                                        founded = founded.Remove(0, 1);
                                        founded = founded.Substring(0, 4);
                                        TeamAndFounded.Add(element, int.Parse(founded));
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception e) { }
            }

            string idList = "";
            foreach (var element in teamIdsList)
            {
                var teamDetails = new Service().GetEntityClaims(element, "P115");

                try
                {
                    foreach (var x in teamDetails)
                    {
                        foreach (var y in x)
                        {
                            foreach (var z in y)
                            {
                                foreach (var w in z)
                                {
                                    foreach (var a in w)
                                    {
                                        string ID = a.mainsnak.datavalue.value.id;
                                        idList += ID + "|";
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception e) { }
            }

            idList = idList.Remove(idList.Length - 1);
            var datax = new Service().GetEntityNames(idList);

            int i = 0;
            try
            {
                foreach (var b in datax)
                {
                    foreach (var c in b)
                    {
                        foreach (var d in c)
                        {
                            foreach (var e in d)
                            {
                                string venue_name = e.labels.en.value.ToString();
                                TeamAndVenue.Add(teamIdsList[i++], venue_name);
                            }
                        }
                    }
                }
            }
            catch (Exception e) { }



            // Console.WriteLine(teamDetails);

            /*foreach(KeyValuePair<string, string> kvp in TeamAndVenue) {
                 Console.WriteLine(kvp.Key + " " + kvp.Value);
            }*/

            /*foreach(var element in teamIds)
            {
                Console.WriteLine(element);
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
                    //string ranking = subKey;
                    //ranking.Remove(0, 1);
                    //int rank = int.Parse(ranking);
                    // Console.WriteLine(rank);
                    Team team = new Team();
                    team.team_id = key;
                    team.teamName = teamName;
                    team.founded = TeamAndFounded[key];
                    team.venue_name = TeamAndVenue[key];
                    wiki.Teams.Add(team);
                }

            }

            foreach (var team in wiki.Teams)
            {
                //Console.WriteLine(team.teamName + " " + team.founded + " " + team.venue_name);
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
                string teamName = RealIdNamePairs[key];
                Team team = new Team();
                team.team_id = key;
                team.teamName = teamName;
                team.founded = TeamAndFounded[key];
                team.venue_name = TeamAndVenue[key];
                wiki.Relegated.Add(team);

            }
            foreach (var team in wiki.Relegated)
            {
                // Console.WriteLine(team.teamName + " " + team.founded + " " + team.venue_name);
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


        private static void getNames(string allKeys)
        {
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


            var rootTeams = new Service().GetTeams(league_id);
            var apiTeams = rootTeams.api;
            var allTeams = apiTeams.teams; // ebbe benne vannak az adatok

            foreach (var team in allTeams)
            {
                if(team.name == "Everton")
                {
                    team.venue_name = "Goodison Park";
                }
                API.Teams.Add(team);
            }

            API.Winner = API.Teams.Last().name;
            var lastTeam = API.Teams[API.Teams.Count - 1];
            lastTeam.name = API.Teams[API.Teams.Count - 1].name;
            var lastButOneTeam = API.Teams[API.Teams.Count - 2];
            lastButOneTeam.name = API.Teams[API.Teams.Count - 2].name;
            var lastButOneButOneTeam = API.Teams[API.Teams.Count - 3];
            lastButOneButOneTeam.name = API.Teams[API.Teams.Count - 3].name;

            API.Relegated.Add(lastTeam);
            API.Relegated.Add(lastButOneTeam);
            API.Relegated.Add(lastButOneButOneTeam);

            API.numberOfTeams = API.Teams.Count().ToString();
            API.numberOfMatches = GetFixtures(league_id).ToString();
            API.numberOfGoals = allGoals.ToString();
        }

        private static void MatchData(MatchData wiki, MatchData API)
        {
            LevenshteinDistance(wiki, API);

            var result = distanceList.GroupBy(item => item.Item1)
                                      .Select(g => g.OrderBy(t => t.Item3).First()).ToList();

            foreach (var x in result)
            {
                Console.WriteLine(x.Item1 + " " + x.Item2 + " " + x.Item3);
            }

            var resultTeams = distanceListTeams.GroupBy(item => item.Item1)
                                     .Select(g => g.OrderBy(t => t.Item3).First()).ToList();

            foreach (var x in resultTeams)
            {
                //Console.WriteLine(x.Item1.teamName + " " +  x.Item2.name + " " + x.Item3);
                Console.WriteLine(x.Item1.team_id + " " + x.Item2.team_id + " " + x.Item3);
            }

        }
        /// <summary>
        /// Compute the distance between two strings.
        /// </summary>
        public static int LevenshteinDistance<T>(T self, T to) where T : class
        {
            List<string> selfPropertyValues = new List<string>();
            List<string> toPropertyValues = new List<string>();

            List<Team> selfTeamPropertyValues = new List<Team>();
            List<Team> toTeamPropertyValues = new List<Team>();
            int n, m;
            if (self != null && to != null)
            {
                Type type = typeof(T);

                foreach (PropertyInfo pi in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    object selfValue = type.GetProperty(pi.Name).GetValue(self, null); // wiki property érték
                    object toValue = type.GetProperty(pi.Name).GetValue(to, null); // API property érték

                    object selfType = type.GetProperty(pi.Name).PropertyType; // a property típusa


                    if ((Type)selfType == typeof(List<Team>))
                    {
                        foreach (var team in (List<Team>)selfValue)
                        {

                            selfTeamPropertyValues.Add(team);
                        }

                        foreach (var team in (List<Team>)toValue)
                        {
                            toTeamPropertyValues.Add(team);
                        }
                    }

                    else
                    {
                        selfPropertyValues.Add(selfValue.ToString());
                        toPropertyValues.Add(toValue.ToString());
                    }
                }

            }


            for (int i = 0; i < selfPropertyValues.Count; i++)
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
            }

            n = 0;
            m = 0;
            int o = 0;
            int p = 0;
            int q = 0;
            int r = 0;
            for (int i = 0; i < selfTeamPropertyValues.Count; i++)
            {
                for (int j = 0; j < toTeamPropertyValues.Count; j++)
                {
                    n = selfTeamPropertyValues[i].teamName.Length; // csapat nevének hossza wikidatáról
                    m = toTeamPropertyValues[j].name.Length; // csapat nevének hossza Football API-ból

                    int[,] d = new int[n + 1, m + 1]; // csapatnév+1 * csapatnév+1 méretű tömböt hoz létre

                    // Step 1
                    if (n == 0) // 0-e a csapatnév hossza wikidatán
                    {
                        return m;
                    }

                    if (m == 0) //0 - e a csapatnév hossza Football API-ból
                    {
                        return n;
                    }

                    // Step 2
                    for (int a = 0; a <= n; d[a, 0] = a++) // a 0. oszlopot felölti számokkal 0-tól n-ig
                    {
                    }

                    for (int b = 0; b <= m; d[0, b] = b++) // a 0. sort feltölti számokkal 0-tól m-ig
                    {
                    }

                    // Step 3
                    for (int a = 1; a <= n; a++)
                    {
                        //Step 4
                        for (int b = 1; b <= m; b++)
                        {
                            // Step 5
                            int cost = ((toTeamPropertyValues[j].name)[b - 1]) == ((selfTeamPropertyValues[i].teamName)[a - 1]) ? 0 : 1; // itt csak azt vizsgálja, hogy egyenlő-e a két vizsgált karakter

                            // Step 6
                            d[a, b] = Math.Min(
                                Math.Min(d[a - 1, b] + 1, d[a, b - 1] + 1),
                                d[a - 1, b - 1] + cost);
                        }
                    }

                    o = selfTeamPropertyValues[i].founded.ToString().Length;
                    p = toTeamPropertyValues[j].founded.ToString().Length;

                    int[,] e = new int[o + 1, p + 1];

                    // Step 1
                    if (o == 0)
                    {
                        return o;
                    }

                    if (p == 0)
                    {
                        return p;
                    }

                    // Step 2
                    for (int a = 0; a <= o; e[a, 0] = a++)
                    {
                    }

                    for (int b = 0; b <= p; e[0, b] = b++)
                    {
                    }

                    // Step 3
                    for (int a = 1; a <= o; a++)
                    {
                        //Step 4
                        for (int b = 1; b <= p; b++)
                        {
                            // Step 5
                            int cost = ((toTeamPropertyValues[j].founded.ToString())[b - 1]) == ((selfTeamPropertyValues[i].founded.ToString())[a - 1]) ? 0 : 1;

                            // Step 6
                            e[a, b] = Math.Min(
                                Math.Min(e[a - 1, b] + 1, e[a, b - 1] + 1),
                                e[a - 1, b - 1] + cost);
                        }
                    }




                    q = selfTeamPropertyValues[i].venue_name.Length;
                    r = toTeamPropertyValues[j].venue_name.Length;

                    int[,] f = new int[q + 1, r + 1];

                    // Step 1
                    if (q == 0) {
                        return q;
                    }

                    if (r == 0) {
                        return r;
                    }

                    // Step 2
                    for (int a = 0; a <= q; f[a, 0] = a++) { }

                    for (int b = 0; b <= r; f[0, b] = b++) { }

                    // Step 3
                    for (int a = 1; a <= q; a++) {
                        //Step 4
                        for (int b = 1; b <= r; b++) {
                            // Step 5
                            int cost = ((toTeamPropertyValues[j].venue_name.ToString())[b - 1]) == ((selfTeamPropertyValues[i].venue_name.ToString())[a - 1]) ? 0 : 1;

                            // Step 6
                            f[a, b] = Math.Min(
                                Math.Min(f[a - 1, b] + 1, f[a, b - 1] + 1),
                                f[a - 1, b - 1] + cost);
                        }
                    }
                    int totalCost = d[n, m] + e[o, p] + f[q, r];

                    // Step 7

                    var tuple = Tuple.Create(selfTeamPropertyValues[i], toTeamPropertyValues[j], totalCost);
                    distanceListTeams.Add(tuple);             

                }
            }
            return 100;
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

    }
}