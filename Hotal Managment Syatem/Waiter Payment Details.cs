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
    public partial class Waiter_Payment_Details : Form
    {
        Database db=new Database();
        public Waiter_Payment_Details()
        {
            InitializeComponent();
        }

        private void Waiter_Payment_Details_Load(object sender, EventArgs e)
        {
            this.CancelButton = button1;
            db.comboFill(cmbWaiterName, "SELECT wname FROM waiter_dtls", "waiter_dtls", "waiter_dtls", "wname");
            lbl_date.Text = System.DateTime.Now.ToShortDateString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string datefrm=dtpfrom.Value.ToString("MM'-'dd'-'yyyy");
             string dateto=dtpto.Value.ToString("MM'-'dd'-'yyyy");
            dataGridView1.DataSource=db.Displaygrid("SELECT date as Date,present_days as [Present Days],amount as Amount,W_status as[Status] FROM   Waiter_Payment where date BETWEEN '" + datefrm + "' AND '" + dateto + "'  AND W_id=(Select W_id from waiter_dtls where wname='"+cmbWaiterName.Text +"')");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Excel Documents (*.xls)|*.xls";
                sfd.FileName = "WaiterPaymentReport.xls";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    //ToCsV(dataGridView1, @"c:\export.xls";
                    db.ToCsV(dataGridView1, sfd.FileName); // Here dataGridview1 is your grid view name 
                    MessageBox.Show("File Sucessfully Exported");

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
    }
}
