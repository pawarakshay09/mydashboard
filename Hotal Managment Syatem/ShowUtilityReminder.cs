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
    public partial class ShowUtilityReminder : Form
    {
        Database db = new Database();
        public ShowUtilityReminder()
        {
            InitializeComponent();
        }

        private void ShowUtilityReminder_Load(object sender, EventArgs e)
        {
            bind_Dg();
           
            DateTime dtmain = System.DateTime.Now;
            string date = Convert.ToDateTime(dtmain).ToString("dd-MM-yyyy");
          
           lblDate.Text = date;
            this.CancelButton = btn_close;
        }

        void bind_Dg()
        {
            DateTime dtmain = System.DateTime.Now;
            string date = Convert.ToDateTime(dtmain).ToString("MM-dd-yyyy");
            dgvReminder.DataSource = db.Displaygrid("select ReminderFor as [Reminder],ReminderDate as[Date] from ReminderSetting where ReminderDate = '" + date + "'");
            dgvReminder.Columns[0].Width = 180;

        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
