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
        public void CreateAutoTest()
        {
            var autoListBefore = Target.GetAllAutos();
            var jag = new LuxusklasseAuto() { Id = 4, Marke = "Jaguar", Reservationen = null, Basistarif = 60, Tagestarif = 160 };
            Target.InsertAuto(jag);
            var autoListAfter = Target.GetAllAutos();
            var jagFromDb = autoListAfter.Find(a => a.Id == 4);
            if (!(jagFromDb is LuxusklasseAuto))
                Assert.Fail();

            var luxusJagFromDb = (LuxusklasseAuto)jagFromDb;

            Assert.AreEqual(autoListBefore.Count + 1, autoListAfter.Count);
            Assert.AreEqual(jag.Marke, luxusJagFromDb.Marke);
            Assert.AreEqual(jag.Tagestarif, luxusJagFromDb.Tagestarif);
            Assert.AreEqual(jag.Basistarif, luxusJagFromDb.Basistarif);
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

        [TestMethod]
        public void RemoveAutoTest()
        {
            var totalAutosBefore = Target.GetAllAutos();
            var auto = totalAutosBefore.Find(k => k.Id == 1);
            Target.RemoveAuto(auto);
            var totalAutosAfter = Target.GetAllAutos();

            Assert.AreEqual(totalAutosBefore.Count - 1, totalAutosAfter.Count);
            Assert.IsFalse(totalAutosAfter.Contains(auto));
        }
    }
}
