using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;
using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
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
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void GetKundeByIdWithIllegalIdTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void GetReservationByNrWithIllegalIdTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        #endregion

        #region Insert

        [TestMethod]
        public void InsertAutoTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void InsertKundeTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void InsertReservationTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        #endregion

        #region Delete  

        [TestMethod]
        public void DeleteAutoTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void DeleteKundeTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void DeleteReservationTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        #endregion

        #region Update

        [TestMethod]
        public void UpdateAutoTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void UpdateKundeTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void UpdateReservationTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        #endregion

        #region Update with optimistic concurrency violation

        [TestMethod]
        public void UpdateAutoWithOptimisticConcurrencyTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void UpdateKundeWithOptimisticConcurrencyTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void UpdateReservationWithOptimisticConcurrencyTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        #endregion

        #region Insert / update invalid time range

        [TestMethod]
        public void InsertReservationWithInvalidDateRangeTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void InsertReservationWithAutoNotAvailableTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void UpdateReservationWithInvalidDateRangeTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void UpdateReservationWithAutoNotAvailableTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        #endregion

        #region Check Availability

        [TestMethod]
        public void CheckAvailabilityIsTrueTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void CheckAvailabilityIsFalseTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        #endregion
    }
}
