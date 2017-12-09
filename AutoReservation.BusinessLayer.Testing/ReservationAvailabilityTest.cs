using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AutoReservation.BusinessLayer.Testing
{
    [TestClass]
    public class ReservationAvailabilityTest
    {
        private ReservationManager target;
        private ReservationManager Target => target ?? (target = new ReservationManager());


        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
        }

        [TestMethod]
        public void ScenarioDifferentCarSameCustomerSameTime()
        {
            var resCountBefore = Target.GetAllReservationen().Count;
            var res1 = new Reservation() { ReservationsNr = 255, AutoId = 1, KundeId =1, Von = DateTime.Now.Date, Bis = DateTime.Now.AddDays(5).Date};
            var res2 = new Reservation() { ReservationsNr = 256, AutoId = 2, KundeId = 1, Von = DateTime.Now.Date, Bis = DateTime.Now.Date.AddDays(5).Date};

            Target.InsertReservation(res1);
            Target.InsertReservation(res2);

            Assert.AreEqual(resCountBefore + 2, Target.GetAllReservationen().Count);
        }

        [TestMethod]
        public void ScenarioDifferentCarsDifferentCustomerSameTime()
        {
            var resCountBefore = Target.GetAllReservationen().Count;
            var res1 = new Reservation() { ReservationsNr = 255, AutoId = 1, KundeId = 1, Von = DateTime.Now.Date, Bis = DateTime.Now.AddDays(5).Date };
            var res2 = new Reservation() { ReservationsNr = 256, AutoId = 2, KundeId = 2, Von = DateTime.Now.Date, Bis = DateTime.Now.AddDays(5).Date };

            Target.InsertReservation(res1);
            Target.InsertReservation(res2);

            Assert.AreEqual(resCountBefore + 2, Target.GetAllReservationen().Count);
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.OverlappingTimesException))]
        public void ScenarioSameCustomerDifferentCarSameTime()
        {
            var res1 = new Reservation() { ReservationsNr = 255, AutoId = 1, KundeId = 1, Von = DateTime.Now.Date, Bis = DateTime.Now.AddDays(1).Date };
            var res2 = new Reservation() { ReservationsNr = 256, AutoId = 1, KundeId = 1, Von = DateTime.Now.Date.AddDays(-4), Bis = DateTime.Now.AddDays(5).Date };

            Target.InsertReservation(res1);
            Target.InsertReservation(res2);
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.OverlappingTimesException))]
        public void ScenarioDifferentCustomerSameCarSameTime()
        {
            var res1 = new Reservation() { ReservationsNr = 255, AutoId = 1, KundeId = 1, Von = DateTime.Now.Date.AddDays(-2), Bis = DateTime.Now.AddDays(1).Date };
            var res2 = new Reservation() { ReservationsNr = 256, AutoId = 1, KundeId = 2, Von = DateTime.Now.Date, Bis = DateTime.Now.AddDays(5).Date };

            Target.InsertReservation(res1);
            Target.InsertReservation(res2);
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.OverlappingTimesException))]
        public void ScenarioDifferentCustomerSameCarInnerTimes()
        {
            var res1 = new Reservation() { ReservationsNr = 255, AutoId = 1, KundeId = 1, Von = DateTime.Now.Date.AddDays(-2), Bis = DateTime.Now.AddDays(6).Date };
            var res2 = new Reservation() { ReservationsNr = 256, AutoId = 1, KundeId = 2, Von = DateTime.Now.Date, Bis = DateTime.Now.AddDays(5).Date };

            Target.InsertReservation(res1);
            Target.InsertReservation(res2);
        }
    }
}
