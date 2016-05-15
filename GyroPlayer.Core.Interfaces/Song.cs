using System;
using GyroPlayer.Core.Interfaces;

namespace GyroPlayer.Core
{
    internal class Song : ISong
    {
        public Song(string albumName, string title, string length)
        {
            AlbumName = albumName;
            Title = title;
            TimeSpan parsedLength;
            if (TimeSpan.TryParse(length, out parsedLength))
            {
                Length = parsedLength;
            }
        }

        public string AlbumName { get; private set; }

        public string Title { get; private set; }
        public TimeSpan Length { get; private set; }

        public bool Equals(ISong other)
        {
            return other != null && 
                !string.IsNullOrEmpty(AlbumName) && !string.IsNullOrEmpty(other.AlbumName) && AlbumName.Equals(other.AlbumName) &&
                !string.IsNullOrEmpty(Title) && !string.IsNullOrEmpty(other.Title) && Title.Equals(other.Title) &&
                Length.Equals(other.Length);
        }
    }
}