using salonfr;
using salonfr.DBConnect;
using salonfr.QuerySelect;
using salonfr.SQLScripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace salonfrSource.UpdateObjectInBase
{
    public interface IUpdateObject<T>
    {
        bool UpdateObject(T dataobjectForChange, int id);
    }
    
    public class UpdateClient : IUpdateObject<Client>
    {
        private ISelectClient selectClient;

        public UpdateClient(ISelectClient selectClient)
        {
            this.selectClient = selectClient;
        }

        public bool UpdateObject(Client dataobjectForChange, int id)
        {
            var updateClient = SUpdateScripts.SqlLiteDBUpdateClient(dataobjectForChange, id);
            string result = DBConnectAndExecute.ExecuteQuery(updateClient);
            if (result != string.Empty)
            {
                return false;
            }
            return selectClient.GetClients(SGetAllRowsFromSpecificTable.ClientSelectAllRowsQuery())
                            .Any(x => x.client_id == id);
        }
    }
}
