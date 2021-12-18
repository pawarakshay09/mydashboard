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
    public partial class Material : Form
    {
        Database db = new Database();
        public Material()
        {
            InitializeComponent();
        }

        private void btnAddMenuItem_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (txt_material.Text == "")
            {
                errorProvider1.SetError(txt_material, "Enter Material Name");
                lblError.Text = "Error - Please Enter  Material Name";
            }
            else if (cmb_unit.Text == "")
            {
                errorProvider1.SetError(cmb_unit, "Enter Unit");
                lblError.Text = "Error - Please Select Unit";
            }
            else
            {
                if (db.ChkDb_Value("select * from material_nm where material_nm='" + txt_material.Text + "'"))
                {
                    MessageBox.Show("Material Already Exists!!!", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txt_material.Focus();
                    dataGridView1.DataSource = db.Displaygrid("select id,material_nm as [Material Name],unit as Unit,description as [Description],minimum_stock as[Min Stock] from material_nm ");
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[3].Visible = false;
                    dataGridView1.Columns[1].Width = 130;
                    dataGridView1.Columns[2].Width = 56;
                    dataGridView1.Columns[4].Width = 56;
                    clear();
                }
                else
                {
                    db.insert("insert into material_nm values('" + txt_material.Text + "','" + cmb_unit.Text + "','" + txt_description.Text + "','" + txt_min_stock.Text + "')");
                    //column name added into query added by sagar on 10072018
                    db.insert("insert into tbl_stock (pur_id,item_name,qty,unit,itemType) values('0','" + txt_material.Text + "','0','" + cmb_unit.Text + "','0')");
                    MessageBox.Show("Record Inserted Successfully", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = db.Displaygrid("select id,material_nm as [Material Name],unit as Unit,description as [Description],minimum_stock as[Min Stock] from material_nm ");
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[3].Visible = false;
                    dataGridView1.Columns[1].Width = 130;
                    dataGridView1.Columns[2].Width = 56;
                    dataGridView1.Columns[4].Width = 56;
                    clear();
                }
            }
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            clear();
        }
        void clear()
        {
            errorProvider1.Clear();
            txt_description.Text = "";
            txt_material.Text = "";
            cmb_unit.Text = "";
            txt_min_stock.Text = "0";
            txt_search.Text = "";
            btn_delete.Enabled = false;
            btnAddMenuItem.Enabled = true;
        }
        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Material_Load(object sender, EventArgs e)
        {
            db.comboFill(cmb_menu_nm, "select * from menu", "menu", "m_name", "m_name");
            db.comboFill(cmb_unit, "select * from tbl_unit", "tbl_unit", "unit_name", "unit_name");
            this.CancelButton = btn_close;
            db.formFix(this);
            dataGridView1.DataSource = db.Displaygrid("select id,material_nm as [Material Name],unit as Unit,description as [Description],minimum_stock as[Min Stock] from material_nm ");
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[1].Width = 130;
            dataGridView1.Columns[2].Width = 56;
            dataGridView1.Columns[4].Width = 56;
           // dataGridView1.Columns[4].Visible = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                foreach (DataGridViewRow item in this.dataGridView1.SelectedRows)
                {
                    dataGridView1.Rows.RemoveAt(item.Index);

                }
                int i = dataGridView1.SelectedCells[0].RowIndex;
                string MaterialName = dataGridView1.Rows[i].Cells[1].Value.ToString();

                db.DeleteData("delete from material_nm where material_nm='" + MaterialName + "' ", "material_nm");
            }
        }

       
        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            string id = db.get_DataGridValue(dataGridView1, "material_nm ", "id ",0);
            db.cnopen();
            SqlCommand cmd = new SqlCommand("select * from material_nm where id='" + id + "'", db.cn);
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                txt_material.Text = rd["material_nm"].ToString();
               cmb_unit.Text=rd["unit"].ToString();
                txt_description.Text=rd["description"].ToString();
                txt_min_stock.Text = rd["minimum_stock"].ToString();
            }
            db.cnclose();
            btn_delete.Enabled = true;
            btnAddMenuItem.Enabled = false;
        }

        private void btn_delete_Click(object sender, EventArgs e)
        { 
               int stk=0;
              if(db.ChkDb_Value("select qty from tbl_stock where item_name='" + txt_material.Text + "'"))
                  stk = Convert.ToInt32( db.getDb_Value("select qty from tbl_stock where item_name='" + txt_material.Text + "'"));
                if (stk > 0)
                {
                    MessageBox.Show("This Material is Available in Stock.", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    DialogResult dlg = MessageBox.Show("Do you Really want to Delete Record?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlg == DialogResult.Yes)
                    {
                        string id = db.get_DataGridValue(dataGridView1, "material_nm ", "id ", 0);
                        db.DeleteData("delete from material_nm where id='" + id + "'", "material_nm");
                        MessageBox.Show("Record Deleted Sucessfully!!!", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                dataGridView1.DataSource = db.Displaygrid("select id,material_nm as [Material Name],unit as Unit,description as [Description],minimum_stock as[Min Stock] from material_nm ");
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[3].Visible = false;
                dataGridView1.Columns[1].Width = 130;
                dataGridView1.Columns[2].Width = 56;
                dataGridView1.Columns[4].Width = 56;
            clear();
        }

        private void txt_min_stock_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Displaygrid("select id,material_nm as [Material Name],unit as Unit,description as [Description],minimum_stock as[Min Stock] from material_nm where material_nm like '" + txt_search.Text + "%'  ");
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[1].Width = 130;
            dataGridView1.Columns[2].Width = 56;
            dataGridView1.Columns[4].Width = 56;
        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
        }

        private void txt_min_stock_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            Unit unit = new Unit();
            unit.ShowDialog();
            db.comboFill(cmb_unit, "select * from tbl_unit", "tbl_unit", "unit_name", "unit_name");
        }
    }
}
