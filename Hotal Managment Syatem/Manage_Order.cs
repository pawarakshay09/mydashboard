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
using Microsoft.VisualBasic;

namespace Hotal_Managment_Syatem
{
    public partial class Manage_Order : Form
    {
        Database db = new Database();
        List<int> editedCelss = new List<int>();
        public Manage_Order()
        {
            InitializeComponent();
        }

        private void dtp_from_ValueChanged(object sender, EventArgs e)
        {
            string date = dtp_from.Value.ToString("MM'-'dd'-'yyyy");
            dataGridView1.DataSource = db.Displaygrid("SELECT  order_id as [Bill No], Total_bill as [Amount], datetime as Date FROM   total_bill WHERE (datetime = '"+date +"')");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxSelectAll.Checked == true)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    dataGridView1.Rows[i].Cells[0].Value = true;

                }

            }
            else
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {

                    dataGridView1.Rows[i].Cells[0].Value = false;
                }
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {

            int i = 0;
            List<int> ChkedRow = new List<int>();

            for (i = 0; i <= dataGridView1.RowCount-1; i++)
            {
                if (Convert.ToBoolean(dataGridView1.Rows[i].Cells["chkcol"].Value) == true)
                {
                    ChkedRow.Add(int.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString()));
                }
            }

            DialogResult dlg = MessageBox.Show("Do you Really want to delete this record?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlg == DialogResult.Yes)
            {
                string date = dtp_from.Value.ToString("MM'-'dd'-'yyyy");

                foreach (int k in ChkedRow)
                {

                   //db.DeleteData("delete from total_bill where order_id='" + k + "'", "total_bill"); //table_order

                    db.DeleteData("delete from table_order where order_id='" + k + "'", "table_order");
                    db.DeleteData("delete from total_bill where order_id='" + k + "'", "total_bill");
                    MessageBox.Show("Bill Deleted Successfully","Confirm",MessageBoxButtons.OK,MessageBoxIcon.Information);

                }
                dataGridView1.DataSource = db.Displaygrid("SELECT  order_id as [Bill No], Total_bill as Amount, datetime as Date FROM   total_bill WHERE (datetime = '" + date + "')");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Manage_Order_Load(object sender, EventArgs e)
        {
            db.formFix(this);
            this.CancelButton = btn_close;
        }
    }
}
