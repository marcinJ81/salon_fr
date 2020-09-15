using salonfrSource;
using salonfrSource.ModelDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salonfr.UnitTest
{
    public class Fake_VReservation : IViewReservation
    {
        public List<VReservation> AllReservation()
        {
            DateTime dateTime = new DateTime(2020,07,13);
            List<VReservation> result = new List<VReservation>
            { new VReservation
                {
                    reservation_date = dateTime,
                    client_name = "Marcin",
                    client_sname = "Juranek",
                    client_description = "oporny",
                    services_name = "strzyżenie"
                }
            };
            return result;
        }
        public List<VReservation> EmptyReservation()
        {
            DateTime dateTime = new DateTime(2099, 1, 1);
            List<VReservation> result = new List<VReservation>
            { new VReservation
                {
                    reservation_date = dateTime,
                    client_name = "brak",
                    client_sname = "brak",
                    client_description = "brak",
                    services_name = "brak"
                }
            };
            return result;
        }
    }
}
