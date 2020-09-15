using salonfrSource.ModelDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salonfr.ViewClientsAndServices
{
    public interface IShowClientsAndServices
    {
        List<Client> GetAllClients();
        List<Services> GetAllServices();
    }
}
