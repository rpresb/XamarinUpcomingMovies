using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace UpcomingMovies.ViewModels
{
    public class MovieItemViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ImageSource _posterSource = null;

        public string Title { get; set; }
        public string Description { get; set; }
        public int RankPercentage { get; set; }
        public string PosterPath { get; set; }
        public int MovieIndex { get; set; }

        public ImageSource PosterSource
        {
            get
            {
                if (_posterSource == null)
                {
                    Task[] tasks = new Task[] {
                        Task.Factory.StartNew(async() =>
                        {
                            await Task.Delay(1000);

                            _posterSource = new UriImageSource
                            {
                                Uri = new Uri(PosterPath),
                                CachingEnabled = true,
                                CacheValidity = new TimeSpan(0, 0, 1, 0)
                            };
                        })
                    };

                    Task.Factory.ContinueWhenAll(tasks, _ =>
                    {
                        OnPropertyChanged("PosterSource");
                    });

                    return ImageSource.FromResource("UpcomingMovies.Assets.hourglass.png");
                }

                return _posterSource;
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
    }
}
