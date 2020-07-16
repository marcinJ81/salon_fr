﻿using Microsoft.Data.Sqlite;
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
        List<Client> GetClients();
        int GetNextClientId();
    }
    public class SelectClient : ISelectClient
    {
        private string querySelect;
        private SqliteConnection sqliteConnection;

        public SelectClient(string querySelect)
        {
            this.querySelect = querySelect;
            this.sqliteConnection = new SqliteConnection(SDataSourceTableFilename.GetDirectoryFileDatabaseClient());
        }

        public List<Client> GetClients()
        {
            List<Client> result = new List<Client>();
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

        public int GetNextClientId()
        {
            int result = -1;
            try
            {
                sqliteConnection.Open();
                SqliteCommand sqliteCommand = new SqliteCommand(this.querySelect, this.sqliteConnection);
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
