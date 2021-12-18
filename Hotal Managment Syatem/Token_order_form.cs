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

namespace Hotal_Managment_Syatem
{
    public partial class Token_order_form : Form
    {
        string _orderId, _tableId, captainID;
        float o_id;
        Database db = new Database();
        string table_status_1 = "";
        Welcome _welcome;
        bool flag_order = false, flagItemName = false, flgOrd = false;
        string menuid, qty;
        float item_rate, itemTotal;
        public bool orderConfirm = true, welcomeFlag = false;

        public Token_order_form(bool ord)
        {
            InitializeComponent();
            this.flgOrd = ord;
        }
        public Token_order_form(Welcome welcome)
        {
            flag_order = true;

            InitializeComponent();
            _welcome = welcome;
        }
        public Token_order_form(string orderID, string tableID)
        {
            InitializeComponent();
            flag_order = true;
            this._orderId = orderID;
            this._tableId = tableID;
            welcomeFlag = true;
            txt_table_no.Focus();

        }
        private void Token_order_form_Load(object sender, EventArgs e)
        {

            if (welcomeFlag)
            {
                txt_table_no.Focus();
                //txt_item_code.Focus();

            }

            label_orderID.Text = _orderId;
            label_tableNo.Text = _tableId;
            txt_table_no.Text = _tableId;
            if (_tableId == "")
            {
                txt_table_no.Focus();
            }
            db.comboFill(cmbItemName, "select * from  menu order by m_name asc", "menu", "m_name", "m_name");
            cmbItemName.Text = "";

            waiters();
            flagItemName = true;
        }


        void waiters()
        {
            //bind captn list 
            db.comboFill(cmbWaiterName1, "select wname from  waiter_dtls where work_type='Captain'", "waiter_dtls", "wname", "wname");
            //waiter /////----Yogesh 20.02.2019
            db.comboFill(combowaiter, "select wname from  waiter_dtls where work_type='Waiter'", "waiter_dtls", "wname", "wname");


            if (db.ChkDb_Value("select status from tbl_option where grp='" + "waiterDetails" + "' and status='Yes'"))
            {
                cmbWaiterName1.Text = "select";

                combowaiter.Text = "select";

            }

           


                      
        }


