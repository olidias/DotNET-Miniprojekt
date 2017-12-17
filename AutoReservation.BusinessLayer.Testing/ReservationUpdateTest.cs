using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AutoReservation.BusinessLayer.Testing
{
    [TestClass]
    public class ReservationUpdateTest
    {
        private ReservationManager target;
        private ReservationManager Target => target ?? (target = new ReservationManager());


        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
        }

        [TestMethod]
        public void UpdateReservationTest()
        {
            Reservation res = Target.GetReservation(1);
            var oldBis = res.Bis;

            res.Bis = oldBis.AddDays(2).Date;
            Target.UpdateReservation(res);
            Assert.AreEqual(oldBis.AddDays(2), Target.GetReservation(1).Bis);
        }
    }
}
