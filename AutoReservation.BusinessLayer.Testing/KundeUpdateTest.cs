using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.ObjectModel;

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
        public void CreateKundeTest()
        {
            var kundenListBefore = Target.GetAllKunden();
            var kunde = new Kunde() { Geburtsdatum = DateTime.Now.Date, Id = 5, Vorname = "Bear", Nachname = "Trapp", Reservationen = null };
            Target.InsertKunde(kunde);
            var kundenListAfter = Target.GetAllKunden();
            var bearTrapp = kundenListAfter.Find(k => k.Id == 5);

            Assert.AreEqual(kundenListBefore.Count + 1, kundenListAfter.Count);
            Assert.AreEqual("Bear Trapp", $"{bearTrapp.Vorname} {bearTrapp.Nachname}");
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

        [TestMethod]
        public void UpdateKundeWithReservationTest()
        {
            // TODO
            //var res = new Reservation() { Auto = new StandardAuto(), AutoId = 1, Bis = DateTime.Now.AddDays(5).Date, Von = DateTime.Now.Date, ReservationsNr = 1 };
            //var kunde = Target.GetKunde(1);
            //res.Kunde = kunde;
            //res.KundeId = kunde.Id;
            //Target.UpdateKunde(kunde);
            //var kundeAfter = Target.GetKunde(1);

            //Assert.AreEqual(res.Von, reservationen)
        }

        [TestMethod]
        public void RemoveKundeTest()
        {
            var totalKundenBefore = Target.GetAllKunden();
            var kunde = totalKundenBefore.Find(k=>k.Id==1);
            Target.RemoveKunde(kunde.Id);
            var totalKundenAfter = Target.GetAllKunden();

            Assert.AreEqual(totalKundenBefore.Count - 1, totalKundenAfter.Count);
            Assert.IsFalse(totalKundenAfter.Contains(kunde));
        }
    }
}
