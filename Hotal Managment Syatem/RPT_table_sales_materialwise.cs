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
    public partial class RPT_table_sales_materialwise : Form
    {
        Database db = new Database();
        public RPT_table_sales_materialwise()
        {
            InitializeComponent();
        }

        private void RPT_table_sales_materialwise_Load(object sender, EventArgs e)
        {
            lbl_date.Text = System.DateTime.Now.ToShortDateString();
            db.formFix(this);
            this.CancelButton = button4;
            db.comboFill(cmb_item_nm,"select *from menu","menu","m_name","m_name");
            cmb_item_nm.Text = "--Select--";

            //dataGridView1.DataSource = db.Displaygrid("SELECT  menu.m_name AS [Item Name], SUM(sales_item.qty) AS Qty, SUM(total_bill.Total_bill) AS Total FROM            total_bill INNER JOIN sales_item ON total_bill.order_id = sales_item.order_id INNER JOIN menu ON sales_item.menu_id = menu.menu_id GROUP BY menu.m_name");
            //dataGridView1.Columns[0].Width = 160;
            //dataGridView1.Columns[1].Width = 100;
            cal();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string datefrm=dateTimePicker1.Value.ToString("MM'-'dd'-'yyyy");
            string dateto=dateTimePicker2.Value.ToString("MM'-'dd'-'yyyy");
           // dataGridView1.DataSource = db.Displaygrid("SELECT menu.m_name as [Item Name],sales_item.qty as Qty, SUM(total_bill.Total_bill) AS Total FROM  total_bill INNER JOIN   sales_item ON total_bill.order_id = sales_item.order_id INNER JOIN menu ON sales_item.menu_id = menu.menu_id WHERE  (total_bill.datetime between '" + datefrm + "' and '" + dateto + "') GROUP BY menu.m_name,sales_item.qty");//(menu.m_name = '"+cmb_item_nm.Text+"') AND  
            dataGridView1.DataSource = db.Displaygrid("SELECT        menu.m_name AS [Item Name], SUM(sales_item.qty) AS Qty, SUM(sales_item.total_amount) AS Total FROM            total_bill INNER JOIN sales_item ON total_bill.order_id = sales_item.order_id INNER JOIN menu ON sales_item.menu_id = menu.menu_id WHERE  (total_bill.datetime between '" + datefrm + "' and '" + dateto + "') GROUP BY menu.m_name");
            dataGridView1.Columns[0].Width = 160;
            dataGridView1.Columns[1].Width = 100;
            cal();
        }
        void cal()
        {
            string datefrm = dateTimePicker1.Value.ToString("MM'-'dd'-'yyyy");
            string dateto = dateTimePicker2.Value.ToString("MM'-'dd'-'yyyy");

            float sum = 0;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                sum += float.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString());
            }
            txt_total.Text = sum.ToString();
            lblAppliedDisc.Text = db.getDb_Value("SELECT        SUM(table_order.discAmt) AS Expr1 FROM            total_bill INNER JOIN   table_order ON total_bill.order_id = table_order.order_id where  (total_bill.datetime between '" + datefrm + "' and '" + dateto + "')").ToString();
            lblGrand.Text =Math.Round(float.Parse(txt_total.Text) -float.Parse(lblAppliedDisc.Text)).ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Excel Documents (*.xls)|*.xls";
                sfd.FileName = "SalesReport_Materialwise.xls";
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

        private void cmb_item_nm_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
