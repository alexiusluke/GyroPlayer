using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GyroPlayer.Core;

namespace GyroPlayer.ViewModel
{
    public class GyroMusicPlayerViewModel : INotifyPropertyChanged
    {
        public GyroMusicPlayerViewModel()
        {
            var jsonUri = "https://gist.githubusercontent.com/edj-boston/77b2cdc0cad5b5d42219/raw/1366c213a5b0ae29f1d29d0bc1d22d29f2586068/music.json";
            var jasonDataAccess = new JsonMusicDataAccess(jsonUri);
            jasonDataAccess.BuildData();
            var musicManager = new MusicManager(jasonDataAccess, null);
            var musicPlayerSelectionController = new MusicPlayerSelectionController();
            ArtistsPaneVm = new ArtistListViewModel(musicManager, musicPlayerSelectionController);
            AlbumsPaneVm = new AlbumListViewModel(musicManager, musicPlayerSelectionController);
            SongPaneVm = new SongListViewModel(musicManager, musicPlayerSelectionController);
        }

        public ArtistListViewModel ArtistsPaneVm { get; set; }

        public AlbumListViewModel AlbumsPaneVm { get; set; }

        public SongListViewModel SongPaneVm { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
