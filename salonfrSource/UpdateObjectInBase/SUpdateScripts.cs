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
    }
}
