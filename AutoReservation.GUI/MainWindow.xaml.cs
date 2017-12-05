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
            Reservations = new List<Reservation>();
            Reservations.Add(new Reservation { ReservationsNr = 12 });
            reservationsDatagrid.DataContext = this;
            reservationListView.DataContext = this;
        }
    }

    public class Reservation
    {
        public int ReservationsNr { get; set; }
    }
}
