using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace Hotal_Managment_Syatem
{
    
    public partial class Menu_Type : Form
    {
        Database db = new Database();
        string id = "";
        public Menu_Type()
        {
            InitializeComponent();
        }

     
        private void button1_Click(object sender, EventArgs e)
        {
            string qry="insert into Menu_Type values('"+txt_menutype.Text+"','"+txt_desc.Text+"')";
            db.InsertData(qry,"Menu_Type");
            MessageBox.Show("Record Saved Successfully", "Hotel", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dataGridView1.DataSource = db.Displaygrid("select menu_id,menu_type as Menu_Type from Menu_Type");

            txt_desc.Text = "";
            txt_menutype.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            txt_desc.Text = "";
            txt_menutype.Text = "";
        }

        private void Menu_Type_Load(object sender, EventArgs e)
        {
            this.CancelButton = button2;
            dataGridView1.DataSource = db.Displaygrid("select menu_id,menu_type as Menu_Type from Menu_Type");
            dataGridView1.Columns[0].Visible = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dlg = MessageBox.Show("Do you want to Delete this Record?","Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (dlg == DialogResult.Yes)
            {
                db.DeleteData("delete from Menu_Type where menu_id='" + id + "'", "Menu_Type");
                MessageBox.Show("Record deleted Successfully");
            }
            dataGridView1.DataSource = db.Displaygrid("select menu_id,menu_type as Menu_Type from Menu_Type");
            dataGridView1.Columns[0].Visible = false;
            txt_desc.Text = "";
            txt_menutype.Text = "";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           

        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            id = db.get_DataGridValue(dataGridView1, "Menu_Type", "menu_id", 0);
            SqlCommand cmd = new SqlCommand("select * from Menu_Type", db.cn);
            db.cnopen();
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read() == true)
            {
                txt_menutype.Text = rd["Menu_Type"].ToString();
                txt_desc.Text=rd["desc"].ToString();
            }
            db.cnclose();
        }
    }
}
