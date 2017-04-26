using Newtonsoft.Json;

namespace UpcomingMovies.Models.Json
{
    public class GenreData
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}
