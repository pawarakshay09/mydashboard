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
    public partial class AddCustmer : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        public string cust_id,order_id,d_date,d_time;
        string dob;
        Database db = new Database();
        string custId;
        bool flagparcelForm = false;
        public AddCustmer()
        {
            InitializeComponent();
        }
        
        public AddCustmer(string o_id,string date,string time) //is active whn form open from welcome page 
        {
            InitializeComponent();
             flagparcelForm = true;
             this.order_id = o_id;// to chk 
             this.d_date = date;
             this.d_time = time;
        }

        public void cleartxt()
        {
            txt_address.Text = "";
            txt_description.Text = "";
            txt_location.Text = "";
            txt_name.Text = "";
            txt_phone.Text = "";
            txt_search.Text = "";
            txtCardNo.Text = "";
            cmbMarritalStatus.Text = "--Select--";
            btn_save.Enabled = true;
            txt_search.Enabled = true;
            btn_delete.Enabled = false;
            buttonUpdate.Enabled = false;

        }
        private void btn_clear_Click(object sender, EventArgs e)
        {
            cleartxt();
         
            txt_name.Focus();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (txt_name.Text == "")
            {
                //MessageBox.Show("Please Enter Customer Name");
                errorProvider1.SetError(txt_name, "Please Enter Customer Name");
                lblError.Text = "Error - Please Enter Customer Name.";
                txt_name.Focus();
            }
            else if (txt_address.Text == "")
            {

                //MessageBox.Show("Please Enter Address");
                errorProvider1.SetError(txt_address, "Please Enter Address ");
                lblError.Text = "Error - Please Enter Address.";
                txt_address.Focus();
            }
            //else if (txt_location.Text == "")
            //{
            //    MessageBox.Show("Please Enter Location");
            //    txt_address.Focus();
            //}
            else if (txt_phone.Text == "")
            {
               // MessageBox.Show("Please Enter Mobile No.");
                errorProvider1.SetError(txt_phone, "Please Enter Mobile No ");
                lblError.Text = "Error - Please Enter Mobile No.";
                txt_address.Focus();
            }
            else if(cmbMarritalStatus.Text=="")
            {
                //MessageBox.Show("Please Enter Marrital Status");
                errorProvider1.SetError(cmbMarritalStatus, "Please Enter Marrital Status.");
                lblError.Text = "Error - Please Enter Marrital Status.";
                cmbMarritalStatus.Focus();
            }
            else if (db.ChkDb_Value("select * from Custmer where name='" + txt_name.Text + "'"))
            {
                MessageBox.Show("Record Already Exists!!!","Confirm",MessageBoxButtons.OK,MessageBoxIcon.Information);

            }
            else
            {
                try
                {
                    string dob = dtp_dob.Value.ToString("MM-dd-yyyy");
                    string anidt = dtpAniversary.Value.ToString("MM-dd-yyyy");
                    db.insert("insert into Custmer(name,addr,location,phone,remark,dateOfBirth,cardNo,status,aniversaryDate)values('" + txt_name.Text.Trim() + "','" + txt_address.Text.Trim() + "','" + txt_location.Text.Trim() + "','" + txt_phone.Text.Trim() + "','" + txt_description.Text.Trim() + "','" + dob + "','" + txtCardNo.Text + "','" + cmbMarritalStatus.Text + "','" + anidt + "')");
                    MessageBox.Show("Record Added succesfully");
                    cleartxt();
                    //dataGridView1.DataSource = db.Displaygrid("select Customer_id as[Customer ID], name as Name ,addr as Address,location as Location,phone as Phone,remark as Remark,cardNo as [Card No] from Custmer");
                    bind_dgv();
                }
                catch(Exception ex)
                {
                   // MessageBox.Show("");
                }
            }
        }

        private void AddCustmer_Load(object sender, EventArgs e)
        {
            this.CancelButton = btn_close;
            db.formFix(this);
            db.comboFill(cmb_waiter, "select * from waiter_dtls", "waiter_dtls", "wname", "wname");
            txt_name.Focus();
            bind_dgv();
           
        }
        void bind_dgv()
        {
            dataGridView1.DataSource = db.Displaygrid("select Customer_id as [Customer ID],name as Name ,addr as Address,location as Location,phone as Phone,remark as Remark ,cardNo as [Card No] from Custmer");

            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            // dataGridView1.Columns[4].Width = 150;
            dataGridView1.Columns[1].Width = 140;
            dataGridView1.Columns[4].Width = 100;
            
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            DialogResult dlg = MessageBox.Show("Do you really want to Delete this record?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlg == DialogResult.Yes)
            {

                try
                {

                    custId = db.getDb_Value("select Customer_id from Custmer where name ='" + txt_name.Text + "'").ToString();
                    
                    db.DeleteData("delete from Custmer where Customer_id='" + Convert.ToInt32(custId) + "'", "custmer");
                    dataGridView1.DataSource = db.Displaygrid("select Customer_id as [Customer ID],name as Name ,addr as Address,location as Location,phone as Phone,remark as Remark,cardNo as [Card No] from Custmer");
                    dataGridView1.Columns[0].Visible = false;
                    cleartxt();
                    btn_save.Enabled = true;
                    MessageBox.Show("Record Deleted successfully");
                    txt_name.Focus();

                }
                catch (Exception ex)
                {
                    ex.ToString();

                }
                finally
                {

                    db.cnclose();
                    
                }
            }
            cleartxt();
        }


        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            string custId = db.get_DataGridValue(dataGridView1, "Custmer", "Customer_id", 0);

            errorProvider1.Clear();
            if (txt_name.Text == "")
            {
                  //MessageBox.Show("Please Enter Customer Name");
                errorProvider1.SetError(txt_name, "Please Enter Customer Name");
                lblError.Text = "Error - Please Enter Customer Name.";
                txt_name.Focus();
            }
            else if (txt_address.Text == "")
            {

                //MessageBox.Show("Please Enter Address");
                errorProvider1.SetError(txt_address, "Please Enter Address ");
                lblError.Text = "Error - Please Enter Address.";
                txt_address.Focus();
            }
            //else if (txt_location.Text == "")
            //{
            //    MessageBox.Show("Please Enter Location");
            //    txt_address.Focus();
            //}
            else if (txt_phone.Text == "")
            {
               // MessageBox.Show("Please Enter Mobile No.");
                errorProvider1.SetError(txt_phone, "Please Enter Mobile No ");
                lblError.Text = "Error - Please Enter Mobile No.";
                txt_address.Focus();
            }
            else if(cmbMarritalStatus.Text=="")
            {
                //MessageBox.Show("Please Enter Marrital Status");
                errorProvider1.SetError(cmbMarritalStatus, "Please Enter Marrital Status.");
                lblError.Text = "Error - Please Enter Marrital Status.";
                cmbMarritalStatus.Focus();
            }
             else if (db.ChkDb_Value("select * from Custmer where name='" + txt_name.Text + "' and Customer_id!='" + custId + "'"))
             {
                 MessageBox.Show("Record Already Exists!!!", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);

             }
             else
             {
                 DialogResult dlg = MessageBox.Show("Do you really want to update this record?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                 if (dlg == DialogResult.Yes)
                 {
                     try
                     {
                         string dob = dtp_dob.Value.ToString("MM-dd-yyyy");
                         db.cnopen();
                         db.update("update Custmer set name='" + txt_name.Text + "',addr='" + txt_address.Text + "',location='" + txt_location.Text + "',phone='" + txt_phone.Text + "',remark='" + txt_description.Text + "' ,dateOfBirth='" + dob + "',cardNo='" + txtCardNo.Text + "',status='" + cmbMarritalStatus.Text + "',aniversaryDate='" + dtpAniversary.Value.ToString("MM/dd/yyyy") + "' where Customer_id='" + Convert.ToInt32(custId) + "'");
                         MessageBox.Show("Record Is Updated Successfully");
                         cleartxt();
                         db.cnclose();
                         dataGridView1.DataSource = db.Displaygrid("select Customer_id as[Customer ID], name as Name ,addr as Address,location as Location,phone as Phone,remark as Remark,cardNo as [Card No] from Custmer");
                         dataGridView1.Columns[0].Visible = false;
                     }
                     catch (Exception ex)
                     {
                         ex.ToString();
                     }
                 }
             }

        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            if (txt_search.Text != "")
            {
                dataGridView1.DataSource = db.Displaygrid("select Customer_id as [Customer ID],name as Name ,addr as Address,location as Location,phone as Phone,remark as Remark ,cardNo as [Card No] from Custmer where name like '" + txt_search.Text + "%'");

            }
            else
                dataGridView1.DataSource = db.Displaygrid("select Customer_id as [Customer ID],name as Name ,addr as Address,location as Location,phone as Phone,remark as Remark ,cardNo as [Card No] from Custmer");

            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            // dataGridView1.Columns[4].Width = 150;
            dataGridView1.Columns[1].Width = 140;
            dataGridView1.Columns[4].Width = 100;
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {

            buttonUpdate.Enabled = true;
            btn_delete.Enabled = true;
            btn_save.Enabled = false;

          

            string custId=db.get_DataGridValue(dataGridView1,"Custmer","Customer_id",0);

            db.cnopen();
            SqlCommand cmd = new SqlCommand("select * from Custmer where Customer_id='" + custId + "'", db.cn);
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                txt_name.Text = rd["name"].ToString();
                txt_address.Text = rd["addr"].ToString();
                txt_phone.Text = rd["phone"].ToString();
                txt_location.Text = rd["location"].ToString();
                txt_description.Text = rd["remark"].ToString();
                dtp_dob.Text = Convert.ToDateTime(rd["dateOfBirth"]).ToString("dd/MM/yyyy");
               
                cmbMarritalStatus.Text = rd["status"].ToString();
                dtpAniversary.Text = Convert.ToDateTime(rd["aniversaryDate"]).ToString("dd/MM/yyyy");
                txtCardNo.Text = rd["cardNo"].ToString();
            }

            db.cnopen();

            if (txt_name.Text != "")
            {
                float cust_id = db.getDb_Value("select Customer_id from Custmer where name='" + txt_name.Text + "'");
                if (db.ChkDb_Value("select * from tbl_parcel_order where Customer_id='" + cust_id + "'"))
                {
                    string wname = db.getDbstatus_Value("select w_name from tbl_parcel_order where Customer_id='" + cust_id + "'");

                    cmb_waiter.Text = wname;

                }
                else
                {
                    db.comboFill(cmb_waiter, "select wname from waiter_dtls", "waiter_dtls", "wname", "wname");
                }
            }
        }

 
        private void button1_Click(object sender, EventArgs e)
        {
            float cust_id;
            if (db.ChkDb_Value("select * from Custmer where name='" + txt_name.Text + "' "))
            {
                cust_id = db.getDb_Value("select Customer_id from Custmer where name='" + txt_name.Text + "'");
                DateTime dt = Convert.ToDateTime(d_date);
                string date = Convert.ToDateTime(dt).ToString("MM-dd-yyyy");
               // db.insert("insert into tbl_parcel_order values('" + order_id + "','" + cust_id + "','Deliver','" + cmb_waiter.Text + "','" + date + "','" + d_time + "')");
                db.insert("insert into tbl_parcel_order(order_id,Customer_id,order_status,w_name,date,order_time) values('" + order_id + "','" + cust_id + "','Deliver','" + cmb_waiter.Text + "','" + date + "','" + d_time + "')");
   
                this.Close();
                //Welcome wel = new Welcome(cust_id);
                //wel.ShowDialog();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                db.comboFill(cmb_waiter, "select wname from waiter_dtls", "waiter_dtls", "wname", "wname");

            }
            else
            {
                float cust_id = db.getDb_Value("select Customer_id from Custmer where name='" + txt_name.Text + "'");

                string wname = db.getDbstatus_Value("select w_name from tbl_parcel_order where Customer_id='" + cust_id + "'");

                cmb_waiter.Text = wname;
            }
        }

        private void cmbMarritalStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMarritalStatus.Text == "Unmarried")
                dtpAniversary.Enabled = false;
            else
                dtpAniversary.Enabled = true;
        }

        private void txt_phone_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);
        }

     
    }
}
