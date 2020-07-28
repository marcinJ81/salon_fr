using Microsoft.Data.Sqlite;
using NUnit.Framework;
using salonfr.DBConnect;
using salonfr.InsertDateToBase;
using salonfr.QuerySelect;
using salonfrSource.QuerySelect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salonfr.UnitTest
{
   public class Test_NewReservation
    {
        private IInsertToDB<Client> addClient;
        private IInsertToDB<Services> addServices;
        private IInsertToDB<Reservation> addReservation;
        private ISelectTableObject<Client> selectClient;
        private ISelectServices selectServices;
        private ISelectReservation selectReservation;
         [SetUp]
        public void Setup()
        {
            selectClient = new SelectClient();
            selectServices = new SelectServices();
            selectReservation = new SelectReservation();
            addClient = new DBInsertClient(selectClient);
            addServices = new DBInsertServices(selectServices);
            addReservation = new DBInsertReservation(selectReservation);
        }
        [Test]
        [Ignore("insert into real db")]
        public void ShouldAddNewClient_ReturnNewID()
        {
            SqlLiteDB.SqlLiteDBCreateTable();
            int clientID = selectClient.GetNextTabletId(SGetIdFromSpecificTable.queryGetLatestClientID());
            Client client = new Client()
            {
                client_id =clientID,
                client_name = "Julian",
                client_sname = "Krol",
                client_phone = "123456789",
                client_description = "test kolejny"
            };
            int lastIndex =  addClient.InsertObjectToDB(client);

            Assert.AreEqual(lastIndex,clientID);
        }
        [Test]
        [Ignore("insert into real db")]
        public void ShouldAddNewServices_ReturnNewID()
        {
            SqlLiteDB.SqlLiteDBCreateTable();
            int servicesID = selectServices.GetNextServicesId(SGetIdFromSpecificTable.queryGetLatestServicesID());
            Services services = new Services()
            {
                services_id = servicesID,
                services_name = "trwała"
            };
            int lastIndex = addServices.InsertObjectToDB(services);
            Assert.AreEqual(lastIndex, servicesID);
        }
        [Test]
        [Ignore("insert into real db")]
        public void ShouldAddNewReservation_ReturnNewID()
        {
            SqlLiteDB.SqlLiteDBCreateTable();
            int reservationID = selectReservation.GetNextReservationtId(SGetIdFromSpecificTable.queryGetLatestReservationID());
            Reservation reservation = new Reservation()
            {
                reservation_id = reservationID,
                reservation_date = new DateTime(2020, 7, 17, 12, 1, 1),
                reservation_time = new TimeSpan(11, 5, 0),
                client_id = 2,
                services_id = 2
            };
            int lastIndex = addReservation.InsertObjectToDB(reservation);
            Assert.AreEqual(lastIndex, reservationID);
        }
    }
}
