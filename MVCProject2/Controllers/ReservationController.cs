﻿using System;
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

        public IActionResult Index()
        {
            var reservationList = getVReservation.GetVReservations();
            

            return View(reservationList);
        }
        [HttpPost]
        public ActionResult Index(string dateReservation,int clientList,int serviceList, int employeeList)
        {
            
            var dt = dateReservation;
            int cl_id = clientList;
            var datetimeReservation = dateReservation.Split(' ');
            DateTime dateReservation1 = DateTime.Parse(datetimeReservation[0]);

            bool result = insertObjectToDB.GetReservationIdAndInsertToDB(dateReservation1, 1, 1, clientList, serviceList, employeeList);

            return RedirectToAction("Index", "Reservation", null); 
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
        public PartialViewResult AddClient()
        {
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
            if (String.IsNullOrEmpty(client.client_phone))
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
        public ActionResult AddReservation(Reservation reservation)
        {
            if (!ModelState.IsValid)
            {
                //komunikat
                return AddReservation(reservation);
            }
            else
            {
                insertObjectToDB.GetReservationIdAndInsertToDB(reservation.reservation_date, reservation.reservation_time.Hours, reservation.reservation_time.Minutes,
                reservation.client_id, reservation.services_id, reservation.employee_id);
               
                return RedirectToAction("Index", "Reservation", new { visibleTrue = false });
            }
        }
        #endregion
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}