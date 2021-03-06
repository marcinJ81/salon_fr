﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salonfrSource.SQLScripts
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
            return @"select reservation_id, reservation_date,reservation_time,client_id,services_id,employee_id from Reservation";
        }
        public static string EmployeeSelectAllRowsQuery()
        {
            return @"select employee_id, employee_name from Employee";
        }
    }
}
