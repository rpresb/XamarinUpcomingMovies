using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpcomingMovies.Models;
using Xamarin.Forms;

namespace UpcomingMovies.Services
{
    class MoviesService : IDataService<IMovieItem>
    {
        private IRemoteService<IMovieItem> _remoteDataService;

        public MoviesService()
        {
            _remoteDataService = DependencyService.Get<IRemoteService<IMovieItem>>();
        }

        public MoviesService(IRemoteService<IMovieItem> remoteDataService)
        {
            _remoteDataService = remoteDataService;
        }

        public int GetCountOfItemsOnPage()
        {
            return _remoteDataService.GetCountOfItemsOnPage();
        }

        public async Task<List<IMovieItem>> GetItemsAtAsync(int page)
        {
            try
            {
                var movieItems = await _remoteDataService.GetItemsAtAsync(page);
                return movieItems;
            }
            catch
            {
                throw new Exception("Unable to connect to the server, please check you connection and try again.");
            }
        }
    }
}
