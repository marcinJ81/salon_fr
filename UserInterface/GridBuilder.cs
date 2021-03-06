﻿using salonfrSource.ModelDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace salonfr.UserInterface
{
    public static class GridBuilder
    {
        public static DataGridView FillTheGrid(List<VReservation> source, DataGridView dgvVReservation)
        {
            dgvVReservation.AutoGenerateColumns = true;
            dgvVReservation.DataSource = source;
            dgvVReservation.Columns["services_id"].Visible = false;
            dgvVReservation.Columns["reservation_id"].Visible = false;
            dgvVReservation.Columns["employee_name"].HeaderText = "Pracownik";
            dgvVReservation.Columns["client_name"].HeaderText = "Imię";
            dgvVReservation.Columns["client_sname"].HeaderText = "Nazwisko";
            dgvVReservation.Columns["client_description"].HeaderText = "Opis";
            dgvVReservation.Columns["client_phone"].HeaderText = "Nr tel.";
            dgvVReservation.Columns["services_name"].HeaderText = "Usługa";
            dgvVReservation.Columns["reservation_date"].HeaderText = "Data rezerwacji";
            dgvVReservation.Columns["reservation_time"].HeaderText = "Godzina rezerwacji";
            return dgvVReservation;
        }
    }
}
