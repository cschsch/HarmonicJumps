using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonicJumps;

namespace GUI.Model
{
    public class MainModel
    {
        public Track NowPlaying { get; set; }
        public Track[] Tracks { get; set; }
        public Track[] FilteredTracks { get; set; }

        public MainModel(Track[] tracks)
        {
            Tracks = tracks;
        }
    }

}
