using salonfr.DBConnect;
using salonfr.InsertDateToBase;
using salonfr.QuerySelect;
using salonfr.SQLScripts;
using salonfr.UserInterface;
using salonfrSource.UpdateObjectInBase;
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
        private IUpdateObject<Client> updateClient;
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
            this.updateClient = new UpdateClient(selectClient);
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
            GridBuilder.FillTheGrid(getVReservation.GetVReservations(),dgvVReservation);
            SetEnabledClientControl(false);
            SetEnabledServiceControl(false);
            ComboBoxSetData.SetDataToCmbClient(cmbClientList);

            ComboBoxSetData.SetDataToCmbServices(cmbListServices);
            FillClientControls(null, true);
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
            FillClientControls(null, true);
            txbNewServices.Text = "usługa";
            cmbClientList.SelectedIndex = 0;
            cmbListServices.SelectedIndex = 0;
            ckbNewClient.Checked = false;
            ckbUpdateClient.Checked = false;
            ckbNewServices.Checked = false;
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        

        private void BtnNewReservation_Click(object sender, EventArgs e)
        {
            insertNewReservation();
        }

        public void insertNewReservation()
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
                if (cmbClientList.SelectedIndex == 0)
                {
                    MessageBox.Show("Wybierz klienta");
                }
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
                if (cmbListServices.SelectedIndex == 0)
                {
                    MessageBox.Show("Wybierz usługę");
                }
                servicesID = cmbListServices.SelectedIndex;
            }
            int reservationID = selectReservation.GetNextReservationtId(SGetIdFromSpecificTable.queryGetLatestReservationID());
            Reservation reservation = new Reservation()
            {
                reservation_id = reservationID,
                reservation_date = dtpReservationDate.Value,
                reservation_time = new TimeSpan(Convert.ToInt32(nudHour.Value), Convert.ToInt32(nudMinute.Value), 0),
                client_id = clientID,
                services_id = servicesID
            };
            int lastIndex = insertReservation.InsertObjectToDB(reservation);

            GridBuilder.FillTheGrid(getVReservation.GetVReservations(),dgvVReservation);
            ComboBoxSetData.SetDataToCmbClient(cmbClientList);
            ComboBoxSetData.SetDataToCmbServices(cmbListServices);
            FillClientControls(null, true);
        }

        private void DtpDateFind_ValueChanged(object sender, EventArgs e)
        {

            var result = getVReservation.GetVReservations()
                                        .Where(x => x.reservation_date.ToShortDateString() == dtpDateFind.Value.ToShortDateString())
                                        .ToList();
            GridBuilder.FillTheGrid(result,dgvVReservation);
        }

        private void CkbUpdateClient_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbUpdateClient.Checked)
            {
                setEnabledClientControlForUpdate(true);
            }
            else
            {
                setEnabledClientControlForUpdate(false);
            }

        }
        private void setEnabledClientControlForUpdate(bool enabled)
        {
            ckbNewClient.Checked = enabled == true ? !enabled : false;
            ckbNewClient.Enabled = !enabled;
            cmbClientList.Enabled = enabled == false ? !enabled : true;
            txbClientDescription.Enabled = enabled;
            txbClientName.Enabled = enabled;
            txbClientSName.Enabled = enabled;
            txbClientPhone.Enabled = enabled;
        }

        private void BtnUpdateClient_Click(object sender, EventArgs e)
        {
            int clientID = -1;
            if (ckbUpdateClient.Checked)
            {
                if (cmbClientList.SelectedIndex == 0)
                    return;
                clientID = cmbClientList.SelectedIndex;
                Client clientU = new Client()
                {
                    client_name = txbClientName.Text,
                    client_sname = txbClientSName.Text,
                    client_phone = txbClientPhone.Text,
                    client_description = txbClientDescription.Text
                };
               bool updateResult = updateClient.UpdateObject(clientU, clientID);
                if (!updateResult)
                {
                    MessageBox.Show("Błąd aktualizacji");
                }
                GridBuilder.FillTheGrid(getVReservation.GetVReservations(),dgvVReservation);
                ComboBoxSetData.SetDataToCmbClient(cmbClientList);
            }
        }

        private void CmbClientList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ckbUpdateClient.Checked)
            {
                if (cmbClientList.SelectedIndex == 0)
                {
                    FillClientControls(null,true);
                    return;
                }
                    
                var selectedClient = selectClient.GetClients(SGetAllRowsFromSpecificTable.ClientSelectAllRowsQuery())
                            .Where(x => x.client_id == cmbClientList.SelectedIndex).First();
                FillClientControls(selectedClient, false);
            }
        }

        private void BtnFindClient_Click(object sender, EventArgs e)
        {
            int clientId;
            var clientData = SClientFullTextSearch.GetClientFullTextSearch(txbFindClient.Text,out clientId);
            FillClientControls(clientData, false);
            lblIdFindClient.Text = clientId.ToString();
        }
        private void FillClientControls(Client client, bool clear)
        {
            if (clear)
            {
                txbClientName.Text = "imie";
                txbClientDescription.Text = "opis";
                txbClientPhone.Text = "nr tel";
                txbClientSName.Text = "nazwisko";
                lblIdFindClient.Text = "";
            }
            if (client != null)
            {
                txbClientDescription.Text = client.client_description;
                txbClientName.Text = client.client_name;
                txbClientSName.Text = client.client_sname;
                txbClientPhone.Text = client.client_phone;
            }
        }
        
    }
}
