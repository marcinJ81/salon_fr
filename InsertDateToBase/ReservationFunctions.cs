using salonfr.DBConnect;
using salonfr.InsertReservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salonfr.InsertDateToBase
{
    public class ReservationFunctions : IAddReservation
    {
        private IAddClientAndServices addclientandservices;
        private IInsertToDB<Reservation> insertDB;
        public ReservationFunctions(IAddClientAndServices addclientandservices)
        {
            this.addclientandservices = addclientandservices;
            insertDB = new DBCrud<Reservation>();
        }
        public bool AddReservation(Reservation reservation)
        {

            return true;
        }
    }
}
