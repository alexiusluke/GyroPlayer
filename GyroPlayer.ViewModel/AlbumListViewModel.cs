﻿using System;
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
    public class AlbumListViewModel : INotifyPropertyChanged
    {
        private int _albumCount;
        private ISortLogic<AlbumViewModel> _selectedAlbumSortOption;
        private IEnumerable<AlbumViewModel> _albumsList;
        private bool _isSortAscending;
        private IEnumerable<ISortLogic<AlbumViewModel>> _albumSortOptions;
        private AlbumViewModel _selectedAlbum;
        private readonly IMusicPlayerSelectionController _musicPlayerSelectionController;
        private readonly IMusicManager _musicManager;


        public AlbumListViewModel(IMusicManager musicManager, IMusicPlayerSelectionController musicPlayerSelectionController)
        {
            _musicManager = musicManager;
            _musicPlayerSelectionController = musicPlayerSelectionController;
            AlbumSortOptions = new ISortLogic<AlbumViewModel>[]
            {
                new AlphabeticalSort<AlbumViewModel>(album => album.AlbumTitle),
                new ReleaseDateSort<AlbumViewModel>(album => album.ReleaseDate)
            };
            SelectedAlbumSortOption = AlbumSortOptions.FirstOrDefault();
            _musicPlayerSelectionController.ArtistSelectedEvent += _musicPlayerSelectionController_ArtistSelectedEvent; ;
            UpdateAlbums();                                                          
        }

        private void _musicPlayerSelectionController_ArtistSelectedEvent(object sender, EventArgs e)
        {
            UpdateAlbums();
        }

        private void UpdateAlbums()
        {
            if (!string.IsNullOrEmpty(_musicPlayerSelectionController.SelectedArtistName))
            {
                var artists = _musicManager.GetAllArtists()
                    .Where(obj => obj.Name == _musicPlayerSelectionController.SelectedArtistName);
                var albumViewModels = _musicManager.GetAlbums(artists).Select(album => new AlbumViewModel(album)).ToList();                
                AlbumsList = SortAlbums(albumViewModels);                
                SelectedAlbum = albumViewModels.FirstOrDefault();
                AlbumCount = albumViewModels.Count();
            }            
        }      

        public int AlbumCount
        {
            get { return _albumCount; }
            set
            {
                _albumCount = value;
                OnPropertyChanged(nameof(AlbumCount));
            }
        }

        public IEnumerable<ISortLogic<AlbumViewModel>> AlbumSortOptions
        {
            get { return _albumSortOptions; }
            set { _albumSortOptions = value; }
        }

        public ISortLogic<AlbumViewModel> SelectedAlbumSortOption
        {
            get { return _selectedAlbumSortOption; }
            set
            {
                if (_selectedAlbumSortOption != value)
                {
                    _selectedAlbumSortOption = value;
                    var lastSelectedAlbum = SelectedAlbum;                                    
                    AlbumsList = SortAlbums(AlbumsList);
                    SelectedAlbum = lastSelectedAlbum;
                }
                OnPropertyChanged(nameof(SelectedAlbumSortOption));
            }
        }

        private IEnumerable<AlbumViewModel> SortAlbums(IEnumerable<AlbumViewModel> albumViewModels)
        {
            IEnumerable<AlbumViewModel> sortedAlbums;
            if (IsSortAscending)
            {
                sortedAlbums = _selectedAlbumSortOption?.SortAscending(albumViewModels);
            }
            else
            {
                sortedAlbums = _selectedAlbumSortOption?.SortDescending(albumViewModels);
            }
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
                    var lastSelectedAlbum = SelectedAlbum;                                       
                    AlbumsList = SortAlbums(AlbumsList);
                    SelectedAlbum = lastSelectedAlbum;
                }
                OnPropertyChanged(nameof(IsSortAscending));
            }
        }

        public IEnumerable<AlbumViewModel> AlbumsList
        {
            get { return _albumsList; }
            set
            {
                _albumsList = value;
                OnPropertyChanged(nameof(AlbumsList));
            }
        }

        public AlbumViewModel SelectedAlbum
        {
            get { return _selectedAlbum; }
            set
            {
                _selectedAlbum = value;
                OnPropertyChanged(nameof(SelectedAlbum));
                if (_selectedAlbum != null)
                {
                    _musicPlayerSelectionController.SelectAlbum(_selectedAlbum.AlbumTitle);
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
