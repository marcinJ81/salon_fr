using salonfr.DBConnect;
using salonfr.QuerySelect;
using salonfr.SQLScripts;
using salonfrSource.ModelDB;
using salonfrSource.QuerySelect;
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
        public DBInsertClient(ISelectClient selectClient)
        {
            this.selectClient = selectClient;
        }
        public int InsertObjectToDB(Client dataObject)
        {
            var insertClient= SInsertScripts.SqlLiteDBInsertClient(dataObject);
            string result = DBConnectAndExecute.ExecuteQuery(insertClient);
            if (result != string.Empty)
            {
                return -1;
            }
            var lastAddedClient = selectClient.GetClients(SGetAllRowsFromSpecificTable.ClientSelectAllRowsQuery())
                            .Where(x => x.client_id == dataObject.client_id).First();
            return lastAddedClient.client_id;
        }
    }

    public class DBInsertServices : IInsertToDB<Services>
    {
        private ISelectServices selectServices;

        public DBInsertServices(ISelectServices selectServices)
        {
            this.selectServices = selectServices;
        }

        public int InsertObjectToDB(Services dataObject)
        {
            var insertServices = SInsertScripts.SqlLiteDBInsertServices(dataObject);
            string result = DBConnectAndExecute.ExecuteQuery(insertServices);
            if (result != string.Empty)
            {
                return -1;
            }
            var lastAddedServices = selectServices.GetServices(SGetAllRowsFromSpecificTable.ServicesSelectAllRowsQuery())
                            .Where(x => x.services_id == dataObject.services_id ).First();
            return lastAddedServices.services_id;
        }
    }

    public class DBInsertReservation : IInsertToDB<Reservation>
    {
        private ISelectReservation selectReservation;

        public DBInsertReservation(ISelectReservation selectReservation)
        {
            this.selectReservation = selectReservation;
        }

        public int InsertObjectToDB(Reservation dataObject)
        {
            var insertReservation = SInsertScripts.SqlLiteDBInsertReservation(dataObject,dataObject.client_id,dataObject.services_id);
            string result = DBConnectAndExecute.ExecuteQuery(insertReservation);
            if (result != string.Empty)
            {
                return -1;
            }
            var lastAddedReservation = selectReservation.GetReservations(SGetAllRowsFromSpecificTable.ReservationSelectAllRowsQuery())
                            .Where(x => x.reservation_id == dataObject.reservation_id).First();
            return lastAddedReservation.reservation_id;
        }
    }
    public class DBInsertEmployee : IInsertToDB<Employee>
    {
        private ISelectEmployee selectEmployee;

        public DBInsertEmployee(ISelectEmployee selectEmpoloyee)
        {
            this.selectEmployee = selectEmpoloyee;
        }

        public int InsertObjectToDB(Employee dataObject)
        {
            var insertEmployes = SInsertScripts.SqlLiteDBInsertEmployee(dataObject);
            string result = DBConnectAndExecute.ExecuteQuery(insertEmployes);
            if (result != string.Empty)
            {
                return -1;
            }
            var lastAddedServices = selectEmployee.GetEmployes(SGetAllRowsFromSpecificTable.EmployeeSelectAllRowsQuery())
                            .Where(x => x.employee_id == dataObject.employee_id).First();
            return lastAddedServices.employee_id;
        }
    }
}