        private void button2_Click(object sender, EventArgs e)
        {


            if (cmbWaiterName1.Text != "select" && combowaiter.Text != "select")
            {

                                addTableOrder();

            }
            else
            {
                    MessageBox.Show("Please select the Captain , Waiter Name.....!");
             }
            



        }
        void addTableOrder()
        {


            //code  by harsha

            table_status_1 = "Processing";
            if (!db.ChkDb_Value("select * from table_status where status='Printing' and t_id='" + txt_table_no.Text + "' "))
            {
                if (table_status_1 != "Empty")
                    table_status_1 = "Processing";
                else
                    table_status_1 = "Empty";
                //update table status

                //db.update("UPDATE table_status SET status='" + table_status_1 + "' WHERE t_id='" + txt_table_no.Text + "'");
                //db.update("UPDATE table_order SET tableStatus='" + table_status_1 + "' WHERE t_id='" + txt_table_no.Text + "' and order_id='"+label_orderID.Text+"'");

            }
            string waiterName = combowaiter.Text;
            if (cmbWaiterName11.Text != "--Select--")
                captainID = db.getDb_Value("select w_id from waiter_dtls where wname='" + cmbWaiterName1.Text + "'").ToString();

            else
                captainID = "0";

            int KOT_id = int.Parse(db.getDb_Value("select max(kot_id) from sales_item").ToString()) + 1; //get the kot from the tbl

            if (label_orderID.Text != "0")
            {
                DialogResult dlgresult = MessageBox.Show("Are You Sure want to Complete Table Order?", "Confirm Complete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgresult == DialogResult.Yes)
                {
                    if (DGV_tblOrder.RowCount > 0)
                    {

                        for (int i = 0; i <= DGV_tblOrder.RowCount - 1; i++)
                        {
                            menuid = db.getDb_Value("select menu_id from menu where m_name='" + DGV_tblOrder.Rows[i].Cells[2].Value.ToString() + "' ").ToString(); //get manu id
                            qty = DGV_tblOrder.Rows[i].Cells[4].Value.ToString(); // get the menu qty from  the DGV

                            //get the item rate according to the table type
                            if (db.ChkDb_Value("select * from table_status where table_type='" + "A/C" + "'and t_id='" + txt_table_no.Text + "'"))
                                item_rate = db.getDb_Value("select rate from menu where m_name='" + DGV_tblOrder.Rows[i].Cells[2].Value.ToString() + "' "); // get  the item rate from the tbl
                            else if (db.ChkDb_Value("select * from table_status where table_type='" + "Non A/C" + "'and t_id='" + txt_table_no.Text + "'"))
                                item_rate = db.getDb_Value("select non_ACrate from menu where m_name='" + DGV_tblOrder.Rows[i].Cells[2].Value.ToString() + "' "); // get  the item rate from the tbl
                            else
                                item_rate = db.getDb_Value("select driverRate from menu where m_name='" + DGV_tblOrder.Rows[i].Cells[2].Value.ToString() + "' "); // get  the item rate from the tbl


                            itemTotal = item_rate * int.Parse(qty); // calculate the items toatal

                            //insert the vlaues into the table for wach item
                            if (db.ChkDb_Value("select * from sales_item where order_id='" + label_orderID.Text + "' and menu_id='" + menuid + "'"))
                            {
                                float old_qty = db.getDb_Value("select qty from sales_item where order_id='" + label_orderID.Text + "' and menu_id='" + menuid + "'");
                                // int r = DGV_tblOrder.SelectedCells[0].RowIndex;
                                float new_qty = float.Parse(DGV_tblOrder.Rows[i].Cells[4].Value.ToString());
                                float tot_qty = old_qty + new_qty;
                                float total = item_rate * tot_qty;
                                db.update("update sales_item set qty='" + tot_qty + "',total_amount='" + total + "' where order_id='" + label_orderID.Text + "' and menu_id='" + menuid + "'");
                            }
                            //end code
                            else
                            {
                                db.insert("INSERT INTO sales_item(order_id,menu_id,qty,rate,total_amount,w_id,kot_id) VALUES('" + label_orderID.Text + "','" + menuid + "','" + qty + "','" + item_rate + "','" + itemTotal + "','" + captainID + "','" + KOT_id + "')");
                            }

                            // code added for kot print
                            string menuCatgory = db.getDbstatus_Value(" select category from menu where menu_id='" + menuid + "'");
                            db.insert("insert into tbl_temporder values('" + menuid + "','" + qty + "','" + DGV_tblOrder.Rows[i].Cells[2].Value.ToString() + "','" + captainID + "','" + label_orderID.Text + "','" + menuCatgory + "','','" + KOT_id + "')  ");


                        }

                        db.update("UPDATE table_status SET status='" + table_status_1 + "' WHERE t_id='" + txt_table_no.Text + "'");
                        db.update("UPDATE table_order SET tableStatus='" + table_status_1 + "' WHERE t_id='" + txt_table_no.Text + "' and order_id='" + label_orderID.Text + "'");

                        //insert table_order id
                        if (flag_order)
                        {
                            if (!db.ChkDb_Value("select * from table_order where order_id='" + label_orderID.Text + "'"))
                                db.insert("insert into table_order (order_id,t_id,timeing,w_id,order_type,tableStatus,noOfGuest,Waiter) values('" + label_orderID.Text + "','" + txt_table_no.Text + "','" + System.DateTime.Now.ToString("MM/dd/yyyy") + "'," + captainID + ",'" + "Novat" + "','Processing','0','" + waiterName + "') ");
                            else

                                db.update("update table_order set w_id='" + captainID + "',Waiter='" + waiterName + "' where order_id='" + label_orderID.Text + "' and t_id='" + txt_table_no.Text + "'");
                        }

                        //add the suggestion for the order / remark

                        db.insert("INSERT INTO tbl_orderSuggestion VALUES('" + KOT_id + "','" + textBox_custSugggestion.Text + "')");
                        int k = DGV_tblOrder.SelectedCells[0].RowIndex;
                        string item_nm = DGV_tblOrder.Rows[k].Cells[2].Value.ToString();
                        if (db.ChkDb_Value("select qty from tbl_stock where item_name='" + item_nm + "'"))
                        {
                            float old_qty = db.getDb_Value("select qty from tbl_stock where item_name='" + item_nm + "'");
                            float new_qty = float.Parse(DGV_tblOrder.Rows[k].Cells[4].Value.ToString());
                            float Quantity = old_qty - new_qty;
                            db.update("update tbl_stock set qty='" + Quantity + "' where item_name='" + item_nm + "'");
                        }

                        //return to welcome page 
                        // ((Welcome)this.Owner).bind(null);


                        //update table status

                        //  db.update("UPDATE table_status SET status='" + table_status_1 + "' WHERE t_id='" + txt_table_no.Text + "'");

                        //*****************code hide by harsha

                        //ask for printing kot  // the db values for the kot 

                        if (db.getDbstatus_Value("select status from tbl_option where grp='" + "KotPrint" + "'").ToString() == "Yes")
                        {
                            string m_name = "";
                            LPrinter lp = new LPrinter();
                            lp.billid = float.Parse(label_orderID.Text);
                            lp.tableno = (txt_table_no.Text);
                            lp.wname = cmbWaiterName11.Text;
                            // lp.nameW = combowaiter.Text;
                            lp.tokenKot = true;
                            int drinkcount = 0;
                            string[] drinkarry = new string[50];
                            int rowcount = DGV_tblOrder.RowCount;
                            for (int i = 0; i < DGV_tblOrder.RowCount; i++)
                            {
                                m_name = DGV_tblOrder.Rows[i].Cells[1].Value.ToString();
                                // ******* here check kot print status NO of menu and remove that menu 
                                if (db.ChkDb_Value("SELECT  menu.category FROM menu INNER JOIN  category ON menu.category = category.cat_name WHERE menu.m_name='" + m_name + "' and      (category.kotprintStatus = 'No')"))
                                {
                                    //  drinkarry[drinkcount] = m_name;
                                    DGV_tblOrder.Rows.RemoveAt(i);
                                    drinkcount++;
                                    i--;
                                }

                            }
                            //if (rowcount != 0)
                            //    lp.print_kot(DGV_tblOrder);


                            //}

                            KOtPrintCode();
                           // KOtPrintCode("Bill Printer");

                            db.insert("delete from tbl_temporder where order_id='" + label_orderID.Text + "'");

                        }


                        // if (!db.ChkDb_Value("select * from table_order where order_id='" + label_orderID.Text + "'"))
                        if (!welcomeFlag)
                        {

                            //Welcome wel = new Welcome();
                            //wel.welcome_pageload(txt_table_no.Text);
                            //wel.bind(null);

                            orderConfirm = true;
                            _welcome.welcome_pageload(txt_table_no.Text);
                            _welcome.bind(null);
                            txt_table_no.Focus();
                        }

                        else
                            ((Welcome)this.Owner).bind(null);

                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Please Give Any Order");
                    }
                }
            }
            else
            {
                MessageBox.Show("Order Id Should not be Zero,Please check Order.");
            }

        }


        void KOtPrintCode()
        {

            LPrinter lp = new LPrinter();
            string[] printNm = new string[10];
            int count = 0;
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
                // lp.nameW = combowaiter.Text;


                
                if (db.ChkDb_Value(qryCheckCategoryOther))
                {
                    lp.printOrder_kot(qryCheckCategoryOther, arr[i], null);
                }


                //if (db.ChkDb_Value(qryCheckCategoryOther))
                //{
                ////    if (RePrintPrinter != "")
                ////    {
                ////        if (db.ChkDb_Value("select * from category where BillPrinter=2 and printerName='" + arr[i] + "'"))
                ////            lp.printOrder_kot(qryCheckCategoryOther, RePrintPrinter);
                ////    }
                ////    else
                //        lp.printOrder_kot(qryCheckCategoryOther, arr[i]);

                //}
            }

        }



