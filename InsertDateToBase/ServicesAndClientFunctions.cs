using salonfr.DBConnect;
using salonfr.InsertReservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            throw new NotImplementedException();
        }

        public int AddServices(Services services)
        {
            throw new NotImplementedException();
        }
    }
}
