using System;
using System.IO;
using MahApps.Metro.Controls;

namespace GW2Trader.Desktop.View
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "GW2Trader");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            AppDomain.CurrentDomain.SetData("DataDirectory", path);

            InitializeComponent();
        }
    }
}