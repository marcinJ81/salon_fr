using salonfr.DBConnect;
using salonfr.QuerySelect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salonfr.InsertDateToBase
{
    public class DBInsertClient : IInsertToDB<Client>
    {
        private ISelectClient selectClient;

        public int InsertObjectToDB(Client dataObject)
        {
            throw new NotImplementedException();
        }
    }
}
