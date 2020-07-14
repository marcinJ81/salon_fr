using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace salonfr.DBConnect
{
    public class SqlLiteDB
    {
        private string PathDBFile;
        private List<CreateTable> createTable;
        public SqlLiteDB()
        {
            PathDBFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            this.createTable = new List<CreateTable>()
            {
                new CreateTable
                {
                    nameTable = @"\client.db",
                    script =  @"CREATE TABLE IF NOT EXISTS Client
                    (
                        client_id INTEGER IDENTITY PRIMARY KEY,
                        client_name VARCHAR  NULL,
                        client_sname VARCHAR  NULL,
                        client_description VARCHAR  NULL
                    );"
                },
                new CreateTable
                {
                    nameTable = @"\services.db",
                    script =
                     @"CREATE TABLE IF NOT EXISTS Services
                        (
                            services_id INTEGER IDENTITY PRIMARY KEY,
                            client_name VARCHAR  NULL
                        );"
                },
                new CreateTable
                {
                    nameTable = @"\reservation.db",
                    script =
                    @"CREATE TABLE IF NOT EXISTS Reservation
                    (
                        reservation_id INTEGER IDENTITY PRIMARY KEY,
                        reservation_date DATETIME  NULL,
                        client_id INT  NULL,
                        services_id INT  NULL
                    );"
                }
            };
            CreateTableAllTypes();  
        }
        private bool CreateTableAllTypes()
        {
            bool result = false;
            if (!this.createTable.Any())
            {
                return false;
            }
            foreach(var i in createTable)
            {
                SqliteConnection con = new SqliteConnection(@"DataSource=" + PathDBFile + i.nameTable);
                result = DBConnect.ExecuteQuery(i.script, con) == string.Empty ? true : false;
                if (!result)
                    return false;
            }
            return result; 
        }

    }
    public class CreateTable
    {
        public string nameTable { get; set; }
        public string script { get; set; }
    }
    public static class DBConnect
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
