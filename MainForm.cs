using salonfr.DBConnect;
using salonfr.InsertDateToBase;
using salonfr.QuerySelect;
using salonfr.SQLScripts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace salonfr
{
    public partial class MainForm : Form
    {
        private ISelectClient selectClient;
        private ISelectServices selectServices;
        private ISelectReservation selectReservation;
        private IGetVReservation getVReservation;
        private IInsertToDB<Client> insertClient;
        private IInsertToDB<Services> insertServices;
        private IInsertToDB<Reservation> insertReservation;
        public MainForm()
        {
            InitializeComponent();
            SqlLiteDB.SqlLiteDBCreateTable();
            this.selectClient = new SelectClient();
            this.selectServices = new SelectServices();
            this.selectReservation = new SelectReservation();
            this.getVReservation = new CreateViewVreservation(selectClient, selectReservation, selectServices);
            insertClient = new DBInsertClient(selectClient);
            insertServices = new DBInsertServices(selectServices);
            insertReservation = new DBInsertReservation(selectReservation);

        }

        private void CkbNewClient_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbNewClient.Checked)
            {
                SetEnabledClientControl(true);
            }
            else
            {
                SetEnabledClientControl(false);
            }
        }
        private void SetEnabledClientControl(bool enabled)
        {
            cmbClientList.Enabled = !enabled;
            txbClientDescription.Enabled = enabled;
            txbClientName.Enabled = enabled;
            txbClientSName.Enabled = enabled;
            txbClientPhone.Enabled = enabled;
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            FillTheGrid(getVReservation.GetVReservations());
            SetEnabledClientControl(false);
            SetEnabledServiceControl(false);
            SetDataToCmbClient();
            SetDAtaToCmbServices();
        }
        private void FillTheGrid(List<VReservation> source)
        {
            dgvVReservation.AutoGenerateColumns = true;
            dgvVReservation.DataSource = source;
            dgvVReservation.Columns["services_id"].Visible = false;
            dgvVReservation.Columns["reservation_id"].HeaderText = "Id";
            dgvVReservation.Columns["client_name"].HeaderText = "Imię";
            dgvVReservation.Columns["client_sname"].HeaderText = "Nazwisko";
            dgvVReservation.Columns["client_description"].HeaderText = "Opis";
            dgvVReservation.Columns["client_phone"].HeaderText = "Nr tel.";
            dgvVReservation.Columns["services_name"].HeaderText = "Usługa";
            dgvVReservation.Columns["reservation_date"].HeaderText = "Data rezerwacji";

        }

        private void CkbNewServices_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbNewServices.Checked)
            {
                SetEnabledServiceControl(true);
            }
            else
            {
                SetEnabledServiceControl(false);
            }
        }
        private void SetEnabledServiceControl(bool enabled)
        {
            cmbListServices.Enabled = !enabled;
            txbNewServices.Enabled = enabled;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            txbClientName.Text = "imie";
            txbClientDescription.Text = "opis";
            txbClientPhone.Text = "nr tel";
            txbClientSName.Text = "nazwisko";
            txbNewServices.Text = "usługa";
            cmbClientList.SelectedIndex = 0;
            cmbListServices.SelectedIndex = 0;
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void SetDataToCmbClient()
        {
            List<Client> clientList = new List<Client>();
            clientList.Add(new Client { client_id = 0, client_name = "klient" });
            foreach (var i in selectClient.GetClients(SGetAllRowsFromSpecificTable.ClientSelectAllRowsQuery()))
            {
                clientList.Add(new Client { client_id = i.client_id, client_name = i.client_name +" "+ i.client_sname });
            }
            cmbClientList.DataSource = clientList;
            cmbClientList.DisplayMember = "client_name";
            cmbClientList.ValueMember = "client_id";
        }
        private void SetDAtaToCmbServices()
        {
            List<Services> listServices = new List<Services>();
            listServices.Add(new Services { services_id = 0, services_name = "usługa" });
            foreach (var i in selectServices.GetServices(SGetAllRowsFromSpecificTable.ServicesSelectAllRowsQuery()))
            {
                listServices.Add(i);
            }
            cmbListServices.DataSource = listServices;
            cmbListServices.DisplayMember = "services_name";
            cmbListServices.ValueMember = "services_id";
        }

        private void BtnNewReservation_Click(object sender, EventArgs e)
        {
            int clientID = -1;
            int servicesID = -1;
            if (ckbNewClient.Checked)
            {
                clientID = selectClient.GetNextClientId(SGetIdFromSpecificTable.queryGetLatestClientID());
                Client client = new Client()
                {
                    client_id = clientID,
                    client_name = txbClientName.Text,
                    client_sname = txbClientSName.Text,
                    client_phone = txbClientPhone.Text,
                    client_description = txbClientDescription.Text
                };
                insertClient.InsertObjectToDB(client);
            }
            else
            {
                clientID = cmbClientList.SelectedIndex;
            }
            if (ckbNewServices.Checked)
            {
                servicesID = selectServices.GetNextServicesId(SGetIdFromSpecificTable.queryGetLatestServicesID());
                Services services = new Services()
                {
                    services_id = servicesID,
                    services_name = txbNewServices.Text
                };
                insertServices.InsertObjectToDB(services);
            }
            else
            {
                servicesID = cmbListServices.SelectedIndex;
            }
            int reservationID = selectReservation.GetNextReservationtId(SGetIdFromSpecificTable.queryGetLatestReservationID());
            Reservation reservation = new Reservation()
            {
                reservation_id = reservationID,
                reservation_date = dtpReservationDate.Value,
                client_id = clientID,
                services_id = servicesID
            };
            int lastIndex = insertReservation.InsertObjectToDB(reservation);

            FillTheGrid(getVReservation.GetVReservations());
            SetDataToCmbClient();
            SetDAtaToCmbServices();
        }

      

        private void DtpDateFind_ValueChanged(object sender, EventArgs e)
        {

            var result = getVReservation.GetVReservations()
                                        .Where(x => x.reservation_date.ToShortDateString() == dtpDateFind.Value.ToShortDateString())
                                        .ToList();
            FillTheGrid(result);
        }
    }
}
