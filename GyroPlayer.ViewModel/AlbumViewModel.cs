using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GyroPlayer.Core.Interfaces;

namespace GyroPlayer.ViewModel
{
    public class AlbumViewModel : INotifyPropertyChanged
    {
        private IAlbum _album;
        private Uri _albumImageSource;
        private string _albumTitle;

        public AlbumViewModel(IAlbum album)
        {
            this._album = album;
            AlbumImageSource = _album.ImageUrl;
            AlbumTitle = _album.Title;
            ReleaseDate = _album.Date;
        }

        public DateTime ReleaseDate { get; set; }

        public Uri AlbumImageSource
        {
            get { return _albumImageSource; }
            set
            {
                _albumImageSource = value;
                OnPropertyChanged();
            }
        }

        public string AlbumTitle
        {
            get { return _albumTitle; }
            set
            {
                _albumTitle = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