        void get_waiter_id()
        {
            int flag = 0;
            string qur = "SELECT * FROM  waiter_dtls where w_id=(select top 1 w_id from  sales_item where order_id='" + label_orderID.Text + "')";

            //db.comboFill(combowaiter, "select CustomerMasterID,Cust_Name  from CustomerMaster", "CustomerMaster", "Cust_Name", "CustomerMasterID");
            db.comboFill(combowaiter1, "select wname from waiter_dtls where work_type='Waiter'", "waiter_dtls", "wname", "wname");


            //
            //SqlConnection con = new SqlConnection(constr);
            //con.Open();

            SqlCommand cmd = new SqlCommand(qur, db.cn);
            try
            {
                db.cnopen();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read() == true)
                {
                    // textBox_waiter_id.Text = rd["w_id"].ToString();
                    cmbWaiterName11.Text = rd["wname"].ToString();

                    flag = 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                db.cnclose();
            }

            if (flag != 0)
                cmbWaiterName11.Enabled = true;
            else
                cmbWaiterName11.Enabled = true;

        }
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dlgresult = MessageBox.Show("Are You Sure want to Clear Table Order?", "Confirm Clear", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlgresult == DialogResult.Yes)
            {
                DGV_tblOrder.Rows.Clear();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (label_orderID.Text != "")
            {
                if (!db.ChkDb_Value("select * from sales_item WHERE order_id='" + label_orderID.Text + "'"))
                {
                    db.update("UPDATE table_status SET status='Empty' WHERE t_id='" + txt_table_no.Text + "'");
                    db.DeleteData("delete from  table_order  where order_id='" + label_orderID.Text + "'", "table_order");
                }
            }

            this.Close();
            orderConfirm = false;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            menuAdd();

        }
        void menuAdd()
        {
            string name = "";//,Amount,quantity;
            int flag = 0, get_qty = 0, qty = 1, amt, rate;
            if (txt_table_no.Text == "")
            {
                MessageBox.Show("Please Enter Table No.");
                txt_table_no.Focus();

            }
            if (txt_item_code.Text == "")
            {
                MessageBox.Show("Please Enter Item Code.");
                txt_item_code.Focus();
            }
            else if (cmbItemName.Text == "")
            {
                MessageBox.Show("Please Enter Item Name.");
                cmbItemName.Focus();
            }
            else if (txt_qty.Text == "" || txt_qty.Text == "0")
            {
                MessageBox.Show("Don't insert Qty Blank  Or  0", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_qty.Focus();
                //txt_qty.GotFocus += delegate { txt_qty.Select(0, 0); };
                txt_qty.SelectionStart = 0;
                txt_qty.SelectionLength = txt_qty.Text.Length;

            }
            else
            {
                if (db.ChkDb_Value("select m_name from menu where item_code='" + txt_item_code.Text + "'"))
                {

                    //string qry = db.getDbstatus_Value("select m_name from menu where item_code='" + txt_item_code.Text + "'");
                    //name = qry.ToString();
                    name = cmbItemName.Text;
                    for (int i = 0; i <= DGV_tblOrder.RowCount - 1; i++)
                    {
                        if (DGV_tblOrder.Rows[i].Cells[2].Value.ToString() == name)
                        {
                            get_qty = int.Parse(DGV_tblOrder.Rows[i].Cells[4].Value.ToString());
                            qty = get_qty + int.Parse(txt_qty.Text); // get old qty and add new qty

                            // rate =int.Parse(db.getDb_Value("select rate from menu where m_name='"+name+"'").ToString());


                            //get the item rate according to the table type
                            if (db.ChkDb_Value("select * from table_status where table_type='" + "A/C" + "'and t_id='" + txt_table_no.Text + "'"))
                                rate = int.Parse(db.getDb_Value("select rate from menu where m_name='" + DGV_tblOrder.Rows[i].Cells[2].Value.ToString() + "' ").ToString()); // get  the item rate from the tbl
                            else if (db.ChkDb_Value("select * from table_status where table_type='" + "Non A/C" + "'and t_id='" + txt_table_no.Text + "'"))
                                rate = int.Parse(db.getDb_Value("select non_ACrate from menu where m_name='" + DGV_tblOrder.Rows[i].Cells[2].Value.ToString() + "' ").ToString()); // get  the item rate from the tbl
                            else
                                rate = int.Parse(db.getDb_Value("select driverRate from menu where m_name='" + DGV_tblOrder.Rows[i].Cells[2].Value.ToString() + "' ").ToString()); // get  the item rate from the tbl




                            amt = qty * (rate);
                            DGV_tblOrder.Rows[i].Cells[4].Value = qty.ToString(); //add new qty into dgv
                            DGV_tblOrder.Rows[i].Cells[5].Value = amt.ToString();
                            DGV_tblOrder.Rows[i].Cells[3].Value = rate.ToString();
                            flag++; //for the alray exists

                        }
                    }
                    if (flag == 0)
                    {

                        //get the item rate according to the table type
                        if (db.ChkDb_Value("select * from table_status where table_type='" + "A/C" + "'and t_id='" + txt_table_no.Text + "'"))
                            rate = int.Parse(db.getDb_Value("select rate from menu where m_name='" + name + "' ").ToString()); // get  the item rate from the tbl
                        else if (db.ChkDb_Value("select * from table_status where table_type='" + "Non A/C" + "'and t_id='" + txt_table_no.Text + "'"))
                            rate = int.Parse(db.getDb_Value("select non_ACrate from menu where m_name='" + name + "' ").ToString()); // get  the item rate from the tbl
                        else
                            rate = int.Parse(db.getDb_Value("select driverRate from menu where m_name='" + name + "' ").ToString()); // get  the item rate from the tbl


                        //rate = int.Parse(db.getDb_Value("select rate from menu where m_name='" + name + "'").ToString());

                        amt = rate * (int.Parse(txt_qty.Text));

                        //Amount = rate.ToString() * txt_qty.Text;


                        string[] arr = { null, txt_item_code.Text, name, rate.ToString(), txt_qty.Text, amt.ToString() };
                        DGV_tblOrder.Rows.Add(arr);
                    }
                }

                labelItemsCount.Text = DGV_tblOrder.RowCount.ToString();
                txt_item_code.Text = "";
                txt_qty.Text = "1";
                txt_item_code.Focus();
                //txt_table_no.Focus();
            }

        }


        private void DGV_tblOrder_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == DGV_tblOrder.Columns[0].Index)
            {
                foreach (DataGridViewRow item in this.DGV_tblOrder.SelectedRows)
                {
                    DGV_tblOrder.Rows.RemoveAt(item.Index);
                }
                txt_item_code.Focus();
                //txt_table_no.Focus();
                //  DGV_tblOrder.ClearSelection();
            }
        }

