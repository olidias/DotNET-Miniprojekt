using AutoReservation.Common.DataTransferObjects;
using AutoReservation.GUI.Commands;
using AutoReservation.GUI.DisplayClasses;
using AutoReservation.GUI.EventAggregatorEvents;
using AutoReservation.GUI.Views;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AutoReservation.GUI.ViewModels
{
    public class ReservationenUebersichtViewModel
    {
        public ObservableCollection<ReservationDto> Reservations { get; set; }
        private IEventAggregator eventAggregator;

        ICommand newReservationCommand;
        public ICommand NewReservationCommand { get => newReservationCommand ?? (newReservationCommand = new RelayCommand(() => this.NewReservationDialog())); }
        public ObservableCollection<KundeDto> Kunden { get; private set; }
        public ObservableCollection<AutoDto> Autos { get; private set; }

        public ReservationenUebersichtViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;

            this.eventAggregator.GetEvent<KundenDataChangedEvent>().Subscribe(this.KundenChanged);
            this.eventAggregator.GetEvent<AutoDataChangedEvent>().Subscribe(this.AutosChanged);
            InitTestData();

        }

        private void AutosChanged(ObservableCollection<AutoDto> newAutoCollection)
        {
            this.Autos = newAutoCollection;
        }

        private void KundenChanged(ObservableCollection<KundeDto> newKundenCollection)
        {
            this.Kunden = newKundenCollection;
        }

        private void NewReservationDialog()
        {
            var resWindow = new NewReservationWindowViewModel(this.Kunden, this.Autos);
            resWindow.NewReservationCompleteEvent += ResWindow_NewReservationCompleteEvent;
            resWindow.ShowView();
        }

        private void ResWindow_NewReservationCompleteEvent(ReservationDto reservation)
        {
            var resNr = this.Reservations.Max(r=>r.ReservationsNr)+1;
            reservation.ReservationsNr = resNr;

            this.Reservations.Add(reservation);
        }

        private void InitTestData()
        {
            var kunde1 = new KundeDto() { Nachname = "Hustler", Vorname = "Heiri" };
            var kunde2 = new KundeDto() { Nachname = "Holter", Vorname = "Honty" };
            var kunde3 = new KundeDto() { Nachname = "High", Vorname = "Helfy" };
            Reservations = new ObservableCollection<ReservationDto>
            {
                new ReservationDto { ReservationsNr = 12, Kunde=kunde1, Auto = new AutoDto(){Marke="Aston Martin" },Von=DateTime.Now.Date,Bis=DateTime.Now.AddDays(3).Date },
                new ReservationDto { ReservationsNr = 13, Kunde=kunde2, Auto = new AutoDto(){Marke="Fiat Punto" },Von=DateTime.Now.Date,Bis=DateTime.Now.AddDays(4).Date },
                new ReservationDto { ReservationsNr = 14, Kunde=kunde3, Auto = new AutoDto(){Marke="Maserati" },Von=DateTime.Now.AddDays(-5),Bis=DateTime.Now.AddDays(3).Date },
            };
        }
    }

  
}
