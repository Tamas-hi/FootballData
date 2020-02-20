using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FootballData
{
    public class Service
    {
        private readonly Uri serverUrl = new Uri("https://api-football-v1.p.rapidapi.com");

        /// <summary>
        /// A method which calls GET and deserializes result from JSON. Handles exceptions
        /// </summary>
        /// <typeparam name="T">Type of the returned result.</typeparam>
        /// <param name="uri">The uri to fetch from. </param>
        /// <returns>The needed data or null</returns>
        private T GetData<T>(Uri uri) where T : class
        {
            var client = new RestClient(uri);
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", "api-football-v1.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "acd21cc6femsh5d6f180530ebd38p1b1101jsnf5eb2e9a6be5");
            IRestResponse response = client.Execute(request);

            if (response.IsSuccessful)
            {
                var json = response.Content;
                T r = JsonConvert.DeserializeObject<T>(json);
                return r;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Get Teams from Football API
        /// </summary>
        /// <returns></returns>
        public RootTeamObject GetTeams(int league_id)
        {
            return GetData<RootTeamObject>(new Uri(serverUrl, $"/v2/teams/league/{league_id}/"));
        }

        public RootStandingObject GetStandings(int league_id)
        {
            return GetData<RootStandingObject>(new Uri(serverUrl, $"/v2/leagueTable/{league_id}/"));
        }

        public RootFixtureObject GetFixtures(int league_id, string round)
        {
            return GetData<RootFixtureObject>(new Uri(serverUrl, $"/v2/fixtures/league/{league_id}/{round}"));
        }

        public RootRoundObject GetRounds(int league_id)
        {
            return GetData<RootRoundObject>(new Uri(serverUrl, $"/v2/fixtures/rounds/{league_id}/"));
        }
    }
}
