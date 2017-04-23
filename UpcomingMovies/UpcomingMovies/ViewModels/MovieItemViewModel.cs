using Xamarin.Forms;

namespace UpcomingMovies.ViewModels
{
    public class MovieItemViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int RankPercentage { get; set; }
        public ImageSource PosterSource { get; set; }
        public int MovieIndex { get; set; }
    }
}
