using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salonfr.QuerySelect
{
    public interface ISelectClient
    {
        List<Client> GetClients(SqliteConnection sqliteConnection);
    }
    public class SelectClient : ISelectClient
    {
        private string querySelect;

        public SelectClient(string querySelect)
        {
            this.querySelect = querySelect;
        }

        public List<Client> GetClients(SqliteConnection sqliteConnection)
        {
            List<Client> result = new List<Client>();
            try
            {
                sqliteConnection.Open();
                SqliteCommand sqliteCommand = new SqliteCommand(this.querySelect, sqliteConnection);
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
                        client_description = reader.GetString(3)
                    });
                }
                reader.NextResult();
            }
            return result;
        }
    }
}
