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
    public partial class reservetable : Form
    {
        Database db = new Database();
        public reservetable()
        {
            InitializeComponent();
        }

        private void reservetable_Load(object sender, EventArgs e)
        {
            db.formFix(this);
            bind();
        }

        void bind()
        {
            db.comboFill(cbtablel, "Select t_id from table_status where status='Empty'  ORDER by t_id", "table_status", "t_id", "t_id");
            db.comboFill(cbreserved, "Select t_id from table_status where status='Reserve'   ORDER by t_id", "table_status", "t_id", "t_id");

        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnreserve_Click(object sender, EventArgs e)
        {
            DateTime trDt = Convert.ToDateTime(dateTimePicker1.Text);

            string date = trDt.ToString("MM-dd-yyyy");


            if (txtreason.Text != "")
            {
                if (checkBox1.Checked)
                {
                    db.insert("insert into tbl_reservation(tid,reason,date) values ('" + cbtablel.Text + "','" + txtreason.Text + "','" + date + "')");
                    MessageBox.Show("Reminder Set sucessfully");

                }
                else 
                {
                   
                    db.update("update table_status set status='Reserve' where t_id='" + cbtablel.Text + "' ");
                    MessageBox.Show("Reserved sucessfully");

                    db.insert("insert into tbl_reservation(tid,reason,date) values ('" + cbtablel.Text + "','" + txtreason.Text + "','" + date + "')");
                    bind();
            
                }
            }
            else
            {
                MessageBox.Show("Remark is Required ....!");
            }
        }

        private void btnavailable_Click(object sender, EventArgs e)
        {
            db.update("update table_status set status='Empty' where t_id='" + cbreserved.Text + "' ");
            MessageBox.Show("UNreserved suucessfully");
            db.delete("delete from tbl_reservation where tid='" + cbtablel.Text + "'");
            bind();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                dateTimePicker1.Enabled = true;
            }
            else
            {
                dateTimePicker1.Enabled = false;
            }

        }
    }
}
