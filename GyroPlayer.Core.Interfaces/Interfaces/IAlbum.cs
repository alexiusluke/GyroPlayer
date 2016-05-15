using System;

namespace GyroPlayer.Core.Interfaces
{
    public interface IAlbum :IEquatable<IAlbum>
    {        
        string Title { get; }

        Uri ImageUrl { get; }

        string Description { get; }

        DateTime Date { get; }

        string ArtistName { get; }
    }
}