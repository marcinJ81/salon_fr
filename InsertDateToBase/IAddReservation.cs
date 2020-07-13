using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salonfr.InsertReservation
{
    public interface IAddReservation
    {
        bool AddReservation(Reservation reservation);
    }
    public interface IAddClientAndServices
    {
       int AddClient(Client client);
       int AddServices(Services services);
    }
}
