using System;
using Xamarin.Forms;

namespace UpcomingMovies.ViewModels
{
    public class MovieDetailViewModel
    {
        public string Title { get; set; }
        public string OriginalTitle { get; set; }
        public int Year { get; set; }
        public string Overview { get; set; }
        public int RankPercentage { get; set; }
        public string ButtonText { get; set; }
        public string Genres { get; set; }
        public bool IsOriginalTitleVisible { get { return string.Compare(OriginalTitle, Title) != 0; } }
        public ImageSource BackdropImageSource { get; set; }
        public ImageSource PosterImageSource { get; set; }
        public Uri MoviePageUri { get; set; }
    }
}
