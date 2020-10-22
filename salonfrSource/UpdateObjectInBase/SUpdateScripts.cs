using salonfrSource;
using salonfrSource.DBConnect;
using salonfrSource.ModelDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace salonfrSource.UpdateObjectInBase
{
   public static class SUpdateScripts
    {
        private static string GetPathDBFile()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        }
        public static TableScripts SqlLiteDBUpdateReservation(Reservation model, int reservation_id)
        {
            TableScripts tableInsertScripts;
            tableInsertScripts = new TableScripts
            {
                nameTable = @"\reservation.db",
                script = @"update Reservation "
                + " set "
                + " reservation_date= " + "'" + model.reservation_date + "',"
                + " reservation_time= " + "'" + model.reservation_time + "',"
                + " client_id= " + "'" + model.client_id + "',"
                + " services_id= " + "'" + model.services_id + "',"
                + " employee_id= " + "'" + model.employee_id + "'"
                + " where reservation_id=" + reservation_id.ToString(),
                operationType = OperationType.update,
                connectionProperties = new Microsoft.Data.Sqlite.SqliteConnection(@"DataSource=" + GetPathDBFile() + @"\reservation.db")
            };
            return tableInsertScripts;
        }
        public static TableScripts SqlLiteDBUpdateClient(Client model, int client_id)
        {
            TableScripts tableInsertScripts;

            tableInsertScripts = new TableScripts
            {
                nameTable = @"\client.db",
                script = @"update Client "
                  + " set " 
                  + " client_name= " + "'" + model.client_name + "',"
                  + " client_sname= " + "'" + model.client_sname + "',"
                  + " client_phone= " + "'" + model.client_phone + "',"
                  + " client_description= " + "'" + model.client_description + "'" 
                  + " where client_id=" + client_id.ToString(),
                operationType = OperationType.update,
                connectionProperties = new Microsoft.Data.Sqlite.SqliteConnection(@"DataSource=" + GetPathDBFile() + @"\client.db")
            };
            return tableInsertScripts;
        }

        public static TableScripts SqlLiteDBUpdateServices(Services model, int services_id)
        {
            TableScripts tableInsertScripts;

            tableInsertScripts = new TableScripts
            {
                nameTable = @"\services.db",
                script = @"update Services "
                  + " set "
                  + " services_name= " + "'" + model.services_name + "',"
                  + " where services_id=" + services_id.ToString(),
                operationType = OperationType.update,
                connectionProperties = new Microsoft.Data.Sqlite.SqliteConnection(@"DataSource=" + GetPathDBFile() + @"\services.db")
            };
            return tableInsertScripts;
        }

        public static TableScripts SqlLiteDBUpdateEmployee(Employee model, int employee_id)
        {
            TableScripts tableInsertScripts;

            tableInsertScripts = new TableScripts
            {
                nameTable = @"\employee.db",
                script = @"update Employee "
                  + " set "
                  + " employee_name= " + "'" + model.employee_name + "',"
                  + " where employee_id=" + employee_id.ToString(),
                operationType = OperationType.update,
                connectionProperties = new Microsoft.Data.Sqlite.SqliteConnection(@"DataSource=" + GetPathDBFile() + @"\employee.db")
            };
            return tableInsertScripts;
        }
    }
}
