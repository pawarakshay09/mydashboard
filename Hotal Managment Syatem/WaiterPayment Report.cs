using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using CrystalDecisions.CrystalReports.Engine;


namespace Hotal_Managment_Syatem
{
    public partial class WaiterPayment_Report : Form
    {
        Database db = new Database();
        public WaiterPayment_Report()
        {
            InitializeComponent();
            //this.MaximumSize = MinimumSize = this.Size;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {


                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM   Waiter_Payment where date BETWEEN '" + dtpfrom.Text + "' AND '" + dtpto.Text + "'  AND W_id=(Select W_id from waiter_dtls where wname='"+cmbWaiterName.Text +"')", db.cn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count <= 0)
                {
                    MessageBox.Show("Informatin Not Available", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dtpfrom.Focus();
                    return;
                }
                ReportDocument rd = new ReportDocument();
               // rd.Load(@"F:\hotel mgmt 22-4-13\Hotel Mgt\Hotal Managment Syatem\waiterPaymentReport.rpt");
                rd.Load(Application.StartupPath + "\\waiterPaymentReport.rpt");
                rd.Database.Tables[0].SetDataSource(dt);
                crystalReportViewer1.ReportSource = rd;
                dtpfrom.Focus();

            }
            catch (Exception ex)
            {
                MessageBox.Show("error:" + ex.Message, "listbox", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
        
        }

        private void cmbWaiterName_SelectedIndexChanged(object sender, EventArgs e)
        {
        
        }

        private void WaiterPayment_Report_Load(object sender, EventArgs e)
        {
            this.CancelButton = button1;
            db.comboFill(cmbWaiterName, "SELECT wname FROM waiter_dtls", "waiter_dtls", "waiter_dtls", "wname");
         
        }
    }
}
