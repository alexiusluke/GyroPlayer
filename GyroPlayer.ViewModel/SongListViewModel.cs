using System;
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
    public class SongListViewModel : INotifyPropertyChanged
    {
        private int _songCount;
        private ISortLogic<AlbumSongViewModel> _selectedSongSortOption;
        private IEnumerable<AlbumSongViewModel> _songsList;
        private bool _isSortAscending;
        private ISortLogic<AlbumSongViewModel>[] _songSortOptions;
        private AlbumSongViewModel _selectedSong;
        private readonly IMusicPlayerSelectionController _musicPlayerSelectionController;
        private readonly IMusicManager _musicManager;


        public SongListViewModel(IMusicManager musicManager, IMusicPlayerSelectionController musicPlayerSelectionController)
        {
            _musicManager = musicManager;
            _musicPlayerSelectionController = musicPlayerSelectionController;
            SongSortOptions = new ISortLogic<AlbumSongViewModel>[]
            {
                new AlphabeticalSort<AlbumSongViewModel>(album => album.SongTitle)
            };
            SelectedSongSortOption = SongSortOptions.FirstOrDefault();
            _musicPlayerSelectionController.AlbumSelectedEvent += MusicPlayerSelectionControllerOnAlbumSelectedEvent;
            UpdateSongsList();
        }

        private void MusicPlayerSelectionControllerOnAlbumSelectedEvent(object sender, EventArgs eventArgs)
        {
            UpdateSongsList();
        }

        private void UpdateSongsList()
        {
            if (!string.IsNullOrEmpty(_musicPlayerSelectionController.SelectedArtistName) &&
                !string.IsNullOrEmpty(_musicPlayerSelectionController.SelectedAlbumTitle))
            {
                var artists = _musicManager.GetAllArtists()
                    .Where(obj => obj.Name.Equals(_musicPlayerSelectionController.SelectedArtistName));
                var albums = _musicManager.GetAlbums(artists).Where(obj => obj.Title.Equals(_musicPlayerSelectionController.SelectedAlbumTitle));
                var songs = _musicManager.GetSong(albums).Select(obj => new AlbumSongViewModel(obj)).ToList();
                var lastSelectedSongTitle = SelectedSong?.SongTitle;
                SongsList = SortSongs(songs);
                var lastSelectedSong = SongsList.SingleOrDefault(song => song.SongTitle.Equals(lastSelectedSongTitle));
                SelectedSong = lastSelectedSong ?? songs.FirstOrDefault();
                SongCount = songs.Count();
            }
        }        

        public int SongCount
        {
            get { return _songCount; }
            set
            {
                _songCount = value;
                OnPropertyChanged(nameof(SongCount));
            }
        }

        public ISortLogic<AlbumSongViewModel>[] SongSortOptions
        {
            get { return _songSortOptions; }
            set { _songSortOptions = value; }
        }

        public ISortLogic<AlbumSongViewModel> SelectedSongSortOption
        {
            get { return _selectedSongSortOption; }
            set
            {
                if (_selectedSongSortOption != value)
                {
                    _selectedSongSortOption = value;
                    SongsList = SortSongs(SongsList);
                }
                OnPropertyChanged(nameof(SelectedSongSortOption));
            }
        }

        private IEnumerable<AlbumSongViewModel> SortSongs(IEnumerable<AlbumSongViewModel> albumViewModels)
        {
            IEnumerable<AlbumSongViewModel> sortedAlbums;
            if (IsSortAscending)
            {
                sortedAlbums = _selectedSongSortOption?.SortAscending(albumViewModels);
            }
            else
            {
                sortedAlbums = _selectedSongSortOption?.SortDescending(albumViewModels);
            }
            sortedAlbums = sortedAlbums?.Select((obj, index) =>
            {
                obj.TrackNumber = (index + 1).ToString("D" + SongCount.ToString().Length);
                return obj;
            });
            return sortedAlbums;
        }

        public bool IsSortAscending
        {
            get { return _isSortAscending; }
            set
            {
                if (_isSortAscending != value)
                {
                    _isSortAscending = value;
                    var lastSelecteSong = SelectedSong;                   
                    SongsList = SortSongs(SongsList);
                    SelectedSong = lastSelecteSong;
                }
                OnPropertyChanged(nameof(IsSortAscending));
            }
        }

        public IEnumerable<AlbumSongViewModel> SongsList
        {
            get { return _songsList; }
            set
            {
                _songsList = value;
                OnPropertyChanged(nameof(SongsList));
            }
        }

        public AlbumSongViewModel SelectedSong
        {
            get { return _selectedSong; }
            set
            {
                _selectedSong = value;
                OnPropertyChanged(nameof(SelectedSong));                
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
