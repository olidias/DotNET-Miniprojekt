using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AutoReservation.BusinessLayer.Testing
{
    [TestClass]
    public class ReservationDateRangeTest
    {
        private ReservationManager target;
        private ReservationManager Target => target ?? (target = new ReservationManager());


        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
        }

        [TestMethod]
        public void ScenarioOkay01Test()
        {
            var resCountBefore = Target.GetAllReservationen().Count;
            var res = new Reservation() { ReservationsNr = 256, AutoId = 3, KundeId = 1, Von = DateTime.Now.Date, Bis = DateTime.Now.Date.AddDays(5).Date };

            Target.InsertReservation(res);

            Assert.AreEqual(resCountBefore + 1, Target.GetAllReservationen().Count);
        }

        [TestMethod]
        public void ScenarioOkay02Test()
        {
            var resCountBefore = Target.GetAllReservationen().Count;
            var res = new Reservation() { ReservationsNr = 256, AutoId = 3, KundeId = 1, Von = DateTime.Now.Date, Bis = DateTime.Now.Date.AddDays(1).Date };

            Target.InsertReservation(res);

            Assert.AreEqual(resCountBefore + 1, Target.GetAllReservationen().Count);
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.InvalidDateRangeException))]
        public void ScenarioNotOkay01Test()
        {
            var resCountBefore = Target.GetAllReservationen().Count;
            var res = new Reservation() { ReservationsNr = 256, AutoId = 3, KundeId = 1, Von = DateTime.Now.Date, Bis = DateTime.Now.Date.AddDays(-1).Date };

            Target.InsertReservation(res);
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.InvalidDateRangeException))]
        public void ScenarioNotOkay02Test()
        {
            var resCountBefore = Target.GetAllReservationen().Count;
            var res = new Reservation() { ReservationsNr = 256, AutoId = 3, KundeId = 1, Von = DateTime.Now.Date, Bis = DateTime.Now.Date };

            Target.InsertReservation(res);
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.InvalidDateRangeException))]
        public void ScenarioNotOkay03Test()
        {
            var resCountBefore = Target.GetAllReservationen().Count;
            var res = new Reservation() { ReservationsNr = 256, AutoId = 3, KundeId = 1, Von = DateTime.Now.Date.AddDays(-2), Bis = DateTime.Now.Date.AddDays(-4)};

            Target.InsertReservation(res);
        }
    }
}
