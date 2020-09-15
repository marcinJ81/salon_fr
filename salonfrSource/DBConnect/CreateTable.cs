using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salonfrSource.DBConnect
{
    public class TableScripts
    {
        public string nameTable { get; set; }
        public string script { get; set; }
        public OperationType operationType { get; set; }
        public SqliteConnection connectionProperties { get; set;}
    }
}
