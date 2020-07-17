using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salonfr.DBConnect
{
    public interface IInsertToDB<T>
    {
        int InsertObjectToDB(T dataObject);
    }
    public class DBCrud<T> : IInsertToDB<T>
    {
        public int InsertObjectToDB(T dataObject)
        {
            //insert specific type of data
            if (dataObject == null)
            {
                return 0;
            }
            return 1;
        }
    }
}
