using System.Collections.Generic;
using System.Threading.Tasks;

namespace UpcomingMovies.Services
{
    public interface IDataService<T>
    {
        Task<List<T>> GetItemsAtAsync(int page);
        int GetCountOfItemsOnPage();
    }
}
