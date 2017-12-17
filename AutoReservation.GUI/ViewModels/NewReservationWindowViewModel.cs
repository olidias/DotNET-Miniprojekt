using AutoReservation.Common.DataTransferObjects;
using AutoReservation.GUI.Commands;
using AutoReservation.GUI.DisplayClasses;
using AutoReservation.GUI.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AutoReservation.GUI.ViewModels
{

    public class NewReservationWindowViewModel
    {
        public delegate void NewReservationComplete(ReservationDto reservation);
        public event NewReservationComplete NewReservationCompleteEvent;

        public KundeDto Kunde { get; set; }
        public ObservableCollection<KundeDto> KundenCollection { get; set; }

        public AutoDto Auto { get; set; }
        public ObservableCollection<AutoDto> AutoCollection{get;set;}

        private DateTime von = DateTime.Now;
        public DateTime Von { get => von; set => von = value; }
        private DateTime bis = DateTime.Now.AddDays(2);
        public DateTime Bis { get =>bis;set { bis = value; } }
        private ICommand reserveCommand;

        public ICommand ReserveCommand { get => reserveCommand ?? (reserveCommand = new RelayCommand(() => this.ReserveComplete())); }

        private NewReservationWindow view;

        public NewReservationWindowViewModel(ObservableCollection<KundeDto> kunden, ObservableCollection<AutoDto> autos)
        {
            this.KundenCollection = kunden;
            this.AutoCollection = autos;
        }

       

        private void ReserveComplete()
        {
            NewReservationCompleteEvent(new ReservationDto()
            {
                Auto = this.Auto,
                Kunde = this.Kunde,
                Von = this.Von,
                Bis = this.Bis
            });

            view.Close();
        }

        public void ShowView()
        {
            view = new NewReservationWindow();
            view.DataContext = this;
            view.ShowDialog();
        }


    }

    

   
}
