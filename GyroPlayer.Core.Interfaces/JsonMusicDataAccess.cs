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
        private IEnumerable<Artist> _artists;

        public JsonMusicDataAccess(string jsonUri)
        {
            _jsonUri = jsonUri;
        }

        public void BuildDataAccess()
        {
            WebRequest request = WebRequest.Create(new Uri(_jsonUri));
            request.ContentType = "application/json; charset=utf-8";
            var response = (HttpWebResponse)request.GetResponse();
            var responseStream = response.GetResponseStream();
            if (responseStream != null)
            {
                var jObject = JObject.Load(new JsonTextReader(new StreamReader(responseStream)));
                _artists = from artistName in jObject["artists"].Values<string>()
                select new Artist(artistName);                
            }
        }
    }

    public class Artist : IArtist
    {
        public Artist(string artistName)
        {
            Name = artistName;
        }

        public string Name { get; private set; }
    }
}
