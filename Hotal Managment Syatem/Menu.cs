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
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Hotal_Managment_Syatem
{
    public partial class Menu : Form
    {
        int menu_id=0;
        string d_name;
        int flag = 0;
        bool cmbFlag = false;
       // string constr = @"Data Source=Admin-PC\SQLEXPRESS;Initial Catalog=hotel;Integrated Security=True";
        //string constr = System.Configuration.ConfigurationSettings.AppSettings.Get("con");

        Database db = new Database();

        public Menu()
        {
            InitializeComponent();
        }

        void clr_txt()
        {
            txtMenuName.Text = "";
            textBoxNonAcRate.Text = "0";
            txtRate.Text = "0";
            txt_printName.Text = "0";
            txt_item_code.Text = "0";
            txt_qty.Text = "0";
            txtdrinkGrp.Text = "0";
            txtDriverRate.Text = "0";
            txtShortName.Text = "";
            cmb_drinkGrp.Text = "";
        }                  
        

        // For Binding Data To DataGridView
        void bind()
        {
            if (cmbFlag)
            {
                try
                {

                    dataGridView1.DataSource = db.Displaygrid("SELECT item_code as [Menu Code],m_name as [Menu Name],category as Category,rate as [AC Rate],non_ACrate as [Non AC Rate],insentive_rate as [Insentive Rate],menu_id,driverRate from menu where category='" + cmbCategory.Text + "'");
                    dataGridView1.Columns[0].Width = 70;
                    dataGridView1.Columns[1].Width = 160;
                    dataGridView1.Columns[2].Width = 120;
                    dataGridView1.Columns[3].Width = 80;
                    dataGridView1.Columns[4].Width = 70;
                    dataGridView1.Columns[5].Visible = false;
                    dataGridView1.Columns[7].Visible = false;
                    dataGridView1.Columns[6].Visible = false;
                    btnExport.Enabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    db.cnclose();
                }
            }
        }

       
        

        private void btnAddMenuItem_Click(object sender, EventArgs e)
        {
            ErrorLog errorLog = new ErrorLog();
            //string m_name="";
            try
            {
                errorProvider1.Clear();

                if (cmbCategory.Text == "" || cmbCategory.Text == "--Select--")
                {
                    errorProvider1.SetError(cmbCategory, "Please Select Category");
                    lblError.Text = "Error - Please Select Category.";
                }
                else if (txtMenuName.Text == "")
                {
                    errorProvider1.SetError(txtMenuName, "Please Enter Menu Name");
                    lblError.Text = "Error - Please Enter Menu Name.";
                }
                else if (txtRate.Text == "")
                {
                    errorProvider1.SetError(txtRate, "Please Enter Rate");
                    lblError.Text = "Error - Please Enter Rate.";
                }
                else
                {
                 string OnlyName= RemoveSpecialCharacters(txtMenuName.Text);

                    //int MID = db.GetID("menu_id", "menu");
                    if (txtMenuName.Text.Length <= 11)
                    {
                        txtMenuName.Text += "          ";
                    }
                    if (db.ChkDb_Value("select * from menu where m_name='" + txtMenuName.Text + "'"))
                    {
                        MessageBox.Show("Menu Already Exists!!!", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtMenuName.Focus();
                        bind();
                        clr_txt();
                    }
                    else
                    {
                        // for remove special characters from string menu Name

                        string queryMenu = "INSERT INTO menu (m_name,category,rate,non_ACrate,item_code,insentive_rate,printName,MenuQty,drinkGroup,driverRate) VALUES('" + OnlyName + "','" + cmbCategory.Text + "','" + txtRate.Text + "','" + textBoxNonAcRate.Text + "','" + txt_item_code.Text + "','" + txt_insentive.Text + "','" + txt_printName.Text + "','" + txt_qty.Text + "','" + cmb_drinkGrp.Text + "','" + txtDriverRate.Text + "')";
                            db.InsertData(queryMenu, "menu");

                            //if (cmbCategory.Text == "HARD DRINKS")
                            //{
                            //    if (!db.ChkDb_Value("select grpName from tbl_drinkgroup WHERE grpName='"+txtdrinkGrp.Text+"'"))
                            //        db.insert("insert into tbl_drinkgroup values('" + txtdrinkGrp.Text + "','0')");
                            //}

                            MessageBox.Show("Record Saved Successfully", "Hotel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            bind();
                            clr_txt();
                       
                    }
                }
            }

            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message);
                errorLog.WriteErrorLog(ex.ToString());
            }
        }
        public static string RemoveSpecialCharacters(string input)
        {
            Regex r = new Regex("(?:[^a-z0-9 ]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
            return r.Replace(input, String.Empty);
        }

        private void Menu_Load(object sender, EventArgs e)
        {
           bind();
            //dataGridView1.DataSource = db.Displaygrid("SELECT item_code as Menu_Code,m_name as [Menu Name],category as Category,rate as [AC Rate],non_ACrate as [Non AC Rate],insentive_rate as [Insentive Rate],menu_id,driverRate from menu");
            //dataGridView1.Columns[0].Width = 70;
            //dataGridView1.Columns[1].Width = 250;
            //dataGridView1.Columns[2].Width = 100;
            //dataGridView1.Columns[3].Width = 80;
            //dataGridView1.Columns[4].Width = 80;
            //dataGridView1.Columns[5].Width = 60;
            //dataGridView1.Columns[7].Width = 80;
            //dataGridView1.Columns[6].Visible = false;

            btnExport.Enabled = true;

            this.CancelButton = btn_close;
            this.MaximumSize = this.MinimumSize = this.Size;
            cmbCategory.Text = "";
            cmbCategory.Text = "---Select any one---";

            db.comboFill(cmbCategory, "SELECT cat_name FROM category order by cat_name asc", "category", "cat_name", "cat_name");
            cmbCategory.Text = "--Select--";
            db.comboFill(cmb_drinkGrp, "SELECT * FROM tbl_drinkgroup order by grpName asc", "tbl_drinkgroup", "grpName", "grpName");
            cmb_drinkGrp.Text = "--Select--";
            cmbFlag = true;
            btnAddMenuItem.Enabled = true;
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
        }

        
        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (cmbCategory.Text == "HARD DRINKS" || cmbCategory.Text == "Hard Drinks" )//|| cmbCategory.Text == "Bar" || cmbCategory.Text == "BAR")
            {
                panel1.Visible = true;
            }
            else
                panel1.Visible = false;
            bind();
            txt_search.Text = "";
          
        }

        private void button_back_Click(object sender, EventArgs e)
        {
            clr_txt();
            btnAddMenuItem.Enabled = true;
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
        }

        private void txtRate_TextChanged(object sender, EventArgs e)
        {
           
           
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void cmbCategory_SelectionChangeCommitted(object sender, EventArgs e)
        {

          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

     

        private void txtMenuName_Leave(object sender, EventArgs e)
        {
            txt_printName.Text = txtMenuName.Text;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string id;
            //if (!chkUpdateRate.Checked)
            //{
            id = db.get_DataGridValue(dataGridView1, "menu ", "menu_id ", 6);

                 errorProvider1.Clear();
                if (cmbCategory.Text == "" || cmbCategory.Text == "--Select--")
                {
                    errorProvider1.SetError(cmbCategory, "Please Select Category");
                    lblError.Text = "Error - Please Select Category.";
                }
                else if (txtMenuName.Text == "")
                {
                    errorProvider1.SetError(txtMenuName, "Please Enter Menu Name");
                    lblError.Text = "Error - Please Enter Menu Name.";
                }
                else if (txtRate.Text == "")
                {
                    errorProvider1.SetError(txtRate, "Please Enter Rate");
                    lblError.Text = "Error - Please Enter Rate.";
                }
                else if (db.ChkDb_Value("select * from menu where m_name='" + txtMenuName.Text + "' and  menu_id!='" + id + "'"))
                {
                    MessageBox.Show("Menu Already Exists!!!", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMenuName.Focus();
                }
                else
                {

                    DialogResult dlg = MessageBox.Show("Do you Really want to update Record?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlg == DialogResult.Yes)
                    {
                        db.update("update menu set m_name='" + txtMenuName.Text + "' ,category='" + cmbCategory.Text + "',rate='" + txtRate.Text + "',non_ACrate='" + textBoxNonAcRate.Text + "',item_code='" + txt_item_code.Text + "',insentive_rate='" + txt_insentive.Text + "',printName='" + txt_printName.Text + "',MenuQty='" + txt_qty.Text + "',drinkGroup='" + cmb_drinkGrp.Text + "',driverRate='" + txtDriverRate.Text + "' where menu_id=" + id + "");
                        MessageBox.Show("Record Updated Sucessfully!!!", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    clr_txt();
                    //}
                    //else
                    //{
                    //    float ACRate, nonACRate, insentiveRate, menuQty, driver;
                    //    string m_name, category, itemCode, printName, drinkgrp;
                    //    for (int i = 0; i < dataGridView1.RowCount; i++)
                    //    {
                    //        ACRate = float.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
                    //        nonACRate = float.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());

                    //        id = (dataGridView1.Rows[i].Cells[6].Value.ToString());

                    //        db.update("update menu set rate='" + ACRate + "',non_ACrate='" + nonACRate + "' where menu_id=" + id + "");

                    //    }
                    //}

                    bind();
                }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
           // float menuID = db.GetUniqueId("SELECT menu_id FROM menu WHERE m_name='" + txtMenuName.Text + "' AND category='" + cmbCategory.Text + "'");
            string id = db.get_DataGridValue(dataGridView1, "menu ", "menu_id ", 6);
            if (db.ChkDb_Value("select * from sales_item where menu_id='" + id + "'"))
            {
                MessageBox.Show("This Item is Used in Todays Order.You can Delete this After Day End Only..", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                DialogResult dlg = MessageBox.Show("Do you Really want to Delete Record?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlg == DialogResult.Yes)
                {
                    db.update("delete from menu where menu_id=" + id + "");
                    MessageBox.Show("Record Deleted Sucessfully!!!", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                bind();
                clr_txt();
            }
        }

         

      

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Category _category = new Category();
            _category.ShowDialog();
            db.comboFill(cmbCategory, "SELECT cat_name FROM category order by cat_name asc", "category", "cat_name", "cat_name");

        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Excel Documents (*.xls)|*.xls";
                sfd.FileName = "Menu Export.xls";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                   
                    db.withReportTitle_ToCsV(dataGridView1, sfd.FileName,"Menu Export"); // Here dataGridview1 is your grid view name 
                
                    MessageBox.Show("File Sucessfully Exported"); 
                    Process.Start(sfd.FileName);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txt_item_code_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);
        }

        private void txtRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);

        }

        private void textBoxNonAcRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);

        }

        private void txtDriverRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);

        }

        private void txt_insentive_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);

        }

        private void txt_qty_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Add_Drink_Group drinkgrp = new Add_Drink_Group();
            drinkgrp.ShowDialog();
            db.comboFill(cmb_drinkGrp, "SELECT * FROM tbl_drinkgroup order by grpName asc", "tbl_drinkgroup", "grpName", "grpName");

        }

      

        private void txtRate_Leave(object sender, EventArgs e)
        {
            textBoxNonAcRate.Text = txtRate.Text;
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            string id = db.get_DataGridValue(dataGridView1, "menu ", "menu_id ", 6);
            db.cnopen();
           
            SqlCommand cmd = new SqlCommand("select * from menu where menu_id='" + id + "'", db.cn);
            SqlDataReader rd = cmd.ExecuteReader();
            
            while (rd.Read())
            {
                
                cmbCategory.Text = rd["category"].ToString();
                txtMenuName.Text = rd["m_name"].ToString();
                txt_item_code.Text = rd["item_code"].ToString();
                txtRate.Text = rd["rate"].ToString();
                textBoxNonAcRate.Text = rd["non_ACrate"].ToString();
                txt_insentive.Text = rd["insentive_rate"].ToString();
                cmb_drinkGrp.Text = rd["drinkGroup"].ToString();
                txt_qty.Text = rd["MenuQty"].ToString();
                txt_printName.Text = rd["printName"].ToString();
                txtDriverRate.Text = rd["driverRate"].ToString();
               // txtShortName.Text = rd["shortName"].ToString();
          
            }
            db.cnclose();
            btnAddMenuItem.Enabled = false;
            btnDelete.Enabled = true;
            btnUpdate.Enabled = true;
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Displaygrid("SELECT item_code as [Menu Code],m_name as [Menu Name],category as Category,rate as [AC Rate],non_ACrate as [Non AC Rate],insentive_rate as [Insentive Rate],menu_id,driverRate from menu where m_name like '" + txt_search.Text + "%' and category='" + cmbCategory.Text + "' ");//and category='"+cmbCategory.Text+"'
            dataGridView1.Columns[0].Width = 70;
            dataGridView1.Columns[1].Width = 160;
            dataGridView1.Columns[2].Width = 120;
            dataGridView1.Columns[3].Width = 80;
            dataGridView1.Columns[4].Width = 70;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[6].Visible = false;
     
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            Category _category = new Category();
            _category.ShowDialog();
            db.comboFill(cmbCategory, "SELECT cat_name FROM category order by cat_name asc", "category", "cat_name", "cat_name");

        }

        private void btnaddgrp_Click(object sender, EventArgs e)
        {
            Add_Drink_Group drinkgrp = new Add_Drink_Group();
            drinkgrp.ShowDialog();
            db.comboFill(cmb_drinkGrp, "SELECT * FROM tbl_drinkgroup order by grpName asc", "tbl_drinkgroup", "grpName", "grpName");

        }
    }
}
