using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salonfr
{
    public enum DataTypeFromDB
    {
        Reservation,
        Client,
        Services
    }
    public class QueQueryToExecute
    {
        public QueQueryToExecute(int que_id, DataTypeFromDB dataType, string queryString, string query_error, bool query_result)
        {
            this.que_id = que_id;
            this.dataType = dataType;
            this.queryString = queryString;
            this.query_error = query_error;
            this.query_result = query_result;
        }

        public int que_id { get; set; }
        public DataTypeFromDB dataType { get; set; }
        public string queryString { get; set; }
        public string query_error { get; set; }
        public bool query_result { get; set; }
    }
}
