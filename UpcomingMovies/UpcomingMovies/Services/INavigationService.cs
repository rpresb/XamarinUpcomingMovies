using System;
using System.Threading.Tasks;
using UpcomingMovies.ViewModels;

namespace UpcomingMovies.Services
{
    public interface INavigationService
    {
        Task NavigateToDetailAsync(MovieDetailViewModel movie);
        Task NavigateBack();
        void OpenUri(Uri uri);
    }
}
