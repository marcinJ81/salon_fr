using salonfr.QuerySelect;
using salonfr.SQLScripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salonfr.UserInterface
{
    public static class SClientFullTextSearch
    {
        private static  ISelectClient selectClient;
        public static Client GetClientFullTextSearch(string searchText, out int clientId)
        {
            selectClient = new SelectClient();
            var result = selectClient.GetClients(SGetAllRowsFromSpecificTable.ClientSelectAllRowsQuery());
            int id = result.Select(x => new
            {
                id = x.client_id,
                text = x.client_name + " " + x.client_sname + " " + x.client_phone
            }).Where(y => y.text.Contains(searchText)).FirstOrDefault().id;
            var selectedClient = selectClient.GetClients(SGetAllRowsFromSpecificTable.ClientSelectAllRowsQuery()).Where(x => x.client_id == id).First();
            clientId = id;
            return selectedClient;
        }
    }
}
