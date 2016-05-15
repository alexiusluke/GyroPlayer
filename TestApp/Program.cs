using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GyroPlayer.Core;
using GyroPlayer.Core.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime startTime = DateTime.Now;
            string _jsonUri =
                "https://gist.githubusercontent.com/edj-boston/77b2cdc0cad5b5d42219/raw/1366c213a5b0ae29f1d29d0bc1d22d29f2586068/music.json";
            WebRequest request = WebRequest.Create(new Uri(_jsonUri));
            request.ContentType = "application/json; charset=utf-8";
            var response = (HttpWebResponse)request.GetResponse();
            var responseStream = response.GetResponseStream();
            Console.WriteLine("Getting Web Response :" + (DateTime.Now - startTime));
            if (responseStream != null)
            {
                startTime = DateTime.Now;
                var jObject = JObject.Load(new JsonTextReader(new StreamReader(responseStream)));
                Console.WriteLine("Loading Json :" + (DateTime.Now - startTime));
                startTime = DateTime.Now;               
                var artists = from artist in jObject["artists"]
                               select new Artist(artist["name"].Value<string>());
                foreach (var artist in artists)
                {
                    var artistName = artist.Name;
                }
                Console.WriteLine("Artists Only :" + (DateTime.Now - startTime));
                startTime = DateTime.Now;
                var musicLibrary = from artist in jObject["artists"]
                    let artistname = artist["name"].Value<string>()
                    let albums = artist["albums"]
                                   select
                                       new KeyValuePair<IArtist, IEnumerable<KeyValuePair<IAlbum, IEnumerable<ISong>>>>(
                                           new Artist(artistname),
                                           from albumOfArtist in albums
                                           let songsOfAlbum = albumOfArtist["songs"]
                                           select
                                               new KeyValuePair<IAlbum, IEnumerable<ISong>>(
                                                   new Album(albumOfArtist["title"].Value<string>(),
                                                       albumOfArtist["image"].Value<string>(),
                                                       albumOfArtist["description"].Value<string>(),
                                                       albumOfArtist["date"].Value<string>()),
                                                   from songOfAlbum in songsOfAlbum
                                                   select
                                                       new Song(songOfAlbum["title"].Value<string>(),
                                                           songOfAlbum["length"].Value<string>())));

                foreach (var artist in musicLibrary)
                {
                    foreach (var album in artist.Value)
                    {
                        foreach (var song in album.Value)
                        {
                            var songName = song.Title;
                        }
                    }
                }
                Console.WriteLine("Complete Library :" + (DateTime.Now - startTime));
                Console.ReadLine();
            }
        }
    }

    internal class Song : ISong
    {
        public Song(string title, string length)
        {
            Title = title;
            TimeSpan parsedLength;
            if (TimeSpan.TryParse(length, out parsedLength))
            {
                Length = parsedLength;
            }            
        }

        public void SetFavourite()
        {
            IsFavourite = true;
        }

        public string Title { get; private set; }
        public TimeSpan Length { get; private set; }
        public bool IsFavourite { get; private set; }
    }

    internal class Album : IAlbum
    {
        public Album(string title, string imageUrl, string description)
        {
            Title = title;
            ImageUrl = new Uri(imageUrl);
            Description = description;
        }
        public Album(string title, string imageUrl, string description, string dateString)
        {
            Title = title;
            ImageUrl = new Uri(imageUrl);
            Description = description;
            DateTime parsedDate;
            if (DateTime.TryParse(dateString, out parsedDate))
            {
                Date = parsedDate;
            }
        }

        public string Title { get; private set; }
        public Uri ImageUrl { get; private set; }
        public string Description { get; private set; }
        public DateTime Date { get; private set; }       
    }
}
