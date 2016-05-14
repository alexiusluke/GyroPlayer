using System.Collections.Generic;

namespace GyroPlayer.Core.Interfaces
{
    interface ISongSort
    {
        IEnumerable<ISong> Sort();
    }
}