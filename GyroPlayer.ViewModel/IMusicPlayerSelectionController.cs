using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GyroPlayer.ViewModel
{
    public interface IMusicPlayerSelectionController
    {
        event EventHandler<MusicSelectionArgs> MusicLibrarySelectionEvent;
        string SelectedArtistName { get; }
        string SelectedAlbumTitle { get; }
        void SelectArtist(string artistName);
        void SelectAlbum(string albumTitle);
    }

    public class MusicPlayerSelectionController : IMusicPlayerSelectionController
    {
        private string _selectedArtistName;
        private string _selectedAlbumTitle;        
        public event EventHandler<MusicSelectionArgs> MusicLibrarySelectionEvent = delegate {};

        public string SelectedArtistName
        {
            get { return _selectedArtistName; }
        }

        public string SelectedAlbumTitle
        {
            get { return _selectedAlbumTitle; }
        }

        public void SelectArtist(string artistName)
        {
            _selectedArtistName = artistName;
            MusicLibrarySelectionEvent(this, new MusicSelectionArgs(artistName, null, null));
        }

        public void SelectAlbum(string albumTitle)
        {
            _selectedAlbumTitle = albumTitle;
            MusicLibrarySelectionEvent(this, new MusicSelectionArgs(_selectedArtistName, _selectedAlbumTitle, null));
        }
    }

    public class MusicSelectionArgs : EventArgs
    {
        public MusicSelectionArgs(string artist, string album, string song)
        {
            Artist = artist;
            Album = album;
            Song = song;
        }

        public string Artist { get; private set; }

        public string Album { get; private set; }

        public string Song { get; private set; }

    }
}
