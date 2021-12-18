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
    public partial class Material_Distribute : Form
    {
        Database db=new Database();
        bool page = false;
        public Material_Distribute()
        {
            InitializeComponent();
        }

        private void Material_Distribute_Load(object sender, EventArgs e)
        {
            pageLoad();
        }
        public void pageLoad()
        {
            db.formFix(this);
            this.CancelButton = btn_close;
            db.comboFill(cmb_material_nm, "select * from material_nm", "material_nm", "material_nm", "material_nm");
            db.comboFill(comboBoxMenuGroup, "select distinct category from menu", "menu", "category", "category");
            // db.comboFill(cmb_item_nm, "select * from menu", "menu", "m_name", "m_name");
             page = true;
        }

        

        private void btnAddMenuItem_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (comboBoxMenuGroup.Text == "")
            {
                errorProvider1.SetError(comboBoxMenuGroup, "Enter Menu Group");
                lblError.Text = "Error - Please Select Menu Group.";
            }
            else if (cmb_item_nm.Text == "")
            {
                errorProvider1.SetError(cmb_item_nm, "Enter Item Name");
                lblError.Text = "Error - Please Select Menu Name";
            }
            else if (cmb_material_nm.Text == "")
            {
                errorProvider1.SetError(cmb_material_nm, "Enter Material Name");
                lblError.Text = "Error - Please Select  Material Name";
            }
            else if (txt_qty.Text == "")
            {
                errorProvider1.SetError(txt_qty, "Enter Qty");
                lblError.Text = "Error - Please Enter Qty.";
            }
            else if (cmb_unit.Text == "")
            {
                errorProvider1.SetError(cmb_unit, "Enter Unit");
                lblError.Text = "Error - Please Select Unit.";
            }
            else
            {

                db.insert("insert into tbl_material_use values('" + cmb_item_nm.Text + "','" + cmb_material_nm.Text + "','" + txt_qty.Text + "','" + cmb_unit.Text + "','" + txt_description.Text + "')");
                MessageBox.Show("Record Inserted Successfully", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = db.Displaygrid("select Material_name as [Material Name],material_qty as [Qty],material_unit as [Unit],description from tbl_material_use where Menu_name='" + cmb_item_nm.Text + "' ");
                dataGridView1.Columns[1].Width = 122;
                dataGridView1.Columns[2].Width = 50;
                dataGridView1.Columns[3].Width = 50;
                dataGridView1.Columns[4].Visible = false;
                clear();
            }
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            clear();
        }
        void clear()
        {
            txt_description.Text = "";
            txt_qty.Text = "";
            txt_unit.Text = "";
            cmb_material_nm.Text = "";
            cmb_item_nm.Text = "";
            lblError.Text = "";
            cmb_item_nm.Text = "";
            cmb_material_nm.Text = "";
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      

        private void linkLabelrefresh_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //pageLoad();
            clear();
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
                string itemName = dataGridView1.Rows[i].Cells[1].Value.ToString();

                db.DeleteData("delete from tbl_material_use where Material_name='"+itemName+"' and  Menu_name='"+cmb_item_nm.Text+"'","tbl_material_use");
            }
            dataGridView1.DataSource = db.Displaygrid("select Material_name as [Material Name],material_qty as [Qty],material_unit as [Unit],description from tbl_material_use where Menu_name='" + cmb_item_nm.Text + "' ");
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[1].Width = 122;
            dataGridView1.Columns[2].Width = 50;
            dataGridView1.Columns[3].Width = 50;
        }

       
        private void txt_qty_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);
        }

        private void comboBoxMenuGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (page)
            {
                db.comboFill(cmb_item_nm, "select * from menu where category='" + comboBoxMenuGroup.Text + "'", "menu", "m_name", "m_name");

            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
             }

        private void cmb_item_nm_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            db.comboFill(cmb_material_nm, "select * from material_nm", "material_nm", "material_nm", "material_nm");
            dataGridView1.DataSource = db.Displaygrid("select Material_name as [Material Name],material_qty as [Qty],material_unit as [Unit],description from tbl_material_use where Menu_name='" + cmb_item_nm.Text + "' ");
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[1].Width = 120;
            dataGridView1.Columns[2].Width = 50;
            dataGridView1.Columns[3].Width = 50;
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            Material m = new Material();
            m.ShowDialog();
            db.comboFill(cmb_material_nm, "select * from material_nm", "material_nm", "material_nm", "material_nm");
      
        }

        private void cmb_material_nm_SelectedIndexChanged(object sender, EventArgs e)
        {//select id,material_nm as [Material Name],unit as Unit
            db.comboFill(cmb_unit, "select * from tbl_unit", "tbl_unit", "unit_name", "unit_name");
        }

       

    }
}
