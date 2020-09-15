using NUnit.Framework;
using salonfr.UnitTest;
using salonfrSource;
using salonfrSource.ModelDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salonfr
{
    public class Test_Reservation
    {
        private IViewReservation fake_vreservation;
        private IViewSpecificReservation vspecreservation;
        [SetUp]
        public void Setup()
        {
            this.fake_vreservation = new Fake_VReservation();
            this.vspecreservation = new ViewReservation(fake_vreservation);
        }
        [Test]
        public void ShouldShowReservationByTheDate_returnOneRow()
        {
            DateTime dateTime = new DateTime(2020, 7, 13);
            List<VReservation> result = vspecreservation.ReservationByDa(dateTime);
            var target = fake_vreservation.AllReservation();
            Assert.AreEqual(target.FirstOrDefault().reservation_date.ToShortDateString(),result.FirstOrDefault().reservation_date.ToShortDateString());
        }
        [Test]
        public void ShouldNotShowReservationBecauseDateIsWrong_RetrunEmptyRow()
        {
            DateTime dateTime = new DateTime(2020, 7, 20);
            List<VReservation> result = vspecreservation.ReservationByDa(dateTime);
            Assert.AreEqual("brak", result.FirstOrDefault().client_name);
        }
        [Test]
        public void ShouldShowReservationByTheSpecificServicesName_retunOneRow()
        {
            List<VReservation> result = vspecreservation.ResrvationByServices("strzyżenie");
            var target = fake_vreservation.AllReservation();
            Assert.AreEqual(target.First().services_name, result.First().services_name);
        }
        [Test]
        public void ShoulSgowEmptyReservationWhenServicesNameAreWrong_ReturnEmptyRow()
        {
            List<VReservation> result = vspecreservation.ResrvationByServices("wyrabianie ciasta");
            Assert.AreEqual("brak", result.First().services_name);
        }
    }
}
