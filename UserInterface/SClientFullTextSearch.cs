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
            
            //error with non unique value in result
            selectClient = new SelectClient();
            List<Client> result = new List<Client>();
            result = selectClient.GetClients(SGetAllRowsFromSpecificTable.ClientSelectAllRowsQuery());
            var client = result.Select(x => new
            {
                id = x.client_id,
                text = x.client_name + " " + x.client_sname + " " + x.client_phone
            }).Where(y => y.text.Contains(searchText));
            if (client.Any() == false)
            {
                clientId = -1;
                return new Client { client_id = 0, client_name = "brak", client_phone = "brak", client_sname = "brak", client_description = "brak" };
            }

            var selectedClient = selectClient.GetClients(SGetAllRowsFromSpecificTable.ClientSelectAllRowsQuery()).Where(x => x.client_id == client.First().id).First();
            clientId = client.First().id;
            return selectedClient;
        }
    }
}
