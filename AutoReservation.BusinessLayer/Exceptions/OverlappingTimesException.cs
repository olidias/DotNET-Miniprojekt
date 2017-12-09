using System;

namespace AutoReservation.BusinessLayer.Exceptions
{
    public class OverlappingTimesException : Exception
    {
        public OverlappingState OverallpingState { get; set; }
        public int KundenId { get; set; }
        public int AutoId { get; set;}
        public int ReservationsNr { get; set; }
    }
    public enum OverlappingState
    {
        CompleteOverlap, PartialOverlap
    }
}
