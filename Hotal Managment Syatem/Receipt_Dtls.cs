using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Hotal_Managment_Syatem
{
    public partial class Receipt_Dtls : Form
    {
        Database db = new Database();
        public Receipt rpt;
        public string cust_nm;
        public Receipt_Dtls()
        {
            InitializeComponent();
        }
        public Receipt_Dtls(string cust)
        {
            InitializeComponent();
            this.cust_nm = cust;
        }
        private void Receipt_Dtls_Load(object sender, EventArgs e)
        {
            lblCustName.Text = cust_nm;
            this.CancelButton = button1;
            dataGridView1.DataSource = db.Displaygrid("SELECT   PaidDate,  PaidAmt as [Paid Amount]  FROM  tbl_Cust_Receipt WHERE   (CustomerName = '" + cust_nm + "' and PaidAmt<>'0') group by PaidDate,PaidAmt,totalBill");
            dataGridView2.DataSource = db.Displaygrid("select tob.datetime,tob.billTime,tob.order_id,tob.Total_bill from total_bill tob inner join Custmer c on tob.Customer_id=c.Customer_id and c.name='" + cust_nm + "' where tob.order_id <>'200'");
     
        
        
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbBill_CheckedChanged(object sender, EventArgs e)
        {
            

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
            lblCustName.Text = cust_nm;
            this.CancelButton = button1;

            dataGridView2.DataSource = db.Displaygrid("select tob.datetime,tob.billTime,tob.order_id,tob.Total_bill from total_bill tob inner join Custmer c on tob.Customer_id=c.Customer_id and c.name='" + cust_nm + "' where tob.order_id <>'200'");
     
        }

        private void dataGridView2_DoubleClick(object sender, EventArgs e)
        {

            try
            {
                int i = dataGridView2.SelectedCells[0].RowIndex;
                int billNo = int.Parse(dataGridView2.Rows[i].Cells[2].Value.ToString());
                Bill_Print _editbill = new Bill_Print(billNo);
                _editbill.ShowDialog();
               // pageLoad();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }





        }
        //"select tob.datetime,tob.billTime,tob.order_id,tob.Total_bill from total_bill tob inner join Custmer c on tob.Customer_id=c.Customer_id and c.name='" + cust_nm + "'"


    }
}
