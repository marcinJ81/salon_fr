using salonfr.QuerySelect;
using salonfr.SQLScripts;
using salonfrSource.QuerySelect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salonfr
{
    public interface IGetVReservation
    {
        List<VReservation> GetVReservations();
    }
    public class CreateViewVreservation : IGetVReservation
    {
        private ISelectClient selectClient;
        private ISelectReservation selectReservation;
        private ISelectServices selectServices;
        private ISelectEmployee selectEmployee;

        public CreateViewVreservation(ISelectClient selectClient, ISelectReservation selectReservation,
            ISelectServices selectServices, ISelectEmployee selectEmployee)
        {
            this.selectClient = selectClient;
            this.selectReservation = selectReservation;
            this.selectServices = selectServices;
            this.selectEmployee = selectEmployee;
        }
        public List<VReservation> GetVReservations()
        {
            var clientsList = selectClient.GetClients(SGetAllRowsFromSpecificTable.ClientSelectAllRowsQuery());
            var servicesList = selectServices.GetServices(SGetAllRowsFromSpecificTable.ServicesSelectAllRowsQuery());
            var reservationList = selectReservation.GetReservations(SGetAllRowsFromSpecificTable.ReservationSelectAllRowsQuery());
            var employeeList = selectEmployee.GetEmployes(SGetAllRowsFromSpecificTable.EmployeeSelectAllRowsQuery());

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
                    empID = x.employee_id
                }).Join(employeeList,
                x => x.empID,
                y => y.employee_id,
                (x,y) => new
                {
                    clName = x.clName,
                    clSname = x.clSname,
                    clPhone = x.clPhone,
                    clDesc = x.clDesc,
                    resId = x.resId,
                    resDate = x.resDate,
                    resTime = x.resTime,
                    serId = x.serId,
                    empName = y.employee_name
                })
                .Join(servicesList,
                x => x.serId,
                y => y.services_id,
                (x, y) => new VReservation
                {
                    reservation_id = x.resId,
                    reservation_date = x.resDate,
                    reservation_time = x.resTime,
                    client_name = x.clName,
                    client_sname = x.clSname,
                    client_phone = x.clPhone,
                    client_description = x.clDesc,
                    services_name = y.services_name,
                    services_id = y.services_id,
                    employee_name = x.empName
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
    }


}
