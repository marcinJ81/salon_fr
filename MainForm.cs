using salonfr.DBConnect;
using salonfr.InsertDateToBase;
using salonfr.QuerySelect;
using salonfr.SQLScripts;
using salonfr.UserInterface;
using salonfrSource.DeleteReservation;
using salonfrSource.Log;
using salonfrSource.ModelDB;
using salonfrSource.QuerySelect;
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
        private ISelectTableObject<Client> selectClient;
        private ISelectTableObject<Services> selectServices;
        private ISelectReservation selectReservation;
        private IGetVReservation getVReservation;
        private ISelectTableObject<Employee> selectEmployee;
        private IInsertToDB<Client> insertClient;
        private IInsertToDB<Services> insertServices;
        private IInsertToDB<Reservation> insertReservation;
        private IUpdateObject<Client> updateClient;
        private IInsertToDB<Employee> insertEmployee;
        private IDeleteReservation deleteReservation;
        public MainForm()
        {
            InitializeComponent();
            SqlLiteDB.SqlLiteDBCreateTable();
            this.selectClient = new SelectClient();
            this.selectServices = new SelectServices();
            this.selectReservation = new SelectReservation();
            this.selectEmployee = new SelectEmployee();
            this.getVReservation = new CreateViewVreservation(selectClient, selectReservation, selectServices,selectEmployee);          
            insertClient = new DBInsertClient(selectClient);
            insertServices = new DBInsertServices(selectServices);
            insertReservation = new DBInsertReservation(selectReservation);
            this.updateClient = new UpdateClient(selectClient);
            this.insertEmployee = new DBInsertEmployee(selectEmployee);
            this.deleteReservation = new DeleteReservation();

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
            ComboBoxSetData.SetDataToCmbEmployee(tscmbEmployee.ComboBox);
           // dtpDateFind.Value = DateTime.Now;
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
            int employeeID = -1;
            if (ckbNewClient.Checked)
            {
                clientID = selectClient.GetNextTabletId(SGetIdFromSpecificTable.queryGetLatestClientID());
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
                    return;
                }
                clientID = cmbClientList.SelectedIndex;
            }
            if (ckbNewServices.Checked)
            {
                servicesID = selectServices.GetNextTabletId(SGetIdFromSpecificTable.queryGetLatestServicesID());
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
                    return;
                }
                servicesID = cmbListServices.SelectedIndex;
            }
            if (tscmbEmployee.ComboBox.SelectedIndex == 0)
            {
                MessageBox.Show("Wybierz pracownika");
                return;
            }
            
            int reservationID = selectReservation.GetNextReservationtId(SGetIdFromSpecificTable.queryGetLatestReservationID());

            Reservation reservation = new Reservation()
            {
                reservation_id = reservationID,
                reservation_date = dtpReservationDate.Value,
                reservation_time = new TimeSpan(Convert.ToInt32(nudHour.Value), Convert.ToInt32(nudMinute.Value), 0),
                client_id = clientID,
                services_id = servicesID,
                employee_id = tscmbEmployee.SelectedIndex
            };
            if (selectReservation.GetReservations(SGetAllRowsFromSpecificTable.ReservationSelectAllRowsQuery())
                    .Any(x => x.reservation_date == reservation.reservation_date 
                    && x.reservation_time == reservation.reservation_time
                    && x.employee_id == reservation.employee_id))
            {
                MessageBox.Show("Nie można daoć reservacji w tym terminie. Jest on zajęty");
                return;
            }
            SLogToFile.SaveDataTebleInToFile("reservation" ,reservation.ToString());
            insertReservation.InsertObjectToDB(reservation);

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
                    
                var selectedClient = selectClient.GetRowsForTable(SGetAllRowsFromSpecificTable.ClientSelectAllRowsQuery())
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
            cmbClientList.SelectedIndex = clientId;
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

        private void BtnAddEmployee_Click(object sender, EventArgs e)
        {
            int employeID = -1;
            if (tscmbEmployee.ComboBox.SelectedIndex == 0)
            {
                employeID = selectEmployee.GetNextTabletId(SGetIdFromSpecificTable.queryGetLatestEmployeeID());
                Employee employee = new Employee()
                {
                    employee_id = employeID,
                    employee_name = tstxbEmployeeName.TextBox.Text
                };
                insertEmployee.InsertObjectToDB(employee);
                ComboBoxSetData.SetDataToCmbEmployee(tscmbEmployee.ComboBox);
            }
            else
            {
                MessageBox.Show("Nie można dodać nowego pracownika");
            }
        }

        private void BtnDeleteResrvation_Click(object sender, EventArgs e)
        {
            
            int numer = dgvVReservation.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            int reservationID = int.Parse(dgvVReservation.Rows[numer].Cells["reservation_id"].Value.ToString());
            if (!deleteReservation.DeleteReservation(reservationID))
            {
                MessageBox.Show("Usunięcie rezerwacji niemożliwe. Zajrzyj do loga");
                return;
            }
            GridBuilder.FillTheGrid(getVReservation.GetVReservations(), dgvVReservation);
        }
    }
}
