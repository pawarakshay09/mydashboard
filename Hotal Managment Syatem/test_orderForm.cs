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
    public partial class test_orderForm : Form
    {
        Database db = new Database();
        string menu = "";
        string item; string selected_item;
        int dgvRow = 0;
        string _orderId, _tableId, captainID,date;
        string menuid, qty, getDate, dt;
        float item_rate, itemTotal;
        public bool orderConfirm = true;
        string order_tb_status = "Novat";
        bool orderadd;

        float new_qty = 0;

        float[] materialQty_array = new float[200];
        string[] materialName_array = new string[200];
        string[] materialUnit_array = new string[200];

        int count = 0;

        public test_orderForm()
        {
            InitializeComponent();
        }
        public test_orderForm(string orderID, string tableID,string date)
        {
            InitializeComponent();
            this._orderId = orderID;
            this._tableId = tableID;
            this.date = date;



        }
        public test_orderForm(string orderID, string tableID, string date ,bool ordera)
        {
            InitializeComponent();
            this._orderId = orderID;
            this._tableId = tableID;
            this.date = date;
           this.orderadd = ordera;


        }
        bool wflag = false;
        private void test_orderForm_Load(object sender, EventArgs e)
        {
            if (db.ChkDb_Value("select value from tbl_option where grp='" + "CustomerDetails" + "' and status='Yes'"))
            {
                panelCustomr.Visible = true;
                cmbWaiterName1.Focus();
            }




            //bind captn list 
            db.comboFill(cmbWaiterName1, "select wname from  waiter_dtls where work_type='Captain'", "waiter_dtls", "wname", "wname");
            //waiter /////----Yogesh 20.02.2019
            db.comboFill(combowaiterN, "select wname from  waiter_dtls where work_type='Waiter'", "waiter_dtls", "wname", "wname");



            if (db.ChkDb_Value("select status from tbl_option where grp='" + "waiterDetails" + "' and status='Yes'"))
            {
                
                red1.Visible = true;
                red2.Visible = true;
            

                cmbWaiterName1.Text = "select";
           
                combowaiterN.Text = "select";
                

                wflag = true;
            }
                      
            
            db.formFix(this); // form layout constant
            //  CreateMyListView();
            this.CancelButton = btn_close;
            //assign values to the labels
            label_orderID.Text = _orderId;
            
            label_tableNo.Text = _tableId;
            label_tbalNo2.Text = _tableId;
            getDate = db.getDbstatus_Value("select ddate from tbl_dayend_status");
            dt = Convert.ToDateTime(getDate).ToString("MM/dd/yyyy");

            //yogesh for dynamic Customer print on bill =====20.02.2019
            if (panelCustomr.Visible && db.ChkDb_Value("select * from table_order where order_id= '" + label_orderID.Text + "'"))
            {
                txtcustname.Text = db.getDbstatus_Value("SELECT ISNULL((SELECT Custname FROM table_order  WHERE order_id = '" + label_orderID.Text + "'),0)");
                txtmob.Text = db.getDbstatus_Value("SELECT ISNULL((SELECT mob FROM table_order   WHERE order_id = '" + label_orderID.Text + "'),0)");
                custgst.Text = db.getDbstatus_Value("SELECT ISNULL((SELECT Custgst FROM table_order   WHERE order_id = '" + label_orderID.Text + "'),0)");

              //  cmbWaiterName1.Text = db.getDbstatus_Value("SELECT w_name FROM  table_order where order_id='" + label_orderID.Text + "'");
                cmbWaiterName1.Text = db.getDbstatus_Value("SELECT wname  FROM  waiter_dtls where w_id=(select top 1 w_id from  sales_item where order_id='" + label_orderID.Text + "')");


                combowaiterN.Text = db.getDbstatus_Value("SELECT Waiter FROM  table_order where order_id='" + label_orderID.Text + "'");
       

            }
                


            try
            {
                SqlCommand cmd = new SqlCommand("select * from category", db.cn);
                db.cnopen();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read() == true)
                {
                    menu = rd["cat_name"].ToString();


                    listView1.Items.Add(menu);

                }
                db.cnclose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }



        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

            bind_itemsList();
            // listView_items.Focus();

        }
        void bind_itemsList()
        {
            listView_items.Clear();

            if (listView1.SelectedItems.Count >= 1)
            {
                selected_item = listView1.SelectedItems[0].Text;

            }
            //selected_item = listView1.SelectedItems[0].Text;

            string qry = "select m_name from menu where category= '" + selected_item + "' order by m_name asc ";

            SqlCommand cmd = new SqlCommand(qry, db.cn);
            db.cnopen();
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                item = rd["m_name"].ToString();

                listView_items.Items.Add(item);

            }

        }

        private void listView_items_SelectedIndexChanged(object sender, EventArgs e)
        {
            string seletedItemName = "";
            int qty = 1;
            int get_qty = 0, flag = 0;



            if (listView_items.SelectedItems.Count > 0)
            {
                seletedItemName = listView_items.SelectedItems[0].Text;

                for (int i = 0; i <= DGV_tblOrder.RowCount - 1; i++)
                {
                    if (DGV_tblOrder.Rows[i].Cells[1].Value.ToString() == seletedItemName)
                    {
                        get_qty = int.Parse(DGV_tblOrder.Rows[i].Cells[2].Value.ToString());
                        qty += get_qty; // get old qty and add new qty
                        flag++; //for the alray exists

                        DGV_tblOrder.Rows[i].Cells[2].Value = qty.ToString(); //add new qty into dgv
                    }
                }

                if (flag == 0)
                    DGV_tblOrder.Rows.Add(null, seletedItemName, qty);

                labelItemsCount.Text = DGV_tblOrder.RowCount.ToString(); //count the dgv rows / items
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dlgresult = MessageBox.Show("Are You Sure want to Clear Table Order?", "Confirm Clear", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlgresult == DialogResult.Yes)
            {
                DGV_tblOrder.Rows.Clear();
                labelItemsCount.Text = "0";
                pnlCalc.Visible = false;
            }
        }

        private void DGV_tblOrder_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.ColumnIndex == DGV_tblOrder.Columns[0].Index)
            //{
            //    foreach (DataGridViewRow item in this.DGV_tblOrder.SelectedRows)
            //    {
            //        DGV_tblOrder.Rows.RemoveAt(item.Index);
            //    }
            // //   DGV_tblOrder.ClearSelection();
            //}

            int k = DGV_tblOrder.SelectedCells[0].RowIndex;
            if (e.ColumnIndex == 2)
                DGV_tblOrder.Rows[k].Cells[2].Value = "";

            if (e.ColumnIndex == 0)
            {
                if (e.ColumnIndex == DGV_tblOrder.Columns[0].Index)
                {
                    foreach (DataGridViewRow item in this.DGV_tblOrder.SelectedRows)
                    {
                        DGV_tblOrder.Rows.RemoveAt(item.Index);
                    }
                    //   DGV_tblOrder.ClearSelection();
                }
            }
            labelItemsCount.Text = DGV_tblOrder.RowCount.ToString();

        }


        private void DGV_tblOrder_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = DGV_tblOrder.SelectedCells[0].RowIndex;
            if (e.ColumnIndex == 2)
            {
                DGV_tblOrder.Rows[i].Cells[2].Value = "";
                pnlCalc.Visible = true;
            }

            else
                pnlCalc.Visible = false;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            // here order is closing without effect

            if (!db.ChkDb_Value("select * from sales_item WHERE order_id='" + label_orderID.Text + "'"))
            {
                db.update("UPDATE table_status SET status='Empty' WHERE t_id='" + label_tableNo.Text + "'");
                db.DeleteData("delete from  table_order  where order_id='" + label_orderID.Text + "'", "table_order");
            }
            pnlCalc.Visible = false;
            orderConfirm = false;
            this.Close();


        }

       public  void getdis()
        {

            //get food discount val and amt
         
          
           // float total_amount = db.getDb_Value("select SUM(total_amount ) from sales_item si inner join table_order tob  on si.order_id=tob.order_id  where tob.order_id='" + label_orderID.Text + "'");
            float Foodamt = db.getDb_Value("SELECT ISNULL((  select SUM(total_amount ) from sales_item si inner join table_order tob  on si.order_id=tob.order_id inner join  menu m on m.menu_id =si.menu_id inner join category cat on m.category =cat.cat_name  where tob.order_id='" + label_orderID.Text + "' and FoodSection ='Food'),0)");
            float Liquoramt = db.getDb_Value("SELECT ISNULL((  select SUM(total_amount ) from sales_item si inner join table_order tob  on si.order_id=tob.order_id inner join  menu m on m.menu_id =si.menu_id inner join category cat on m.category =cat.cat_name  where tob.order_id='" + label_orderID.Text + "' and FoodSection ='Liquor'),0)");
            float Beveragesamt = db.getDb_Value("SELECT ISNULL((  select SUM(total_amount ) from sales_item si inner join table_order tob  on si.order_id=tob.order_id inner join  menu m on m.menu_id =si.menu_id inner join category cat on m.category =cat.cat_name  where tob.order_id='" + label_orderID.Text + "' and FoodSection ='Beverages'),0)");
         
            if (db.ChkDb_Value("select * from tbl_option where grp='Discountpercentage' "))
            {
                float value = db.getDb_Value("select value from tbl_option where grp='Discountpercentage'");
                if (value > 0)
                {


                  //  float disAmt = ((value) * total_amount / 100);
                    float fooddisamt = ((value) * Foodamt / 100);
                    float Liquodisamt = ((value) * Liquoramt / 100);
                    float Beveragdisamt = ((value) * Beveragesamt / 100);
                 //   db.update("update table_order set discValue  ='" + value + "',discAmt='" + (disAmt) + "' where order_id='" + label_orderID.Text + "' and t_id='" + label_tbalNo2.Text + "'");

                    db.update("update table_order set foodDiscValue='" + value + "',foodDiscAmt='" + (fooddisamt) + "', liquorDiscValue='" + value + "',liquorDiscAmt='" + (Liquodisamt) + "',beveragesDiscValue='" + value + "' ,beveragesDiscAmt='" + (Beveragdisamt) + "' where order_id='" + label_orderID.Text + "' and t_id='" + label_tbalNo2.Text + "'");
             

                }
            }


        }
        string category = "";
        bool flg = false;
        bool flagcust = false;
        bool ff = true;
        string table_status_1 = "Processing";
        private void button2_Click(object sender, EventArgs e)
        {

            string MenuComment = string.Empty;
            pnlCalc.Visible = false;
            for (int i = 0; i < DGV_tblOrder.Rows.Count; i++)
            {

                if (DGV_tblOrder.Rows[i].Cells[2].Value != "0" || DGV_tblOrder.Rows[i].Cells[2].Value != "")
                {
                    flg = true;
                }
                else
                {
                    flg = false;
                    MessageBox.Show("Please Enter the Qty.");

                }
            }// string  query = "INSERT INTO sales_item(order_id,menu_id,qty,rate,total_amount,w_id) VALUES('" + _orderId + "','" + menu_id + "','" + txtQty.Text + "','" + txt_price.Text + "','" + txt_totalAmt.Text + "','" + wid + "')";
          
            if (wflag && cmbWaiterName1.Text == "select" && combowaiterN.Text == "select")
            {
                MessageBox.Show("Please Select Captain and Waiter For Order");
                ff = false;

            }
            else 
            {
                ff = true;
            }
            if (ff)
            {
                //get the captian is 
                if (flg)
                {
                    if (cmbWaiterName1.Text != "")
                        captainID = db.getDb_Value("select w_id from waiter_dtls where wname='" + cmbWaiterName1.Text + "'").ToString();
                    else
                        captainID = "0";

                    int KOT_id = int.Parse(db.getDb_Value("select max(kot_id) from sales_item").ToString()) + 1; //get the kot from the tbl


                    if (DGV_tblOrder.RowCount != 0)
                    {

                        //assing waiter to the table 

                        string oldid = db.getDbstatus_Value("select MAX(order_id) from table_order").ToString();
                        //qur = "insert into table_order (order_id,t_id,timeing,w_id,order_type) values('" + label_order_id.Text + "','" + txtTableNo.Text + "','" + label_date.Text + "'," + wID + ",'" + order_tb_status + "') ";
                      //  if (panelCustomr.Visible && oldid == label_orderID.Text)
                        if (panelCustomr.Visible && !db.ChkDb_Value("select * from table_order where order_id='" + label_orderID .Text+ "' "))
                     
                        {

                            db.insert("insert into table_order (order_id,t_id,timeing,w_id,order_type,tableStatus,discValue,discAmt,gst,Waiter,noOfGuest,Custname,mob,Custgst) values('" + label_orderID.Text + "','" + label_tbalNo2.Text + "','" + Convert.ToDateTime(date).ToString("MM/dd/yyyy") + "'," + captainID + ",'"+order_tb_status+"','Processing','0','0','0','" + combowaiterN.Text + "','" + txtPerson.Text + "','" + txtcustname.Text + "','" + txtmob.Text + "','" + custgst.Text + "') ");

                            //db.update("update table_order set w_id='" + captainID + "',tableStatus='Processing' , Waiter='" + combowaiterN.Text + "', noOfGuest='" + txtPerson.Text + "' ,Custname='" + txtcustname.Text + "',mob='" + txtmob.Text + "' ,Custgst='" + custgst.Text + "' where order_id='" + label_orderID.Text + "' and t_id='" + label_tbalNo2.Text + "'");
                             flagcust = true;
                         //   db.update("UPDATE table_status SET status='' WHERE t_id='" + txtTableNo.Text + "'");
                      


                        }
                        else if (!orderadd)
                        {
                            db.insert("insert into table_order (order_id,t_id,timeing,w_id,order_type,tableStatus,discValue,discAmt,gst,Waiter,noOfGuest,Custname,mob,Custgst) values('" + label_orderID.Text + "','" + label_tbalNo2.Text + "','" + Convert.ToDateTime(date).ToString("MM/dd/yyyy") + "'," + captainID + ",'" + order_tb_status + "','Processing','0','0','0','" + combowaiterN.Text + "','" + txtPerson.Text + "','" + txtcustname.Text + "','" + txtmob.Text + "','" + custgst.Text + "') ");

                            flagcust = false;
                        }
                        else
                        {
                            //db.update("update table_order set w_id='" + captainID + "',tableStatus='Processing' ,  Waiter='" + combowaiterN.Text + "', noOfGuest='" + txtPerson.Text + "' where order_id='" + label_orderID.Text + "' and t_id='" + label_tbalNo2.Text + "'");
                            db.update("update table_order set w_id='" + captainID + "',tableStatus='Processing' , Waiter='" + combowaiterN.Text + "', noOfGuest='" + txtPerson.Text + "' ,Custname='" + txtcustname.Text + "',mob='" + txtmob.Text + "' ,Custgst='" + custgst.Text + "' where order_id='" + label_orderID.Text + "' and t_id='" + label_tbalNo2.Text + "'");
                         
                        }
                        if (label_orderID.Text != "0")
                        {
                            DialogResult dlgresult = MessageBox.Show("Are You Sure want to Complete Table Order?", "Confirm Complete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (dlgresult == DialogResult.Yes)
                            {
                                count = 0;


                                for (int i = 0; i <= DGV_tblOrder.RowCount - 1; i++)
                                {
                                    if (DGV_tblOrder.Rows[i].Cells[2].Value == "0" || DGV_tblOrder.Rows[i].Cells[2].Value == "")
                                    {
                                        MessageBox.Show("Please Enter the Qty.");
                                        return;
                                    }
                                    else
                                    {
                                        menuid = db.getDb_Value("select menu_id from menu where m_name='" + DGV_tblOrder.Rows[i].Cells[1].Value.ToString() + "' ").ToString(); //get manu id
                                        qty = DGV_tblOrder.Rows[i].Cells[2].Value.ToString(); // get the menu qty from  the DGV

                                        //   MenuComment = DGV_tblOrder.Rows[i].Cells["comment"].Value.ToString(); // get the menu comment from  the DGV

                                        category = db.getDbstatus_Value("select category from menu where m_name='" + DGV_tblOrder.Rows[i].Cells[1].Value.ToString() + "'");


                                        //get the item rate according to the table type
                                        if (db.ChkDb_Value("select * from table_status where table_type='" + "A/C" + "'and t_id='" + label_tableNo.Text + "'"))
                                            item_rate = db.getDb_Value("select rate from menu where m_name='" + DGV_tblOrder.Rows[i].Cells[1].Value.ToString() + "' "); // get  the item rate from the tbl
                                        else if (db.ChkDb_Value("select * from table_status where table_type='" + "Non A/C" + "'and t_id='" + label_tableNo.Text + "'"))
                                            item_rate = db.getDb_Value("select non_ACrate from menu where m_name='" + DGV_tblOrder.Rows[i].Cells[1].Value.ToString() + "' "); // get  the item rate from the tbl
                                        else
                                            item_rate = db.getDb_Value("select driverRate from menu where m_name='" + DGV_tblOrder.Rows[i].Cells[1].Value.ToString() + "' "); // get  the item rate from the tbl


                                        itemTotal = item_rate * int.Parse(qty); // calculate the items toatal

                                        //insert the vlaues into the table for wach item

                                        //code hide by sagar for add seprate itmesin sales item
                                        ////update existin menu by harshu.. start 
                                        //if (db.ChkDb_Value("select * from sales_item where order_id='" + label_orderID.Text + "' and menu_id='" + menuid + "'"))
                                        //{
                                        //    float old_qty = db.getDb_Value("select qty from sales_item where order_id='" + label_orderID.Text + "' and menu_id='" + menuid + "'");

                                        //    float new_qty = float.Parse(DGV_tblOrder.Rows[i].Cells[2].Value.ToString());
                                        //    float tot_qty = old_qty + new_qty;
                                        //    float total = item_rate * tot_qty;
                                        //    db.update("update sales_item set qty='" + tot_qty + "',total_amount='" + total + "' where order_id='" + label_orderID.Text + "' and menu_id='" + menuid + "' and Date='" + dt + "'");
                                        //}
                                        ////end code
                                        //else
                                        //{
                                        //    db.insert("INSERT INTO sales_item(order_id,menu_id,qty,rate,total_amount,w_id,kot_id,Date) VALUES('" + _orderId + "','" + menuid + "','" + qty + "','" + item_rate + "','" + itemTotal + "','" + captainID + "','" + KOT_id + "','" + dt + "')");
                                        //}

                                        //db.insert("INSERT INTO sales_item(order_id,menu_id,qty,order_sugg,rate,total_amount,w_id,kot_id,Date) VALUES('" + _orderId + "','" + menuid + "','" + qty + "','" + Comment + "','" + item_rate + "','" + itemTotal + "','" + captainID + "','" + KOT_id + "','" + dt + "')");
                                        db.insert("INSERT INTO sales_item(order_id,menu_id,qty ,rate,total_amount,w_id,kot_id,Date) VALUES('" + _orderId + "','" + menuid + "','" + qty + "' ,'" + item_rate + "','" + itemTotal + "','" + captainID + "','" + KOT_id + "','" + dt + "' )");
                                        //ItemComment
                                        //  db.insert("insert into tbl_temporder values('" + menuid + "','" + qty + "','" + DGV_tblOrder.Rows[i].Cells[1].Value.ToString() + "','" + captainID + "','" + label_orderID.Text + "','" + category + "','','" + KOT_id + "')  ");

                                        db.insert("INSERT INTO  [tbl_temporder]([menuid],[qty],[m_name],[w_id],[order_id],[category],[ordersuggestion],[kot_id]) values('" + menuid + "','" + qty + "','" + DGV_tblOrder.Rows[i].Cells[1].Value.ToString() + "','" + captainID + "','" + label_orderID.Text + "','" + category + "','" + DGV_tblOrder.Rows[i].Cells[3].Value + "','" + KOT_id + "')  ");

                                        orderConfirm = true;

                                        db.update("UPDATE table_status SET status='" + table_status_1 + "' WHERE t_id='" + label_tbalNo2.Text + "'");
               
                                    }
                                }


                                //add the suggestion for the order / remark

                                //       db.insert("INSERT INTO tbl_orderSuggestion VALUES('" + KOT_id + "','" + textBox_custSugggestion.Text + "')");
                                int k = DGV_tblOrder.SelectedCells[0].RowIndex;

                                //return to welcome page 
                                ((Welcome)this.Owner).bind(null);
                                
                                
                                this.Close();

                                //ask for printing kot  // the db values for the kot 
                                if (db.getDbstatus_Value("select status from tbl_option where grp='" + "KotPrint" + "'").ToString() == "Yes")
                                {
                                    string m_name = "";
                                    LPrinter lp = new LPrinter();
                                    lp.billid = float.Parse(_orderId);
                                    lp.tableno = (_tableId);
                                    lp.wname = cmbWaiterName1.Text;
                                    int drinkcount = 0;
                                    string[] drinkarry = new string[50];
                                    int rowcount = DGV_tblOrder.RowCount;
                                    for (int i = 0; i < DGV_tblOrder.RowCount; i++)
                                    {
                                        m_name = DGV_tblOrder.Rows[i].Cells[1].Value.ToString();
                                        // ******* here check kot print status NO of menu and remove that menu 
                                        if (db.ChkDb_Value("SELECT   menu.category FROM   menu INNER JOIN      category ON menu.category = category.cat_name WHERE menu.m_name='" + m_name + "' and      (category.kotprintStatus = 'No')"))
                                        {
                                            //  drinkarry[drinkcount] = m_name;
                                            DGV_tblOrder.Rows.RemoveAt(i);
                                            drinkcount++;
                                            i--;
                                        }

                                    }
                                    //if (rowcount != 0)
                                    //    lp.print_kot(DGV_tblOrder);


                                    KOtPrintCode();
                                    db.insert("delete from tbl_temporder where order_id='" + label_orderID.Text + "'");
                                }

                                //if (db.getDbstatus_Value("select status from tbl_option where grp='" + "KOT Printer_Snacks" + "' ").ToString() == "Yes")
                                //{
                                //    KOtPrintCode();
                                //    db.insert("delete from tbl_temporder where order_id='" + label_orderID.Text + "'");
                                //}
                            }
                        }
                        else
                        {
                            MessageBox.Show("Order Id Should not be Zero,Please check Order.");
                        }
                    }

                    else
                    { MessageBox.Show("Please Select Item For Order"); }
                }
            }
            //else
            //{ MessageBox.Show("Please Select Captain and Waiter For Order"); }
            getdis();

        }


       public void KOtPrintCode()
        {
            
   
            LPrinter lp = new LPrinter();
            string[] printNm = new string[10];
            count = 0;
            db.cnopen();
            //  SqlCommand cmdowner = new SqlCommand("SELECT DISTINCT category.printerName  FROM category INNER JOIN   tbl_temporder ON category.cat_name = tbl_temporder.category WHERE (tbl_temporder.order_id = '" + OrdId[k] + "')", db.cn);
            SqlCommand cmdowner = new SqlCommand("select DISTINCT cat.printerName from sales_item si inner join menu m on si.menu_id=m.menu_id inner join category cat on m.category=cat.cat_name and si.order_id='" + label_orderID.Text + "'", db.cn);

            SqlDataReader rdow = cmdowner.ExecuteReader();
            while (rdow.Read())
            {
                printNm[count] = rdow["printerName"].ToString();
                count++;
            }
            db.cnclose();
            string[] arr = printNm.Distinct().ToArray();
            arr = arr.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            for (int i = 0; i < count; i++)
            {
                // string qryCheckCategoryOther = "SELECT tbl_temporder.menuid, tbl_temporder.qty, tbl_temporder.m_name, tbl_temporder.w_id, tbl_temporder.order_id, tbl_temporder.category , tbl_temporder.ordersuggestion FROM category INNER JOIN  tbl_temporder ON category.cat_name = tbl_temporder.category where printerName='" + arr[i] + "' and order_id = '" + OrdId[k] + "' ";
               // string qryCheckCategoryOther = "SELECT tbl_temporder.menuid, tbl_temporder.qty, tbl_temporder.m_name, tbl_temporder.w_id, tbl_temporder.order_id, tbl_temporder.category , tbl_temporder.ordersuggestion FROM category INNER JOIN  tbl_temporder ON category.cat_name = tbl_temporder.category where printerName='" + arr[i] + "' and order_id = '" + label_orderID.Text + "' ";
                string qryCheckCategoryOther = @"SELECT tbl_temporder.menuid, tbl_temporder.qty, tbl_temporder.m_name, tbl_temporder.w_id, tbl_temporder.order_id, 
tbl_temporder.category , tbl_temporder.ordersuggestion as order_sugg
 FROM category INNER JOIN  tbl_temporder ON category.cat_name = tbl_temporder.category 
where printerName='" + arr[i] + "' and order_id = '" + label_orderID.Text + "' ";
                
                
                lp.billid = float.Parse(label_orderID.Text);
                lp.tableno = (_tableId);
                lp.wname = cmbWaiterName1.Text;
                if (db.ChkDb_Value(qryCheckCategoryOther))
                {
                    lp.printOrder_kot(qryCheckCategoryOther, arr[i],null);
                }
            }

        }



        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            listView_items.Items.Clear();
            string qry = "select m_name from menu where m_name like '%" + txt_search.Text + "%'";

            SqlCommand cmd = new SqlCommand(qry, db.cn);
            db.cnopen();
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                item = rd["m_name"].ToString();

                listView_items.Items.Add(item);

            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Menu _addMenu = new Menu();
            _addMenu.ShowDialog();
            bind_itemsList();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int k = DGV_tblOrder.SelectedCells[0].RowIndex;
            DGV_tblOrder.Rows[k].Cells[2].Value = "";
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pnlCalc.Visible = false;
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            int k = DGV_tblOrder.SelectedCells[0].RowIndex;

            DGV_tblOrder.Rows[k].Cells[2].Value += "1";
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            int k = DGV_tblOrder.SelectedCells[0].RowIndex;

            DGV_tblOrder.Rows[k].Cells[2].Value += "2";
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            int k = DGV_tblOrder.SelectedCells[0].RowIndex;

            DGV_tblOrder.Rows[k].Cells[2].Value += "3";
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            int k = DGV_tblOrder.SelectedCells[0].RowIndex;

            DGV_tblOrder.Rows[k].Cells[2].Value += "4";
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            int k = DGV_tblOrder.SelectedCells[0].RowIndex;

            DGV_tblOrder.Rows[k].Cells[2].Value += "5";
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            int k = DGV_tblOrder.SelectedCells[0].RowIndex;

            DGV_tblOrder.Rows[k].Cells[2].Value += "6";
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            int k = DGV_tblOrder.SelectedCells[0].RowIndex;

            DGV_tblOrder.Rows[k].Cells[2].Value += "7";
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            int k = DGV_tblOrder.SelectedCells[0].RowIndex;

            DGV_tblOrder.Rows[k].Cells[2].Value += "8";
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            int k = DGV_tblOrder.SelectedCells[0].RowIndex;

            DGV_tblOrder.Rows[k].Cells[2].Value += "9";
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            int k = DGV_tblOrder.SelectedCells[0].RowIndex;

            DGV_tblOrder.Rows[k].Cells[2].Value += "0";
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < DGV_tblOrder.Rows.Count; i++)
            {               

                if (DGV_tblOrder.Rows[i].Cells[2].Value.ToString() == "")
                {
                    MessageBox.Show("cell is Empty");
                    return;
                }
                if (DGV_tblOrder.Rows[i].Cells[2].Value == "0")
                {
                    MessageBox.Show("Please Enter the Qty.");
                    return;
                }
                if (DGV_tblOrder.Rows[i].Cells[2].Value != "" || DGV_tblOrder.Rows[i].Cells[2].Value != "0")
                {
                    int get_qty;
                    get_qty = int.Parse(DGV_tblOrder.Rows[i].Cells[2].Value.ToString());

                }
            }

            pnlCalc.Visible = false;
        }

        private void DGV_tblOrder_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int get_qty = 0;
            int j = DGV_tblOrder.SelectedCells[0].RowIndex;
           
            if (DGV_tblOrder.Rows[j].Cells[2].Value.ToString() == "" || DGV_tblOrder.Rows[j].Cells[2].Value.ToString() == "0")
            {
                MessageBox.Show("You Can't Insert Empty Or ZERO Qty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                get_qty = 1;
                DGV_tblOrder.Rows[j].Cells[2].Value = get_qty;               

            }
            
        }

        private void DGV_tblOrder_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
            if (DGV_tblOrder.CurrentCell.ColumnIndex == 2 ) //Desired Column
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

        private void label_tbalNo2_Click(object sender, EventArgs e)
        {

        }

        private void txt_itemCode_TextChanged(object sender, EventArgs e)
        {
            listView_items.Items.Clear();
            string qry = "select m_name from menu where item_code = '" + txt_itemCode.Text + "'";

            SqlCommand cmd = new SqlCommand(qry, db.cn);
            db.cnopen();
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                item = rd["m_name"].ToString();

                listView_items.Items.Add(item);

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbWaiterName1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combowaiterN.Text == "select")
            {
                combowaiterN.Focus();
               // MessageBox.Show("Select Waiter");
            }

        }

        private void combowaiterN_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbWaiterName1.Text == "select")
            {
                cmbWaiterName1.Focus();
               // MessageBox.Show("Select Captain");
            }
        }
    }
}
