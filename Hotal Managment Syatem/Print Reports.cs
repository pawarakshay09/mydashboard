using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Hotal_Managment_Syatem
{
    public partial class Print_Reports : Form
    {
        Database db = new Database();
        string[] counterarray = new string[100];
        string[] qryArray = new string[150];
        int ctr = 0;
        string qry;
        LPrinter lp = new LPrinter();
        public Print_Reports()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string date = db.getDbstatus_Value("select ddate from tbl_dayend_status");
            DateTime dt = Convert.ToDateTime(date);
            string todaydate = dt.ToString("MM-dd-yyyy");
         
                lp.printCategoryWiseReport(dtpfrom.Text,dtpTo.Text);
           

        }
      
        void sum()
        {
            double total = 0;
            for (int i = 0; i < dataGridView1.RowCount-1; i++)
            {
                total += double.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
            }
            lblTotal.Text = total.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rdbCategory_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Print_Reports_Load(object sender, EventArgs e)
        {
            lblDate.Text = db.getDbstatus_Value("select ddate from tbl_dayend_status");
            db.formFix(this);
            this.CancelButton = btnClose;
        }
    }
}
