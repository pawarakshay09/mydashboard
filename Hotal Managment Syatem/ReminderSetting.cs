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
    public partial class ReminderSetting : Form
    {
        Database db = new Database();
        

        public ReminderSetting()
        {
            InitializeComponent();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();

            try
            {
                if (txt_reminder.Text == "")
                {
                    errorProvider1.SetError(txt_reminder, "Please Enter Reminder.");
                    lblError.Text = "Error - Please Enter Reminder.";
                }
                else
                {

                    if (db.confirm())
                    {
                        db.insert("insert into ReminderSetting values ('" + txt_reminder.Text + "','" + dtpdate.Value.ToString("MM/dd/yyyy") + "') ");
                        MessageBox.Show("Record Inserted Successfully", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    dataGridView1.DataSource = db.Displaygrid("SELECT reminderId,ReminderFor as [Reminder],ReminderDate as[Date] FROM ReminderSetting");
                    dataGridView1.Columns[1].Width = 220;
                    dataGridView1.Columns[0].Visible = false;
                    clear();

                }
            }
            catch (Exception ex)
            { }
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string value_id = db.get_DataGridValue(dataGridView1, "ReminderSetting", "reminderId", 0);
            errorProvider1.Clear();

            try
            {
                if (txt_reminder.Text == "")
                {
                    errorProvider1.SetError(txt_reminder, "Please Enter Reminder.");
                    lblError.Text = "Error - Please Enter Reminder.";
                }
                else
                {
                    DialogResult dlg = MessageBox.Show("Do you really want to Update Record?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlg == DialogResult.Yes)
                    {
                        db.update("update ReminderSetting set ReminderFor='" + txt_reminder.Text + "',ReminderDate='" + dtpdate.Value.ToString("MM/dd/yyyy") + "' where reminderId='" + value_id + "' ");
                        MessageBox.Show("Record Updated Successfully", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    dataGridView1.DataSource = db.Displaygrid("SELECT reminderId,ReminderFor as [Reminder],ReminderDate as[Date] FROM ReminderSetting");
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].Width = 220;
                    clear();
                }

            }
            catch (Exception ex)
            { }
        }

        void clear()
        {
            txt_reminder.Text = "";
           // dtpdate.Text = "";
            lblError.Text = "";
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;

            btn_save.Enabled = true;
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            string value_id = db.get_DataGridValue(dataGridView1, "ReminderSetting", "reminderId", 0);
            db.cnopen();
            SqlCommand cmd = new SqlCommand("select * from ReminderSetting where reminderId='" + value_id + "'", db.cn);
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                txt_reminder.Text = rd["ReminderFor"].ToString();
                dtpdate.Text = rd["ReminderDate"].ToString();
                

            }
            db.cnclose();

            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
            btn_save.Enabled = false;
        }

        private void ReminderSetting_Load(object sender, EventArgs e)
        {
            db.formFix(this);
            this.CancelButton = btn_close;
            dataGridView1.DataSource = db.Displaygrid("SELECT reminderId,ReminderFor as [Reminder],ReminderDate as[Date] FROM ReminderSetting");
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Width = 220;
            btnUpdate.Enabled = false;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            try
            {
                DialogResult dlg = MessageBox.Show("Do you really want to Delete Record?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlg == DialogResult.Yes)
                {

                    string value_id = db.get_DataGridValue(dataGridView1, "ReminderSetting", "reminderId", 0);
                    db.DeleteData("delete from ReminderSetting where reminderId='" + value_id + "'", "ReminderSetting");
                    MessageBox.Show("Record Deleted Successfully", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    dataGridView1.DataSource = db.Displaygrid("SELECT reminderId,ReminderFor as [Reminder],ReminderDate as[Date] FROM ReminderSetting");
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].Width = 220;

                }
                clear();
            }
            catch (Exception ex)
            { }

        }
    }
}
