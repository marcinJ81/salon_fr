using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace salonfr.DBConnect
{
    public enum OperationType
    {
        create,
        read,
        write,
        update
    }
    public static class SqlLiteDB
    {
        private static string PathDBFile;
        public static List<TableScripts> createTable { get; private set; }

        public static void SqlLiteDBCreateTable()
        {
            PathDBFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            createTable = new List<TableScripts>()
            {
                new TableScripts
                {
                    nameTable = @"\client.db",
                    script =  @"CREATE TABLE IF NOT EXISTS Client
                    (
                        client_id INTEGER IDENTITY PRIMARY KEY,
                        client_name VARCHAR  NULL,
                        client_sname VARCHAR  NULL,
                        client_phone VARCHAR  NULL,
                        client_description VARCHAR  NULL
                    );",
                    operationType = OperationType.create
                },
                new TableScripts
                {
                    nameTable = @"\services.db",
                    script =
                     @"CREATE TABLE IF NOT EXISTS Services
                        (
                            services_id INTEGER IDENTITY PRIMARY KEY,
                            services_name VARCHAR  NULL
                        );",
                    operationType = OperationType.create
                },
                new TableScripts
                {
                    nameTable = @"\reservation.db",
                    script =
                    @"CREATE TABLE IF NOT EXISTS Reservation
                    (
                        reservation_id INTEGER IDENTITY PRIMARY KEY,
                        reservation_date DATETIME  NULL,
                        reservation_time DATETIME NULL,
                        client_id INT  NULL,
                        services_id INT  NULL
                    );",
                    operationType = OperationType.create
                }
            };
            CreateTableAllTypes();  
        }
       
        private static bool CreateTableAllTypes()
        {
            bool result = false;
            if (!createTable.Any())
            {
                return false;
            }
            foreach(var i in createTable)
            {
                SqliteConnection con = new SqliteConnection(@"DataSource=" + PathDBFile + i.nameTable);
                result = DBConnectAndExecute.ExecuteQuery(i.script, con) == string.Empty ? true : false;
                if (!result)
                    return false;
            }
            return result; 
        }

    }
   
   
}
