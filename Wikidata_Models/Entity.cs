using Newtonsoft.Json;
namespace FootballData
{
    public partial class Temperatures
    {
        [JsonProperty("entities")]
        public Entities Entities { get; set; }

        [JsonProperty("success")]
        public long Success { get; set; }
    }

    public partial class Entities
    {
        [JsonPropertyNameBasedOnItemClass]
        public ID ID { get; set; }
    }

    public partial class ID
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("labels")]
        public Labels Labels { get; set; }
    }

    public partial class Labels
    {
        [JsonProperty("en")]
        public En En { get; set; }
    }

    public partial class En
    {
        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}