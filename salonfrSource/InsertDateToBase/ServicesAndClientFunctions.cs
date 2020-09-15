using salonfrSource.DBConnect;
using salonfrSource.ModelDB;
using System;

namespace salonfrSource.InsertDateToBase
{

    public class ServicesAndClientFunctions 
    {
        private IInsertToDB<Client> addClient;
        private IInsertToDB<Services> addServices;
        private IInsertToDB<Reservation> addReservation;

        public ServicesAndClientFunctions(IInsertToDB<Client> addClient, IInsertToDB<Services> addServices,IInsertToDB<Reservation> addReservation)
        {
            this.addClient = addClient;
            this.addServices = addServices;
            this.addReservation = addReservation;
        }

        
    }
}
