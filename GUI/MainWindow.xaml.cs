using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using Database;
using GUI.Model;
using Microsoft.Extensions.Configuration;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Configuration Configuration { get; set; }
        public MainModel Model { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddCommandLine(Environment.GetCommandLineArgs())
                .Build()
                .Get<Configuration>();
        }
    }
}
