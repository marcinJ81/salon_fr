using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salonfr
{
    public static class SGetIdFromSpecificTable
    {
        public static string queryGetLatestClientID()
        {
            return "select  client_id from Client order by client_id desc";
        }
        public static string queryGetLatestServicesID()
        {
            return "select  services_id from Services order by services_id desc";
        }
        public static string queryGetLatestReservationID()
        {
            return "select reservation_id from Reservation order by reservation_id desc";
        }
        public static string queryGetLatestEmployeeID()
        {
            return "select employee_id from Employee order by employee_id desc";
        }

    }
}
