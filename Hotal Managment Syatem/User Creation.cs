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
    public partial class User_Creation : Form
    {
        Database db = new Database();
        public User_Creation()
        {
            InitializeComponent();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
         
        void bind_dg()
        {
            dataGridView1.DataSource = db.Displaygrid("select user_id,User_Name as [User Name],type as Type from tbl_login ");//where user_name like '" + txt_SearchUserNm.Text + "%" + "'");
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Width = 110;
            dataGridView1.Columns[2].Width = 70;
        }

        private void btn_save_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (txt_usernm.Text == "")
                {
                    errorProvider1.SetError(txt_usernm,"Please Enter User Name");
                    lblError.Text = "Error-Please Enter User Name";
                }
                else if (txt_pwd.Text == "")
                {
                    errorProvider1.SetError(txt_pwd, "Please Enter Password");
                    lblError.Text = "Error-Please Enter Password";
                }
                else
                {
                    if (txt_pwd.Text == txt_con_pwd.Text)
                    {
                        if (db.confirm())
                        {
                           // db.insert("insert into tbl_user values('" + txt_usernm.Text + "','" + txt_pwd.Text + "')");
                            db.insert("insert into tbl_login(User_Name,Password,type) values('" + txt_usernm.Text + "','" + txt_pwd.Text + "','" + txt_type.Text + "')");
                            bind_dg();
                            dataGridView1.Columns[1].Width = 150;

                            //string type=db.getDbstatus_Value("select type from tbl_login");

                            //if (db.ChkDb_Value("select type from tbl_login where type='No_vat'"))
                            //{
                            //    float user_ID = db.getDb_Value("Select max(user_id) from tbl_login where type='No_vat'");
                            //    db.update("update tbl_login set user_Name='" + txt_usernm.Text + "',Password='" + txt_pwd.Text + "' where user_id='" + user_ID + "'");
                               
                            //    bind_dg();
                            //}
                            //else
                            //{
                            //    float user_ID = db.getDb_Value("Select max(user_id) from tbl_login where type='vat'");
                            //    db.update("update tbl_login set user_Name='" + txt_usernm.Text + "',Password='" + txt_pwd.Text + "' where user_id='" + user_ID + "'");
                            //    bind_dg();

                            //}
                           
                        }
                    }
                    else
                        MessageBox.Show("Password Doesn't Match!");
                    db.reset(this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            clear();
        }
        private void btn_delete_Click(object sender, EventArgs e)
        {
            db.deleteGridRow(dataGridView1, "tbl_login", "user_id", 0);
            bind_dg();
          //  dataGridView1.Columns[1].Width = 150;
        }
        private void txt_SearchUserNm_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Displaygrid("select user_id, user_name as Username from tbl_user where user_name like '" + txt_SearchUserNm.Text + "%" + "'");
            dataGridView1.Columns[1].Width = 150;
           // bind_dg();
        }
        private void User_Creation_Load(object sender, EventArgs e)
        {
            this.CancelButton = btn_close;
            db.formFix(this);
            bind_dg();
            //dataGridView1.Columns[1].Width = 150;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            int i = dataGridView1.SelectedCells[0].RowIndex;
            txt_usernm.Text=dataGridView1.Rows[i].Cells[1].Value.ToString();
            txt_type.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            btn_save.Enabled = false;
            btn_delete.Enabled = true;
            btn_update.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            float user_ID = db.getDb_Value("Select max(user_id) from tbl_login where type='" + txt_usernm.Text + "'");
//float custId = db.getDb_Value("select Customer_id from Custmer where name ='" + txt_usernm.Text + "'");

             

            DialogResult dlg = MessageBox.Show("Do you want to update record?","Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (dlg == DialogResult.Yes)
            {
                db.update("update tbl_login set user_Name='" + txt_usernm.Text + "',Password='" + txt_pwd.Text + "',type='"+txt_type.Text+"' where user_id='" + user_ID + "'");
               // db.update("update waiter_dtls set Password='" + txt_pwd.Text + "',work_type='" + txt_type.Text + "' where wname='" + txt_usernm.Text + "'");
              
                bind_dg();
            }
            MessageBox.Show("Record Updated Successfully","Confirm",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            clear();
        }

        public void clear()
        {
            txt_type.Text = "";
            txt_usernm.Text = "";
            txt_pwd.Text = "";
            txt_con_pwd.Text = "";
            btn_save.Enabled = true;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Employeelogin el = new Employeelogin();
            el.ShowDialog();
        }
             
     }
}
