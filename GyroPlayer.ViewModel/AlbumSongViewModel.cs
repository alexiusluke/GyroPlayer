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
    public class AlbumSongViewModel : INotifyPropertyChanged
    {
        private string _songTitle;
        private string _trackNumber;
        private ISong _song;

        public AlbumSongViewModel(ISong song)
        {
            this._song = song;
            SongTitle = _song.Title;
        }

        public string TrackNumber
        {
            get { return _trackNumber; }
            set
            {
                _trackNumber = value;
                OnPropertyChanged();
            }
        }

        public string SongTitle
        {
            get { return _songTitle; }
            set
            {
                _songTitle = value;
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
