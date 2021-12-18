using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Hotal_Managment_Syatem
{
    public partial class NC_Report : Form
    {
        Database db = new Database();
        public NC_Report()
        {
            InitializeComponent();
        }

        private void NC_Report_Load(object sender, EventArgs e)
        {
            db.formFix(this);
            this.CancelButton = btnCLose;
            dtpFrom.Enabled = true;
            dtpTo.Enabled = true;
            db.comboFill(cmbPaymentMode, "select distinct status from total_bill", "total_bill", "status", "status");
            cmbPaymentMode.Text = "All";
            //dataGridView1.DataSource = db.Displaygrid("SELECT   order_id AS [Order ID], Total_bill, datetime AS Date, status AS Status, remark AS Remark, billTime AS [Bill Time] FROM            total_bill");
            bindGrid();
        }

        private void cmbWaiter_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindGrid();
        }
        void bindGrid()
        {
            string qry = "SELECT   order_id AS [Order ID], Total_bill, datetime AS Date, status AS Status, remark AS Remark, billTime AS [Bill Time] FROM            total_bill where ";
            int flag = 0;
            if (cmbPaymentMode.Text != "All")
            {
                if (flag != 0)
                    qry += " and ";
                qry += "status ='"+cmbPaymentMode.Text+"'";
                flag++;
                
            }
            if (chkDate.Checked)
            {
                if (flag != 0)
                    qry += " and ";
                qry += "datetime between '" + dtpFrom.Value.ToString("MM/dd/yyyy") + "' and '"+dtpTo.Value.ToString("MM/dd/yyyy")+"'";
                flag++;
            }
            if (flag == 0)
            {
                dataGridView1.DataSource = db.Displaygrid("SELECT   order_id AS [Order ID], Total_bill, datetime AS Date, status AS Status, remark AS Remark, billTime AS [Bill Time] FROM            total_bill");
            }
            else
                dataGridView1.DataSource = db.Displaygrid(qry);
            cal();
        }

        void cal()
        {
            double sum = 0;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                sum += double.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString());
            }
            lblTotal.Text = sum.ToString();
        }
        private void dtpFrom_ValueChanged(object sender, EventArgs e)
        {
            bindGrid();

        }

        private void dtpTo_ValueChanged(object sender, EventArgs e)
        {
            bindGrid();

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            cmbPaymentMode.Text = "All";
            chkDate.Checked = false;
            bindGrid();

        }

        private void chkDate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDate.Checked)
            {
                dtpFrom.Enabled = true;
                dtpTo.Enabled = true;
            }
            else
            {
                dtpFrom.Enabled = false;
                dtpTo.Enabled = false;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Excel Documents (*.xls)|*.xls";
                sfd.FileName = "NC_Report.xls";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    //ToCsV(dataGridView1, @"c:\export.xls";
                    db.withReportTitle_ToCsV(dataGridView1, sfd.FileName,"NC Report"); // Here dataGridview1 is your grid view name 
                    MessageBox.Show("File Sucessfully Exported");

                    Process.Start(sfd.FileName);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
