using AutoReservation.Common.DataTransferObjects;
using AutoReservation.GUI.Commands;
using AutoReservation.GUI.EventAggregatorEvents;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static AutoReservation.GUI.ViewModels.NewAutoWindowViewModel;

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
        private ICommand addKundeCommand;
        public ICommand AddKundeCommand { get=> addKundeCommand ?? (addKundeCommand = new RelayCommand(() => this.NewKunde())); }

        private ICommand addAutoCommand;
        public ICommand AddAutoCommand { get => addAutoCommand ?? (addAutoCommand = new RelayCommand(() => this.NewAuto())); }

 
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
        private void NewAuto()
        {
            int newId = 0;
            if (this.autoCollection.Count > 0)
                newId = this.autoCollection.Max(k => k.Id) + 1;
            var newAutoWindow = new NewAutoWindowViewModel(newId);
            newAutoWindow.NewAutoCompleteEvent += NewAutoComplete;
            newAutoWindow.ShowView();

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
