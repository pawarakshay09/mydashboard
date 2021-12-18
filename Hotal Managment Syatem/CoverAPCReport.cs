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
    public partial class CoverAPCReport : Form
    {
        Database db = new Database();
        public CoverAPCReport()
        {
            InitializeComponent();
        }

        private void CoverAPCReport_Load(object sender, EventArgs e)
        {
            db.formFix(this);
            this.CancelButton = btnCLose;
            dataGridView1.DataSource = db.Displaygrid("SELECT table_order.order_id as [Order ID], total_bill.Total_bill, table_order.noOfGuest as [No Of Guest], total_bill.Total_bill / table_order.noOfGuest AS APC FROM   table_order INNER JOIN   total_bill ON table_order.order_id = total_bill.order_id");
            dataGridView1.Columns[3].DefaultCellStyle.Format = "N2";
            cal();
        }
        void cal()
        {
            double sum = 0,apc=0;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                sum += double.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString());
               apc += double.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
            }
            lblTotal.Text = sum.ToString();
            lblAPC.Text = apc.ToString();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        void dateChange()
        {
            dataGridView1.DataSource = db.Displaygrid("SELECT table_order.order_id as [Order ID], total_bill.Total_bill, table_order.noOfGuest as [No Of Guest], total_bill.Total_bill / table_order.noOfGuest AS APC FROM   table_order INNER JOIN   total_bill ON table_order.order_id = total_bill.order_id where datetime between '"+dtpFrom.Value.ToString("MM/dd/yyyy")+"' and '"+dtpTo.Value.ToString("MM/dd/yyyy")+"'");
            dataGridView1.Columns[3].DefaultCellStyle.Format = "N2";
            cal();

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

        private void dtpFrom_ValueChanged(object sender, EventArgs e)
        {
            dateChange();
        }

        private void dtpTo_ValueChanged(object sender, EventArgs e)
        {
            dateChange();

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Excel Documents (*.xls)|*.xls";
                sfd.FileName = "Cover_APC Report.xls";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    //ToCsV(dataGridView1, @"c:\export.xls";
                    db.withReportTitle_ToCsV(dataGridView1, sfd.FileName,"Cover_APC Report"); // Here dataGridview1 is your grid view name 
                    MessageBox.Show("File Sucessfully Exported");

                    Process.Start(sfd.FileName);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCLose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
