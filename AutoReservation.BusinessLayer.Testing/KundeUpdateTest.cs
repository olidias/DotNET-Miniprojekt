using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AutoReservation.BusinessLayer.Testing
{
    [TestClass]
    public class KundeUpdateTest
    {
        private KundeManager target;
        private KundeManager Target => target ?? (target = new KundeManager());


        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
        }

        [TestMethod]
        public void UpdateKundeTest()
        {
            var now = DateTime.Now.Date;
            var toUpdate = Target.GetKunde(4);
            toUpdate.Vorname = "Cain";
            toUpdate.Geburtsdatum = now;
            Target.UpdateKunde(toUpdate);
            var res = Target.GetKunde(4);
            Assert.AreEqual("Cain Zufall", $"{res.Vorname} {res.Nachname}");
            Assert.AreEqual(now, res.Geburtsdatum);
        }
    }
}
