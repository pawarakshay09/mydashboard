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
    public partial class UpdatePresenty : Form
    {
        Database db = new Database();
        public UpdatePresenty()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string date=dtp_date.Value.ToString("MM-dd-yyyy");
            string w_id = db.getDb_Value("select w_id from waiter_dtls where wname='"+cmb_waiter.Text+"'").ToString();
            db.update("update waiter_prsenty set w_id='"+w_id+"',date='"+date+"',status='"+cmb_status.Text+"' where date='"+date+"' and w_id='"+w_id+"'");
            MessageBox.Show("Record Updated Successfully","Confirm",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UpdatePresenty_Load(object sender, EventArgs e)
        {
            db.comboFill(cmb_waiter, "select * from waiter_dtls","waiter_dtls","wname","wname");
            cmb_waiter.Text = "--Select--";
            db.formFix(this);
            this.CancelButton = button1;
        }
    }
}
