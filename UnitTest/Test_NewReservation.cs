using Microsoft.Data.Sqlite;
using NUnit.Framework;
using salonfr.DBConnect;
using salonfr.InsertDateToBase;
using salonfr.QuerySelect;
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
        private ISelectClient selectClient;
        private ISelectServices selectServices;
         [SetUp]
        public void Setup()
        {
            selectClient = new SelectClient();
            selectServices = new SelectServices();
            addClient = new DBInsertClient(selectClient);
            addServices = new DBInsertServices(selectServices);
        }
        [Test]
        public void ShouldAddNewClient_ReturnNewID()
        {
            SqlLiteDB.SqlLiteDBCreateTable();
            int clientID = 1;//selectClient.GetNextClientId(SGetIdFromSpecificTable.queryGetLatestClientID());
            Client client = new Client()
            {
                client_id =clientID,
                client_name = "Julian",
                client_sname = "Krol",
                client_phone = "123456789",
                client_description = "test"
            };
            int lastIndex = 1;// addClient.InsertObjectToDB(client);

            Assert.AreEqual(lastIndex,clientID);
        }
        [Test]
        public void ShoulAddNewServices_ReturnNewID()
        {
            SqlLiteDB.SqlLiteDBCreateTable();
            int servicesID = 1;// selectServices.GetNextServicesId(SGetIdFromSpecificTable.queryGetLatestServicesID());
            Services services = new Services()
            {
                services_id = servicesID,
                services_name = "trwała"
            };
            int lastIndex = 1;//addServices.InsertObjectToDB(services);
            Assert.AreEqual(lastIndex, servicesID);
        }
    }
}
