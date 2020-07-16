using Microsoft.Data.Sqlite;
using NUnit.Framework;
using salonfr.DBConnect;
using salonfr.InsertDateToBase;
using salonfr.InsertReservation;
using salonfr.QuerySelect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salonfr.UnitTest
{
   public class Test_NewReservation
    {
        private IAddReservation addreservation;
        private IAddClientAndServices fake_addClientAndServices;
        [SetUp]
        public void Setup()
        {
            this.fake_addClientAndServices = new Fake_AddServicesAndClients();
            this.addreservation = new ReservationFunctions(fake_addClientAndServices);
        }
        [Test]
        public void ShouldAddNewReservation_ReturnTrue()
        {
            //SqlLiteDB.SqlLiteDBCreateTable();
            //CreateInsertScripts insert = new CreateInsertScripts();
            //var result2 = CreateInsertScripts.SqlLiteDBInsertReservation(
            //    new Reservation()
            //    {
            //        reservation_id = 10,
            //        reservation_date = new DateTime(2020,7,15)
            //    }, 1, 1
            //    ) ;
            //DBConnectAndExecute.ExecuteQuery(result2.First());
            string PathDBFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string query = @"select reservation_id, reservation_date,client_id,services_id from Reservation";
            SelectReservation selectReservation = new SelectReservation(query);
            
            selectReservation.GetReservations(new SqliteConnection(SDataSourceTableFilename.GetDirectoryFileDatabaseReservation()));

             Reservation newReservation = new Reservation()
            {
                reservation_id = 1,
                reservation_date = new DateTime(2020,7,13),
            };
            bool result = addreservation.AddReservation(newReservation);
            Assert.IsTrue(result);
        }
    }
}
