using AutoReservation.GUI.Views;
using Prism.Regions;
using System.Windows;

namespace AutoReservation.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(IRegionManager regionManager)
        {
            InitializeComponent();

            regionManager.RegisterViewWithRegion("ContentRegion", typeof(MainContent));
        }
    }

}
