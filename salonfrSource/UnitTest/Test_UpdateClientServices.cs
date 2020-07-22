using NUnit.Framework;
using salonfr;
using salonfr.DBConnect;
using salonfr.QuerySelect;
using salonfr.SQLScripts;
using salonfrSource.UpdateObjectInBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace salonfrSource.UnitTest
{
    public class Test_UpdateClientServices
    {
        private IUpdateObject<Client> updateClient;
        private ISelectClient selectClient;
        [SetUp]
        public void Setup()
        {
            selectClient = new SelectClient();
            updateClient = new UpdateClient(selectClient);
        }
        [Test]
        public void ShouldUpdateClient_when_IHaveHisId_returnTrue()
        {
            SqlLiteDB.SqlLiteDBCreateTable();
            //search client
            var clientResult = selectClient.GetClients(SGetAllRowsFromSpecificTable.ClientSelectAllRowsQuery()).First();
            //get his client_id
            int clientId = clientResult.client_id;
            //change his data
            Client changeClient = new Client()
            {
                client_name = clientResult.client_name,
                client_sname = clientResult.client_sname,
                client_phone = clientResult.client_phone,
                client_description = "zaktualizowny"
            };
            bool result = updateClient.UpdateObject(changeClient, clientId);
            Assert.IsTrue(result);
            //check result
        }
    }
}
