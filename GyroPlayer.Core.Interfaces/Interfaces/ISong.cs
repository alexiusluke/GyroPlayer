using System;

namespace GyroPlayer.Core.Interfaces
{
    public interface ISong : IEquatable<ISong>
    {
        string AlbumName { get; }

        string Title { get; }

        TimeSpan Length { get; }
    }
}