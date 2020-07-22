using NUnit.Framework;
using salonfr.DBConnect;
using System;
using System.Collections.Generic;
using System.Text;

namespace salonfrSource.UnitTest
{
    class Test_UpdateClientServices
    {
        [Test]
        public void ShouldUpdateClient_when_IHaveHisId_returnTrue()
        {
            SqlLiteDB.SqlLiteDBCreateTable();
            //search client

            //get his client_id
            //change his data
            //check result
        }
    }
}
