using Microsoft.Data.Sqlite;
using salonfrSource.DBConnect;
using salonfrSource.ModelDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salonfrSource.QuerySelect
{
    public interface ISelectReservation
    {
        List<Reservation> GetReservations(string query);
        int GetNextReservationtId(string query);
    }

    public class SelectReservation : ISelectReservation
    {
        private SqliteConnection sqliteConnection;
        public SelectReservation()
        {
            this.sqliteConnection = new SqliteConnection(SDataSourceTableFilename.GetDirectoryFileDatabaseReservation());
        }
        public List<Reservation> GetReservations(string query)
        {
            List<Reservation> result = new List<Reservation>();
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

        private int GetIDFromReservationTable(SqliteDataReader reader)
        {
            List<Reservation> result = new List<Reservation>();
            if (!reader.HasRows)
                return 0;
            while (reader.HasRows)
            {
                while (reader.Read())
                {
                    result.Add(new Reservation
                    {
                        reservation_id = reader.GetInt32(0),
                    });
                }
                reader.NextResult();
            }
            return result.FirstOrDefault().reservation_id;
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
                            reservation_time = TimeSpan.Parse(reader.GetString(2)),
                            client_id = reader.GetInt32(3),
                            services_id = reader.GetInt32(4),
                            employee_id = reader.GetInt32(5)
                        });
                    }
                    reader.NextResult();
                }
            
           
            return result;
        }

        public int GetNextReservationtId(string query)
        {
            int result = -1;
            try
            {
                sqliteConnection.Open();
                SqliteCommand sqliteCommand = new SqliteCommand(query, this.sqliteConnection);
                sqliteCommand.ExecuteNonQuery();
                List<string> result2 = new List<string>();
                var rdr = sqliteCommand.ExecuteReader();
                result = GetIDFromReservationTable(rdr);
                sqliteConnection.Close();
                if (result == 0)
                    return 1;
                return ++result;
            }
            catch (SqliteException ex)
            {
                string er = ex.Message;
                return result;
            }
        }
    }
   
}
