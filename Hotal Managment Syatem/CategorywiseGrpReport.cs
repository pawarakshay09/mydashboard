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
    public partial class CategorywiseGrpReport : Form
    {
        Database db = new Database();
        double sum = 0;
        public CategorywiseGrpReport()
        {
            InitializeComponent();
        }

        private void CategorywiseGrpReport_Load(object sender, EventArgs e)
        {
            this.CancelButton = btn_close;
            db.formFix(this);
            lblDate.Text = db.getDbstatus_Value("select ddate from tbl_dayend_status");
            dataGridView1.DataSource = db.Displaygrid("SELECT        menu.category as Category, SUM(total_bill.Total_bill) AS [Total Bill Amount] FROM            menu INNER JOIN     sales_item ON menu.menu_id = sales_item.menu_id INNER JOIN    total_bill ON sales_item.order_id = total_bill.order_id where total_bill.datetime ='"+Convert.ToDateTime(lblDate.Text).ToString("MM/dd/yyyy")+"' GROUP BY menu.category");
            dataGridView1.Columns[0].Width=180;
            cal();
        }

        void cal()
        {
            sum = 0; 
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                sum += double.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString());
            }
            txt_total.Text = sum.ToString();

        }
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Excel Documents (*.xls)|*.xls";
                sfd.FileName = "CategoryWise Report.xls";
                 if (sfd.ShowDialog() == DialogResult.OK)
                {

                     db.withReportTitle_ToCsV(dataGridView1, sfd.FileName,"CategoryWise Report of "+lblDate.Text+""); // Here dataGridview1 is your grid view name 
                    MessageBox.Show("File Sucessfully Exported");
 
                    Process.Start(sfd.FileName);


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
