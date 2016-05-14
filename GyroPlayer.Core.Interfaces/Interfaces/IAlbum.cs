using System;

namespace GyroPlayer.Core.Interfaces
{
    public interface IAlbum
    {        
        string Title { get; }

        string ImageUrl { get; }

        string Description { get; }

        DateTime Date { get; }
    }
}