 
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Diagnostics;

namespace Hotal_Managment_Syatem
{
	 
	public partial class Counter_Order_Form : Form
	{
		Database db=new Database();
		  string menu = "";
        string item; string selected_item;
        int dgvRow = 0;
             string _orderId, _tableId;
        string menuid, qty,getDate,dt;
        float item_rate, itemTotal;
        public bool orderConfirm=true;
         int count = 0,captainID;
         int locX = 20;
         int locY = 10;
         int sizeWidth = 200;
         int sizeHeight = 200;

         LPrinter lp = new LPrinter();
         string[] counterarray = new string[100];
         int ctr = 0;

		public Counter_Order_Form()
		{
			 
			InitializeComponent();
			
		 
		}
        public bool flag_tokenPrint = false;
        public double order_id = 0;
        public double view_order_id = 0;
    	void Counter_Order_Form_Load(object sender, EventArgs e)
		{
            if (db.getDbstatus_Value("select status from tbl_option  where grp='TokenKotPrint'") == "Yes")
                flag_tokenPrint = true;

            getDate = db.getDbstatus_Value("select ddate from tbl_dayend_status");
            label_date.Text = getDate;

            dt = Convert.ToDateTime(getDate).ToString("MM-dd-yyyy");

        //    string date = Convert.ToDateTime(getDate).ToString("MM'-'dd'-'yyyy");


             // bind_itemsList();
            db.comboFill(cmbWaiterName, "select wname from  waiter_dtls where work_type='Captain'", "waiter_dtls", "wname", "wname");
                      
            DGV_tblOrder.Columns[1].ReadOnly = true;
            DGV_tblOrder.Columns[3].ReadOnly = true;

            bind_itemsList();
            bindCat();

            captainID = int.Parse(db.getDb_Value("select w_id from waiter_dtls where wname='" + cmbWaiterName.Text + "'").ToString());

            KOT_id = int.Parse(db.getDb_Value("select max(kot_id) from sales_item").ToString()) + 1; //get the kot from the tbl

              order_id = db.GetUniqueId("Select top 1 order_id from table_order  order by  order_id desc");
            label_orderID.Text = (order_id + 1).ToString(); //assign the order id here         
            order_id++;
              view_order_id = db.getDb_Value("SELECT max(ISNULL(viewOrderId,0)) as MaxOrderId FROM  table_order where timeing= '" + dt.ToString() + "'");

            lbl_viewOrderID.Text = (view_order_id + 1).ToString(); //assign the order id here
            view_order_id++;
		}
        void bindCat()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("select * from category  WHERE        (type = 'Self Counter')", db.cn);
                db.cnopen();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read() == true)
                {
                    menu = rd["cat_name"].ToString();


                    listViewCounter.Items.Add(menu);

                }
                db.cnclose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public void counterOrderFormLoad()
        {
           //reset all the fields
            DGV_tblOrder.Rows.Clear();
            listViewCounter.Clear();
           
            labelItemsCount.Text = "0";
            lbl_total.Text = "0";
            //bindCat();
            //((DataGridViewTextBoxColumn)DGV_tblOrder.Columns["Qty"]).MaxInputLength = 3;
            //double order_id = db.GetUniqueId("Select top 1 order_id from table_order order by  order_id desc");
            //  double order_id = db.GetUniqueId("Select top 1 order_id from sales_item order by  order_id desc"); viewOrderId


           // double order_id = db.GetUniqueId("Select top 1 order_id from sales_item  order by  order_id desc"); 

            double order_id = db.GetUniqueId("Select top 1 order_id from table_order  order by  order_id desc"); 
           
            label_orderID.Text = (order_id+1).ToString(); //assign the order id here         
            
            
            double view_order_id = db.getDb_Value("SELECT max(ISNULL(viewOrderId,0)) as MaxOrderId FROM  table_order where timeing= '" + dt.ToString() + "'");

             lbl_viewOrderID.Text = (view_order_id + 1).ToString(); //assign the order id here

        


            try
            {
                
                //SqlCommand cmd = new SqlCommand("select * from category where type='Self Counter'", db.cn);
                //db.cnopen();
                //SqlDataReader rd = cmd.ExecuteReader();
                //while (rd.Read() == true)
                //{
                //    menu = rd["cat_name"].ToString();
                //    listViewCounter.Items.Add(menu);
                //}
                //db.cnclose();
               // bind_itemsList();


            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
		  void bind_itemsList()
        {
         

           
            //double order_id = db.GetUniqueId("Select top 1 order_id from sales_item  order by  order_id desc");
            double order_id = db.GetUniqueId("select isnull((Select top 1 order_id from table_order  order by  order_id desc),0)");
            label_orderID.Text = (order_id + 1).ToString();
              //assign the order id here  
            double view_order_id = db.getDb_Value("SELECT max(ISNULL(viewOrderId,0)) as MaxOrderId FROM  table_order where timeing= '" + dt.ToString() + "'");
           lbl_viewOrderID.Text = (view_order_id + 1).ToString(); //assign the order id here



            listView_items.Clear();

            //if (listViewCounter.SelectedItems.Count >= 1)
            //{
            //    selected_item = listViewCounter.SelectedItems[0].Text;

            //}

          //  string qry = "SELECT        menu.m_name FROM            category INNER JOIN     menu ON category.cat_name = menu.category WHERE        (category.type = 'Self Counter') and category= '" + selected_item + "'";
             string qry = "SELECT        menu.m_name FROM   category INNER JOIN     menu ON category.cat_name = menu.category order by driverRate asc ";

            SqlCommand cmd = new SqlCommand(qry, db.cn);
            db.cnopen();
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                item = rd["m_name"].ToString();

                listView_items.Items.Add(item);
            }
//**************** DISPLAY MENU IMAGES ****************************************
            //ImageList Imagelist = new ImageList();

            ////retrieve all image files
            //String[] ImageFiles = System.IO.Directory.GetFiles(@"D:\MenuImages");

            //foreach (var file in ImageFiles)
            //{
            //    //Add images to Imagelist
            //    Imagelist.Images.Add(Image.FromFile(file));
            //}
            ////set the small and large ImageList properties of listview
            //listView_items.View = View.LargeIcon;
            //Imagelist.ImageSize = new Size(100,100);
            //listView_items.LargeImageList = Imagelist;
           
            //for (int j = 0; j < Imagelist.Images.Count; j++)
            //{
            //    ListViewItem item = new ListViewItem();
            //    item.ImageIndex = j;
            //    listView_items.Items.Add(item);
            //}

            
        }
          private void loadImagestoPanel(String imageName, String ImageFullName, int newLocX, int newLocY)
          {
              PictureBox ctrl = new PictureBox();
              Label lbl = new Label();


              ctrl.Image = Image.FromFile(ImageFullName);
              lbl.Text = imageName.ToString();

              ctrl.Name = imageName.ToString();
              ctrl.BackColor = Color.Black;
              ctrl.Location = new Point(newLocX, newLocY);
              ctrl.Size = new System.Drawing.Size(sizeWidth, sizeHeight);
              ctrl.SizeMode = PictureBoxSizeMode.StretchImage;
              //ctrl.MouseMove += new MouseEventHandler(control_MouseMove);
         // ctrl.MouseMove += new MouseEventHandler(control_MouseMove);
             // ctrl.Click += new System.EventHandler(pictureBox1_Click);
              listView_items.Controls.Add(ctrl);
              listView_items.Controls.Add(lbl);


          }
		void listViewCounter_SelectedIndexChanged(object sender, EventArgs e)
		{
			bind_itemsList();
		}
		void listView_items_SelectedIndexChanged(object sender, EventArgs e)
		{
	       
		}

        void sumTotal()
        {
            double sum = 0;
            for (int i = 0; i <DGV_tblOrder.RowCount; i++)
            {
                sum +=double.Parse(DGV_tblOrder.Rows[i].Cells[3].Value.ToString());
            }
            lbl_total.Text = sum.ToString();
        }
        void button1_Click(object sender, EventArgs e)
		{
	         DialogResult dlgresult = MessageBox.Show("Are You Sure want to Clear Table Order?", "Confirm Clear", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlgresult == DialogResult.Yes)
            {
                DGV_tblOrder.Rows.Clear();
                lbl_total.Text = "0";
                labelItemsCount.Text = "0";
            }
		}
        void DGV_tblOrder_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          
            int i=DGV_tblOrder.SelectedCells[0].RowIndex;
            if (e.ColumnIndex == 2)
            {
                DGV_tblOrder.Rows[i].Cells[2].Value = "1";
                pnlcalc.Visible = true;
            }

            else
                pnlcalc.Visible = false;

            if (e.ColumnIndex == 0)
            {
                foreach (DataGridViewRow item in this.DGV_tblOrder.SelectedRows)
                {
                    DGV_tblOrder.Rows.RemoveAt(item.Index);
                }
                
                labelItemsCount.Text = DGV_tblOrder.RowCount.ToString(); //count the dgv rows / items
               // label_counterNo.Text = db.getDbstatus_Value("SELECT        category FROM            menu WHERE        (m_name = '" + seletedItemName + "')");


                sumTotal();
            }
            
            
        }
		void btn_close_Click(object sender, EventArgs e)
		{
          //  this.Hide();
            this.Close();
            //Welcome wel=new Welcome();
            //wel.ShowDialog();
		}
        public int KOT_id = 0;
       public  string get_menuItemRate = string.Empty;
       public string sale_itemQuery = string.Empty;
       void button2_Click(object sender, EventArgs e)
       {

           DialogResult dlgresultt = MessageBox.Show("Do You Want to Complete the order ?", "Confirm Complete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
           if (dlgresultt == DialogResult.Yes)
           {

               ctr = 0;

               // string qur = "SELECT w_id FROM  waiter_dtls where wname='" + cmbWaiterName.Text + "'";

               // captainID = 0;
               captainID = int.Parse(db.getDb_Value("select w_id from waiter_dtls where wname='" + cmbWaiterName.Text + "'").ToString());


               if (DGV_tblOrder.RowCount != 0)
               {

                   // string ddate = db.getDbstatus_Value("select ddate from tbl_dayend_status");
                   //   string date = Convert.ToDateTime(ddate).ToString("MM'-'dd'-'yyyy");

                   //assing waiter to the table 
                   //captainID = int.Parse(db.getDb_Value("select w_id from waiter_dtls where wname='" + cmbWaiterName.Text + "'").ToString());

                   //  KOT_id = int.Parse(db.getDb_Value("select max(kot_id) from sales_item").ToString()) + 1; //get the kot from the tbl
                   if (!db.ChkDb_Value("select * from table_order where order_id ='"+label_orderID.Text+"'"))
                   {
                       db.insert("insert into table_order (order_id ,viewOrderId, t_id,timeing,w_id,order_type,tableStatus,discValue,discAmt,gst) values('" + label_orderID.Text + "' ,'" + lbl_viewOrderID.Text + "','500','" + dt + "'," + captainID + ",'0','0','0','0','0') ");
                   }
                   else
                   {
                       db.insert("update  table_order  set viewOrderId ='" + lbl_viewOrderID.Text + "', t_id='500' ,timeing='" + dt + "',w_id=" + captainID + ",order_type ='0',tableStatus='0',discValue='0',discAmt='0',gst='0' ");
               
                   }
                   //get the token print

                   lp.billid = float.Parse(label_orderID.Text);
                  // lp.view_orderID = float.Parse(lbl_viewOrderID.Text);
                   lp.counterNo = (label_counterNo.Text);

                   count = 0;
                   sale_itemQuery = string.Empty;

                   for (int i = 0; i <= DGV_tblOrder.RowCount - 1; i++)
                   {

                       get_menuItemRate = db.getDbstatus_Value(" select  convert(varchar,menu_id) +'*'+convert(varchar,rate) from menu where m_name='" + DGV_tblOrder.Rows[i].Cells[1].Value.ToString() + "' ").ToString();

                       //menuid = db.getDb_Value("select menu_id from menu where m_name='" + DGV_tblOrder.Rows[i].Cells[1].Value.ToString() + "' ").ToString(); //get manu id
                       //item_rate = db.getDb_Value("select rate from menu where m_name='" + DGV_tblOrder.Rows[i].Cells[1].Value.ToString() + "' "); // get  the item rate from the tbl


                       menuid = get_menuItemRate.Split('*')[0].ToString();
                       item_rate = float.Parse(get_menuItemRate.Split('*')[1].ToString());

                       qty = DGV_tblOrder.Rows[i].Cells[2].Value.ToString(); // get the menu qty from  the DGV

                       itemTotal = item_rate * int.Parse(qty); // calculate the items toatal

                       //insert the vlaues into the table for wach item

                       //update existin menu by harshu.. start 
                       //if (db.ChkDb_Value("select * from sales_item where order_id='" + label_orderID.Text + "' and menu_id='" + menuid + "'"))
                       //{
                       //    float old_qty = db.getDb_Value("select qty from sales_item where order_id='" + label_orderID.Text + "' and menu_id='" + menuid + "'");
                       //    int r = DGV_tblOrder.SelectedCells[0].RowIndex;
                       //    float new_qty = float.Parse(DGV_tblOrder.Rows[r].Cells[2].Value.ToString());
                       //    float tot_qty = old_qty + new_qty;
                       //    float total = item_rate * tot_qty;
                       //    db.update("update sales_item set qty='" + tot_qty + "',total_amount='" + total + "' where order_id='" + label_orderID.Text + "' and menu_id='" + menuid + "' and Date='" + dt + "'");
                       //}
                       ////end code
                       //else
                       //{
                       //    db.insert("INSERT INTO sales_item(order_id,menu_id,qty,rate,total_amount,w_id,kot_id,Date) VALUES('" + label_orderID.Text + "','" + menuid + "','" + qty + "','" + item_rate + "','" + itemTotal + "','" + captainID + "','" + KOT_id + "','" + dt + "')");
                       //}

                       //    db.insert("INSERT INTO sales_item(order_id,menu_id,qty,rate,total_amount,w_id,kot_id,Date) VALUES('" + label_orderID.Text + "','" + menuid + "','" + qty + "','" + item_rate + "','" + itemTotal + "','" + captainID + "','" + KOT_id + "','" + dt + "')");

                       sale_itemQuery += " INSERT INTO sales_item(order_id,menu_id,qty,rate,total_amount,w_id,kot_id,Date) VALUES('" + label_orderID.Text + "','" + menuid + "','" + qty + "','" + item_rate + "','" + itemTotal + "','" + captainID + "','" + KOT_id + "','" + dt + "') ;";
                       orderConfirm = true;
                   }


                   db.insert(sale_itemQuery);


                   KOT_id++;  // updated new 

                   int k = DGV_tblOrder.SelectedCells[0].RowIndex;


                   string qur = " SELECT  menu.category FROM  menu INNER JOIN  sales_item ON menu.menu_id = sales_item.menu_id where sales_item.order_id='" + label_orderID.Text + "' GROUP BY menu.category";


                   using (SqlCommand command = new SqlCommand(qur, db.cn))
                   {
                       db.cnopen();
                       using (SqlDataReader reader = command.ExecuteReader())
                       {

                           while (reader.Read())
                           {
                               counterarray[ctr] = reader["category"].ToString();
                               ctr++;
                           }
                       }
                   }







                   //if (db.ChkDb_Value("select * from tbl_option where grp='printconfirmation' and status='Yes'"))
                   //{

                   DialogResult dlgresult = MessageBox.Show("Do You Want Bill Print?", "Confirm Complete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                   if (dlgresult == DialogResult.Yes)
                   {


                       // Print token  
                        counterPrint();//print token 

                       //  lp.totalBillPrint(); //print bill 
                      // lp.totalBillPrint_with_gv(DGV_tblOrder); //print bill 

                  // }
                   }
                   else
                   {
                      // lp.totalBillPrint_with_gv(DGV_tblOrder);
                   }
                   //else

                   //if (db.getDbstatus_Value("select status from tbl_option  where grp='TokenKotPrint'") =="Yes")

                   if (flag_tokenPrint)
                       counterPrint();//print token 

                   DataGridView temp_dgv = new DataGridView();

                 //  FinalBill fb = new FinalBill(label_orderID.Text, "500", lbl_total.Text, temp_dgv, false); //here send dgv to the final bill to rreduce material
                   //  FinalBill fb = new FinalBill(lbl_viewOrderID.Text, label_counterNo.Text, lbl_total.Text, this, temp_dgv); //here send dgv to the final bill to rreduce material

                  // fb.ShowDialog();


                   //this.Hide();
                   //Welcome wel = new Welcome();
                   //wel.ShowDialog();


                   //   counterOrderFormLoad();

                   order_id++;
                   label_orderID.Text = (order_id).ToString(); //assign the order id here         

                   view_order_id++;
                   lbl_viewOrderID.Text = (view_order_id).ToString(); //assign the order id here


                   DGV_tblOrder.Rows.Clear();
                   listViewCounter.Clear();

                   labelItemsCount.Text = "0";
                   lbl_total.Text = "0";

               }
               else
               {
                   MessageBox.Show("Please Select Item For Order");
               }
           }
       }

        void counterPrint()
        {
            
          //  for (int k = 0; k < ctr; k++)
            //{

              // lp.printToken(DGV_tblOrder);

            //}
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            if (txt_search.Text != "")
            {
                listView_items.Items.Clear();
                string qry = "SELECT menu.m_name FROM category INNER JOIN menu ON category.cat_name = menu.category WHERE (menu.m_name  like '%" + txt_search.Text + "%')  AND (category.type = 'Self Counter') ";


                using (SqlCommand command = new SqlCommand(qry, db.cn))
                {
                    db.cnopen();
                    using (SqlDataReader rd = command.ExecuteReader())
                    {

                       
                        while (rd.Read())
                        {
                            item = rd["m_name"].ToString();

                            listView_items.Items.Add(item);

                        }
                    }
                }
            }
            else
            {
                listView_items.Items.Clear();
                bind_itemsList();
            }
        }

        private void DGV_tblOrder_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            //  panel1.Visible = true;

            string seletedItemName = "";

            int get_qty = 0, amount = 0, get_amt;
            int j = DGV_tblOrder.SelectedCells[0].RowIndex;
            seletedItemName = DGV_tblOrder.Rows[j].Cells[1].Value.ToString();
            get_amt = int.Parse(db.getDb_Value("select rate from menu where m_name='" + seletedItemName + "' ").ToString());
                         
            if (DGV_tblOrder.Rows[j].Cells[2].Value.ToString() == "" || DGV_tblOrder.Rows[j].Cells[2].Value.ToString() == "0")
            {
                MessageBox.Show("You Can't Insert Empty Or ZERO Qty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                get_qty = 1;
                DGV_tblOrder.Rows[j].Cells[3].Value = get_qty;
                amount = get_amt * get_qty;
                DGV_tblOrder.Rows[j].Cells[3].Value = amount.ToString();
               
            }
            else
            {   
                get_qty = int.Parse(DGV_tblOrder.Rows[j].Cells[2].Value.ToString());
                amount = get_amt * get_qty;
                // DGV_tblOrder.Rows[i].Cells[2].Value = qty.ToString(); //add new qty into dgv
                DGV_tblOrder.Rows[j].Cells[3].Value = amount.ToString();
            }
            sumTotal();
        }

        private void listView_items_Click(object sender, EventArgs e)
        {
            string seletedItemName = "";
            int qty = 1;
            int get_qty = 0, flag = 0, amount = 1, get_amt = 0;



            if (listView_items.SelectedItems.Count > 0)
            {
                seletedItemName = listView_items.SelectedItems[0].Text;

                for (int i = 0; i <= DGV_tblOrder.RowCount - 1; i++)
                {
                    if (DGV_tblOrder.Rows[i].Cells[1].Value.ToString() == seletedItemName)
                    {
                        get_qty = int.Parse(DGV_tblOrder.Rows[i].Cells[2].Value.ToString());
                        qty += get_qty; // get old qty and add new qty


                        //  get_amt=int.Parse(DGV_tblOrder.Rows[i].Cells[3].Value.ToString());
                        get_amt = int.Parse(db.getDb_Value("select rate from menu where m_name='" + seletedItemName + "' ").ToString());

                        amount = get_amt * qty;
                        flag++; //for the alray exists
                        DGV_tblOrder.Rows[i].Cells[2].Value = qty.ToString(); //add new qty into dgv
                        DGV_tblOrder.Rows[i].Cells[3].Value = amount;
                    }
                }


                if (flag == 0)
                {
                    amount = int.Parse(db.getDb_Value("select rate from menu where m_name='" + seletedItemName + "' ").ToString());
                    DGV_tblOrder.Rows.Add(null, seletedItemName, qty, amount);
                }


                labelItemsCount.Text = DGV_tblOrder.RowCount.ToString(); //count the dgv rows / items
                label_counterNo.Text = db.getDbstatus_Value("SELECT     category FROM            menu WHERE        (m_name = '" + seletedItemName + "')");

            }
            sumTotal();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            int k=DGV_tblOrder.SelectedCells[0].RowIndex;
        
            DGV_tblOrder.Rows[k].Cells[2].Value += "1";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int k = DGV_tblOrder.SelectedCells[0].RowIndex;
            DGV_tblOrder.Rows[k].Cells[2].Value += "2";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int k = DGV_tblOrder.SelectedCells[0].RowIndex;
            DGV_tblOrder.Rows[k].Cells[2].Value += "3";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int k = DGV_tblOrder.SelectedCells[0].RowIndex;
            DGV_tblOrder.Rows[k].Cells[2].Value += "4";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int k = DGV_tblOrder.SelectedCells[0].RowIndex;
            DGV_tblOrder.Rows[k].Cells[2].Value += "5";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int k = DGV_tblOrder.SelectedCells[0].RowIndex;
            DGV_tblOrder.Rows[k].Cells[2].Value += "6";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            int k = DGV_tblOrder.SelectedCells[0].RowIndex;
            DGV_tblOrder.Rows[k].Cells[2].Value += "7";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            int k = DGV_tblOrder.SelectedCells[0].RowIndex;
            DGV_tblOrder.Rows[k].Cells[2].Value += "8";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            int k = DGV_tblOrder.SelectedCells[0].RowIndex;
            DGV_tblOrder.Rows[k].Cells[2].Value += "9";
        }

        private void button12_Click(object sender, EventArgs e)
        {
            int k = DGV_tblOrder.SelectedCells[0].RowIndex;
            DGV_tblOrder.Rows[k].Cells[2].Value += "0";
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pnlcalc.Visible = false;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            string seletedItemName = "";

            int get_qty = 0, amount = 0, get_amt;
            int j = DGV_tblOrder.SelectedCells[0].RowIndex;
            seletedItemName = DGV_tblOrder.Rows[j].Cells[1].Value.ToString();
            get_amt = int.Parse(db.getDb_Value("select rate from menu where m_name='" + seletedItemName + "' ").ToString());

            if (DGV_tblOrder.Rows[j].Cells[2].Value.ToString() == "" || DGV_tblOrder.Rows[j].Cells[2].Value.ToString() == "0")
            {
                MessageBox.Show("You Can't Insert Empty Or ZERO Qty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                get_qty = 1;
                DGV_tblOrder.Rows[j].Cells[2].Value = get_qty;
                amount = get_amt * get_qty;
                DGV_tblOrder.Rows[j].Cells[3].Value = amount.ToString();
            }
            else
            {
                get_qty = int.Parse(DGV_tblOrder.Rows[j].Cells[2].Value.ToString());
                amount = get_amt * get_qty;
                // DGV_tblOrder.Rows[i].Cells[2].Value = qty.ToString(); //add new qty into dgv
                DGV_tblOrder.Rows[j].Cells[3].Value = amount.ToString();
               

            }

            sumTotal();
            pnlcalc.Visible = false;
        }

        private void DGV_tblOrder_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //panel1.Visible = true;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int k = DGV_tblOrder.SelectedCells[0].RowIndex;
            DGV_tblOrder.Rows[k].Cells[2].Value  = "";
        }

        private void DGV_tblOrder_Leave(object sender, EventArgs e)
        {

        }

        private void DGV_tblOrder_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
            if (DGV_tblOrder.CurrentCell.ColumnIndex == 3 || DGV_tblOrder.CurrentCell.ColumnIndex == 2) //Desired Column
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

        private void Counter_Order_Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (var process in Process.GetProcessesByName("DataSynchronisation"))//DataSynchronisation.exe
            {
                process.Kill();
            }
          
            Application.Exit();
        }

        private void categoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Category cat = new Category();
            cat.ShowDialog();
        }

