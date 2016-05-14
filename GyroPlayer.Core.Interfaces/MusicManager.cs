using System;
using System.Collections.Generic;
using GyroPlayer.Core.Interfaces;

namespace GyroPlayer.Core
{
    class MusicManager : IMusicManager
    {
        public MusicManager(IMusicSourceDataAccess musicDataAccess, ISongPlayManager songPlayManager)
        {
                            
        }

        public IEnumerable<IArtist> GetAllArtists()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IAlbum> GetAlbums(IEnumerable<IArtist> artists)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ISong> GetSong(IEnumerable<IAlbum> album)
        {
            throw new NotImplementedException();
        }

        public void PlaySong()
        {
            throw new NotImplementedException();
        }
    }
}