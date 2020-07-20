using salonfr.DBConnect;
using salonfr.QuerySelect;
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
        public MainForm()
        {
            InitializeComponent();
            SqlLiteDB.SqlLiteDBCreateTable();
            this.selectClient = new SelectClient();
            this.selectServices = new SelectServices();
            this.selectReservation = new SelectReservation();
            this.getVReservation = new CreateViewVreservation(selectClient, selectReservation, selectServices);
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
            FillTheGrid();
            SetEnabledClientControl(false);
        }
        private void FillTheGrid()
        {
            var result = getVReservation.GetVReservations();
            dgvVReservation.DataSource = result;
        }

    }
}
