using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salonfr
{
    public class Reservation
    {
        public int reservation_id { get; set; }
        public DateTime reservation_date { get; set; }
        public int client_id { get; set; }
        public int services_id { get; set; }
    }
}
