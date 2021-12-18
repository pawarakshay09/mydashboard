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
    public partial class Parcel_Detail_list : Form
    {
        Database db = new Database();
        public string cname, order_No,wname;
        public Parcel_Detail_list()
        {
            InitializeComponent();
        }
        public Parcel_Detail_list(string c_name,string o_id,string wName)
        {
            InitializeComponent();
            this.order_No = o_id;
            this.cname = c_name;
            this.wname = wName;
        }
        private void Parcel_Detail_list_Load(object sender, EventArgs e)
        {
            db.formFix(this);
           // db.comboFill(cmb_w_name, "select * from waiter_dtls","waiter_dtls","wname","wname");
            txt_cust_name.Text = cname;
            txt_order_no.Text = order_No;
            cmb_w_name.Text = wname;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            float c_id = float.Parse(db.getDb_Value("select Customer_id from Custmer where name='"+txt_cust_name.Text+"'").ToString());
            db.update("update tbl_parcel_order set w_name='" + cmb_w_name.Text + "',order_status='"+cmb_status.Text+"' where Customer_id='" + c_id + "'");
            MessageBox.Show("Status Updated Successfully ");
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
