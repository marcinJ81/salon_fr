﻿using Microsoft.Data.Sqlite;
using salonfrSource.DBConnect;
using salonfrSource.ModelDB;
using salonfrSource.QuerySelect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salonfrSource.QuerySelect
{

    public class SelectServices : ISelectTableObject<Services>
    {
        private SqliteConnection sqliteConnection;

        public SelectServices()
        {
            this.sqliteConnection = new SqliteConnection(SDataSourceTableFilename.GetDirectoryFileDatabaseServices());
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
                result = GetIDFromServicesTable(rdr);
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

        public List<Services> GetRowsForTable(string query)
        {
            List<Services> result = new List<Services>();
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
        private int GetIDFromServicesTable(SqliteDataReader reader)
        {
            List<Services> result = new List<Services>();
            if (!reader.HasRows)
                return 0;
            while (reader.HasRows)
            {
                while (reader.Read())
                {
                    result.Add(new Services
                    {
                        services_id = reader.GetInt32(0),
                    });
                }
                reader.NextResult();
            }
            return result.FirstOrDefault().services_id;
        }
    }
}
