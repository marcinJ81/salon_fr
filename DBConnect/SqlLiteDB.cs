using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salonfr.DBConnect
{
   public class SqlLiteDB
    {
        private string createTable_Reservation;
        private string createTable_Client;
        private string createTable_Services;

        public SqlLiteDB()
        {
            this.createTable_Client =
                @"CREATE TABLE IF NOT EXISTS Client
                (
                    client_id INTEGER IDENTITY PRIMARY KEY,
                    client_name VARCHAR  NULL,
                    client_sname VARCHAR  NULL,
                    client_description VARCHAR  NULL
                );";
            this.createTable_Services =
                @"CREATE TABLE IF NOT EXISTS Services
                (
                    services_id INTEGER IDENTITY PRIMARY KEY,
                    client_name VARCHAR  NULL
                );";
            this.createTable_Reservation =
                @"CREATE TABLE IF NOT EXISTS Reservation
                (
                    reservation_id INTEGER IDENTITY PRIMARY KEY,
                    reservation_date DATETIME  NULL,
                    client_id INT  NULL,
                    services_id INT  NULL
                );";
        }
        private bool CreateTableClient()
        {
            SqliteConnection con = new SqliteConnection(@"DataSource=E:\SQLite_sklep\client.db");
            string query = this.createTable_Client;
            return ExecuteQuery(query, con) == string.Empty ? false : true;
        }
        private bool CreateTableServices()
        {
            SqliteConnection con = new SqliteConnection(@"DataSource=E:\SQLite_sklep\services.db");
            string query = this.createTable_Services;
            return ExecuteQuery(query, con) == string.Empty ? false : true;
        }
        private bool CreateTableReservation()
        {
            SqliteConnection con = new SqliteConnection(@"DataSource=E:\SQLite_sklep\reservation.db");
            string query = this.createTable_Reservation;
            return ExecuteQuery(query, con) == string.Empty ? false : true;
        }
        private string ExecuteQuery(string query, SqliteConnection connection)
        {
            SqliteCommand com0 = new SqliteCommand(query, connection);
            try
            {
                com0.ExecuteNonQuery();
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
