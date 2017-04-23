using System;
using UpcomingMovies.Models;
using UpcomingMovies.ViewModels;
using Xamarin.Forms;

namespace UpcomingMovies.Extensions
{
    public static class HelperExtensions
    {
        public static MovieItemViewModel ToMovieItemViewModel(this IMovieItem item)
        {
            return new MovieItemViewModel()
            {
                MovieIndex = item.Index,
                Title = item.Title,
                Description = $"({(int)(item.VoteAverage * 10)}%) {item.Genres}",
                PosterSource = new UriImageSource
                {
                    Uri = new Uri(item.PosterThumbPath),
                    CachingEnabled = true,
                    CacheValidity = new TimeSpan(10, 0, 1, 0)
                }
            };
        }

        public static MovieDetailViewModel ToMovieDetailViewModel(this IMovieItem movieItem)
        {
            return new MovieDetailViewModel
            {
                Title = movieItem.Title,
                OriginalTitle = movieItem.OriginalTitle,
                Genres = movieItem.Genres,
                Overview = movieItem.Overview,
                Year = Convert.ToDateTime(movieItem.ReleaseDate).Year,
                RankPercentage = (int)(movieItem.VoteAverage * 10),
                ButtonText = "Open Website",
                PosterImageSource = new UriImageSource()
                {
                    Uri = new Uri(movieItem.PosterPath),
                    CachingEnabled = true,
                    CacheValidity = new TimeSpan(1, 0, 0, 0)
                },
                BackdropImageSource = new UriImageSource()
                {
                    Uri = new Uri(movieItem.BackdropPath), // TODO: what if empty?
                    CachingEnabled = true,
                    CacheValidity = new TimeSpan(1, 0, 0, 0)
                },
                MoviePageUri = new Uri(movieItem.MovieUrl),

            };
        }

    }
}
