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
    public partial class warehousestock : Form
    {

        Database db = new Database();

        public warehousestock()
        {
            InitializeComponent();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void warehousestock_Load(object sender, EventArgs e)
        {
            this.CancelButton = btn_close;
            db.formFix(this);
            fillcombo();
            fillgrid();

        }
        public void fillcombo()
        {
            db.comboFill(txt_material_nm, "select distinct item_name from tbl_Warehouse ", "tbl_Warehouse", "item_name", "item_name");
            txt_material_nm.Text = "All";
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
        public void fillgrid()
        {
            if (txt_material_nm.Text == "All")
            {
                //dataGridView1.DataSource = db.Displaygrid("select item_name as[Material Name],sum(qty) as Quantity,unit as Unit from tbl_Warehouse where itemType!='drinks' GROUP BY item_name, unit");
                //  dataGridView1.DataSource = db.Displaygrid("select w.item_name as [Product Name], w.qty- ISNULL (s.qty,0)  as [Qty],w.unit as [Unit ]from tbl_warehouse w left join tbl_stock s on w.item_name =s.item_name  ");
                dataGridView1.DataSource = db.Displaygrid("select item_name as [Product Name], qty as [Qty] ,unit as [Unit ] from tbl_warehouse   ");


                dataGridView1.Columns[0].Width = 220;
                dataGridView1.Columns[1].Width = 95;
                dataGridView1.Columns[2].Width = 70;
                cal();
            }
        }

        private void txt_material_nm_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Displaygrid("select item_name as [Product Name], qty as [Qty] ,unit as [Unit ] from tbl_warehouse  WHERE item_name = '" + txt_material_nm.Text + "' ");


            dataGridView1.Columns[0].Width = 220;
            dataGridView1.Columns[1].Width = 95;
            dataGridView1.Columns[2].Width = 70;
            cal();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            fillcombo();
            fillgrid();
        }

        private void txt_material_nm_TextChanged(object sender, EventArgs e)
        {

            dataGridView1.DataSource = db.Displaygrid("select item_name as [Product Name], qty as [Qty] ,unit as [Unit ] from tbl_warehouse   ");

            dataGridView1.Columns[0].Width = 220;
            dataGridView1.Columns[1].Width = 95;
            dataGridView1.Columns[2].Width = 70;
            cal();

        }

    }
}
