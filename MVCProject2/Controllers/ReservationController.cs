using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCProject2.Models;
using salonfr;
using salonfr.QuerySelect;
using salonfr.SQLScripts;
using salonfrSource.ModelDB;
using salonfrSource.QuerySelect;

namespace MVCProject2.Controllers
{
    public class ReservationController : Controller
    {
        private ISelectTableObject<Client> selectClient;
        private ISelectTableObject<Services> selectServices;
        private ISelectReservation selectReservation;
        private IGetVReservation getVReservation;
        private ISelectTableObject<Employee> selectEmployee;


        public ReservationController()
        {
            this.selectClient = new SelectClient(); 
            this.selectServices = new SelectServices();
            this.selectReservation = new SelectReservation();
            this.selectEmployee = new SelectEmployee();
            this.getVReservation = new CreateViewVreservation(selectClient, selectReservation, selectServices, selectEmployee);
        }

        public IActionResult Index()
        {
            var reservationList = getVReservation.GetVReservations();
            var clientList = selectClient.GetRowsForTable(SGetAllRowsFromSpecificTable.ClientSelectAllRowsQuery());
            var serviceList = selectServices.GetRowsForTable(SGetAllRowsFromSpecificTable.ServicesSelectAllRowsQuery());
            var employreList = selectEmployee.GetRowsForTable(SGetAllRowsFromSpecificTable.EmployeeSelectAllRowsQuery());

            ViewBag.clientList = new MultiSelectList(clientList.Select( x => new 
            {
                key = x.client_id,
                value = x.client_name + " " +x.client_sname
            }), "key", "value");
            ViewBag.serviceList = new MultiSelectList(serviceList.Select(x => new
            {
                key = x.services_id,
                value = x.services_name
            }), "key", "value");
            ViewBag.employeeList = new MultiSelectList(employreList.Select(x => new
            {
                key = x.employee_id,
                value = x.employee_name
            }), "key", "value");

            return View(reservationList);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}