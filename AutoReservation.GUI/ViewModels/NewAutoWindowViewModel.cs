using AutoReservation.Common.DataTransferObjects;
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
    public class NewAutoWindowViewModel
    {
        public delegate void NewAutoComplete(AutoDto auto);
        public event NewAutoComplete NewAutoCompleteEvent;

        private ICommand addAutoCommand;
        private NewAutoWindow view;

        public ICommand AddAutoCommand { get => this.addAutoCommand ?? (addAutoCommand = new RelayCommand(() => this.CommitNewAuto())); }


        public AutoDto Auto { get; }
        public AutoKlasse AutoKlasse { get => Auto.AutoKlasse; }

        public bool CanHaveBasistarif { get => this.Auto.AutoKlasse == AutoKlasse.Luxusklasse; }
        public NewAutoWindowViewModel(int newId)
        {
            this.Auto = new AutoDto()
            {
                Id = newId
            };
        }
        private void CommitNewAuto()
        {
            NewAutoCompleteEvent(this.Auto);
            this.view.Close();
        }
        public void ShowView()
        {
            view = new NewAutoWindow();
            view.DataContext = this;
            view.ShowDialog();
        }
    }
}