        private void hotelDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Company_Info comp_info = new Company_Info();
            comp_info.ShowDialog();
        }

        private void materialToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Material material = new Material();
            material.ShowDialog();
        }

        private void distributeMaterialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Material_Distribute md = new Material_Distribute();
            md.ShowDialog();
        }

        private void addSupplierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Supplier_Add sup_add = new Supplier_Add();
            sup_add.ShowDialog();
        }

        private void addCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddCustmer cust = new AddCustmer();
            cust.ShowDialog();
        }

        private void addEmployeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Waiter_add w_add = new Waiter_add();
            w_add.ShowDialog();
        }

        private void addExpencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            genralExp GenralExp = new genralExp();
            GenralExp.Show();
        }

        private void vageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Menu mn = new Menu();
            mn.ShowDialog();
        }

        private void updateMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Update_Menu _update = new Update_Menu();
            _update.ShowDialog();
        }

        private void addCustomerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AddCustmer cust = new AddCustmer();
            cust.ShowDialog();
        }

        private void receiptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Receipt rct = new Receipt();
            rct.ShowDialog();
        }

        private void salesItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TodayCounter t = new TodayCounter();
            t.ShowDialog();
        }

        private void cancelOrderDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Cancel_Order_Report cancel_order = new Cancel_Order_Report();
            cancel_order.ShowDialog();
        }

        private void dayEndToolStripMenuItem_Click(object sender, EventArgs e)
        {
         //   DayEnd_bkupServices dyend = new DayEnd_bkupServices();
        //    dyend.ShowDialog();
        }

        private void addExpensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            genralExp GenralExp = new genralExp();
            GenralExp.Show();
        }

        private void expensesDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpensesDtls dtls = new ExpensesDtls();
            dtls.ShowDialog();
        }

        private void menuDetailsReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Menu_Details rpt = new Menu_Details();
            rpt.ShowDialog();
        }

        private void datewiseToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Rpt_table__sales_date_wise rpt = new Rpt_table__sales_date_wise();
            rpt.ShowDialog();
        }

        private void materialWiseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_table_sales_materialwise rpt = new RPT_table_sales_materialwise();
            rpt.ShowDialog();
        }

        private void waiterWiseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Rpt_table__sales_name_wise rpt = new Rpt_table__sales_name_wise();
            rpt.ShowDialog();
        }

        private void tableWiseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Rpt_table__sales_Table_wise rpt = new Rpt_table__sales_Table_wise();
            rpt.ShowDialog();
        }

        private void insentiveDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CRT_Insentive_dtls rpt = new CRT_Insentive_dtls();
            rpt.ShowDialog();
        }

        private void categorywiseReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Print_Reports categoryRpt = new Print_Reports();
            categoryRpt.ShowDialog();
        }

        private void deletedItemReportToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Deleted_Item_Details dtl = new Deleted_Item_Details();
            dtl.ShowDialog();
        }

        private void cancelKOTReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cancel_KOT_Details dtl = new Cancel_KOT_Details();
            dtl.ShowDialog();
        }

        private void excelReportToolStripMenuItem_Click(object sender, EventArgs e)
        {

        //    Excel_Report er = new Excel_Report();
        //    er.ShowDialog();
        }

        private void receiptFormatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReceiptFormat rcformat = new ReceiptFormat();
            rcformat.ShowDialog();
        }

        private void userToolStripMenuItem_Click(object sender, EventArgs e)
        {
            User_Creation user = new User_Creation();
            user.ShowDialog();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rdbTeklogic op = new rdbTeklogic();
            op.ShowDialog();
            //lblOpMode.Text = db.getDbstatus_Value("select process_type from tbl_option");
           // getdate();
        }

        private void manageOrdersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Manage_Order manageOrder = new Manage_Order();
            manageOrder.ShowDialog();
        }

        private void accessControlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AccessControl accCobntrol = new AccessControl();
            accCobntrol.ShowDialog();
        }

        private void databaseBackupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DatabaseBackup bkup = new DatabaseBackup();
            bkup.ShowDialog();
        }

        private void configurationPanelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Configuation_Panel panel = new Configuation_Panel();
            panel.ShowDialog();
        }

        private void configurationSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {

            ConfigurationSetting cs = new ConfigurationSetting();
            cs.ShowDialog();
        }

        private void userMailIdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserMailId mail = new UserMailId();
            mail.ShowDialog();
        }

        private void logoutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            foreach (var process in Process.GetProcessesByName("DataSynchronisation"))//DataSynchronisation.exe
            {
                process.Kill();
            }
          
            //this.Hide();
            //login_test log = new login_test();
            //log.ShowDialog();
            Application.Exit();
        
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pnlcalc_Paint(object sender, PaintEventArgs e)
        {

        }
	}
}
