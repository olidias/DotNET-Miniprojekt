using System;
using AutoReservation.Dal;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using AutoReservation.Dal.Entities;

namespace AutoReservation.BusinessLayer
{
    public class KundeManager
        : ManagerBase
    {


        public List<Kunde> GetAllKunden()
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                return context.Kunden.ToList();
            }
        }
        public Kunde GetKunde(int kundeId)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                var dbo = from k in context.Kunden
                          where k.Id == kundeId
                          select k;
                return dbo.FirstOrDefault();
            }
        }
        public bool InsertKunde(Kunde kunde)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                try
                {
                    context.Kunden.Add(kunde);
                    context.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return false;
                }
            }
        }

        public bool UpdateKunde(Kunde target)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                var dbo = (from k in context.Kunden
                           where k.Id == target.Id
                           select k).FirstOrDefault();
                if (dbo != null)
                {
                    dbo.Geburtsdatum = target.Geburtsdatum;
                    dbo.Nachname = target.Nachname;
                    dbo.Vorname = target.Vorname;
                    dbo.Reservationen = target.Reservationen;
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool RemoveKunde(Kunde target)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                var dbo = (from k in context.Kunden
                           where k.Id == target.Id
                           select k).FirstOrDefault();
                if (dbo != null)
                {
                    context.Kunden.Remove(dbo);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }
    }

}