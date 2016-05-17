using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GyroPlayer.Core.Interfaces;
using GyroPlayer.ViewModel.Interfaces;

namespace GyroPlayer.ViewModel
{
    public class ArtistListViewModel : INotifyPropertyChanged
    {
        private int _artistCount;
        private ISortLogic<ArtistViewModel> _selectedArtistSortOption;
        private IEnumerable<ArtistViewModel> _artistsList;
        private bool _isSortAscending;
        private IEnumerable<ISortLogic<ArtistViewModel>> _artistSortOptions;
        private ArtistViewModel _selectedArtist;
        private readonly IMusicPlayerSelectionController _musicPlayerSelectionController;


        public ArtistListViewModel(IMusicManager musicManager, IMusicPlayerSelectionController musicPlayerSelectionController)
        {
            _musicPlayerSelectionController = musicPlayerSelectionController;
            ArtistsList = musicManager.GetAllArtists().Select(artist => new ArtistViewModel(artist)).ToList();
            ArtistCount = ArtistsList.Count();
            SelectedArtist = ArtistsList.FirstOrDefault();
            ArtistSortOptions = new[] {new AlphabeticalSort<ArtistViewModel>(artist => artist.ArtistName)};
            SelectedArtistSortOption = ArtistSortOptions.FirstOrDefault();            
        }

        public int ArtistCount
        {
            get { return _artistCount; }
            set
            {
                _artistCount = value;
                OnPropertyChanged(nameof(ArtistCount));
            }
        }

        public IEnumerable<ISortLogic<ArtistViewModel>> ArtistSortOptions
        {
            get { return _artistSortOptions; }
            set { _artistSortOptions = value; }
        }

        public ISortLogic<ArtistViewModel> SelectedArtistSortOption
        {
            get { return _selectedArtistSortOption; }
            set
            {
                if (_selectedArtistSortOption != value)
                {
                    _selectedArtistSortOption = value;
                    if (IsSortAscending)
                    {
                        ArtistsList = _selectedArtistSortOption.SortAscending(ArtistsList);
                    }
                    else
                    {
                        ArtistsList = _selectedArtistSortOption.SortDescending(ArtistsList);
                    }
                }
                OnPropertyChanged(nameof(SelectedArtistSortOption));
            }
        }

        public bool IsSortAscending
        {
            get { return _isSortAscending; }
            set
            {
                if (_isSortAscending != value)
                {
                    _isSortAscending = value;
                    var lastSelectedArtist = SelectedArtist;                    
                    ArtistsList = SortArtistList();
                    SelectedArtist = lastSelectedArtist;
                }
                OnPropertyChanged(nameof(IsSortAscending));
            }
        }

        private IEnumerable<ArtistViewModel> SortArtistList()
        {
            IEnumerable<ArtistViewModel> sortedArtistList;
            if (IsSortAscending)
            {
                sortedArtistList = _selectedArtistSortOption.SortAscending(ArtistsList);
            }
            else
            {
                sortedArtistList = _selectedArtistSortOption.SortDescending(ArtistsList);
            }
            return sortedArtistList;
        }

        public IEnumerable<ArtistViewModel> ArtistsList
        {
            get { return _artistsList; }
            set
            {
                _artistsList = value; 
                OnPropertyChanged(nameof(ArtistsList));
            }
        }

        public ArtistViewModel SelectedArtist
        {
            get { return _selectedArtist; }
            set
            {                
                _selectedArtist = value;
                OnPropertyChanged(nameof(SelectedArtist));
                if (_selectedArtist != null)
                {
                    _musicPlayerSelectionController?.SelectArtist(_selectedArtist.ArtistName);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }    
}
