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
    public partial class Rpt_table__sales_Table_wise : Form
    {
        Database db = new Database();
        public Rpt_table__sales_Table_wise()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string datefrm = dateTimePicker1.Value.ToString("MM'-'dd'-'yyyy");
            string dateto = dateTimePicker1.Value.ToString("MM'-'dd'-'yyyy");
            //string datefrm = dateTimePicker1.Value.ToString("dd'-'MM'-'yyyy");
            //string dateto = dateTimePicker2.Value.ToString("dd'-'MM'-'yyyy");

        //    dataGridView1.DataSource = db.Displaygrid("SELECT table_order.t_id AS [Table No], SUM(total_bill.Total_bill) AS Amount FROM table_order INNER JOIN total_bill ON table_order.order_id = total_bill.order_id WHERE (table_order.timeing BETWEEN '" + datefrm + "' AND '" + dateto + "') GROUP BY table_order.t_id");
            // qry by harsha
            dataGridView1.DataSource = db.Displaygrid("SELECT  table_order.t_id as [Table No.], SUM(total_bill.Total_bill) AS Total FROM table_order INNER JOIN   total_bill ON table_order.order_id = total_bill.order_id WHERE (total_bill.datetime BETWEEN '" + datefrm + "' and '" + dateto + "') GROUP BY table_order.t_id, total_bill.datetime");

             dataGridView1.Columns[1].Width = 160; 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

                SaveFileDialog sfd = new SaveFileDialog();
               
                sfd.Filter = "Excel Documents (*.xls)|*.xls";
                sfd.FileName = "SalesReport_Tablewise.xls";
             //  sfd.Title = "Sales Report Table wise";
              
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    //ToCsV(dataGridView1, @"c:\export.xls";
                    db.ToCsV(dataGridView1, sfd.FileName); // Here dataGridview1 is your grid view name 
                   // db.ToCsV(dataGridView1,sfd.Title);
                    MessageBox.Show("File Sucessfully Expoted");
                    string title = ("Sales Report Table wise");
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

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Rpt_table__sales_Table_wise_Load(object sender, EventArgs e)
        {
            lbl_date.Text = System.DateTime.Now.ToShortDateString();
            db.formFix(this);
            this.CancelButton = button4;
            dataGridView1.DataSource = db.Displaygrid("SELECT        table_order.t_id AS [Table No], SUM(total_bill.Total_bill) AS Amount FROM            table_order INNER JOIN  sales_item ON table_order.order_id = sales_item.order_id INNER JOIN total_bill ON table_order.order_id = total_bill.order_id GROUP BY table_order.t_id ");
            dataGridView1.Columns[1].Width = 160; 
        }
    }
}
