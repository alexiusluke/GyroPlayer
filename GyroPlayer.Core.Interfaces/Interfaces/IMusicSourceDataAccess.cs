using System.Collections.Generic;

namespace GyroPlayer.Core.Interfaces
{
    public interface IMusicSourceDataAccess
    {
        void BuildData();
        IEnumerable<IArtist> GetArtists();
        IEnumerable<IAlbum> GetAlbums(string queryArtistName);
        IEnumerable<ISong> GetSongs(string queryArtistName, string queryAlbumTitle);
    }
}