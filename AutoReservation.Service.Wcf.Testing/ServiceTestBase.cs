using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.DataTransferObjects.Faults;
using AutoReservation.Common.Interfaces;
using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.ServiceModel;
//using AutoReservation.Common.DataTransferObjects.Faults;

namespace AutoReservation.Service.Wcf.Testing
{
    [TestClass]
    public abstract class ServiceTestBase
    {
        protected abstract IAutoReservationService Target { get; }

        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
        }

        #region Read all entities

        [TestMethod]
        public void GetAutosTest()
        {
            var autos = Target.GetAllAutos();
            Assert.AreEqual(3, autos.Count);
        }

        [TestMethod]
        public void GetKundenTest()
        {
            var kunden = Target.GetAllKunden();
            Assert.AreEqual(4, kunden.Count);
        }

        [TestMethod]
        public void GetReservationenTest()
        {
            var res = Target.GetAllReservations();
            Assert.AreEqual(3, res.Count);
        }

        #endregion

        #region Get by existing ID

        [TestMethod]
        public void GetAutoByIdTest()
        {
            var auto = Target.GetAuto(2);
            Assert.AreEqual("VW Golf", auto.Marke);
            Assert.AreEqual(120, auto.Tagestarif);
            Assert.AreEqual(AutoKlasse.Mittelklasse, auto.AutoKlasse);

        }

        [TestMethod]
        public void GetKundeByIdTest()
        {
            var kunde = Target.GetKunde(4);
            Assert.AreEqual("Rainer", kunde.Vorname);
            Assert.AreEqual("Zufall", kunde.Nachname);
            Assert.AreEqual(new DateTime(1954, 11, 11).Date, kunde.Geburtsdatum.Date);
        }

        [TestMethod]
        public void GetReservationByNrTest()
        {
            var res = Target.GetReservation(3);
            Assert.AreEqual(3, res.Kunde.Id);
            Assert.AreEqual(3, res.Auto.Id);
            Assert.AreEqual(new DateTime(2020, 01, 10).Date, res.Von.Date);
            Assert.AreEqual(new DateTime(2020, 01, 20).Date, res.Bis.Date);
        }

        #endregion

        #region Get by not existing ID

        [TestMethod]
        public void GetAutoByIdWithIllegalIdTest()
        {
            var auto = Target.GetAuto(10);
            Assert.IsNull(auto);
        }

        [TestMethod]
        public void GetKundeByIdWithIllegalIdTest()
        {
            var kunde = Target.GetKunde(10);
            Assert.IsNull(kunde);
        }

        [TestMethod]
        public void GetReservationByNrWithIllegalIdTest()
        {
            var res = Target.GetReservation(10);
            Assert.IsNull(res);
        }

        #endregion

        #region Insert

        [TestMethod]
        public void InsertAutoTest()
        {
            var countBeforeInsertion = Target.GetAllAutos().Count;
            var auto = new AutoDto() { AutoKlasse = AutoKlasse.Luxusklasse, Basistarif = 10, Tagestarif = 100, Marke = "Aston Martin" };
            Target.InsertAuto(auto);

            var autoFromDb = Target.GetAuto(4);
            Assert.AreEqual(countBeforeInsertion + 1, Target.GetAllAutos().Count);
            Assert.AreEqual(auto.AutoKlasse, autoFromDb.AutoKlasse);
            Assert.AreEqual(auto.Marke, autoFromDb.Marke);
            Assert.AreEqual(auto.Tagestarif, autoFromDb.Tagestarif);
            Assert.AreEqual(auto.Basistarif, autoFromDb.Basistarif);
        }

