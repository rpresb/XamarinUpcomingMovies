using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using UpcomingMovies.Models;
using UpcomingMovies.Services;
using Xamarin.Forms;
using UpcomingMovies.Extensions;
using System.Windows.Input;
using System.Collections.Generic;

namespace UpcomingMovies.ViewModels
{
    public class MoviesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public MovieItemsObservableCollection MovieItems { get; protected set; } = new MovieItemsObservableCollection();

        private bool _isLoadig;
        public bool IsLoading
        {
            get { return _isLoadig; }
            protected set
            {
                _isLoadig = value;
                OnPropertyChanged("IsLoading");
            }
        }

        private ICommand _loadNextPageCommand;
        public ICommand LoadNextPageCommand
        {
            get
            {
                return _loadNextPageCommand = _loadNextPageCommand ??
                    new Command<MovieItemViewModel>(async (item) => await ExecuteLoadNextPageCommand(), CanExecuteLoadNextPageCommand);
            }
        }

        private ICommand _showDetailPageCommand;
        public ICommand ShowDetailPageCommand
        {
            get
            {
                return _showDetailPageCommand = _showDetailPageCommand ??
                    new Command<MovieItemViewModel>(async (item) => await ExecuteShowDetailPageCommand(item));
            }
        }

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
            try
            {
                IsLoading = true;
                var items = await _dataService.GetItemsAtAsync(page);

                if (items.Count == 0)
                {
                    // TODO: tell user
                }

                MovieItems.Add(items);

                _loadedPages = page;
            }
            finally
            {
                IsLoading = false;
            }

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

        public async Task ExecuteLoadNextPageCommand()
        {
            await LoadPage(_loadedPages + 1);
        }

        public bool CanExecuteLoadNextPageCommand(MovieItemViewModel item)
        {
            return !IsLoading && MovieItems.Count - 1 == item.MovieIndex;
        }

        public async Task ExecuteShowDetailPageCommand(MovieItemViewModel item)
        {
            await _navigationService.NavigateToDetail(item.MovieIndex);
        }
    }
}
