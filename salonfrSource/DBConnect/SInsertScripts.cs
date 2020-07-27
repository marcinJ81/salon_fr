using salonfrSource.ModelDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salonfr.DBConnect
{
    public static class SInsertScripts
    {
        private static string GetPathDBFile()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        }
        public static TableScripts SqlLiteDBInsertClient(Client model)
        {
            TableScripts tableInsertScripts;

          tableInsertScripts =  new TableScripts
            {
                nameTable = @"\client.db",
                script = @"insert into Client values"
                + "(" + model.client_id.ToString() + ",'"
                + model.client_name + "','"
                + model.client_sname + "','"
                + model.client_phone + "','"
                + model.client_description + "')",
                operationType = OperationType.write,
                connectionProperties = new Microsoft.Data.Sqlite.SqliteConnection(@"DataSource=" + GetPathDBFile() + @"\client.db")
            };
           
            return tableInsertScripts;
        }

        public static TableScripts SqlLiteDBInsertServices(Services model)
        {
            TableScripts tableInsertScripts;
            tableInsertScripts =
                new TableScripts
                {
                    nameTable = @"\services.db",
                    script = @"insert into Services values"
                    + "(" + model.services_id.ToString() + ",'"
                    + model.services_name + "')",
                    operationType = OperationType.write,
                    connectionProperties = new Microsoft.Data.Sqlite.SqliteConnection(@"DataSource=" + GetPathDBFile() + @"\services.db")
                };
            return tableInsertScripts;
        }
        public static TableScripts SqlLiteDBInsertEmployee(Employee model)
        {
            TableScripts tableInsertScripts;
            tableInsertScripts =
                new TableScripts
                {
                    nameTable = @"\employee.db",
                    script = @"insert into Employee values"
                    + "(" + model.employee_id.ToString() + ",'"
                    + model.employee_name + "')",
                    operationType = OperationType.write,
                    connectionProperties = new Microsoft.Data.Sqlite.SqliteConnection(@"DataSource=" + GetPathDBFile() + @"\employee.db")
                };
            return tableInsertScripts;
        }

        public static TableScripts SqlLiteDBInsertReservation(Reservation model, int client_id, int services_id)
        {
            TableScripts tableInsertScripts;
            tableInsertScripts =
                new TableScripts
                {
                    nameTable = @"\reservation.db",
                    script = @"insert into Reservation values"
                    + "(" + model.reservation_id.ToString() + ",'"
                    + model.reservation_date + "','"
                    +model.reservation_time + "',"
                    + client_id.ToString() + ","
                    + services_id.ToString()
                    + ")",
                    operationType = OperationType.write,
                    connectionProperties = new Microsoft.Data.Sqlite.SqliteConnection(@"DataSource=" + GetPathDBFile() + @"\reservation.db")
                };
            return tableInsertScripts;
        }
    }
}
