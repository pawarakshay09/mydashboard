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
    public partial class waiter_prsenty_dtls : Form
    {
        Database db = new Database();
        string dateFrom, dateto;
        //string constr = System.Configuration.ConfigurationSettings.AppSettings.Get("con");
        public waiter_prsenty_dtls()
        {
            InitializeComponent();
        }

        void prsenty_bind()
        {
            label_date.Text = dtpFrom.Text + "  And  " + dtpTo.Text; ;

            dateFrom = dtpFrom.Value.ToString("MM'-'dd'-'yyyy");
            dateto = dtpTo.Value.ToString("MM'-'dd'-'yyyy");
            string qry = "SELECT  waiter_dtls.wname as [Employee Name], waiter_prsenty.date as Date, waiter_prsenty.status as Status FROM waiter_dtls INNER JOIN  waiter_prsenty ON waiter_dtls.w_id = waiter_prsenty.w_id  WHERE ";
            int flag = 0;
            if (cmbWaiter.Text != "All")
            {
                if (flag != 0)
                    qry += " and ";
                    qry += "waiter_dtls.wname ='"+cmbWaiter.Text+"'";
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
                dataGridView_waiterPrsenty.DataSource = db.Displaygrid("SELECT  waiter_dtls.wname as [Employee Name], waiter_prsenty.date as Date, waiter_prsenty.status as Status FROM waiter_dtls INNER JOIN  waiter_prsenty ON waiter_dtls.w_id = waiter_prsenty.w_id ");
            else
                dataGridView_waiterPrsenty.DataSource = db.Displaygrid(qry);

                dataGridView_waiterPrsenty.Columns[0].Width = 170;

                cal();
        }
        
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnShowDetails_Click(object sender, EventArgs e)
        {
            label_date.Text = dtpFrom.Text;
            prsenty_bind();
        }

        private void waiter_prsenty_dtls_Load(object sender, EventArgs e)
        {
            this.CancelButton = btnBack;
            db.comboFill(cmbWaiter, "SELECT wname FROM waiter_dtls", "waiter_dtls", "wname", "wname");
            cmbWaiter.Text = "All";
            dataGridView_waiterPrsenty.DataSource = db.Displaygrid("SELECT  waiter_dtls.wname as [Employee Name], waiter_prsenty.date as Date, waiter_prsenty.status as Status FROM waiter_dtls INNER JOIN  waiter_prsenty ON waiter_dtls.w_id = waiter_prsenty.w_id ");
            dataGridView_waiterPrsenty.Columns[0].Width = 170;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dtpFrom_ValueChanged(object sender, EventArgs e)
        {
            prsenty_bind();
        }

        void cal()
        {
            double presentDays = 0, absentDays = 0, halfDays = 0;
            string status = "";
            for (int i = 0; i < dataGridView_waiterPrsenty.RowCount; i++)
            {
                status = dataGridView_waiterPrsenty.Rows[i].Cells[2].Value.ToString();
                if (status == "Present")
                    presentDays++;
                else if (status == "Absent")
                    absentDays++;
                else
                    halfDays++;
            }
            lblAbsentDays.Text=absentDays.ToString();
            lblHalfDays.Text=halfDays.ToString();
            lblPresentDays.Text=presentDays.ToString();
           
        }
        private void dtpTo_ValueChanged(object sender, EventArgs e)
        {
            prsenty_bind();
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

        private void cmbWaiter_SelectedIndexChanged(object sender, EventArgs e)
        {
            prsenty_bind(); 
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            cmbWaiter.Text = "All";
            chkDate.Checked = false;
            //prsenty_bind();
            dataGridView_waiterPrsenty.DataSource = db.Displaygrid("SELECT  waiter_dtls.wname as [Employee Name], waiter_prsenty.date as Date, waiter_prsenty.status as Status FROM waiter_dtls INNER JOIN  waiter_prsenty ON waiter_dtls.w_id = waiter_prsenty.w_id ");
            dataGridView_waiterPrsenty.Columns[0].Width = 170;
         }
    }
}
