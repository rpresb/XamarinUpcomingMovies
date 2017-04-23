using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpcomingMovies.Models;
using UpcomingMovies.ViewModels;

namespace UpcomingMovies.Extensions
{
    public class MovieItemsObservableCollection : ObservableCollection<MovieDetailViewModel>
    {
        bool isObserving = true;
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (isObserving)
            {
                base.OnCollectionChanged(e);
            }
        }

        public void Add(List<IMovieItem> items)
        {
            isObserving = false;
            foreach (var i in items)
            {
                Add(i.ToMovieDetailViewModel());
            }
            isObserving = true;

            var e = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, items.ToList());
            base.OnCollectionChanged(e);
        }
    }
}
