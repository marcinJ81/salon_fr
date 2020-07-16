using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salonfr.QuerySelect
{
    public interface ISelectServices
    {
        List<Services> GetReservations(SqliteConnection sqliteConnection);
    }

    public class SelectServices : ISelectServices
    {
        private string querySelect;

        public SelectServices(string querySelect)
        {
            this.querySelect = querySelect;
        }

        public List<Services> GetReservations(SqliteConnection sqliteConnection)
        {
            List<Services> result = new List<Services>();
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

        private List<Services> GetAllRows(SqliteDataReader reader)
        {
            List<Services> result = new List<Services>();

            while (reader.HasRows)
            {
                while (reader.Read())
                {
                    result.Add(new Services
                    {
                        services_id = reader.GetInt32(0),
                        services_name = reader.GetString(1)
                    });
                }
                reader.NextResult();
            }
            return result;
        }
    }
}
