using Microsoft.Data.Sqlite;
using salonfrSource.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salonfr.DBConnect
{
    public static class DBConnectAndExecute
    {
        public static string ExecuteQuery(string query, SqliteConnection connection)
        {
            using (var com0 = new SqliteCommand(query, connection))
            {
                try
                {
                    connection.Open();
                    com0.ExecuteNonQuery();
                    connection.Close();
                    return string.Empty;
                }
                catch (SqliteException ex)
                {
                    SLogToFile.SaveInfoInFile("|ExecuteQuery|" + ex.Message);
                    string er = ex.Message;
                    return er;
                }
            }
        }
        public static string ExecuteQuery(TableScripts tableScripts)
        {
            using (var com0 = new SqliteCommand(tableScripts.script, tableScripts.connectionProperties))
            {
                try
                {
                    tableScripts.connectionProperties.Open();
                    com0.ExecuteNonQuery();
                    tableScripts.connectionProperties.Close();
                    return string.Empty;
                }
                catch (SqliteException ex)
                {
                    SLogToFile.SaveInfoInFile("|ExecuteQuery|" + ex.Message);
                    string er = ex.Message;
                    return er;
                }
            }
        }

    }
}
