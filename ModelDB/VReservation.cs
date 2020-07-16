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
                    serId = x.services_id
                }).Join(servicesList,
                x => x.serId,
                y => y.services_id,
                (x, y) => new VReservation
                {
                    reservation_id = x.resId,
                    reservation_date = x.resDate,
                    client_name = x.clName,
                    client_sname = x.clSname,
                    client_phone = x.clPhone,
                    client_description = x.clDesc,
                    services_name = y.services_name,
                    services_id = y.services_id
                }).ToList();

            return result;
        }
    }
    public class VReservation
    {
        public int reservation_id { get; set; }
        public DateTime reservation_date { get; set; }
        public string client_name { get; set; }
        public string client_sname { get; set; }
        public string client_description { get; set; }
        public string client_phone { get; set; }
        public string services_name { get; set; }
        public int services_id { get; set; }
    }


}
