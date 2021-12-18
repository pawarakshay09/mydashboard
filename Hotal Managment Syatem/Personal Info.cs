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
    public partial class Personal_Info : Form
    {
        Database db = new Database();
        public string rm_no;
        string value_id = "";
        public Personal_Info()
        {
            InitializeComponent();
        }
        public Personal_Info(string room_no)
        {
            InitializeComponent();
            this.rm_no = room_no;
        }
       

        private void Personal_Info_Load(object sender, EventArgs e)
        {
            db.formFix(this);
            this.CancelButton = button2;
            txt_room_no.Text = rm_no;
        }
                                                                
        private void btn_save_Click(object sender, EventArgs e)
        {
            string date=dtp_date.Value.ToString("MM-dd-yyyy");
            db.insert("insert into Personal_Info values('"+txt_room_no.Text+"','"+txt_name.Text+"','"+txt_address.Text+"','"+txt_mob.Text+"','"+cmb_address_prooof.Text+"','"+txt_id.Text+"','"+date+"','"+dtp_time.Text+"','"+txt_adv_payment.Text+"')");
            MessageBox.Show("Record Inserted Successfully","Confirm",MessageBoxButtons.OK,MessageBoxIcon.Information);
            bind_dg();
            //bind_dg();
            clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dlg = MessageBox.Show("Do You Really want to update record","Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (dlg == DialogResult.Yes)
            {
                db.update("update Personal_Info set name='" + txt_name.Text + "',address='" + txt_address.Text + "',mobile='" + txt_mob.Text + "',address_proof='" + cmb_address_prooof.Text + "',address_id='" + txt_id.Text + "' where room_no='" + txt_room_no.Text + "'");
                MessageBox.Show("Record updated successfully","Confirm",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            bind_dg();
            clear();
        }

        void bind_dg()
        {
            dataGridView1.DataSource = db.Displaygrid("select p_id, room_no as [Room No],cust_name as Name,address as Address,mobile_no as [Mobile No],address_proof as [Address Proof],proof_id as [Address_Id] from Personal_Info");
            dataGridView1.Columns[0].Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            value_id = db.get_DataGridValue(dataGridView1, "Personal_Info","p_id",0).ToString();
            DialogResult dlg = MessageBox.Show("Do You Really want to Delete record", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dlg == DialogResult.Yes)
            {
                db.DeleteData("delete from Personal_Info where id='" + value_id + "'", "Personal_Info");
                MessageBox.Show("Record Deleted successfully", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            bind_dg();
            clear();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            clear();
        }
        void clear()
        {
            txt_name.Text = "";
            txt_address.Text = "";
            txt_adv_payment.Text = "";
            txt_mob.Text = "";
            txt_room_no.Text = "";
            cmb_address_prooof.Text = "";
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
