using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace UpcomingMovies.ViewModels
{
    public class MovieItemViewModel
    {
        private bool isImageLoaded = false;

        public string Title { get; set; }
        public string Description { get; set; }
        public int RankPercentage { get; set; }
        public string PosterPath { get; set; }
        public int MovieIndex { get; set; }

        public ImageSource PosterSource
        {
            get
            {
                if (!isImageLoaded)
                {
                    return ImageSource.FromFile("hourglass.png");
                }

                return null;
                //return Task.Run(() => (ImageSource)new UriImageSource
                //{
                //    Uri = new Uri(PosterPath),
                //    CachingEnabled = true,
                //    CacheValidity = new TimeSpan(0, 0, 1, 0)
                //});
            }
        }
    }
}
