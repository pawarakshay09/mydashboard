using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Hotal_Managment_Syatem
{
    
    public partial class Rpt_table__sales_date_wise : Form
    {
        Database db = new Database();
        public Rpt_table__sales_date_wise()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string fdate = dateTimePicker1.Value.ToString("MM'-'dd'-'yyyy");
            string sdate = dateTimePicker2.Value.ToString("MM'-'dd'-'yyyy");
            dataGridView1.DataSource = db.Displaygrid("SELECT  datetime AS Date, SUM(Total_bill) AS [Counter Amount] FROM  total_bill WHERE (datetime between '" + fdate + "' and '" + sdate + "') GROUP BY datetime ");// AND (invoice_dtls.invoice_id = @id)");
            dataGridView1.Columns[0].Width = 150;
            dataGridView1.Columns[1].Width = 150;

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
                sfd.FileName = "SalesReportDatewise.xls";
                //Table table = new Table();
                ////table.GridLines = dataGridView1.GridLines;
                //table.Caption = "Report";
                //table.CaptionAlign = TableCaptionAlign.Top;
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

        private void Rpt_table__sales_date_wise_Load(object sender, EventArgs e)
        {
            lbl_date.Text = System.DateTime.Now.ToShortDateString();
            db.formFix(this);
            this.CancelButton = btn_close;
            /*SELECT        datetime AS Date, SUM(Total_bill) AS CounterAmount
FROM            total_bill
GROUP BY datetime*/

            //dataGridView1.DataSource = db.Displaygrid("SELECT  datetime AS Date, SUM(Total_bill) AS CounterAmount FROM  total_bill GROUP BY datetime");// AND (invoice_dtls.invoice_id = @id)");
            //dataGridView1.Columns[0].Width = 150;
            cal();
        }
    }
}
