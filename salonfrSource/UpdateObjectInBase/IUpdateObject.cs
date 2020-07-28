using salonfr;
using salonfr.DBConnect;
using salonfr.QuerySelect;
using salonfr.SQLScripts;
using salonfrSource.QuerySelect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace salonfrSource.UpdateObjectInBase
{
    public interface IUpdateObject<T>
    {
        bool UpdateObject(T dataobjectForChange, int id);
        bool VerifyUpdateData(T newData, T modifiedData);
    }
    
    public class UpdateClient : IUpdateObject<Client>
    {
        private ISelectTableObject<Client> selectClient;

        public UpdateClient(ISelectTableObject<Client> selectClient)
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

           return VerifyUpdateData(dataobjectForChange, selectClient.GetRowsForTable(SGetAllRowsFromSpecificTable.ClientSelectAllRowsQuery())
                .Where(x => x.client_id == id).First());
        }

        public bool VerifyUpdateData(Client newData, Client modiefiedData)
        {
            List<bool> listError = new List<bool>();
            listError.Add(newData.client_name == modiefiedData.client_name ? true : false);
            listError.Add(newData.client_sname == modiefiedData.client_sname ? true : false);
            listError.Add(newData.client_description == modiefiedData.client_description ? true : false);
            listError.Add(newData.client_phone == modiefiedData.client_phone ? true : false);
            return !listError.Any(x => !x);
        }
    }
}
