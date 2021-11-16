using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Database;
using GUI.Model;
using HarmonicJumps;
using Microsoft.Extensions.Configuration;
using SQLite;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Configuration Configuration { get; set; }
        public MainModel Model 
        {
            get => DataContext as MainModel;
            set => DataContext = value;
        }
        public TrackFinder TrackFinder { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddCommandLine(Environment.GetCommandLineArgs())
                .Build()
                .Get<Configuration>();
            Model = new MainModel();

            var harmonizer = new Harmonizer(Configuration.HarmonizerDepth);
            var sharePath = Path.Combine(Path.GetDirectoryName(Configuration.DatabasePath), "share");
            var rekordboxDatabase = new RekordboxDatabase(Configuration.DatabasePath);
            using var db = new SQLiteConnection(rekordboxDatabase.ConnectionString);
            var tracks = db.Table<Content>().AsParallel().Select(c => Track.FromID(db, c.ID, sharePath)).ToArray();

            TrackFinder = new TrackFinder(harmonizer, tracks);
            Model.NowPlaying = tracks.First();
            Model.FilteredTracks = tracks.Where(track => track.BPM == 125).ToArray();
        }

        private void GeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            var propertyDescriptor = (PropertyDescriptor) e.PropertyDescriptor;
            if (!propertyDescriptor.IsBrowsable) e.Cancel = true;
            if (e.PropertyType == typeof(DateTime) || e.PropertyType == typeof(DateTime?)) (e.Column as DataGridTextColumn).Binding.StringFormat = "dd.MM.yyyy";
            e.Column.Header = propertyDescriptor.DisplayName;
        }
    }
}
