using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpcomingMovies.Models;
using UpcomingMovies.Models.Json;
using UpcomingMovies.Settings;
using Xamarin.Forms;
using System.Net.Http;
using Newtonsoft.Json;

[assembly: Dependency(typeof(UpcomingMovies.Services.RemoteService))]
namespace UpcomingMovies.Services
{
    class RemoteService : IRemoteService<IMovieItem>
    {
        public List<IMovieItem> Items { get; private set; }
        public IRemoteSettings Settings { get; private set; }

        public static Dictionary<int, string> GenresDict = new Dictionary<int, string>()
        {
            { 28, "Action" },
            { 12, "Adventure" },
            { 16, "Animation" },
            { 35, "Comedy" },
            { 80, "Crime" },
            { 99, "Documentary" },
            { 18, "Drama" },
            { 10751, "Family" },
            { 14, "Fantasy" },
            { 10769, "Foreign" },
            { 36, "History" },
            { 27, "Horror" },
            { 10402, "Music" },
            { 9648, "Mystery" },
            { 10749, "Romance" },
            { 878, "Science Fiction" },
            { 10770, "TV Movie" },
            { 53, "Thriller" },
            { 10752, "War" },
            { 37, "Western" },
        };

        private HttpClient _client;

        public RemoteService()
        {
            _client = new HttpClient();
            Settings = DependencyService.Get<IRemoteSettings>();
        }

        public async Task<List<IMovieItem>> GetItemsAtAsync(int page)
        {
            Items = new List<IMovieItem>();

            var pageData = await RequestPageDataFromMovieDb(page);

            return GenerateMovieListFromJsonPageData(pageData);
        }

        public int GetCountOfItemsOnPage()
        {
            return Settings.ItemsOnPage;
        }

        public string GetPosterImagePath(string imagePath, bool isThumbnail = false)
        {
            try
            {
                return string.Concat(Settings.ImageBaseUrl, isThumbnail ? Settings.PosterThumbImageMethod : Settings.PosterImageMethod, imagePath);
            }
            catch
            {
                return string.Empty;
            }
        }

        public string GetBackdropImagePath(string imagePath)
        {
            try
            {
                return string.Concat(Settings.ImageBaseUrl, Settings.BackdropImageMethod, imagePath);
            }
            catch
            {
                return string.Empty;
            }
        }

        public string GetMovieUrl(int movieId)
        {
            try
            {
                return string.Format("{0}/{1}", Settings.MoviePageBaseUrl, movieId);
            }
            catch
            {
                return string.Empty;
            }
        }

        private static string CreateRequestUri(string baseUrl, string method, Dictionary<string, object> parameters)
        {
            var sb = new StringBuilder();

            sb.Append(baseUrl);
            sb.Append(method);

            // Add parameters
            bool first = true;
            foreach (var param in parameters)
            {
                var format = first ? "?{0}={1}" : "&{0}={1}";
                sb.AppendFormat(format, Uri.EscapeDataString(param.Key), Uri.EscapeDataString(param.Value.ToString()));
                first = false;
            }

            return sb.ToString();
        }

        private static string CreateGenresString(IEnumerable<int> genres)
        {
            try
            {
                return string.Join(" / ", genres.Select(g => GenresDict[g]));
            }
            catch
            {
                return string.Empty;
            }
        }

        private async Task<PageData> RequestPageDataFromMovieDb(int page)
        {
            PageData pageData = null;
            var items = new List<MovieItemData>();
            var uriString = CreateRequestUri
            (
                Settings.BaseUrl,
                Settings.DiscoverMethod,
                new Dictionary<string, object>()
                {
                    { "api_key", Settings.ApiKey },
                    { "page", page },
                }
            );

            var response = await _client.GetAsync(uriString);

            if (response.IsSuccessStatusCode)
            {
                // get data
                var content = await response.Content.ReadAsStringAsync();
                pageData = JsonConvert.DeserializeObject<PageData>(content);
            }

            return pageData;
        }

        private List<IMovieItem> GenerateMovieListFromJsonPageData(PageData pageData)
        {
            var items = new List<IMovieItem>();
            var jsonItems = pageData.Results;
            foreach (var jsonItem in jsonItems)
            {
                var item = new MovieItem()
                {
                    Adult = jsonItem.Adult,
                    Id = jsonItem.Id,
                    OriginalLanguage = jsonItem.OriginalLanguage,
                    OriginalTitle = jsonItem.OriginalTitle,
                    Overview = jsonItem.Overview,
                    Popularity = jsonItem.Popularity,
                    ReleaseDate = jsonItem.ReleaseDate,
                    Title = jsonItem.Title,
                    VoteAverage = jsonItem.VoteAverage,
                    VoteCount = jsonItem.VoteCount,

                    // Add some additional info
                    Index = (pageData.Page - 1) * GetCountOfItemsOnPage() + jsonItems.IndexOf(jsonItem),
                    Genres = CreateGenresString(jsonItem.GenreIds),
                    BackdropPath = GetBackdropImagePath(jsonItem.BackdropPath),
                    MovieUrl = GetMovieUrl(jsonItem.Id),
                    PosterPath = GetPosterImagePath(jsonItem.PosterPath),
                    PosterThumbPath = GetPosterImagePath(jsonItem.PosterPath, true),
                };
                items.Add(item);
            }

            return items;
        }

    }
}
