[assembly: Xamarin.Forms.Dependency(typeof(UpcomingMovies.Settings.RemoteSettings))]
namespace UpcomingMovies.Settings
{
    public interface IRemoteSettings
    {
        string ApiKey { get; }
        string BaseUrl { get; }
        string ImageBaseUrl { get; }
        string MoviePageBaseUrl { get; }
        string DiscoverMethod { get; }
        string UpcomingMethod { get; }
        string PosterImageMethod { get; }
        string PosterThumbImageMethod { get; }
        string BackdropImageMethod { get; }
        int ItemsOnPage { get; }
    }
}
