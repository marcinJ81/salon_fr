﻿using System;
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
}
