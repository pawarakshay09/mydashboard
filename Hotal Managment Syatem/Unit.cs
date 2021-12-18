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
   
    public partial class Unit : Form
    {
        Database db = new Database(); 
        int id;
        public Unit()
        {
            InitializeComponent();
        }

        private void Unit_Load(object sender, EventArgs e)
        {
            db.formFix(this);
            this.CancelButton = btn_close;
            fillgrid();
            btn_update.Enabled = false;
            btn_delete.Enabled = false;
        }
        public void fillgrid()
        {
            dataGridView1.DataSource = db.Displaygrid("select unit_id as Id,unit_name as [Unit Name] from tbl_unit");
            dataGridView1.Columns[0].Visible = false;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (db.ChkDb_Value("select * from tbl_unit where unit_name='" + txt_unit_nm.Text + "'"))
            {
                MessageBox.Show("Unit Name Already Exist", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (txt_unit_nm.Text == "")
                {
                    errorProvider1.SetError(txt_unit_nm, "Please Fill Unit Name");
                    lbl_erormsg.Text = "Please Fill Unit Name";
                }

                else
                {
                    errorProvider1.Clear();
                    db.insert("insert into tbl_unit values('" + txt_unit_nm.Text + "','" + txt_desc.Text + "')");
                    MessageBox.Show("Record Inserted Successfully");
                    fillgrid();
                    clear();
                }
            }
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            clear();
            //dataGridView1.DataSource = "";
        }
        public void clear()
        {
            txt_unit_nm.Text = "";
            txt_desc.Text = "";
            btn_update.Enabled = false;
            btn_delete.Enabled = false;
            btn_save.Enabled = true;
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            if (txt_unit_nm.Text == "")
            {
                errorProvider1.SetError(txt_unit_nm, "Please Fill Unit Name");
                lbl_erormsg.Text = "Please Fill Unit Name";
            }
            else if (db.ChkDb_Value("select * from tbl_unit where unit_name='" + txt_unit_nm.Text + "' and unit_id='" + label2.Text + "'"))
            {
                MessageBox.Show("Unit Name Already Exist", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else
            {
                //string id = db.getDb_Value("select unit_id from tbl_unit  where unit_name='" + txt_unit_nm.Text + "'").ToString();
                db.update("update tbl_unit set unit_name='" + txt_unit_nm.Text + "',desciption='" + txt_desc.Text + "' where unit_id='" + label2.Text + "'");
                MessageBox.Show("Record Updated Sucessfully");
                fillgrid();
                clear();
            }
            btn_save.Enabled = false;
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            db.deleteGridRow(dataGridView1, "tbl_unit", "unit_name", 1);
            //MessageBox.Show("Record Deleted Sucessfully");
            fillgrid();
            clear();
        }

        

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                id = dataGridView1.SelectedCells[0].RowIndex;
                label2.Text = dataGridView1.SelectedCells[0].Value.ToString();
                txt_unit_nm.Text = dataGridView1.SelectedCells[1].Value.ToString();
                txt_desc.Text = dataGridView1.SelectedCells[2].Value.ToString();
                btn_update.Enabled = true;
                
            }
            catch (Exception ex)
            {
               
            }
            btn_save.Enabled = false;
            btn_update.Enabled = true;
            btn_delete.Enabled = true;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            fillgrid();
            clear();
        }
    }
}
