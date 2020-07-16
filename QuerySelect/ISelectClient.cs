using Microsoft.Data.Sqlite;
using salonfr.DBConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salonfr.QuerySelect
{
    public interface ISelectClient
    {
        List<Client> GetClients(string query);
        int GetNextClientId(string query);
    }
    public class SelectClient : ISelectClient
    {
        private SqliteConnection sqliteConnection;

        public SelectClient()
        {
            this.sqliteConnection = new SqliteConnection(SDataSourceTableFilename.GetDirectoryFileDatabaseClient());
        }

        public List<Client> GetClients(string query)
        {
            List<Client> result = new List<Client>();
            try
            {
                sqliteConnection.Open();
                SqliteCommand sqliteCommand = new SqliteCommand(query, this.sqliteConnection);
                sqliteCommand.ExecuteNonQuery();
                List<string> result2 = new List<string>();
                var rdr = sqliteCommand.ExecuteReader();
                result = GetAllRows(rdr);
                sqliteConnection.Close();
                return result;
            }
            catch (SqliteException ex)
            {
                string er = ex.Message;
                return result;
            }

        }

        public int GetNextClientId(string query)
        {
            int result = -1;
            try
            {
                sqliteConnection.Open();
                SqliteCommand sqliteCommand = new SqliteCommand(query, this.sqliteConnection);
                sqliteCommand.ExecuteNonQuery();
                List<string> result2 = new List<string>();
                var rdr = sqliteCommand.ExecuteReader();
                result = GetIDFromClientTable(rdr);
                sqliteConnection.Close();
                return result++;
            }
            catch (SqliteException ex)
            {
                string er = ex.Message;
                return result;
            }
        }
        private int GetIDFromClientTable(SqliteDataReader reader)
        {
            List<Client> result = new List<Client>();

            while (reader.HasRows)
            {
                while (reader.Read())
                {
                    result.Add(new Client
                    {
                        client_id = reader.GetInt32(0),
                    });
                }
                reader.NextResult();
            }
            return result.FirstOrDefault().client_id;
        }

        private List<Client> GetAllRows(SqliteDataReader reader)
        {
            List<Client> result = new List<Client>();

            while (reader.HasRows)
            {
                while (reader.Read())
                {
                    result.Add(new Client
                    {
                        client_id = reader.GetInt32(0),
                        client_name = reader.GetString(1),
                        client_sname = reader.GetString(2),
                        client_phone = reader.GetString(3),
                        client_description = reader.GetString(4)
                    });
                }
                reader.NextResult();
            }
            return result;
        }
    }
}
