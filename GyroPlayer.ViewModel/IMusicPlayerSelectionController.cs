using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GyroPlayer.ViewModel
{
    public interface IMusicPlayerSelectionController
    {
        event EventHandler<EventArgs> ArtistSelectedEvent;
        event EventHandler<EventArgs> AlbumSelectedEvent;
        string SelectedArtistName { get; }
        string SelectedAlbumTitle { get; }
        void SelectArtist(string artistName);
        void SelectAlbum(string albumTitle);
    }

    public class MusicPlayerSelectionController : IMusicPlayerSelectionController
    {
        private string _selectedArtistName;
        private string _selectedAlbumTitle;        
        public event EventHandler<EventArgs> ArtistSelectedEvent = delegate {};
        public event EventHandler<EventArgs> AlbumSelectedEvent = delegate { };

        public string SelectedArtistName => _selectedArtistName;

        public string SelectedAlbumTitle => _selectedAlbumTitle;

        public void SelectArtist(string artistName)
        {
            if (_selectedArtistName != artistName)
            {
                _selectedArtistName = artistName;
                ArtistSelectedEvent(this, null);
            }            
        }

        public void SelectAlbum(string albumTitle)
        {
            if (_selectedAlbumTitle != albumTitle)
            {
                _selectedAlbumTitle = albumTitle;
                AlbumSelectedEvent(this, null);
            }            
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