        [TestMethod]
        public void InsertKundeTest()
        {
            var countBeforeInsertion = Target.GetAllKunden().Count;
            var kunde = new KundeDto() { Vorname = "Manfred", Geburtsdatum = DateTime.Now.Date, Nachname = "Matter" };
            Target.InsertKunde(kunde);

            var kundeFromDb = Target.GetKunde(5);
            Assert.AreEqual(countBeforeInsertion + 1, Target.GetAllKunden().Count);
            Assert.AreEqual(kunde.Vorname, kundeFromDb.Vorname);
            Assert.AreEqual(kunde.Geburtsdatum, kundeFromDb.Geburtsdatum);
            Assert.AreEqual(kunde.Nachname, kundeFromDb.Nachname);
        }

        [TestMethod]
        public void InsertReservationTest()
        {
            var countBeforeInsertion = Target.GetAllReservations().Count;
            var auto = Target.GetAuto(1);
            var kunde = Target.GetKunde(3);
            var res = new ReservationDto() { Auto=auto, Kunde = kunde, Bis = DateTime.Now.AddDays(3).Date, Von = DateTime.Now.Date };
            Target.InsertReservation(res);

            var resFromDb = Target.GetReservation(4);
            Assert.AreEqual(countBeforeInsertion + 1, Target.GetAllReservations().Count);
            Assert.AreEqual(res.Von, resFromDb.Von);
            Assert.AreEqual(res.Bis, resFromDb.Bis);
            Assert.AreEqual(res.Kunde.Nachname, resFromDb.Kunde.Nachname);
            Assert.AreEqual(res.Kunde.Id, resFromDb.Kunde.Id);
            Assert.AreEqual(res.Auto.Marke, resFromDb.Auto.Marke);
            Assert.AreEqual(res.Auto.Tagestarif, resFromDb.Auto.Tagestarif);
        }

        #endregion

        #region Delete  

        [TestMethod]
        public void DeleteAutoTest()
        { 
            var countBeforeDeletion = Target.GetAllAutos().Count;
            Target.DeleteAuto(1);
            Assert.AreEqual(countBeforeDeletion - 1, Target.GetAllAutos().Count);
        }

        [TestMethod]
        public void DeleteKundeTest()
        {
            var countBeforeDeletion = Target.GetAllKunden().Count;
            Target.DeleteKunde(1);
            Assert.AreEqual(countBeforeDeletion - 1, Target.GetAllKunden().Count);
        }

        [TestMethod]
        public void DeleteReservationTest()
        {
            var countBeforeDeletion = Target.GetAllReservations().Count;
            Target.DeleteReservation(1);
            Assert.AreEqual(countBeforeDeletion - 1, Target.GetAllReservations().Count);
        }

        #endregion

        #region Update

        [TestMethod]
        public void UpdateAutoTest()
        {
            var auto = Target.GetAuto(1);
            auto.Marke = "Aston Martin";
            Target.UpdateAuto(auto);

            Assert.AreEqual(auto.Marke, Target.GetAuto(1).Marke);
        }

        [TestMethod]
        public void UpdateKundeTest()
        {
            var kunde = Target.GetKunde(1);
            var nn = "Muool";
            kunde.Nachname = nn;

            Target.UpdateKunde(kunde);
            Assert.AreEqual(nn, Target.GetKunde(1).Nachname);
        }

        [TestMethod]
        public void UpdateReservationTest()
        {
            var res = Target.GetReservation(1);
            res.Von = DateTime.Now.AddDays(-2).Date;
            res.Bis = DateTime.Now.AddDays(3).Date;

            Target.UpdateReservation(res);
            var resDb = Target.GetReservation(1);
            Assert.AreEqual(DateTime.Now.AddDays(-2).Date, resDb.Von);
            Assert.AreEqual(DateTime.Now.AddDays(3).Date, resDb.Bis);

        }

        #endregion

