using AutoReservation.Common.Interfaces;
using System;
using System.Diagnostics;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.DataTransferObjects.Faults;
using System.Collections.Generic;
using AutoReservation.BusinessLayer;
using System.ServiceModel;

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

        }

        public void DeleteReservation(int reservationsNr)
        {
            throw new NotImplementedException();
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
                    ErrorOperation = ErrorOperation.Delete
                };
                throw new FaultException<EntityOperationFault>(e, new FaultReason(ex.Message));
            }
        }

        public void InsertKunde(KundeDto kunde)
        {
            throw new NotImplementedException();
        }

        public void InsertReservation(ReservationDto reservation)
        {
            throw new NotImplementedException();
        }

        public bool IsAutoAvailable(AutoDto auto)
        {
            throw new NotImplementedException();
        }

        public void UpdateAuto(AutoDto auto)
        {
            throw new NotImplementedException();
        }

        public void UpdateKunde(KundeDto kunde)
        {
            throw new NotImplementedException();
        }

        public void UpdateReservation(ReservationDto reservation)
        {
            throw new NotImplementedException();
        }
    }
}