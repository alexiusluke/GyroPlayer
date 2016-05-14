using System.Collections.Generic;

namespace GyroPlayer.Core.Interfaces
{
    public interface IAlbumSort
    {
        IEnumerable<IAlbum> Sort();
    }
}