        private void txt_table_no_Leave(object sender, EventArgs e)
        {
        }

        private void txt_table_no_TextChanged(object sender, EventArgs e)
        {
            string dayendDt = db.getDbstatus_Value("Select ddate from tbl_dayend_status");
            string dt = Convert.ToDateTime(dayendDt).ToString("MM/dd/yyyy");
            txt_table_no.Focus();
            if (txt_table_no.Text == "")
            {
                label_orderID.Text = "";
            }
            if (!flgOrd)
            {
               

                    if (db.ChkDb_Value("select order_id from table_order where t_id='" + txt_table_no.Text + "' and tableStatus='Processing' and timeing='" + dt + "' "))
                    {
                        _orderId = db.getDbstatus_Value("select order_id from table_order where t_id='" + txt_table_no.Text + "' and tableStatus='Processing' and timeing='" + dt + "' ");

                        label_orderID.Text = _orderId;
                    }
               
            }
            //if (txt_table_no.Text == "")
            //{
            //    label_orderID.Text = "";
            //}
            // cmbWaiterName1.Focus();

        }

        void getStatus()
        {




            if (db.ChkDb_Value("select * from table_status where t_id='" + txt_table_no.Text + "'"))
            {

                if (txt_table_no.Text != "")
                {
                    if (_orderId == null)
                    {

                        if (db.ChkDb_Value("select * from table_status where status!='" + "Empty" + "' and t_id='" + txt_table_no.Text + "'"))
                        {
                            o_id = db.getDb_Value("select max(order_id) from table_order where t_id='" + txt_table_no.Text + "' ");
                        }
                        else
                        {
                            o_id = db.getDb_Value("select max(order_id)+1 from table_order");
                            if (o_id == 0)
                            {
                                o_id = 1;
                            }
                        }


                        label_orderID.Text = o_id.ToString();

                        if (db.ChkDb_Value("select * from table_order where order_id='" + label_orderID.Text + "'"))
                        {

                            cmbWaiterName1.Text = db.getDbstatus_Value(" select ISNULL((SELECT wname FROM  waiter_dtls where w_id=(select top 1 w_id from  sales_item where order_id='" + label_orderID.Text + "')),0)");


                            combowaiter.Text = db.getDbstatus_Value("SELECT ISNULL((SELECT Waiter FROM  table_order where order_id='" + label_orderID.Text + "'),0)");
                        }
                        else
                        {
                            waiters();
                        }
                        //get table status and assign



                        //}
                        //else
                        //{
                        //    table_status_1 = "Empty";
                        //}
                    }
                    //string dayendDt = db.getDbstatus_Value("Select ddate from tbl_dayend_status");
                    //string dt = Convert.ToDateTime(dayendDt).ToString("MM/dd/yyyy");

                    //if (db.ChkDb_Value("select order_id from table_order where t_id='" + txt_table_no.Text + "' and tableStatus='Processing' and timeing='" + dt + "' "))
                    //{

                    //    _orderId = db.getDbstatus_Value("select order_id from table_order where t_id='" + txt_table_no.Text + "' and tableStatus='Processing' and timeing='" + dt + "' ");
                    //}

                }

               
               



            }
            else
            {
                MessageBox.Show("Table No No Valid !");
                txt_table_no.Text = "";
            }

        }

