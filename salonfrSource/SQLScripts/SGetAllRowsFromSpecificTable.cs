using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salonfr.SQLScripts
{
   public static class SGetAllRowsFromSpecificTable
    {
        public static string ClientSelectAllRowsQuery()
        {
            return @"select client_id, client_name,client_sname,client_phone,client_description from Client";
        }
        public static string ServicesSelectAllRowsQuery()
        {
            return @"select services_id, services_name from Services";
        }
        public static string ReservationSelectAllRowsQuery()
        {
            return @"select reservation_id, reservation_date,client_id,services_id from Reservation";
        }
    }
}
