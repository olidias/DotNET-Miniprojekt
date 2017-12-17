using AutoReservation.Common.DataTransferObjects;
using AutoReservation.GUI.EventAggregatorEvents;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoReservation.GUI.ViewModels
{
    class StammdatenViewModel 
    {
        private IEventAggregator eventAggregator;

        public StammdatenViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
        }

        private ObservableCollection<KundeDto> kundenCollection;
        public ObservableCollection<KundeDto> KundenCollection { get => kundenCollection; set
            {
                if (value != kundenCollection)
                {
                    kundenCollection = value;
                    eventAggregator.GetEvent<KundenDataChangedEvent>().Publish(kundenCollection);
                }
            }
        }
        private ObservableCollection<AutoDto> autoCollection;
        public ObservableCollection<AutoDto> AutoCollection { get => autoCollection; set
            {
                if (value != autoCollection)
                {
                    autoCollection = value;
                    eventAggregator.GetEvent<AutoDataChangedEvent>().Publish(autoCollection);

                }
            }

        }
    }
}
