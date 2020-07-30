using Microsoft.Data.Sqlite;
using salonfr.DBConnect;
using salonfrSource.ModelDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace salonfrSource.QuerySelect
{
     
    public class SelectEmployee :  ISelectTableObject<Employee>
    {
        private SqliteConnection sqliteConnection;

        public SelectEmployee()
        {
            this.sqliteConnection = new SqliteConnection(SDataSourceTableFilename.GetDirectoryFileDatabaseEmployee());
        }

        public List<Employee> GetEmployes(string query)
        {
            List<Employee> result = new List<Employee>();
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

        public int GetNextEmployeetId(string query)
        {
            int result = -1;
            try
            {
                sqliteConnection.Open();
                SqliteCommand sqliteCommand = new SqliteCommand(query, this.sqliteConnection);
                sqliteCommand.ExecuteNonQuery();
                List<string> result2 = new List<string>();
                var rdr = sqliteCommand.ExecuteReader();
                result = GetIDFromEmployeeTable(rdr);
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
        private int GetIDFromEmployeeTable(SqliteDataReader reader)
        {
            List<Employee> result = new List<Employee>();
            if (!reader.HasRows)
                return 0;
            while (reader.HasRows)
            {
                while (reader.Read())
                {
                    result.Add(new Employee
                    {
                        employee_id = reader.GetInt32(0),
                    });
                }
                reader.NextResult();
            }
            return result.FirstOrDefault().employee_id;
        }

        private List<Employee> GetAllRows(SqliteDataReader reader)
        {
            List<Employee> result = new List<Employee>();

            while (reader.HasRows)
            {
                while (reader.Read())
                {
                    result.Add(new Employee
                    {
                        employee_id = reader.GetInt32(0),
                        employee_name = reader.GetString(1),
                    });
                }
                reader.NextResult();
            }
            return result;
        }

        public List<Employee> GetRowsForTable(string query)
        {
            List<Employee> result = new List<Employee>();
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

        public int GetNextTabletId(string query)
        {
            int result = -1;
            try
            {
                sqliteConnection.Open();
                SqliteCommand sqliteCommand = new SqliteCommand(query, this.sqliteConnection);
                sqliteCommand.ExecuteNonQuery();
                List<string> result2 = new List<string>();
                var rdr = sqliteCommand.ExecuteReader();
                result = GetIDFromEmployeeTable(rdr);
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
