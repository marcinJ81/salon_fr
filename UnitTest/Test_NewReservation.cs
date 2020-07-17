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
        private ISelectClient selectClient;
         [SetUp]
        public void Setup()
        {
            selectClient = new SelectClient();
            addClient = new DBInsertClient(selectClient);
        }
        [Test]
        public void ShouldAddNewClient_ReturnTrue()
        {
            SqlLiteDB.SqlLiteDBCreateTable();
            int clientID = selectClient.GetNextClientId(SGetIdFromSpecificTable.queryGetLatestClientID());
            Client client = new Client()
            {
                client_id =clientID,
                client_name = "Marcin",
                client_sname = "Juranek",
                client_phone = "123456789",
                client_description = "test"
            };
            int lastIndex = addClient.InsertObjectToDB(client);

            Assert.AreEqual(lastIndex,clientID);
        }
    }
}
