using salonfrSource.QuerySelect;
using salonfrSource.ModelDB;
using salonfrSource.SQLScripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salonfrSource.ModelDB
{
    public interface IGetVReservation
    {
        List<VReservation> GetVReservations();
    }
    public class CreateViewVreservation : IGetVReservation
    {
        private ISelectTableObject<Client> selectClient;
        private ISelectTableObject<Reservation> selectReservation;
        private ISelectTableObject<Services> selectServices;
        private ISelectTableObject<Employee> selectEmployee;

        public CreateViewVreservation(ISelectTableObject<Client> selectClient, ISelectTableObject<Reservation> selectReservation,
            ISelectTableObject<Services> selectServices, ISelectTableObject<Employee> selectEmployee)
        {
            this.selectClient = selectClient;
            this.selectReservation = selectReservation;
            this.selectServices = selectServices;
            this.selectEmployee = selectEmployee;
        }
        public List<VReservation> GetVReservations()
        {
            var clientsList = selectClient.GetRowsForTable(SGetAllRowsFromSpecificTable.ClientSelectAllRowsQuery());
            var servicesList = selectServices.GetRowsForTable(SGetAllRowsFromSpecificTable.ServicesSelectAllRowsQuery());
            var reservationList = selectReservation.GetRowsForTable(SGetAllRowsFromSpecificTable.ReservationSelectAllRowsQuery());
            var employeeList = selectEmployee.GetRowsForTable(SGetAllRowsFromSpecificTable.EmployeeSelectAllRowsQuery());

            var result = reservationList.Join(clientsList,
                x => x.client_id,
                y => y.client_id,
                (x, y) => new
                {
                    clName = y.client_name,
                    clSname = y.client_sname,
                    clPhone = y.client_phone,
                    clDesc = y.client_description,
                    resId = x.reservation_id,
                    resDate = x.reservation_date,
                    resTime = x.reservation_time,
                    serId = x.services_id,
                    empID = x.employee_id,
                    clId = y.client_id
                })
                .Join(servicesList,
                x => x.serId,
                y => y.services_id,
                (x, y) => new 
                {
                    x.resId,
                    x.resDate,
                    x.resTime,
                    x.clName,
                    x.clSname,
                    x.clPhone,
                    x.clDesc,
                    y.services_name,
                    y.services_id,
                    x.empID,
                    x.clId
                })
                .Join(employeeList,
                x => x.empID,
                y => y.employee_id,
                (x, y) => new
                {
                    x.clName,
                    x.clSname,
                    x.clPhone,
                    x.clDesc,
                    x.resId,
                    x.resDate,
                    x.resTime,
                    x.services_id,
                    x.services_name,
                    empName = y.employee_name,
                    x.empID,
                    x.clId
                }).Select(x => new VReservation
                {
                    reservation_id = x.resId,
                    reservation_date = x.resDate,
                    reservation_time = x.resTime,
                    client_name = x.clName,
                    client_sname = x.clSname,
                    client_phone = x.clPhone,
                    client_description = x.clDesc,
                    services_name = x.services_name,
                    services_id = x.services_id,
                    employee_name = x.empName,
                    client_id = x.clId,
                    employee_id = x.empID,
                    client_data = x.clName + " " + x.clSname + " " + x.clPhone + " " + x.clDesc
                }).OrderByDescending(x => x.reservation_date.ToShortDateString()).ThenByDescending(y => y.reservation_time).ToList();

            return result;
 
        }
    }
    public class VReservation
    {
        public int reservation_id { get; set; }
        public DateTime reservation_date { get; set; }
        public TimeSpan reservation_time { get; set; }
        public string client_name { get; set; }
        public string client_sname { get; set; }
        public string client_description { get; set; }
        public string client_phone { get; set; }
        public string services_name { get; set; }
        public int services_id { get; set; }
        public string employee_name { get; set; } 
        public int employee_id { get; set; }
        public int client_id { get; set; }
        public string client_data { get; set; }
    }


}
