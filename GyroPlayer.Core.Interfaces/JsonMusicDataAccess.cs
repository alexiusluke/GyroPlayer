using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using GyroPlayer.Core.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GyroPlayer.Core
{
    public class JsonMusicDataAccess : IMusicSourceDataAccess
    {
        private readonly string _jsonUri;              
        private JObject _musicLibraryJsonData;

        public JsonMusicDataAccess(string jsonUri)
        {
            _jsonUri = jsonUri;
        }

        public void BuildData()
        {           
            WebRequest request = WebRequest.Create(new Uri(_jsonUri));
            request.ContentType = "application/json; charset=utf-8";
            var response = (HttpWebResponse) request.GetResponse();
            var responseStream = response.GetResponseStream();
            if (responseStream != null)
            {
                _musicLibraryJsonData = JObject.Load(new JsonTextReader(new StreamReader(responseStream)));
            }                                 
        }

        public IEnumerable<IArtist> GetArtists()
        {
            return from artist in _musicLibraryJsonData["artists"]
                select new Artist(artist["name"].Value<string>());
        }

        public IEnumerable<IAlbum> GetAlbums(string queryArtistName)
        {
            return from artist in _musicLibraryJsonData["artists"]
                where artist["name"].Value<string>().Equals(queryArtistName)
                let albums = artist["albums"]
                from albumOfArtist in albums
                select new Album(artist["name"].Value<string>(), albumOfArtist["title"].Value<string>(),
                    albumOfArtist["image"].Value<string>(),
                    albumOfArtist["description"].Value<string>(),
                    albumOfArtist["date"].Value<string>());
        }

        public IEnumerable<ISong> GetSongs(string queryArtistName, string queryAlbumTitle)
        {
            return from artist in _musicLibraryJsonData["artists"]
                   where artist["name"].Value<string>().Equals(queryArtistName)
                   let albums = artist["albums"]
                   from albumOfArtist in albums
                   where albumOfArtist["title"].Value<string>().Equals(queryAlbumTitle)
                   from songOfAlbum in albumOfArtist["songs"]
                   select new Song(albumOfArtist["title"].Value<string>(), songOfAlbum["title"].Value<string>(),
                                            songOfAlbum["length"].Value<string>());
        }
    }
}
