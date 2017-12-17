using AutoReservation.Common.DataTransferObjects;
using Prism.Events;
using System.Collections.ObjectModel;

namespace AutoReservation.GUI.EventAggregatorEvents
{
    class AutoDataChangedEvent : PubSubEvent<ObservableCollection<AutoDto>>
    {
    }
}
