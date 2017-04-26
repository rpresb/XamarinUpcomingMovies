﻿using System;
using System.Globalization;
using UpcomingMovies.Models;
using UpcomingMovies.ViewModels;

namespace UpcomingMovies.Extensions
{
    public static class HelperExtensions
    {
        public static MovieDetailViewModel ToMovieDetailViewModel(this IMovieItem movieItem)
        {
            DateTime releaseDate;
            string releaseDateString = string.Empty;
            if (DateTime.TryParseExact(movieItem.ReleaseDate, "yyyy-MM-dd",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out releaseDate))
            {
                releaseDateString = releaseDate.ToString("MM/dd/yyyy");
            }

            int voteAverage = (int)Math.Ceiling(movieItem.VoteAverage * 10);

            return new MovieDetailViewModel
            {
                MovieIndex = movieItem.Index,
                Title = movieItem.Title,
                OriginalTitle = movieItem.OriginalTitle,
                Description = $"({voteAverage}%) {movieItem.Genres}",
                Genres = movieItem.Genres,
                Overview = movieItem.Overview,
                Year = Convert.ToDateTime(movieItem.ReleaseDate).Year,
                RankPercentage = voteAverage,
                ButtonText = "Open Website",
                PosterImagePath = movieItem.PosterPath,
                BackdropImagePath = movieItem.BackdropPath,
                MoviePageUri = new Uri(movieItem.MovieUrl),
                ReleaseDate = releaseDateString
            };
        }

    }
}
