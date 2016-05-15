using System.Collections.Generic;

namespace GyroPlayer.Core.Interfaces
{
    public interface IAlbumSort
    {
        string SortDisplayString { get; }

        IEnumerable<IAlbum> Sort(IEnumerable<IAlbum> album);
    }

    public class Sort<T>
    {
        
    }
}