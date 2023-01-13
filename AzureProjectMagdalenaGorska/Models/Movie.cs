using Newtonsoft.Json;

namespace AzureProjectMagdalenaGorska.Models
{
    public class Movie
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("director")]
        public string Director { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
