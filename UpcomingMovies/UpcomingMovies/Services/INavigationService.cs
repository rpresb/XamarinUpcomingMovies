using System;
using System.Threading.Tasks;

namespace UpcomingMovies.Services
{
    public interface INavigationService
    {
        Task NavigateToDetail(int movieIndex);
        Task NavigateBack();
        void OpenUri(Uri uri);
    }
}
