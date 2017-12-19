using AutoReservation.Common.Interfaces;
using System;
using System.Diagnostics;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.DataTransferObjects.Faults;
using System.Collections.Generic;
using AutoReservation.BusinessLayer;
using System.ServiceModel;
using AutoReservation.BusinessLayer.Exceptions;

namespace AutoReservation.Service.Wcf
{
    public class AutoReservationService : IAutoReservationService
    {
        private ReservationManager reservationManager;
        private KundeManager kundeManager;
        private AutoManager autoManager;

        public AutoReservationService()
        {
            this.reservationManager = new ReservationManager();
            this.kundeManager = new KundeManager();
            this.autoManager = new AutoManager();
        }

        private static void WriteActualMethod() 
            => Console.WriteLine($"Calling: {new StackTrace().GetFrame(1).GetMethod().Name}");

        public void DeleteAuto(int autoId)
        {
            WriteActualMethod();
            try
            {
                this.autoManager.RemoveAuto(autoId);
            }
            catch (Exception ex)
            {
                var e = new EntityOperationFault()
                {
                    ErrorInputType = typeof(AutoDto),
                    ErrorInputId = autoId,
                    ErrorOperation = ErrorOperation.Delete
                };
                throw new FaultException<EntityOperationFault>(e, new FaultReason(ex.Message));
            }
        }

        public void DeleteKunde(int kundenId)
        {
            WriteActualMethod();
            try
            {
                this.kundeManager.RemoveKunde(kundenId);
            }catch(Exception ex)
            {
                var e = new EntityOperationFault()
                {
                    ErrorInputType = typeof(KundeDto),
                    ErrorInputId = kundenId,
                    ErrorOperation = ErrorOperation.Delete
                };
                throw new FaultException<EntityOperationFault>(e, new FaultReason(ex.Message));
            }
        }

        public void DeleteReservation(int reservationsNr)
        {
            WriteActualMethod();
            try
            {
                this.reservationManager.RemoveReservation(reservationsNr);
            }
            catch (Exception ex)
            {
                var e = new EntityOperationFault()
                {
                    ErrorInputType = typeof(ReservationDto),
                    ErrorInputId = reservationsNr,
                    ErrorOperation = ErrorOperation.Delete
                };
                throw new FaultException<EntityOperationFault>(e, new FaultReason(ex.Message));
            }
        }

        public List<AutoDto> GetAllAutos()
        {
            WriteActualMethod();
            try
            {
                return autoManager.GetAllAutos().ConvertToDtos();
            }

            catch (Exception ex)
            {
                var e = new EntityOperationFault()
                {
                    ErrorInputType = typeof(List<AutoDto>),
                    ErrorOperation = ErrorOperation.Read
                };
                throw new FaultException<EntityOperationFault>(e, new FaultReason(ex.Message));
            }
        }

        public List<KundeDto> GetAllKunden()
        {
            WriteActualMethod();
            try
            {
                return kundeManager.GetAllKunden().ConvertToDtos();
            }

            catch (Exception ex)
            {
                var e = new EntityOperationFault()
                {
                    ErrorInputType = typeof(List<AutoDto>),
                    ErrorOperation = ErrorOperation.Read
                };
                throw new FaultException<EntityOperationFault>(e, new FaultReason(ex.Message));
            }
        }

        public List<ReservationDto> GetAllReservations()
        {
            WriteActualMethod();
            try
            {
                return reservationManager.GetAllReservationen().ConvertToDtos();
            }

            catch (Exception ex)
            {
                var e = new EntityOperationFault()
                {
                    ErrorInputType = typeof(List<ReservationDto>),
                    ErrorOperation = ErrorOperation.Read
                };
                throw new FaultException<EntityOperationFault>(e, new FaultReason(ex.Message));
            }
        }

        public AutoDto GetAuto(int autoId)
        {
            WriteActualMethod();
            try
            {
                return autoManager.GetAuto(autoId).ConvertToDto();
            }catch(Exception ex)
            {
                var e = new EntityOperationFault()
                {
                    ErrorInputType = typeof(AutoDto),
                    ErrorOperation = ErrorOperation.Read
                };
                throw new FaultException<EntityOperationFault>(e, new FaultReason(ex.Message));
            }
        }

        public KundeDto GetKunde(int kundenId)
        {
            WriteActualMethod();
            try
            {
                return kundeManager.GetKunde(kundenId).ConvertToDto();
            }
            catch (Exception ex)
            {
                var e = new EntityOperationFault()
                {
                    ErrorInputType = typeof(KundeDto),
                    ErrorOperation = ErrorOperation.Read
                };
                throw new FaultException<EntityOperationFault>(e, new FaultReason(ex.Message));
            }
        }

