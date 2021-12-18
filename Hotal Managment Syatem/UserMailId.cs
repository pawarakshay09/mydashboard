using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Hotal_Managment_Syatem
{
    public partial class UserMailId : Form
    {
        Database db = new Database();
        public UserMailId()
        {
            InitializeComponent();
        }

        private void UserMailId_Load(object sender, EventArgs e)
        {
            db.formFix(this);
            this.CancelButton = btn_close;
            bind();
        }
        void bind()
        {
            dataGridView1.DataSource = db.Displaygrid("select user_id,emailId as [Email Id] from UserEmailID");
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Width = 210;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
             Regex reg = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            errorProvider1.Clear();
            if (txt_mailId.Text == "")
            {
                errorProvider1.SetError(txt_mailId, "Please Enter E Mail Id.");
                lblError.Text = "Error - Please Enter E Mail Id.";
                txt_mailId.Focus();
            }
            else
            {
                if (!reg.IsMatch(txt_mailId.Text))
                {
                    errorProvider1.SetError(txt_mailId, "Please Enter Valid E Mail Id.");
                    lblError.Text = "Error - Please Enter Valid E Mail Id.";
                    txt_mailId.Focus();
                }
                else
                {
                    if (db.ChkDb_Value("select * from UserEmailID where emailId='" + txt_mailId.Text + "'"))
                        MessageBox.Show("Record Already Exists!!", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                    {
                        if (db.confirm())
                        {
                            db.insert("insert into UserEmailID values('" + txt_mailId.Text + "')");
                            MessageBox.Show("Record Inserted Successfully!!!", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            clear();
                            bind();
                        }
                        lblError.Text = "";
                    }
                }
            }
        }

        void clear()
        {
            txt_mailId.Text = "";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            float id = float.Parse(db.get_DataGridValue(dataGridView1, "UserEmailID", "user_id", 0).ToString());
            DialogResult dlg = MessageBox.Show("Do You Really want to Delete record?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlg == DialogResult.Yes)
            {
                db.DeleteData("delete from UserEmailID where user_id='" + id + "'", "UserEmailID");
                MessageBox.Show("Record Deleted Successfully!!!");

            }
            clear();
            bind();
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            btnDelete.Enabled = true;
            btn_save.Enabled = false;
        }
    }
}
