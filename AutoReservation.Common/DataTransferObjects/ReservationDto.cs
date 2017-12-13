﻿using System;
using System.Runtime.Serialization;

namespace AutoReservation.Common.DataTransferObjects
{
    [DataContract]
    public class ReservationDto
    {
        public int ReservationsNr { get; set; }
        public AutoDto Auto { get; set; }
        public KundeDto Kunde{ get; set; }
        public DateTime Von { get; set; }
        public DateTime Bis { get; set; }

        public byte[] RowVersion { get; set; }
        public override string ToString()
            => $"{ReservationsNr}; {Von}; {Bis}; {Auto}; {Kunde}";
    }
}
