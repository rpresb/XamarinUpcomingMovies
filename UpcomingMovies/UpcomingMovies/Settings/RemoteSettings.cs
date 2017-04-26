using System;

namespace UpcomingMovies.Settings
{
    public class RemoteSettings : IRemoteSettings
    {
        public string ApiKey { get; } = "1f54bd990f1cdfb230adb312546d765d";
        public string BaseUrl { get; } = "http://api.themoviedb.org/3";
        public string ImageBaseUrl { get; } = "https://image.tmdb.org";
        public string MoviePageBaseUrl { get; } = "https://www.themoviedb.org/movie";
        public string GenreMethod { get; } = "/genre/movie/list";
        public string DiscoverMethod { get; } = "/discover/movie";
        public string UpcomingMethod { get; } = "/movie/upcoming";
        public string PosterImageMethod { get; } = "/t/p/w370";
        public string PosterThumbImageMethod { get; } = "/t/p/w92";
        public string BackdropImageMethod { get; } = "/t/p/w600";
        public int ItemsOnPage { get; } = 20;
    }
}
