using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpcomingMovies.Settings;

namespace UpcomingMovies.Services
{
    public interface IRemoteService<T> : IDataService<T>
    {
        IRemoteSettings Settings { get; }
    }
}
