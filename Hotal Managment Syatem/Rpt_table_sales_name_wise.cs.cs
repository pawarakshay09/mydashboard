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
    public partial class Rpt_table__sales_name_wise : Form
    {
        Database db = new Database();
        bool chkBoxflag = false;
        public Rpt_table__sales_name_wise()
        {
            InitializeComponent();
        }

        private void Rpt_table__sales_name_wise_Load(object sender, EventArgs e)
        {
            db.formFix(this);
            this.CancelButton = button4;
            lbl_date.Text = System.DateTime.Now.ToShortDateString();

            db.comboFill(cmb_waiter_nm, "select * from waiter_dtls", "waiter_dtls", "wname", "wname");
            cmb_waiter_nm.Text = "--Select--";
            dataGridView1.DataSource = db.Displaygrid(" SELECT  waiter_dtls.wname as [Waiter Name], SUM(total_bill.Total_bill) AS TotalAmount FROM            waiter_dtls INNER JOIN table_order ON waiter_dtls.w_id = table_order.w_id INNER JOIN  total_bill ON table_order.order_id = total_bill.order_id  GROUP BY waiter_dtls.wname ");
            dataGridView1.Columns[0].Width = 160;
            cal();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string datefrm=dateTimePicker1.Value.ToString("MM'-'dd'-'yyyy");
            string dateto=dateTimePicker2.Value.ToString("MM'-'dd'-'yyyy");
       
            if(chkBoxflag)
                dataGridView1.DataSource = db.Displaygrid(" SELECT  waiter_dtls.wname as [Waiter Name], SUM(total_bill.Total_bill) AS TotalAmount FROM            waiter_dtls INNER JOIN table_order ON waiter_dtls.w_id = table_order.w_id INNER JOIN  total_bill ON table_order.order_id = total_bill.order_id   WHERE      (total_bill.datetime BETWEEN '" + datefrm + "' AND '" + dateto + "') GROUP BY waiter_dtls.wname "); 
            else
                dataGridView1.DataSource = db.Displaygrid(" SELECT  waiter_dtls.wname as [Waiter Name], SUM(total_bill.Total_bill) AS TotalAmount FROM            waiter_dtls INNER JOIN table_order ON waiter_dtls.w_id = table_order.w_id INNER JOIN  total_bill ON table_order.order_id = total_bill.order_id   WHERE     (waiter_dtls.wname = '" + cmb_waiter_nm.Text + "') AND (total_bill.datetime BETWEEN '" + datefrm + "' AND '" + dateto + "') GROUP BY waiter_dtls.wname "); 
            
            dataGridView1.Columns[0].Width = 160;
            cal();
        }
        void cal()
        {
            float sum = 0;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                sum += float.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString());
            }
            txt_total.Text = sum.ToString();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Excel Documents (*.xls)|*.xls";
                sfd.FileName = "SalesReport_WaiterWise.xls";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    //ToCsV(dataGridView1, @"c:\export.xls";
                    db.ToCsV(dataGridView1, sfd.FileName); // Here dataGridview1 is your grid view name 
                    MessageBox.Show("File Sucessfully Exported");
 
                    Process.Start(sfd.FileName);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmb_waiter_nm_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            { cmb_waiter_nm.Enabled = false;
            chkBoxflag = true;
            }
            else
            { cmb_waiter_nm.Enabled = true; chkBoxflag = false; }
 
        }
    }
}
