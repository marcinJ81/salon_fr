using salonfrSource.ModelDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salonfrSource
{
    public class ViewReservation :  IViewSpecificReservation
    {
        private IViewReservation allReservations;

        public ViewReservation(IViewReservation allReservations)
        {
            this.allReservations = allReservations;
        }

        public List<VReservation> ReservationByDa(DateTime day)
        {
            if (!allReservations.AllReservation().Any(x => x.reservation_date.ToShortDateString() == day.ToShortDateString()))
            {
                return allReservations.EmptyReservation();
            }
            var result = allReservations.
                        AllReservation()
                        .Where(x => x.reservation_date.ToShortDateString() == day.ToShortDateString()).ToList();
            return result;
        }

        public List<VReservation> ResrvationByServices(string servicesType)
        {
            if (!allReservations.AllReservation().Any(x => x.services_name == servicesType))
            {
                return allReservations.EmptyReservation();
            }
            var result = allReservations.
                        AllReservation()
                        .Where(x => x.services_name == servicesType).ToList();
            return result;
        }
    }
}
