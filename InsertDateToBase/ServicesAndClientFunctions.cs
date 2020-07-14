using salonfr.DBConnect;
using salonfr.InsertReservation;
using System;

namespace salonfr.InsertDateToBase
{
    public class ServicesAndClientFunctions : IAddClientAndServices
    {
        private IInsertToDB<Client> addClient;
        private IInsertToDB<Services> addServices;

        public ServicesAndClientFunctions(IInsertToDB<Client> addClient, IInsertToDB<Services> addServices)
        {
            this.addClient = addClient;
            this.addServices = addServices;
        }

        public int AddClient(Client client)
        {
            DBConnectAndExecute.ExecuteQuery();
        }

        public int AddServices(Services services)
        {
            throw new NotImplementedException();
        }
    }
}
