using System.Collections.Generic;

namespace GyroPlayer.ViewModel.Interfaces
{
    public interface ISortLogic<T> : ISortOption
    {        
        IEnumerable<T> SortAscending(IEnumerable<T> input);
        IEnumerable<T> SortDescending(IEnumerable<T> input);
    }
}