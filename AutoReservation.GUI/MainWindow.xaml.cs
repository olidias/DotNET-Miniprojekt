using System.Collections.Generic;
using System.Windows;

namespace AutoReservation.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Reservation> Reservations { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }
    }

    public class Reservation
    {
        public int ReservationsNr { get; set; }
    }
}
