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
    public partial class Cancel_KOT_Details : Form
    {
        Database db = new Database();
        string dateFrm, dateTo;
        public Cancel_KOT_Details()
        {
            InitializeComponent();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Cancel_KOT_Details_Load(object sender, EventArgs e)
        {
            db.formFix(this);
            this.CancelButton = btn_close;
            lblDate.Text = db.getDbstatus_Value("select ddate from tbl_dayend_status");
            dataGridView1.DataSource = db.Displaygrid("SELECT        date AS Date, orderId AS [Order ID], tblNo AS [Table No.], menuName AS [Menu Name], qty AS Qty, rate AS Rate, amount AS Amount, waiterName AS [Waiter Name] FROM            CancelKOTDetails where date='"+Convert.ToDateTime(lblDate.Text).ToString("MM/dd/yyyy")+"'");
            cellFormation();
        }

       
        void dateChange()
        {
            dateFrm = dtpFrom.Value.ToString("MM/dd/yyyy");
            dateTo = dtpTo.Value.ToString("MM/dd/yyyy");

            dataGridView1.DataSource = db.Displaygrid("SELECT        date AS Date, orderId AS [Order ID], tblNo AS [Table No.], menuName AS [Menu Name], qty AS Qty, rate AS Rate, amount AS Amount, waiterName AS [Waiter Name] FROM            CancelKOTDetails where date between '" + dateFrm + "' and '"+dateTo+"'");
            cellFormation();
        }

        private void dtpFrom_ValueChanged(object sender, EventArgs e)
        {
            dateChange();
        }

        private void dtpTo_ValueChanged(object sender, EventArgs e)
        {
            dateChange();

        }

        private void chkDate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDate.Checked)
            {
                dtpFrom.Enabled = true;
                dtpTo.Enabled = true;
                dateChange();
            }
            else
            {
                dtpFrom.Enabled = false;
                dtpTo.Enabled = false;
                dataGridView1.DataSource = db.Displaygrid("SELECT        date AS Date, orderId AS [Order ID], tblNo AS [Table No.], menuName AS [Menu Name], qty AS Qty, rate AS Rate, amount AS Amount, waiterName AS [Waiter Name] FROM            CancelKOTDetails where date='" + Convert.ToDateTime(lblDate.Text).ToString("MM/dd/yyyy") + "'");
                cellFormation();
            }
        }
        void cellFormation()
        {
            dataGridView1.Columns[3].Width = 220;
            dataGridView1.Columns[1].Width = 60;
            dataGridView1.Columns[2].Width = 60;
            dataGridView1.Columns[4].Width = 60;
            dataGridView1.Columns[5].Width = 60;
            dataGridView1.Columns[6].Width = 80;
            dataGridView1.Columns[7].Width = 120;
            cal();
        }

        void cal()
        {
            double sumQty=0,sumAmt=0;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                sumQty+=double.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString());
                sumAmt += double.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString());
            }
            lblQty.Text = sumQty.ToString();
            lblAmt.Text = sumAmt.ToString();
        }
        private void btn_export_Click(object sender, EventArgs e)
        {
            try
            {

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Excel Documents (*.xls)|*.xls";
                sfd.FileName = "CancelKOTReport.xls";

                if (sfd.ShowDialog() == DialogResult.OK)
                {

                    //ToCsV(dataGridView1, @"c:\export.xls";
                    db.withReportTitle_ToCsV(dataGridView1, sfd.FileName,"Cancel KOT Report"); // Here dataGridview1 is your grid view name 
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
