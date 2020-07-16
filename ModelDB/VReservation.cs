using salonfr.QuerySelect;
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

        public CreateViewVreservation(ISelectClient selectClient, ISelectReservation selectReservation, ISelectServices selectServices)
        {
            this.selectClient = selectClient;
            this.selectReservation = selectReservation;
            this.selectServices = selectServices;
        }
        public List<VReservation> GetVReservations()
        {

            var clientsList = selectClient.GetClients();
            var servicesList = selectServices.GetServices();
            var reservationList = selectReservation.GetReservations();

            return new List<VReservation>();
        }
    }
    public class VReservation
    {
        public DateTime reservation_date { get; set; }
        public string client_name { get; set; }
        public string client_sname { get; set; }
        public string client_description { get; set; }
        public string services_name { get; set; }
        public int services_id { get; set; }
    }


}
