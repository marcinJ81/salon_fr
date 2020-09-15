using salonfrSource.ModelDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salonfrSource
{
   public interface IViewReservation
    {
        List<VReservation> AllReservation();
        List<VReservation> EmptyReservation();
    }

    public interface IViewSpecificReservation
    {
        List<VReservation> ReservationByDa(DateTime day);
        List<VReservation> ResrvationByServices(string servicesType);
    }
}
