using AutoReservation.Common.DataTransferObjects;
using AutoReservation.GUI.Commands;
using AutoReservation.GUI.Views;
using System.Windows.Input;
using System;

namespace AutoReservation.GUI.ViewModels
{
    public class NewKundeWindowViewModel
    {
        private NewKundeWindow view;
        public KundeDto Kunde { get; set; }


        public delegate void NewKundeComplete(KundeDto newKunde);
        public event NewKundeComplete NewKundeCompleteEvent;
        private ICommand addKundeCommand;
        public ICommand AddKundeCommand { get => addKundeCommand ?? (addKundeCommand = new RelayCommand(() => this.AddKundeComplete())); }

        private void AddKundeComplete()
        {
            NewKundeCompleteEvent(this.Kunde);
            this.view.Close();
        }

        public NewKundeWindowViewModel(int newId)
        {
            this.Kunde = new KundeDto() { Id = newId };
        }

        public void ShowView()
        {
            this.view = new NewKundeWindow();
            this.view.DataContext = this;
            this.view.ShowDialog();
        }
    }
}
