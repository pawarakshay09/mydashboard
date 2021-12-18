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
    public partial class Company_Info : Form
    {
        Database db = new Database();
        string value_id = "";
        string cname = "";
        public Company_Info()
        {
            InitializeComponent();
        }

        private void Company_Info_Load(object sender, EventArgs e)
        {
            this.CancelButton = btn_close;
            db.formFix(this);
            dataGridView1.DataSource = db.Displaygrid("select companyName as[Hotel Name] from tbl_CompanyInfo ");
            dataGridView1.Columns[0].Width = 198;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (txt_name.Text=="")
            {
                MessageBox.Show("Please Enter Company Name.");
                txt_name.Focus();
            }
            else if(txt_address.Text=="")
            {
                MessageBox.Show("Please Enter Address.");
                txt_address.Focus();
            }
            else if(txt_phone.Text=="")
            {
                MessageBox.Show("Please Enter Phone No.");
                txt_phone.Focus();
            }
            else if (txt_mobile.Text == "")
            {
                MessageBox.Show("Please Enter Mobile No.");
                txt_mobile.Focus();
            }
            else if(txt_email.Text=="")
            {
                 MessageBox.Show("Please Enter Email Id.");
                txt_email.Focus();
            }
            else if (textBox_vatNo.Text == "")
            {
                MessageBox.Show("Please Enter VAT No.");
                textBox_vatNo.Focus();
            }
            else if (textBox_cstNo.Text == "")
            {
                MessageBox.Show("Please Enter CST No.");
                textBox_cstNo.Focus();
            }
            else if (txt_description.Text == "")
            {
                MessageBox.Show("Please Enter Bill Title.");
                txt_description.Focus();
            }
            else
            {
                if (db.confirm())
                {
                    db.insert("insert into tbl_CompanyInfo values('" + txt_name.Text + "','" + txt_address.Text + "','" + txt_phone.Text + "','" + txt_mobile.Text + "','" + txt_email.Text + "','" + textBox_vatNo.Text + "','" + txt_lbt.Text + "','" + textBox_cstNo.Text + "','" + txt_description.Text + "')");
                    db.msgbox();
                    db.reset(this);
                    dataGridView1.DataSource = db.Displaygrid("select companyName as [Hotel Name] from tbl_CompanyInfo ");
                    dataGridView1.Columns[0].Width = 198;
                }
            }
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
           // db.reset(this);
            clear();
            btn_update.Enabled = false;
        }
        public void clear()
        {
            txt_name.Text = "";
            txt_address.Text = "";
            txt_phone.Text = "";
            txt_mobile.Text = "";
            txt_email.Text = "";
            textBox_vatNo.Text = "";
            textBox_cstNo.Text = "";
            txt_lbt.Text = "";
            txt_description.Text = "";
            txt_search.Text = "";
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
             if (txt_name.Text=="")
            {
                //MessageBox.Show("Please Enter Company Name.");
                //txt_name.Focus();
                errorProvider1.SetError(txt_name, "Please Enter Company Name");
                lblError.Text = "Error - Please Enter Company Name";
            }
            else if(txt_address.Text=="")
            {
                //MessageBox.Show("Please Enter Address.");
                //txt_address.Focus();
                errorProvider1.SetError(txt_address, "Please Enter Address");
                lblError.Text = "Error - Please Enter Address";
            }
            //else if(txt_phone.Text=="")
            //{
            //    MessageBox.Show("Please Enter Phone No.");
            //    txt_phone.Focus();
            //}
            else if (txt_mobile.Text == "")
            {
                //MessageBox.Show("Please Enter Mobile No.");
                //txt_mobile.Focus();
                errorProvider1.SetError(txt_mobile, "Please Enter Mobile No");
                lblError.Text = "Error - Please Enter Mobile No";
            }
            else if(txt_email.Text=="")
            {
                // MessageBox.Show("Please Enter Email Id.");
                //txt_email.Focus();
                errorProvider1.SetError(txt_email, "Please Enter Email Id");
                lblError.Text = "Error - Please Enter Email Id";
            }
            else if (textBox_vatNo.Text == "")
            {
                //MessageBox.Show("Please Enter VAT No.");
                //textBox_vatNo.Focus();
                errorProvider1.SetError(txt_email, "Please Enter VAT No");
                lblError.Text = "Error - Please Enter VAT No";
            }
            else if (textBox_cstNo.Text == "")
            {
                //MessageBox.Show("Please Enter CST No.");
                //textBox_cstNo.Focus();
                errorProvider1.SetError(txt_email, "Please Enter CST No");
                lblError.Text = "Error - Please Enter CST No";
            }
             //else if (txt_description.Text == "")
             //{
             //    MessageBox.Show("Please Enter Bill Title.");
             //    txt_description.Focus();
             //}
             //else if (db.ChkDb_Value("select * from tbl_CompanyInfo where companyName='" + txt_name.Text + "'"))
             //{
             //    MessageBox.Show("Record Already Exists!!!!!!");
             //    txt_name.Focus();
             //}
             else
             {

                 if (MessageBox.Show("Do You Really Want to Update Record?", "Confirmation Update", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                 {
                     db.update("update tbl_CompanyInfo  set Address='" + txt_address.Text + "',phone='" + txt_phone.Text + "',Mobile='" + txt_mobile.Text + "',emailId='" + txt_email.Text + "',vatNo='" + textBox_vatNo.Text + "',cstNo='" + textBox_cstNo.Text + "',Description='" + txt_description.Text + "' where companyName ='" + cname + "'");
                     MessageBox.Show("Record Updated Successfully!!!!","Confirm",MessageBoxButtons.OK,MessageBoxIcon.Information);
                 }
                 dataGridView1.DataSource = db.Displaygrid("select companyName as [Hotel Name] from tbl_CompanyInfo");
                 dataGridView1.Columns[0].Width = 198;
                 clear();
                 //db.reset(this);
             }
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                value_id = db.get_DataGridValue(dataGridView1, "tbl_CompanyInfo ", "companyName ", 0);
                db.cnopen();
                SqlCommand cmd = new SqlCommand("select * from tbl_CompanyInfo where companyName ='" + value_id + "'", db.cn);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    txt_name.Text = rd["companyName"].ToString();
                    txt_address.Text = rd["Address"].ToString();
                   
                    txt_phone.Text = rd["Phone"].ToString();
                    txt_mobile.Text = rd["Mobile"].ToString();
                    txt_email.Text = rd["emailId"].ToString();
                     
                    txt_description.Text = rd["Description"].ToString();

                    textBox_vatNo.Text = rd["vatNo"].ToString();
                    textBox_cstNo.Text = rd["cstNo"].ToString(); 
                }
                btn_update.Enabled = true;
                db.cnclose();
                cname = txt_name.Text.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            db.deleteGridRow(dataGridView1, "tbl_CompanyInfo", "companyName", 0);

            dataGridView1.DataSource = db.Displaygrid("select companyName as [Hotel Name] from tbl_CompanyInfo");
            dataGridView1.Columns[0].Width = 198;
            //db.reset(this);
            btn_update.Enabled = false;
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txt_phone_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);
        }

        private void txt_mobile_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_phone_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);
        }

        private void txt_mobile_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);
        }
    }
}
