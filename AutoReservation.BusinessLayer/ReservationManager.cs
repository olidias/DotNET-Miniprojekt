﻿using System;
using AutoReservation.Dal;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using AutoReservation.Dal.Entities;
using AutoReservation.BusinessLayer.Exceptions;
using System.Collections;

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
                var res = (from r in context.Reservationen
                           where r.ReservationsNr == reservationsNr
                           select r)
                           .Include(r=>r.Auto)
                           .Include(r=>r.Kunde)
                           .FirstOrDefault();
                return (Reservation)res;
            }
        }

        public bool InsertReservation(Reservation target)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                CheckAvailability(context, target);
                CheckRange(target);

                try
                {
                    context.Reservationen.Add(target);
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

        public bool UpdateReservation(Reservation target)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                var dbo = (from r in context.Reservationen
                           where r.ReservationsNr == target.ReservationsNr
                           select r)
                           .Include(p=>p.Auto)
                           .Include(p=>p.Kunde)
                           .FirstOrDefault();

                CheckAvailability(context, target);
                CheckRange(target);

                if (dbo != null)
                {
                    if (StructuralComparisons.StructuralComparer.Compare(dbo.RowVersion, target.RowVersion)>0)
                    {
                        throw CreateOptimisticConcurrencyException(context, target);
                    }
                    dbo.Kunde = target.Kunde;
                    dbo.Von = target.Von;
                    dbo.Bis = target.Bis;
                    dbo.Auto = target.Auto;
                    context.Entry(dbo).State = EntityState.Modified;

                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        private void CheckRange(Reservation target)
        {
            if(target.Bis <= target.Von)
            {
                throw new InvalidDateRangeException();
            }
        }

        private void CheckAvailability(AutoReservationContext context, Reservation res)
        {
            var conflictCandidates = (from r in context.Reservationen
                                      where r.AutoId == res.AutoId
                                      where r.ReservationsNr != res.ReservationsNr
                                      select r)
                                      .AsNoTracking();

            foreach(Reservation dbRes in conflictCandidates)
            {
                if(CheckIntersection(dbRes, res))
                {
                    throw new OverlappingTimesException()
                    {
                        AutoId = dbRes.AutoId,
                        ReservationsNr = dbRes.ReservationsNr
                    };
                }
            }
        }
        private bool CheckIntersection(Reservation res1, Reservation res2)
        {
            return !(res1.Von >= res2.Bis || res2.Bis <= res1.Von);
        }

        public bool RemoveReservation(int resNr)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                var dbo = (from r in context.Reservationen
                           where r.ReservationsNr == resNr
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