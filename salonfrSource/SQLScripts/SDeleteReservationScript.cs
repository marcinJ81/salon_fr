using salonfrSource.DBConnect;
using System;
using System.Collections.Generic;
using System.Text;

namespace salonfrSource.SQLScripts
{
    public static class SDeleteReservationScript
    {
        private static string GetPathDBFile()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        }
        public static TableScripts SqlLiteDBDeleteReservation(int reservation_id)
        {
            TableScripts tableInsertScripts;
            tableInsertScripts =
                new TableScripts
                {
                    nameTable = @"\reservation.db",
                    script = @"DELETE FROM reservation WHERE reservation_id="
                    + reservation_id.ToString(),
                    operationType = OperationType.delete,
                    connectionProperties = new Microsoft.Data.Sqlite.SqliteConnection(@"DataSource=" + GetPathDBFile() + @"\reservation.db")
                };
            return tableInsertScripts;
        }
    }
}
