using Microsoft.Data.Sqlite;
using salonfr.DBConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salonfr.QuerySelect
{
    public interface ISelectServices
    {
        List<Services> GetServices();
    }

    public class SelectServices : ISelectServices
    {
        private string querySelect;
        private SqliteConnection sqliteConnection;

        public SelectServices(string querySelect)
        {
            this.querySelect = querySelect;
            this.sqliteConnection = new SqliteConnection(SDataSourceTableFilename.GetDirectoryFileDatabaseReservation());
        }

        public List<Services> GetServices()
        {
            List<Services> result = new List<Services>();
            try
            {
                sqliteConnection.Open();
                SqliteCommand sqliteCommand = new SqliteCommand(this.querySelect, this.sqliteConnection);
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
