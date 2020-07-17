using salonfr.DBConnect;
using salonfr.QuerySelect;
using salonfr.SQLScripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salonfr.InsertDateToBase
{
    public class DBInsertClient : IInsertToDB<Client>
    {
        private ISelectClient selectClient;
        public DBInsertClient(ISelectClient selectClient)
        {
            this.selectClient = selectClient;
        }
        public int InsertObjectToDB(Client dataObject)
        {
            var insertClient= SInsertScripts.SqlLiteDBInsertClient(dataObject);
            string result = DBConnectAndExecute.ExecuteQuery(insertClient);
            if (result != string.Empty)
            {
                return -1;
            }
            var lastAddedClient = selectClient.GetClients(SGetAllRowsFromSpecificTable.ClientSelectAllRowsQuery())
                            .Where(x => x.client_name == dataObject.client_name && 
                                        x.client_sname == dataObject.client_sname &&
                                        x.client_phone == dataObject.client_phone).First();
            return lastAddedClient.client_id;
        }
    }
}
