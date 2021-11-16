using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using HarmonicJumps;

namespace GUI.Model
{
    public class MainModel : INotifyPropertyChanged
    {
        private Track _nowPlaying;
        public Track NowPlaying 
        {
            get => _nowPlaying;
            set { if (_nowPlaying == value) { return; } _nowPlaying = value; NotifyPropertyChanged(); }
        }

        private Track[] _filteredTracks;
        public Track[] FilteredTracks
        {
            get => _filteredTracks;
            set { if (_filteredTracks == value) { return; } _filteredTracks = value; NotifyPropertyChanged(); }
        }

        private Track _selectedTrack;
        public Track SelectedTrack
        {
            get => _selectedTrack;
            set { if (_selectedTrack == value) { return; } _selectedTrack = value; NotifyPropertyChanged(); }
        }

        public MainModel()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
