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
    public partial class Supplier_Add : Form
    {
        Database db = new Database();
        string value_id = "";
      //  stock_Add stAdd = new stock_Add();

        public bool flg = false;
        
        public Supplier_Add()
        {
            InitializeComponent();
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (textBoxsup_nm.Text == "")
            {
                errorProvider1.SetError(textBoxsup_nm, "Please Enter Supplier Name");
                lblError.Text = "Error - Please Enter Supplier Name.";
            }
            else if (textBoxshop_nm.Text == "")
            {
                errorProvider1.SetError(textBoxshop_nm, "Please Enter Company Name");
                lblError.Text = "Error - Please Enter Company Name.";
            }
            else if (textBox_address.Text == "")
            {
                errorProvider1.SetError(textBox_address, "Please Enter Address");
                lblError.Text = "Error - Please Enter Address.";
            }
            else if (textBox_cnt1.Text == "")
            {
                errorProvider1.SetError(textBox_cnt1, "Please Enter Contact Number");
                lblError.Text = "Error - Please Enter Contact Number.";

            }
            else if (db.ChkDb_Value("select * from supplier_dtls where sup_name='" + textBoxsup_nm.Text + "'"))
                MessageBox.Show("Record Already Exists!!!","Confirm",MessageBoxButtons.OK,MessageBoxIcon.Information);
            else
            {
                if (db.confirm())
                {
                    try
                    {
                        string query = "insert into supplier_dtls(sup_name,shop_name,address,contact_1,contact_2,specialist,remark,trade_imp_lic,vat_no,cst_no,pan_no)Values('" + textBoxsup_nm.Text + "','" + textBoxshop_nm.Text + "','" + textBox_address.Text + "','" + textBox_cnt1.Text + "','" + textBox_cnt2.Text + "','" + textBox_spl.Text + "','" + textBox_remark.Text + "','" + textBox_trd_impNo.Text + "','" + textBox_vatNo.Text + "','" + textBox_cstNo.Text + "','" + textBoxPanNo.Text + "')";
                        db.InsertData(query, "supplier_dtls");
                        MessageBox.Show("Supplier Details Added Sucessfully", "Hotel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        bind_dg();
                        clear();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        public void clear()
        {
            textBoxsup_nm.Text = "";
            textBoxshop_nm.Text = "";
            textBox_address.Text = "";
            textBox_cnt1.Text = "";
            //textBox_cnt2.Text="";
            textBox_spl.Text="";
            textBox_remark.Text="";
            textBox_trd_impNo.Text="0";
            textBox_vatNo.Text="0";
            textBox_cstNo.Text="0";
            textBoxPanNo.Text = "0";
            lblError.Text = "";
            btnUpdate.Enabled = false;
            btn_delete.Enabled = false;
            button1.Enabled = true;
        }
        private void textBox_cnt1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))// && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            ////only allow one decimal point

            //if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            //{
            //    e.Handled = true;
            //}
        }

        private void Supplier_Add_Load(object sender, EventArgs e)
        {
            db.formFix(this);
            this.CancelButton = btn_back;
            bind_dg();
            btnUpdate.Enabled = false;
            btn_delete.Enabled = false;
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {

            clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string supId = db.get_DataGridValue(dataGridView1, "supplier_dtls", "sup_id", 0);
            errorProvider1.Clear();
            if (textBoxsup_nm.Text == "")
            {
                errorProvider1.SetError(textBoxsup_nm, "Please Enter Supplier Name");
                lblError.Text = "Error - Please Enter Supplier Name.";
            }
            else if (textBoxshop_nm.Text == "")
            {
                errorProvider1.SetError(textBoxshop_nm, "Please Enter Company Name");
                lblError.Text = "Error - Please Enter Company Name.";
            }
            else if (textBox_address.Text == "")
            {
                errorProvider1.SetError(textBox_address, "Please Enter Address");
                lblError.Text = "Error - Please Enter Address.";
            }
            else if (textBox_cnt1.Text == "")
            {
                errorProvider1.SetError(textBox_cnt1, "Please Enter Contact Number");
                lblError.Text = "Error - Please Enter Contact Number.";

            }
            else if (db.ChkDb_Value("select * from supplier_dtls where sup_name='" + textBoxsup_nm.Text + "' and sup_id!='" + supId + "'"))
                MessageBox.Show("Record Already Exists!!!", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);

            else
            {
                DialogResult dlg = MessageBox.Show("Do you want to Update this Record", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlg == DialogResult.Yes)
                {
                    string query = "UPDATE supplier_dtls SET  sup_name='" + textBoxsup_nm.Text + "',shop_name='" + textBoxshop_nm.Text + "',address='" + textBox_address.Text + "',contact_1='" + textBox_cnt1.Text + "',contact_2='" + textBox_cnt2.Text + "',specialist='" + textBox_spl.Text + "',remark='" + textBox_remark.Text + "',trade_imp_lic='" + textBox_trd_impNo.Text + "',vat_no='" + textBox_vatNo.Text + "',cst_no='" + textBox_cstNo.Text + "',pan_no='" + textBoxPanNo.Text + "' WHERE sup_id='" + supId + "'";
                    db.UpdateData(query);
                    MessageBox.Show("Record Updated Successfully...!", "Hotel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                bind_dg();
                clear();
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            string value_id = db.get_DataGridValue(dataGridView1, "supplier_dtls", "sup_id", 0);

            DialogResult dlg = MessageBox.Show("Do you want to delete this Record", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlg == DialogResult.Yes)
            {
                db.DeleteData("delete from supplier_dtls where sup_id='" + value_id + "'", "supplier_dtls");
                MessageBox.Show("Record Deleted Successfully");
            }
           // db.comboFill(cmbSupplierName, "SELECT sup_name FROM supplier_dtls", "supplier_dtls", "supplier_dtls", "sup_name");
            bind_dg();
            clear();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            string value_id = db.get_DataGridValue(dataGridView1, "supplier_dtls","sup_id",0);
            db.cnopen();
            SqlCommand cmd = new SqlCommand("select * from supplier_dtls where sup_id='"+value_id+"'",db.cn);
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                textBoxsup_nm.Text=rd["sup_name"].ToString();
                textBoxshop_nm.Text=rd["shop_name"].ToString();
                textBox_address.Text=rd["address"].ToString();
                textBox_cnt1.Text=rd["contact_1"].ToString();
                textBox_cnt2.Text=rd["contact_2"].ToString();
                textBox_spl.Text=rd["specialist"].ToString();
                textBox_remark.Text=rd["remark"].ToString();
                textBox_trd_impNo.Text=rd["trade_imp_lic"].ToString();
                textBox_vatNo.Text = rd["vat_no"].ToString();
                textBox_cstNo.Text=rd["cst_no"].ToString();
                textBoxPanNo.Text=rd["pan_no"].ToString();
            }
            db.cnclose();
            btn_delete.Enabled = true;
            btnUpdate.Enabled = true;
            button1.Enabled = false;
        }
        void bind_dg()
        {
            
            dataGridView1.DataSource = db.Displaygrid("SELECT sup_id, sup_name as [Supplier Name], address as [Address], contact_1 as [Mobile No] FROM  supplier_dtls ");
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Width = 160;
            dataGridView1.Columns[2].Width = 120;
        }

      
        private void textBox_cnt2_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Displaygrid("SELECT sup_id, sup_name as [Supplier Name], address as [Address], contact_1 as [Mobile No] FROM  supplier_dtls where sup_name like '" + txt_search.Text + "%'  ");
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Width = 160;
            dataGridView1.Columns[2].Width = 120;
        }

        
    }
}
