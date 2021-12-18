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
    public partial class BirthdayReminder : Form
    {
        Database db = new Database();
         string todayDate = System.DateTime.Now.ToString("MM-dd-yyyy");

        public BirthdayReminder()
        {
            InitializeComponent();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BirthdayReminder_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Displaygrid("select name as [Customer Name],phone as [Mobile No.]  from Custmer where MONTH(dateOfBirth) = MONTH('" + todayDate + "') and DAY(dateOfBirth) = DAY('" + todayDate + "') ");
            dataGridView2.DataSource = db.Displaygrid("select name as [Customer Name],phone as [Mobile No.] from Custmer where MONTH(aniversaryDate) = MONTH('" + todayDate + "') and DAY(aniversaryDate) = DAY('" + todayDate + "') and status='Married' ");
            dataGridView1.Columns[0].Width = 220;
            dataGridView2.Columns[0].Width = 220;

        }
    }
}
