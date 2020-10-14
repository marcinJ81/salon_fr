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
using salonfrSource.DeleteReservation;
using salonfrSource.UpdateObjectInBase;

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
        private IDeleteReservation deletereservation;
        private IUpdateObject<Client> updateClient;

        public ReservationController()
        {
            this.selectClient = new SelectClient();
            this.updateClient = new UpdateClient(selectClient);
            this.selectServices = new SelectServices();
            this.selectReservation = new SelectReservation();
            this.selectEmployee = new SelectEmployee();
            this.getVReservation = new CreateViewVreservation(selectClient, selectReservation, selectServices, selectEmployee);
            this.insertObjectToDB = new FasadeInsertDB(new DBInsertClient(selectClient), new DBInsertServices(selectServices), new DBInsertReservation(selectReservation),
                new DBInsertEmployee(selectEmployee), new SelectClient(), new SelectServices(), new SelectReservation(), new SelectEmployee()); 
        }

        public IActionResult Index(string dateReservation)
        {
            var employreList = selectEmployee.GetRowsForTable(SGetAllRowsFromSpecificTable.EmployeeSelectAllRowsQuery());

            ViewBag.clientList = GenerateMultiSelectListWithClient();
            ViewBag.serviceList = GenerateMultiSelectListWithServices();
            ViewBag.employeeList = GenerateMultiSelectListWithEmployee();

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
            ViewBag.clientList = GenerateMultiSelectListWithClient();
            ViewBag.serviceList = GenerateMultiSelectListWithServices();
            ViewBag.employeeList = GenerateMultiSelectListWithEmployee();

            return PartialView();
        }
        [HttpGet]
        public PartialViewResult AddClient(int? client_id)
        {
           
            ViewBag.clientList = GenerateMultiSelectListWithClient();

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
        [HttpGet]
        public PartialViewResult deleteReservation(int reservation_id)
        {
            if (getVReservation.GetVReservations()
                .Where(x => x.reservation_id == reservation_id).Any())
            {
                var reservationList = getVReservation.GetVReservations()
                                      .Where(x => x.reservation_id == reservation_id).First();
                return PartialView(reservationList);
            }
            else
            {
                return PartialView();
            }
        }
        [HttpGet]
        public PartialViewResult UpdateClient(int? client_id,string findClient)
        {

            ViewBag.clientList = GenerateMultiSelectListWithClient();

            if ((client_id != null) || (client_id == 0))
            {
                return PartialView(selectClient.GetRowsForTable(SGetAllRowsFromSpecificTable.ClientSelectAllRowsQuery())
                    .Where(x => x.client_id == client_id).First());
            }
            if (!String.IsNullOrEmpty(findClient))
            {
                var clientList = selectClient.GetRowsForTable(SGetAllRowsFromSpecificTable.ClientSelectAllRowsQuery())
                                .Select(x => new
                                {
                                    desccription = x.client_name + " " + x.client_sname + " " + x.client_phone + " " + x.client_description,
                                    clientId = x.client_id
                                }).ToList();
                return PartialView(clientList.Where(x => x.desccription.Contains(findClient)).First());
            }
            return PartialView();
        }
        #endregion
        #region modalWindows_POST
        [HttpPost]
        public ActionResult UpdateClient(Client client)
        {
            bool updateResult = updateClient.UpdateObject(client, client.client_id);
            return RedirectToAction("UpdateClient", "Reservation", new { @client_id = client.client_id, @findClient="Aktualizacja OK"});
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult deleteReservation(VReservation vReservation)
        {
            deletereservation = new DeleteReservation();
            deletereservation.DeleteReservation(vReservation.reservation_id);
            return RedirectToAction("Index", "Reservation", new { visibleTrue = false });
        }
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
        #region privateMethods
        private MultiSelectList GenerateMultiSelectListWithClient()
        {
            var allclientList = selectClient.GetRowsForTable(SGetAllRowsFromSpecificTable.ClientSelectAllRowsQuery());
            List<Client> listC = new List<Client>() { new Client { client_id = 0, client_name = "Wybierz klienta" } };
            if (!allclientList.Any())
            {
                return new MultiSelectList(listC.Select(x => new
                {
                    key = x.client_id,
                    value = x.client_name + " " + x.client_sname
                }), "key", "value");
            }
                foreach (var i in allclientList)
                {
                    listC.Add(i);
                }
                var result = new MultiSelectList(listC.Select(x => new
                {
                    key = x.client_id,
                    value = x.client_name + " " + x.client_sname
                }), "key", "value");

                return result;
            
        }
        private MultiSelectList GenerateMultiSelectListWithServices()
        {
            var serviceList = selectServices.GetRowsForTable(SGetAllRowsFromSpecificTable.ServicesSelectAllRowsQuery());
            List<Services> listS = new List<Services>() { new Services { services_id = 0, services_name = "Wybierz usługę" } };
            if (!serviceList.Any())
            {
                return new MultiSelectList(listS.Select(x => new
                {
                    key = x.services_id,
                    value = x.services_name
                }), "key", "value");
            }
            foreach (var i in serviceList)
            {
                listS.Add(i);
            }
            return new MultiSelectList(listS.Select(x => new
            {
                key = x.services_id,
                value = x.services_name
            }), "key", "value");

        }
        private MultiSelectList GenerateMultiSelectListWithEmployee()
        {
            var employeeList = selectEmployee.GetRowsForTable(SGetAllRowsFromSpecificTable.EmployeeSelectAllRowsQuery());
            List<Employee> listE = new List<Employee> { new Employee { employee_id = 0, employee_name = "Wybierz pracownika" } };
            if (!employeeList.Any())
            {
                return new MultiSelectList(listE.Select(x => new
                {
                    key = x.employee_id,
                    value = x.employee_name
                }), "key", "value");
            }
            foreach (var i in employeeList)
            {
                listE.Add(i);
            }
            return new MultiSelectList(listE.Select(x => new
            {
                key = x.employee_id,
                value = x.employee_name
            }), "key", "value");
        }
        #endregion
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}