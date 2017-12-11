using AutoReservation.GUI.Commands;
using AutoReservation.GUI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AutoReservation.GUI.ViewModels
{
    public class ReservationenUebersichtViewModel
    {
        public List<Reservation> Reservations { get; set; }
        
        public ReservationenUebersichtViewModel()
        {
            Reservations = new List<Reservation>
            {
                new Reservation { ReservationsNr = 12 },
                new Reservation { ReservationsNr = 13 },
                new Reservation { ReservationsNr = 14 }
            };
        }

        ICommand newReservationCommand;
        public ICommand NewReservationCommand { get => newReservationCommand ?? (newReservationCommand = new RelayCommand(() => this.NewReservation())); }

        private void NewReservation()
        {
            new NewReservationWindow().ShowDialog();
        }
    }

}
public class Reservation
{
    private int kundenId;
    private string kundenName;
    private string automarke;
    private string autoId;

    public int ReservationsNr { get; set; }
    public string KundenName { get => "Petri Heil"; set => kundenName = value; }
    public int KundenId { get => 12; set => kundenId = value; }
    public DateTime Von { get => DateTime.Now.Date; }
    public DateTime Bis { get => DateTime.Now.AddDays(3).Date; }
    public string Automarke { get => "Aston Martin"; set => automarke = value; }
    public string AutoId { get => "101"; set => autoId = value; }

}
