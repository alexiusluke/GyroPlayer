using System;
using GyroPlayer.Core.Interfaces;

namespace GyroPlayer.Core
{
    internal class Album : IAlbum
    {
        public Album(string title, string imageUrl, string description)
        {
            Title = title;
            ImageUrl = new Uri(imageUrl);
            Description = description;
        }

        public Album(string artist, string title, string imageUrl, string description, string dateString)
        {
            ArtistName = artist;
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

        public string ArtistName { get; private set; }

        public bool Equals(IAlbum other)
        {
            return other != null &&
                !string.IsNullOrEmpty(ArtistName) && !string.IsNullOrEmpty(other.ArtistName) && ArtistName.Equals(other.ArtistName) &&
                !string.IsNullOrEmpty(Title) && !string.IsNullOrEmpty(other.Title) && Title.Equals(other.Title) &&
                !string.IsNullOrEmpty(Description) && !string.IsNullOrEmpty(other.Description) && Description.Equals(other.Description) &&
                ImageUrl != null && other.ImageUrl != null &&
                ImageUrl.Equals(other.ImageUrl) &&
                Date.Equals(other.Date);
        }       
    }
}