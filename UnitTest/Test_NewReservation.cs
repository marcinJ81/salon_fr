﻿using NUnit.Framework;
using salonfr.InsertDateToBase;
using salonfr.InsertReservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salonfr.UnitTest
{
   public class Test_NewReservation
    {
        private IAddReservation addreservation;
        private IAddClientAndServices fake_addClientAndServices;
        [SetUp]
        public void Setup()
        {
            this.fake_addClientAndServices = new Fake_AddServicesAndClients();
            this.addreservation = new ReservationFunctions(fake_addClientAndServices);
        }
        [Test]
        public void ShouldAddNewReservation_ReturnTrue()
        {
            Reservation newReservation = new Reservation()
            {
                reservation_id = 1,
                reservation_date = new DateTime(2020,7,13),
            };
            bool result = addreservation.AddReservation(newReservation);
            Assert.IsTrue(result);
        }
    }
}
