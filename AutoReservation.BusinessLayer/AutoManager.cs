    using System;
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
    public class AutoManager
        : ManagerBase
    {
        public List<Auto> GetAllAutos()
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                return context.Autos.ToList();
            }
        }
        public Auto GetAuto(int autoId)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                var dbo = from a in context.Autos
                          //.Include(auto => auto.Reservationen) // Eager to guarantee full access to reservations too
                          where a.Id == autoId
                          select a;
                return dbo.FirstOrDefault();
            }
        }
        public bool InsertAuto(Auto auto)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                try
                {
                    context.Autos.Add(auto);
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

        public bool UpdateAuto(Auto target)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                var dbo = (from a in context.Autos
                           where a.Id == target.Id
                           select a).FirstOrDefault();
                if (dbo != null)
                {
                    if (StructuralComparisons.StructuralComparer.Compare(dbo.RowVersion, target.RowVersion) > 0)
                    {
                        throw CreateOptimisticConcurrencyException(context, target);
                    }
                    dbo.Marke = target.Marke;
                    dbo.Reservationen = target.Reservationen;
                    dbo.Tagestarif = target.Tagestarif;
                    if (dbo is LuxusklasseAuto && target is LuxusklasseAuto)
                    {
                        ((LuxusklasseAuto)dbo).Basistarif = ((LuxusklasseAuto)target).Basistarif;
                    }
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public bool RemoveAuto(Auto target)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                var dbo = (from a in context.Autos
                           where a.Id == target.Id
                           select a).FirstOrDefault();
                if (dbo != null) {
                    context.Autos.Remove(dbo);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }
    }
}