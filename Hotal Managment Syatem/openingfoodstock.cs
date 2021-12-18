using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Hotal_Managment_Syatem
{
    public partial class openingfoodstock : Form
    {

        Database db = new Database();
        public string getDate = string.Empty;
        public string dt;
        public openingfoodstock()
        {
            InitializeComponent();
        }

        private void openingfoodstock_Load(object sender, EventArgs e)
        {
            getDate = db.getDbstatus_Value("select ddate from tbl_dayend_status");
            dt = Convert.ToDateTime(getDate).ToString("MM-dd-yyyy");

            this.CancelButton = btn_close;
            db.formFix(this);
            fillcombo();
            fillgrid();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void fillgrid()
        {
            if (txt_material_nm.Text == "All")
            {
                //   dataGridView1.DataSource = db.Displaygrid("select item_name as[Material Name],sum(qty) as Quantity,unit as Unit from tbl_stock where itemType!='drinks' GROUP BY item_name, unit");


                dataGridView1.DataSource = db.Displaygrid(@"select item_name as[Material Name],isnull(sum(a.useQty),0)+sum(stk.qty) as OpeningQty,isnull(sum(a.useQty),0) as SaleQty,sum(stk.qty) as BalanceQty,unit as Unit  from tbl_stock stk
left join food_pervDaySales  a on stk.item_name=a.Material_name   and a.saleDate='" + dt + "' where itemType!='drinks' GROUP BY item_name, unit");


                dataGridView1.Columns[0].Width = 220;
                dataGridView1.Columns[1].Width = 95;
                dataGridView1.Columns[2].Width = 70;
                cal();
                dataGridView1.Columns[0].ReadOnly = true;
            }
        }
        public void fillcombo()
        {
            db.comboFill(txt_material_nm, "select distinct item_name from tbl_stock where itemType!='drinks'", "tbl_stock", "item_name", "item_name");
            txt_material_nm.Text = "All";
        }

        private void txt_material_nm_SelectedIndexChanged(object sender, EventArgs e)
        {

            dataGridView1.DataSource = db.Displaygrid(@"select item_name as[Material Name],isnull(sum(a.useQty),0)+sum(stk.qty) as OpeningQty,isnull(sum(a.useQty),0) as SaleQty,sum(stk.qty) as BalanceQty,unit as Unit  from tbl_stock stk
left join food_pervDaySales  a on stk.item_name=a.Material_name   and a.saleDate='" + dt + "' where itemType!='drinks' and (item_name = '" + txt_material_nm.Text + "') GROUP BY item_name, unit");


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

        private void btn_print_Click(object sender, EventArgs e)
        {
            try
            {

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Excel Documents (*.xls)|*.xls";
                sfd.FileName = "StockReport.xls";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    //ToCsV(dataGridView1, @"c:\export.xls";
                    //******  db.ToCsV(dataGridView1, sfd.FileName); // Here dataGridview1 is your grid view name 
                    db.withReportTitle_ToCsV(dataGridView1, sfd.FileName, "StockReport");

                    MessageBox.Show("File Sucessfully Exported");

                    // string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Solution.xls");
                    // string filePath = Path.Combine(Environment.GetFolderPath(sfd.Selected), sfd.FileName);

                    Process.Start(sfd.FileName);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // float ACRate, nonACRate, driverRate, itemCode, insentiveRate;

            string id, itemname, balance;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                itemname = dataGridView1.Rows[i].Cells[0].Value.ToString();
                //insentiveRate = float.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString());
                //  ACRate = float.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString());
                balance = dataGridView1.Rows[i].Cells[3].Value.ToString();
                //  driverRate = float.Parse(dataGridView1.Rows[i].Cells[8].Value.ToString());
                //  id = (dataGridView1.Rows[i].Cells[7].Value.ToString());

                db.update("update tbl_stock set qty='" + balance + "' where item_name='" + itemname + "' and itemType!='drinks' ");

            }
            MessageBox.Show("Updated Successfully!!!", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // bind();
            fillgrid();
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.U)//e.Control == true && 
            {

                button2.Visible = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LPrinter lp = new LPrinter();
            lp.print_stockreport(dataGridView1, txt_material_nm.Text);

        }









    }
}
