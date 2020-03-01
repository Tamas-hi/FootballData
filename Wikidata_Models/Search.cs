using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FootballData.Wikidata_Models
{

    public partial class RootSearchObject
    {
        [JsonProperty("searchinfo")]
        public Searchinfo Searchinfo { get; set; }

        [JsonProperty("search")]
        public Search[] Search { get; set; }

        [JsonProperty("search-continue")]
        public long SearchContinue { get; set; }

        [JsonProperty("success")]
        public long Success { get; set; }
    }

    public partial class Search
    {
        [JsonProperty("repository")]
        public string Repository { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("concepturi")]
        public Uri Concepturi { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("pageid")]
        public long Pageid { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("match")]
        public Match Match { get; set; }
    }

    public partial class Match
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }

    public partial class Searchinfo
    {
        [JsonProperty("search")]
        public string Search { get; set; }
    }
}