using Newtonsoft.Json;
using System.Collections.Generic;

namespace UpcomingMovies.Models.Json
{
    public class PageData
    {
        [JsonProperty(PropertyName = "page")]
        public int Page { get; set; }

        [JsonProperty(PropertyName = "results")]
        public List<MovieItemData> Results { get; set; }

        [JsonProperty(PropertyName = "total_results")]
        public int TotalResults { get; set; }

        [JsonProperty(PropertyName = "total_pages")]
        public int TotalPages { get; set; }
    }
}
