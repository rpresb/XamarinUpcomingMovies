﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using UpcomingMovies.Models;
using UpcomingMovies.Services;
using Xamarin.Forms;
using UpcomingMovies.Extensions;
using System.Windows.Input;
using System.Collections.Generic;
using UpcomingMovies.Exceptions;
using System.Runtime.CompilerServices;

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
                OnPropertyChanged();
            }
        }

        private bool _isConnected = true;
        public bool IsConnected
        {
            get { return _isConnected; }
            protected set
            {
                _isConnected = value;
                OnPropertyChanged();
            }
        }

        private ICommand _loadNextPageCommand;
        public ICommand LoadNextPageCommand
        {
            get
            {
                return _loadNextPageCommand = _loadNextPageCommand ??
                    new Command<MovieDetailViewModel>(async (item) => await ExecuteLoadNextPageCommand(), CanExecuteLoadNextPageCommand);
            }
        }

        private ICommand _showDetailPageCommand;
        public ICommand ShowDetailPageCommand
        {
            get
            {
                return _showDetailPageCommand = _showDetailPageCommand ??
                    new Command<MovieDetailViewModel>(async (item) => await ExecuteShowDetailPageCommand(item));
            }
        }

        public ICommand RefreshListView
        {
            get
            {
                return new Command(async () =>
                {
                    MovieItems.Clear();
                    await LoadPage(1);
                });
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

                MovieItems.AddMovieItems(items);

                _loadedPages = page;
                IsConnected = true;
            }
            catch (ContentUnavailableException ex)
            {
                IsConnected = false;
            }
            finally
            {
                IsLoading = false;
            }

        }

        protected void OnPropertyChanged([CallerMemberName]string propertyName = "")
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

        public bool CanExecuteLoadNextPageCommand(MovieDetailViewModel item)
        {
            return !IsLoading && MovieItems.Count - 1 == item.MovieIndex;
        }

        public async Task ExecuteShowDetailPageCommand(MovieDetailViewModel item)
        {
            await _navigationService.NavigateToDetailAsync(item);
        }
    }
}
