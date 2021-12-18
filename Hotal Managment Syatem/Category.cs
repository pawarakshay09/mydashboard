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
    public partial class Category : Form
    {
        Database db = new Database();
        bool flag = false;
        int id; string name;
        public Category()
        {
            InitializeComponent();
        }

        private void Category_Load(object sender, EventArgs e)
        {
            this.CancelButton = btnclose;
            db.formFix(this);  
            cmb_menu.Text ="---Select any one---";
            flag = true;

            bindGrid();
            btn_update.Enabled = false;
            button1.Enabled = false;
            btnadd.Enabled = true;
            db.comboFill(combosection, "select Distinct FoodSection  from category order by 1", "category", "FoodSection", "FoodSection");
            db.comboFill(comboprinterNm, "select distinct grp  from tbl_option where  process_type ='KP' order by 1", "tbl_option", "grp", "grp");
    


        }
       

        private void btnadd_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            ErrorLog errorLog = new ErrorLog();
            if (txt_category.Text == "" || txt_category.Text == " ")
            {
                errorProvider1.SetError(txt_category, "Select Category");
                lblError.Text = "Error - Please Enter Category";
            }
            else if (cmb_type.Text == "" || cmb_type.Text == "--Select--")
            {
                errorProvider1.SetError(cmb_type, "Select Type");
                lblError.Text = "Error - Please Select Type";
            }
            else if (cmb_kotstatus.Text == "")
            {
                errorProvider1.SetError(cmb_kotstatus, "Select Type");
                lblError.Text = "Error - Please Select KOT Status";
            }
            else if (db.ChkDb_Value("select * from category where cat_name='" + txt_category.Text + "'"))
                MessageBox.Show("Record Already Exists!!!!", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                try
                {
                    string qry = "insert into category (menu_id,cat_name,descr,kotprintStatus,type,printerName ,FoodSection ,GST ) values('" + "0" + "','" + txt_category.Text + "','" + txt_desc.Text + "','" + cmb_kotstatus.Text + "','" + cmb_type.Text + "','" + comboprinterNm.SelectedValue.ToString() + "','" + combosection.SelectedValue.ToString() + "','" + combogst .Text + "')";
                    db.InsertData(qry, "category");
                    MessageBox.Show("Record Saved Successfully", "Hotel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    bindGrid();
                    clear();
                }
                catch (Exception ex)
                {
                    // MessageBox.Show(ex.Message);
                    errorLog.WriteErrorLog(ex.ToString());
                }
            }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            clear();
        }
        void clear()
        {
            txt_category.Text = "";
            txt_desc.Text = "";
            cmb_menu.SelectedValue = "";
            cmb_kotstatus.Text = "Yes";
            cmb_type.Text = "--Select--";
            btn_update.Enabled = false;
            button1.Enabled = false;
            btnadd.Enabled = true;
            lblError.Text = "";
            errorProvider1.Clear();
        }
        public void bindGrid()
        {
            dataGridView1.DataSource = db.Displaygrid("SELECT cat_id,cat_name AS [Category],kotprintStatus as Status,type as Type, descr as Description , printerName As PrinterName,FoodSection As Section,GST As GST_Status FROM category ");
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Width = 182;
            dataGridView1.Columns[2].Width = 60;
            dataGridView1.Columns[4].Visible = false;
        }
        private void cmb_menu_SelectedIndexChanged(object sender, EventArgs e)
        {
            string menuId = "0";
            if (flag && cmb_menu.Text!="")
            {
                menuId = db.getDb_Value("select menu_id from Menu_Type where menu_type='"+cmb_menu.Text+"' ").ToString();
                bindGrid();
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {

            id = int.Parse(db.get_DataGridValue(dataGridView1, "category", "cat_id", 0));
            name = db.get_DataGridValue(dataGridView1, "category", "cat_name", 1);
            DialogResult dlg = MessageBox.Show("Do you want to Delete this Record?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlg == DialogResult.Yes)
            {
                DialogResult dialog = MessageBox.Show("If you delete this category,Menu related this category also Deletes..Want To do this?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialog == DialogResult.Yes)
                {
                    db.DeleteData("delete from category where cat_id='" + id + "'", "category");
                    db.DeleteData("delete from menu where category='" + name + "'", "menu");
                    MessageBox.Show("Record Deleted Successfully");
                }
            }

            bindGrid();
            clear();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            id = dataGridView1.SelectedCells[0].RowIndex;
            int value_id=int.Parse(dataGridView1.Rows[id].Cells[0].Value.ToString());
            txt_category.Text = dataGridView1.Rows[id].Cells[1].Value.ToString();
            txt_desc.Text = dataGridView1.Rows[id].Cells[4].Value.ToString();
            cmb_kotstatus.Text = dataGridView1.Rows[id].Cells[2].Value.ToString();
            cmb_type.Text = dataGridView1.Rows[id].Cells[3].Value.ToString();

            combosection.Text = dataGridView1.Rows[id].Cells[5].Value.ToString();
            comboprinterNm.Text = dataGridView1.Rows[id].Cells[6].Value.ToString();
            combogst.Text = dataGridView1.Rows[id].Cells[7].Value.ToString();
            btn_update.Enabled = true;
            button1.Enabled = true;
            btnadd.Enabled = false;
           
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            id = int.Parse(db.get_DataGridValue(dataGridView1, "category", "cat_id", 0));

            if (txt_category.Text == "")
            {
                errorProvider1.SetError(txt_category, "Select Category");
                lblError.Text = "Error - Please Enter Category";
            }
            else if (cmb_type.Text == "")
            {
                errorProvider1.SetError(cmb_type, "Select Type");
                lblError.Text = "Error - Please Select Type";
            }
            else if (cmb_kotstatus.Text == "")
            {
                errorProvider1.SetError(cmb_kotstatus, "Select Type");
                lblError.Text = "Error - Please Select KOT Status";
            }
             else if (db.ChkDb_Value("select * from category where cat_name='" + txt_category.Text + "' and cat_id!='"+id+"'"))
                 MessageBox.Show("Record Already Exists!!!!", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);
             else
             {
                 DialogResult dlg = MessageBox.Show("Do you want to Update this Record?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                 if (dlg == DialogResult.Yes)
                 {
                   //  db.update("Update category set menu_id='0',cat_name='" + txt_category.Text + "',descr='" + txt_desc.Text + "', kotprintStatus='" + cmb_kotstatus.Text + "',type='" + cmb_type.Text + "',printerName='" + comboprinterNm.SelectedValue.ToString() + "' ,FoodSection='" + combosection.SelectedValue.ToString() + "',GST='" + combogst.SelectedValue.ToString ()+ "' where cat_id='" + id + "'");
                   
                     
                     db.update("Update category set menu_id='0' ,cat_name='" + txt_category.Text + "',descr='" + txt_desc.Text + "',kotprintStatus='" + cmb_kotstatus.Text + "',type='" + cmb_type.Text + "',printerName='" + comboprinterNm.Text + "',FoodSection='" + combosection.Text + "',GST='" + combogst.Text + "' where cat_id='" + id + "'");

                     db.update("Update category set menu_id='0' ,cat_name='" + txt_category.Text + "',descr='" + txt_desc.Text + "',kotprintStatus='" + cmb_kotstatus.Text + "',type='" + cmb_type.Text + "',printerName='" + comboprinterNm.Text + "',FoodSection='" + combosection.Text + "',GST='" + combogst.Text + "' where cat_name='" + txt_category.Text + "'");
             
                     MessageBox.Show("Record Updated Successfully");
                 }
                 bindGrid();
                 clear();
             }
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Displaygrid("SELECT cat_id,cat_name AS [Category],kotprintStatus as Status,type as Type,descr as Description FROM category where cat_name like '" + txt_search.Text + "%'  ");
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Width = 182;
            dataGridView1.Columns[2].Width = 60;
            dataGridView1.Columns[4].Visible = false;
        }

        private void cmb_type_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
