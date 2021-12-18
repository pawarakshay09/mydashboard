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
    public partial class Add_Drink_Group : Form
    {
        Database db = new Database();
        public Add_Drink_Group()
        {
            InitializeComponent();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (txt_grpName.Text == "")
            {
                errorProvider1.SetError(txt_grpName,"Please Enter Group Name.");
                lblError.Text = "Error - Please Enter Group Name.";
                txt_grpName.Focus();
            }
            else if (txt_qtyML.Text == "")
            {
               errorProvider1.SetError(txt_qtyML,"Please Enter Qty.");
               lblError.Text = "Error - Please Enter Qty.";
                txt_qtyML.Focus();
            }
            else if (db.ChkDb_Value("select * from tbl_drinkgroup where grpName='" + txt_grpName.Text + "'"))
                MessageBox.Show("Record Already Exists!!", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (db.confirm())
                {
                    db.insert("insert into tbl_drinkgroup values('" + txt_grpName.Text + "','" + txt_qtyML.Text + "')");
                    MessageBox.Show("Record Inserted Successfully!!!", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clear();
                    bind();
                }
            }
        }
        void clear()
        {
            txt_qtyML.Text = "";
            txt_grpName.Text = "";
            btn_save.Enabled = true;
            btnDelete.Enabled = false;
            lblError.Text = "";
        }
        void bind()
        {
            dataGridView1.DataSource = db.Displaygrid("select id,grpName as [Group Name],qtyInML as [Qty in ML] from tbl_drinkgroup");
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Width = 140;
            dataGridView1.Columns[2].Width = 100;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            float id =float.Parse(db.get_DataGridValue(dataGridView1, "tbl_drinkgroup", "id", 0).ToString());
            DialogResult dlg = MessageBox.Show("Do You Really want to Delete record?","Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (dlg == DialogResult.Yes)
            {
                db.DeleteData("delete from tbl_drinkgroup where id='"+id+"'","tbl_drinkgroup");
                MessageBox.Show("Record Deleted Successfully!!!");
                
            }
            clear();
            bind();
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            clear();

        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Add_Drink_Group_Load(object sender, EventArgs e)
        {
            db.formFix(this);
            this.CancelButton = btn_close;
            bind();
        }

        private void txt_qtyML_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            btnDelete.Enabled = true;
            btn_save.Enabled = false;
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Displaygrid("select id,grpName as [Group Name],qtyInML as [Qty in ML] from tbl_drinkgroup where grpName like '" + txt_search.Text + "%'  ");
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Width = 120;
            dataGridView1.Columns[2].Width = 100;
        }

        private void txt_qtyML_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);
        }
    }
}
