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
    public partial class ParcelReport : Form
    {
        Database db = new Database();
        public ParcelReport()
        {
            InitializeComponent();
        }

        private void ParcelReport_Load(object sender, EventArgs e)
        {
            db.formFix(this);
            this.CancelButton = btnCLose;
            dataGridView1.DataSource = db.Displaygrid("SELECT  table_order.t_id as [Table No], table_order.order_id as [Order ID], total_bill.Total_bill, total_bill.datetime  as Date FROM            total_bill INNER JOIN   table_order ON total_bill.order_id = table_order.order_id WHERE        (table_order.t_id LIKE 'P%')");
            cal();

        }

        void dateChange()
        {
            dataGridView1.DataSource = db.Displaygrid("SELECT  table_order.t_id as [Table No], table_order.order_id as [Order ID], total_bill.Total_bill, total_bill.datetime as Date FROM            total_bill INNER JOIN   table_order ON total_bill.order_id = table_order.order_id WHERE        (table_order.t_id LIKE 'P%') and datetime between '" + dtpFrom.Value.ToString("MM/dd/yyyy") + "' and '" + dtpTo.Value.ToString("MM/dd/yyyy") + "'");
            cal();
        }
        void cal()
        {
            double sum = 0;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                sum += double.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString());
            }
            lblTotal.Text = sum.ToString();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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
                sfd.FileName = "Parcel_Report.xls";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    //ToCsV(dataGridView1, @"c:\export.xls";
                    db.withReportTitle_ToCsV(dataGridView1, sfd.FileName, "Parcel Report"); // Here dataGridview1 is your grid view name 
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
    }
}
