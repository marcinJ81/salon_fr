using salonfr.DBConnect;
using salonfr.QuerySelect;
using salonfr.SQLScripts;
using salonfrSource.Log;
using salonfrSource.ModelDB;
using salonfrSource.QuerySelect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salonfr
{
    public interface IFasadeInsertDB
    {
        int GetClientIdAndInsertToDB(string clientName, string clientSName, string clientPhone, string clientDescription);
        int GetServicesIdAndInsertDB(string servicesName);
        bool GetReservationIdAndInsertToDB(DateTime reservationDate, int reservationHour, int reservationMinute,
            int clientID, int servicesID, int employeeID);
        int GetEmployeeIdAndInsertToDB(string employeeName);
    }
    public class FasadeInsertDB : IFasadeInsertDB
    { 
        //----------------------------------------------
        private IInsertToDB<Client> insertClient;
        private ISelectTableObject<Client> selectClient;
        //----------------------------------------------
        private IInsertToDB<Services> insertServices;
        private ISelectTableObject<Services> selectServices;
        //----------------------------------------------
        private IInsertToDB<Reservation> insertReservation;
        private ISelectReservation selectReservation;
        //----------------------------------------------
        private IInsertToDB<Employee> insertEmployee;   
        private ISelectTableObject<Employee> selectEmployee;
        //----------------------------------------------
        public FasadeInsertDB(IInsertToDB<Client> insertClient, IInsertToDB<Services> insertServices, 
            IInsertToDB<Reservation> insertReservation, IInsertToDB<Employee> insertEmployee,
            ISelectTableObject<Client> selectClient, ISelectTableObject<Services> selectServices, 
            ISelectReservation selectReservation, ISelectTableObject<Employee> selectEmployee)
        {
            this.insertClient = insertClient;
            this.insertServices = insertServices;
            this.insertReservation = insertReservation;
            this.insertEmployee = insertEmployee;
            this.selectClient = selectClient;
            this.selectServices = selectServices;
            this.selectReservation = selectReservation;
            this.selectEmployee = selectEmployee;
        }

        public int GetClientIdAndInsertToDB(string clientName, string clientSName, string clientPhone, string clientDescription)
        {
            Client client = new Client()
            {
                client_id = selectClient.GetNextTabletId(SGetIdFromSpecificTable.queryGetLatestClientID()),
                client_name = clientName,
                client_sname = clientSName,
                client_phone = clientPhone,
                client_description = clientDescription
            };
            return insertClient.InsertObjectToDB(client);
        }

        public int GetEmployeeIdAndInsertToDB(string employeeName)
        {
            int employeID = -1;
            employeID = selectEmployee.GetNextTabletId(SGetIdFromSpecificTable.queryGetLatestEmployeeID());
            Employee employee = new Employee()
            {
                employee_id = employeID,
                employee_name = employeeName
            };
            return insertEmployee.InsertObjectToDB(employee);
        }

        public bool GetReservationIdAndInsertToDB(DateTime reservationDate, int reservationHour, int reservationMinute, int clientID, int servicesID, int employeeID)
        {
            int reservationID = selectReservation.GetNextReservationtId(SGetIdFromSpecificTable.queryGetLatestReservationID());
            Reservation reservation = new Reservation()
            {
                reservation_id = reservationID,
                reservation_date = reservationDate,
                reservation_time = new TimeSpan(reservationHour, reservationMinute, 0),
                client_id = clientID,
                services_id = servicesID,
                employee_id = employeeID
            };
            if (selectReservation.GetReservations(SGetAllRowsFromSpecificTable.ReservationSelectAllRowsQuery())
                    .Any(x => x.reservation_date == reservation.reservation_date
                    && x.reservation_time == reservation.reservation_time
                    && x.employee_id == reservation.employee_id))
            {

                return false;
            }
            SLogToFile.SaveDataTebleInToFile("reservation", reservation.ToString());
            insertReservation.InsertObjectToDB(reservation);
            return true;
        }

        public int GetServicesIdAndInsertDB(string servicesName)
        {
            Services services = new Services()
            {
                services_id = selectServices.GetNextTabletId(SGetIdFromSpecificTable.queryGetLatestServicesID()),
                services_name = servicesName
            };
            return insertServices.InsertObjectToDB(services);
        }
    }
}