        public ReservationDto GetReservation(int reservationsNr)
        {
            WriteActualMethod();
            try
            {
                return reservationManager.GetReservation(reservationsNr).ConvertToDto();
            }
            catch(Exception ex)
            {
                var e = new EntityOperationFault()
                {
                    ErrorInputType = typeof(ReservationDto),
                    ErrorOperation = ErrorOperation.Read
                };
                throw new FaultException<EntityOperationFault>(e, new FaultReason(ex.Message));
            }
        }

        public void InsertAuto(AutoDto auto)
        {
            WriteActualMethod();
            try
            {
                this.autoManager.InsertAuto(auto.ConvertToEntity());
            }
            catch (Exception ex)
            {
                var e = new EntityOperationFault()
                {
                    ErrorInputType = typeof(AutoDto),
                    ErrorInputId = auto.Id,
                    ErrorOperation = ErrorOperation.Create
                };
                throw new FaultException<EntityOperationFault>(e, new FaultReason(ex.Message));
            }
        }

        public void InsertKunde(KundeDto kunde)
        {
            WriteActualMethod();
            try
            {
                this.kundeManager.InsertKunde(kunde.ConvertToEntity());
            }
            catch (Exception ex)
            {
                var e = new EntityOperationFault()
                {
                    ErrorInputType = typeof(AutoDto),
                    ErrorInputId = kunde.Id,
                    ErrorOperation = ErrorOperation.Create
                };
                throw new FaultException<EntityOperationFault>(e, new FaultReason(ex.Message));
            }
        }

        public void InsertReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            if(!IsAutoAvailable(reservation.Auto, reservation.Von, reservation.Bis))
            {
                throw new FaultException<AutoUnavailableFault>(new AutoUnavailableFault(), new FaultReason("Auto is unavailable on these dates."));
            }
            try
            {
                this.reservationManager.InsertReservation(reservation.ConvertToEntity());
            }
            catch(InvalidDateRangeException ex)
            {
                throw new FaultException<InvalidDateRangeFault>(new InvalidDateRangeFault(), new FaultReason("Date range is not valid."));
            }
            catch(OverlappingTimesException ex)
            {
                var e = new OverlappingTimesFault();
                throw new FaultException<OverlappingTimesFault>(e, new FaultReason("The times overlap: "+ ex.Message));
            }
            catch (Exception ex)
            {
                var e = new EntityOperationFault()
                {
                    ErrorInputType = typeof(AutoDto),
                    ErrorInputId = reservation.ReservationsNr,
                    ErrorOperation = ErrorOperation.Delete
                };
                throw new FaultException<EntityOperationFault>(e, new FaultReason(ex.Message));
            }
        }

        public bool IsAutoAvailable(AutoDto auto, DateTime von, DateTime bis)
        {
            var currentAutoReservation = reservationManager.GetAllReservationen()
                .FindAll(r=>r.AutoId == auto.Id)
                .FindAll(r => r.Von.Date <= von.Date && r.Bis.Date >= bis.Date);

            return currentAutoReservation.Count == 0;
        }

        public void UpdateAuto(AutoDto auto)
        {
            WriteActualMethod();
            try
            {
                autoManager.UpdateAuto(auto.ConvertToEntity());
            }
            catch(Exception ex)
            {
                var e = new EntityOperationFault()
                {
                    ErrorInputType = typeof(AutoDto),
                    ErrorInputId = auto.Id,
                    ErrorOperation = ErrorOperation.Update
                };
                throw new FaultException<EntityOperationFault>(e, new FaultReason(ex.Message));
            }
        }

        public void UpdateKunde(KundeDto kunde)
        {
            WriteActualMethod();
            try
            {
                kundeManager.UpdateKunde(kunde.ConvertToEntity());
            }
            catch(Exception ex)
            {
                var e = new EntityOperationFault()
                {
                    ErrorInputType = typeof(KundeDto),
                    ErrorInputId = kunde.Id,
                    ErrorOperation = ErrorOperation.Update
                };
                throw new FaultException<EntityOperationFault>(e, new FaultReason(ex.Message));
            }
        }

        public void UpdateReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            if (!IsAutoAvailable(reservation.Auto, reservation.Von, reservation.Bis))
            {
                throw new FaultException<AutoUnavailableFault>(new AutoUnavailableFault(), "Auto not available on these dates");
            }
            try
            {
                reservationManager.UpdateReservation(reservation.ConvertToEntity());
            }
            catch(OverlappingTimesException ex)
            {
                throw new FaultException<OverlappingTimesFault>(new OverlappingTimesFault(), ex.Message);
            }
            catch (Exception ex)
            {
                throw new FaultException<InvalidDateRangeFault>(new InvalidDateRangeFault(), new FaultReason(ex.Message));
            }
        }
    }
}