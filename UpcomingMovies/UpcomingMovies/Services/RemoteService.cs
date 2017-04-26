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
using UpcomingMovies.Exceptions;

[assembly: Dependency(typeof(UpcomingMovies.Services.RemoteService))]
namespace UpcomingMovies.Services
{
    class RemoteService : IRemoteService<IMovieItem>
    {
        public List<IMovieItem> Items { get; private set; }
        public IRemoteSettings Settings { get; private set; }

        private List<GenreData> _genresCollection = null;
        private HttpClient _client;

        public RemoteService()
        {
            _client = new HttpClient();
            Settings = DependencyService.Get<IRemoteSettings>();
        }

        public async Task<List<GenreData>> GetGenresAsync()
        {
            if (_genresCollection == null)
            {
                _genresCollection = await RequestGenreListFromMovieDb();
            }

            return _genresCollection;
        }

        public async Task<List<IMovieItem>> GetItemsAtAsync(int page)
        {
            Items = new List<IMovieItem>();

            var pageData = await RequestUpcomingPageDataFromMovieDb(page);

            return await GenerateMovieListFromJsonPageDataAsync(pageData);
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

        private async Task<string> CreateGenresStringAsync(IEnumerable<int> genres)
        {
            try
            {
                var cachedGenres = await GetGenresAsync();

                return string.Join(" / ", genres.Select(g => cachedGenres.FirstOrDefault(c => c.Id.Equals(g)).Name));
            }
            catch
            {
                return string.Empty;
            }
        }

        private async Task<List<GenreData>> RequestGenreListFromMovieDb()
        {
            GenreListData genreListData = null;
            var uriString = CreateRequestUri
            (
                Settings.BaseUrl,
                Settings.GenreMethod,
                new Dictionary<string, object>()
                {
                    { "api_key", Settings.ApiKey },
                    { "language", "en-US" },
                }
            );

            var content = await GetContentFromUriAsync(uriString);
            if (!string.IsNullOrWhiteSpace(content))
            {
                genreListData = JsonConvert.DeserializeObject<GenreListData>(content);
            }

            return genreListData?.Genres;
        }

        private async Task<PageData> RequestUpcomingPageDataFromMovieDb(int page)
        {
            PageData pageData = null;
            var uriString = CreateRequestUri
            (
                Settings.BaseUrl,
                Settings.UpcomingMethod,
                new Dictionary<string, object>()
                {
                    { "api_key", Settings.ApiKey },
                    { "page", page },
                }
            );

            var content = await GetContentFromUriAsync(uriString);
            if (!string.IsNullOrWhiteSpace(content))
            {
                pageData = JsonConvert.DeserializeObject<PageData>(content);
            }

            return pageData;
        }

        private async Task<List<IMovieItem>> GenerateMovieListFromJsonPageDataAsync(PageData pageData)
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
                    Genres = await CreateGenresStringAsync(jsonItem.GenreIds),
                    BackdropPath = GetBackdropImagePath(jsonItem.BackdropPath),
                    MovieUrl = GetMovieUrl(jsonItem.Id),
                    PosterPath = GetPosterImagePath(jsonItem.PosterPath),
                    PosterThumbPath = GetPosterImagePath(jsonItem.PosterPath, true),
                };
                items.Add(item);
            }

            return items.OrderByDescending(x => x.ReleaseDate).ToList();
        }

        private async Task<string> GetContentFromUriAsync(string uriString)
        {
            string content = null;

            try
            {
                var response = await _client.GetAsync(uriString);

                // TODO: Handle other status codes
                if (response.IsSuccessStatusCode)
                {
                    content = await response.Content.ReadAsStringAsync();
                    return content;
                }
            }
            catch // ignore all connection exceptions and raise custom exception when content is unavailable
            {
            }

            throw new ContentUnavailableException();
        }

    }
}
