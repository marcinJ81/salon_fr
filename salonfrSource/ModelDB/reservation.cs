using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salonfrSource.ModelDB
{
    public class Reservation
    {
        public int reservation_id { get; set; }
        public DateTime reservation_date { get; set; }
        public TimeSpan reservation_time { get; set; }
        public int client_id { get; set; }
        public int services_id { get; set; }
        public int employee_id { get; set; }
        public override string ToString()
        {
            string result = String.Empty;
            result += "reservation_id;" + this.reservation_id.ToString()
                  + ";"
                  + "reservation_date"
                  + ";"
                  + reservation_date.ToShortDateString()
                  + ";"
                  + "reservation_time"
                  + ";"
                  + reservation_time.ToString()
                  + ";"
                  + "client_id"
                  + ";"
                  + client_id.ToString()
                  + ";"
                  + "services_id"
                  + ";"
                  + services_id.ToString()
                  + ";"
                  + "employee_id"
                  + employee_id.ToString();

            return result;
        }
        public  bool Equal(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            Reservation result = (Reservation)obj;
            if (result.client_id == 0)
            {
                return false;
            }
            if (result.employee_id == 0)
            {
                return false;
            }
            if (result.reservation_id == 0)
            {
                return false;
            }
            if (result.reservation_date == null)
            {
                return false;
            }
            if (result.reservation_time == null)
            {
                return false;
            }
            return true;
        }
    }
}
