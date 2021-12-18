using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Hotal_Managment_Syatem
{
    public partial class PresentyDetails : Form
    {

        Database db = new Database();
        string dateFrom, dateto;

        public PresentyDetails()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PresentyDetails_Load(object sender, EventArgs e)
        {

            this.CancelButton = btnBack;
            db.comboFill(cmbWaiter, "SELECT wname FROM waiter_dtls", "waiter_dtls", "wname", "wname");
            cmbWaiter.Text = "All";
            dataGridView_waiterPrsenty.DataSource = db.Displaygrid("SELECT  waiter_dtls.wname as [Employee Name], waiter_prsenty.p_id, waiter_prsenty.date as Date, waiter_prsenty.status as Status FROM waiter_dtls INNER JOIN  waiter_prsenty ON waiter_dtls.w_id = waiter_prsenty.w_id ");
            dataGridView_waiterPrsenty.Columns["Employee Name"].Width = 170;
            dataGridView_waiterPrsenty.Columns["Date"].Width = 150;
            dataGridView_waiterPrsenty.Columns["p_id"].Visible = false;

        }
        void prsenty_bind()
        {
            label_date.Text = dtpFrom.Text + "  And  " + dtpTo.Text; ;

            dateFrom = dtpFrom.Value.ToString("MM'-'dd'-'yyyy");
            dateto = dtpTo.Value.ToString("MM'-'dd'-'yyyy");
            string qry = "SELECT  waiter_dtls.wname as [Employee Name],waiter_prsenty.p_id ,waiter_prsenty.date as Date, waiter_prsenty.status as Status FROM waiter_dtls INNER JOIN  waiter_prsenty ON waiter_dtls.w_id = waiter_prsenty.w_id  WHERE ";
            int flag = 0;
            if (cmbWaiter.Text != "All")
            {
                if (flag != 0)
                    qry += " and ";
                qry += "waiter_dtls.wname ='" + cmbWaiter.Text + "'";
                flag++;


            }
            if (chkDate.Checked)
            {
                if (flag != 0)
                    qry += " and ";

                qry += "date between  '" + dateFrom + "' and '" + dateto + "'";
                flag++;



            }
            if (flag == 0)
                dataGridView_waiterPrsenty.DataSource = db.Displaygrid("SELECT  waiter_dtls.wname as [Employee Name], waiter_prsenty.p_id,waiter_prsenty.date as Date, waiter_prsenty.status as Status FROM waiter_dtls INNER JOIN  waiter_prsenty ON waiter_dtls.w_id = waiter_prsenty.w_id ");

            else
                dataGridView_waiterPrsenty.DataSource = db.Displaygrid(qry);

            dataGridView_waiterPrsenty.Columns["Employee Name"].Width = 170;

            cal();
            dataGridView_waiterPrsenty.Columns["p_id"].Visible = false;

        }
        void cal()
        {
            double presentDays = 0, absentDays = 0, halfDays = 0, overtime = 0;
            string status = "";
            for (int i = 0; i < dataGridView_waiterPrsenty.RowCount; i++)
            {
                status = dataGridView_waiterPrsenty.Rows[i].Cells["Status"].Value.ToString();
                if (status == "Present")
                    presentDays++;
                else if (status == "Absent")
                    absentDays++;
                else if (status == "Overtime")
                    overtime++;
                else
                    halfDays++;

            }
            lblAbsentDays.Text = absentDays.ToString();
            lblHalfDays.Text = halfDays.ToString();
            lblPresentDays.Text = presentDays.ToString();
            lblot.Text = overtime.ToString();


        }

        private void chkDate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDate.Checked)
            {
                dtpTo.Enabled = true;
                dtpFrom.Enabled = true;
            }
            else
            {

                dtpTo.Enabled = false;
                dtpFrom.Enabled = false;
            }
        }

        private void dtpFrom_ValueChanged(object sender, EventArgs e)
        {
            prsenty_bind();
        }

        private void dtpTo_ValueChanged(object sender, EventArgs e)
        {
            prsenty_bind();
        }

        private void cmbWaiter_SelectedIndexChanged(object sender, EventArgs e)
        {
            prsenty_bind();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            cmbWaiter.Text = "All";
            chkDate.Checked = false;
            //prsenty_bind();
            dataGridView_waiterPrsenty.DataSource = db.Displaygrid("SELECT  waiter_dtls.wname as [Employee Name],waiter_prsenty.p_id, waiter_prsenty.date as Date, waiter_prsenty.status as Status FROM waiter_dtls INNER JOIN  waiter_prsenty ON waiter_dtls.w_id = waiter_prsenty.w_id ");
            dataGridView_waiterPrsenty.Columns["Employee Name"].Width = 170;
            dataGridView_waiterPrsenty.Columns["p_id"].Visible = false;

        }

        private void dataGridView_waiterPrsenty_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == dataGridView_waiterPrsenty.Columns[0].Index)
            {
                string itemName;

                int i = dataGridView_waiterPrsenty.SelectedCells[0].RowIndex;

                string pid = dataGridView_waiterPrsenty.Rows[i].Cells["p_id"].Value.ToString();

                itemName = dataGridView_waiterPrsenty.Rows[i].Cells["Employee Name"].Value.ToString();

                string wid = db.getDbstatus_Value("select w_id from waiter_dtls where wname='" + itemName + "'");

                DialogResult dlgresult = MessageBox.Show("Are you sure want to Delete?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgresult == DialogResult.Yes)
                {
                    SqlCommand cmd = new SqlCommand("delete from waiter_prsenty where p_id='" + pid + "' and w_id='" + wid + "'", db.cn);
                    try
                    {

                        cmd.ExecuteScalar();
                        MessageBox.Show("Entry Deleted");
                        // bind();
                        dataGridView_waiterPrsenty.DataSource = db.Displaygrid("SELECT  waiter_dtls.wname as [Employee Name],waiter_prsenty.p_id, waiter_prsenty.date as Date, waiter_prsenty.status as Status FROM waiter_dtls INNER JOIN  waiter_prsenty ON waiter_dtls.w_id = waiter_prsenty.w_id ");
                        dataGridView_waiterPrsenty.Columns["Employee Name"].Width = 170;
                        dataGridView_waiterPrsenty.Columns["p_id"].Visible = false;

                       
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error  " + ex.Message);
                    }

                }


            }
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }
    }
}
