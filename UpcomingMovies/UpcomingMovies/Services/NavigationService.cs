using System;
using System.Threading.Tasks;
using UpcomingMovies.ViewModels;
using Xamarin.Forms;

[assembly: Dependency(typeof(UpcomingMovies.Services.NavigationService))]
namespace UpcomingMovies.Services
{
    public class NavigationService : INavigationService
    {
        private INavigation _navigation
        {
            get { return App.Navigation; }
        }

        public Task NavigateBack()
        {
            return _navigation.PopAsync();
        }

        public async Task NavigateToDetailAsync(MovieDetailViewModel movie)
        {
            await _navigation.PushAsync(new MovieDetailPage() { BindingContext = movie });
        }

        public void OpenUri(Uri uri)
        {
            Device.OpenUri(uri);
        }
    }
}
