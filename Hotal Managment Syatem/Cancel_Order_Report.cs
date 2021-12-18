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
    public partial class Cancel_Order_Report : Form
    {
        Database db = new Database();
         string datefrm,dateto;
           
        public Cancel_Order_Report()
        {
            InitializeComponent();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Cancel_Order_Report_Load(object sender, EventArgs e)
        {
            db.formFix(this);
            this.CancelButton = btn_close;

            lbldate.Text = db.getDbstatus_Value("select ddate from tbl_dayend_status");
           // string date = Convert.ToDateTime(lbldate.Text).ToString();

            dataGridView1.DataSource = db.Displaygrid("select order_id as [Bill No.],t_id as [Table No.],amount as [Amount],by_whome as [By],date as Date,time as Time,reason as Reason from Cancel_order where date='" +Convert.ToDateTime(lbldate.Text).ToString("MM/dd/yyyy") + "'");
            dataGridView1.Columns[0].Width=70;
             dataGridView1.Columns[1].Width=80;
             dataGridView1.Columns[2].Width=70;
             dataGridView1.Columns[4].Width = 75;
             cal();
        }
        void cal()
        {
            double sum = 0;
            for (int k = 0; k < dataGridView1.RowCount; k++)
            {
                sum+=double.Parse(dataGridView1.Rows[k].Cells[2].Value.ToString());
            }
            txt_total.Text = sum.ToString();
        }
        private void btn_export_Click(object sender, EventArgs e)
        {
            try
            {

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Excel Documents (*.xls)|*.xls";
                sfd.FileName = "Cancel_OrderReport.xls";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    //ToCsV(dataGridView1, @"c:\export.xls";
                    db.ToCsV(dataGridView1, sfd.FileName); // Here dataGridview1 is your grid view name 
                    MessageBox.Show("File Sucessfully Expoted");

                    // string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Solution.xls");
                    // string filePath = Path.Combine(Environment.GetFolderPath(sfd.Selected), sfd.FileName);

                    Process.Start(sfd.FileName);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dtpFrom_ValueChanged(object sender, EventArgs e)
        {
            datebetween();
        }
        void datebetween()
        {
             datefrm = dtpFrom.Value.ToString("MM/dd/yyyy");
             dateto = dtp_to.Value.ToString("MM/dd/yyyy");
            dataGridView1.DataSource = db.Displaygrid("select order_id as [Bill No.],t_id as [Table No.],amount as [Amount],by_whome as [By],date as Date,time as Time,reason as Reason from Cancel_order where date between '" + datefrm + "' and '" + dateto + "'");
            cal();
        }

        private void dtp_to_ValueChanged(object sender, EventArgs e)
        {
            datebetween();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            int j = dataGridView1.SelectedCells[0].RowIndex;
            int billNo = int.Parse(dataGridView1.Rows[j].Cells[0].Value.ToString());
            OrderDeatails o_dtls = new OrderDeatails(billNo);
            o_dtls.ShowDialog();
        }
    }
}
