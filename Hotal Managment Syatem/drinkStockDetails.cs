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
    public partial class drinkStockDetails : Form
    {
        Database db = new Database();
        public drinkStockDetails()
        {
            InitializeComponent();
        }

        private void drinkStockDetails_Load(object sender, EventArgs e)
        {
            this.CancelButton = btn_close;
            db.formFix(this);
            fillcombo();
            fillgrid();
            cal();

        }

        public void fillgrid()
        {
            if (txt_material_nm.Text == "All")
            {
                dataGridView1.DataSource = db.Displaygrid("select item_name as[Material Name],sum(qty) as Quantity,unit as Unit from tbl_stock  where itemType='drinks' GROUP BY item_name, unit");
                dataGridView1.Columns[0].Width = 200;
                 
            }
        }
        public void fillcombo()
        {
            db.comboFill(txt_material_nm, "select  item_name from tbl_stock where itemType='drinks'", "tbl_stock", "item_name", "item_name");
            txt_material_nm.Text = "All";
        }

        private void txt_material_nm_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Displaygrid("select item_name as[Material Name],sum(qty) as Quantity,unit as Unit from tbl_stock  where itemType='drinks' and item_name='" + txt_material_nm.Text + "' GROUP BY item_name, unit");
            dataGridView1.Columns[0].Width = 200;
        }
        void cal()
        {
            double sum = 0;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
               sum+=double.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString());
            }
            txt_total.Text = sum.ToString();
        }

        private void txt_material_nm_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Displaygrid("select item_name as[Material Name],sum(qty) as Quantity,unit as Unit from tbl_stock  where itemType='drinks' and item_name='" + txt_material_nm.Text + "' GROUP BY item_name, unit");
            dataGridView1.Columns[0].Width = 200;

            cal();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txt_material_nm.Text = "All";
            fillgrid();
            cal();
        }
    }
}
