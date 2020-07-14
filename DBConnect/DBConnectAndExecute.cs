using Microsoft.Data.Sqlite;
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
                    string er = ex.Message;
                    return er;
                }
            }
        }

    }
}
