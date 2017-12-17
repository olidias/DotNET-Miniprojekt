using AutoReservation.Common.DataTransferObjects;
using AutoReservation.GUI.Commands;
using AutoReservation.GUI.EventAggregatorEvents;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;

namespace AutoReservation.GUI.ViewModels
{
    class StammdatenViewModel
    {
        private ObservableCollection<KundeDto> kundenCollection;
        public ObservableCollection<KundeDto> KundenCollection
        {
            get => kundenCollection; set
            {
                if (value != kundenCollection)
                {
                    kundenCollection = value;
                    eventAggregator.GetEvent<KundenDataChangedEvent>().Publish(kundenCollection);
                }
            }
        }
        public KundeDto SelectedKunde { get; set; }
        private ObservableCollection<AutoDto> autoCollection;
        public ObservableCollection<AutoDto> AutoCollection
        {
            get => autoCollection; set
            {
                if (value != autoCollection)
                {
                    autoCollection = value;
                    eventAggregator.GetEvent<AutoDataChangedEvent>().Publish(autoCollection);

                }
            }

        }
        public AutoDto SelectedAuto { get; set; }
        private ICommand addKundeCommand;
        public ICommand AddKundeCommand { get=> addKundeCommand ?? (addKundeCommand = new RelayCommand(() => this.NewKunde())); }
        private ICommand deleteKundeCommand;
        public ICommand DeleteKundeCommand { get => deleteKundeCommand ?? (deleteKundeCommand = new RelayCommand(() => this.DeleteKunde()));}

        private ICommand addAutoCommand;
        public ICommand AddAutoCommand { get => addAutoCommand ?? (addAutoCommand = new RelayCommand(() => this.NewAuto())); }
        private ICommand deleteAutoCommand;
        public ICommand DeleteAutoCommand { get => deleteAutoCommand ?? (deleteAutoCommand = new RelayCommand(() => this.DeleteAuto())); }


        private IEventAggregator eventAggregator;

        public StammdatenViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.kundenCollection = new ObservableCollection<KundeDto>();
            this.autoCollection = new ObservableCollection<AutoDto>();
        }
        private void NewKunde()
        {
            int newId = 0;
            if(this.kundenCollection.Count>0)
                newId = this.kundenCollection.Max(k => k.Id) + 1;
            var newKundeWindow = new NewKundeWindowViewModel(newId);
            newKundeWindow.NewKundeCompleteEvent += NewKundeComplete;
            newKundeWindow.ShowView();
        }
        private void DeleteKunde()
        {
            if (this.SelectedKunde == null) return;
            DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Wollen Sie den ausgewählten Eintrag wirklich löschen?", "Eintrag Löschen?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                this.KundenCollection.Remove(this.SelectedKunde);
            }
        }
        private void NewAuto()
        {
            int newId = 0;
            if (this.autoCollection.Count > 0)
                newId = this.autoCollection.Max(k => k.Id) + 1;
            var newAutoWindow = new NewAutoWindowViewModel(newId);
            newAutoWindow.NewAutoCompleteEvent += NewAutoComplete;
            newAutoWindow.ShowView();
        }

        private void DeleteAuto()
        {
            if (SelectedAuto == null)
                return;
            DialogResult dialogResult = MessageBox.Show("Wollen Sie den ausgewählten Eintrag wirklich löschen?", "Eintrag Löschen?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                this.AutoCollection.Remove(this.SelectedAuto);
            }
        }
        private void NewAutoComplete(AutoDto newAuto)
        {
            this.autoCollection.Add(newAuto);
            eventAggregator.GetEvent<AutoDataChangedEvent>().Publish(autoCollection);
        }

        private void NewKundeComplete(KundeDto newKunde)
        {
            this.kundenCollection.Add(newKunde);
            eventAggregator.GetEvent<KundenDataChangedEvent>().Publish(kundenCollection);
        }
    }
}
