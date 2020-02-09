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
        private readonly Uri serverUrl = new Uri("https://api-football-v1.p.rapidapi.com/v2/teams/league/524");

        /// <summary>
        /// A method which calls GET asynchronously and deserializes result from JSON. Handles exceptions e.g., no word found.
        /// </summary>
        /// <typeparam name="T">Type of the returned result.</typeparam>
        /// <param name="uri">The uri to fetch from. </param>
        /// <returns>The needed data or null</returns>
        public List<Team> GetAsync()
        {
            var client = new RestClient(serverUrl);
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", "api-football-v1.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "acd21cc6femsh5d6f180530ebd38p1b1101jsnf5eb2e9a6be5");
            IRestResponse response = client.Execute(request);

            if (response.IsSuccessful)
            {
                var json = response.Content;
                var result = JsonConvert.DeserializeObject<JObject>(json);
                var teams = result.Value<JObject>("api").Value<JArray>("teams").ToObject<List<Team>>();
                return teams;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// Get the languages asynchronously using <see cref="GetAsync{T}(Uri)"/>
        /// </summary>
        /// <returns>The languages.</returns>
        /*public Team GetTeamsAsync()
        {
            return GetAsync<Team>(new Uri(serverUrl, "/teams/league/524"));
        }*/

    }
}
