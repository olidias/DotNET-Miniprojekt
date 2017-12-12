using AutoReservation.Common.DataTransferObjects;
using System.Collections.Generic;
using System.ServiceModel;

namespace AutoReservation.Common.Interfaces
{
    [ServiceContract]
    public interface IAutoReservationService
    {
        [OperationContract]
        List<ReservationDto> GetAllReservations();
        [OperationContract]
        ReservationDto GetReservation(int reservationsNr);
        [OperationContract]
        void InsertReservation(ReservationDto reservation);
        [OperationContract]
        void UpdateReservation(ReservationDto reservation);
        [OperationContract]
        void DeleteReservation(int reservationsNr);

        [OperationContract]
        List<KundeDto> GetAllKunden();
        [OperationContract]
        KundeDto GetKunde(int kundenId);
        [OperationContract]
        void InsertKunde(KundeDto kunde);
        [OperationContract]
        void UpdateKunde(KundeDto kunde);
        [OperationContract]
        void DeleteKunde(int kundenId);

        [OperationContract]
        List<AutoDto> GetAllAutos();
        [OperationContract]
        AutoDto GetAuto(int autoId);
        [OperationContract]
        void InsertAuto(AutoDto auto);
        [OperationContract]
        void UpdateAuto(AutoDto auto);
        [OperationContract]
        void DeleteAuto(int autoId);


    }
}
