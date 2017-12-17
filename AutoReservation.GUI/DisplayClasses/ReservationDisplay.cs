using AutoReservation.Common.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoReservation.GUI.DisplayClasses
{
    public class ReservationDisplay : ReservationDto
    {
        public string Kundenname { get => $"{this.Kunde.Vorname} {this.Kunde.Nachname}"; }
    }
}
