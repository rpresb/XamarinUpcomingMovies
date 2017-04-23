using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using UpcomingMovies.Models;
using UpcomingMovies.Services;
using Xamarin.Forms;
using UpcomingMovies.Extensions;

namespace UpcomingMovies.ViewModels
{
    public class MoviesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<MovieItemViewModel> MovieItems { get; protected set; } = new ObservableCollection<MovieItemViewModel>();

        private bool _isLoadig;
        public bool IsLoading { get { return _isLoadig; } protected set { _isLoadig = value; OnPropertyChanged("IsLoading"); } }

        private IDataService<IMovieItem> _dataService;
        private INavigationService _navigationService;
        private int _loadedPages;

        public MoviesViewModel()
        {
            _dataService = _dataService ?? DependencyService.Get<IDataService<IMovieItem>>();
            _navigationService = _navigationService ?? DependencyService.Get<INavigationService>();

            InitializeAsync();
        }

        private async void InitializeAsync()
        {
            await LoadPage(1);
        }

        public async Task LoadPage(int page)
        {
            IsLoading = true;
            var items = await _dataService.GetItemsAtAsync(page);

            if (items.Count == 0)
            {
                // TODO: tell user
            }

            foreach (var item in items)
            {
                MovieItems.Add(item.ToMovieItemViewModel());
            }

            _loadedPages = page;
            IsLoading = false;

        }

        protected void OnPropertyChanged(string propertyName)
        {
            try
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            catch
            {
            }
        }
    }
}
