using salonfrSource.DBConnect;
using salonfrSource.Log;
using salonfrSource.SQLScripts;
using System;
using System.Collections.Generic;
using System.Text;

namespace salonfrSource.DeleteReservation
{
   public interface IDeleteReservation
    {
        bool DeleteReservation(int reservationId);
    }
    public class DeleteReservation : IDeleteReservation
    {
        bool IDeleteReservation.DeleteReservation(int reservationId)
        {
            var deleteReservation = SDeleteReservationScript.SqlLiteDBDeleteReservation(reservationId);
            string result = DBConnectAndExecute.ExecuteQuery(deleteReservation);
            if (result != string.Empty)
            {
                SLogToFile.SaveInfoInFile(result);
                return false;
            }
            return true;
        }
    }
}
