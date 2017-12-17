using AutoReservation.Common.DataTransferObjects;
using AutoReservation.GUI.Commands;
using AutoReservation.GUI.EventAggregatorEvents;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Input;

namespace AutoReservation.GUI.ViewModels
{
    public class ReservationenUebersichtViewModel
    {
        private ObservableCollection<ReservationDto> reservations;
        private CollectionViewSource reservationsCollection;
        public ICollectionView Reservations { get=> this.reservationsCollection.View; }

        public ReservationDto SelectedReservation { get; set; }

        private bool showOnlyCurrentReservations;
        public bool ShowOnlyCurrentReservations { get=>showOnlyCurrentReservations;
            set
            {
                showOnlyCurrentReservations = value;
                this.reservationsCollection.View.Refresh();
            }
        }

        private IEventAggregator eventAggregator;

        ICommand newReservationCommand;
        public ICommand NewReservationCommand { get => newReservationCommand ?? (newReservationCommand = new RelayCommand(() => this.NewReservationDialog())); }

        private ICommand deleteReservationCommand;

        public ICommand DeleteReservationCommand { get => deleteReservationCommand ?? (deleteReservationCommand = new RelayCommand(() => this.DeleteReservation())); }


        public ObservableCollection<KundeDto> Kunden { get; private set; }
        public ObservableCollection<AutoDto> Autos { get; private set; }

        public ReservationenUebersichtViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;

            this.eventAggregator.GetEvent<KundenDataChangedEvent>().Subscribe(this.KundenChanged);
            this.eventAggregator.GetEvent<AutoDataChangedEvent>().Subscribe(this.AutosChanged);
            InitTestData();
            this.reservationsCollection = new CollectionViewSource();
            this.reservationsCollection.Source = reservations;
            this.reservationsCollection.Filter += ShowCurrentReservations_Filter;
        }

        private void ShowCurrentReservations_Filter(object sender, FilterEventArgs e)
        {
            if (!showOnlyCurrentReservations)
            {
                e.Accepted = true;
                return;
            }
            var res = e.Item as ReservationDto;

            if (res.Von.Date <= DateTime.Now.Date && res.Bis.Date >= DateTime.Now.Date)
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = false;
            }
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

        private void DeleteReservation()
        {
            if (this.SelectedReservation == null)
                return;
            DialogResult dialogResult = MessageBox.Show("Wollen Sie den ausgewählten Eintrag wirklich löschen?", "Eintrag Löschen?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                this.reservations.Remove(this.SelectedReservation);
            }

        }
        private void ResWindow_NewReservationCompleteEvent(ReservationDto reservation)
        {
            var resNr = this.reservations.Max(r=>r.ReservationsNr)+1;
            reservation.ReservationsNr = resNr;

            this.reservations.Add(reservation);
        }

        private void InitTestData()
        {
            var kunde1 = new KundeDto() { Nachname = "Hustler", Vorname = "Heiri" };
            var kunde2 = new KundeDto() { Nachname = "Holter", Vorname = "Honty" };
            var kunde3 = new KundeDto() { Nachname = "High", Vorname = "Helfy" };
            reservations = new ObservableCollection<ReservationDto>
            {
                new ReservationDto { ReservationsNr = 12, Kunde=kunde1, Auto = new AutoDto(){Marke="Aston Martin" },Von=DateTime.Now.Date,Bis=DateTime.Now.AddDays(3).Date },
                new ReservationDto { ReservationsNr = 13, Kunde=kunde2, Auto = new AutoDto(){Marke="Fiat Punto" },Von=DateTime.Now.Date,Bis=DateTime.Now.AddDays(4).Date },
                new ReservationDto { ReservationsNr = 14, Kunde=kunde3, Auto = new AutoDto(){Marke="Maserati" },Von=DateTime.Now.AddDays(-5),Bis=DateTime.Now.AddDays(3).Date },
            };
        }
    }

  
}
