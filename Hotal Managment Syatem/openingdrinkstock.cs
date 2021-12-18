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
    public partial class openingdrinkstock : Form
    {
        Database db = new Database();
        public string getDate = string.Empty;
        public string dt;
        public openingdrinkstock()
        {
            InitializeComponent();
        }

        private void openingdrinkstock_Load(object sender, EventArgs e)
        {

            getDate = db.getDbstatus_Value("select ddate from tbl_dayend_status");
            dt = Convert.ToDateTime(getDate).ToString("MM-dd-yyyy");


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
                // dataGridView1.DataSource = db.Displaygrid("select item_name as[Material Name],sum(qty) as Quantity,unit as Unit from tbl_stock  where itemType='drinks' GROUP BY item_name, unit");

                                dataGridView1.DataSource = db.Displaygrid(@"select item_name as[Material Name],isnull(sum(a.useQty),0)+sum(stk.qty) as OpeningQty,isnull(sum(a.useQty),0) as SaleQty,sum(stk.qty) as BalanceQty,unit as Unit  from tbl_stock stk
                                                            left join 
                                                            ( 
                    select m.drinkGroup,(sum(si.qty)*sum(m.MenuQty))/d_grp.qtyInML useQty from sales_item si
                       inner join total_bill tob on si.order_id=tob.order_id and tob.datetime<'"+dt+"' inner join menu m on m.menu_id=si.menu_id inner join tbl_drinkgroup d_grp on m.drinkGroup=d_grp.grpName  where m.drinkGroup<>''  group by m.drinkGroup,d_grp.qtyInML   ) a on stk.item_name=a.drinkGroup   where itemType='drinks'  GROUP BY item_name, unit ");

                //original qry

//                dataGridView1.DataSource = db.Displaygrid(@"select item_name as[Material Name],isnull(sum(a.useQty),0)+sum(stk.qty) as OpeningQty,isnull(sum(a.useQty),0) as SaleQty,sum(stk.qty) as BalanceQty,unit as Unit  
//from tbl_stock stk   left join Drink_prevDaySales a on stk.item_name=a.drinkGroup and a.saleDate='" + dt + "' where itemType='drinks'    GROUP BY item_name, unit");

                dataGridView1.Columns[0].Width = 200;
                dataGridView1.Columns[0].ReadOnly = true;
                dataGridView1.Columns[1].ReadOnly = true;

               


            }
        }
        public void fillcombo()
        {
            db.comboFill(txt_material_nm, "select  item_name from tbl_stock where itemType='drinks'", "tbl_stock", "item_name", "item_name");
            txt_material_nm.Text = "All";
        }
        void cal()
        {
            double sum = 0;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                sum += double.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString());
            }
            txt_total.Text = sum.ToString();
        }

        private void txt_material_nm_SelectedIndexChanged(object sender, EventArgs e)
        {
            //  dataGridView1.DataSource = db.Displaygrid("select item_name as[Material Name],sum(qty) as Quantity,unit as Unit from tbl_stock  where itemType='drinks' and item_name='" + txt_material_nm.Text + "' GROUP BY item_name, unit");

            dataGridView1.DataSource = db.Displaygrid(@"select item_name as[Material Name],isnull(sum(a.useQty),0)+sum(stk.qty) as OpeningQty,isnull(sum(a.useQty),0) as SaleQty,sum(stk.qty) as BalanceQty,unit as Unit  from tbl_stock stk
                                                    left join 
                                                    ( 
                                                    select m.drinkGroup,(sum(si.qty)*sum(m.MenuQty))/d_grp.qtyInML useQty from sales_item si
                                                     inner join total_bill tob on si.order_id=tob.order_id and tob.datetime<'2019-02-19'
                                                     inner join menu m on m.menu_id=si.menu_id  
                                                     inner join tbl_drinkgroup d_grp on m.drinkGroup=d_grp.grpName
                                                     where m.drinkGroup<>'' 
                                                    group by m.drinkGroup,d_grp.qtyInML 
                                                    )
                                                     a on stk.item_name=a.drinkGroup
                                                    where itemType='drinks' and item_name='" + txt_material_nm.Text + "'  GROUP BY item_name, unit   ");








            dataGridView1.Columns[0].Width = 200;

            cal();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txt_material_nm.Text = "All";
            fillgrid();
            cal();
        }

        private void btn_print_Click(object sender, EventArgs e)
        {

            try
            {

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Excel Documents (*.xls)|*.xls";
                sfd.FileName = "DrinkStockReport.xls";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    //ToCsV(dataGridView1, @"c:\export.xls";
                    //******  db.ToCsV(dataGridView1, sfd.FileName); // Here dataGridview1 is your grid view name 
                    db.withReportTitle_ToCsV(dataGridView1, sfd.FileName, "DrinkStockReport");

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

        private void button1_Click(object sender, EventArgs e)
        {
            LPrinter lp = new LPrinter();
            lp.print_drinkreport(dataGridView1, txt_material_nm.Text);

        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.U)//e.Control == true && 
            {

                button2.Visible = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            string id, itemname, balance;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                itemname = dataGridView1.Rows[i].Cells[0].Value.ToString();
                //insentiveRate = float.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString());
                //  ACRate = float.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString());
                balance = dataGridView1.Rows[i].Cells[3].Value.ToString();
                //  driverRate = float.Parse(dataGridView1.Rows[i].Cells[8].Value.ToString());
                //  id = (dataGridView1.Rows[i].Cells[7].Value.ToString());

                db.update("update tbl_stock set qty='" + balance + "' where item_name='" + itemname + "' and itemType='drinks' ");

            }
            MessageBox.Show("Updated Successfully!!!", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // bind();
            fillgrid();
        }








    }
}
