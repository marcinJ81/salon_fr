using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salonfrSource.ModelDB
{
    public class Client
    {
        public int client_id { get; set; }
        public string client_name { get; set; }
        public string client_sname { get; set; }
        [StringLength(9, MinimumLength = 9,
        ErrorMessage = "Numer telefonu 9 znaków")]
        public string client_phone { get; set; }
        public string client_description { get; set; }
    }
}
