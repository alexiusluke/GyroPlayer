using System;
using System.Collections.Generic;
using System.Linq;
using GyroPlayer.ViewModel.Interfaces;

namespace GyroPlayer.ViewModel
{
    public class ReleaseDateSort<T> : ISortLogic<T>
    {
        private readonly Func<T, DateTime> _dateSortFunc;

        public ReleaseDateSort(Func<T, DateTime> dateSortFunc)
        {
            _dateSortFunc = dateSortFunc;
        }

        public string SortText => "Release Date";
        public IEnumerable<T> SortAscending(IEnumerable<T> input)
        {
            return input.OrderBy(_dateSortFunc);
        }

        public IEnumerable<T> SortDescending(IEnumerable<T> input)
        {
            return input.OrderByDescending(_dateSortFunc);
        }
    }
}