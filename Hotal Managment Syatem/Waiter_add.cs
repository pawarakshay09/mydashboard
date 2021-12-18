using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace Hotal_Managment_Syatem
{
    public partial class Waiter_add : Form
    {
        Database db = new Database();
        string newFileName = "";
        string filename;
        bool flag = false;

        public Waiter_add()
        {
            InitializeComponent();
        }               

          private void btnBack_Click(object sender, EventArgs e)
          {
              this.Close();
          }

          private void btnAdd_Click(object sender, EventArgs e)
          {

              string path = "";
              if (!System.IO.Directory.Exists(path))
                  System.IO.Directory.CreateDirectory("D:\\EmpImages\\");
              
             
              
              try
              {
                  errorProvider1.Clear();
                  if (txtWaiterName.Text == "")
                  {
                      errorProvider1.SetError(txtWaiterName,"Please Enter Waiter Name");
                      lblError.Text = "Error - Please Enter Employee Name.";
                  }
                  else if(txtAddress.Text=="")
                  {
                      errorProvider1.SetError(txtAddress, "Please Enter Address");
                      lblError.Text = "Error - Please Enter Address.";
                  }
                  else if (txtMobileNo.Text == "")
                  {
                      errorProvider1.SetError(txtMobileNo, "Please Enter Mobile Number");
                      lblError.Text = "Error - Please Enter Mobile Number.";
                  }
                  else if (cmb_designation.Text == "" || cmb_designation.Text =="--Select--")
                  {
                      errorProvider1.SetError(cmb_designation, "Please Select Designation");
                      lblError.Text = "Error - Please Select Designation.";
                  }
                  else if (txtSalary.Text == "")
                  {
                      errorProvider1.SetError(txtSalary, "Please Enter Salary");
                      lblError.Text = "Error - Please Enter Salary.";
                  }
                  else if(db.ChkDb_Value("select * from waiter_dtls where wname='"+txtWaiterName.Text+"'"))
                      MessageBox.Show("Record Already Exist", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                  else
                  {
                      string query = "INSERT INTO waiter_dtls(wname,waddress,mob_no,work_type,salary,remark,img_path) VALUES('" + txtWaiterName.Text + "','" + txtAddress.Text + "','" + txtMobileNo.Text + "','" + cmb_designation.Text + "','" + txtSalary.Text + "','" + txtRemark.Text + "','" + newFileName + "')";
                        db.InsertData(query, "waiter_dtls");
                        if (newFileName != "")
                        {
                            System.IO.File.Copy(filename, newFileName, true); // save imag into our emp table 
                        }


                      string wid=db.getDbstatus_Value("select max(w_id) from waiter_dtls ");
                      db.insert(" insert into tbl_waiterAssignSection(w_id,wName) values('" + wid + "','" + txtWaiterName.Text + "') ");

        
                        clear();
                      MessageBox.Show("Saved Successfully...!", "RestroSoft", MessageBoxButtons.OK, MessageBoxIcon.Information);
                      bindGrid();
                  }
              }
              catch (Exception ex)
              {
                  MessageBox.Show(ex.Message);
              }
          }

          public void bindGrid()
          {
              dataGridView1.DataSource = db.Displaygrid("SELECT w_id,wname as [Employee Name],work_type as [Designation],waddress as Address,mob_no as [Mobile No],salary as Salary FROM waiter_dtls");
              dataGridView1.Columns[0].Visible = false;
              dataGridView1.Columns[1].Width = 120;
              dataGridView1.Columns[2].Width = 90;
              dataGridView1.Columns[3].Width = 80;
              dataGridView1.Columns[4].Width = 80;
              dataGridView1.Columns[5].Width = 65;

          }
          public void clear()
          {
             
              txtWaiterName.Text = "";
              txtAddress.Text = "";
              txtMobileNo.Text = "";
              txtRemark.Text = "";
              txtSalary.Text = "";

              cmb_designation.Text = "--Select--";
              //  pictureBox1.Image = null;
              btn_delete.Enabled = false;
              btnAdd.Enabled = true;
              btn_waiterAdd.Enabled = false;
              Image initialImg = pictureBox1.Image;
          }
          private void txtMobileNo_KeyPress(object sender, KeyPressEventArgs e)
          {            
              if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '+')
              {
                  e.Handled = true;
              }
           }

          private void txtSalary_KeyPress(object sender, KeyPressEventArgs e)
          {
              if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
              {
                  e.Handled = true;
              }

              //only allow one decimal point

              if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
              {
                  e.Handled = true;
              }
          }

          private void button1_Click(object sender, EventArgs e)
          {
              clear();
          }

          private void Waiter_add_Load(object sender, EventArgs e)
          {
              Image initialImg = pictureBox1.Image;
              this.CancelButton = btnBack;
              db.formFix(this);
              db.comboFill(cmb_designation, "select * from tbl_designation", "tbl_designation", "design_name","design_name");
              cmb_designation.Text = "--Select--";
              //dataGridView1.DataSource = db.Displaygrid("SELECT  w_id,wname as [Waiter Name],work_type as [Work type],waddress as Address,mob_no as [Mobile Number],salary as Salary FROM waiter_dtls");
              //dataGridView1.Columns[0].Visible = false;
              bindGrid();
              flag = true;
              btn_delete.Enabled = false;
              btnAdd.Enabled = true;
              btn_waiterAdd.Enabled = false;
          }

          private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
          {

          }

          private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
          {
              //detination filename
               newFileName = @"D:\\EmpImages\\" + txtWaiterName.Text+ ".jpg";
              newFileName=txt_path.Text + txtWaiterName.Text+".jpg";
              // if the user presses OK instead of Cancel
               openFileDialog1.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

              if (openFileDialog1.ShowDialog() == DialogResult.OK)
              {
                  //get the selected filename
                   filename = openFileDialog1.FileName;
               
                   Image img = new Bitmap(openFileDialog1.FileName);
                  pictureBox1.Image = img.GetThumbnailImage(126, 135, null, new IntPtr());
                  
                 
              }
          }

          private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
          {
                       }

          private void groupBox1_Enter(object sender, EventArgs e)
          {

          }

        
          private void btn_waiterAdd_Click(object sender, EventArgs e)
          {
              //int ID = db.GetUniqueId("SELECT w_id FROM waiter_dtls WHERE wname='" + txtWaiterName.Text + "'");
              string ID = db.get_DataGridValue(dataGridView1, "waiter_dtls ", "w_id ", 0);

              errorProvider1.Clear();
                  if (txtWaiterName.Text == "")
                  {
                      errorProvider1.SetError(txtWaiterName, "Please Enter Waiter Name");
                      lblError.Text = "Error - Please Enter Employee Name.";
                  }
                  else if (txtAddress.Text == "")
                  {
                      errorProvider1.SetError(txtAddress, "Please Enter Address");
                      lblError.Text = "Error - Please Enter Address.";
                  }
                  else if (txtMobileNo.Text == "")
                  {
                      errorProvider1.SetError(txtMobileNo, "Please Enter Mobile Number");
                      lblError.Text = "Error - Please Enter Mobile Number.";
                  }
                  else if (cmb_designation.Text == "" || cmb_designation.Text == "--Select--")
                  {
                      errorProvider1.SetError(cmb_designation, "Please Select Designation");
                      lblError.Text = "Error - Please Select Designation.";
                  }
                  else if (txtSalary.Text == "")
                  {
                      errorProvider1.SetError(txtSalary, "Please Enter Salary");
                      lblError.Text = "Error - Please Enter Salary.";
                  }
                  else if (db.ChkDb_Value("select * from waiter_dtls where wname='" + txtWaiterName.Text + "' and w_id!='"+ID+"'"))
                      MessageBox.Show("Record Already Exist", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                  else
                  {
                      DialogResult dlg = MessageBox.Show("Do you want to update this Record", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                      if (dlg == DialogResult.Yes)
                      {

                          string query = "UPDATE waiter_dtls SET waddress='" + txtAddress.Text + "', mob_no='" + txtMobileNo.Text + "', work_type='" + cmb_designation.Text + "', salary='" + txtSalary.Text + "', remark='" + txtRemark.Text + "' WHERE w_id=" + ID + "";
                          db.UpdateData(query);
                          MessageBox.Show("Record Updated Successfully...!", "Hotel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                          //dataGridView1.DataSource = db.Displaygrid("SELECT  w_id,wname as [Waiter Name],work_type as [Work type],waddress as Address,mob_no as [Mobile Number],salary as Salary FROM waiter_dtls");
                          //dataGridView1.Columns[0].Visible = false;
                          bindGrid();
                          clear();
                      }
                  }
          }

          private void btn_delete_Click(object sender, EventArgs e)
          {
              DialogResult dlg = MessageBox.Show("Do you want to delete this Record", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
              if (dlg == DialogResult.Yes)
              {
                  int ID = db.GetUniqueId("SELECT w_id FROM waiter_dtls WHERE wname='" + txtWaiterName.Text + "'");

                  string query = "Delete from waiter_dtls WHERE w_id=" + ID + "";
                  db.DeleteData(query, "waiter_dtls");
                  MessageBox.Show("Record Deleted Successfully...!", "Hotel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                  //dataGridView1.DataSource = db.Displaygrid("SELECT  w_id,wname as [Waiter Name],work_type as [Work type],waddress as Address,mob_no as [Mobile Number],salary as Salary FROM waiter_dtls");
                  //dataGridView1.Columns[0].Visible = false;
                  bindGrid();

              }
              clear();
          }

          private void txt_search_TextChanged(object sender, EventArgs e)
          {

              dataGridView1.DataSource = db.Displaygrid("SELECT  w_id,wname as [Employee Name],work_type as [Designation],waddress as Address,mob_no as [Mobile No],salary as Salary FROM waiter_dtls where wname like '" + txt_search.Text + "%'");
              dataGridView1.Columns[0].Visible = false;
              dataGridView1.Columns[1].Width = 120;
              dataGridView1.Columns[2].Width = 80;
              dataGridView1.Columns[3].Width = 80;
              dataGridView1.Columns[4].Width = 80;
              dataGridView1.Columns[5].Width = 65;

          }

          private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
          {
              if (flag)
              {
                  string value_id = db.get_DataGridValue(dataGridView1, "waiter_dtls ", "w_id ", 0);
                  db.cnopen();
                  SqlCommand cmd = new SqlCommand("select * from waiter_dtls where w_id='" + value_id + "'", db.cn);
                  SqlDataReader rd = cmd.ExecuteReader();
                  while (rd.Read())
                  {
                      txtWaiterName.Text = rd["wname"].ToString();
                      txtAddress.Text = rd["waddress"].ToString();
                      txtMobileNo.Text = rd["mob_no"].ToString();
                      cmb_designation.Text = rd["work_type"].ToString();
                      txtSalary.Text = rd["salary"].ToString();
                      txtRemark.Text = rd["remark"].ToString();
                  }
                  db.cnclose();

                  btn_delete.Enabled = true;
                  btnAdd.Enabled = false;
                  btn_waiterAdd.Enabled = true;
                  //img hide by sagar bcoz clint not reuired

                  string imgPath = db.getDbstatus_Value("select img_path from waiter_dtls where wname='" + txtWaiterName.Text + "'").ToString();
                  if (File.Exists(imgPath.ToString()))
                  {
                      Image img = new Bitmap(db.getDbstatus_Value("select img_path from waiter_dtls where wname='" + txtWaiterName.Text + "'"));

                      pictureBox1.Image = img.GetThumbnailImage(126, 135, null, new IntPtr());
                  }

                  else
                  {
                      MessageBox.Show("Image Could not Found");
                      pictureBox1.Image = null;
                  }

              }
          }

          private void btnAddNew_Click(object sender, EventArgs e)
          {
              Add_Designation design = new Add_Designation();
              design.ShowDialog();
              db.comboFill(cmb_designation, "select * from tbl_designation", "tbl_designation", "design_name", "design_name");

          }

          private void btnImgadd_Click(object sender, EventArgs e)
          {
              //detination filename
              newFileName = @"D:\\EmpImages\\" + txtWaiterName.Text + ".jpg";
              newFileName = txt_path.Text + txtWaiterName.Text + ".jpg";
              // if the user presses OK instead of Cancel
              openFileDialog1.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

              if (openFileDialog1.ShowDialog() == DialogResult.OK)
              {
                  //get the selected filename
                  filename = openFileDialog1.FileName;

                  Image img = new Bitmap(openFileDialog1.FileName);
                  pictureBox1.Image = img.GetThumbnailImage(126, 135, null, new IntPtr());


              }
          }

          private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
          {

          }

        
       
    }
}
