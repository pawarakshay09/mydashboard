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
    public partial class Employeelogin : Form
    {
        Database db = new Database();
        public Employeelogin()
        {
            InitializeComponent();
        }

        private void Employeelogin_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Displaygrid("Select w_id, wname as Name,work_type as Type,password from waiter_dtls");
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            int i = dataGridView1.SelectedCells[0].RowIndex;
            txt_usernm.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
           
            btn_save.Enabled = false;
            btn_delete.Enabled = true;
            btn_update.Enabled = true;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (txt_pwd.Text != "" && txt_con_pwd.Text != "")
            {
                if (txt_pwd.Text == txt_con_pwd.Text)
                {

                    db.insert("Update waiter_dtls set password='" + txt_con_pwd.Text + "' where wname='"+txt_usernm.Text+"'");

                }
                else
                {
                    MessageBox.Show("Confirm Password Not match Plz ReEnter....!");
                }
            }
            else
            {
                MessageBox.Show("Enter Password ");
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            if (txt_pwd.Text != "" && txt_con_pwd.Text != "")
            {
                if (txt_pwd.Text == txt_con_pwd.Text)
                {

                    db.insert("Update waiter_dtls set password='" + txt_con_pwd.Text + "' where wname='" + txt_usernm.Text + "'");
                    MessageBox.Show("Updated Sucessfully....!");

                }
                else
                {
                    MessageBox.Show("Confirm Password Not match Plz ReEnter....!");
                }
            }
            else
            {
                MessageBox.Show("Enter Password ");
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            db.delete("delete from waiter_dtls where wname='" + txt_usernm .Text+ "'");
            dataGridView1.DataSource = db.Displaygrid("Select w_id, wname as Name,work_type as Type,password from waiter_dtls");
   
        }
    }
}
