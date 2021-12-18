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
    public partial class ItemNameWise_Order : Form
    {
        private TextBox focusedTextbox = null;

        public bool orderConfirm = true;
        string _orderId, _tableId, captainID;
        float o_id;
        public int dgvSelectedRowIndex = 0;
        Database db = new Database();
        string table_status_1 = "";
        Welcome _welcome;
        bool flag_order = false, flagQty = false, cmbFlag = false;
        string menuid, qty;
        float item_rate, itemTotal;
        bool pageload = false;
        public bool welcomeFlag = false, shortNameFlag = false, dgvFlag = true, waiterNameFlag = false, dvgWaiterFlag = true, itemCodeDgvFlag = true;

        public ItemNameWise_Order()
        {
            InitializeComponent();
        }
         public ItemNameWise_Order(Welcome welcome)
        {
            flag_order = true;
            InitializeComponent();
           this._welcome = welcome;
            
        }

         public ItemNameWise_Order(string orderID, string tableID)
        {
            InitializeComponent();
            this._orderId = orderID;
            this._tableId = tableID;
           
        }
         void textBox_Enter(object sender, EventArgs e)
         {

             focusedTextbox = (TextBox)sender;
         }
        private void ItemNameWise_Order_Load(object sender, EventArgs e)
        {
            db.formFix(this);
            this.CancelButton = btnclose;
            label_orderID.Text = _orderId;
            label_tableNo.Text = _tableId;
            txt_table_no.Text = _tableId;

            if (db.ChkDb_Value("select * from table_status where t_id='" + txt_table_no.Text + "' and status!='Empty'"))
            {
                txtWaiterName.Text = db.getDbstatus_Value("SELECT        waiter_dtls.wname FROM            table_order INNER JOIN   waiter_dtls ON table_order.w_id = waiter_dtls.w_id where t_id='" + txt_table_no.Text + "' and order_id='" + label_orderID.Text + "'");
                txtWaiterName.Enabled = false;
            }

            else
            {
                // db.comboFill(cmbWaiterName, "Select wname from  waiter_dtls", "waiter_dtls", "wname", "wname");
                txtWaiterName.Enabled = true;

                txtWaiterName.Text = "";
            }

            //db.comboFill(cmbShortName, "SELECT      distinct(ConcateName) FROM            menu", "menu", "ConcateName", "ConcateName");
            //cmbShortName.Text = "";

            //db.comboFill(txt_item_code, "select * from menu ", "menu", "m_name", "m_name");

            //txt_item_code.Text = "";



            //get the txtbox name which hv focued assign the enter method
            foreach (TextBox tb in this.Controls.OfType<TextBox>())
            {
                tb.Enter += textBox_Enter;
            }
            //bindListViewWaiterName();
            //  listViewWaiterName.Items[0].Selected = true;
            //  db.comboFill(cmbWaiterName1, "select wname from  waiter_dtls where work_type='Captain' order by wname", "waiter_dtls", "wname", "wname");

            //welcome page focuse from 
            pageload = true;
            //DGV_tblOrder.Columns[0].Width = 80;
            //DGV_tblOrder.Columns[1].Width = 200;
            //DGV_tblOrder.Columns[2].Width = 80;
            //txt_item_code.Focus();
            txtWaiterName.Focus();
            cmbFlag = true;
        }

        private void btnDoneOrder_Click(object sender, EventArgs e)
        {
            if (txtWaiterName.Text != "")
            {
                try
                {
                    addTableOrder();
                    kotPrintFunction();
                }
                catch (Exception exp1)
                {
                    MessageBox.Show("Kindly Re-check Order");
                }
            }
            else
            {
                MessageBox.Show("Please select Waiter Name", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtWaiterName.Focus();
            }
        }
        void kotPrintFunction()
        {
            //ask for printing kot  // the db values for the kot 
            if (db.getDbstatus_Value("select status from tbl_option where grp='" + "KotPrint" + "'").ToString() == "Yes")
            {
                string m_name = "";
                LPrinter lp = new LPrinter();
                lp.billid = float.Parse(_orderId);
                lp.tableno = (_tableId);
                lp.wname = txtWaiterName.Text;
                int drinkcount = 0;
                string[] drinkarry = new string[50];
                int rowcount = DGV_tblOrder.RowCount;
                for (int i = 0; i < DGV_tblOrder.RowCount; i++)
                {
                    m_name = DGV_tblOrder.Rows[i].Cells[1].Value.ToString();
                    // ******* here check kot print status NO of menu and remove that menu 
                    if (db.ChkDb_Value("SELECT        menu.category FROM            menu INNER JOIN      category ON menu.category = category.cat_name WHERE menu.m_name='" + m_name + "' and      (category.kotStatus = 'No')"))
                    {
                        //  drinkarry[drinkcount] = m_name;
                        DGV_tblOrder.Rows.RemoveAt(i);
                        drinkcount++;
                        i--;
                    }

                }
                if (rowcount != 0)
                    lp.print_kot(DGV_tblOrder);
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

                db.update("UPDATE table_status SET status='" + table_status_1 + "' WHERE t_id='" + txt_table_no.Text + "'");

            }



            if (txtWaiterName.Text != "")
                captainID = db.getDb_Value("select w_id from waiter_dtls where wname='" + txtWaiterName.Text + "'").ToString();
            else
                captainID = "0";

            int KOT_id = int.Parse(db.getDb_Value("select max(kot_id) from sales_item").ToString()) + 1; //get the kot from the tbl


            //DialogResult dlgresult = MessageBox.Show("Are You Sure want to Complete Table Order?", "Confirm Complete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            // if (dlgresult == DialogResult.Yes)
            if (true)
            {
                for (int i = 0; i <= DGV_tblOrder.RowCount - 1; i++)
                {
                    menuid = db.getDb_Value("select menu_id from menu where m_name='" + DGV_tblOrder.Rows[i].Cells[1].Value.ToString() + "' ").ToString(); //get manu id
                    qty = DGV_tblOrder.Rows[i].Cells[2].Value.ToString(); // get the menu qty from  the DGV

                    //get the item rate according to the table type
                    if (db.ChkDb_Value("select * from table_status where table_type='" + "A/C" + "'and t_id='" + txt_table_no.Text + "'"))
                        item_rate = db.getDb_Value("select rate from menu where m_name='" + DGV_tblOrder.Rows[i].Cells[1].Value.ToString() + "' "); // get  the item rate from the tbl
                    else
                        item_rate = db.getDb_Value("select non_ACrate from menu where m_name='" + DGV_tblOrder.Rows[i].Cells[1].Value.ToString() + "' "); // get  the item rate from the tbl


                    itemTotal = item_rate * int.Parse(qty); // calculate the items toatal

                    //insert the vlaues into the table for wach item
                    if (db.ChkDb_Value("select * from sales_item where order_id='" + label_orderID.Text + "' and menu_id='" + menuid + "'"))
                    {
                        float old_qty = db.getDb_Value("select qty from sales_item where order_id='" + label_orderID.Text + "' and menu_id='" + menuid + "'");
                        int r = DGV_tblOrder.SelectedCells[0].RowIndex;
                        float new_qty = float.Parse(DGV_tblOrder.Rows[r].Cells[2].Value.ToString());
                        float tot_qty = old_qty + new_qty;
                        float total = item_rate * tot_qty;
                        db.update("update sales_item set qty='" + tot_qty + "',total_amount='" + total + "' where order_id='" + label_orderID.Text + "' and menu_id='" + menuid + "'");
                    }
                    //end code
                    else
                    {
                        db.insert("INSERT INTO sales_item(order_id,menu_id,qty,rate,total_amount,w_id,kot_id) VALUES('" + label_orderID.Text + "','" + menuid + "','" + qty + "','" + item_rate + "','" + itemTotal + "','" + captainID + "','" + KOT_id + "')");
                    }
                }


                //insert table_order id
                //if (flag_order)
                //{
                if (!db.ChkDb_Value("select * from table_order where order_id='" + label_orderID.Text + "'"))
                    db.insert("insert into table_order (order_id,t_id,timeing,w_id,order_type) values('" + label_orderID.Text + "','" + txt_table_no.Text + "','" + "0" + "'," + captainID + ",'" + "Novat" + "') ");
                else
                    db.update("update table_order set w_id='" + captainID + "' where order_id='" + label_orderID.Text + "' and t_id='" + txt_table_no.Text + "'");
                //}

                //add the suggestion for the order / remark

                db.insert("INSERT INTO tbl_orderSuggestion VALUES('" + KOT_id + "','" + textBox_custSugggestion.Text + "')");
                int k = DGV_tblOrder.SelectedCells[0].RowIndex;
                string item_nm = DGV_tblOrder.Rows[k].Cells[1].Value.ToString();
                if (db.ChkDb_Value("select qty from tbl_stock where item_name='" + item_nm + "'"))
                {
                    float old_qty = db.getDb_Value("select qty from tbl_stock where item_name='" + item_nm + "'");
                    float new_qty = float.Parse(DGV_tblOrder.Rows[k].Cells[2].Value.ToString());
                    float Quantity = old_qty - new_qty;
                    db.update("update tbl_stock set qty='" + Quantity + "' where item_name='" + item_nm + "'");
                }

                if (!welcomeFlag)
                {

                    // Welcome wel = new Welcome();
                    //wel.welcome_pageload(txt_table_no.Text);
                    //wel.bind(null);

                    orderConfirm = true;
                    //_welcome.welcome_pageload();
                    //_welcome.welcome_pageload(txt_table_no.Text);
                    //_welcome.bind(null);

                    ((Welcome)this.Owner).bind(null);
                }

                else
                    ((Welcome)this.Owner).bind(null);

                this.Close();
            }

        }
        void get_waiter_id()
        {
            int flag = 0;
            string qur = "SELECT * FROM  waiter_dtls where w_id=(select top 1 w_id from  sales_item where order_id='" + label_orderID.Text + "')";
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
                    txtWaiterName.Text = rd["wname"].ToString();

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
                txtWaiterName.Enabled = true;
            else
                txtWaiterName.Enabled = true;

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            DialogResult dlgresult = MessageBox.Show("Are You Sure want to Clear Table Order?", "Confirm Clear", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlgresult == DialogResult.Yes)
            {
                DGV_tblOrder.Rows.Clear();
            }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            if (label_orderID.Text != "")
            {
                if (!db.ChkDb_Value("select * from sales_item WHERE order_id='" + label_orderID.Text + "'"))
                {
                    db.update("UPDATE table_status SET status='Empty' WHERE t_id='" + txt_table_no.Text + "'");
                    db.DeleteData("delete from  table_order  where order_id='" + label_orderID.Text + "'", "table_order");
                }
            }
            orderConfirm = false;
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            menuAdd();
        }

        void menuAdd()
        {
            string name = "";
            int flag = 0, get_qty = 0, qty = 1;

            if (txt_qty.Text == "" || txt_qty.Text == "0")
            {
                MessageBox.Show("Don't insert Qty Blank  Or  0", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_qty.Focus();
            }
            else
            {
                if (db.ChkDb_Value("select m_name from menu where m_name='" + txtItemName.Text + "'"))
                {

                    //string qry = db.getDbstatus_Value("select m_name from menu where item_code='" + txt_item_code.Text + "'");
                    //name = qry.ToString();
                    name = txtItemName.Text;
                    for (int i = 0; i <= DGV_tblOrder.RowCount - 1; i++)
                    {
                        if (DGV_tblOrder.Rows[i].Cells[1].Value.ToString() == name)
                        {
                            get_qty = int.Parse(DGV_tblOrder.Rows[i].Cells[2].Value.ToString());
                            qty = get_qty + int.Parse(txt_qty.Text); // get old qty and add new qty
                            flag++; //for the alray exists

                            DGV_tblOrder.Rows[i].Cells[2].Value = qty.ToString(); //add new qty into dgv
                        }
                    }
                    if (flag == 0)
                    {
                        string[] arr = { null, name, txt_qty.Text };
                        DGV_tblOrder.Rows.Add(arr);
                    }
                }

                labelItemsCount.Text = DGV_tblOrder.RowCount.ToString();
                txtItemName.Text = "";
                txt_qty.Text = "";
                txtItemName.Focus();
            }

        }

        private void DGV_tblOrder_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = DGV_tblOrder.SelectedCells[0].RowIndex;

            if (DGV_tblOrder.Rows[i].Cells[2].Selected == true)
                DGV_tblOrder.Rows[i].Cells[2].Value = "";


            if (e.ColumnIndex == DGV_tblOrder.Columns[0].Index)
            {
                foreach (DataGridViewRow item in this.DGV_tblOrder.SelectedRows)
                {
                    DGV_tblOrder.Rows.RemoveAt(item.Index);
                }
                // txt_item_code.Focus();
                txtItemName.Focus();
            }
        }

        private void txt_table_no_TextChanged(object sender, EventArgs e)
        {
            if (txt_table_no.Text == "")
            {
                label_orderID.Text = "";
            }
        }
        void getStatus()
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

                   
                }

            }
        }

        private void txt_qty_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txt_qty.Text != "")
            {
                //  txt_qty.Text = db.delLastChar(txt_qty.Text);

                menuAdd();
                txtShortName.Text = "";
                txtItemName.Text = "";
                txtShortName.Focus();
                dgvAutoSuggestionBox.Visible = false;
                dgvItemNameAutoSuggest.Visible = false;
            }
        }

        private void txt_table_no_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Add)
            {
                //cmbWaiterName1.Focus();
                txt_table_no.Text = db.delLastChar(txt_table_no.Text);
                getStatus();
                // txt_item_code.Focus();
                txtItemName.Focus();
            }
        }

        private void txtShortName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtItemName.Text != "") // 1st enter for focus goto qty
            {
                txt_qty.Focus();
            }

            if (e.KeyCode == Keys.Enter && txtItemName.Text == "") // final orders
            {
                if (txtWaiterName.Text != "")
                {
                    try
                    {
                        addTableOrder();
                        kotPrintFunction();
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show("Kindly Re-check Order. ");
                    }
                }
                else
                {
                    MessageBox.Show("Please select Waiter Name", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtWaiterName.Focus();
                }
            }
        }

        private void txt_qty_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);
        }

        private void dgvAutoSuggestionBox_KeyUp(object sender, KeyEventArgs e)
        {
            int i = dgvAutoSuggestionBox.SelectedCells[0].RowIndex;
            dgvSelectedRowIndex = i;
            if (e.KeyCode == Keys.Enter)
            {
                if (i > 0)
                {
                    dgvAutoSuggestionBox.ClearSelection();
                    txtShortName.Text = dgvAutoSuggestionBox.Rows[i - 1].Cells[0].Value.ToString();

                    dgvAutoSuggestionBox.Visible = false;
                    dgvAutoSuggestionBox.DataSource = null;
                    // db.comboFill(txt_item_code, "select m_name from menu where ConcateName='" + txtShortName.Text + "'", "menu", "m_name", "m_name");
                    txtItemName.Text = db.getDbstatus_Value("select m_name from menu where ConcateName='" + txtShortName.Text + "'").ToString();
                    txt_qty.Focus();
                    dgvFlag = true;
                }
            }
        }

        private void txtShortName_TextChanged(object sender, EventArgs e)
        {
            if (dgvFlag || txtShortName.Text == "")
                shortNameFlag = true;
            if (txtShortName.Text != "" && shortNameFlag)
            {
                dgvAutoSuggestionBox.Visible = true;
                dgvAutoSuggestionBox.DataSource = db.Displaygrid("select ConcateName from menu where ConcateName like '" + txtShortName.Text + "%' ");
                dgvAutoSuggestionBox.Columns[0].Width = 380;
                dgvFlag = false;
                dgvAutoSuggestionBox.ClearSelection();
            }
        }

        private void txtShortName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)
            {
                dgvAutoSuggestionBox.Focus();
                //if (dgvFlag)
                dgvAutoSuggestionBox.Rows[0].Cells[0].Selected = true;
                //else
                //    dgvAutoSuggestionBox.Rows[dgvSelectedRowIndex].Cells[0].Selected = true;


            }
            if (e.KeyCode == Keys.Enter && txtItemName.Text != "") // 1st enter for focus goto qty
            {
                txt_qty.Focus();
            }

            if (e.KeyCode == Keys.Enter && txtItemName.Text == "") // final orders
            {
                if (txtWaiterName.Text != "")
                {
                    try
                    {
                        if (DGV_tblOrder.RowCount > 0)
                        {
                            addTableOrder();
                            kotPrintFunction();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Kindly Re-check Order. ");
                    }
                }
                else
                {
                    MessageBox.Show("Please select Waiter Name", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtWaiterName.Focus();
                }
            }
        }

        private void txtWaiterName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)
            {
                dgvWaiterAutoSuggest.Focus();
                dgvWaiterAutoSuggest.Rows[0].Cells[0].Selected = true;


            }
            if (e.KeyCode == Keys.Enter && txtWaiterName.Text != "")
            {

                txtWaiterName.Text = txtWaiterName.SelectedText;
                captainID = db.getDb_Value("select w_id from waiter_dtls where wname='" + txtWaiterName.SelectedText + "'").ToString();

                txtShortName.Focus();

            }
        }

        private void txtWaiterName_TextChanged(object sender, EventArgs e)
        {
            if (dgvFlag || txtWaiterName.Text == "")
                shortNameFlag = true;
            if (txtWaiterName.Text != "" && shortNameFlag)
            {
                dgvWaiterAutoSuggest.Visible = true;
                txtShortName.Visible = false;
                dgvWaiterAutoSuggest.DataSource = db.Displaygrid("select wname from waiter_dtls where wname like '" + txtWaiterName.Text + "%' ");
                dgvWaiterAutoSuggest.Columns[0].Width = 380;
                dgvFlag = false;
                dgvWaiterAutoSuggest.ClearSelection();
            }
        }

        private void dgvWaiterAutoSuggest_KeyUp(object sender, KeyEventArgs e)
        {
            int i = dgvWaiterAutoSuggest.SelectedCells[0].RowIndex;
            dgvSelectedRowIndex = i;
            if (e.KeyCode == Keys.Enter)
            {
                if (i > 0)
                {
                    dgvWaiterAutoSuggest.ClearSelection();
                    txtWaiterName.Text = dgvWaiterAutoSuggest.Rows[i - 1].Cells[0].Value.ToString();

                    dgvWaiterAutoSuggest.Visible = false;
                    txtShortName.Visible = true;

                    dgvWaiterAutoSuggest.DataSource = null;
                    txtShortName.Focus();
                    dgvFlag = true;
                }
            }
        }

        private void dgvItemNameAutoSuggest_KeyUp(object sender, KeyEventArgs e)
        {
            int i = dgvItemNameAutoSuggest.SelectedCells[0].RowIndex;
            dgvSelectedRowIndex = i;
            if (e.KeyCode == Keys.Enter)
            {
                if (i > 0)
                {
                    dgvItemNameAutoSuggest.ClearSelection();
                    //txt_item_code.Text = dgvItemNameAutoSuggest.Rows[i - 1].Cells[0].Value.ToString();
                    txtItemName.Text = dgvItemNameAutoSuggest.Rows[i - 1].Cells[0].Value.ToString();

                    dgvItemNameAutoSuggest.Visible = false;
                    //  txtShortName.Visible = true;

                    dgvItemNameAutoSuggest.DataSource = null;
                    txt_qty.Focus();
                    // dgvFlag = true;
                    itemCodeDgvFlag = true;
                }
            }
        }

        private void txtItemName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)
            {
                dgvItemNameAutoSuggest.Focus();
                dgvItemNameAutoSuggest.Rows[0].Cells[0].Selected = true;


            }

            if (e.KeyCode == Keys.Enter && txtItemName.Text != "") // 1st enter for focus goto qty
            {
                txt_qty.Focus();
            }

            if (e.KeyCode == Keys.Enter && txtItemName.Text == "") // final orders
            {
                if (txtWaiterName.Text != "")
                {
                    try
                    {
                        addTableOrder();
                        kotPrintFunction();
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show("Kindly Re-check Order. ");
                    }
                }
                else
                {
                    MessageBox.Show("Please select Waiter Name", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtWaiterName.Focus();
                }
            }
        }

        private void txtItemName_TextChanged(object sender, EventArgs e)
        {
            if (pageload)
            {
                if (itemCodeDgvFlag || txtItemName.Text == "")
                    shortNameFlag = true;
                if (txtItemName.Text != "" && shortNameFlag)
                {
                    dgvItemNameAutoSuggest.Visible = true;
                    dgvItemNameAutoSuggest.DataSource = db.Displaygrid("select m_name from menu where m_name like '" + txtItemName.Text + "%'"); //
                    dgvItemNameAutoSuggest.Columns[0].Width = 380;
                    //  dgvFlag = false;
                    itemCodeDgvFlag = false;

                    dgvItemNameAutoSuggest.ClearSelection();
                }
            }
        }

    }
}
