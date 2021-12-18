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
    public partial class Stock_Details : Form
    {
        Database db = new Database();
        public Stock_Details()
        {
            InitializeComponent();
        }

        private void Stock_Details_Load(object sender, EventArgs e)
        {
            this.CancelButton = btn_close;
            db.formFix(this);
            fillcombo();
            fillgrid();
        }
        public void fillgrid()
        {
            if (txt_material_nm.Text == "All")
            {
                dataGridView1.DataSource = db.Displaygrid("select item_name as[Material Name],sum(qty) as Quantity,unit as Unit from tbl_stock where itemType!='drinks' GROUP BY item_name, unit");
                dataGridView1.Columns[0].Width = 220;
                dataGridView1.Columns[1].Width = 95;
                dataGridView1.Columns[2].Width = 70;
                cal();
            }
        }
        public void fillcombo()
        {
            db.comboFill(txt_material_nm, "select distinct item_name from tbl_stock where itemType!='drinks'", "tbl_stock", "item_name", "item_name");
            txt_material_nm.Text = "All";
        }
        private void txt_material_nm_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Displaygrid("SELECT  item_name AS [Material Name], SUM(qty) AS Quantity, unit AS Unit FROM   tbl_stock WHERE (item_name = '" + txt_material_nm.Text + "') GROUP BY item_name, unit");
            dataGridView1.Columns[0].Width = 220;
            dataGridView1.Columns[1].Width = 95;
            dataGridView1.Columns[2].Width = 70;
            cal();
        }
        public void cal()
        {
            double total = 0;
            for (int i = 0; i <= dataGridView1.Rows.Count - 1; i++)
            {

                {
                    total += Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value);
                }

            }
            txt_total.Text = total.ToString();
        }

        

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            fillcombo();
            fillgrid();
        }

        private void txt_material_nm_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

       

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            //dataGridView1.DataSource = db.Displaygrid("select item_name as[Material Name],sum(qty) as Quantity,unit as Unit from tbl_stock where date between '" + dateTimePicker1.Text + "' and '" + dateTimePicker2.Text + "'GROUP BY item_name, unit");
            //dataGridView1.Columns[0].Width = 200;
            //cal();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            //dataGridView1.DataSource = db.Displaygrid("select item_name as[Material Name],sum(qty) as Quantity,unit as Unit from tbl_stock where date between '" + dateTimePicker1.Text + "' and '" + dateTimePicker2.Text + "'GROUP BY item_name, unit");
            //dataGridView1.Columns[0].Width = 200;
            //cal();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //if (checkBox1.Checked)
            //{
            //    dateTimePicker1.Enabled = true;
            //    dateTimePicker2.Enabled = true;
            //}
            //else
            //{
            //    dateTimePicker1.Enabled = false;
            //    dateTimePicker2.Enabled = false;
            //}
        }
    }
}
