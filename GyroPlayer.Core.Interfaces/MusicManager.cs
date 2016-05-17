using System;
using System.Collections.Generic;
using System.Linq;
using GyroPlayer.Core.Interfaces;

namespace GyroPlayer.Core
{
    public class MusicManager : IMusicManager
    {
        private readonly IMusicSourceDataAccess _musicDataAccess;
        private IEnumerable<IArtist> _artists;
        private readonly IDictionary<IArtist, IEnumerable<IAlbum>> _artistAlbums = new Dictionary<IArtist, IEnumerable<IAlbum>>();
        private readonly IDictionary<IAlbum, IEnumerable<ISong>> _albumSongs = new Dictionary<IAlbum, IEnumerable<ISong>>();

        public MusicManager(IMusicSourceDataAccess musicDataAccess, ISongPlayManager songPlayManager)
        {
            _musicDataAccess = musicDataAccess;
        }

        public IEnumerable<IArtist> GetAllArtists()
        {
            if (_artists == null)
            {
                _artists = _musicDataAccess.GetArtists();
            }
            return _artists;
        }

        public IEnumerable<IAlbum> GetAlbums(IEnumerable<IArtist> artists)
        {
            return artists.SelectMany(GetAlbum);
        }

        private IEnumerable<IAlbum> GetAlbum(IArtist artist)
        {
            if (!_artistAlbums.ContainsKey(artist))
            {
                var albums = _musicDataAccess.GetAlbums(artist.Name);
                _artistAlbums.Add(artist, albums);
            }
            return _artistAlbums[artist];
        }

        public IEnumerable<ISong> GetSong(IEnumerable<IAlbum> albums)
        {
            return albums.SelectMany(GetSongs);
        }

        private IEnumerable<ISong> GetSongs(IAlbum album)
        {
            if (!_albumSongs.ContainsKey(album))
            {
                var songs = _musicDataAccess.GetSongs(album.ArtistName, album.Title);
                _albumSongs.Add(album, songs);
            }
            return _albumSongs[album];
        }

        public void PlaySong()
        {
            throw new NotImplementedException();
        }
    }
}