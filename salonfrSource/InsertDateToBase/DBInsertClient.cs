﻿using salonfrSource.QuerySelect;
using salonfrSource.DBConnect;
using salonfrSource.ModelDB;
using salonfrSource.SQLScripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salonfrSource.InsertDateToBase
{
    public class DBInsertClient : IInsertToDB<Client>
    {
        private ISelectTableObject<Client> selectClient;
        public DBInsertClient(ISelectTableObject<Client> selectClient)
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
            //this is not nessery because i passes in client_id in the parameter
            var lastAddedClient = selectClient.GetRowsForTable(SGetAllRowsFromSpecificTable.ClientSelectAllRowsQuery())
                            .Where(x => x.client_id == dataObject.client_id).First();
            return lastAddedClient.client_id;
        }
    }

    public class DBInsertServices : IInsertToDB<Services>
    {
        private ISelectTableObject<Services>  selectServices;

        public DBInsertServices(ISelectTableObject<Services> selectServices)
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
            var lastAddedServices = selectServices.GetRowsForTable(SGetAllRowsFromSpecificTable.ServicesSelectAllRowsQuery())
                            .Where(x => x.services_id == dataObject.services_id ).First();
            return lastAddedServices.services_id;
        }
    }

    public class DBInsertReservation : IInsertToDB<Reservation>
    {
        private ISelectTableObject<Reservation> selectReservation;

        public DBInsertReservation(ISelectTableObject<Reservation> selectReservation)
        {
            this.selectReservation = selectReservation;
        }

        public int InsertObjectToDB(Reservation dataObject)
        {
            var insertReservation = SInsertScripts.SqlLiteDBInsertReservation(dataObject,dataObject.client_id,dataObject.services_id,dataObject.employee_id);
            string result = DBConnectAndExecute.ExecuteQuery(insertReservation);
            if (result != string.Empty)
            {
                return -1;
            }
            var lastAddedReservation = selectReservation.GetRowsForTable(SGetAllRowsFromSpecificTable.ReservationSelectAllRowsQuery())
                            .Where(x => x.reservation_id == dataObject.reservation_id).First();
            return lastAddedReservation.reservation_id;
        }
    }
    public class DBInsertEmployee : IInsertToDB<Employee>
    {
        private ISelectTableObject<Employee> selectEmployee;

        public DBInsertEmployee(ISelectTableObject<Employee> selectEmpoloyee)
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
            var lastAddedServices = selectEmployee.GetRowsForTable(SGetAllRowsFromSpecificTable.EmployeeSelectAllRowsQuery())
                            .Where(x => x.employee_id == dataObject.employee_id).First();
            return lastAddedServices.employee_id;
        }
    }
}
