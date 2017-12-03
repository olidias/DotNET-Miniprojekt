using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AutoReservation.BusinessLayer.Testing
{
    [TestClass]
    public class AutoUpdateTests
    {
        private AutoManager target;
        private AutoManager Target => target ?? (target = new AutoManager());


        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
        }

        [TestMethod]
        public void UpdateAutoTest()
        {
            var toUpdate = Target.GetAuto(3);
            toUpdate.Marke = "Aston Martin";
            toUpdate.Tagestarif = 200;
            target.UpdateAuto(toUpdate);
            Assert.AreEqual("Aston Martin", target.GetAuto(3).Marke);
            Assert.AreEqual(200, target.GetAuto(3).Tagestarif);
        }
    }
}
