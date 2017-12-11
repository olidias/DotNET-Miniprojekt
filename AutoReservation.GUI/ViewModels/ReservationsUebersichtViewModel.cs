using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoReservation.GUI.ViewModels
{
    public class ReservationsUebersichtViewModel
    {
        public List<Reservation> Reservations { get; set; }
        
        public ReservationsUebersichtViewModel()
        {
            Reservations = new List<Reservation>();
            Reservations.Add(new Reservation { ReservationsNr = 12 });
        }

    }
    public class Reservation
    {
        public int ReservationsNr { get; set; }
    }
}
