using System;
using Xamarin.Forms;

namespace UpcomingMovies.ViewModels
{
    public class MovieDetailViewModel
    {
        public int MovieIndex { get; set; }
        public string Title { get; set; }
        public string OriginalTitle { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public string Overview { get; set; }
        public int RankPercentage { get; set; }
        public string ButtonText { get; set; }
        public string Genres { get; set; }
        public bool IsOriginalTitleVisible { get { return string.Compare(OriginalTitle, Title) != 0; } }
        public string ReleaseDate { get; set; }
        public string BackdropImagePath { get; set; }
        public string PosterImagePath { get; set; }
        public Uri MoviePageUri { get; set; }
    }
}
