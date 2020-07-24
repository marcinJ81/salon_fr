﻿using salonfr.QuerySelect;
using salonfr.SQLScripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace salonfr.UserInterface
{
   public static  class ComboBoxSetData
    {
        private static ISelectClient selectClient;
        private static ISelectServices selectServices;
        public static void SetDataToCmbClient(ComboBox cmbClientList)
        {
            selectClient = new SelectClient();
            List<Client> clientList = new List<Client>();
            clientList.Add(new Client { client_id = 0, client_name = "klient" });
            foreach (var i in selectClient.GetClients(SGetAllRowsFromSpecificTable.ClientSelectAllRowsQuery()))
            {
                clientList.Add(new Client { client_id = i.client_id, client_name = i.client_name + " " + i.client_sname });
            }
            cmbClientList.DataSource = clientList;
            cmbClientList.DisplayMember = "client_name";
            cmbClientList.ValueMember = "client_id";
          
        }
        public static void SetDataToCmbServices(ComboBox cmbListServices)
        {
            selectServices = new SelectServices();
            List<Services> listServices = new List<Services>();
            listServices.Add(new Services { services_id = 0, services_name = "usługa" });
            foreach (var i in selectServices.GetServices(SGetAllRowsFromSpecificTable.ServicesSelectAllRowsQuery()))
            {
                listServices.Add(i);
            }
            cmbListServices.DataSource = listServices;
            cmbListServices.DisplayMember = "services_name";
            cmbListServices.ValueMember = "services_id";
        }
    }
}