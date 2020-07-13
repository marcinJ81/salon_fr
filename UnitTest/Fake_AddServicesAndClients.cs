using salonfr.InsertReservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salonfr.UnitTest
{
    public class Fake_AddServicesAndClients : IAddClientAndServices
    {
        
        public int AddClient(Client client)
        {
            return 1;
        }

        public int AddServices(Services services)
        {
            return 1;
        }
    }
}
