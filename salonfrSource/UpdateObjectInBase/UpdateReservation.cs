using salonfrSource.DBConnect;
using salonfrSource.ModelDB;
using salonfrSource.QuerySelect;
using salonfrSource.SQLScripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace salonfrSource.UpdateObjectInBase
{
    public class UpdateReservation : IUpdateObject<Reservation>
    {
        private ISelectTableObject<Reservation> selectReservation;

        public UpdateReservation(ISelectTableObject<Reservation> selectReservation)
        {
            this.selectReservation = selectReservation;
        }

        public bool UpdateObject(Reservation dataobjectForChange, int id)
        {
            var updateReservation = SUpdateScripts.SqlLiteDBUpdateReservation(dataobjectForChange, id);
            string result = DBConnectAndExecute.ExecuteQuery(updateReservation);
            if (result != string.Empty)
            {
                return false;
            }

            return VerifyUpdateData(dataobjectForChange, selectReservation.GetRowsForTable(SGetAllRowsFromSpecificTable.ReservationSelectAllRowsQuery())
                 .Where(x => x.reservation_id == id).First());
        }

        public bool VerifyUpdateData(Reservation newData, Reservation modifiedData)
        {
            List<bool> listError = new List<bool>();
            listError.Add(newData.reservation_date == modifiedData.reservation_date ? true : false);
            listError.Add(newData.reservation_time == modifiedData.reservation_time ? true : false);
            listError.Add(newData.employee_id == modifiedData.employee_id ? true : false);
            listError.Add(newData.client_id == modifiedData.client_id ? true : false);
            listError.Add(newData.services_id == modifiedData.services_id ? true : false);
            return !listError.Any(x => !x);
        }
    }
}