        private void txt_item_code_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txt_item_code_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                if (txt_item_code.Text != "")
                {
                    if (db.ChkDb_Value("select m_name from menu where item_code='" + txt_item_code.Text + "'"))
                        cmbItemName.Text = db.getDbstatus_Value("select m_name from menu where item_code='" + txt_item_code.Text + "'");
                    else
                        cmbItemName.Text = "";

                    txt_qty.Focus();

                    //  txt_qty.GotFocus += delegate { txt_qty.Select(0, 0); }; // this is for clear selection
                    txt_qty.SelectionStart = 0;
                    txt_qty.SelectionLength = txt_qty.Text.Length; // this is for set focus last of textbox
                }
                else { txt_item_code.Focus(); }
                // else {txt_table_no.Focus(); }

            }
            if (e.KeyCode == Keys.Right)
                cmbItemName.Focus();

            if (txt_item_code.Text == "0" && e.KeyCode == Keys.Enter)//&& cmbItemName.Text == ""
            {
                //..............
                if (cmbWaiterName1.Text != "select" && combowaiter.Text != "select")
                {

                    addTableOrder();

                }
                else
                {
                    MessageBox.Show("Please select the Captain , Waiter Name.....!");
                }
            

                //addTableOrder();


                //..................

            }

            if (txt_item_code.Text == "" && txt_table_no.Text == "")
            {
                txt_table_no.Focus();
            }

        }

        private void txt_qty_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                //  txt_qty.Text = db.delLastChar(txt_qty.Text);
                menuAdd();
                cmbItemName.Text = "";
            }

            //if (e.KeyCode == Keys.Add)
            //{
            //    txt_qty.Text = db.delLastChar(txt_qty.Text);
            //    menuAdd();
            //  //  txt_item_code.Focus();
            //}
        }

        private void txt_table_no_KeyUp(object sender, KeyEventArgs e)
        {

            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                getStatus();
                txt_item_code.Focus();
                //txt_table_no.Focus();


            }

            //if (e.KeyCode == Keys.Add)
            //{
            //    //cmbWaiterName1.Focus();
            //    txt_table_no.Text = db.delLastChar(txt_table_no.Text);
            //    getStatus();
            //    cmbItemName.Focus();
            //   // txt_item_code.Focus();
            //}
        }

        private void cmbWaiterName1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //txt_item_code.Focus();
                txt_table_no.Focus();
            }
        }

        private void Token_order_form_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void txt_qty_TextChanged(object sender, EventArgs e)
        {
            txt_qty.SelectionStart = 0;
            txt_qty.SelectionLength = txt_qty.Text.Length;
        }

        private void cmbItemName_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                this.SelectNextControl((Control)sender, true, true, true, true); getStatus();
                txt_qty.SelectionStart = 0;
                txt_qty.SelectionLength = txt_qty.Text.Length;
                txt_qty.Focus();

                // txt_qty.GotFocus += delegate { txt_qty.Select(0, 0); }; // this is for clear selection
                // this is for set focus last of textbox

            }
            if (e.KeyCode == Keys.Left)
                txt_item_code.Focus();
            //txt_table_no.Focus();

            //if (e.KeyCode == Keys.Add)
            //{
            //    cmbItemName.Text = db.delLastChar(cmbItemName.Text);
            //    getStatus();
            //    txt_qty.Focus();
            //    txt_qty.GotFocus += delegate { txt_qty.Select(0, 0); }; // this is for clear selection
            //    txt_qty.SelectionStart = txt_qty.Text.Length; // this is for set focus last of textbox
            //}

        }

        private void cmbItemName_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cmbItemName.Text != "" && flagItemName)
            {
                txt_item_code.Text = db.getDb_Value("select item_code from menu where m_name='" + cmbItemName.Text + "'").ToString();

                txt_qty.Text = "1";
                txt_qty.SelectionStart = 0;
                txt_qty.SelectionLength = txt_qty.Text.Length;
                txt_qty.Focus();

                //  txt_qty.GotFocus += delegate { txt_qty.Select(0, 0); };

            }
        }

        private void DGV_tblOrder_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            float rate, qty, amt;
            int i = DGV_tblOrder.SelectedCells[0].RowIndex;
            rate = float.Parse(DGV_tblOrder.Rows[i].Cells[3].Value.ToString());
            qty = float.Parse(DGV_tblOrder.Rows[i].Cells[4].Value.ToString());
            amt = rate * qty;
            DGV_tblOrder.Rows[i].Cells[5].Value = amt.ToString();
        }

        private void txt_item_code_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);
        }

        private void txt_qty_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void cmbWaiterName1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txt_item_code_TextChanged(object sender, EventArgs e)
        {
            //cmbItemName.Focus();
        }

        private void label_orderID_Click(object sender, EventArgs e)
        {

        }

        private void combowaiter_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }

}

