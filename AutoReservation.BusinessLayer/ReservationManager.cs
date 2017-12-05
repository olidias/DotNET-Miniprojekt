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