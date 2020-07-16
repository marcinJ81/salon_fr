using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salonfr.DBConnect
{
    public  class CreateInsertScripts
    {
        private  static List<TableScripts> tableInsertScripts { get; set; }
        private static string PathDBFile { get; set; }

        public CreateInsertScripts()
        {
            PathDBFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            tableInsertScripts = new List<TableScripts>();
        }
        public static List<TableScripts> SqlLiteDBInsertClient(Client model)
        {
           
            tableInsertScripts.Add
            (
                new TableScripts
                {
                    nameTable = @"\client.db",
                    script =  @"insert into Client values"
                    + "(" + model.client_id.ToString() + ",'"
                    + model.client_name + "','" 
                    + model.client_sname + "','" 
                    +model.client_phone + "','"
                    + model.client_description + "')",  
                    operationType = OperationType.write,
                    connectionProperties = new Microsoft.Data.Sqlite.SqliteConnection(@"DataSource=" + PathDBFile + @"\client.db")
                }  
            );
            return tableInsertScripts;
        }
        public static List<TableScripts> SqlLiteDBInsertServices(Services model)
        {
           
            tableInsertScripts.Add
            (
                new TableScripts
                {
                    nameTable = @"\services.db",
                    script = @"insert into Services values"
                    + "(" + model.services_id.ToString() + ",'"
                    + model.services_name  + "')",
                    operationType = OperationType.write,
                    connectionProperties = new Microsoft.Data.Sqlite.SqliteConnection(@"DataSource=" + PathDBFile + @"\services.db")
                }
            );
            return tableInsertScripts;
        }
        public static List<TableScripts> SqlLiteDBInsertReservation(Reservation model, int client_id,int services_id)
        {
           
            tableInsertScripts.Add
            (
                new TableScripts
                {
                    nameTable = @"\reservation.db",
                    script = @"insert into Reservation values"
                    + "(" + model.reservation_id.ToString() + ",'"
                    + model.reservation_date + "',"
                    + client_id.ToString() + ","
                    + services_id.ToString() 
                    + ")",
                    operationType = OperationType.write,
                    connectionProperties = new Microsoft.Data.Sqlite.SqliteConnection(@"DataSource=" + PathDBFile + @"\reservation.db")
                }
            );
            return tableInsertScripts;
        }
    }
    
}