        #region Update with optimistic concurrency violation

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void UpdateAutoWithOptimisticConcurrencyTest()
        {
            var auto1 = Target.GetAuto(2);
            var auto2 = Target.GetAuto(2);
            auto1.Marke = "Aston Martin";
            auto2.Marke = "VW";
            Target.UpdateAuto(auto1);
            Target.UpdateAuto(auto2);
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void UpdateKundeWithOptimisticConcurrencyTest()
        {
            var k1 = Target.GetKunde(1);
            var k2 = Target.GetKunde(1);

            k1.Vorname = "Heiri";
            k2.Vorname = "Hanswas";
            Target.UpdateKunde(k1);
            Target.UpdateKunde(k2);
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void UpdateReservationWithOptimisticConcurrencyTest()
        {
            var r1 = Target.GetReservation(1);
            var r2 = Target.GetReservation(1);
            r1.Bis = DateTime.Now.AddDays(10);
            r2.Bis = DateTime.Now.AddDays(15);

            Target.UpdateReservation(r1);
            Target.UpdateReservation(r2);
            Assert.Fail();
        }

        #endregion

        #region Insert / update invalid time range

        [TestMethod]
        [ExpectedException(typeof(FaultException<InvalidDateRangeFault>))]
        public void InsertReservationWithInvalidDateRangeTest()
        {
            var res = new ReservationDto() { Kunde = Target.GetKunde(1),Auto=Target.GetAuto(1), Von = new DateTime(2017, 12, 1), Bis = new DateTime(2017, 11, 30) };
            Target.InsertReservation(res);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<AutoUnavailableFault>))]
        public void InsertReservationWithAutoNotAvailableTest()
        {
            var auto = new AutoDto() { Marke = "Aston Martin" };
            var res = new ReservationDto() { Auto = Target.GetAuto(1), Kunde = Target.GetKunde(1), Von = DateTime.Now.Date, Bis = DateTime.Now.AddDays(1) };
            var res1 = new ReservationDto() { Auto = Target.GetAuto(1), Kunde = Target.GetKunde(2), Von = DateTime.Now.Date, Bis = DateTime.Now.AddDays(1) };
            Target.InsertReservation(res);
            Target.InsertReservation(res1);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void UpdateReservationWithInvalidDateRangeTest()
        {
            var res = Target.GetReservation(1);
            res.Von = new DateTime(2017, 12, 1);
            res.Bis = new DateTime(2017, 11, 30);
            Target.UpdateReservation(res);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<OverlappingTimesFault>))]
        public void UpdateReservationWithAutoNotAvailableTest()
        {
            var auto = Target.GetAuto(1);
            var res = new ReservationDto() { Auto = auto, Kunde = Target.GetKunde(1), Von = new DateTime(2017, 12, 5), Bis = new DateTime(2017, 12, 10) };
            Target.InsertReservation(res);
            var resDb = Target.GetReservation(1);
            resDb.Auto = auto;
            resDb.Von = new DateTime(2017, 12, 3);
            resDb.Bis = new DateTime(2017, 12, 8);
            Target.UpdateReservation(resDb);
        }

        #endregion

        #region Check Availability

        [TestMethod]
        public void CheckAvailabilityIsTrueTest()
        {
            var auto = new AutoDto() { Id = 4, Marke = "MG" };
            var res = new ReservationDto() { Auto = auto, Kunde=Target.GetKunde(1), Von = new DateTime(2018, 1, 1), Bis = new DateTime(2018,1,5) };

            Target.InsertAuto(auto);
            Target.InsertReservation(res);
            Assert.IsTrue(Target.IsAutoAvailable(auto, new DateTime(2017, 12,17), new DateTime(2017,12,20)));
        }

        [TestMethod]
        public void CheckAvailabilityIsFalseTest()
        {
            var auto = new AutoDto() { Id = 4, Marke = "MG" };
            var res = new ReservationDto() { Auto = auto, Kunde=Target.GetKunde(1), Von = new DateTime(2017, 12, 17), Bis = new DateTime(2017, 12, 20) };

            Target.InsertAuto(auto);
            Target.InsertReservation(res);
            Assert.IsFalse(Target.IsAutoAvailable(auto, new DateTime(2017, 12,17), new DateTime(2017,12,20)));
        }

        #endregion
    }
}
