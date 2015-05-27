using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MahApps.Metro.Controls;

namespace GW2Trader_Windows
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