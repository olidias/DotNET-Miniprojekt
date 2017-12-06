using System;
using AutoReservation.Dal;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using AutoReservation.Dal.Entities;

namespace AutoReservation.BusinessLayer
{
    public class ReservationManager
        : ManagerBase
    {
        public List<Reservation> GetAllReservationen()
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                return context.Reservationen.ToList();
            }
        }

        public Reservation GetReservation(int reservationsNr)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                return (from r in context.Reservationen
                           where r.ReservationsNr == reservationsNr
                           select r).FirstOrDefault();
            }
        }

        public bool InsertReservation(Reservation res)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                CheckAvailability(context, res);
                try
                {
                    context.Reservationen.Add(res);
                    context.SaveChanges();
                    return true;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                    return false;
                }
            }
        }

        public bool UpdateReservation(Reservation res)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                var dbo = (from r in context.Reservationen
                           where r.ReservationsNr == res.ReservationsNr
                           select r).FirstOrDefault();

                CheckAvailability(context, res);

                if (dbo != null)
                {
                    dbo.Kunde = res.Kunde;
                    dbo.KundeId = res.KundeId;
                    dbo.Von = res.Von;
                    dbo.Bis = res.Bis;
                    dbo.Auto = res.Auto;
                    dbo.AutoId = res.AutoId;
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        private void CheckAvailability(AutoReservationContext context, Reservation res)
        {
            var conflictCandidates = (from r in context.Reservationen
                                      where r.AutoId == res.AutoId || r.KundeId == res.KundeId
                                      select r)
                                      .AsNoTracking()
                                      .ToList();

            foreach(Reservation dbRes in conflictCandidates)
            {
                if (IsCompletelyOverlapping(res, dbRes) || IsCompletelyOverlapping(dbRes,res))
                {
                    throw new Exceptions.OverlappingTimesException()
                    {
                        AutoId = res.AutoId,
                        KundenId = res.KundeId,
                        OverallpingState = Exceptions.OverlappingState.CompleteOverlap
                    };
                }
                else if(IsPartlyOverlapping(res, dbRes) || IsPartlyOverlapping(dbRes, res))
                {
                    throw new Exceptions.OverlappingTimesException()
                    {
                        AutoId = res.AutoId,
                        KundenId = res.KundeId,
                        OverallpingState = Exceptions.OverlappingState.PartialOverlap
                    };
                }
            }
        }

        private bool IsPartlyOverlapping(Reservation res1, Reservation res2)
        {
            return res1.Von < res2.Von && (res1.Bis > res2.Von && res1.Bis < res2.Bis);
        }

        private bool IsCompletelyOverlapping(Reservation res1, Reservation res2)
        {
            return res2.Von < res1.Von && res2.Bis > res1.Bis;
        }

        public bool RemoveReservation(Reservation res)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                var dbo = (from r in context.Reservationen
                           where r.ReservationsNr == res.ReservationsNr
                           select r).FirstOrDefault();
                if (dbo != null)
                {
                    context.Reservationen.Remove(dbo);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }

    }
}