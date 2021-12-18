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
    public partial class waiter_prsenty : Form
    {
        Database db = new Database();
        bool flag;
        string st = "";
        int wID;
        public waiter_prsenty()
        {
            InitializeComponent();
        }

        private void waiter_prsenty_Load(object sender, EventArgs e)
        {
            this.CancelButton = btn_back;
            db.comboFill(cmbWaiterName, "SELECT wname FROM waiter_dtls", "waiter_dtls", "waiter_dtls", "wname");
            label_date.Text=System.DateTime.Now.ToShortDateString();
            prsenty_bind();           
        }

        void insert_waiter_dtls()
        {
            string query = "INSERT INTO waiter_prsenty(w_id,date,status,remark) VALUES("+ wID +",'"+label_date.Text+"','"+st+"','"+textBox_remark.Text+"') ";

            SqlCommand cmd = new SqlCommand(query, db.cn);
            db.cnopen();
            int cnt = (int)cmd.ExecuteNonQuery();
            if (cnt != 0)
            {
                MessageBox.Show("Prsenty Added Sucessfully", "Status Message");
            }
            db.cnclose();
        }

        void prsenty_bind()
        {
            string query = "SELECT w_id FROM waiter_dtls WHERE wname='" + cmbWaiterName.Text + "'";
            wID = db.GetUniqueId(query);

            SqlDataAdapter da = new SqlDataAdapter("SELECT waiter_dtls.wname as Waiter_Name, waiter_prsenty.status Status, waiter_prsenty.date as Date FROM waiter_dtls INNER JOIN waiter_prsenty ON waiter_dtls.w_id = waiter_prsenty.w_id WHERE(waiter_prsenty.date = '" + label_date.Text + "')", db.cn);
            db.cnopen();
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            db.cnclose();
        }

        void rechk_prsenty()
        {
            string qur = "Select w_id from waiter_prsenty where w_id=" + wID + " and date = '" + label_date.Text + "'";
                      
            SqlCommand cmd = new SqlCommand(qur, db.cn);
            db.cnopen();
            SqlDataReader rd = cmd.ExecuteReader();
            if(rd.Read() == true)
            {
                MessageBox.Show("Waiter Prsenty Alredy Added","Waring",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                flag = false;
            }
            db.cnclose();
        }


        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbWaiterName_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxw_name.Text = cmbWaiterName.Text;
            prsenty_bind();
        }

        private void btnAddPresenty_Click(object sender, EventArgs e)
        {
            flag = true;
            if (radioButton1.Checked == true)
            {
                st = "P";
            }
            else
            {
                st = "A";
            }
            rechk_prsenty();

            if (flag == true)
            {
                insert_waiter_dtls();
            }
            prsenty_bind();
        }

        private void btnShowDetails_Click(object sender, EventArgs e)
        {

        }   
    }
}
