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
        [Required]
        public string client_phone { get; set; }
        public string client_description { get; set; }
    }
}
