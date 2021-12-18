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
    public partial class Cancel_KOT : Form
    {
        Database db = new Database();
        public string order_id, tableNo,waiterName,date;
       public bool cancelKOTFlag = false;
        string[] mName=new string[100];
        int[] qty = new int[50];

        public Cancel_KOT()
        {
            InitializeComponent();
        }
        public Cancel_KOT(string orderID, string tblNo,string wName)
        {
            InitializeComponent();
            this.order_id = orderID;
            this.tableNo = tblNo;
            this.waiterName = wName;
        }
        private void Cancel_KOT_Load(object sender, EventArgs e)
        {
            db.formFix(this);
            this.CancelButton = btnClose;
            
            lblTblNo.Text = tableNo;
            lblOrderId.Text = order_id;
            date = db.getDbstatus_Value("select ddate from tbl_dayend_status");
            dataGridView1.DataSource = db.Displaygrid("SELECT        menu.m_name AS Item_Name,SUM(sales_item.qty) AS Qty, SUM(sales_item.qty) AS [Cancel Qty], sales_item.rate AS Rate, SUM(sales_item.total_amount) AS Amount FROM            menu INNER JOIN   sales_item ON menu.menu_id = sales_item.menu_id GROUP BY menu.m_name, sales_item.rate,sales_item.order_id having        (sales_item.order_id = '" + lblOrderId.Text + "') and SUM(sales_item.qty)<>'0'");
            dataGridView1.Columns[1].Width = 220;
            dataGridView1.Columns[2].Width =80;
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Columns[4].Width = 80;
            dataGridView1.Columns[3].Width = 80;
            dataGridView1.Columns[5].Width = 80;
            dataGridView1.Columns[4].ReadOnly = true;
            dataGridView1.Columns[5].ReadOnly = true;
            ((DataGridViewTextBoxColumn)dataGridView1.Columns[3]).MaxInputLength = 3;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                mName[i] = dataGridView1.Rows[i].Cells[1].Value.ToString();
                qty[i] =int.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString());
                dataGridView1.Rows[i].Cells[3].Value = "0";
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.ColumnIndex == 0)
            //{
            //    string itemName, name = "", getsalesqty="";
            //    double qty1, updatedqty, rate, amt;
               
            //    int i = dataGridView1.SelectedCells[0].RowIndex;
            //    itemName = dataGridView1.Rows[i].Cells[1].Value.ToString();
            //    qty1 = double.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString());
            //    rate = double.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
            //    amt = double.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString());
            //    DialogResult dlgresult = MessageBox.Show("Are you sure want to Update?", "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //    if (dlgresult == DialogResult.Yes)
            //    {

            //        name = db.getDbstatus_Value("Select category from menu where menu_id=(select menu_id from menu where m_name='" + itemName + "') ");
            //        if (name == "Hard Drinks")
            //        {

            //            getsalesqty = db.getDb_Value("select qty from sales_item where menu_id=(select menu_id from menu where m_name='" + itemName + "') and order_id='" + lblOrderId.Text + "'").ToString();

            //            updatedqty = double.Parse(getsalesqty) - qty1;
            //            //..............................................//
            //            //.................qty add in stocks
            //            double sum = 0, stockqty = 0;
                      

            //            stockqty = db.getDb_Value("select qty from tbl_stock where item_name='" + itemName + "'");
            //            sum = (updatedqty + stockqty);
            //            db.update("update tbl_stock set qty='" + sum + "'  where item_name='" + itemName + "'");
                       
            //        }

            //        ///..................................................//
            //        db.update("Update  sales_item set qty='" + qty1 + "' ,total_amount='" + (qty1 * rate) + "' where menu_id=(select menu_id from menu where m_name='" + itemName + "') and order_id='" + lblOrderId.Text + "'");
            //              MessageBox.Show("Item Updated");
            //dataGridView1.DataSource = db.Displaygrid("SELECT        menu.m_name AS Item_Name, SUM(sales_item.qty) AS Qty, sales_item.rate AS Rate, SUM(sales_item.total_amount) AS Amount FROM            menu INNER JOIN   sales_item ON menu.menu_id = sales_item.menu_id WHERE        (sales_item.order_id = '" + lblOrderId.Text + "') GROUP BY menu.m_name, sales_item.rate");
                      

            //        }
                   
            //    }
            }
        bool flg = true;
        private void btnPrint_Click(object sender, EventArgs e)
        {
            // here update code
            string itemName, name = "", getsalesqty = "";
            double qty1, updatedqty, rate, amt, reduceQty, updatedqtyItem;
            DialogResult dlgresult = MessageBox.Show("Are you sure want to Update?", "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlgresult == DialogResult.Yes)
            {
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    itemName = dataGridView1.Rows[i].Cells[1].Value.ToString();
                    qty1 = double.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString());
                    reduceQty = double.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
                    rate = double.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString());
                    amt = double.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString());

                    if (reduceQty <= qty1)
                    {
                        name = db.getDbstatus_Value("Select category from menu where menu_id=(select menu_id from menu where m_name='" + itemName + "') ");
                        if (name == "Hard Drinks")
                        {

                            getsalesqty = db.getDb_Value("select qty from sales_item where menu_id=(select menu_id from menu where m_name='" + itemName + "') and order_id='" + lblOrderId.Text + "'").ToString();

                            //updatedqty = double.Parse(getsalesqty) - qty1;
                            updatedqty = double.Parse(getsalesqty) - reduceQty;
                            double sum = 0, stockqty = 0;


                            stockqty = db.getDb_Value("select qty from tbl_stock where item_name='" + itemName + "'");
                            sum = (updatedqty + stockqty);
                            db.update("update tbl_stock set qty='" + sum + "'  where item_name='" + itemName + "'");

                        }

                        updatedqtyItem = qty1 - reduceQty;

                        db.update("Update  sales_item set qty='" + updatedqtyItem + "' ,total_amount='" + (updatedqtyItem * rate) + "' where menu_id=(select menu_id from menu where m_name='" + itemName + "') and order_id='" + lblOrderId.Text + "'");
                        if (reduceQty != 0)
                            db.insert("insert into CancelKOTDetails ([orderId],[tblNo],[menuName],[qty],[rate],[amount] ,[date],[waiterName]) values('" + lblOrderId.Text + "','" + lblTblNo.Text + "','" + itemName + "','" + reduceQty + "','" + rate + "','" + (rate * reduceQty) + "','" + Convert.ToDateTime(date).ToString("MM/dd/yyyy") + "','" + waiterName + "')");


                    }
                    else
                    {
                        flg = false;
                        MessageBox.Show("Reduce Qty shoud be Less than or equal to Qty");
                    }
                }
            }
            MessageBox.Show("Item Updated");
          //  dataGridView1.DataSource = db.Displaygrid("SELECT        menu.m_name AS Item_Name, SUM(sales_item.qty) AS Qty, sales_item.rate AS Rate, SUM(sales_item.total_amount) AS Amount FROM            menu INNER JOIN   sales_item ON menu.menu_id = sales_item.menu_id WHERE        (sales_item.order_id = '" + lblOrderId.Text + "') GROUP BY menu.m_name, sales_item.rate");
            
                string[] newName = new string[100];
                int[] newQty = new int[50];

                int qtyDiff = 0;

                for (int k = 0; k < dataGridView1.RowCount; k++)
                {
                    rate = double.Parse(dataGridView1.Rows[k].Cells[3].Value.ToString());

                    if (int.Parse(dataGridView1.Rows[k].Cells[3].Value.ToString()) != 0)//qty[k] !=
                    {
                        // qtyDiff = qty[k] - int.Parse(dataGridView1.Rows[k].Cells[3].Value.ToString());
                        // dataGridView2.Rows.Add("", mName[k], qty[k]);

                        dataGridView2.Rows.Add("", mName[k], int.Parse(dataGridView1.Rows[k].Cells[3].Value.ToString()));
                        // db.insert("insert into CancelKOTDetails values('" + lblOrderId.Text + "','" + lblTblNo.Text + "','" + mName[k] + "','" + qtyDiff + "','" + rate + "','" + (rate * qtyDiff) + "','" + Convert.ToDateTime(date).ToString("MM/dd/yyyy") + "','" + waiterName + "')");

                    }


                }


                if (db.getDbstatus_Value("select status from tbl_option where grp='" + "KotPrint" + "'").ToString() == "Yes")
                {
                    string m_name = "";
                    LPrinter lp = new LPrinter();
                    lp.billid = float.Parse(lblOrderId.Text);
                    lp.tableno = (lblTblNo.Text);
                    lp.wname = waiterName;
                    int drinkcount = 0;
                    string[] drinkarry = new string[50];
                    int rowcount = dataGridView1.RowCount;
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        m_name = dataGridView1.Rows[i].Cells[1].Value.ToString();
                        // ******* here check kot print status NO of menu and remove that menu 
                        if (db.ChkDb_Value("SELECT        menu.category FROM            menu INNER JOIN      category ON menu.category = category.cat_name WHERE menu.m_name='" + m_name + "' and      (category.kotprintStatus = 'No')"))
                        {
                            //  drinkarry[drinkcount] = m_name;
                            dataGridView1.Rows.RemoveAt(i);
                            drinkcount++;
                            i--;
                        }

                    }



                    if (rowcount != 0)
                    {
                        lp.cancelKOTFlag = true;
                        lp.print_kot(dataGridView2);
                    }

                    //}
                }
                dataGridView1.DataSource = db.Displaygrid("SELECT   menu.m_name AS Item_Name, SUM(sales_item.qty) AS Qty,SUM(sales_item.qty) AS [Cancel Qty], sales_item.rate AS Rate, SUM(sales_item.total_amount) AS Amount FROM            menu INNER JOIN   sales_item ON menu.menu_id = sales_item.menu_id GROUP BY menu.m_name, sales_item.rate,sales_item.order_id having        (sales_item.order_id = '" + lblOrderId.Text + "') and SUM(sales_item.qty)<>'0'");

                dataGridView1.Columns[1].Width = 220;
                dataGridView1.Columns[2].Width = 80;
                dataGridView1.Columns[4].Width = 80;
                dataGridView1.Columns[3].Width = 80;
                dataGridView1.Columns[5].Width = 80;
                dataGridView1.Columns[2].ReadOnly = true;
                dataGridView1.Columns[4].ReadOnly = true;
                dataGridView1.Columns[5].ReadOnly = true;
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    dataGridView1.Rows[i].Cells[3].Value = "0";
                }
           
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
           
            

        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.ColumnIndex == 3)
            //{
            //    int i = dataGridView1.SelectedCells[0].RowIndex;
            //    if (int.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString()) > int.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString()))
            //    {
            //        dataGridView1.Rows[i].Cells[3].Value = 0;
            //    }
            //}
        }

        private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.ColumnIndex == 3)
            //{
            //    int i = dataGridView1.SelectedCells[0].RowIndex;
            //    if (int.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString()) > int.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString()))
            //    {
            //        dataGridView1.Rows[i].Cells[3].Value = 0;
            //    }
            //}
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    if (int.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString()) > int.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString()))
                    {
                        dataGridView1.Rows[i].Cells[3].Value = 0;
                    }
                }
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
            if (dataGridView1.CurrentCell.ColumnIndex == 3 || dataGridView1.CurrentCell.ColumnIndex == 4) //Desired Column
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                }
            }
        }

        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}