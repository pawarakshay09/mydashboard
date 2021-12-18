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
    public partial class Bill_Print : Form
    {
        Database db = new Database();
        private List<int> editedCelss = new List<int>();
        private float rate, qty, total_row;
        private int update_flag = 0;
        private int bill_no;
        private int menu_id;
        string[] orderIdArray = new string[200];
        int billNo;
        public Bill_Print()
        {
            InitializeComponent();
        }
        public Bill_Print(int bill_no)
        {
            InitializeComponent();
            this.billNo = bill_no;
        }
        private void button1_Click(object sender, EventArgs e)
        {
           
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            db.update("update total_bill set status='" + cmb_pay_type.Text + "' where order_id='" + billNo + "'");
            MessageBox.Show("Payment Type Save Successfully!!!");
            // update_EditChnages();
        }
        private void update_EditChnages()
        {
            // code here for update chnges into the dgv
           string order_id = "0";

            foreach (int k in editedCelss)
            {
                order_id = dataGridView1.Rows[k].Cells[0].Value.ToString();
                 menu_id= int.Parse(dataGridView1.Rows[k].Cells[6].Value.ToString());
                rate = float.Parse(dataGridView1.Rows[k].Cells[3].Value.ToString());
                qty = float.Parse(dataGridView1.Rows[k].Cells[4].Value.ToString());
                total_row = rate * qty;
                float totalAmountOfFood = db.getDb_Value("SELECT        SUM(sales_item.total_amount) AS Expr1 FROM            menu INNER JOIN  sales_item ON menu.menu_id = sales_item.menu_id INNER JOIN  category ON menu.category = category.cat_name WHERE        (sales_item.order_id = '" + billNo + "') AND (category.cat_name <> 'Hard Drinks')");
                float discAmt = ((totalAmountOfFood) * float.Parse(lblDiscValue.Text) / 100);
                lbl_disc.Text = discAmt.ToString();
                db.UpdateData("update sales_item set rate='" + rate + "',qty='" + qty + "',total_amount='" + total_row + "'  where  order_id='" + billNo + "' and menu_id='" + menu_id + "'");
               // db.UpdateData("update total_bill set Total_bill='" + lbl_grand_tot.Text + "'  where  order_id='" + billNo + "'");
                db.update("update table_order set discAmt='"+lbl_disc.Text+"' where order_id='" + billNo + "'");
            }
            pageLoad();
            db.update("update table_order set gst='" + (float.Parse(lbl_taxAmt.Text) * 2) + "' where order_id='" + billNo + "' ");
            db.UpdateData("update total_bill set Total_bill='" + lbl_grand_tot.Text + "'  where  order_id='" + billNo + "'");
            
            MessageBox.Show("Changed Data Save Successfully!!!");
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            double sum = 0;
            if (e.ColumnIndex == 3 || e.ColumnIndex == 4)
            {
                //dataGridView_allChellan.CommitEdit(DataGridViewDataErrorContexts.Commit);

                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    bill_no = int.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString());
                    rate = float.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
                    qty = float.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString());
                    total_row = rate * qty;
                    dataGridView1.Rows[i].Cells[5].Value = total_row.ToString();
                    editedCelss.Add(i);
                }
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    sum += double.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString());
                }
                txt_total.Text = sum.ToString();
                btn_update.Enabled = true;
                update_flag = 1;

                if (db.ChkDb_Value("select discValue from table_order where order_id='" + billNo + "'"))
                {
                    lblDiscValue.Text = db.getDb_Value("select discValue from table_order where order_id='" + billNo + "'").ToString();
                    lbl_disc.Text = db.getDb_Value("select discAmt from table_order where order_id='" + billNo + "'").ToString();
                }
                else
                {
                    lblDiscValue.Text = "0";
                    lbl_disc.Text = "0";
                }

                //if (db.ChkDb_Value("select discAmt  from table_order where order_id='" + billNo + "'"))
                //    lbl_disc.Text = db.getDb_Value("select discAmt  from table_order where order_id='" + billNo + "'").ToString();
                //else
                //    lbl_disc.Text = "0";

                //taxDiscCal();
                lbl_grand_tot.Text = Math.Round(float.Parse(txt_total.Text) - float.Parse(lbl_disc.Text)).ToString();

            }
        }

        void taxDiscCal()
        {
            //// float discAmt = (float.Parse(txt_total.Text) * float.Parse(lblDiscValue.Text) / 100);
            //float taxAmt = (float.Parse(txt_total.Text) * float.Parse(lblTaxValue.Text) / 100);
            //lbl_grand_tot.Text = (float.Parse(txt_total.Text) + taxAmt).ToString();// - discAmt
            //// lbl_disc.Text = discAmt.ToString();
            //lbl_tax.Text = taxAmt.ToString();
           
        }
        private void button3_Click(object sender, EventArgs e)
        {
           
            string tbl_no =(db.getDbstatus_Value("select t_id from table_order where order_id='"+lblBill.Text+"'"));
            LPrinter lp = new LPrinter();
            lp.billid = float.Parse(lblBill.Text);
            lp.tableno =(tbl_no.ToString());
            lp.wname = "";
            lp.print();
      
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Bill_Print_Load(object sender, EventArgs e)
        {
            db.comboFill(cmb_pay_type, "select payMode from tbl_paymentMode", "tbl_paymentMode", "payMode", "payMode");
          
            db.formFix(this);
            btn_update.Visible = false;
            this.CancelButton = btn_close;
            lblBill.Text = billNo.ToString();

            pageLoad();

          
            if (db.ChkDb_Value("SELECT order_id FROM total_bill GROUP BY order_id HAVING (COUNT(order_id) > 1) and order_id='" + lblBill.Text + "'"))
            {
               
                    btn_update.Enabled = false;
                    dataGridView1.ReadOnly = true;
                    dataGridView1.Columns[1].ReadOnly = true;
                    lblIntimate.Visible = true;
            }
            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TodayCounter ctr = new TodayCounter();
            ctr.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dlgresult = MessageBox.Show("Are you sure want to Delete?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgresult == DialogResult.Yes)
                {

                    db.DeleteData("delete from sales_item where order_id='" + billNo + "'", "sales_item");
                    db.DeleteData("delete from total_bill where order_id='" + billNo + "'", "total_bill");
                    db.DeleteData("delete from table_order where order_id='" + billNo + "'", "table_order");
                    MessageBox.Show("Bill Deleted Successfully!!!!", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
                       
        }

        void pageLoad()
        {
            try
            {
                string gst = "0";
                lbl_taxAmt.Text = "0";
                lblSgstAmt.Text = "0";
                Foodsc.Text= "0";
                lblVat.Text = "0";
                if (db.ChkDb_Value("select * from tbl_option where grp='" + "Tax" + "' and status='Yes'"))
                    pnlGST.Visible = true;
                else
                    pnlGST.Visible = false;
                if (db.ChkDb_Value("select order_id from sales_item where order_id='" + billNo + "'") == true)
                {


                    double sum = 0;
                    float w_id = float.Parse(db.getDb_Value("select w_id from sales_item where order_id='" + billNo + "'").ToString());
                    string name = db.getDbstatus_Value("SELECT wname from waiter_dtls where w_id='" + w_id + "' ").ToString();
                    lbl_wname.Text = name.ToString();
                    dataGridView1.DataSource = db.Displaygrid("SELECT sales_item.order_id, menu.m_name as [Menu Name] , sales_item.rate as Rate,sales_item.qty as [Qty], sales_item.total_amount as Total,sales_item.menu_id FROM   menu INNER JOIN   sales_item ON menu.menu_id = sales_item.menu_id WHERE(sales_item.order_id = '" + billNo + "') ");
                    dataGridView1.Columns[1].Visible = false;
                    dataGridView1.Columns[6].Visible = false;
                    dataGridView1.Columns[2].Width = 180;
                    dataGridView1.Columns[3].Width = 60;
                    dataGridView1.Columns[4].Width = 60;
                    dataGridView1.Columns[5].Width = 60;
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        sum += double.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString());
                    }
                    txt_total.Text = sum.ToString();

                    if (db.ChkDb_Value("select discValue from table_order where order_id='" + billNo + "'"))
                    {
                        //lblDiscValue.Text = db.getDb_Value("select discValue from table_order where order_id='" + billNo + "'").ToString();
                        //lbl_disc.Text = db.getDb_Value("select discAmt from table_order where order_id='" + billNo + "'").ToString();
                        lblDiscValue.Text = db.getDb_Value("select discValue from table_order where order_id='" + billNo + "'").ToString();
                        lbl_disc.Text = Math.Round(db.getDb_Value("select discAmt  from table_order where order_id='" + billNo + "'")).ToString();
                        Foodsc.Text = Math.Round(db.getDb_Value("select serviceTaxAmt  from table_order where order_id='" + billNo + "'")).ToString();
                        lblVat.Text = Math.Round(db.getDb_Value("select vatAmt  from table_order where order_id='" + billNo + "'")).ToString();
                       // cmb_pay_type.Text = db.getDb_Value("select status  from total_bill where order_id='" + billNo + "'").ToString();

                        cmb_pay_type.Text= db.getDbstatus_Value("select  status  from total_bill where order_id='" + billNo + "'").ToString();
                        lbl_foodDisAmt.Text = db.getDbstatus_Value("select  foodDiscAmt  from table_order where order_id='" + billNo + "'").ToString();
                        lbl_foodDisVal.Text = db.getDbstatus_Value("select  foodDiscValue  from table_order where order_id='" + billNo + "'").ToString();
                        lbl_BevDisVal.Text = db.getDbstatus_Value("select  beveragesDiscValue  from table_order where order_id='" + billNo + "'").ToString();
                        lbl_BevDisAmt.Text = db.getDbstatus_Value("select  beveragesDiscAmt  from table_order where order_id='" + billNo + "'").ToString();
                        lbl_LiquorDisVal.Text = db.getDbstatus_Value("select  liquorDiscValue  from table_order where order_id='" + billNo + "'").ToString();
                        lbl_LiquorDisAmt.Text = db.getDbstatus_Value("select  liquorDiscAmt  from table_order where order_id='" + billNo + "'").ToString();
                        lblFoodSCVal.Text = db.getDbstatus_Value("select  serviceTaxFoodVal  from table_order where order_id='" + billNo + "'").ToString();
                        lblFoodSCAmt.Text = db.getDbstatus_Value("select  serviceTaxFoodVal  from table_order where order_id='" + billNo + "'").ToString();
                        lblLiquorSCVal.Text = db.getDbstatus_Value("select  serviceTaxLiquorVal  from table_order where order_id='" + billNo + "'").ToString();
                        lblLiquorSCAmt.Text = db.getDbstatus_Value("select  serviceTaxLiquorAmt  from table_order where order_id='" + billNo + "'").ToString();


                    }
                    else
                        lblDiscValue.Text = "0";                   
                   

                    //taxDiscCal();

                    float totalAmountOfFood = db.getDb_Value("SELECT        SUM(sales_item.total_amount) AS Expr1 FROM            menu INNER JOIN  sales_item ON menu.menu_id = sales_item.menu_id INNER JOIN  category ON menu.category = category.cat_name WHERE        (sales_item.order_id = '" + billNo + "') AND (category.cat_name <> 'Hard Drinks')");
                    float totalAmount = db.getDb_Value("SELECT        SUM(total_amount) AS Expr1 FROM            sales_item WHERE        (order_id = '" + billNo + "')");
                
                    //string netAmt = (float.Parse(txt_total.Text) - float.Parse(lbl_disc.Text)).ToString();
                    string netAmt = (totalAmountOfFood - float.Parse(lbl_disc.Text)).ToString();
                    if (db.ChkDb_Value("select value from tbl_option where grp='" + "Tax" + "' and status='Yes'"))
                    {
                        gst = db.getDb_Value("select value from tbl_option where grp='" + "Tax" + "' and status='Yes'").ToString();
                        float per = float.Parse(gst) / 2;
                        lblTaxValue.Text = per.ToString();
                        lblSgstVal.Text = lblTaxValue.Text;
                        double CGSTval = (double.Parse(netAmt) * per / 100);
                        lbl_taxAmt.Text = Math.Round(CGSTval,2).ToString();
                        lblSgstAmt.Text = Math.Round(CGSTval,2).ToString();                        
                    }

                    lbl_grand_tot.Text = db.getDbstatus_Value("select  Total_bill  from total_bill where order_id='" + billNo + "'");
                    //lbl_grand_tot.Text = Math.Round(float.Parse(txt_total.Text) - float.Parse(lbl_disc.Text) + float.Parse(lbl_taxAmt.Text) + float.Parse(lblSgstAmt.Text) + float.Parse(Foodsc.Text) + float.Parse(lblVat.Text)).ToString();
                    //lbl_grand_tot.Text = Math.Round(float.Parse(txt_total.Text) - float.Parse(lbl_disc.Text)).ToString();
                }
                else
                {
                    MessageBox.Show("Bill No Does Not Exist", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dataGridView1.DataSource = "";
                    lbl_wname.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!db.ChkDb_Value("SELECT order_id FROM total_bill GROUP BY order_id HAVING (COUNT(order_id) > 1) and order_id='" + lblBill.Text + "'"))
            {

                if (e.ColumnIndex == 0)
                {
                    string itemName, name;
                    double qty1, stockqty;
                    db.cnopen();
                    int i = dataGridView1.SelectedCells[0].RowIndex;
                    qty1 = double.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString());
                    itemName = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    float orderID = float.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString());
                    DialogResult dlgresult = MessageBox.Show("Are you sure want to Delete?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgresult == DialogResult.Yes)
                    {
                        try
                        {
                            db.DeleteData("delete FROM sales_item where menu_id=(select menu_id from menu where m_name='" + itemName + "') and order_id='" + orderID + "'", "sales_item");

                            pageLoad();
                            db.UpdateData("update total_bill set Total_bill='" + lbl_grand_tot.Text + "'  where  order_id='" + billNo + "'");
                            name = db.getDbstatus_Value(" Select category from menu where menu_id=(select menu_id from menu where m_name='" + itemName + "')");

                            if (name == "Hard Drinks" && name == "HARD DRINKS")
                            {
                                stockqty = float.Parse(db.getDb_Value("select qty from tbl_stock where item_name='" + itemName + "'").ToString());
                                double sum = (stockqty) + qty1;
                                db.update("update tbl_stock set qty='" + sum + "'  where item_name='" + itemName + "'");
                            }
                            db.update("update table_order set gst='" + (float.Parse(lbl_taxAmt.Text) * 2) + "' where order_id='" + billNo + "' ");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error  " + ex.Message);
                        }
                        db.cnclose();
                    }

                }
            }
        }

        private void Bill_Print_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
            {
                btn_update.Visible = true;
            }
        }

        private void pnlGST_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void cmb_pay_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            btn_update.Visible = true;
        }

    }
}
