using GyroPlayer.Core.Interfaces;

namespace GyroPlayer.Core
{
    public interface ISongPlayManager
    {
        void AddListenCount(ISong song);

        void AddToFavourite(ISong song);

        int GetListenCount(ISong song);
    }
}