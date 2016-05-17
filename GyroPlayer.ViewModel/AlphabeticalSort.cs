using System;
using System.Collections.Generic;
using System.Linq;
using GyroPlayer.ViewModel.Interfaces;

namespace GyroPlayer.ViewModel
{
    public class AlphabeticalSort<T> : ISortLogic<T>
    {
        private readonly Func<T, string> _selector;

        public AlphabeticalSort(Func<T, string> selector)
        {
            _selector = selector;
        }

        public string SortText => "A-Z";

        public IEnumerable<T> SortAscending(IEnumerable<T> input)
        {           
            return input?.OrderBy(_selector);               
        }

        public IEnumerable<T> SortDescending(IEnumerable<T> input)
        {
            return input?.OrderByDescending(_selector);
        }
    }
}