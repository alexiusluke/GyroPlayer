using System;

namespace GyroPlayer.Core.Interfaces
{
    public interface ISong
    {
        string Title { get; }

        TimeSpan Length { get; }

        bool IsFavourite { get; }
    }
}