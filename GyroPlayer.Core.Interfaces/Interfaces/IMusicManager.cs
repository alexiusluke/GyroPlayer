using System.Collections.Generic;

namespace GyroPlayer.Core.Interfaces
{
    public interface IMusicManager
    {
        IEnumerable<IArtist> GetAllArtists();

        IEnumerable<IAlbum> GetAlbums(IEnumerable<IArtist> artists);

        IEnumerable<ISong> GetSong(IEnumerable<IAlbum> album);

        void PlaySong();
    }
}