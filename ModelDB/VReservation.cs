using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salonfr
{
    public class VReservation
    {
        public DateTime reservation_date { get; set; }
        public string client_name { get; set; }
        public string client_sname { get; set; }
        public string client_description { get; set; }
        public string services_name { get; set; }
        public int services_id { get; set; }
    }


}
