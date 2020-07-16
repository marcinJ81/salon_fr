using Microsoft.Data.Sqlite;
using salonfr.DBConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salonfr.QuerySelect
{
    public interface ISelectReservation
    {
        List<Reservation> GetReservations();
    }

    public class SelectReservation : ISelectReservation
    {
        private string querySelect;
        private SqliteConnection sqliteConnection;
        public SelectReservation(string querySelect)
        {
            this.querySelect = querySelect;
            this.sqliteConnection = new SqliteConnection(SDataSourceTableFilename.GetDirectoryFileDatabaseReservation());
        }
        public List<Reservation> GetReservations()
        {
            List<Reservation> result = new List<Reservation>();
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

        private List<Reservation> GetAllRows(SqliteDataReader reader)
        {
            List<Reservation> result = new List<Reservation>();

            while (reader.HasRows)
            {
                while (reader.Read())
                {
                    result.Add(new Reservation
                    {
                        reservation_id = reader.GetInt32(0),
                        reservation_date = DateTime.Parse(reader.GetString(1)),
                        client_id = reader.GetInt32(2),
                        services_id = reader.GetInt32(3)
                    });
                }
                reader.NextResult();
            }

            return result;
        }
    }
   
}
