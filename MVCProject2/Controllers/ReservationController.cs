using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCProject2.Models;
using salonfrSource.QuerySelect;
using salonfrSource.ModelDB;
using salonfrSource.SQLScripts;
using salonfrSource;
using salonfrSource.InsertDateToBase;

namespace MVCProject2.Controllers
{
    public enum systemTableType
    {
        clientTable,
        serviceTable,
        employyeTable
    }
    public class ReservationController : Controller
    {
        private ISelectTableObject<Client> selectClient;
        private ISelectTableObject<Services> selectServices;
        private ISelectReservation selectReservation;
        private IGetVReservation getVReservation;
        private ISelectTableObject<Employee> selectEmployee;
        private IFasadeInsertDB insertObjectToDB;

        public ReservationController()
        {
            this.selectClient = new SelectClient(); 
            this.selectServices = new SelectServices();
            this.selectReservation = new SelectReservation();
            this.selectEmployee = new SelectEmployee();
            this.getVReservation = new CreateViewVreservation(selectClient, selectReservation, selectServices, selectEmployee);
            this.insertObjectToDB = new FasadeInsertDB(new DBInsertClient(selectClient), new DBInsertServices(selectServices), new DBInsertReservation(selectReservation),
                new DBInsertEmployee(selectEmployee), new SelectClient(), new SelectServices(), new SelectReservation(), new SelectEmployee());

        }

        public IActionResult Index(string dateReservation)
        {
            DateTime reservationdate;
            var reservationList = getVReservation.GetVReservations();
            if (!String.IsNullOrEmpty(dateReservation))
            {
                reservationdate = DateTime.Parse(dateReservation);
                reservationList = reservationList.Where(x => x.reservation_date.ToShortDateString() == reservationdate.ToShortDateString()).ToList();
            }
            return View(reservationList);
        }
       
        #region modalWindows_GET
        [HttpGet]
        public PartialViewResult AddReservation()
        {
            var clientList = selectClient.GetRowsForTable(SGetAllRowsFromSpecificTable.ClientSelectAllRowsQuery());
            var serviceList = selectServices.GetRowsForTable(SGetAllRowsFromSpecificTable.ServicesSelectAllRowsQuery());
            var employreList = selectEmployee.GetRowsForTable(SGetAllRowsFromSpecificTable.EmployeeSelectAllRowsQuery());

            ViewBag.clientList = new MultiSelectList(clientList.Select(x => new
            {
                key = x.client_id,
                value = x.client_name + " " + x.client_sname
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

            return PartialView();
        }
        [HttpGet]
        public PartialViewResult AddClient(int? client_id)
        {
            var allclientList = selectClient.GetRowsForTable(SGetAllRowsFromSpecificTable.ClientSelectAllRowsQuery());
            ViewBag.clientList = new MultiSelectList(allclientList.Select(x => new
            {
                key = x.client_id,
                value = x.client_name + " " + x.client_sname
            }), "key", "value");

            if (client_id != null)
            {
                var selectedClient = selectClient.GetRowsForTable(SGetAllRowsFromSpecificTable.ClientSelectAllRowsQuery())
                           .Where(x => x.client_id == client_id).ToList();
                return PartialView(selectedClient);
            }

            return PartialView();
        }
        [HttpGet]
        public PartialViewResult AddEmployee()
        {
            return PartialView();
        }
        [HttpGet]
        public PartialViewResult AddServices()
        {
            return PartialView();
        }
        [HttpGet]
        public PartialViewResult info1(string messageWindow)
        {
            ViewBag.InfoMessage = messageWindow;
            return PartialView();
        }
        #endregion
        #region modalWindows_POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddServices(Services services)
        {
            if (!ModelState.IsValid)
            {
                return AddServices(services);
            }
            else
            {
                int servicesID = insertObjectToDB.GetServicesIdAndInsertDB(services.services_name);
                return RedirectToAction("Index", "Reservation", new { visibleTrue = false });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEmployee(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return AddEmployee(employee);
            }
            else
            {

               int employeID = insertObjectToDB.GetEmployeeIdAndInsertToDB(employee.employee_name);
                
                return RedirectToAction("Index", "Reservation", new { visibleTrue = false });
            } 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddClient(Client client)
        {
            if ( !ModelState.IsValid)
            {
                return AddClient(client);
            }
            else
            {

               int clientID = insertObjectToDB.GetClientIdAndInsertToDB(client.client_name, client.client_sname, client.client_phone, client.client_description);
                if(clientID == -1)
                {
                    return RedirectToAction("info1", new { messageWindow = "Bład przy dodawaniu nowego klienta" });
                }
                return RedirectToAction("Index", "Reservation", new { visibleTrue = false });
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddReservation(Reservation reservation, string findClient)
        {
            int clientId = reservation.client_id;
            if (!String.IsNullOrEmpty(findClient))
            {
                var clientList = selectClient.GetRowsForTable(SGetAllRowsFromSpecificTable.ClientSelectAllRowsQuery())
                                 .Select( x => new
                                 {
                                     desccription = x.client_name + " " + x.client_sname + " " + x.client_phone + " " + x.client_description,
                                     clientId = x.client_id
                                 }).ToList();

                clientId = clientList.Where(x => x.desccription.Contains(findClient)).First().clientId;
            }
                insertObjectToDB.GetReservationIdAndInsertToDB(reservation.reservation_date, reservation.reservation_time.Hours, reservation.reservation_time.Minutes,
                clientId, reservation.services_id, reservation.employee_id);
               
                return RedirectToAction("Index", "Reservation", new { visibleTrue = false });
    
        }
        #endregion
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}