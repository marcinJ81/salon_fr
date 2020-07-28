using System;
using System.Collections.Generic;
using System.Text;

namespace salonfrSource.QuerySelect
{
   public interface ISelectTableObject<T>
    {
        List<T> GetRowsForTable(string query);
        int GetNextTabletId(string query);
    }
}
