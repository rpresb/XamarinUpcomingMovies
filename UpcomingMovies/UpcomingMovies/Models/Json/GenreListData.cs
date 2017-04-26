using Newtonsoft.Json;
using System.Collections.Generic;

namespace UpcomingMovies.Models.Json
{
    public class GenreListData
    {
        [JsonProperty(PropertyName = "genres")]
        public List<GenreData> Genres { get; set; }
    }
}
