using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace salonfrSource.ModelDB
{
    public class Reservation
    {
        [Required]
        public int reservation_id { get; set; }
        [Required]
        public DateTime reservation_date { get; set; }
        [Required] 
        public TimeSpan reservation_time { get; set; }
        [Required]
        public int client_id { get; set; }
        [Required]
        public int services_id { get; set; }
        [Required]
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
       
    }
}
