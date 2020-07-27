using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salonfr.DBConnect
{
   public static class SDataSourceTableFilename
    {
        public static string GetDirectoryFileDatabaseClient()
        {
            string PathDBFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string result;
            result = @"DataSource=" + PathDBFile + @"\client.db";
            return result;
        }
        public static string GetDirectoryFileDatabaseReservation()
        {
            string PathDBFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string result;
            result = @"DataSource=" + PathDBFile + @"\reservation.db";
            return result;
        }
        public static string GetDirectoryFileDatabaseServices()
        {
            string PathDBFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string result;
            result = @"DataSource=" + PathDBFile + @"\services.db";
            return result;
        }
        public static string GetDirectoryFileDatabaseEmployee()
        {
            string PathDBFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string result;
            result = @"DataSource=" + PathDBFile + @"\employee.db";
            return result;
        }
    }
}
