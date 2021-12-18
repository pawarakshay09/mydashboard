using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing.Printing;
using DbBackupDLL;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Diagnostics;


namespace Hotal_Managment_Syatem
{
    public partial class Welcome : Form
    {
        PrintDocument pdoc = null;
       // GetNewDatabase Odb = new GetNewDatabase();
        int order_id = 0;
        string cr_st = "", id, trialDate;
        int flag = 0, NoofDays;
        string n = 0.ToString();
        string name, todayDate = "";
        string stockqty;
        double getsalesqty;
        double updatedqty;
        // Login cur_log; //For existing login form
        login_test cur_log;
        FinalBill finalBIll;
        bool tokenOrderFlag, welcomeflag;
        public string tableIdFrom = "0", User_name = "";
        string order_tb_status = "Novat";
        public string orderID_print = "0", reason_cancel;
        public float custId;
        string qur = "";
        float stockQty;
        static bool flgdel = false;
        //SqlConnection con = new SqlConnection();
        Database db = new Database();
        DbBackup bc = new DbBackup();

        // public Login login;
        LPrinter myprinter = new LPrinter();

        // get syncronization code path and staus
        string syncStatus = string.Empty;
        string syncExepath = string.Empty;

        public static string ConString = StringCipher.Decrypt(System.IO.File.ReadAllText("Config.txt"), "5");

        public SqlConnection cn = new SqlConnection(ConString);
       
        public Welcome()
        {


            InitializeComponent();
            timerLableMove.Start();
        }
        public Welcome(string reason_cancel, bool flg)
        {
            InitializeComponent();

            this.reason_cancel = reason_cancel;
            flgdel = flg;
        }

        public Welcome(int table_no)
        {
            InitializeComponent();
            tokenOrderFlag = false;
        }

        public Welcome(float cust_id)
        {
            InitializeComponent();

            this.custId = cust_id;
        }
        //public Welcome(Login log, string name)
        //{
        //    // log.Close();
        //    cur_log = log;
        //    this.User_name = name;
        //    InitializeComponent();

        //}

        public Welcome(login_test log, string name)
        {
            // log.Close();
            cur_log = log;
            this.User_name = name;
            InitializeComponent();

        }

        //string constr = System.Configuration.ConfigurationSettings.AppSettings.Get("con");
        public string autoPrintKot = string.Empty;
        private void Form1_Load(object sender, EventArgs e)
        {
            panelorder.Visible = false;
            //string isHIstory = string.Empty;
            //isHIstory = (System.DateTime.Now >= DateTime.Parse("24/05/2019")) ? isHIstory = "Yes" : isHIstory = "No";
            //MessageBox.Show(isHIstory);

               
            //string myHost = System.Net.Dns.GetHostName();
            //string myIP = System.Net.Dns.GetHostByName(myHost).AddressList[0].ToString();
            //lblip.Text = myIP.ToString();
            //string port = db.getDbstatus_Value("select value from tbl_option where grp='Portno'");
            //lblport.Text = port.ToString();

          //  Panelsections.Visible = true;
            PanelParcel.Visible = false;
            panelServicecharge.Visible = false;
            if (db.ChkDb_Value("select * from tbl_option where grp='ServiceTaxvalue' and status='Yes'"))
            {
                panelServicecharge.Visible = true;
            }
            //get current product version


            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fileVersionInfo.ProductVersion;
            lbl_productVersion.Text = "Version: " + version;

            //get sync exe path and status from db -- code added by sagar on 06072018

            string syncValue = db.getDbstatus_Value("select status+'-'+value from tbl_option where grp='onlineSync'");
            syncStatus = syncValue.Split('-')[0];
            syncExepath = syncValue.Split('-')[1];

            autoPrintKot = db.getDbstatus_Value("select status  from tbl_option where grp='AutoBillPrint'");

            string dayendDt;
            dayendDt = db.getDbstatus_Value("Select ddate from tbl_dayend_status");
            string toDayDt = System.DateTime.Now.ToString("MM/dd/yyyy");

            lblOpMode.Text = db.getDbstatus_Value("select  status  from tbl_option where grp='OperationMode'");

            if (lblOpMode.Text !="By Mouse Click")
            {
                panel_tokenorder.Visible = true;
            }
            else
            {
                panel_tokenorder.Visible = false;
            }
        

            // get Trial Period here
            double dateDiff, count;
            NoofDays = int.Parse(db.getDb_Value("select NoOfDays from TrialPeriodSet").ToString());

            trialDate = db.getDbstatus_Value("select TrialDate from TrialPeriodSet");

            DateTime trDt = Convert.ToDateTime(trialDate);

            DateTime dtmain = System.DateTime.Now;

            string date = Convert.ToDateTime(dtmain).ToString("MM-dd-yyyy");


            dateDiff = (dtmain - trDt).TotalDays;
            count = (NoofDays - dateDiff);
            if (count <= 0)
            {
                foreach (Button button in this.Controls.OfType<Button>())
                    button.Enabled = false;

                MessageBox.Show(" Please Contact System Administrator \n\n Error 10561:NSC group surface 2,Source 0 \n Geometry error object 2 detected.\n Start xyz=0.0000E+000,1.250E-004,0.0000E+000 \n Start lmn=0.000000000,0.0000000,1.0000000000 \n Please Contact System Administrator", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
                if (count < 30)
                {
                    lblremain.Visible = true;
                    lblrem.Visible = true;
                    string remainday = count.ToString().Split('.')[0].ToString();
                   

                    lblrem.Text = remainday;
                }


                bindList();
                LoadBind();
                lbl_name.Text = User_name.ToString();
                //  txtTableNo.Focus();                 
                welcome_pageload(welcomeflag, "0");
                btnPayBill.Enabled = false;
                btnCancelOrder.Enabled = false;
                button_print.Enabled = false;
                btn_KOTprint.Enabled = false;
                if (db.ChkDb_Value("select * from tbl_option where grp='" + "Tax" + "' and status='Yes'"))
                {
                    pnlGST.Visible = true;
                    pnl_ST_VAT.Visible = true;
                }
                else
                {
                    pnlGST.Visible = false;
                   pnl_ST_VAT.Visible = true;
                }

                db.comboFill(cmbWaiterName, "select * from waiter_dtls where work_type='Captain' ", "waiter_dtls", "wname", "wname");
                db.comboFill(combowaiter, "select * from waiter_dtls where work_type='Waiter'", "waiter_dtls", "wname", "wname");


                //yogesh

                string qur = "SELECT  * from tbl_reservation";
                db.cnopen();
                SqlCommand cmd1 = new SqlCommand(qur, cn);
                cn.Open();
                SqlDataReader rd = cmd1.ExecuteReader();
                while (rd.Read() == true)
                {
                    string tid = rd["tid"].ToString();
                    string rdate = rd["date"].ToString();

                    DateTime rdat = Convert.ToDateTime(rdate);

                    string rrdate = rdat.ToString("MM-dd-yyyy");


                    if (rrdate == date)
                    {
                        db.update("update table_status set status='Reserve' where t_id='" + tid + "' ");

                        //reserve_btns(tid);
                    }

                }
                cn.Close();




                if (toDayDt != Convert.ToDateTime(dayendDt).ToString("MM/dd/yyyy"))
                {
                    MessageBox.Show("First Make DayEnd");
                }
                else
                {

                    Timer timer = new Timer();
                    timer.Interval = 500;
                    timer.Enabled = false;

                    timer.Start();

                    // ******************* for birthday reminder *******************

                    if (db.ChkDb_Value("select * from tbl_option where grp='" + "Notification" + "' and status='Yes' "))
                    {
                        if (db.ChkDb_Value("select name,phone from Custmer where dateOfBirth = '" + date + "' "))
                        {
                            linkLabel_birthday.Visible = true;
                            pictureBox1.Visible = true;
                            pictureBox2.Visible = true;


                            if (db.ChkDb_Value("select name,phone from Custmer where dateOfBirth = '" + date + "' "))
                                timer.Tick += new EventHandler(timer_Tick);
                            else
                                timer.Tick -= timer_Tick;
                            BirthdayReminder bday = new BirthdayReminder();
                            bday.ShowDialog();
                        }




                        //******************* for stock Reminder ******************* 

                        dataGridView2.DataSource = db.Displaygrid("SELECT        tbl_stock.item_name as [Material Name], tbl_stock.qty as Qty, tbl_stock.unit as Unit, material_nm.minimum_stock as [Minimum Stock] FROM            tbl_stock INNER JOIN      material_nm ON tbl_stock.item_name = material_nm.material_nm");
                        for (int i = 0; i < dataGridView2.RowCount; i++)
                        {
                            string materialName = dataGridView2.Rows[i].Cells[0].Value.ToString();

                            //  materialName = db.getDbstatus_Value("select material_nm from material_nm");
                            if (db.ChkDb_Value("select minimum_stock from material_nm where material_nm='" + materialName + "'"))
                                stockQty = db.getDb_Value("select minimum_stock from material_nm where material_nm='" + materialName + "'");
                            if (db.ChkDb_Value("select * from tbl_stock where item_name='" + materialName + "' and  qty<='" + stockQty + "'"))
                            {
                                timer.Tick += new EventHandler(timer_Tick_Stock);
                                // linkLabel_stock.Visible = true;
                                linkLabel_birthday.Visible = true;
                                pictureBox1.Visible = true;
                                pictureBox2.Visible = true;

                            }
                            else
                                timer.Tick -= timer_Tick_Stock;


                        }

                        //******************* for ReminderSetting***********************
                        todayDate = System.DateTime.Now.ToString("MM-dd-yyyy");
                        string todayDay = todayDate.Substring(0, 6);
                        int getday = 0, getmonth = 0;

                        if (db.ChkDb_Value("SELECT datepart (dd, ReminderDate) as day, datepart (MM, ReminderDate) as Month from ReminderSetting where DAY(ReminderDate) = DAY('" + todayDate + "') and MONTH(ReminderDate) = MONTH('" + todayDate + "') "))
                        {
                            getday = int.Parse(db.getDb_Value("SELECT datepart (dd, ReminderDate) as day from ReminderSetting where DAY(ReminderDate) = DAY('" + todayDate + "')").ToString());
                            getmonth = int.Parse(db.getDb_Value("SELECT datepart (MM, ReminderDate) as Month from ReminderSetting where MONTH(ReminderDate) = MONTH('" + todayDate + "') and DAY(ReminderDate) = DAY('" + todayDate + "')").ToString());
                        }
                        db.cnopen();
                        SqlCommand cmd = new SqlCommand("select ReminderFor as [Reminder],ReminderDate as [Date] from ReminderSetting where MONTH(ReminderDate) = MONTH('" + todayDate + "') and DAY(ReminderDate) = DAY('" + todayDate + "') ", db.cn);
                        SqlDataReader rdReminder = cmd.ExecuteReader();
                        while (rdReminder.Read())
                        {

                            int day = (System.DateTime.Today.Day);
                            int month = (System.DateTime.Today.Month);
                            // if (db.ChkDb_Value("select customerName,mobileNo from Customer where aniversaryDate = '" + dt.Day + "' and maritalstatus='Married' "))



                            if (day == getday && month == getmonth)
                            {
                                linkLabel_birthday.Visible = true;
                                pictureBox1.Visible = true;
                                pictureBox2.Visible = true;


                                // if (db.ChkDb_Value("select customerName,mobileNo from Customer where aniversaryDate = '" + dt.Day + "' and maritalstatus='Married' "))

                                if (day == getday && month == getmonth)
                                    timer.Tick += new EventHandler(timer_Tick);
                                else
                                    timer.Tick -= timer_Tick;



                            }


                        }
                        dgvReminder.DataSource = db.Displaygrid("select ReminderFor as [Reminder],ReminderDate as [Date] from ReminderSetting where MONTH(ReminderDate) = MONTH('" + todayDate + "') and DAY(ReminderDate) = DAY('" + todayDate + "') ");
                        if (dgvReminder.RowCount > 0)
                        {
                            ShowUtilityReminder res = new ShowUtilityReminder();
                            res.ShowDialog();
                        }

                        db.cnclose();

                    }

                    //******************** Send bday SMS *******************
                    if (!db.ChkDb_Value("select * from SmsStatus where smsSend='SMS Sent' and date='" + System.DateTime.Now.ToString("MM/dd/yyyy") + "'"))
                    {
                        string mobile, msg = "", name;
                        try
                        {
                            dataGridView2.DataSource = db.Displaygrid("select name as [Customer Name],phone as [Mobile No.]  from Custmer where MONTH(dateOfBirth) = MONTH('" + todayDate + "') and DAY(dateOfBirth) = DAY('" + todayDate + "') ");
                            dataGridView3.DataSource = db.Displaygrid("select name as [Customer Name],phone as [Mobile No.] from Custmer where MONTH(aniversaryDate) = MONTH('" + todayDate + "') and DAY(aniversaryDate) = DAY('" + todayDate + "') and status='Married' ");
                            int Getcount = 0;
                            if (dataGridView2.RowCount > 0)
                            {
                                msg = db.getDbstatus_Value("select message from SmsMaster where name='BIRTHDAY'");
                                for (int i = 0; i <= dataGridView2.Rows.Count - 1; i++)
                                {
                                    //if (Convert.ToBoolean(dataGridView1.Rows[i].Cells["Select"].Value) == true)
                                    //{

                                    mobile = dataGridView2.Rows[i].Cells[1].Value.ToString();
                                    name = dataGridView2.Rows[i].Cells[0].Value.ToString();

                                    if (mobile != "" && msg != "")
                                    {
                                        smssendingcode(mobile, name, msg);
                                        Getcount++;
                                    }

                                    //  }
                                }
                                db.update("update SmsStatus set date='" + System.DateTime.Now.ToString("MM/dd/yyyy") + "',smsSend='SMS Sent'");

                            }
                            if (dataGridView3.RowCount > 0)
                            {

                                msg = db.getDbstatus_Value("select message from SmsMaster where name='ANNIVERSARY'");
                                for (int i = 0; i <= dataGridView3.Rows.Count - 1; i++)
                                {
                                    //if (Convert.ToBoolean(dataGridView1.Rows[i].Cells["Select"].Value) == true)
                                    //{

                                    mobile = dataGridView3.Rows[i].Cells[1].Value.ToString();
                                    name = dataGridView3.Rows[i].Cells[0].Value.ToString();

                                    if (mobile != "" && msg != "")
                                    {
                                        smssendingcode(mobile, name, msg);
                                        Getcount++;
                                    }

                                    // }
                                }
                                db.update("update SmsStatus set date='" + System.DateTime.Now.ToString("MM/dd/yyyy") + "',smsSend='SMS Sent'");

                            }




                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }//first if else

            if (db.ChkDb_Value(" select * from tbl_option where  grp='DiscountReason'and status='Yes'"))
            {
                lblDiscountReason.Visible = true;
               // lbl_distext.Visible = true;
                string value = db.getDbstatus_Value("select value from tbl_option where grp ='DiscountReason' and status='Yes' ");

                lblDiscountReason.Text = value;
            }

            panelorder.Visible = false;

        
        }

        string mobileno = "";
        string msg = "";
        string Getname = "";
        public void smssendingcode(string mobileno, string name, string msg)
        {
            string urlName = "", senderName = "", smsType = "", apiKey = "";
            try
            {
                this.mobileno = mobileno;
                this.msg = msg;
                this.Getname = name;
                db.cnopen();
                SqlCommand cmd = new SqlCommand("select * from URLSetting", db.cn);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    urlName = rd["url"].ToString();
                    senderName = rd["senderName"].ToString();
                    smsType = rd["smstype"].ToString();
                    apiKey = rd["apiKey"].ToString();
                }
                db.cnclose();
                //string url = "http://sms1.businesslead.co.in:8081/sendSMS?username=abmstechno&message=Dear " + name.Trim() + "," + msg.Trim() + "&sendername=ABMSTE&smstype=PROMO&numbers=" + mobileno + "&apikey=229abe7a-40d2-4ec1-9c75-7f483a027feb";

                string url = "";// urlName + name.Trim() + "," + msg.Trim() + "&sendername=" + senderName + "&smstype=" + smsType + "&numbers=" + mobileno + "&apikey=" + apiKey + "";

                smsApiCall(url);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void smsApiCall(string getAPi)
        {

            HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(getAPi);
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] data = encoding.GetBytes("1");
            httpWReq.Method = "POST";
            httpWReq.ContentType = "application/x-www-form-urlencoded";
            httpWReq.ContentLength = data.Length;
            using (Stream stream = httpWReq.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string responseString = reader.ReadToEnd();

        }



        //yogesh    


        string lbl = "";
        void bindList()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("select distinct lblName from table_status", db.cn);
                db.cnopen();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read() == true)
                {
                    lbl = rd["lblName"].ToString();

                    listView1.Items.Add(lbl);
                    if (lbl == "All")
                    {
                        //listView1.Location.X.Equals(0);
                        //listView1.Location.Y.Equals(0);
                        //listView1.Width = 0;
                        listView1.Visible = false;
                        //listView_items.Location.X.Equals(0);
                        // listView_items.Location.Y.Equals(126);
                        // listView_items.Width = (548);
                    }
                    else
                    {
                        listView1.Visible = true;
                        //listView1.Location.X.Equals(0);
                        //listView1.Location.Y.Equals(126);
                        //listView1.Width = 151;
                        //listView_items.Width = 403;
                        //listView_items.Location.X.Equals(157);
                        //listView_items.Location.Y.Equals(126);
                    }
                }
                db.cnclose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        string selected_item, tblNm;
        void bind_itemsList()
        {
            listView_items.Clear();

            if (listView1.SelectedItems.Count >= 1)
            {
                selected_item = listView1.SelectedItems[0].Text;

            }
            //selected_item = listView1.SelectedItems[0].Text;

            string qry = "select t_id from table_status where lblName= '" + selected_item + "' and tblStatus='Active' order by id asc ";

            SqlCommand cmd = new SqlCommand(qry, db.cn);
            db.cnopen();
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                tblNm = rd["t_id"].ToString();
                string mystring = "   " + tblNm + "     ";

                listView_items.Items.Add(mystring);

            }
            chk_tb_statusList();
            if (db.ChkDb_Value("select tblStatus from table_status where tblStatus='All'"))
                chk_tb_statusListLoad();
        }
        void LoadBind()
        {
            listView_items.Clear();

            string qry = "select t_id from table_status where tblStatus='Active' order by id asc ";

            SqlCommand cmd = new SqlCommand(qry, db.cn);
            db.cnopen();
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                tblNm = rd["t_id"].ToString();

                string mystring = "   " + tblNm + "     ";
                listView_items.Items.Add(mystring);
            }
            chk_tb_statusListLoad();
        }
        private void timerLableMove_Tick(object sender, EventArgs e)
        {
        }
        void timer_Tick(object sender, EventArgs e) // ******** For Birthday Reminder *************** 
        {
            //if (linkLabel_birthday.BackColor == Color.Aqua)
            //    linkLabel_birthday.BackColor = Color.Red;

            //else
            //    linkLabel_birthday.BackColor = Color.Aqua;

            if (pictureBox1.Visible == true)
            {
                pictureBox1.Visible = false;
                pictureBox2.Visible = true;
            }
            else
            {
                pictureBox2.Visible = false;
                pictureBox1.Visible = true;
            }
        }

        void timer_Tick_Stock(object sender, EventArgs e) // ******** For Stock finish Reminder *************** 
        {
            //if (linkLabel_birthday.BackColor == Color.Aqua)
            //    linkLabel_birthday.BackColor = Color.Pink;
            //else
            //    linkLabel_birthday.BackColor = Color.Aqua;
            if (pictureBox1.Visible == true)
            {
                pictureBox1.Visible = false;
                pictureBox2.Visible = true;
            }
            else
            {
                pictureBox2.Visible = false;
                pictureBox1.Visible = true;
            }
        }
        public void acess_control()
        {
            SqlCommand cmd = new SqlCommand("SELECT formLinlk, tbl_user_access_control.status FROM            tbl_accessControl INNER JOIN tbl_user_access_control ON tbl_accessControl.FormName = tbl_user_access_control.FormName WHERE        (tbl_user_access_control.user_name = '" + lbl_name.Text + "')", db.cn);
            db.cnopen();
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                //ToolStripMenuItem item = new ToolStripMenuItem();
                //   item.Name = rd["formLinlk"].ToString();
                switch (rd["formLinlk"].ToString())
                {
                    //*** Initial ****
                    case "categoryToolStripMenuItem":
                        categoryToolStripMenuItem.Visible = true;
                        break;
                    case "hotelDetailsToolStripMenuItem":
                        hotelDetailsToolStripMenuItem.Visible = true;
                        break;

                    case "createGroupToolStripMenuItem":
                        createGroupToolStripMenuItem.Visible = true;
                        break;
                    case "tableTypeToolStripMenuItem":
                        tableTypeToolStripMenuItem.Visible = true;
                        break;
                    case "addTAQ6ToolStripMenuItem":
                        addTAQ6ToolStripMenuItem.Visible = true;
                        break;
                    case "materialToolStripMenuItem":
                        materialToolStripMenuItem.Visible = true;
                        break;
                    case "distributeMaterialToolStripMenuItem":
                        distributeMaterialToolStripMenuItem.Visible = true;
                        break;
                    case "addDrinkGroupToolStripMenuItem":
                        addDrinkGroupToolStripMenuItem.Visible = true;
                        break;

                    // ********* Menu *********
                    case "vageToolStripMenuItem":
                        vageToolStripMenuItem.Visible = true;
                        break;

                    // *********** Purchase ***************
                    case "addRecordToolStripMenuItem":
                        addRecordToolStripMenuItem.Visible = true;
                        break;
                    case "purchaseChangesToolStripMenuItem":
                        purchaseChangesToolStripMenuItem.Visible = true;
                        break;
                    case "paymentToolStripMenuItem3":
                        paymentToolStripMenuItem3.Visible = true;
                        break;

                    case "purchaseReportToolStripMenuItem":
                        purchaseReportToolStripMenuItem.Visible = true;
                        break;

                    //******** Stock ***********
                    case "stockToolStripMenuItem":
                        stockToolStripMenuItem.Visible = true;
                        break;

                    //******** Supplier ********
                    //case "addToolStripMenuItem1":
                    //    addToolStripMenuItem1.Visible = true;
                    //    break;
                    case "addSupplierToolStripMenuItem":
                        addSupplierToolStripMenuItem.Visible = true;
                        break;

                    case "directoryToolStripMenuItem2":
                        directoryToolStripMenuItem2.Visible = true;
                        break;

                    case "balanceReportToolStripMenuItem1":
                        balanceReportToolStripMenuItem1.Visible = true;
                        break;


                    // ********** Customer *********
                    //case "addCustomerToolStripMenuItem1":addCustomerToolStripMenuItem
                    //    addCustomerToolStripMenuItem1.Visible = true;
                    //    break;
                    case "addCustomerToolStripMenuItem":
                        addCustomerToolStripMenuItem.Visible = true;
                        break;

                    case "directoryToolStripMenuItem1":
                        directoryToolStripMenuItem1.Visible = true;
                        break;
                    case "receiptToolStripMenuItem":
                        receiptToolStripMenuItem.Visible = true;
                        break;
                    case "balanceReportToolStripMenuItem":
                        balanceReportToolStripMenuItem.Visible = true;
                        break;

                    // ***** Transaction *************
                    case "finelBillToolStripMenuItem":
                        finelBillToolStripMenuItem.Visible = true;
                        break;
                    case "salesItemToolStripMenuItem":
                        salesItemToolStripMenuItem.Visible = true;
                        break;

                    case "swapTableToolStripMenuItem":
                        swapTableToolStripMenuItem.Visible = true;
                        break;
                    case "cancelOrderDetailsToolStripMenuItem":
                        cancelOrderDetailsToolStripMenuItem.Visible = true;
                        break;

                    case "newOrderToolStripMenuItem":
                        newOrderToolStripMenuItem.Visible = false;
                        break;
                    case "paymentToolStripMenuItem2":
                        paymentToolStripMenuItem2.Visible = false;
                        break;
                    case "dayEndToolStripMenuItem":
                        dayEndToolStripMenuItem.Visible = true;
                        break;

                    case "parcelOrderDetailsToolStripMenuItem":
                        parcelOrderDetailsToolStripMenuItem.Visible = true;
                        break;

                    //******** Employee **********

                    //case "addToolStripMenuItem2":
                    //    addToolStripMenuItem2.Visible = true;
                    //    break;

                    case "addEmployeeToolStripMenuItem":
                        addEmployeeToolStripMenuItem.Visible = true;
                        break;
                    case "waiterPresetyToolStripMenuItem":
                        waiterPresetyToolStripMenuItem.Visible = true;
                        break;
                    case "presentyDetailsToolStripMenuItem":
                        presentyDetailsToolStripMenuItem.Visible = true;
                        break;

                    case "paymentToolStripMenuItem":
                        paymentToolStripMenuItem.Visible = true;
                        break;

                    // ********** Expences **************
                    //case "addExpensesToolStripMenuItem":
                    //    addExpensesToolStripMenuItem.Visible = true;
                    //    break;
                    case "addExpencesToolStripMenuItem":
                        addExpencesToolStripMenuItem.Visible = true;
                        break;
                    case "expensesDetailsToolStripMenuItem":
                        expensesDetailsToolStripMenuItem.Visible = true;
                        break;
                    case "issueKitchenToolStripMenuItem":
                        issueKitchenToolStripMenuItem.Visible = true;
                        break;
                    case "issueKitchenReportToolStripMenuItem":
                        issueKitchenReportToolStripMenuItem.Visible = true;
                        break;


                    // ******** Setting ********
                    case "settingToolStripMenuItem":
                        settingToolStripMenuItem.Visible = true;
                        break;

                    //*******SMS******
                    case "smsTempletToolStripMenuItem":
                        smsTempletToolStripMenuItem.Visible = true;
                        break;

                    case "sMSTemplateToolStripMenuItem":
                        sMSTemplateToolStripMenuItem.Visible = true;
                        break;

                    case "sMSURLSettingToolStripMenuItem":
                        sMSURLSettingToolStripMenuItem.Visible = true;
                        break;


                    //****** Report *********
                    case "hELPToolStripMenuItem":
                        hELPToolStripMenuItem.Visible = true;
                        break;

                    case "dailyToolStripMenuItem":
                        dailyToolStripMenuItem.Visible = true;
                        break;

                    case "totalCollectionReportToolStripMenuItem":
                        totalCollectionReportToolStripMenuItem.Visible = true;
                        break;

                    case "purchaseDetailsToolStripMenuItem":
                        purchaseDetailsToolStripMenuItem.Visible = true;
                        break;

                    case "waiterPaymentDetailsReportToolStripMenuItem":
                        waiterPaymentDetailsReportToolStripMenuItem.Visible = true;
                        break;

                    case "profitAndLossToolStripMenuItem":
                        profitAndLossToolStripMenuItem.Visible = true;
                        break;

                    case "menuDetailsReportToolStripMenuItem":
                        menuDetailsReportToolStripMenuItem.Visible = true;
                        break;

                    case "salesReportToolStripMenuItem":
                        salesReportToolStripMenuItem.Visible = true;
                        break;

                    case "drinkStockReportToolStripMenuItem":
                        drinkStockReportToolStripMenuItem.Visible = true;
                        break;

                    case "categorywiseReportToolStripMenuItem":
                        categorywiseReportToolStripMenuItem.Visible = true;
                        break;

                    case "deletedItemReportToolStripMenuItem":
                        deletedItemReportToolStripMenuItem.Visible = true;
                        break;

                    case "cancelKOTReportToolStripMenuItem":
                        cancelKOTReportToolStripMenuItem.Visible = true;
                        break;
                    case "excelReportToolStripMenuItem":
                        excelReportToolStripMenuItem.Visible = true;
                        break;

                }
                //string status = rd["status"].ToString();
                //item.Visible =Convert.ToBoolean(status);
            }
            db.cnclose();

        }
        public void ModuleAcessControl()
        {
            SqlCommand cmd = new SqlCommand("SELECT formLinlk, AssignModules.status FROM            ModuleAcessCntrol INNER JOIN AssignModules ON ModuleAcessCntrol.ControlUnder = AssignModules.ControlUnder ", db.cn);
            db.cnopen();
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                switch (rd["formLinlk"].ToString())
                {
                    case "initialRecordToolStripMenuItem":
                        initialRecordToolStripMenuItem.Visible = true;
                        break;
                    case "gUESTToolStripMenuItem":
                        gUESTToolStripMenuItem.Visible = true;
                        break;

                    case "toolStripMenuItem1":
                        toolStripMenuItem1.Visible = true;
                        break;
                    case "stockToolStripMenuItem":
                        stockToolStripMenuItem.Visible = true;
                        break;
                    case "supplierToolStripMenuItem":
                        supplierToolStripMenuItem.Visible = true;
                        break;
                    case "customerToolStripMenuItem":
                        customerToolStripMenuItem.Visible = true;
                        break;
                    case "logoutToolStripMenuItem":
                        logoutToolStripMenuItem.Visible = true;
                        break;
                    case "workerToolStripMenuItem":
                        workerToolStripMenuItem.Visible = true;
                        break;

                    case "kichenExpensesToolStripMenuItem":
                        kichenExpensesToolStripMenuItem.Visible = true;
                        break;

                    case "hELPToolStripMenuItem":
                        hELPToolStripMenuItem.Visible = true;
                        break;
                    case "settingToolStripMenuItem":
                        settingToolStripMenuItem.Visible = true;
                        break;


                }

            }
            db.cnclose();

        }
        public void welcome_pageload(bool welcomeflag, string tblNo)
        {
            if (welcomeflag)
                txtTableNo.Text = tblNo;
            if (db.ChkDb_Value("select * from tbl_option where status='Yes' and grp='Counter Order'"))
                btn_selfCounter.Visible = true;
            else
                btn_selfCounter.Visible = false;

            if (db.ChkDb_Value("select * from tbl_option where status='Yes' and grp='Parcel Order'"))
                btnParcelOrder.Visible = true;
            else
                btnParcelOrder.Visible = false;

            if (db.ChkDb_Value("select * from tbl_option where status='Yes' and grp='Cancel Order'"))
                btnCancelOrder.Visible = true;
            else
                btnCancelOrder.Visible = false;

            if (db.ChkDb_Value("select * from tbl_option where status='Yes' and grp='Kot Print Button'"))
                btn_KOTprint.Visible = true;
            else
                btn_KOTprint.Visible = false;

            if (db.ChkDb_Value("select * from tbl_option where status='Yes' and grp='Pay Button'"))
                btnPayBill.Visible = true;
            else
                btnPayBill.Visible = false;

            if (label_order_id.Text == "0")
            {
                btnPayBill.Enabled = false;
            }
            else { btnPayBill.Enabled = true; }
            acess_control();// for forms acess control
            ModuleAcessControl(); // for module acess Control 

            label_hotelName.Text = db.getDbstatus_Value("select companyName from tbl_CompanyInfo").ToString();
            //db.comboFill(cmbWaiterName, "SELECT wname FROM waiter_dtls", "waiter_dtls", "waiter_dtls", "wname");            

            chk_tb_status();
            get_table_status();
            getdate();
        }
        public void getdate()// for geting date from database table 'tbl_dayend_status'
        {
            string ddate = db.getDbstatus_Value("select ddate from tbl_dayend_status");
            label_date.Text = ddate;
        }


        public void welcome_pageload(string tbl_id)
        {
            label_hotelName.Text = db.getDbstatus_Value("select companyName from tbl_CompanyInfo").ToString();
            //db.comboFill(cmbWaiterName, "SELECT wname FROM waiter_dtls", "waiter_dtls", "waiter_dtls", "wname");            
            txtTableNo.Text = tbl_id;

            chk_tb_status();
            get_table_status();
        }


        //......................................
        public void WID()
        {
            string qury = "select w_id from sales_item where order_id='" + label_order_id.Text + "' ";

            try
            {

                SqlCommand cmd = new SqlCommand(qury, db.cn);
                db.cnopen();
                SqlDataReader rd = cmd.ExecuteReader();
                if (label_order_id.Text == "")
                {
                    return;
                }
                if (rd.Read() == true)
                {
                    n = rd["w_id"].ToString();
                }


            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
        //..............................................

        public void WNAME()
        {
            WID();
            if (n == 0.ToString())
            {
                return;
            }
            string qury = "select wname from waiter_dtls where W_id='" + n + "' ";

            try
            {

                SqlCommand cmd = new SqlCommand(qury, db.cn);
                db.cnopen();
                SqlDataReader rd = cmd.ExecuteReader();

                if (rd.Read() == true)
                {
                    cmbWaiterName.Text = rd["wname"].ToString();
                }

            }

            catch (Exception ex)
            { MessageBox.Show(ex.Message); }

        }
        public void chk_tb_status()
        {
            try
            {
                string qur = "Select * from table_status ";

                Button btn_tb = new Button();
                SqlCommand cmd = new SqlCommand(qur, db.cn);
                db.cnopen();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read() == true)
                {
                    cr_st = rd["status"].ToString();
                    id = (rd["t_id"].ToString());
                 

                    if (cr_st == "Empty")
                    {
                        green_btns(id);

                    }

                    if (cr_st == "Processing")
                    {
                        red_btns(id);
                    }
                    if (cr_st == "Printing")
                    {
                        pink_btns(id);
                    }

                    if (cr_st == "Reserve")
                    {
                        reserve_btns(id);
                    }
                    //if (cr_st != "Printing")
                    //{
                    //    green_btns(id);
                    //}

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
        }

        string table_status = "";
        void reserve_btns(string tid)
        {
            switch (id)
            {




                case "B1": btn_B1.BackColor = Color.LightSkyBlue; break;
                case "B2": btn_B2.BackColor = Color.LightSkyBlue; break;
                case "B3": btn_B3.BackColor = Color.LightSkyBlue; break;
                case "B4": btn_B4.BackColor = Color.LightSkyBlue; break;
                case "B5": btn_B5.BackColor = Color.LightSkyBlue; break;
                case "B6": btn_B6.BackColor = Color.LightSkyBlue; break;
                case "B7": btn_B7.BackColor = Color.LightSkyBlue; break;
                case "B8": btn_B8.BackColor = Color.LightSkyBlue; break;
                case "B9": btn_B9.BackColor = Color.LightSkyBlue; break;
                case "B10": btn_B10.BackColor = Color.LightSkyBlue; break;
                case "B11": btn_B11.BackColor = Color.LightSkyBlue; break;
                case "B12": btn_B12.BackColor = Color.LightSkyBlue; break;
                case "B13": btn_B13.BackColor = Color.LightSkyBlue; break;
                case "B14": btn_B14.BackColor = Color.LightSkyBlue; break;
                case "B15": btn_B15.BackColor = Color.LightSkyBlue; break;
                case "B16": btn_B16.BackColor = Color.LightSkyBlue; break;
                case "B17": btn_B17.BackColor = Color.LightSkyBlue; break;
                case "B18": btn_B18.BackColor = Color.LightSkyBlue; break;
                case "B19": btn_B19.BackColor = Color.LightSkyBlue; break; 



                case "P1": btn_P1.BackColor = Color.LightSkyBlue; break;
                case "P2": btn_P2.BackColor = Color.LightSkyBlue; break;
                case "P3": btn_P3.BackColor = Color.LightSkyBlue; break;
                case "P4": btn_P4.BackColor = Color.LightSkyBlue; break;
                case "P5": btn_P5.BackColor = Color.LightSkyBlue; break;
                case "P6": btn_P6.BackColor = Color.LightSkyBlue; break;
                case "P7": btn_P7.BackColor = Color.LightSkyBlue; break;
                case "P8": btn_P8.BackColor = Color.LightSkyBlue; break;
                case "P9": btn_P9.BackColor = Color.LightSkyBlue; break;
                case "P10": btn_P10.BackColor = Color.LightSkyBlue; break;
                case "P11": btn_P11.BackColor = Color.LightSkyBlue; break;
                case "P12": btn_P12.BackColor = Color.LightSkyBlue; break;
               
                case "P14": btn_P14.BackColor = Color.LightSkyBlue; break;
                case "P15": btn_P15.BackColor = Color.LightSkyBlue; break;





                case "T1": btn_T1.BackColor = Color.LightSkyBlue; break;
                case "T2": btn_T2.BackColor = Color.LightSkyBlue; break;
                case "T3": btn_T3.BackColor = Color.LightSkyBlue; break;
                case "T4": btn_T4.BackColor = Color.LightSkyBlue; break;
                case "T5": btn_T5.BackColor = Color.LightSkyBlue; break;
                case "T6": btn_T6.BackColor = Color.LightSkyBlue; break;
                case "T7": btn_T7.BackColor = Color.LightSkyBlue; break;
                case "T8": btn_T8.BackColor = Color.LightSkyBlue; break;
                case "T9": btn_T9.BackColor = Color.LightSkyBlue; break;
                case "T10": btn_T10.BackColor = Color.LightSkyBlue; break;
                case "T11": btn_T11.BackColor = Color.LightSkyBlue; break;
                case "T12": btn_T12.BackColor = Color.LightSkyBlue; break;
                case "T13": btn_T13.BackColor = Color.LightSkyBlue; break;
                case "T14": btn_T14.BackColor = Color.LightSkyBlue; break;
                case "T15": btn_T15.BackColor = Color.LightSkyBlue; break;
                case "T16": btn_T16.BackColor = Color.LightSkyBlue; break;
                case "T17": btn_T17.BackColor = Color.LightSkyBlue; break;
                case "T18": btn_T18.BackColor = Color.LightSkyBlue; break;
                case "T19": btn_T19.BackColor = Color.LightSkyBlue; break;
                case "T20": btn_T20.BackColor = Color.LightSkyBlue; break;
                case "T21": btn_T21.BackColor = Color.LightSkyBlue; break;
                case "T22": btn_T22.BackColor = Color.LightSkyBlue; break;
                case "T23": btn_T23.BackColor = Color.LightSkyBlue; break;
                case "T24": btn_T24.BackColor = Color.LightSkyBlue; break;




                case "L1": btn_L1.BackColor = Color.LightSkyBlue; break;
                case "L2": btn_L2.BackColor = Color.LightSkyBlue; break;
                case "L3": btn_L3.BackColor = Color.LightSkyBlue; break;
                case "L4": btn_L4.BackColor = Color.LightSkyBlue; break;
                case "L5": btn_L5.BackColor = Color.LightSkyBlue; break;
                case "L6": btn_L6.BackColor = Color.LightSkyBlue; break;
                case "L7": btn_L7.BackColor = Color.LightSkyBlue; break;
                case "L8": btn_L8.BackColor = Color.LightSkyBlue; break;
                case "L9": btn_L9.BackColor = Color.LightSkyBlue; break;
                case "L10": btn_L10.BackColor = Color.LightSkyBlue; break;
                case "L11": btn_L11.BackColor = Color.LightSkyBlue; break;
                case "L12": btn_L12.BackColor = Color.LightSkyBlue; break;
               // case "L13": btn_L13.BackColor = Color.LightSkyBlue; break;
                case "L14": btn_L14.BackColor = Color.LightSkyBlue; break;
                case "L15": btn_L15.BackColor = Color.LightSkyBlue; break;
                case "L16": btn_L16.BackColor = Color.LightSkyBlue; break;
                case "L17": btn_L17.BackColor = Color.LightSkyBlue; break;
                case "L18": btn_L18.BackColor = Color.LightSkyBlue; break;
                case "L19": btn_L19.BackColor = Color.LightSkyBlue; break;





                case "G1": btn_G1.BackColor = Color.LightSkyBlue; break;
                case "G2": btn_G2.BackColor = Color.LightSkyBlue; break;
                case "G3": btn_G3.BackColor = Color.LightSkyBlue; break;
                case "G4": btn_G4.BackColor = Color.LightSkyBlue; break;
                case "G5": btn_G5.BackColor = Color.LightSkyBlue; break;
                case "G6": btn_G6.BackColor = Color.LightSkyBlue; break;
                case "G7": btn_G7.BackColor = Color.LightSkyBlue; break;
                case "G8": btn_G8.BackColor = Color.LightSkyBlue; break;
                case "G9": btn_G9.BackColor = Color.LightSkyBlue; break;
                case "G10": btn_G10.BackColor = Color.LightSkyBlue; break;
                case "G11": btn_G11.BackColor = Color.LightSkyBlue; break;
                case "G12": btn_G12.BackColor = Color.LightSkyBlue; break;
               // case "G13": btn_G13.BackColor = Color.LightSkyBlue; break;
                case "G14": btn_G14.BackColor = Color.LightSkyBlue; break;
                case "G15": btn_G15.BackColor = Color.LightSkyBlue; break;
                case "G16": btn_G16.BackColor = Color.LightSkyBlue; break;
                case "G17": btn_G17.BackColor = Color.LightSkyBlue; break;
                case "G18": btn_G18.BackColor = Color.LightSkyBlue; break;
                case "G19": btn_G19.BackColor = Color.LightSkyBlue; break; 



            }
        }


        public void green_btns(string tid)
        {
            switch (id)
            {

                //case "1": btn_tb1.BackColor = Color.Green; break;
                //case "2": btn_tb2.BackColor = Color.Green; break;
                //case "3": btn_tb3.BackColor = Color.Green; break;
                //case "4": btn_tb4.BackColor = Color.Green; break;
                //case "5": btn_tb5.BackColor = Color.Green; break;
                //case "6": btn_tb6.BackColor = Color.Green; break;
                //case "7": btn_tb7.BackColor = Color.Green; break;
                //case "8": btn_tb8.BackColor = Color.Green; break;
                //case "9": btn_tb9.BackColor = Color.Green; break;
                //case "10": btn_tb10.BackColor = Color.Green; break;
                //case "11": btn_tb11.BackColor = Color.Green; break;
                //case "12": btn_tb12.BackColor = Color.Green; break;



                //case "HD1": btn_HD1.BackColor = Color.Green; break;
                //case "HD2": btn_HD2.BackColor = Color.Green; break;
                //case "HD3": btn_HD3.BackColor = Color.Green; break;
                //case "HD4": btn_HD4.BackColor = Color.Green; break;
                //case "HD5": btn_HD5.BackColor = Color.Green; break;
                //case "HD6": btn_HD6.BackColor = Color.Green; break;
                //case "HD7": btn_HD7.BackColor = Color.Green; break;
                //case "HD8": btn_HD8.BackColor = Color.Green; break; 










                //case "15":
                //    btn_tb15.BackColor = Color.Green;
                //    break;
                //case "16":
                //    btn_tb16.BackColor = Color.Green;
                //    break;
                //case "17":
                //    btn_tb17.BackColor = Color.Green;
                //    break;
                //case "18":
                //    btn_tb18.BackColor = Color.Green;
                //    break;
                //case "19":
                //    btn_tb19.BackColor = Color.Green;
                //    break;
                //case "20":
                //    btn_tb20.BackColor = Color.Green;
                //    break;
                //case "21":
                //    btn_tb21.BackColor = Color.Green;
                //    break;
                //case "22":
                //    btn_tb22.BackColor = Color.Green;
                //    break;
                //case "23":
                //    btn_tb23.BackColor = Color.Green;
                //    break;
                //case "24":
                //    btn_tb24.BackColor = Color.Green;
                //    break;
                //case "25":
                //    btn_tb25.BackColor = Color.Green;
                //    break;
                //case "26":
                //    btn_tb26.BackColor = Color.Green;
                //    break;
                //case "27":
                //    btn_tb27.BackColor = Color.Green;
                //    break;
                //case "28":
                //    btn_tb28.BackColor = Color.Green;
                //    break;
                //case "29":
                //    btn_tb29.BackColor = Color.Green;
                //    break;
                //case "30":
                //    btn_tb30.BackColor = Color.Green;
                //    break;

                //case "31":
                //    btn_tb31.BackColor = Color.Green;
                //    break;
                //case "32":
                //    btn_tb32.BackColor = Color.Green;
                //    break;
                //case "33":
                //    btn_tb33.BackColor = Color.Green;
                //    break;
                //case "34":
                //    btn_tb34.BackColor = Color.Green;
                //    break;
                //case "35":
                //    btn_tb35.BackColor = Color.Green;
                //    break;
                //case "36":
                //    btn_tb36.BackColor = Color.Green;
                //    break;
                //case "37":
                //    btn_tb37.BackColor = Color.Green;
                //    break;
                //case "38":
                //    btn_tb38.BackColor = Color.Green;
                //    break;
                //case "39":
                //    btn_tb39.BackColor = Color.Green;
                //    break;
                //case "40":
                //    btn_tb40.BackColor = Color.Green;
                //    break;
                //case "41":
                //    btn_tb41.BackColor = Color.Green;
                //    break;
                //case "42":
                //    btn_tb42.BackColor = Color.Green;
                //    break;

                //case "P1":
                //    btn_tbP1.BackColor = Color.Green;
                //    break;
                //case "P2":
                //    btn_tbP2.BackColor = Color.Green;
                //    break;
                //case "P3":
                //    btn_tbP3.BackColor = Color.Green;
                //    break;
                //case "P4":
                //    btn_tbP4.BackColor = Color.Green;
                //    break;
                //case "P5":
                //    btn_tbP5.BackColor = Color.Green;
                //    break;
                //case "P6":
                //    btn_tbP6.BackColor = Color.Green;
                //    break;
                //case "P7":
                //    btn_tbP7.BackColor = Color.Green;
                //    break;
                //case "P8":
                //    btn_tbP8.BackColor = Color.Green;
                //    break;



                case "P1": btn_P1.BackColor = Color.Green; break;
                case "P2": btn_P2.BackColor = Color.Green; break;
                case "P3": btn_P3.BackColor = Color.Green; break;
                case "P4": btn_P4.BackColor = Color.Green; break;
                case "P5": btn_P5.BackColor = Color.Green; break;
                case "P6": btn_P6.BackColor = Color.Green; break;
                case "P7": btn_P7.BackColor = Color.Green; break;
                case "P8": btn_P8.BackColor = Color.Green; break;



                case "L1": btn_L1.BackColor = Color.Green; break;
                case "L2": btn_L2.BackColor = Color.Green; break;
                case "L3": btn_L3.BackColor = Color.Green; break;
                case "L4": btn_L4.BackColor = Color.Green; break;
                case "L5": btn_L5.BackColor = Color.Green; break;
                case "L6": btn_L6.BackColor = Color.Green; break;
                case "L7": btn_L7.BackColor = Color.Green; break;
                case "L8": btn_L8.BackColor = Color.Green; break;
                case "L9": btn_L9.BackColor = Color.Green; break;
                case "L10": btn_L10.BackColor = Color.Green; break;
                case "L11": btn_L11.BackColor = Color.Green; break;
                case "L12": btn_L12.BackColor = Color.Green; break;

                case "L14": btn_L14.BackColor = Color.Green; break;
                case "L15": btn_L15.BackColor = Color.Green; break;
                case "L16": btn_L16.BackColor = Color.Green; break;
                case "L17": btn_L17.BackColor = Color.Green; break;
                case "L18": btn_L18.BackColor = Color.Green; break;
                case "L19": btn_L19.BackColor = Color.Green; break;




                case "B1": btn_B1.BackColor = Color.Green; break;
                case "B2": btn_B2.BackColor = Color.Green; break;
                case "B3": btn_B3.BackColor = Color.Green; break;
                case "B4": btn_B4.BackColor = Color.Green; break;
                case "B5": btn_B5.BackColor = Color.Green; break;
                case "B6": btn_B6.BackColor = Color.Green; break;
                case "B7": btn_B7.BackColor = Color.Green; break;
                case "B8": btn_B8.BackColor = Color.Green; break;
                case "B9": btn_B9.BackColor = Color.Green; break;
                case "B10": btn_B10.BackColor = Color.Green; break;
                case "B11": btn_B11.BackColor = Color.Green; break;
                case "B12": btn_B12.BackColor = Color.Green; break;
                case "B13": btn_B13.BackColor = Color.Green; break;
                case "B14": btn_B14.BackColor = Color.Green; break;
                case "B15": btn_B15.BackColor = Color.Green; break;

                case "B17": btn_B17.BackColor = Color.Green; break;
                case "B18": btn_B18.BackColor = Color.Green; break;
                case "B19": btn_B19.BackColor = Color.Green; break;
                case "B16": btn_B16.BackColor = Color.Green; break;



                case "G1": btn_G1.BackColor = Color.Green; break;
                case "G2": btn_G2.BackColor = Color.Green; break;
                case "G3": btn_G3.BackColor = Color.Green; break;
                case "G4": btn_G4.BackColor = Color.Green; break;
                case "G5": btn_G5.BackColor = Color.Green; break;
                case "G6": btn_G6.BackColor = Color.Green; break;
                case "G7": btn_G7.BackColor = Color.Green; break;
                case "G8": btn_G8.BackColor = Color.Green; break;
                case "G9": btn_G9.BackColor = Color.Green; break;
                case "G10": btn_G10.BackColor = Color.Green; break;
                case "G11": btn_G11.BackColor = Color.Green; break;
                case "G12": btn_G12.BackColor = Color.Green; break;

                case "G14": btn_G14.BackColor = Color.Green; break;
                case "G15": btn_G15.BackColor = Color.Green; break;
                case "G16": btn_G16.BackColor = Color.Green; break;
                case "G17": btn_G17.BackColor = Color.Green; break;
                case "G18": btn_G18.BackColor = Color.Green; break;



                case "G19": btn_G19.BackColor = Color.Green; break;




                case "O1": btn_O1.BackColor = Color.Green; break;
                case "O2": btn_O2.BackColor = Color.Green; break;
                case "O3": btn_O3.BackColor = Color.Green; break;
                case "O4": btn_O4.BackColor = Color.Green; break;
                case "O5": btn_O5.BackColor = Color.Green; break;
                case "O6": btn_O6.BackColor = Color.Green; break;
                case "O7": btn_O7.BackColor = Color.Green; break;
                case "O8": btn_O8.BackColor = Color.Green; break;
                case "O9": btn_O9.BackColor = Color.Green; break;
                case "O10": btn_O10.BackColor = Color.Green; break;
                case "O11": btn_O11.BackColor = Color.Green; break;
                case "O12": btn_O12.BackColor = Color.Green; break;
                case "O13": btn_O13.BackColor = Color.Green; break;
                case "O14": btn_O14.BackColor = Color.Green; break;
                case "O15": btn_O15.BackColor = Color.Green; break;
                case "O16": btn_O16.BackColor = Color.Green; break;
                case "O17": btn_O17.BackColor = Color.Green; break;



                case "T1":
                    btn_T1.BackColor = Color.Green;
                    break;
                case "T2":
                    btn_T2.BackColor = Color.Green;
                    break;
                case "T3":
                    btn_T3.BackColor = Color.Green;
                    break;
                case "T4":
                    btn_T4.BackColor = Color.Green;
                    break;
                case "T5":
                    btn_T5.BackColor = Color.Green;
                    break;
                case "T6":
                    btn_T6.BackColor = Color.Green;
                    break;
                case "T7":
                    btn_T7.BackColor = Color.Green;
                    break;
                case "T8":
                    btn_T8.BackColor = Color.Green;
                    break;

                case "T9": btn_T9.BackColor = Color.Green; break;
                case "T10": btn_T10.BackColor = Color.Green; break;
                case "T11": btn_T11.BackColor = Color.Green; break;
                case "T12": btn_T12.BackColor = Color.Green; break;
                case "T13": btn_T13.BackColor = Color.Green; break;
                case "T14": btn_T14.BackColor = Color.Green; break;
                case "T15": btn_T15.BackColor = Color.Green; break;
                case "T16": btn_T16.BackColor = Color.Green; break;
                case "T17": btn_T17.BackColor = Color.Green; break;
                case "T18": btn_T18.BackColor = Color.Green; break;
                case "T19": btn_T19.BackColor = Color.Green; break;
                case "T20": btn_T20.BackColor = Color.Green; break;
                case "T21": btn_T21.BackColor = Color.Green; break;
                case "T22": btn_T22.BackColor = Color.Green; break;
                case "T23": btn_T23.BackColor = Color.Green; break;
                case "T24": btn_T24.BackColor = Color.Green; break;




                case "P9": btn_P9.BackColor = Color.Green; break;
                case "P10": btn_P10.BackColor = Color.Green; break;
                case "P11": btn_P11.BackColor = Color.Green; break;
                case "P12": btn_P12.BackColor = Color.Green; break;

                case "P14": btn_P14.BackColor = Color.Green; break;
                case "P15": btn_P15.BackColor = Color.Green; break;
                case "P16": btn_P16.BackColor = Color.Green; break;
                case "P17": btn_P17.BackColor = Color.Green; break;
                case "P18": btn_P18.BackColor = Color.Green; break;
                case "P19": btn_P19.BackColor = Color.Green; break;










            }
        }

        void red_btns(string tid)
        {
            switch (id)
            {




                case "T1":
                    btn_T1.BackColor = Color.Red;
                    break;
                case "T2":
                    btn_T2.BackColor = Color.Red;
                    break;
                case "T3":
                    btn_T3.BackColor = Color.Red;
                    break;
                case "T4":
                    btn_T4.BackColor = Color.Red;
                    break;
                case "T5":
                    btn_T5.BackColor = Color.Red;
                    break;
                case "T6":
                    btn_T6.BackColor = Color.Red;
                    break;
                case "T7":
                    btn_T7.BackColor = Color.Red;
                    break;
                case "T8":
                    btn_T8.BackColor = Color.Red;
                    break;



                case "T9": btn_T9.BackColor = Color.Red; break;
                case "T10": btn_T10.BackColor = Color.Red; break;
                case "T11": btn_T11.BackColor = Color.Red; break;
                case "T12": btn_T12.BackColor = Color.Red; break;
                case "T13": btn_T13.BackColor = Color.Red; break;
                case "T14": btn_T14.BackColor = Color.Red; break;
                case "T15": btn_T15.BackColor = Color.Red; break;
                case "T16": btn_T16.BackColor = Color.Red; break;
                case "T17": btn_T17.BackColor = Color.Red; break;
                case "T18": btn_T18.BackColor = Color.Red; break;
                case "T19": btn_T19.BackColor = Color.Red; break;
                case "T20": btn_T20.BackColor = Color.Red; break;
                case "T21": btn_T21.BackColor = Color.Red; break;
                case "T22": btn_T22.BackColor = Color.Red; break;
                case "T23": btn_T23.BackColor = Color.Red; break;
                case "T24": btn_T24.BackColor = Color.Red; break;




                //case "P1":
                //    btn_tbP1.BackColor = Color.Red;
                //    break;
                //case "P2":
                //    btn_tbP2.BackColor = Color.Red;
                //    break;
                //case "P3":
                //    btn_tbP3.BackColor = Color.Red;
                //    break;
                //case "P4":
                //    btn_tbP4.BackColor = Color.Red;
                //    break;
                //case "P5":
                //    btn_tbP5.BackColor = Color.Red;
                //    break;
                //case "P6":
                //    btn_tbP6.BackColor = Color.Red;
                //    break;
                //case "P7":
                //    btn_tbP7.BackColor = Color.Red;
                //    break;
                //case "P8":
                //    btn_tbP8.BackColor = Color.Red;
                //    break;



                case "P1": btn_P1.BackColor = Color.Red; break;
                case "P2": btn_P2.BackColor = Color.Red; break;
                case "P3": btn_P3.BackColor = Color.Red; break;
                case "P4": btn_P4.BackColor = Color.Red; break;
                case "P5": btn_P5.BackColor = Color.Red; break;
                case "P6": btn_P6.BackColor = Color.Red; break;
                case "P7": btn_P7.BackColor = Color.Red; break;
                case "P8": btn_P8.BackColor = Color.Red; break;


                case "L1": btn_L1.BackColor = Color.Red; break;
                case "L2": btn_L2.BackColor = Color.Red; break;
                case "L3": btn_L3.BackColor = Color.Red; break;
                case "L4": btn_L4.BackColor = Color.Red; break;
                case "L5": btn_L5.BackColor = Color.Red; break;
                case "L6": btn_L6.BackColor = Color.Red; break;
                case "L7": btn_L7.BackColor = Color.Red; break;
                case "L8": btn_L8.BackColor = Color.Red; break;
                case "L9": btn_L9.BackColor = Color.Red; break;
                case "L10": btn_L10.BackColor = Color.Red; break;
                case "L11": btn_L11.BackColor = Color.Red; break;
                case "L12": btn_L12.BackColor = Color.Red; break;


                case "L14": btn_L14.BackColor = Color.Red; break;
                case "L15": btn_L15.BackColor = Color.Red; break;
                case "L16": btn_L16.BackColor = Color.Red; break;
                case "L17": btn_L17.BackColor = Color.Red; break;
                case "L18": btn_L18.BackColor = Color.Red; break;
                case "L19": btn_L19.BackColor = Color.Red; break;








                case "B1": btn_B1.BackColor = Color.Red; break;
                case "B2": btn_B2.BackColor = Color.Red; break;
                case "B3": btn_B3.BackColor = Color.Red; break;
                case "B4": btn_B4.BackColor = Color.Red; break;
                case "B5": btn_B5.BackColor = Color.Red; break;
                case "B6": btn_B6.BackColor = Color.Red; break;
                case "B7": btn_B7.BackColor = Color.Red; break;
                case "B8": btn_B8.BackColor = Color.Red; break;
                case "B9": btn_B9.BackColor = Color.Red; break;
                case "B10": btn_B10.BackColor = Color.Red; break;
                case "B11": btn_B11.BackColor = Color.Red; break;
                case "B12": btn_B12.BackColor = Color.Red; break;
                case "B13": btn_B13.BackColor = Color.Red; break;
                case "B14": btn_B14.BackColor = Color.Red; break;
                case "B15": btn_B15.BackColor = Color.Red; break;
                case "B17": btn_B17.BackColor = Color.Red; break;
                case "B18": btn_B18.BackColor = Color.Red; break;
                case "B19": btn_B19.BackColor = Color.Red; break;
                case "B16": btn_B16.BackColor = Color.Red; break;


                case "G1": btn_G1.BackColor = Color.Red; break;
                case "G2": btn_G2.BackColor = Color.Red; break;
                case "G3": btn_G3.BackColor = Color.Red; break;
                case "G4": btn_G4.BackColor = Color.Red; break;
                case "G5": btn_G5.BackColor = Color.Red; break;
                case "G6": btn_G6.BackColor = Color.Red; break;
                case "G7": btn_G7.BackColor = Color.Red; break;
                case "G8": btn_G8.BackColor = Color.Red; break;
                case "G9": btn_G9.BackColor = Color.Red; break;
                case "G10": btn_G10.BackColor = Color.Red; break;

                case "G11": btn_G11.BackColor = Color.Red; break;
                case "G12": btn_G12.BackColor = Color.Red; break;

                case "G14": btn_G14.BackColor = Color.Red; break;
                case "G15": btn_G15.BackColor = Color.Red; break;
                case "G16": btn_G16.BackColor = Color.Red; break;
                case "G17": btn_G17.BackColor = Color.Red; break;
                case "G18": btn_G18.BackColor = Color.Red; break;
                case "G19": btn_G19.BackColor = Color.Red; break;



                case "O1": btn_O1.BackColor = Color.Red; break;
                case "O2": btn_O2.BackColor = Color.Red; break;
                case "O3": btn_O3.BackColor = Color.Red; break;
                case "O4": btn_O4.BackColor = Color.Red; break;
                case "O5": btn_O5.BackColor = Color.Red; break;
                case "O6": btn_O6.BackColor = Color.Red; break;
                case "O7": btn_O7.BackColor = Color.Red; break;
                case "O8": btn_O8.BackColor = Color.Red; break;
                case "O9": btn_O9.BackColor = Color.Red; break;
                case "O10": btn_O10.BackColor = Color.Red; break;
                case "O11": btn_O11.BackColor = Color.Red; break;
                case "O12": btn_O12.BackColor = Color.Red; break;
                case "O13": btn_O13.BackColor = Color.Red; break;
                case "O14": btn_O14.BackColor = Color.Red; break;
                case "O15": btn_O15.BackColor = Color.Red; break;
                case "O16": btn_O16.BackColor = Color.Red; break;
                case "O17": btn_O17.BackColor = Color.Red; break;




                case "P9": btn_P9.BackColor = Color.Red; break;
                case "P10": btn_P10.BackColor = Color.Red; break;
                case "P11": btn_P11.BackColor = Color.Red; break;
                case "P12": btn_P12.BackColor = Color.Red; break;

                case "P14": btn_P14.BackColor = Color.Red; break;
                case "P15": btn_P15.BackColor = Color.Red; break;
                case "P16": btn_P16.BackColor = Color.Red; break;
                case "P17": btn_P17.BackColor = Color.Red; break;
                case "P18": btn_P18.BackColor = Color.Red; break;
                case "P19": btn_P19.BackColor = Color.Red; break;









            }
        }

        public void pink_btns(string tid)
        {
            switch (tid)
            {




                case "L1": btn_L1.BackColor = Color.HotPink; break;
                case "L2": btn_L2.BackColor = Color.HotPink; break;
                case "L3": btn_L3.BackColor = Color.HotPink; break;
                case "L4": btn_L4.BackColor = Color.HotPink; break;
                case "L5": btn_L5.BackColor = Color.HotPink; break;
                case "L6": btn_L6.BackColor = Color.HotPink; break;
                case "L7": btn_L7.BackColor = Color.HotPink; break;
                case "L8": btn_L8.BackColor = Color.HotPink; break;
                case "L9": btn_L9.BackColor = Color.HotPink; break;
                case "L10": btn_L10.BackColor = Color.HotPink; break;
                case "L11": btn_L11.BackColor = Color.HotPink; break;
                case "L12": btn_L12.BackColor = Color.HotPink; break;
                case "L14": btn_L14.BackColor = Color.HotPink; break;
                case "L15": btn_L15.BackColor = Color.HotPink; break;
                case "L16": btn_L16.BackColor = Color.HotPink; break;
                case "L17": btn_L17.BackColor = Color.HotPink; break;
                case "L18": btn_L18.BackColor = Color.HotPink; break;
                case "L19": btn_L19.BackColor = Color.HotPink; break;








                case "B1": btn_B1.BackColor = Color.HotPink; break;
                case "B2": btn_B2.BackColor = Color.HotPink; break;
                case "B3": btn_B3.BackColor = Color.HotPink; break;
                case "B4": btn_B4.BackColor = Color.HotPink; break;
                case "B5": btn_B5.BackColor = Color.HotPink; break;
                case "B6": btn_B6.BackColor = Color.HotPink; break;
                case "B7": btn_B7.BackColor = Color.HotPink; break;
                case "B8": btn_B8.BackColor = Color.HotPink; break;
                case "B9": btn_B9.BackColor = Color.HotPink; break;
                case "B10": btn_B10.BackColor = Color.HotPink; break;
                case "B11": btn_B11.BackColor = Color.HotPink; break;
                case "B12": btn_B12.BackColor = Color.HotPink; break;
                case "B13": btn_B13.BackColor = Color.HotPink; break;
                case "B14": btn_B14.BackColor = Color.HotPink; break;
                case "B15": btn_B15.BackColor = Color.HotPink; break;
                case "B17": btn_B17.BackColor = Color.HotPink; break;
                case "B18": btn_B18.BackColor = Color.HotPink; break;
                case "B19": btn_B19.BackColor = Color.HotPink; break;
                case "B16": btn_B16.BackColor = Color.HotPink; break;





                case "G1": btn_G1.BackColor = Color.HotPink; break;
                case "G2": btn_G2.BackColor = Color.HotPink; break;
                case "G3": btn_G3.BackColor = Color.HotPink; break;
                case "G4": btn_G4.BackColor = Color.HotPink; break;
                case "G5": btn_G5.BackColor = Color.HotPink; break;
                case "G6": btn_G6.BackColor = Color.HotPink; break;
                case "G7": btn_G7.BackColor = Color.HotPink; break;
                case "G8": btn_G8.BackColor = Color.HotPink; break;
                case "G9": btn_G9.BackColor = Color.HotPink; break;
                case "G10": btn_G10.BackColor = Color.HotPink; break;
                case "G11": btn_G11.BackColor = Color.HotPink; break;
                case "G12": btn_G12.BackColor = Color.HotPink; break;

                case "G14": btn_G14.BackColor = Color.HotPink; break;
                case "G15": btn_G15.BackColor = Color.HotPink; break;
                case "G16": btn_G16.BackColor = Color.HotPink; break;
                case "G17": btn_G17.BackColor = Color.HotPink; break;
                case "G18": btn_G18.BackColor = Color.HotPink; break;




                case "G19": btn_G19.BackColor = Color.HotPink; break;




                case "O1": btn_O1.BackColor = Color.HotPink; break;
                case "O2": btn_O2.BackColor = Color.HotPink; break;
                case "O3": btn_O3.BackColor = Color.HotPink; break;
                case "O4": btn_O4.BackColor = Color.HotPink; break;
                case "O5": btn_O5.BackColor = Color.HotPink; break;
                case "O6": btn_O6.BackColor = Color.HotPink; break;
                case "O7": btn_O7.BackColor = Color.HotPink; break;
                case "O8": btn_O8.BackColor = Color.HotPink; break;
                case "O9": btn_O9.BackColor = Color.HotPink; break;
                case "O10": btn_O10.BackColor = Color.HotPink; break;
                case "O11": btn_O11.BackColor = Color.HotPink; break;
                case "O12": btn_O12.BackColor = Color.HotPink; break;
                case "O13": btn_O13.BackColor = Color.HotPink; break;
                case "O14": btn_O14.BackColor = Color.HotPink; break;
                case "O15": btn_O15.BackColor = Color.HotPink; break;
                case "O16": btn_O16.BackColor = Color.HotPink; break;
                case "O17": btn_O17.BackColor = Color.HotPink; break;



                //case "P1":
                //    btn_tbP1.BackColor = Color.HotPink;
                //    break;
                //case "P2":
                //    btn_tbP2.BackColor = Color.HotPink;
                //    break;
                //case "P3":
                //    btn_tbP3.BackColor = Color.HotPink;
                //    break;
                //case "P4":
                //    btn_tbP4.BackColor = Color.HotPink;
                //    break;
                //case "P5":
                //    btn_tbP5.BackColor = Color.HotPink;
                //    break;
                //case "P6":
                //    btn_tbP6.BackColor = Color.HotPink;
                //    break;
                //case "P7":
                //    btn_tbP7.BackColor = Color.HotPink;
                //    break;
                //case "P8":
                //    btn_tbP8.BackColor = Color.HotPink;
                //    break;



                case "P1": btn_P1.BackColor = Color.HotPink; break;
                case "P2": btn_P2.BackColor = Color.HotPink; break;
                case "P3": btn_P3.BackColor = Color.HotPink; break;
                case "P4": btn_P4.BackColor = Color.HotPink; break;
                case "P5": btn_P5.BackColor = Color.HotPink; break;
                case "P6": btn_P6.BackColor = Color.HotPink; break;
                case "P7": btn_P7.BackColor = Color.HotPink; break;
                case "P8": btn_P8.BackColor = Color.HotPink; break;

                case "T1":
                    btn_T1.BackColor = Color.HotPink;
                    break;
                case "T2":
                    btn_T2.BackColor = Color.HotPink;
                    break;
                case "T3":
                    btn_T3.BackColor = Color.HotPink;
                    break;
                case "T4":
                    btn_T4.BackColor = Color.HotPink;
                    break;
                case "T5":
                    btn_T5.BackColor = Color.HotPink;
                    break;
                case "T6":
                    btn_T6.BackColor = Color.HotPink;
                    break;
                case "T7":
                    btn_T7.BackColor = Color.HotPink;
                    break;
                case "T8":
                    btn_T8.BackColor = Color.HotPink;
                    break;


                case "T9": btn_T9.BackColor = Color.HotPink; break;
                case "T10": btn_T10.BackColor = Color.HotPink; break;
                case "T11": btn_T11.BackColor = Color.HotPink; break;
                case "T12": btn_T12.BackColor = Color.HotPink; break;
                case "T13": btn_T13.BackColor = Color.HotPink; break;
                case "T14": btn_T14.BackColor = Color.HotPink; break;
                case "T15": btn_T15.BackColor = Color.HotPink; break;
                case "T16": btn_T16.BackColor = Color.HotPink; break;
                case "T17": btn_T17.BackColor = Color.HotPink; break;
                case "T18": btn_T18.BackColor = Color.HotPink; break;
                case "T19": btn_T19.BackColor = Color.HotPink; break;
                case "T20": btn_T20.BackColor = Color.HotPink; break;
                case "T21": btn_T21.BackColor = Color.HotPink; break;
                case "T22": btn_T22.BackColor = Color.HotPink; break;
                case "T23": btn_T23.BackColor = Color.HotPink; break;
                case "T24": btn_T24.BackColor = Color.HotPink; break;




                case "P9": btn_P9.BackColor = Color.HotPink; break;
                case "P10": btn_P10.BackColor = Color.HotPink; break;
                case "P11": btn_P11.BackColor = Color.HotPink; break;
                case "P12": btn_P12.BackColor = Color.HotPink; break;

                case "P14": btn_P14.BackColor = Color.HotPink; break;
                case "P15": btn_P15.BackColor = Color.HotPink; break;
                case "P16": btn_P16.BackColor = Color.HotPink; break;
                case "P17": btn_P17.BackColor = Color.HotPink; break;
                case "P18": btn_P18.BackColor = Color.HotPink; break;
                case "P19": btn_P19.BackColor = Color.HotPink; break;





            }
        }

        private void billGenerationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FinalBill bill = new FinalBill();
            bill.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //    Item item = new Item(label_order_id.Text, txtTableNo.Text);
            //    item.Show();
        }

        private void salesItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TodayCounter t = new TodayCounter();
            t.ShowDialog();
        }

        private void finelBillToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //print();
            //Bill_Print bill = new Bill_Print();
            //bill.ShowDialog();
        }

        private void purchaseEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Supplier_details sup_dtls = new Supplier_details();
            //sup_dtls.Show();          
        }

        private void itemStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Item_2 i_stock = new Item_2();
            //i_stock.Show();
        }

        private void purchaseToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        void bill_paid()
        {
            if (label_total_bill.Text == "")
            {
                MessageBox.Show("No Item Order", "RestroSoft", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //// insert the bill dtls according to the table id
                try
                {
                    string date = System.DateTime.Now.ToString("MM'-'dd'-'yyyy");
                    string qur = "INSERT INTO total_bill (order_id,Total_bill,datetime) VALUES('" + label_order_id.Text + "','" + label_total_bill.Text + "','" + date + "') ";

                    DialogResult dlgresult = MessageBox.Show("Are you sure want to pay?", "Confirm Pay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgresult == DialogResult.Yes)
                    {


                        FinalBill fb = new FinalBill(label_order_id.Text, txtTableNo.Text, label_total_bill.Text, this, dataGridView1, lbl_name.Text); //here send dgv to the final bill to rreduce material
                        fb.ShowDialog();
                        // db.InsertData("INSERT INTO total_bill (order_id,Total_bill,datetime,status,Customer_id,transaction_id,remark) VALUES('" + label_order_id.Text + "','" + label_total_bill.Text + "','" + date + "','Cash','" + 0 + "','" + "0" + "','" + "0" + "')", "");

                        //chk the order are paid successfully or not 
                        if (db.ChkDb_Value("Select * from total_bill where order_id='" + label_order_id.Text + "'"))
                        {


                            // here table bill paid then assign table to empty
                            table_status = "Empty";
                            db.update("update table_status set status='" + "Empty" + "' where t_id='" + txtTableNo.Text + "'");
                            db.update("update table_order set tableStatus='" + "Empty" + "' where t_id='" + txtTableNo.Text + "' and order_id='" + label_order_id.Text + "'");
                            label_order_id.Text = "0";

                            // update_tb_status(int.Parse(txtTableNo.Text.ToString()));
                            chk_tb_status();
                            txtTableNo.Text = "0";
                            dataGridView1.DataSource = "";
                            label_total_bill.Text = "0";
                            chkAmt.Checked = false;
                            chkPerc.Checked = false;
                            txtDiscAmt.Text = "0";
                            txtDiscValue.Text = "0";
                            lblStatus.Visible = false;
                            lblTotalAmt.Text = "0";
                            lblDisc.Text = "0";
                            lblCgstAmt.Text = "0";
                            lblSgstAmt.Text = "0";
                            btnPayBill.Enabled = false;
                            btnCancelOrder.Enabled = false;
                            button_print.Enabled = false;
                            btn_KOTprint.Enabled = false;

                        }
                        // end od pay code
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
            }


        }
        public void tableCall(string tbl_no, Button btn_name)
        {

            if (db.ChkDb_Value("select status from table_status where  t_id='" + tbl_no + "' and  status='Reserve'"))
            {
                chk_tb_status();
                panelreserv.Visible = true;
                txtremark.Text = db.getDbstatus_Value(" select ISNULL ((select reason from tbl_reservation where  tid='" + tbl_no + "' ),0)");

            }
            else
            {

                btn_add.Enabled = true;
                btnCancelOrder.Enabled = true;
                txtTableNo.Text = tbl_no;
                table_status = "Processing";
                get_table_status();

                chk_tb_status();
                update_tb_status((txtTableNo.Text.ToString()));
            }


            if (db.ChkDb_Value("select status from table_status where  t_id='" + tbl_no + "' and  status='Empty'"))
            {
                btnPayBill.Enabled = false;
                btnCancelOrder.Enabled = false;
                button_print.Enabled = false;
                btn_KOTprint.Enabled = false;
                linkCancelKot.Enabled = false;
                lblCgstAmt.Text = "0";
                lbl_vat_amt.Text = "0";
                lblCgstValue.Text = "0";
                lblSgstValue.Text = "0";
                lbl_Vat_per.Text = "0";
                lblSgstAmt.Text = "0";
                lblFoodSCVal.Text = "0";
                lblLiquorSCVal.Text = "0";
                lblFoodSCAmt.Text = "0";
                lblLiquorSCAmt.Text = "0";
                lblTotalAmt.Text = "0";
                label_total_bill.Text = "0";


            }
            else
            {
                btnPayBill.Enabled = true;
                btnCancelOrder.Enabled = true;
                button_print.Enabled = true;
                btn_KOTprint.Enabled = true;
                linkCancelKot.Enabled = true;
                lblCgstAmt.Text = "0";
                lbl_vat_amt.Text = "0";
                lblCgstValue.Text = "0";
                lblSgstValue.Text = "0";
                lbl_Vat_per.Text = "0";
                lblSgstAmt.Text = "0";
                lblFoodSCVal.Text = "0";
                lblLiquorSCVal.Text = "0";
                lblFoodSCAmt.Text = "0";
                lblLiquorSCAmt.Text = "0";
                btnMore.Enabled = true;
                dataGridView1.Enabled = true;




            }

            if (db.ChkDb_Value("select status from table_status where  t_id='" + tbl_no + "' and  status='Printing'"))
            {
               

                if (db.ChkDb_Value("select * from tbl_option where grp='" + "AfterPrint" + "' and status='Yes' "))
                {
                    btnMore.Enabled = false;
                    btnCancelOrder.Enabled = false;
                    linkCancelKot.Enabled = false;
                
                    dataGridView1.Enabled = false;
                }




            }

            if (db.ChkDb_Value("select discValue from table_order where t_id='" + tbl_no + "' and tableStatus='Processing' and order_id='" + label_order_id.Text + "'"))
            {
                txtDiscValue.Text = db.getDb_Value("select discValue from table_order where t_id='" + tbl_no + "' and tableStatus='Processing' and order_id='" + label_order_id.Text + "'").ToString();



            }
            else
            {
                txtDiscValue.Text = "0";

            }

            if (txtDiscValue.Text != "0")
            {
                chkPerc.Checked = true;
                lblStatus.Visible = true;
            }
            else
            {
                chkPerc.Checked = false;
                lblStatus.Visible = false;

            }

            if (db.ChkDb_Value("select discAmt from table_order where t_id='" + tbl_no + "' and tableStatus='Processing' and order_id='" + label_order_id.Text + "'"))
                txtDiscAmt.Text = db.getDb_Value("select discAmt from table_order where t_id='" + tbl_no + "' and tableStatus='Processing' and order_id='" + label_order_id.Text + "'").ToString();
            else
                txtDiscAmt.Text = "0";

            if (txtDiscAmt.Text != "0")
            {
                chkAmt.Checked = true;
                lblStatus.Visible = true;

            }
            else
            {
                chkAmt.Checked = false;
                lblStatus.Visible = false;

            }
            try
            {
                if (txtTableNo.Text != "0")
                {
                    ApplyDisc();
                }




                if (lbl_name.Text != "admin")
                {
                    if (db.ChkDb_Value("select * from table_status where t_id='" + tbl_no + "' and Status='Printing'"))
                    {
                        btn_add.Enabled = false;
                        btn_KOTprint.Enabled = false;
                        btnCancelOrder.Enabled = false;

                        dataGridView1.Columns[0].Visible = false;
                        dataGridView1.Columns[1].Visible = false;
                        linkCancelKot.Visible = false;
                        txtDiscValue.ReadOnly = true;
                        txtDiscAmt.ReadOnly = true;
                        chkPerc.Enabled = false;
                        chkAmt.Enabled = false;
                    }
                    else
                    {
                        btn_add.Enabled = true;
                        btn_KOTprint.Enabled = true;
                        btnCancelOrder.Enabled = true;
                        dataGridView1.Columns[0].Visible = true;
                        dataGridView1.Columns[1].Visible = true;
                        linkCancelKot.Visible = true;
                        txtDiscValue.ReadOnly = false;
                        txtDiscAmt.ReadOnly = false;
                        chkPerc.Enabled = true;
                        chkAmt.Enabled = true;

                    }

                }
                




            }
            catch (Exception ex)
            {
                MessageBox.Show("Please Check Order");
            }
        }

        string status = "", t_id = "";
        int cnt = 0;
        public void chk_tb_statusList()
        {
            try
            {
                sira = listView_items.Items.Count;
                if (listView1.SelectedItems.Count >= 1)
                {
                    selected_item = listView1.SelectedItems[0].Text;

                }
                int i = 0;
                SqlCommand cmd = new SqlCommand("select id, t_id,status,lblName from table_status where lblName='" + selected_item + "' and tblStatus='Active'", db.cn);
                db.cnopen();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read() == true)
                {
                    status = rd["status"].ToString();
                    t_id = rd["t_id"].ToString();
                    lbl = rd["lblName"].ToString();
                    //= int.Parse(rd["id"].ToString());                    
                    if (status == "Empty")
                    {
                        listView_items.Items[i].BackColor = Color.DarkCyan;
                       
                        i++;
                    }
                    if (status == "Processing")
                    {
                        listView_items.Items[i].BackColor = Color.Red;
                        i++;
                    }
                    if (status == "Printing")
                    {
                        listView_items.Items[i].BackColor = Color.Orange;
                        i++;
                    }
                    if (status == "Reserve")
                    {
                        listView_items.Items[i].BackColor = Color.Yellow;
                        i++;
                    }
                    //}


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
        }
        public void chk_tb_statusListLoad()
        {
            try
            {
                //sira = listView_items.Items.Count;

                int i = 0;
                SqlCommand cmd = new SqlCommand("select id, t_id,status,lblName from table_status where tblStatus='Active' ", db.cn);
                db.cnopen();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read() == true)
                {
                    status = rd["status"].ToString();
                    t_id = rd["t_id"].ToString();
                    lbl = rd["lblName"].ToString();
                    //= int.Parse(rd["id"].ToString());                    
                    if (status == "Empty")
                    {
                        listView_items.Items[i].BackColor = Color.DarkCyan;
                        i++;
                    }
                    if (status == "Processing")
                    {
                        listView_items.Items[i].BackColor = Color.Red;
                        i++;
                    }
                    if (status == "Printing")
                    {
                        listView_items.Items[i].BackColor = Color.Orange;
                        i++;
                    }
                    if (status == "Reserve")
                    {
                        listView_items.Items[i].BackColor = Color.Yellow;
                        i++;
                    }
                    //}


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
        }

        private void btn_tb1_Click(object sender, EventArgs e)
        {
            tableCall("1", btn_B1);
        }

        private void btn_tb2_Click(object sender, EventArgs e)
        {
            tableCall("2", btn_B2);

        }

        private void btn_tb3_Click(object sender, EventArgs e)
        {
            tableCall("3", btn_B3);

        }

        private void btn_tb4_Click(object sender, EventArgs e)
        {
            tableCall("4", btn_B4);

        }

        private void btn_tb5_Click(object sender, EventArgs e)
        {

            tableCall("5", btn_B5);

        }

        private void btn_tb6_Click(object sender, EventArgs e)
        {
            tableCall("6", btn_B6);
        }


        //private void btn_tb8_Click_1(object sender, EventArgs e)
        //{
        //    tableCall("8", btn_tb8);
        //}

        //private void btn_tb9_Click_1(object sender, EventArgs e)
        //{
        //    tableCall("9", btn_tb8);
        //}

        //private void btn_tb7_Click_1(object sender, EventArgs e)
        //{
        //    tableCall("7", btn_tb7);

        //}

        //private void btn_tb10_Click_1(object sender, EventArgs e)
        //{
        //    tableCall("10", btn_tb9);
        //}

        //private void btn_tb11_Click_1(object sender, EventArgs e)
        //{
        //    tableCall("11", btn_tb10);

        //}

        //private void btn_tb12_Click_1(object sender, EventArgs e)
        //{
        //    tableCall("12", btn_tb11);

        //}

        private void btn_tb13_Click_1(object sender, EventArgs e)
        {
            //tableCall("13", btn_tb13);
        }

        //private void btn_tb14_Click_1(object sender, EventArgs e)
        //{
        //    tableCall("14", btn_tb12);
        //}

        //private void btn_tb15_Click_1(object sender, EventArgs e)
        //{
        //    tableCall("15", btn_tb15);

        //}

        //private void btn_tb16_Click_1(object sender, EventArgs e)
        //{
        //    tableCall("16", btn_tb16);
        //}

        //private void btn_tb17_Click_1(object sender, EventArgs e)
        //{
        //    tableCall("17", btn_tb17);
        //}

        private void btn_tb18_Click_1(object sender, EventArgs e)
        {
            //tableCall("18", btn_HD1);
        }

        public void update_tb_status(string tb_st)
        {

            if (!db.ChkDb_Value("select * from table_status where status='Printing' and t_id='" + tb_st + "' "))
            {

                if (table_status != "Empty" && table_status != "Reserve")
                {
                    table_status = "Processing ";
                }
                else
                {
                    table_status = "Empty";
                }



                string qur = "UPDATE table_status SET status='" + table_status + "' WHERE t_id='" + tb_st + "'";
                //SqlConnection con = new SqlConnection(constr);
                //con.Open();

                SqlCommand cmd = new SqlCommand(qur, db.cn);
                db.cnopen();
                int cnt = (int)cmd.ExecuteNonQuery();
                db.cnclose();
                if (cnt != 0)
                {
                    //label_order_id.Text = "12";
                    chk_tb_status();
                }
                else
                {
                    // btn_tb1.BackColor = Color.Red;
                    chk_tb_status();
                }

            }
            //else
            //{
            //    if (table_status == "Processing")
            //    {
            //        table_status = "Processing";
            //    }
            //    else
            //    {
            //        table_status = "Empty";
            //    }

            //    string qur = "UPDATE table_status SET status='" + table_status + "' WHERE t_id='" + tb_st + "'";
            //    //SqlConnection con = new SqlConnection(constr);
            //    //con.Open();

            //    SqlCommand cmd = new SqlCommand(qur, db.cn);
            //    db.cnopen();
            //    int cnt = (int)cmd.ExecuteNonQuery();
            //    db.cnclose();
            //    if (cnt != 0)
            //    {
            //        //label_order_id.Text = "12";
            //        chk_tb_status();
            //    }
            //    else
            //    {
            //        // btn_tb1.BackColor = Color.Red;
            //        chk_tb_status();
            //    }

            //}
            total_bill();

        }


        public void update_status(string tb_st)
        {
            if (table_status != "Empty")
            {
                table_status = "Printing";
            }
            else
            {
                table_status = "Empty";
            }

            string qur = "UPDATE table_status SET status='" + table_status + "' WHERE t_id='" + tb_st + "'";
            //SqlConnection con = new SqlConnection(constr);
            //con.Open();

            SqlCommand cmd = new SqlCommand(qur, db.cn);
            db.cnopen();
            int cnt = (int)cmd.ExecuteNonQuery();
            db.cnclose();
            if (cnt != 0)
            {
                //label_order_id.Text = "12";
                //chk_tb_status();
                string status = db.getDbstatus_Value("select status from table_status where t_id='" + tb_st + "'");
                if (status == "Printing")
                {
                    pink_btns(tb_st);


                }
                else
                {
                    green_btns(tb_st);
                }
            }
            else
            {
                // btn_tb1.BackColor = Color.Red;
                chk_tb_status();
            }
            // i++;
            //this.dataGridView1.Rows.Clear();
            total_bill();
            // bind();
        }
        public void bind(string type)
        {
            try
            {

                //code hide by sagar as on 02012019

                //dataGridView1.DataSource = db.Displaygrid("SELECT        menu.m_name AS [Item Name], SUM(sales_item.qty) AS Qty, sales_item.rate AS Rate, SUM(sales_item.total_amount) AS Amount FROM            menu INNER JOIN   sales_item ON menu.menu_id = sales_item.menu_id GROUP BY menu.m_name, sales_item.rate,sales_item.order_id having        (sales_item.order_id = '" + label_order_id.Text + "') and SUM(sales_item.qty)<>'0'  ");
                dataGridView1.DataSource = db.Displaygrid(@"SELECT  menu.m_name AS [Item Name],  sales_item.qty AS Qty, sales_item.rate AS Rate, sales_item.total_amount  AS Amount ,sales_item.sales_id
                    FROM   menu INNER JOIN   sales_item ON menu.menu_id = sales_item.menu_id and sales_item.order_id = '" + label_order_id.Text + "'");

                dataGridView1.Columns[2].ReadOnly = true;
                // dataGridView1.Columns[3].ReadOnly = true;
                dataGridView1.Columns[2].Width = 230;
                dataGridView1.Columns[3].Width = 80;
                dataGridView1.Columns[4].Width = 80;
                dataGridView1.Columns[5].Width = 80;
                dataGridView1.Columns[5].ReadOnly = true;
                ((DataGridViewTextBoxColumn)dataGridView1.Columns[3]).MaxInputLength = 3;

                //label_total_bill.Text = db.getDb_Value("SELECT SUM(total_amount) FROM sales_item WHERE order_id='" + label_order_id.Text + "'").ToString();
                //lblTotalAmt.Text = label_total_bill.Text;
               
                db.cnclose();
                ApplyDisc();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error" + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void get_old_order_id()
        {
            order_id = 0;
            string qur = "Select top 1 order_id  from table_order where t_id='" + txtTableNo.Text + "' and tableStatus='Processing' order by order_id DESC";

            //SqlConnection con = new SqlConnection(constr);
            //con.Open();
            SqlCommand cmd = new SqlCommand(qur, db.cn);
            db.cnopen();
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read() == true)
            {
                order_id = int.Parse(rd["order_id"].ToString());
            }
            db.cnclose();

            label_order_id.Text = order_id.ToString();
        }

        void retrive_new_id()
        {
            double order_id = 0;
            //string qur = "Select * from table_order where order_type='Novat'";
            if (db.ChkDb_Value("Select top 1 order_id from table_order  order by  order_id desc"))
                order_id = db.GetUniqueId("Select top 1 order_id from table_order  order by  order_id desc");
            else
                order_id = 0;

            order_id++;

            label_order_id.Text = order_id.ToString(); //assign the order id here



        }

        void assign_orderId_toTable()
        {
            string WaiterID = "0";
            int wID = 0;
            try
            {

                if (db.ChkDb_Value("SELECT  w_id FROM waiter_dtls WHERE wname='" + cmbWaiterName.Text + "'"))
                {
                    WaiterID = "SELECT  w_id FROM waiter_dtls WHERE wname='" + cmbWaiterName.Text + "'";


                    wID = db.GetUniqueId(WaiterID);
                }

              //  db.insert("insert into table_order (order_id,t_id,timeing,w_id,order_type,tableStatus,discValue,discAmt,gst) values('" + label_order_id.Text + "','" + txtTableNo.Text + "','" + Convert.ToDateTime(label_date.Text).ToString("MM/dd/yyyy") + "'," + wID + ",'" + order_tb_status + "','0','0','0','0') ");


            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void get_table_status()
        {
            try
            {
                string qur = "select * from table_status where t_id='" + txtTableNo.Text + "'";
                //SqlConnection con = new SqlConnection(constr);
                //con.Open();
                string cur_status = "";
                SqlCommand cmd = new SqlCommand(qur, db.cn);
                db.cnopen();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read() == true)
                {
                    cur_status = rd["status"].ToString();


                }
                if (cur_status != "Empty")
                {
                    panelorder.Visible = true;
                    get_old_order_id();
                    get_waiter_id();
                    total_bill();
                }
                else
                {

                    retrive_new_id();
                    if (txtTableNo.Text != "0")
                        assign_orderId_toTable();
                    if (db.ChkDb_Value("select * from tbl_option where process_type='" + "By Mouse Click" + "'"))
                    {
                        test_orderForm item = new test_orderForm(label_order_id.Text, txtTableNo.Text, label_date.Text);//, textBox_waiter_id.Text);
                        Token_order_form item2 = new Token_order_form(label_order_id.Text, txtTableNo.Text);
                        ItemNameWise_Order itemNameOrder = new ItemNameWise_Order(label_order_id.Text, txtTableNo.Text);


                        if (db.ChkDb_Value("select status from tbl_option where grp= '" + "OperationMode" + "'  and status='" + "By Mouse Click" + "'"))
                        {
                            item.ShowDialog(this);
                        }
                        else if (db.ChkDb_Value("select status from tbl_option where grp='" + "OperationMode" + "' and status= '" + "By Item Code" + "'"))
                        {
                            item2.ShowDialog(this);
                        }
                        else
                        {
                            itemNameOrder.ShowDialog(this);
                        }
                        //if (item.orderConfirm)
                        //{//whn cancel btn click
                        //    table_status = "Processing";
                        //}

                        //else
                        //{
                            // chk in db is tbl process or not 
                            if (db.ChkDb_Value("select * from table_status where status = 'Empty' and t_id='" + txtTableNo.Text + "' "))
                            {
                                table_status = "Empty";
                                txtTableNo.Text = "0";
                            }

                        //}

                        chk_tb_status();
                    }
                    else
                    {
                        Token_order_form item = new Token_order_form(label_order_id.Text, txtTableNo.Text);
                        item.ShowDialog(this);

                        //if (item.orderConfirm)
                        //{//whn cancel btn click
                        //    table_status = "Processing";
                        //}

                        //else
                        //{
                            // chk in db is tbl process or not 
                            if (db.ChkDb_Value("select * from table_status where status = 'Empty' and t_id='" + txtTableNo.Text + "' "))
                            {
                                table_status = "Empty";
                                txtTableNo.Text = "0";
                            }

                       // }

                        chk_tb_status();

                    }

                    // if (db.ChkDb_Value("SELECT        t_id FROM  table_order WHERE       (t_id like '%P%') and order_id='" + label_order_id.Text + "' "))//  if (txtTableNo.Text == "100")//
                    if (db.ChkDb_Value("SELECT        t_id FROM  table_order WHERE       (t_id like '%T%') and order_id='" + label_order_id.Text + "' "))//  if (txtTableNo.Text == "100")//
                    {
                        DialogResult dlgresult = MessageBox.Show("You want to Deliver this Order ?  Note: Press yes: if u will deliver this order, No: if order is taking by customer ", "Parcel Order", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dlgresult == DialogResult.Yes)
                        {

                            AddCustmer addcustomer = new AddCustmer(label_order_id.Text, label_date.Text, label_time.Text);
                            addcustomer.ShowDialog();
                        }
                    }
                    db.cnclose();
                    get_waiter_id();
                    total_bill();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void get_new_status()
        {
            try
            {
                string qur = "select * from table_status where t_id='" + txtTableNo.Text + "'";
                string cur_status = "";
                SqlCommand cmd = new SqlCommand(qur, db.cn);
                db.cnopen();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read() == true)
                {
                    cur_status = rd["status"].ToString();


                }
                if (cur_status != "Empty")
                {
                    get_old_order_id();
                    get_waiter_id();
                    total_bill();
                }
                else
                {

                    retrive_new_id();
                    if (txtTableNo.Text != "0")
                        assign_orderId_toTable();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void total_bill() // retrive the total bill of each table
        {
            try
            {
                bind(null);
                if (txtTableNo.Text != "0")
                {
                    panelorder.Visible = true;
                }
                //label_total_bill.Text = db.getDb_Value("select sum(total_amount) from sales_item where order_id='" + label_order_id.Text + "'").ToString();
                //lblTotalAmt.Text = label_total_bill.Text;
                db.cnclose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        bool ordr = true;
        private void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                //if (db.ChkDb_Value("select * from tbl_option where process_type='" + "By Mouse Click" + "'"))
                //{
                  if (db.ChkDb_Value("select status from tbl_option where grp= '" + "OperationMode" + "'  and status='" + "By Mouse Click" + "'"))
                  {
                   // test_orderForm item = new test_orderForm(label_order_id.Text, txtTableNo.Text," "," "," ");//, textBox_waiter_id.Text);
                    test_orderForm item = new test_orderForm(label_order_id.Text, txtTableNo.Text, label_date.Text, ordr);
                    item.ShowDialog(this);
                }
                else
                {
                    Token_order_form item = new Token_order_form(label_order_id.Text, txtTableNo.Text);
                    item.ShowDialog(this);
                }

                btnApplyFunction();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void nonVageToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void vageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Menu mn = new Menu();
            mn.ShowDialog();
        }

        private void manageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //update_menu ud_menu = new update_menu();
            //ud_menu.ShowDialog();
        }

        //private void btn_tb_19_Click(object sender, EventArgs e)
        //{
        //    tableCall("19", btn_HD2);

        //}

        //private void btn_tb20_Click(object sender, EventArgs e)
        //{
        //    tableCall("20", btn_HD3);
        //}

        //private void btn_tb21_Click(object sender, EventArgs e)
        //{
        //    tableCall("21", btn_HD4);
        //}

        //private void btn_tb22_Click(object sender, EventArgs e)
        //{
        //    tableCall("22", btn_HD5);
        //}

        //private void btn_tb23_Click(object sender, EventArgs e)
        //{
        //    tableCall("23", btn_HD6);
        //}

        //private void btn_tb24_Click(object sender, EventArgs e)
        //{
        //    tableCall("24", btn_HD7);
        //}

        //private void btn_tb25_Click(object sender, EventArgs e)
        //{
        //    tableCall("25", btn_HD8);
        //}

        //private void btn_tb26_Click(object sender, EventArgs e)
        //{
        //    tableCall("26", btn_tb26);
        //}

        //private void button11_Click(object sender, EventArgs e)
        //{
        //    tableCall("27", btn_tb27);
        //}

        //private void button10_Click(object sender, EventArgs e)
        //{
        //    tableCall("28", btn_tb28);
        //}

        //private void button9_Click(object sender, EventArgs e)
        //{
        //    tableCall("29", btn_tb29);
        //}

        //private void button8_Click(object sender, EventArgs e)
        //{
        //    tableCall("30", btn_tb30);
        //}

        private void addToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Supplier_Add sup_add = new Supplier_Add();
            sup_add.ShowDialog();
        }

        private void manageToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            //Supplier_manage sup_mng = new Supplier_manage();
            //sup_mng.ShowDialog();
        }

        private void reportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Supplier_details sup_dtls = new Supplier_details();
            //sup_dtls.ShowDialog();
        }

        private void addToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Waiter_add w_add = new Waiter_add();
            w_add.ShowDialog();
        }

        private void manageToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            //    Waiter_manage w_mng = new Waiter_manage();
            //    w_mng.ShowDialog();
        }

        private void detailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //waiter_details w_dtls = new waiter_details();
            //w_dtls.ShowDialog();
        }

        private void prsentyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            waiter_prsenty w_prsenty = new waiter_prsenty();
            w_prsenty.ShowDialog();
        }

        private void paymentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //waitr_payment_1 payment = new waitr_payment_1();
            //payment.ShowDialog();


            Employmodule payment = new Employmodule();
            payment.ShowDialog();

            

        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //stock_Add stk = new stock_Add();
            //stk.ShowDialog();
        }

        private void manageToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //stock_manage stk_mng = new stock_manage();
            //stk_mng.Show();
        }

        private void reportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //stock_details1 stk_dts = new stock_details1();
            //stk_dts.ShowDialog();
        }

        private void searchToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }


        private void addToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            //waiter_prsenty wp = new waiter_prsenty();
            //wp.ShowDialog();
            //Muster2 pre = new Muster2();
            //pre.ShowDialog();
        }

        private void manageToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            //waiter_prsenty_dtls w_p_dtls = new waiter_prsenty_dtls();
            //w_p_dtls.ShowDialog();
        }

        private void comboBox_waiter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbWaiterName.SelectedItem != null) // chk the waiter id for the particuler table
                textBox_waiter_id.Text = cmbWaiterName.SelectedValue.ToString();
        }

        void get_waiter_id()
        {

            flag = 0;
            string qur = "SELECT * FROM  waiter_dtls where w_id=(select top 1 w_id from  sales_item where order_id='" + label_order_id.Text + "')";
            combowaiter.Text = db.getDbstatus_Value("SELECT ISNULL((SELECT Waiter FROM  table_order where order_id='" + label_order_id.Text + "'),0)");
           
            
       
            SqlCommand cmd = new SqlCommand(qur, db.cn);
            try
            {
                db.cnopen();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read() == true)
                {
                    textBox_waiter_id.Text = rd["w_id"].ToString();
                    cmbWaiterName.Text = rd["wname"].ToString();

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
                cmbWaiterName.Enabled = true;
            else
                cmbWaiterName.Enabled = true;

        }

        private void button_print_Click(object sender, EventArgs e)
        {
            billprint();
        }


        void billprint()
        {
            try
            {
                //LPrinter lp = new LPrinter();
                LPrinterY lp = new LPrinterY();
                lp.billid = float.Parse(label_order_id.Text);
                lp.tableno = (txtTableNo.Text);
                lp.GTotal = (label_total_bill.Text);
                //tejashri 13-04-2019
                String status = db.getDbstatus_Value("Select status from tbl_option where grp='DiscountReason'");
                String value = db.getDbstatus_Value("Select value from tbl_option where grp='DiscountReason'");
                if (status.Equals("Yes"))
                {

                    lp.discountReason = value;

                }

                float noPrint = db.getDb_Value("select value from tbl_option where grp='" + "BillPrintNo" + "'");
                for (int i = 0; i < noPrint; i++)
                {
                    lp.print();
                }
                get_table_status();
                string t_id = (txtTableNo.Text);
                pink_btns(t_id);
                table_status = "Printing";
                update_status((txtTableNo.Text.ToString()));

                if (txtTableNo.Text != "0")
                    ApplyDisc();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dailyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Cursor.Current = Cursors.WaitCursor;
            //Total_Coolection_report t = new Total_Coolection_report();
            //t.ShowDialog();
            //Cursor.Current = Cursors.Default;
        }

        private void purchaseItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //stock_details1 stDtls = new stock_details1();
            //stDtls.ShowDialog();
        }

        private void galaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //    GalaReport grpt = new GalaReport();
            //    grpt.ShowDialog();
        }

        private void invoiceDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //InvoiceDtls invoic = new InvoiceDtls();
            //invoic.ShowDialog();
        }
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void btnCancelOrder_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count >= 0)
                {
                    DialogResult dialogResult = MessageBox.Show("Do You Want To Cancel?", "Hotel", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                    if (dialogResult == DialogResult.Yes)
                    {
                        Reason reason_obj = new Reason();
                        reason_obj.ShowDialog();
                        if (reason_obj.reason != "")
                        {
                            db.insert("insert into Cancel_order ( order_id, t_id, amount, by_whome, date, reason, time) values('" + label_order_id.Text + "','" + txtTableNo.Text + "','" + label_total_bill.Text + "','" + lbl_name.Text + "','" + Convert.ToDateTime(label_date.Text).ToString("MM/dd/yyyy") + "','" + reason_obj.reason + "','" + label_time.Text + "')");
                           // db.insert("insert into CancelKOTDetails ([orderId],[tblNo],[menuName],[qty],[rate],[amount] ,[date],[waiterName],cancelKotTime,cancelKotReson) values('" + label_order_id.Text + "','" + txtTableNo.Text + "','" + lbl_nc_menu.Text + "','" + txtCancelQty.Text + "','" + lbl_ncMenuRate.Text + "','" + (float.Parse(lbl_ncMenuRate.Text) * float.Parse(txtCancelQty.Text)) + "','" + Convert.ToDateTime(label_date.Text).ToString("MM/dd/yyyy") + "','" + lbl_name.Text + "','" + Convert.ToDateTime(label_time.Text).ToString("hh:mm:ss") + "','" + cmb_cancelKotReson.Text + "')");
                            db.insert( @"  insert into CancelKOTDetails ([orderId],[tblNo],[menuName],[qty],[rate],[amount] ,[date],[waiterName],cancelKotTime,cancelKotReson)"
                                      + "select order_id,'" + txtTableNo.Text + "',m.m_name,qty,si.rate,si.total_amount,Date,'" + lbl_name.Text + "','" + label_time.Text + "','" + reason_obj.reason + "' "
                                      + " from sales_item si inner join menu m on si.menu_id=m.menu_id where order_id='" + label_order_id.Text + "'");
                          
                            //db.update(" update sales_item set  total_amount=0 where order_id='" + label_order_id.Text + "' ");
                            //db.update("update table_order set foodDiscValue='0',foodDiscAmt='0', liquorDiscValue='0',liquorDiscAmt='0',beveragesDiscValue='0' ,beveragesDiscAmt='0',gst=0 where order_id='" + label_order_id.Text + "' ");
                            //db.update("update table_order set vatAmt=0,serviceTaxVal='0', serviceTaxFoodVal='0', serviceTaxLiquorVal='0', serviceTaxAmt='0',  serviceTaxLiquorAmt='0', serviceTaxFoodAmt='0' where order_id='" + label_order_id.Text + "'");





                            db.update("Delete from sales_item where order_id='" + label_order_id.Text + "' ");
                            db.update("Delete from table_order where order_id='" + label_order_id.Text + "' ");


                            btn_add.Enabled = false;
                            btnCancelOrder.Enabled = false;

                            //update status of table here 
                            db.update("UPDATE table_status SET status='Empty' WHERE t_id='" + txtTableNo.Text + "'");
                            db.update("UPDATE table_order SET tableStatus='Empty' WHERE t_id='" + txtTableNo.Text + "' and order_id='" + label_order_id.Text + "'");

                            table_status = "Empty";

                            update_tb_status((txtTableNo.Text.ToString()));
                            chk_tb_status();
                            dataGridView1.DataSource = "";
                            txtTableNo.Text = "0";
                            label_order_id.Text = "";
                            label_total_bill.Text = "0";

                            chkAmt.Checked = false;
                            chkPerc.Checked = false;
                            txtDiscAmt.Text = "0";
                            txtDiscValue.Text = "0";

                            lblStatus.Visible = false;
                            lblTotalAmt.Text = "0";
                            lblDisc.Text = "0";
                        }
                    }

                }
                else
                {
                    MessageBox.Show("Please Add Items", "Hotel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnPayBill_Click(object sender, EventArgs e)
        {
            if (db.ChkDb_Value("select status  from tbl_option where grp='Printcompulsary' and status='Yes'"))
            {
                if (db.ChkDb_Value("select * from table_status where status='printing' and t_id='" + txtTableNo.Text + "'"))
                {
                    db.reduceMaterialFromStock(dataGridView1);  // send the dgv to the reduce matrial function 
                    bill_paid();
                }
                else
                {
                    MessageBox.Show("Take Bill Print First .....! ");
                }
            }
            else
            {
                db.reduceMaterialFromStock(dataGridView1);  // send the dgv to the reduce matrial function 
                bill_paid();
            }

            //rest all lable after print

            lblCgstAmt.Text = "0";
            lbl_vat_amt.Text = "0";
            lblCgstValue.Text = "0";
            lblSgstValue.Text = "0";
            lbl_Vat_per.Text = "0";
            lblSgstAmt.Text = "0";
            lblFoodSCVal.Text = "0";
            lblLiquorSCVal.Text = "0";
            lblFoodSCAmt.Text = "0";
            lblLiquorSCAmt.Text = "0";
            lbl_foodDisVal.Text = "0";
            lbl_foodDisAmt.Text = "0";


        }
        private void btnParcelOrder_Click(object sender, EventArgs e)
        {
            try
            {

                Welcome wel = new Welcome();
                btn_add.Enabled = true;
                btnCancelOrder.Enabled = true;
                txtTableNo.Text = "100";
                table_status = "Processing";
                get_table_status();
                update_tb_status((txtTableNo.Text.ToString()));
                chk_tb_status();
                if (db.ChkDb_Value("select discValue from table_order where t_id='" + txtTableNo.Text + "' and tableStatus='Processing' and order_id='" + label_order_id.Text + "'"))
                    txtDiscValue.Text = db.getDb_Value("select discValue from table_order where t_id='" + txtTableNo.Text + "' and tableStatus='Processing' and order_id='" + label_order_id.Text + "'").ToString();
                else
                    txtDiscValue.Text = "0";

                if (txtDiscValue.Text != "0")
                {
                    chkPerc.Checked = true;
                    lblStatus.Visible = true;
                }
                else
                {
                    chkPerc.Checked = false;
                    lblStatus.Visible = false;

                }

                if (db.ChkDb_Value("select status from table_status where  t_id='" + txtTableNo.Text + "' and  status='Empty'"))
                {
                    btnPayBill.Enabled = false;
                    btnCancelOrder.Enabled = false;
                    button_print.Enabled = false;
                    btn_KOTprint.Enabled = false;
                }
                else
                {
                    btnPayBill.Enabled = true;
                    btnCancelOrder.Enabled = true;
                    button_print.Enabled = true;
                    btn_KOTprint.Enabled = true;
                }
                if (db.ChkDb_Value("select discAmt from table_order where t_id='" + txtTableNo.Text + "' and tableStatus='Processing' and order_id='" + label_order_id.Text + "'"))
                    txtDiscAmt.Text = db.getDb_Value("select discAmt from table_order where t_id='" + txtTableNo.Text + "' and tableStatus='Processing' and order_id='" + label_order_id.Text + "'").ToString();
                else
                    txtDiscAmt.Text = "0";

                if (txtDiscAmt.Text != "0")
                {
                    chkAmt.Checked = true;
                    lblStatus.Visible = true;

                }
                else
                {
                    chkAmt.Checked = false;
                    lblStatus.Visible = false;
                }
                if (txtTableNo.Text == "0")
                {
                    btnPayBill.Enabled = false;
                    btnCancelOrder.Enabled = false;
                    button_print.Enabled = false;
                    btn_KOTprint.Enabled = false;
                }
                ApplyDisc();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        private void cmbWaiterName_MouseHover(object sender, EventArgs e)
        {
            cmbWaiterName.BackColor = Color.Red;
        }

        public string NCMenuSalesID = string.Empty;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            string itemName;
            double qty1, rate, amt;
            string menuSectionCategory;


            int i = dataGridView1.SelectedCells[0].RowIndex;
            qty1 = double.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
            itemName = dataGridView1.Rows[i].Cells[2].Value.ToString();
            rate = double.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString());
            amt = double.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString());
            NCMenuSalesID = dataGridView1.Rows[i].Cells["sales_id"].Value.ToString();
            //mark menu as NC Menu  code by sagar for CR mark menu as NC

            if (e.ColumnIndex == dataGridView1.Columns[2].Index)
            {
                pnl_ncMenuUpdate.Visible = true;

                if (db.ChkDb_Value("select *  from tbl_login where User_Name='" + lbl_name.Text + "' and type='admin'"))
                {
                    btnDeleteitem.Visible = true;
                }

                //item reson bind
                //cmb_ncItemReson
                db.comboFill(cmb_ncItemReson, "select reason from NC_itemDetails where reason<>''  group by reason", "NC_itemDetails", "reason", "reason");
                cmb_ncItemReson.Text = "";

                // cancel kot with reson

                db.comboFill(cmb_cancelKotReson, "select cancelKotReson from CancelKOTDetails  where cancelKotReson<>'' group by cancelKotReson", "CancelKOTDetails", "cancelKotReson", "cancelKotReson");
                cmb_cancelKotReson.Text = "";

                //bind running table list
                db.comboFill(cmb_tablesListforShift, @"select tob.t_id as tableNO,MAX(tob.order_id) as OrderID from table_order tob 
                                                inner join table_status ts on ts.t_id=tob.t_id and ts.status<>'Empty' 
                                                group by tob.t_id 
                                                ", "NC_itemDetails", "tableNO", "OrderID");
                cmb_tablesListforShift.Text = "";

                //bind shift table reson

                db.comboFill(cmbShiftitemReson, "select cancelKotReson from CancelKOTDetails  where cancelKotReson<>'' group by cancelKotReson", "CancelKOTDetails", "cancelKotReson", "cancelKotReson");
                cmbShiftitemReson.Text = "";


                //db.comboFill(cmbRunningTable, "select reason from NC_itemDetails group by reason", "NC_itemDetails", "reason", "reason");
                //cmb_ncItemReson.Text = "";


                lbl_nc_menu.Text = itemName;
                lbl_ncMenuQty.Text = qty1.ToString();
                lbl_ncMenuRate.Text = rate.ToString();
                lbl_ncMenuTotal.Text = amt.ToString();

                txtNcQtyforNC.Text = lbl_ncMenuQty.Text;

                txtShiftItemQty.Text = qty1.ToString();

                //get men usection and category from db 
                menuSectionCategory = db.getDbstatus_Value("select cat_name +'*'+FoodSection from category c inner join menu  m on m.category=c.cat_name and m.m_name='" + itemName + "'");
                lbl_ncMenuSection.Text = menuSectionCategory.Split('*')[1];
                lbl_ncMenucategory.Text = menuSectionCategory.Split('*')[0];
            }



            if (e.ColumnIndex == dataGridView1.Columns[0].Index)
            {


                KOtPrintCode_rePrint(NCMenuSalesID + "*Reprint");  // reprint particuler item

                //db.cnopen();        

                //DialogResult dlgresult = MessageBox.Show("Are you sure want to Delete?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //if (dlgresult == DialogResult.Yes)
                //{
                //    Reason reason_obj = new Reason(itemName, qty1);
                //    reason_obj.ShowDialog();

                //    try
                //    {
                //        if (reason_obj.reason != "")
                //        {
                //            db.insert("insert into DeletedItemMaster (orderId,tblNo,deleteDate,deleteTime,itemName,rate,qty,amount,userName,reason) values('" + label_order_id.Text + "','" + txtTableNo.Text + "','" + Convert.ToDateTime(label_date.Text).ToString("MM/dd/yyyy") + "','" + Convert.ToDateTime(label_time.Text).ToString("hh:mm:ss") + "','" + itemName + "','" + rate + "','" + qty1 + "','" + amt + "','" + lbl_name.Text + "','" + reason_obj.reason + "')");

                //            db.DeleteData("delete FROM sales_item where menu_id=(select menu_id from menu where m_name='" + itemName + "') and order_id='" + label_order_id.Text + "'", "sales_item");
                //            //MessageBox.Show("Item Deleted");

                //            name = db.getDbstatus_Value(" Select category from menu where menu_id=(select menu_id from menu where m_name='" + itemName + "') ");
                //            if (name == "Hard Drinks")
                //            {

                //                stockqty = db.getDb_Value("select qty from tbl_stock where item_name='" + itemName + "'").ToString();
                //                double sum = double.Parse(stockqty) + qty1;
                //                db.update("update tbl_stock set qty='" + sum + "'  where item_name='" + itemName + "'");


                //            }
                //        }
                //    }
                //    catch (Exception ex)
                //    {
                //        MessageBox.Show("Error  " + ex.Message);
                //    }
                //    db.cnclose();
                //    bind(null);
                //    btnApplyFunction();
                //}

            }
            //...............................
            if (e.ColumnIndex == dataGridView1.Columns[1].Index)
            {
                // string itemName;
                double oldqty;
                //double rate;
                double delAmt, delQty;


                db.cnopen();
                //  int i = dataGridView1.SelectedCells[0].RowIndex;
                itemName = dataGridView1.Rows[i].Cells[2].Value.ToString();
                qty1 = double.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
                rate = double.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString());
                amt = double.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString());
                oldqty = db.getDb_Value("SELECT   sales_item.qty   FROM      sales_item INNER JOIN    menu ON sales_item.menu_id = menu.menu_id   WHERE  (sales_item.order_id = '" + label_order_id.Text + "') AND (sales_item.sales_id = '" + NCMenuSalesID + "')");
                DialogResult dlgresult = MessageBox.Show("Are you sure want to Update?", "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgresult == DialogResult.Yes)
                {
                    if (oldqty > qty1)
                    {
                        bool flg = true;
                        Reason rsn = new Reason(itemName, qty1, flg);
                        rsn.ShowDialog();
                        if (rsn.reason != "")
                        {
                            delQty = oldqty - qty1;
                            delAmt = delQty * rate;
                            db.insert("insert into DeletedItemMaster([orderId],[tblNo],[deleteDate],[deleteTime],[itemName],[rate],[qty],[amount],[userName],[reason]) values('" + label_order_id.Text + "','" + txtTableNo.Text + "','" + Convert.ToDateTime(label_date.Text).ToString("MM/dd/yyyy") + "','" + Convert.ToDateTime(label_time.Text).ToString("hh:mm:ss") + "','" + itemName + "','" + rate + "','" + delQty + "','" + delAmt + "','" + lbl_name.Text + "','" + rsn.reason + "')");
                           // db.update("Update  sales_item set qty='" + qty1 + "' ,rate='" + rate + "',total_amount='" + (qty1 * rate) + "' where menu_id=(select menu_id from menu where m_name='" + itemName + "') and order_id='" + label_order_id.Text + "'"); //AND (sales_item.sales_id = '" + NCMenuSalesID + "')
                            db.update("Update  sales_item set qty='" + qty1 + "' ,rate='" + rate + "',total_amount='" + (qty1 * rate) + "' where (sales_item.sales_id = '" + NCMenuSalesID + "') and order_id='" + label_order_id.Text + "'");  
                        }
                    }
                    else
                    {
                        name = db.getDbstatus_Value(" Select category from menu where menu_id=(select menu_id from menu where m_name='" + itemName + "') ");
                        if (name == "Hard Drinks")
                        {

                            //...................get sales items qty

                            getsalesqty = db.getDb_Value("select qty from sales_item where menu_id=(select menu_id from menu where m_name='" + itemName + "') and order_id='" + label_order_id.Text + "'");
                            updatedqty = getsalesqty - qty1;
                            //..............................................//
                            //.................qty add in stocks



                            stockqty = (db.getDb_Value("select qty from tbl_stock where item_name='" + itemName + "'").ToString());
                            double sum = (updatedqty + double.Parse(stockqty));
                            db.update("update tbl_stock set qty='" + sum + "'  where item_name='" + itemName + "'");

                        }

                        ///..................................................//
                        try
                        {
                          //  db.update("Update  sales_item set qty='" + qty1 + "' ,rate='" + rate + "',total_amount='" + (qty1 * rate) + "' where menu_id=(select menu_id from menu where m_name='" + itemName + "') and order_id='" + label_order_id.Text + "'");
                            db.update("Update  sales_item set qty='" + qty1 + "' ,rate='" + rate + "',total_amount='" + (qty1 * rate) + "' where (sales_item.sales_id = '" + NCMenuSalesID + "') and order_id='" + label_order_id.Text + "'");  

                            // MessageBox.Show("Item Updated");
                            bind(null);


                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error  " + ex.Message);
                        }
                    }
                    bind(null);
                    db.cnclose();
                    btnApplyFunction();

                }

            }
            btnPayBill.Enabled = true;
            btnCancelOrder.Enabled = true;
            button_print.Enabled = true;
            btn_KOTprint.Enabled = true;

        }
        private void label_order_id_TextChanged(object sender, EventArgs e)
        {
            WNAME();

        }

        private void totalCollectionReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //    stock_details1 sd = new stock_details1();
            //    sd.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label_time.Text = DateTime.Now.ToString("hh:mm:ss");
            chk_tb_status();
            //chk_tb_statusList();
           // chk_tb_statusListLoad();
            if (syncStatus == "Yes")
                exeStatus = ProgramIsRunning(@syncExepath); //@"C:\Program Files\Default Company Name\SynchData\DataSynchronisation.exe"
            if (autoPrintKot == "Yes")
            {
                //KOtPrintCode("N"); // for print normal KOT
                //KOtPrintCode("C"); //for cancel kot

              autoBillPrintFromApp();
            }

        }

        public void autoBillPrintFromApp()
        {
            string billId = string.Empty;
            string tableNo = string.Empty;
            float noPrint = 0;
            string t_id = string.Empty;

            try
            {
                //LPrinter lp = new LPrinter();

                LPrinterY lp = new LPrinterY();
                // get auto bill print status
                DataTable getAutoBillPrintStatus = new DataTable();
                getAutoBillPrintStatus = db.Displaygrid("select bill_id,tableNo from tbl_billPrintStatus  where printStatus='N'");

                if (getAutoBillPrintStatus.Rows.Count != 0)
                {


                    for (int j = 0; j < getAutoBillPrintStatus.Rows.Count; j++)
                    {
                        billId = getAutoBillPrintStatus.Rows[j]["bill_id"].ToString();
                        lp.billid = float.Parse(billId);
                        lp.tableno = (getAutoBillPrintStatus.Rows[j]["tableNo"].ToString());

                        lp.GTotal = (label_total_bill.Text);

                        noPrint = db.getDb_Value("select value from tbl_option where grp='" + "BillPrintNo" + "'");
                        for (int i = 0; i < noPrint; i++)
                        {
                            lp.print();
                        }
                        get_table_status();
                        t_id = (txtTableNo.Text);
                        pink_btns(t_id);
                        table_status = "Printing";
                        update_status((txtTableNo.Text.ToString()));
                        ApplyDisc();

                        //update auto bill print status in tbl_billPrintStatus as Y 

                        db.update("update tbl_billPrintStatus set printStatus='Y' where bill_id='" + billId + "'");


                    }  // end for loop here 
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
        }

        bool exeStatus;
        private bool ProgramIsRunning(string FullPath)
        {
            string FilePath = Path.GetDirectoryName(FullPath);
            string FileName = Path.GetFileNameWithoutExtension(FullPath).ToLower();
            bool isRunning = false;

            Process[] pList = Process.GetProcessesByName(FileName);
            foreach (Process p in pList)
                if (p.MainModule.FileName.StartsWith(FilePath, StringComparison.InvariantCultureIgnoreCase))
                {
                    isRunning = true;
                    break;
                }
            return isRunning;
        }
        private void addRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Purchase_Form purchase = new Purchase_Form();
            purchase.ShowDialog();
        }

        private void purchaseChangesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // PurchaseChanges p = new PurchaseChanges();
            Purchase_Report rpt = new Purchase_Report();
            rpt.ShowDialog();

        }

        private void purchaseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Purchase_Report rpt = new Purchase_Report();
            rpt.ShowDialog();
        }

        private void waiterPaymentDetailsReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            //WaiterPayment_Report r = new WaiterPayment_Report();
            Waiter_Payment_Details r = new Waiter_Payment_Details();
            r.ShowDialog();
            Cursor.Current = Cursors.Default;
        }



        private void menuTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Menu_Type mtype = new Menu_Type();
            mtype.ShowDialog();
        }

        private void categoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Category cat = new Category();
            cat.ShowDialog();
        }

        private void logoutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
           // Odb.chkOnDbSt();
           // checkonline cs = new checkonline();
           // cs.Rdetails();


            // backup();
            this.Hide();
            login_test log = new login_test();
            log.ShowDialog();
            //Application.Exit();

        }

        private void kichenExpensesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dateWiseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Rpt_table__sales_date_wise tdate = new Rpt_table__sales_date_wise();
            //tdate.ShowDialog();
        }

        private void tableWiseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //    Rpt_table__sales_Table_wise table = new Rpt_table__sales_Table_wise();
            //    table.ShowDialog();
        }

        private void waiterWiseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Rpt_table__sales_name_wise name = new Rpt_table__sales_name_wise();
            //name.ShowDialog();
        }

        private void profitAndLossToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Profit_Loss_Report rpt = new Profit_Loss_Report();
            rpt.ShowDialog();
            Cursor.Current = Cursors.Default;
        }


        private void addExpensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Expenses exp = new Expenses();
            //exp.ShowDialog();
            genralExp GenralExp = new genralExp();
            GenralExp.Show();
        }

        private void expensesDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpensesDtls dtls = new ExpensesDtls();
            dtls.ShowDialog();
        }


        private void stockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void printBillToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Cursor.Current = Cursors.WaitCursor;
            //test_printSalesBill pBill = new test_printSalesBill();
            //pBill.ShowDialog();
            //Cursor.Current = Cursors.Default;
        }

        private void hotelDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Company_Info comp_info = new Company_Info();
            comp_info.ShowDialog();
        }

        private void receiptFormatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReceiptFormat rcformat = new ReceiptFormat();
            rcformat.ShowDialog();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rdbTeklogic op = new rdbTeklogic();
            op.ShowDialog();
            lblOpMode.Text = db.getDbstatus_Value("select process_type from tbl_option");
            getdate();

        }

        private void addCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddCustmer cust = new AddCustmer();
            cust.ShowDialog();
        }

        private void directoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Directory dir = new Directory();
            //dir.ShowDialog();
        }

        private void swapTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Table_Swap swap = new Table_Swap(this);
            swap.ShowDialog();
        }

        private void createGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            creatGroup grp = new creatGroup();
            grp.ShowDialog();
        }

        private void userToolStripMenuItem_Click(object sender, EventArgs e)
        {
            User_Creation user = new User_Creation();
            user.ShowDialog();
        }

        private void tableTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Table_Type type = new Table_Type();
            type.ShowDialog();
        }

        private void addCustomerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AddCustmer cust = new AddCustmer();
            cust.ShowDialog();
        }

        private void directoryToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //Directory dir = new Directory();
            //dir.ShowDialog();
        }

        private void directoryToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            SuppDirectory Suppdir = new SuppDirectory();
            Suppdir.ShowDialog();
        }



        private void purchaseReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //  Purchase_Report rpt=new Purchase_Report();

            //  Payment_Report rpt = new Payment_Report();
            Supplier_Payment_Master rpt = new Supplier_Payment_Master();
            rpt.ShowDialog();
        }

        private void receiptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Receipt rct = new Receipt();
            rct.ShowDialog();
        }

        private void menuDetailsReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Menu_Details rpt = new Menu_Details();
            rpt.ShowDialog();
        }

        private void newOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Token_order_form tokenOrder = new Token_order_form(this);
            tokenOrder.ShowDialog();

        }

        private void paymentToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            FinalBill bill = new FinalBill(this, dataGridView1);
            bill.ShowDialog();
        }

        private void txtTableNo_TextChanged(object sender, EventArgs e)
        {
            if (txtTableNo.Text == "0")
                btn_add.Enabled = false;
            else
                btn_add.Enabled = true;

        }

        private void txtTableNo_TabIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtTableNo_Leave(object sender, EventArgs e)
        {

        }

        private void txtTableNo_Enter(object sender, EventArgs e)
        {
            //if (txtTableNo != null)
            //    welcome_pageload();
        }

        private void balanceReportToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Balance_Report rpt = new Balance_Report();
            rpt.ShowDialog();
        }

        private void balanceReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Balance_Report rpt = new Balance_Report();
            rpt.ShowDialog();
        }

        private void manageOrdersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Manage_Order manageOrder = new Manage_Order();
            manageOrder.ShowDialog();
        }

        private void Welcome_FormClosing(object sender, FormClosingEventArgs e)
        {
           // Odb.chkOnDbSt();
           // checkonline cs = new checkonline();
            //cs.Rdetails();
            foreach (var process in Process.GetProcessesByName("DataSynchronisation"))//DataSynchronisation.exe
            {
                process.Kill();
            }
            Application.Exit();
            //  backup();
        }
        public void backup()
        {
            try
            {
                string DBName = db.getDbstatus_Value("select top 1 databasename from tbl_dbBackup");
                string PathName = db.getDbstatus_Value("select top 1  path from tbl_dbBackup");
                string dPathName = db.getDbstatus_Value("select top 1 dpath from tbl_dbBackup");
                bc.DBBackup(DBName, PathName);
                bc.EncryptFile(PathName, dPathName);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                MessageBox.Show("Check Database Backup Path");
            }
        }
        private void datewiseToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Rpt_table__sales_date_wise rpt = new Rpt_table__sales_date_wise();
            rpt.ShowDialog();
        }

        private void materialWiseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RPT_table_sales_materialwise rpt = new RPT_table_sales_materialwise();
            rpt.ShowDialog();
        }

        private void waiterWiseToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Rpt_table__sales_name_wise rpt = new Rpt_table__sales_name_wise();
            rpt.ShowDialog();
        }

        private void tableWiseToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Rpt_table__sales_Table_wise rpt = new Rpt_table__sales_Table_wise();
            rpt.ShowDialog();
        }

        private void accessControlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AccessControl accCobntrol = new AccessControl();
            accCobntrol.ShowDialog();
            Welcome welcome = new Welcome();
        }

        private void cancelOrderDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cancel_Order_Report cancel_order = new Cancel_Order_Report();
            cancel_order.ShowDialog();
        }

        private void addTAQ6ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tax_Deduction tax = new Tax_Deduction();
            tax.ShowDialog();
        }

        private void presentyDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //waiter_prsenty_dtls w_p_dtls = new waiter_prsenty_dtls();
            //w_p_dtls.ShowDialog();

            PresentyDetails pd = new PresentyDetails();
            pd.ShowDialog();
        }

        private void waiterPresetyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Muster2 pre = new Muster2();
            //pre.ShowDialog();

            EMPPRESENTY ep = new EMPPRESENTY();
            ep.ShowDialog();



        }

        private void issueKitchenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Issue_Kitchen issue = new Issue_Kitchen();
            issue.ShowDialog();
        }

        private void issueKitchenReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Issue_Kitchen_Details rpt = new Issue_Kitchen_Details();
            rpt.ShowDialog();
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



        private void dayEndToolStripMenuItem_Click(object sender, EventArgs e)
        {
         //   DayEnd_bkupServices dyend = new DayEnd_bkupServices();
         //   dyend.ShowDialog();

        }

        private void insentiveDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CRT_Insentive_dtls rpt = new CRT_Insentive_dtls();
            rpt.ShowDialog();
        }

        private void parcelOrderDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Parcel_order_Details dtl = new Parcel_order_Details();
            dtl.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Table_status tbl = new Table_status();
            tbl.ShowDialog();
        }

        private void databaseBackupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DatabaseBackup bkup = new DatabaseBackup();
            bkup.ShowDialog();
        }

        private void drinkStockReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drinkStockDetails drink = new drinkStockDetails();
            drink.ShowDialog();
        }

        //private void btn_tb31_Click(object sender, EventArgs e)
        //{
        //    tableCall("31", btn_tb31);
        //}

        //private void btn_tb32_Click(object sender, EventArgs e)
        //{
        //    tableCall("32", btn_tb32);
        //}

        //private void btn_tb33_Click(object sender, EventArgs e)
        //{
        //    tableCall("33", btn_tb33);
        //}

        //private void btn_tb34_Click(object sender, EventArgs e)
        //{
        //    tableCall("34", btn_tb34);
        //}

        //private void btn_tb35_Click(object sender, EventArgs e)
        //{
        //    tableCall("35", btn_tb35);
        //}

        private void btn_tb42_Click(object sender, EventArgs e)
        {
            //tableCall("42", btn_tb42);
        }

        //private void btn_tb41_Click(object sender, EventArgs e)
        //{
        //    tableCall("41", btn_tb41);
        //}

        //private void btn_tb40_Click(object sender, EventArgs e)
        //{
        //    tableCall("40", btn_tb40);
        //}

        //private void btn_tb39_Click(object sender, EventArgs e)
        //{
        //    tableCall("39", btn_tb39);
        //}

        //private void btn_tb38_Click(object sender, EventArgs e)
        //{
        //    tableCall("38", btn_tb38);
        //}

        //private void btn_tb37_Click(object sender, EventArgs e)
        //{

        //    tableCall("37", btn_tb37);
        //}

        //private void btn_tb36_Click(object sender, EventArgs e)
        //{
        //    tableCall("36", btn_tb36);
        //}

        private void addExpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            genralExp GenralExp = new genralExp();
            GenralExp.Show();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // customerBirthdayRemainder custRemainder = new customerBirthdayRemainder();
            // custRemainder.ShowDialog();
            BirthdayReminder bday = new BirthdayReminder();
            bday.ShowDialog();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            StockRemainder stockremainder = new StockRemainder();
            stockremainder.ShowDialog();
        }

        private void paymentToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            //payment pay = new payment();
            Purchase_Bill_Payment pay = new Purchase_Bill_Payment();
            pay.ShowDialog();
        }

        private void categorywiseReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // CategorywiseGrpReport categoryRpt = new CategorywiseGrpReport();
            Print_Reports categoryRpt = new Print_Reports();
            categoryRpt.ShowDialog();
        }

        private void sampleToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void btn_notification_Click(object sender, EventArgs e)
        {
            customerBirthdayRemainder custRemainder = new customerBirthdayRemainder();
            custRemainder.ShowDialog();
        }

        private void configurationPanelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Configuation_Panel panel = new Configuation_Panel();
            panel.ShowDialog();
        }

        private void addDrinkGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_Drink_Group drinkgrp = new Add_Drink_Group();
            drinkgrp.ShowDialog();
        }

        private void btn_selfCounter_Click(object sender, EventArgs e)
        {
            Counter_Order_Form counter = new Counter_Order_Form();
            counter.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            checkKotprint();
        }

        void checkKotprint()
        {
            if (label_order_id.Text != "0")
            {
                LPrinter lp = new LPrinter();
                lp.billid = float.Parse(label_order_id.Text);
                lp.tableno = (txtTableNo.Text);
                lp.wname = cmbWaiterName.Text;
                lp.print_kotFinalTable();
            }
            else
                MessageBox.Show("No order");
        }

        private void addSupplierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Supplier_Add sup_add = new Supplier_Add();
            sup_add.ShowDialog();
        }

        private void addCustomerToolStripMenuItem_Click_1(object sender, EventArgs e)
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

        private void updateMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Update_Menu _update = new Update_Menu();
            _update.ShowDialog();
        }

        private void linkLabel2_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                float driverRate, menuId, qty, amount;
                string m_nm;
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    m_nm = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    menuId = db.getDb_Value("select menu_id from menu where m_name='" + m_nm + "'");
                    driverRate = db.getDb_Value("Select driverRate from menu where m_name='" + m_nm + "'");
                    qty = db.getDb_Value("select qty from sales_item where menu_id='" + menuId + "' and order_id='" + label_order_id.Text + "'");
                    amount = driverRate * qty;

                    db.update("update sales_item set rate='" + driverRate + "',total_amount='" + amount + "' where  order_id='" + label_order_id.Text + "' and menu_id='" + menuId + "'");
                }
                bind(null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDriver_Click(object sender, EventArgs e)
        {
            try
            {
                float driverRate, menuId, qty, amount;
                string m_nm;
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    m_nm = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    menuId = db.getDb_Value("select menu_id from menu where m_name='" + m_nm + "'");
                    driverRate = db.getDb_Value("Select driverRate from menu where m_name='" + m_nm + "'");
                    qty = db.getDb_Value("select qty from sales_item where menu_id='" + menuId + "' and order_id='" + label_order_id.Text + "'");
                    amount = driverRate * qty;

                    db.update("update sales_item set rate='" + driverRate + "',total_amount='" + amount + "' where  order_id='" + label_order_id.Text + "' and menu_id='" + menuId + "'");
                }
                bind(null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Welcome_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.PageUp)//e.Control == true && 
            {
                if (db.ChkDb_Value("select *  from tbl_login where User_Name='" + lbl_name.Text + "' and type='admin'"))
                {
                    pnl_searchOrder.Visible = true;
                }
                //btnDriver.PerformClick();

            }
            if (e.KeyCode == Keys.PageDown)//e.Control == true && 
            {
               // btnNormal.PerformClick();
                pnl_searchOrder.Visible = false;
            }

            if (e.KeyCode == Keys.K)//e.Control == true && 
            {
              checkKotprint();

            }
            if (e.KeyCode == Keys.B)//e.Control == true && 
            {
                billprint();

            }
             
        }

        private void btnNormal_Click(object sender, EventArgs e)
        {
            try
            {
                float NonACRate, menuId, qty, amount;
                string m_nm;
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    m_nm = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    menuId = db.getDb_Value("select menu_id from menu where m_name='" + m_nm + "'");
                    NonACRate = db.getDb_Value("Select non_ACrate from menu where m_name='" + m_nm + "'");
                    qty = db.getDb_Value("select qty from sales_item where menu_id='" + menuId + "' and order_id='" + label_order_id.Text + "'");
                    amount = NonACRate * qty;

                    db.update("update sales_item set rate='" + NonACRate + "',total_amount='" + amount + "' where  order_id='" + label_order_id.Text + "' and menu_id='" + menuId + "'");
                }
                bind(null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            btnApplyFunction();
        }

        float totalAmountOfFood = 0;
        void btnApplyFunction()
        {
            //  float totalAmountOfFood = db.getDb_Value("SELECT        SUM(sales_item.total_amount) AS Expr1 FROM            menu INNER JOIN  sales_item ON menu.menu_id = sales_item.menu_id INNER JOIN  category ON menu.category = category.cat_name WHERE        (sales_item.order_id = '" + label_order_id.Text + "') AND (category.cat_name <> 'Hard Drinks')");
            try
            {
                float discAmt = 0;
                if (txtDiscValue.Text != "" && txtDiscAmt.Text != "")
                {
                    ApplyDisc();

                    if (chkPerc.Checked)
                        discAmt = (float.Parse(txtDiscValue.Text) * totalAmountOfFood / 100);
                    else
                        discAmt = float.Parse(txtDiscAmt.Text);

                    db.update("update table_order set discValue='" + txtDiscValue.Text + "',discAmt='" + (discAmt) + "' where order_id='" + label_order_id.Text + "' and t_id='" + txtTableNo.Text + "'");
                    if (txtDiscValue.Text != "0" || txtDiscAmt.Text != "0")
                        lblStatus.Visible = true;
                    // lblStatus.Text = "Discount Applied..";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void ApplyDisc()
        {
            float gstAmt, grdTot, serviceTaxAmt, vatAmt, liquorAmt, BevrageAmt;
            float foodDisVal, foodDisAmt, bevDisVal, bevDisAmt, liquorDisVal, liquorDisAmt;
            float foodSubTotal, bevSubTotal, LiquorSubTotal;
            string netAmt = "0";
            try
            {

                //     totalAmountOfFood = db.getDb_Value("SELECT  SUM(sales_item.total_amount) AS Expr1 FROM            menu INNER JOIN  sales_item ON menu.menu_id = sales_item.menu_id INNER JOIN  category ON menu.category = category.cat_name WHERE        (sales_item.order_id = '" + label_order_id.Text + "') AND (category.cat_name <> 'Hard Drinks')");
                // totalAmountOfFood = db.getDb_Value("SELECT  SUM(sales_item.total_amount) AS Expr1 FROM            menu INNER JOIN  sales_item ON menu.menu_id = sales_item.menu_id INNER JOIN  category ON menu.category = category.cat_name WHERE        (sales_item.order_id = '" + label_order_id.Text + "')    AND (category.FoodSection <> 'Liquor')");

              //  if (label_order_id.Text != "0")  //txtTableNo
                if (label_order_id.Text != "0" && txtTableNo.Text != "0" && db.ChkDb_Value("select * from table_order where order_id='" + label_order_id.Text + "'"))  //txtTableNo
                {

                    totalAmountOfFood = db.getDb_Value("SELECT  SUM(sales_item.total_amount) AS Expr1 FROM            menu INNER JOIN  sales_item ON menu.menu_id = sales_item.menu_id INNER JOIN  category ON menu.category = category.cat_name WHERE        (sales_item.order_id = '" + label_order_id.Text + "')    AND (category.FoodSection = 'Food')");
                    liquorAmt = db.getDb_Value("SELECT  SUM(sales_item.total_amount) AS Expr1 FROM            menu INNER JOIN  sales_item ON menu.menu_id = sales_item.menu_id INNER JOIN  category ON menu.category = category.cat_name WHERE        (sales_item.order_id = '" + label_order_id.Text + "')    AND (category.FoodSection = 'Liquor')");
                    BevrageAmt = db.getDb_Value("SELECT  SUM(sales_item.total_amount) AS Expr1 FROM            menu INNER JOIN  sales_item ON menu.menu_id = sales_item.menu_id INNER JOIN  category ON menu.category = category.cat_name WHERE        (sales_item.order_id = '" + label_order_id.Text + "')    AND (category.FoodSection = 'Beverages')");


                    float totalAmount = db.getDb_Value("SELECT   SUM(total_amount) AS Expr1 FROM   sales_item WHERE        (order_id = '" + label_order_id.Text + "')");
                    if (chkPerc.Checked)
                    {
                        float discamt = totalAmountOfFood * float.Parse(txtDiscValue.Text) / 100;
                        txtDiscAmt.Text = Math.Round(discamt).ToString();
                        //label_total_bill.Text = Math.Round(totalAmount - discamt).ToString();
                        netAmt = (totalAmountOfFood - float.Parse(txtDiscAmt.Text)).ToString();

                    }
                    if (chkAmt.Checked)
                    {
                        label_total_bill.Text = Math.Round(totalAmount - float.Parse(txtDiscAmt.Text)).ToString();
                    }
                    lblDisc.Text = txtDiscAmt.Text;
                    lblTotalAmt.Text = totalAmount.ToString();


                    //discount Applied here 



                    //get food discount val and amt
                    foodDisVal = db.getDb_Value("  select isnull(foodDiscValue,0)  from table_order where order_id= '" + label_order_id.Text + "'");
                    foodDisAmt = db.getDb_Value("  select isnull(foodDiscAmt,0)  from table_order where order_id= '" + label_order_id.Text + "'");


                    //get Beverage dis and amt

                    bevDisVal = db.getDb_Value("  select isnull(beveragesDiscValue,0)  from table_order where order_id= '" + label_order_id.Text + "'");
                    bevDisAmt = db.getDb_Value("  select isnull(beveragesDiscAmt,0)  from table_order where order_id= '" + label_order_id.Text + "'");

                    //get liquor dis and amt
                    liquorDisVal = db.getDb_Value("  select isnull(liquorDiscValue,0)  from table_order where order_id= '" + label_order_id.Text + "'");
                    liquorDisAmt = db.getDb_Value("  select isnull(liquorDiscAmt,0)  from table_order where order_id= '" + label_order_id.Text + "'");


                    // service tax vat gst code applied here on both food and liquor

                    //assign to welcome page veribls
                    lbl_foodDisVal.Text = foodDisVal.ToString();
                    lbl_BevDisVal.Text = bevDisVal.ToString();
                    lbl_LiquorDisVal.Text = liquorDisVal.ToString();

                    lbl_foodDisAmt.Text = foodDisAmt.ToString();
                    lbl_BevDisAmt.Text = bevDisAmt.ToString();
                    lbl_LiquorDisAmt.Text = liquorDisAmt.ToString();



                    foodSubTotal = totalAmountOfFood - foodDisAmt;
                    bevSubTotal = BevrageAmt - bevDisAmt;
                    LiquorSubTotal = liquorAmt - liquorDisAmt;


                    string value = "";
                    string ServiceTaxvalue = "";
                    string Vatvalue = "0";

                    float foodScVal = 0, liquorScVal = 0, foodScAmt = 0, liquorScAmt = 0;

                    if (db.ChkDb_Value("select * from tbl_option where grp='" + "Tax" + "' and status='Yes'"))
                    {
                        pnlGST.Visible = true;

                        value = db.getDbstatus_Value("select value from tbl_option where grp='" + "Tax" + "' and status='Yes'");
                        // ServiceTaxvalue = db.getDbstatus_Value("select ISNULL(select value  from tbl_option where grp='" + "ServiceTaxvalue" + "' and status='Yes'),0)");
                        ServiceTaxvalue = db.getDbstatus_Value("SELECT ISNULL((SELECT value FROM tbl_option WHERE grp = 'ServiceTaxvalue'  and status='Yes'),0)");


                        //for vat
                        if (db.ChkDb_Value("select value  from tbl_option where grp='" + "Vatvalue" + "' and status='Yes'"))
                        {
                            panelvat.Visible = true;
                            Vatvalue = db.getDbstatus_Value("select value  from tbl_option where grp='" + "Vatvalue" + "' and status='Yes'");
                        }
                        else { panelvat.Visible = false; }
                        //


                        //GET food sc val
                        //ServiceTaxvalue = db.getDbstatus_Value("select ISNULL(select value  from tbl_option where grp='" + "ServiceTaxvalue" + "' and status='Yes'),0)");

                        //yogesh
                        ServiceTaxvalue = db.getDbstatus_Value("SELECT ISNULL((SELECT value FROM tbl_option WHERE grp = 'ServiceTaxvalue'  and status='Yes'),0)");


                        // foodScVal = db.getDb_Value("select serviceTaxFoodVal  from table_order where order_id='" + label_order_id.Text + "'");
                        foodScVal = float.Parse(ServiceTaxvalue);


                        lblFoodSCVal.Text = foodScVal.ToString();

                        //yogesh
                        //get liquor sc val
                        // liquorScVal = db.getDb_Value("select serviceTaxLiquorVal  from table_order where order_id='" + label_order_id.Text + "'");

                        liquorScVal = float.Parse(ServiceTaxvalue);
                        lblLiquorSCVal.Text = liquorScVal.ToString();

                        lblCgstValue.Text = (float.Parse(value) / 2).ToString();
                        lblSgstValue.Text = (float.Parse(value) / 2).ToString();

                        // service tax and vat calculation 

                        lbl_st_per.Text = ServiceTaxvalue;
                        lbl_Vat_per.Text = Vatvalue;

                        netAmt = ((foodSubTotal + bevSubTotal) - float.Parse(txtDiscAmt.Text)).ToString();
                        //netAmt = (totalAmountOfFood - float.Parse(txtDiscAmt.Text)).ToString();
                        //gstAmt = (float.Parse(lblCgstValue.Text) * (totalAmountOfFood)) / 100;
                        gstAmt = (float.Parse(lblCgstValue.Text) * float.Parse(netAmt)) / 100;

                        //service tax
                        //   serviceTaxAmt = (float.Parse(ServiceTaxvalue) * (float.Parse(netAmt) + liquorAmt)) / 100;

                        serviceTaxAmt = (float.Parse(ServiceTaxvalue) * (foodSubTotal + bevSubTotal) / 100);

                        //vat
                        vatAmt = (float.Parse(Vatvalue) * LiquorSubTotal) / 100;


                        lblCgstAmt.Text = Math.Round(gstAmt, 2).ToString();
                        lblSgstAmt.Text = Math.Round(gstAmt, 2).ToString();

                        // service Tax Amt 

                        //lbl_serviceTaxAmt.Text = Math.Round(serviceTaxAmt, 2).ToString(); 
                        lbl_vat_amt.Text = Math.Round(vatAmt, 2).ToString();

                        //food Service Charge
                        foodScAmt = ((foodSubTotal + bevSubTotal) * foodScVal) / 100;
                        lblFoodSCAmt.Text = Math.Round(foodScAmt, 2).ToString();

                        //Liquor Service Charge
                        liquorScAmt = (LiquorSubTotal * liquorScVal) / 100;
                        lblLiquorSCAmt.Text = Math.Round(liquorScAmt, 2).ToString();

                        grdTot = float.Parse(lblCgstAmt.Text) + float.Parse(lblSgstAmt.Text);
                        //label_total_bill.Text = Math.Round(totalAmount + float.Parse(lblCgstAmt.Text) + float.Parse(lblSgstAmt.Text) - float.Parse(lblDisc.Text)).ToString();

                        //    label_total_bill.Text = Math.Round(totalAmount + float.Parse(lblCgstAmt.Text) + float.Parse(lblSgstAmt.Text) - float.Parse(lblDisc.Text) + float.Parse(lbl_serviceTaxAmt.Text) + float.Parse(lbl_vat_amt.Text)).ToString();

                        label_total_bill.Text = Math.Round(totalAmount + float.Parse(lblCgstAmt.Text) + float.Parse(lblSgstAmt.Text) - float.Parse(lblDisc.Text) + foodScAmt + float.Parse(lbl_vat_amt.Text) + liquorScAmt - foodDisAmt - bevDisAmt - liquorDisAmt).ToString();
                        //db.update("update table_order set gst='" + grdTot + "' where order_id='" + label_order_id.Text + "' and t_id='" + txtTableNo.Text + "'");
                        //    db.update("update table_order set gst='" + grdTot + "',serviceTaxAmt='" + serviceTaxAmt + "',vatAmt='" + vatAmt + "' where order_id='" + label_order_id.Text + "' and t_id='" + txtTableNo.Text + "'");
                        db.update("update table_order set gst='" + grdTot + "',serviceTaxAmt='" + serviceTaxAmt + "',vatAmt='" + vatAmt + "',serviceTaxFoodVal='" + foodScVal.ToString() + "', serviceTaxFoodAmt='" + foodScAmt.ToString() + "',serviceTaxLiquorVal='" + liquorScVal.ToString() + "',serviceTaxLiquorAmt='" + liquorScAmt.ToString() + "',DiscountReson='" + cmb_discountReson.Text + "' where order_id='" + label_order_id.Text + "' and t_id='" + txtTableNo.Text + "'");

                        if (txtTableNo.Text.Contains('T'))
                        {

                            db.update("update table_order set serviceTaxAmt='0',serviceTaxFoodVal='0', serviceTaxFoodAmt='0',serviceTaxLiquorVal='0',serviceTaxLiquorAmt='0' where order_id='" + label_order_id.Text + "' and t_id='" + txtTableNo.Text + "'");

                        }
                    } //if colde 



                    else
                    {
                        pnlGST.Visible = false;
                        if (db.ChkDb_Value("select value  from tbl_option where grp='" + "Vatvalue" + "' and status='Yes'"))
                        {
                            panelvat.Visible = true;
                            Vatvalue = db.getDbstatus_Value("select value  from tbl_option where grp='" + "Vatvalue" + "' and status='Yes'");
                            lbl_Vat_per.Text = Vatvalue;
                            vatAmt = (float.Parse(Vatvalue) * LiquorSubTotal) / 100;
                            lbl_vat_amt.Text = Math.Round(vatAmt, 2).ToString();

                            label_total_bill.Text = Math.Round(totalAmount - float.Parse(lblDisc.Text) + float.Parse(lbl_serviceTaxAmt.Text) + float.Parse(lbl_vat_amt.Text)).ToString();

                        }
                        panelvat.Visible = false;

                    }

                    if (db.ChkDb_Value("select * from tbl_option where grp='" + "Tax" + "' and status='No'"))
                    {
                        ApplyMoreDisc();
                        label_total_bill.Text = Math.Round(totalAmount + foodScAmt + float.Parse(lbl_vat_amt.Text) + liquorScAmt - foodDisAmt - bevDisAmt - liquorDisAmt).ToString();

                    }

                }
            }
            catch (Exception ex)
            {
               // throw ex;
            }
        }

        private void chkPerc_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPerc.Checked)
            {
                txtDiscValue.ReadOnly = false;
            }
            else
            {
                txtDiscValue.ReadOnly = true;
                txtDiscValue.Text = "0";
                txtDiscAmt.Text = "0";
                lblStatus.Visible = false;
                //total_bill();
            }
        }

        private void chkAmt_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAmt.Checked)
                txtDiscAmt.ReadOnly = false;
            else
            {
                txtDiscAmt.ReadOnly = true;
                txtDiscAmt.Text = "0";
                //total_bill();

            }
        }

        private void txtDiscValue_TextChanged(object sender, EventArgs e)
        {
            if (txtDiscValue.Text != "" && float.Parse(txtDiscValue.Text) <= 100)
            {
                // txtDiscAmt.Text= (float.Parse(txtDiscValue.Text) * float.Parse(label_total_bill.Text) / 100).ToString();
                ApplyDisc();
            }
            else
            {
                txtDiscValue.Text = "0";
            }
        }

        private void txtDiscAmt_TextChanged(object sender, EventArgs e)
        {
            if (txtDiscAmt.Text != "")
            {
                // txtDiscValue.Text = (float.Parse(txtDiscAmt.Text) * 100 / float.Parse(label_total_bill.Text)).ToString();
                ApplyDisc();
            }
            else
            {
                txtDiscAmt.Text = "0";
            }
        }

        private void deletedItemReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Deleted_Item_Details dtl = new Deleted_Item_Details();
            dtl.ShowDialog();
        }

        private void linkLabel3_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Cancel_KOT _kot = new Cancel_KOT(label_order_id.Text, txtTableNo.Text, cmbWaiterName.Text);
            _kot.ShowDialog();
            bind(null);
            btnApplyFunction();
        }

        private void cancelKOTReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cancel_KOT_Details dtl = new Cancel_KOT_Details();
            dtl.ShowDialog();
        }

        private void reminderSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReminderSetting rmd = new ReminderSetting();
            rmd.ShowDialog();
        }

        private void showUtilityReminderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowUtilityReminder srm = new ShowUtilityReminder();
            srm.ShowDialog();
        }

        private void txtDiscValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);
        }

        private void txtDiscAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);
        }

        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void smsTempletToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SMS_Template_Master stm = new SMS_Template_Master();
            stm.ShowDialog();
        }

        private void sMSTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SMSTemplet st = new SMSTemplet();
            st.ShowDialog();
        }

        private void sMSURLSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SMS_URL_Co_Ordinates surl = new SMS_URL_Co_Ordinates();
            surl.ShowDialog();
        }

        private void excelReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
         //   Excel_Report er = new Excel_Report();
         //   er.ShowDialog();
        }

        private void duplicateBillToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DummyBillPrint dummy = new DummyBillPrint();
            dummy.ShowDialog();
        }

        private void btn_tbP1_Click(object sender, EventArgs e)
        {
            tableCall("P1", btn_tbP1);
        }

        private void btn_tbP2_Click(object sender, EventArgs e)
        {
            tableCall("P2", btn_tbP2);
        }

        private void btn_tbP3_Click(object sender, EventArgs e)
        {
            tableCall("P3", btn_tbP3);
        }

        private void btn_tbP4_Click(object sender, EventArgs e)
        {
            tableCall("P4", btn_tbP4);
        }

        private void btn_tbP5_Click(object sender, EventArgs e)
        {
            tableCall("P5", btn_tbP5);
        }

        private void btn_tbP6_Click(object sender, EventArgs e)
        {
            tableCall("P6", btn_tbP6);
        }

        private void btn_tbP7_Click(object sender, EventArgs e)
        {
            tableCall("P7", btn_tbP7);
        }

        private void configurationSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfigurationSetting cs = new ConfigurationSetting();
            cs.ShowDialog();
        }
        float qty = 0, rate = 0, amount = 0;
        private void dataGridView1_CellEndEdit_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                int i = dataGridView1.SelectedCells[0].RowIndex;
                rate = float.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString());
                if (dataGridView1.Rows[i].Cells[3].Value.ToString() == "" || dataGridView1.Rows[i].Cells[3].Value.ToString() == "0")
                {
                    MessageBox.Show("You Can't Insert Empty Or ZERO Qty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    qty = 1;
                    dataGridView1.Rows[i].Cells[3].Value = qty;
                    amount = rate * qty;
                    dataGridView1.Rows[i].Cells[5].Value = amount.ToString();
                }
                else
                {
                    qty = float.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
                    amount = rate * qty;
                    dataGridView1.Rows[i].Cells[5].Value = amount.ToString();
                }

                //if (qty != 0)
                //{
                //    MessageBox.Show("Please Click On Update Button");
                //}


                btnPayBill.Enabled = false;
                btnCancelOrder.Enabled = false;
                button_print.Enabled = false;
                btn_KOTprint.Enabled = false;


            }


            if (e.ColumnIndex == 4)
            {
                int i = dataGridView1.SelectedCells[0].RowIndex;
                qty = float.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());


                if (dataGridView1.Rows[i].Cells[4].Value.ToString() == "" || dataGridView1.Rows[i].Cells[4].Value.ToString() == "0")
                {
                    //rate = 0;
                    //dataGridView1.Rows[i].Cells[3].Value = qty;
                    MessageBox.Show("You Can't Insert Empty Or ZERO Rate", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    rate = float.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString());
                    amount = rate * qty;
                    dataGridView1.Rows[i].Cells[5].Value = amount.ToString();
                }

                //if (rate != 0)
                //{
                //    MessageBox.Show("Please Click On Update Button");
                //}

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

        private void userMailIdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserMailId mail = new UserMailId();
            mail.ShowDialog();
        }

        private void btn_tbP8_Click(object sender, EventArgs e)
        {
            tableCall("P8", btn_tbP8);
        }

        //private void btn_tbP9_Click(object sender, EventArgs e)
        //{
        //    tableCall("P9", btn_tbP9);
        //}

        //private void btn_tbP10_Click(object sender, EventArgs e)
        //{
        //    tableCall("P10", btn_tbP10);
        //}

        //private void btn_tbP11_Click(object sender, EventArgs e)
        //{
        //    tableCall("P11", btn_tbP11);
        //}

        //private void btn_tbP12_Click(object sender, EventArgs e)
        //{
        //    tableCall("P12", btn_tbP12);
        //}

        //private void btn_tbP13_Click(object sender, EventArgs e)
        //{
        //    tableCall("P13", btn_tbP13);
        //}

        //private void btn_tbP14_Click(object sender, EventArgs e)
        //{
        //    tableCall("P14", btn_tbP14);
        //}

        //private void btn_tbP15_Click(object sender, EventArgs e)
        //{
        //    tableCall("P15", btn_tbP15);
        //}

        private void timerLableMove_Tick_1(object sender, EventArgs e)
        {

        }


        void KOtPrintCode(string printStatus)
        {
            LPrinter lp = new LPrinter();
            int count = 0, tblCnt = 0;
            string printQuery = string.Empty;
            string qryCheckCategoryOther = string.Empty;
            string getPlrintTableStauts = string.Empty;
            lp.printKotType = printStatus;   //assign the print status so it will print on kot 

            try
            {
                string[] OrdId = new string[200];
                string[] wid = new string[200];
                string[] kotId = new string[200];
                string wtrName = "", tblNo = "";

                db.cnopen();
                SqlCommand cmdKot = new SqlCommand("SELECT  w_id, order_id,kot_id FROM  tbl_kotPrintStatus WHERE (kotPrint = '" + printStatus + "')", db.cn);
                SqlDataReader rdKt = cmdKot.ExecuteReader();
                while (rdKt.Read())
                {

                    wid[tblCnt] = rdKt["w_id"].ToString();
                    OrdId[tblCnt] = rdKt["order_id"].ToString();
                    kotId[tblCnt] = rdKt["kot_id"].ToString();
                    tblCnt++;
                }
                db.cnclose();

                for (int k = 0; k < tblCnt; k++)
                {
                    //wtrName = db.getDbstatus_Value("SELECT waiter_dtls.wname FROM table_order INNER JOIN  waiter_dtls ON table_order.w_id = waiter_dtls.w_id WHERE  (table_order.w_id = '" + wid[k] + "') and  table_order.order_id='" + OrdId[k] + "'");
                    wtrName = db.getDbstatus_Value("select wname from waiter_dtls where w_id='" + wid[k] + "'");
                    tblNo = db.getDbstatus_Value("SELECT  t_id FROM table_order WHERE (order_id = '" + OrdId[k] + "')");
                    string[] printNm = new string[10];
                    count = 0;
                    db.cnopen();
                    //  SqlCommand cmdowner = new SqlCommand("SELECT DISTINCT category.printerName  FROM category INNER JOIN   tbl_temporder ON category.cat_name = tbl_temporder.category WHERE (tbl_temporder.order_id = '" + OrdId[k] + "')", db.cn);

                    if (printStatus == "N")
                        printQuery = "select DISTINCT cat.printerName from sales_item si inner join menu m on si.menu_id=m.menu_id inner join category cat on m.category=cat.cat_name and si.order_id='" + OrdId[k] + "'";
                    else
                        printQuery = "select  DISTINCT cat.printerName  from CancelKOTDetails cnk   inner join menu m on cnk.menuName=m.m_name  inner join category cat on m.category=cat.cat_name and cnk.orderId='" + OrdId[k] + "'";


                    SqlCommand cmdowner = new SqlCommand(printQuery, db.cn);

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
                        if (printStatus == "N")
                            qryCheckCategoryOther = "SELECT tbl_temporder.menuid, tbl_temporder.qty, tbl_temporder.m_name, tbl_temporder.w_id, tbl_temporder.order_id, tbl_temporder.category , tbl_temporder.ordersuggestion FROM category INNER JOIN  tbl_temporder ON category.cat_name = tbl_temporder.category where printerName='" + arr[i] + "' and order_id = '" + OrdId[k] + "' ";
                        else
                            qryCheckCategoryOther = "select  cnk.qty as qty,cnk.menuName as m_name,cnk.orderId,'' as ordersuggestion    from CancelKOTDetails cnk   inner join menu m on cnk.menuName=m.m_name  inner join category cat on m.category=cat.cat_name and cnk.orderId='" + OrdId[k] + "' and cat.printerName='" + arr[i] + "'";

                        lp.billid = float.Parse(OrdId[k]);
                        lp.tableno = tblNo;
                        lp.wname = wtrName;
                        if (db.ChkDb_Value(qryCheckCategoryOther))
                        {
                            lp.printOrder_kot(qryCheckCategoryOther, arr[i], "");
                        }
                    }
                    if (printStatus == "N")
                    {
                        db.update("Update tbl_kotPrintStatus Set kotPrint = 'Y' WHERE  order_id='" + OrdId[k] + "' and kotPrint = 'N' ");
                        db.insert("delete from tbl_temporder where order_id='" + OrdId[k] + "'");
                    }
                    else
                    {
                        db.update("Update tbl_kotPrintStatus Set kotPrint = 'CY' WHERE  order_id='" + OrdId[k] + "' and  kotPrint = 'C'");

                    }


                }
            }
            catch (Exception Ex)
            {
                db.insert("insert into tbl_eventlog (pageName,logType,logDetails,logDate) values('Kot Print','Exception','" + Ex + "','" + DateTime.Now + "')");
                MessageBox.Show("There is unhandle Error Please check table order before processing.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void label_order_id_Click(object sender, EventArgs e)
        {



        }

        private void btn_tb1_Click_1(object sender, EventArgs e)
        {
            tableCall("1", btn_B1);



        }

        private void btn_tb2_Click_1(object sender, EventArgs e)
        {
            tableCall("2", btn_B2);




        }

        private void btn_tb3_Click_1(object sender, EventArgs e)
        {
            tableCall("3", btn_B3);

        }

        private void btn_tb4_Click_1(object sender, EventArgs e)
        {
            tableCall("4", btn_B4);


        }

        private void btn_tb5_Click_1(object sender, EventArgs e)
        {
            tableCall("5", btn_B5);

        }

        private void btn_tb6_Click_1(object sender, EventArgs e)
        {
            tableCall("6", btn_B6);


        }

        //private void btn_tb7_Click(object sender, EventArgs e)
        //{
        //    tableCall("7", btn_tb7);

        //}

        //private void btn_tb8_Click(object sender, EventArgs e)
        //{
        //    tableCall("8", btn_tb8);

        //}

        //private void btn_tb9_Click(object sender, EventArgs e)
        //{
        //    tableCall("9", btn_tb9);

        //}

        //private void btn_tb10_Click(object sender, EventArgs e)
        //{
        //    tableCall("10", btn_tb10);



        //}

        //private void btn_tb11_Click(object sender, EventArgs e)
        //{
        //    tableCall("11", btn_tb11);

        //}

        //private void btn_tb12_Click(object sender, EventArgs e)
        //{
        //    tableCall("12", btn_tb12); 
        //}

        //private void btn_HD1_Click(object sender, EventArgs e)
        //{


        //    tableCall("HD1", btn_HD1);

        //}

        //private void btn_HD2_Click(object sender, EventArgs e)
        //{
        //    tableCall("HD2", btn_HD2);


        //}

        //private void btn_HD3_Click(object sender, EventArgs e)
        //{
        //    tableCall("HD3", btn_HD3);

        //}

        //private void btn_HD4_Click(object sender, EventArgs e)
        //{
        //    tableCall("HD4", btn_HD4);

        //}

        //private void btn_HD5_Click(object sender, EventArgs e)
        //{
        //    tableCall("HD5", btn_HD5);

        //}

        //private void btn_HD6_Click(object sender, EventArgs e)
        //{
        //    tableCall("HD6", btn_HD6);

        //}

        //private void btn_HD7_Click(object sender, EventArgs e)
        //{
        //    tableCall("HD7", btn_HD7);

        //}

        //private void btn_HD8_Click(object sender, EventArgs e)
        //{
        //    tableCall("HD8", btn_HD8);
        //}

        private void btn_tbP1_Click_1(object sender, EventArgs e)
        {


            tableCall("P1", btn_tbP1);


        }

        private void btn_tbP2_Click_1(object sender, EventArgs e)
        {
            tableCall("P2", btn_tbP2);

        }

        private void btn_tbP3_Click_1(object sender, EventArgs e)
        {
            tableCall("P3", btn_tbP3);

        }

        private void btn_tbP4_Click_1(object sender, EventArgs e)
        {
            tableCall("P4", btn_tbP4);

        }

        private void btn_tbP5_Click_1(object sender, EventArgs e)
        {
            tableCall("P5", btn_tbP5);

        }

        private void btn_tbP6_Click_1(object sender, EventArgs e)
        {
            tableCall("P6", btn_tbP6);

        }

        private void btn_tbP7_Click_1(object sender, EventArgs e)
        {
            tableCall("P7", btn_tbP7);

        }

        private void btn_tbP8_Click_1(object sender, EventArgs e)
        {
            tableCall("P8", btn_tbP8);
        }

        private void btn_tb21_Click_1(object sender, EventArgs e)
        {
            //tableCall("tb21", btn_tb21); 
        }

        private void button53_Click(object sender, EventArgs e)
        {
            Panelsections.Visible = true;
            PanelParcel.Visible = false;
        }

        private void button54_Click(object sender, EventArgs e)
        {
            Panelsections.Visible = false;
            PanelParcel.Visible = true;
        }

        private void btn_B1_Click(object sender, EventArgs e)
        {
            tableCall("B1", btn_B1);

        }

        private void btn_B2_Click(object sender, EventArgs e)
        {
            tableCall("B2", btn_B2);

        }

        private void btn_B3_Click(object sender, EventArgs e)
        {
            tableCall("B3", btn_B3);

        }

        private void btn_B4_Click(object sender, EventArgs e)
        {
            tableCall("B4", btn_B4);

        }

        private void btn_B5_Click(object sender, EventArgs e)
        {
            tableCall("B5", btn_B5);

        }

        private void btn_B6_Click(object sender, EventArgs e)
        {
            tableCall("B6", btn_B6);

        }

        private void btn_B7_Click(object sender, EventArgs e)
        {
            tableCall("B7", btn_B7);

        }

        private void btn_B8_Click(object sender, EventArgs e)
        {
            tableCall("B8", btn_B8);

        }

        private void btn_B9_Click(object sender, EventArgs e)
        {
            tableCall("B9", btn_B9);

        }

        private void btn_B10_Click(object sender, EventArgs e)
        {
            tableCall("B10", btn_B10);

        }

        private void btn_B11_Click(object sender, EventArgs e)
        {
            tableCall("B11", btn_B11);

        }

        private void btn_B12_Click(object sender, EventArgs e)
        {
            tableCall("B12", btn_B12);

        }

        private void btn_B13_Click(object sender, EventArgs e)
        {
            tableCall("B13", btn_B13);

        }

        private void btn_B14_Click(object sender, EventArgs e)
        {
            tableCall("B14", btn_B14);


        }

        private void btn_B15_Click(object sender, EventArgs e)
        {
            tableCall("B15", btn_B15);
        }

        private void btn_G1_Click(object sender, EventArgs e)
        {


            tableCall("G1", btn_G1);

        }

        private void btn_G2_Click(object sender, EventArgs e)
        {
            tableCall("G2", btn_G2);

        }

        private void btn_G3_Click(object sender, EventArgs e)
        {
            tableCall("G3", btn_G3);

        }

        private void btn_G4_Click(object sender, EventArgs e)
        {
            tableCall("G4", btn_G4);

        }

        private void btn_G5_Click(object sender, EventArgs e)
        {
            tableCall("G5", btn_G5);

        }

        private void btn_G6_Click(object sender, EventArgs e)
        {
            tableCall("G6", btn_G6);

        }

        private void btn_G7_Click(object sender, EventArgs e)
        {
            tableCall("G7", btn_G7);

        }

        private void btn_G8_Click(object sender, EventArgs e)
        {
            tableCall("G8", btn_G8);

        }

        private void btn_G9_Click(object sender, EventArgs e)
        {
            tableCall("G9", btn_G9);

        }

        private void btn_G10_Click(object sender, EventArgs e)
        {
            tableCall("G10", btn_G10);
        }

        private void btn_O1_Click(object sender, EventArgs e)
        {
            tableCall("O1", btn_O1);


        }

        private void btn_O2_Click(object sender, EventArgs e)
        {
            tableCall("O2", btn_O2);

        }

        private void btn_O3_Click(object sender, EventArgs e)
        {
            tableCall("O3", btn_O3);

        }

        private void btn_O4_Click(object sender, EventArgs e)
        {
            tableCall("O4", btn_O4);

        }

        private void btn_O5_Click(object sender, EventArgs e)
        {
            tableCall("O5", btn_O5);

        }

        private void btn_O6_Click(object sender, EventArgs e)
        {
            tableCall("O6", btn_O6);

        }

        private void btn_O7_Click(object sender, EventArgs e)
        {
            tableCall("O7", btn_O7);

        }

        private void btn_O8_Click(object sender, EventArgs e)
        {
            tableCall("O8", btn_O8);

        }

        private void btn_O9_Click(object sender, EventArgs e)
        {
            tableCall("O9", btn_O9);

        }

        private void btn_O10_Click(object sender, EventArgs e)
        {
            tableCall("O10", btn_O10);

        }

        private void btn_O11_Click(object sender, EventArgs e)
        {
            tableCall("O11", btn_O11);

        }

        private void btn_O12_Click(object sender, EventArgs e)
        {
            tableCall("O12", btn_O12);

        }

        private void btn_O13_Click(object sender, EventArgs e)
        {
            tableCall("O13", btn_O13);

        }

        private void btn_O14_Click(object sender, EventArgs e)
        {
            tableCall("O14", btn_O14);

        }

        private void btn_O15_Click(object sender, EventArgs e)
        {
            tableCall("O15", btn_O15);

        }

        private void btn_O16_Click(object sender, EventArgs e)
        {
            tableCall("O16", btn_O16);

        }

        private void btn_O17_Click(object sender, EventArgs e)
        {
            tableCall("O17", btn_O17);
        }

        private void btn_L1_Click(object sender, EventArgs e)
        {
            tableCall("L1", btn_L1);

        }

        private void btn_L2_Click(object sender, EventArgs e)
        {
            tableCall("L2", btn_L2);

        }

        private void btn_L3_Click(object sender, EventArgs e)
        {
            tableCall("L3", btn_L3);

        }

        private void btn_L4_Click(object sender, EventArgs e)
        {
            tableCall("L4", btn_L4);

        }

        private void btn_L5_Click(object sender, EventArgs e)
        {
            tableCall("L5", btn_L5);

        }

        private void btn_L6_Click(object sender, EventArgs e)
        {
            tableCall("L6", btn_L6);

        }

        private void btn_L7_Click(object sender, EventArgs e)
        {
            tableCall("L7", btn_L7);

        }

        private void btn_L8_Click(object sender, EventArgs e)
        {
            tableCall("L8", btn_L8);

        }

        private void btn_L9_Click(object sender, EventArgs e)
        {
            tableCall("L9", btn_L9);

        }

        private void btn_L10_Click(object sender, EventArgs e)
        {
            tableCall("L10", btn_L10);

        }

        private void btn_L11_Click(object sender, EventArgs e)
        {
            tableCall("L11", btn_L11);

        }

        private void btn_L12_Click(object sender, EventArgs e)
        {
            tableCall("L12", btn_L12);
        }

        private void btn_tbP1_Click_2(object sender, EventArgs e)
        {
            tableCall("P1", btn_tbP1);
        }

        private void btn_tbP2_Click_2(object sender, EventArgs e)
        {

            tableCall("P2", btn_tbP2);


        }

        private void btn_tbP3_Click_2(object sender, EventArgs e)
        {
            tableCall("P3", btn_tbP3);

        }

        private void btn_tbP4_Click_2(object sender, EventArgs e)
        {
            tableCall("P4", btn_tbP4);

        }

        private void btn_tbP5_Click_2(object sender, EventArgs e)
        {
            tableCall("P5", btn_tbP5);

        }

        private void btn_tbP6_Click_2(object sender, EventArgs e)
        {
            tableCall("P6", btn_tbP6);

        }

        private void btn_tbP7_Click_2(object sender, EventArgs e)
        {
            tableCall("P7", btn_tbP7);

        }

        private void btn_tbP8_Click_2(object sender, EventArgs e)
        {
            tableCall("P8", btn_tbP8);
        }

        private void btn_ncMenuClose_Click(object sender, EventArgs e)
        {
            pnl_ncMenuUpdate.Visible = false;
            resetNC_MenuUpdateValue();
        }

        public void resetNC_MenuUpdateValue()
        {
            lbl_nc_menu.Text = string.Empty;
            lbl_ncMenuQty.Text = string.Empty;
            lbl_ncMenuRate.Text = string.Empty;
            lbl_ncMenuTotal.Text = string.Empty;

            //get men usection and category from db 
            lbl_ncMenuSection.Text = string.Empty;
            lbl_ncMenucategory.Text = string.Empty;
            chkNCMenu.Checked = false;
        }

        private void txt_ncMenuUpdate_Click(object sender, EventArgs e)
        {
            //update menu rate in sale_item
            try
            {
                //NC_itemDetails
                if (cmb_ncItemReson.Text != "" && chkNCMenu.Checked)
                {
                    int exstingMenuQty, newMenuQty;
                    exstingMenuQty = int.Parse(lbl_ncMenuQty.Text);
                    newMenuQty = int.Parse(txtNcQtyforNC.Text);

                    if (newMenuQty <= exstingMenuQty)
                    {

                        db.insert("insert into NC_itemDetails ([orderId],[tblNo] ,[ncDate]  ,[ncTime] ,[itemName],[rate],[qty],[amount],[userName],[reason]) values('" + label_order_id.Text + "','" + txtTableNo.Text + "','" + Convert.ToDateTime(label_date.Text).ToString("MM/dd/yyyy") + "','" + Convert.ToDateTime(label_time.Text).ToString("hh:mm:ss") + "','" + lbl_nc_menu.Text + "','" + lbl_ncMenuRate.Text + "','" + txtNcQtyforNC.Text + "','" + lbl_ncMenuTotal.Text + "','" + lbl_name.Text + "','" + cmb_ncItemReson.Text + "')");

                        if (int.Parse(lbl_ncMenuQty.Text) == int.Parse(txtNcQtyforNC.Text))
                        {
                            db.update("Update  sales_item set rate='0',total_amount='0' where  sales_id='" + NCMenuSalesID + "' and order_id='" + label_order_id.Text + "'");
                        }
                        else
                        {
                            int newQty = int.Parse(lbl_ncMenuQty.Text) - int.Parse(txtNcQtyforNC.Text);

                            double newtotalAmt = double.Parse(lbl_ncMenuRate.Text) * newQty;
                         
                            db.insert(@"INSERT INTO [sales_item]([order_id],[menu_id] ,[qty],[rate],[total_amount],[w_id],[kot_id],[Date] )
                                SELECT [order_id],[menu_id] ,'" + txtNcQtyforNC.Text + "','0','0' ,[w_id],[kot_id],[Date]  FROM [sales_item] where sales_id='" + NCMenuSalesID + "' and order_id='" + label_order_id.Text + "'");
                           
                            db.update("Update  sales_item set qty='" + newQty + "',total_amount='" + newtotalAmt + "' where  sales_id='" + NCMenuSalesID + "' and order_id='" + label_order_id.Text + "'");

                        }

                        bind(null);
                        resetNC_MenuUpdateValue();
                        ApplyDisc();
                        pnl_ncMenuUpdate.Visible = false;
                    }
                    else
                    {
                        MessageBox.Show("NC Menu Qty Can not be Grater then Existing Qty.");

                    }



                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error  " + ex.Message);
            }


        }

        private void itemWiseNCReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            itemWiseNCReport incReport = new itemWiseNCReport();
            incReport.ShowDialog();
        }

        private void chkFoodPerc_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkFoodPerc.Checked)
            //{
            //    txtFoodPerc.ReadOnly = false;
            //}
            //else
            //{
            //    txtFoodPerc.ReadOnly = true;
            //    txtFoodPerc.Text = "0";
            //    txtFoodAmt.Text = "0";
            //    lblStatus.Visible = false;
            //    //total_bill();
            //}
        }

        private void txtFoodPerc_TextChanged(object sender, EventArgs e)
        {
            if (txtFoodPerc.Text != "" && float.Parse(txtFoodPerc.Text) <= 100)
            {
                // txtDiscAmt.Text= (float.Parse(txtDiscValue.Text) * float.Parse(label_total_bill.Text) / 100).ToString();
                ApplyMoreDisc();
            }
            else
            {
                txtFoodPerc.Text = "0";
            }
        }

        private void chkFoodAmt_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkFoodAmt.Checked)
            //    txtFoodAmt.ReadOnly = false;
            //else
            //{
            //    txtFoodAmt.ReadOnly = true;
            //    txtFoodAmt.Text = "0";
            //    //total_bill();

            //}
        }
        void ApplyMoreDisc()
        {
            


            pnl_ST_VAT.Visible = true;
            string netAmt = "0";
            try
            {



                    //**** Food Disc ***********

                    //     totalAmountOfFood = db.getDb_Value("SELECT  SUM(sales_item.total_amount) AS Expr1 FROM            menu INNER JOIN  sales_item ON menu.menu_id = sales_item.menu_id INNER JOIN  category ON menu.category = category.cat_name WHERE        (sales_item.order_id = '" + label_order_id.Text + "') AND (category.cat_name <> 'Hard Drinks')");
                    totalAmountOfFood = db.getDb_Value("SELECT  SUM(sales_item.total_amount) AS Expr1 FROM    menu INNER JOIN  sales_item ON menu.menu_id = sales_item.menu_id INNER JOIN  category ON menu.category = category.cat_name WHERE        (sales_item.order_id = '" + label_order_id.Text + "')    AND (category.FoodSection = 'Food')");
                    liquorAmt = db.getDb_Value("SELECT  SUM(sales_item.total_amount) AS Expr1 FROM  menu INNER JOIN  sales_item ON menu.menu_id = sales_item.menu_id INNER JOIN  category ON menu.category = category.cat_name WHERE        (sales_item.order_id = '" + label_order_id.Text + "')    AND (category.FoodSection = 'Liquor')");
                    beverageAmt = db.getDb_Value("SELECT  SUM(sales_item.total_amount) AS Expr1 FROM   menu INNER JOIN  sales_item ON menu.menu_id = sales_item.menu_id INNER JOIN  category ON menu.category = category.cat_name WHERE        (sales_item.order_id = '" + label_order_id.Text + "')    AND (category.FoodSection = 'Beverages')");






                    if (rbtFoodAmt.Checked && totalAmountOfFood != 0)
                    {
                        //label_total_bill.Text = Math.Round(totalAmount - float.Parse(txtFoodAmt.Text)).ToString();
                        txtFoodPerc.Text = Math.Round(((float.Parse(txtFoodAmt.Text) / totalAmountOfFood) * 100), 2).ToString();

                    }

                    if (rbtLiqDisAmt.Checked && liquorAmt != 0)
                    {
                        txtLiquorPerc.Text = Math.Round(((float.Parse(txtLiquorAmt.Text) / liquorAmt) * 100), 2).ToString();
                    }

                    if (rbtBevDisAmt.Checked && beverageAmt != 0)
                    {
                        txtBeveragesPerc.Text = Math.Round(((float.Parse(txtBeveragesAmt.Text) / beverageAmt) * 100), 2).ToString();
                    }




                    float totalAmount = db.getDb_Value("SELECT  SUM(total_amount) AS Expr1 FROM  sales_item WHERE (order_id = '" + label_order_id.Text + "')");
                    if (chkFoodPerc.Checked)
                    {
                        float discamt = totalAmountOfFood * float.Parse(txtFoodPerc.Text) / 100;
                        txtFoodAmt.Text = Math.Round(discamt).ToString();
                        //label_total_bill.Text = Math.Round(totalAmount - discamt).ToString();
                        netAmt = (totalAmountOfFood - float.Parse(txtFoodAmt.Text)).ToString();

                    }




                    //**** Beverages Disc ***********
                    if (chkBevPerc.Checked)
                    {
                        float discamt = beverageAmt * float.Parse(txtBeveragesPerc.Text) / 100;
                        txtBeveragesAmt.Text = Math.Round(discamt).ToString();
                        //label_total_bill.Text = Math.Round(totalAmount - discamt).ToString();
                        netAmt = (totalAmountOfFood - float.Parse(txtBeveragesAmt.Text)).ToString();

                    }
                    if (chkBevAmt.Checked)
                    {
                        label_total_bill.Text = Math.Round(totalAmount - float.Parse(txtBeveragesAmt.Text)).ToString();
                    }

                    //**** Liquor Disc ***********
                    if (chkLiquorPerc.Checked)
                    {
                        float discamt = liquorAmt * float.Parse(txtLiquorPerc.Text) / 100;
                        txtLiquorAmt.Text = Math.Round(discamt).ToString();
                        //label_total_bill.Text = Math.Round(totalAmount - discamt).ToString();
                        netAmt = (totalAmountOfFood - float.Parse(txtLiquorAmt.Text)).ToString();

                    }
                    if (chkLiquorAmt.Checked)
                    {
                        label_total_bill.Text = Math.Round(totalAmount - float.Parse(txtLiquorAmt.Text)).ToString();
                    }
                
                    lblDisc.Text = (float.Parse(txtFoodAmt.Text) + float.Parse(txtLiquorAmt.Text) + float.Parse(txtBeveragesAmt.Text)).ToString();
                    lblTotalAmt.Text = totalAmount.ToString();




                    if (flgnsubmit)
                    {
                        string value = "";
                        string ServiceTaxvalue = "";
                        string Vatvalue = "0";
                        if (db.ChkDb_Value("select * from tbl_option where grp='" + "Tax" + "' and status='Yes'"))
                        {
                            pnlGST.Visible = true;

                            value = db.getDbstatus_Value("select value from tbl_option where grp='" + "Tax" + "' and status='Yes'");
                            //ServiceTaxvalue = db.getDbstatus_Value("select value  from tbl_option where grp='" + "ServiceTaxvalue" + "' and status='Yes'");

                            ServiceTaxvalue = db.getDbstatus_Value("SELECT ISNULL((SELECT value FROM tbl_option WHERE grp = 'ServiceTaxvalue'  and status='Yes'),0)");
                            if (db.ChkDb_Value("select value  from tbl_option where grp='" + "Vatvalue" + "' and status='Yes'"))
                            {
                                panelvat.Visible = true;
                                Vatvalue = db.getDbstatus_Value("select ISNULL((select value  from tbl_option where grp='" + "Vatvalue" + "' and status='Yes'),0)");
                            }
                            else { panelvat.Visible = false; };

                            lblCgstValue.Text = (float.Parse(value) / 2).ToString();
                            lblSgstValue.Text = (float.Parse(value) / 2).ToString();

                            // service tax and vat calculation 

                            lbl_st_per.Text = ServiceTaxvalue;
                            lbl_Vat_per.Text = Vatvalue;

                            netAmt = ((totalAmountOfFood + liquorAmt + beverageAmt) - (float.Parse(txtFoodAmt.Text) + float.Parse(txtLiquorAmt.Text) + float.Parse(txtBeveragesAmt.Text))).ToString();
                            //gstAmt = (float.Parse(lblCgstValue.Text) * (totalAmountOfFood)) / 100;
                            gstAmt = (float.Parse(lblCgstValue.Text) * float.Parse(netAmt)) / 100;

                            //service tax
                            serviceTaxAmt = (float.Parse(ServiceTaxvalue) * (float.Parse(netAmt) + liquorAmt)) / 100;
                            //vat
                            vatAmt = (float.Parse(Vatvalue) * liquorAmt) / 100;


                            lblCgstAmt.Text = Math.Round(gstAmt, 2).ToString();
                            lblSgstAmt.Text = Math.Round(gstAmt, 2).ToString();

                            // service Tax Amt 

                            lbl_serviceTaxAmt.Text = Math.Round(serviceTaxAmt, 2).ToString();
                            lbl_vat_amt.Text = Math.Round(vatAmt, 2).ToString();


                            double getAllDiscount = double.Parse(lbl_foodDisAmt.Text) + double.Parse(lbl_BevDisAmt.Text) + double.Parse(lbl_LiquorDisAmt.Text);
                            grdTot = float.Parse(lblCgstAmt.Text) + float.Parse(lblSgstAmt.Text);
                            //label_total_bill.Text = Math.Round(totalAmount + float.Parse(lblCgstAmt.Text) + float.Parse(lblSgstAmt.Text) - float.Parse(lblDisc.Text)).ToString();
                            label_total_bill.Text = Math.Round(totalAmount + float.Parse(lblCgstAmt.Text) + float.Parse(lblSgstAmt.Text) - float.Parse(lblDisc.Text) + float.Parse(lbl_serviceTaxAmt.Text) + float.Parse(lbl_vat_amt.Text) - getAllDiscount).ToString();

                            //db.update("update table_order set gst='" + grdTot + "' where order_id='" + label_order_id.Text + "' and t_id='" + txtTableNo.Text + "'");
                            db.update("update table_order set gst='" + grdTot + "',serviceTaxAmt='" + serviceTaxAmt + "',vatAmt='" + vatAmt + "' where order_id='" + label_order_id.Text + "' and t_id='" + txtTableNo.Text + "'");

                        }

                        else
                        {
                            pnlGST.Visible = false;


                            if (db.ChkDb_Value("select value  from tbl_option where grp='" + "Vatvalue" + "' and status='Yes'"))
                            {
                                panelvat.Visible = true;
                                Vatvalue = db.getDbstatus_Value("select value  from tbl_option where grp='" + "Vatvalue" + "' and status='Yes'");
                                lbl_Vat_per.Text = Vatvalue;

                                vatAmt = (float.Parse(Vatvalue) * liquorAmt) / 100;

                                double getAllDiscount = double.Parse(lbl_foodDisAmt.Text) + double.Parse(lbl_BevDisAmt.Text) + double.Parse(lbl_LiquorDisAmt.Text);

                                label_total_bill.Text = Math.Round(totalAmount - float.Parse(lblDisc.Text) + float.Parse(lbl_serviceTaxAmt.Text) + float.Parse(lbl_vat_amt.Text) - getAllDiscount).ToString();

                            }
                            else { panelvat.Visible = false; };

                        }
                    }
            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message);
            }
        }


        private void txtFoodAmt_TextChanged(object sender, EventArgs e)
        {
            if (txtFoodAmt.Text != "")
            {
                // txtDiscValue.Text = (float.Parse(txtDiscAmt.Text) * 100 / float.Parse(label_total_bill.Text)).ToString();
                ApplyMoreDisc();
            }
            else
            {
                txtFoodAmt.Text = "0";
            }
        }

        private void chkBevPerc_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkBevPerc.Checked)
            //{
            //    txtBeveragesPerc.ReadOnly = false;
            //}
            //else
            //{
            //    txtBeveragesPerc.ReadOnly = true;
            //    txtBeveragesPerc.Text = "0";
            //    txtBeveragesAmt.Text = "0";
            //    lblStatus.Visible = false;
            //    //total_bill();
            //}
        }

        private void txtBeveragesPerc_TextChanged(object sender, EventArgs e)
        {
            if (txtBeveragesPerc.Text != "" && float.Parse(txtBeveragesPerc.Text) <= 100)
            {
                // txtDiscAmt.Text= (float.Parse(txtDiscValue.Text) * float.Parse(label_total_bill.Text) / 100).ToString();
                ApplyMoreDisc();
            }
            else
            {
                txtBeveragesPerc.Text = "0";
            }
        }

        private void chkBevAmt_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkBevAmt.Checked)
            //    txtBeveragesAmt.ReadOnly = false;
            //else
            //{
            //    txtBeveragesAmt.ReadOnly = true;
            //    txtBeveragesAmt.Text = "0";
            //    //total_bill();

            //}
        }

        private void txtBeveragesAmt_TextChanged(object sender, EventArgs e)
        {
            if (txtBeveragesAmt.Text != "")
            {
                // txtDiscValue.Text = (float.Parse(txtDiscAmt.Text) * 100 / float.Parse(label_total_bill.Text)).ToString();
                ApplyMoreDisc();
            }
            else
            {
                txtBeveragesAmt.Text = "0";
            }
        }

        private void chkLiquorPerc_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkLiquorPerc.Checked)
            //{
            //    txtLiquorPerc.ReadOnly = false;
            //}
            //else
            //{
            //    txtLiquorPerc.ReadOnly = true;
            //    txtLiquorPerc.Text = "0";
            //    txtLiquorAmt.Text = "0";
            //    lblStatus.Visible = false;
            //    //total_bill();
            //}
        }

        private void txtLiquorPerc_TextChanged(object sender, EventArgs e)
        {
            if (txtLiquorPerc.Text != "" && float.Parse(txtLiquorPerc.Text) <= 100)
            {
                // txtDiscAmt.Text= (float.Parse(txtDiscValue.Text) * float.Parse(label_total_bill.Text) / 100).ToString();
                ApplyMoreDisc();
            }
            else
            {
                txtLiquorPerc.Text = "0";
            }
        }

        private void chkLiquorAmt_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkLiquorAmt.Checked)
            //    txtLiquorAmt.ReadOnly = false;
            //else
            //{
            //    txtLiquorAmt.ReadOnly = true;
            //    txtLiquorAmt.Text = "0";
            //    //total_bill();

            //}
        }

        private void txtLiquorAmt_TextChanged(object sender, EventArgs e)
        {
            if (txtLiquorAmt.Text != "")
            {
                // txtDiscValue.Text = (float.Parse(txtDiscAmt.Text) * 100 / float.Parse(label_total_bill.Text)).ToString();
                ApplyMoreDisc();
            }
            else
            {
                txtLiquorAmt.Text = "0";
            }
        }

        bool flgnsubmit = false;
        private void btnsubmit_Click(object sender, EventArgs e)
        {
            //if (cmb_discountReson.Text != "")
            //{
            //    btnApplyMoreDisc();
            //    ApplyDisc();
            //    txtDiscAll.Text = "";
            //    pnlDiscount.Visible = false;
            //}
            //else
            //{
            //    MessageBox.Show("Please Enter Discount Reson..");
            //}
            if (cmb_discountReson.Text != "")
            {
                flgnsubmit = true;

                btnApplyMoreDisc();
                ApplyDisc();
                txtDiscAll.Text = "";

                //reset values

                txtFoodPerc.Text = "0";
                txtLiquorPerc.Text = "0";
                txtBeveragesPerc.Text = "0";

                txtFoodAmt.Text = "0";
                txtLiquorAmt.Text = "0";
                txtBeveragesAmt.Text = "0";

                // rbt 

                rbtFoodPer.Checked = false;
                rbtLiqDisVal.Checked = false;
                rbtBevDisVal.Checked = false;

                rbtFoodAmt.Checked = false;
                rbtLiqDisAmt.Checked = false;
                rbtBevDisAmt.Checked = false;


                pnlDiscount.Visible = false;
            }
            else
            {
                MessageBox.Show("Please Enter Discount Reson..");
            }

            flgnsubmit = false;
        }
        void btnApplyMoreDisc()
        {
            //  float totalAmountOfFood = db.getDb_Value("SELECT        SUM(sales_item.total_amount) AS Expr1 FROM            menu INNER JOIN  sales_item ON menu.menu_id = sales_item.menu_id INNER JOIN  category ON menu.category = category.cat_name WHERE        (sales_item.order_id = '" + label_order_id.Text + "') AND (category.cat_name <> 'Hard Drinks')");
            try
            {
                float discFoodAmt = 0, discLiqAmt = 0, discBevAmt = 0;
                if (txtFoodPerc.Text != "" && txtFoodAmt.Text != "" && txtLiquorAmt.Text != "" && txtLiquorPerc.Text != "" && txtBeveragesAmt.Text != "" && txtBeveragesPerc.Text != "")
                {
                    ApplyMoreDisc();

                    //if (chkFoodPerc.Checked)
                    //    discFoodAmt = (float.Parse(txtFoodPerc.Text) * totalAmountOfFood / 100);
                    //else
                    //    discFoodAmt = float.Parse(txtFoodAmt.Text);

                    //if (chkBevPerc.Checked)
                    //    discBevAmt = (float.Parse(txtBeveragesPerc.Text) * beverageAmt / 100);
                    //else
                    //    discBevAmt = float.Parse(txtBeveragesAmt.Text);

                    //if (chkLiquorPerc.Checked)
                    //    discLiqAmt = (float.Parse(txtLiquorPerc.Text) * liquorAmt / 100);
                    //else
                    //    discLiqAmt = float.Parse(txtLiquorAmt.Text);

                    if (rbtFoodPer.Checked)
                        discFoodAmt = (float.Parse(txtFoodPerc.Text) * totalAmountOfFood / 100);
                    else
                        discFoodAmt = float.Parse(txtFoodAmt.Text);

                    if (rbtBevDisVal.Checked)
                        discBevAmt = (float.Parse(txtBeveragesPerc.Text) * beverageAmt / 100);
                    else
                        discBevAmt = float.Parse(txtBeveragesAmt.Text);

                    if (rbtLiqDisVal.Checked)
                        discLiqAmt = (float.Parse(txtLiquorPerc.Text) * liquorAmt / 100);
                    else
                        discLiqAmt = float.Parse(txtLiquorAmt.Text);



                    db.update("update table_order set foodDiscValue='" + txtFoodPerc.Text + "',foodDiscAmt='" + (discFoodAmt) + "', liquorDiscValue='" + txtLiquorPerc.Text + "',liquorDiscAmt='" + (discLiqAmt) + "',beveragesDiscValue='" + txtBeveragesPerc.Text + "' ,beveragesDiscAmt='" + (discBevAmt) + "' where order_id='" + label_order_id.Text + "' and t_id='" + txtTableNo.Text + "'");
                    if (txtDiscValue.Text != "0" || txtDiscAmt.Text != "0")
                        lblStatus.Visible = true;
                    // lblStatus.Text = "Discount Applied..";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        float gstAmt, grdTot, serviceTaxAmt, vatAmt, liquorAmt, beverageAmt;

        private void linkLbl_more_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (label_order_id.Text != "0")
            {

                pnlDiscount.Visible = true;
                if (rbt_allDiscount.Checked)
               {
                   txtDiscAll.Visible = true;
               }
                db.comboFill(cmb_discountReson, "select DiscountReson from table_order where DiscountReson<>'' group by DiscountReson", "table_order", "DiscountReson", "DiscountReson");
                cmb_discountReson.Text = "";


               cbCurenttble.Text= txtTableNo.Text;
               lblcmergeoid.Text = label_order_id.Text;

               db.comboFill(cbmergetbl, "select t_id from table_status where   status='Processing' and t_id<>'" + cbCurenttble.Text + "'", "table_status", "t_id", "t_id");
            
                cbmergetbl.Text="Select";

                txtFoodPerc.Text = "0";
                txtLiquorPerc.Text = "0";
                txtBeveragesPerc.Text = "0";

                chkFoodPerc.Checked = false;
                chkLiquorPerc.Checked = false;
                chkBevPerc.Checked = false;
              



            }
            else
            {
                MessageBox.Show("Please Select Table Order .");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            pnlDiscount.Visible = false;
        }

        private void rdbCategory_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbCategory.Checked)
                pnlCategory.Visible = true;
            else
                pnlCategory.Visible = false;
        }

        private void chkFood_CheckedChanged(object sender, EventArgs e)
        {
            float foodAmt = 0;
            if (chkFood.Checked)
            {
                totalAmountOfFood = db.getDb_Value("SELECT  SUM(sales_item.total_amount) AS Expr1 FROM            menu INNER JOIN  sales_item ON menu.menu_id = sales_item.menu_id INNER JOIN  category ON menu.category = category.cat_name WHERE        (sales_item.order_id = '" + label_order_id.Text + "')    AND (category.FoodSection = 'Food')");
                liquorAmt = db.getDb_Value("SELECT  SUM(sales_item.total_amount) AS Expr1 FROM            menu INNER JOIN  sales_item ON menu.menu_id = sales_item.menu_id INNER JOIN  category ON menu.category = category.cat_name WHERE        (sales_item.order_id = '" + label_order_id.Text + "')    AND (category.FoodSection = 'Liquor')");

                foodAmt = (totalAmountOfFood) * float.Parse(lbl_st_per.Text) / 100;
                txtfood.Text = (totalAmountOfFood + foodAmt).ToString();
            }
            else
                txtfood.Text = "0";
        }

        private void chkLiquor_CheckedChanged(object sender, EventArgs e)
        {
            float liqrAmt = 0;
            if (chkFood.Checked)
            {
                liquorAmt = db.getDb_Value("SELECT  SUM(sales_item.total_amount) AS Expr1 FROM            menu INNER JOIN  sales_item ON menu.menu_id = sales_item.menu_id INNER JOIN  category ON menu.category = category.cat_name WHERE        (sales_item.order_id = '" + label_order_id.Text + "')    AND (category.FoodSection = 'Liquor')");

                liqrAmt = (liquorAmt) * float.Parse(lbl_st_per.Text) / 100;
                txtLiquor.Text = (liquorAmt + liqrAmt).ToString();
            }
            else
                txtLiquor.Text = "0";
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            //if (MessageBox.Show("Are you sure want To Update ??", "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //{
            //    string serveiceTaxAMt = "0";

            //    if (chkFood.Checked)
            //        serveiceTaxAMt = txtfood.Text;
            //    else
            //        serveiceTaxAMt = txtLiquor.Text;

            //    db.update("update table_order set serviceTaxAmt='" + serveiceTaxAMt + "' where order_id='" + label_order_id.Text + "'");
            //    MessageBox.Show("Record Updated Successfully!!!");
            //    lbl_serviceTaxAmt.Text = serveiceTaxAMt.ToString();
            //}

            if (txtChngeSC.Text != "")
            {
                if (rbtOnlyFood.Checked)
                { db.update("update table_order set serviceTaxFoodVal='" + txtChngeSC.Text + "' where order_id='" + label_order_id.Text + "'"); }
                else if (rbtOnlyLiquor.Checked)
                {
                    db.update("update table_order set serviceTaxLiquorVal='" + txtChngeSC.Text + "' where order_id='" + label_order_id.Text + "'");
                }
                else
                {
                    db.update("update table_order set serviceTaxVal='" + txtChngeSC.Text + "', serviceTaxFoodVal='" + txtChngeSC.Text + "', serviceTaxLiquorVal='" + txtChngeSC.Text + "' where order_id='" + label_order_id.Text + "'");

                }

                MessageBox.Show("Service Charge Change Successfully!!!");


                pnlDiscount.Visible = false;
                //reset controls
                txtChngeSC.Text = "0";
                rdbAll.Checked = true;

                //update new values 
                ApplyDisc();

            }
            else
            {
                MessageBox.Show("Please Enter Service Charge.");
            }
        }

        private void rbtOnlyFood_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbtOnlyLiquor_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rdbAll_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtChngeSC_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);
        }

        private void txtNcQtyforNC_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);
        }

        private void chkNCMenu_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNCMenu.Checked)
                txtNcQtyforNC.ReadOnly = false;
            else
                txtNcQtyforNC.ReadOnly = true;
        }



        public void KOtPrintCode_rePrint(string reprintSalesItemID)
        {
            string[] arrValues = new string[5];
            string getOrderIdForPrint = label_order_id.Text;


          arrValues = reprintSalesItemID.Split('*');
          int ar_count = arrValues.Length;

          if (ar_count == 3)
          {
              getOrderIdForPrint = reprintSalesItemID.Split('*')[2];
          }

            LPrinter lp = new LPrinter();
            string[] printNm = new string[10];
            int count = 0;
            db.cnopen();
            //  SqlCommand cmdowner = new SqlCommand("SELECT DISTINCT category.printerName  FROM category INNER JOIN   tbl_temporder ON category.cat_name = tbl_temporder.category WHERE (tbl_temporder.order_id = '" + OrdId[k] + "')", db.cn);
            SqlCommand cmdowner = new SqlCommand("select DISTINCT cat.printerName from sales_item si inner join menu m on si.menu_id=m.menu_id inner join category cat on m.category=cat.cat_name and si.order_id='" + getOrderIdForPrint + "' and si.sales_id='" + reprintSalesItemID.Split('*')[0] + "'", db.cn);

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
                // string qryCheckCategoryOther = "SELECT tbl_temporder.menuid, tbl_temporder.qty, tbl_temporder.m_name, tbl_temporder.w_id, tbl_temporder.order_id, tbl_temporder.category , tbl_temporder.ordersuggestion FROM category INNER JOIN  tbl_temporder ON category.cat_name = tbl_temporder.category where printerName='" + arr[i] + "' and order_id = '" + label_order_id.Text + "' ";
                string qryCheckCategoryOther = @"SELECT tbl_temporder.menuid, tbl_temporder.qty, tbl_temporder.m_name, tbl_temporder.w_id, tbl_temporder.order_id, 
            tbl_temporder.category , tbl_temporder.ordersuggestion as order_sugg
             FROM category INNER JOIN  tbl_temporder ON category.cat_name = tbl_temporder.category 
            where printerName='" + arr[i] + "' and order_id = '" + getOrderIdForPrint + "' ";



                lp.billid = float.Parse(getOrderIdForPrint);
                lp.tableno = (txtTableNo.Text);
                lp.wname = cmbWaiterName.Text;
                //if (db.ChkDb_Value(qryCheckCategoryOther))

                if (true)
                {
                    lp.printOrder_kot(qryCheckCategoryOther, arr[i], reprintSalesItemID);  // sales id
                }
            }

        }

        private void btn_T1_Click(object sender, EventArgs e)
        {
            tableCall("T1", btn_T1);
        }

        private void btn_T2_Click(object sender, EventArgs e)
        {
            tableCall("T2", btn_T2);
        }

        private void btn_T3_Click(object sender, EventArgs e)
        {
            tableCall("T3", btn_T3);
        }

        private void btn_T4_Click(object sender, EventArgs e)
        {
            tableCall("T4", btn_T4);
        }

        private void btn_T5_Click(object sender, EventArgs e)
        {
            tableCall("T5", btn_T5);
        }

        private void btn_T6_Click(object sender, EventArgs e)
        {
            tableCall("T6", btn_T6);
        }

        private void btn_T7_Click(object sender, EventArgs e)
        {
            tableCall("T7", btn_T7);
        }

        private void btn_T8_Click(object sender, EventArgs e)
        {
            tableCall("T8", btn_T8);
        }

        private void btn_T9_Click(object sender, EventArgs e)
        {
            tableCall("T9", btn_T9);
        }

        private void btn_T10_Click(object sender, EventArgs e)
        {
            tableCall("T10", btn_T10);
        }

        private void btn_T11_Click(object sender, EventArgs e)
        {
            tableCall("T11", btn_T11);
        }

        private void btn_T12_Click(object sender, EventArgs e)
        {
            tableCall("T12", btn_T12);
        }

        private void btn_T13_Click(object sender, EventArgs e)
        {
            tableCall("T13", btn_T13);
        }

        private void btn_T14_Click(object sender, EventArgs e)
        {
            tableCall("T14", btn_T14);
        }

        private void btn_T15_Click(object sender, EventArgs e)
        {
            tableCall("T15", btn_T15);
        }

        private void btn_T16_Click(object sender, EventArgs e)
        {
            tableCall("T16", btn_T16);
        }

        private void btn_T17_Click(object sender, EventArgs e)
        {
            tableCall("T17", btn_T17);
        }

        private void btn_T18_Click(object sender, EventArgs e)
        {
            tableCall("T18", btn_T18);
        }

        private void btn_T19_Click(object sender, EventArgs e)
        {
            tableCall("T19", btn_T19);
        }

        private void btn_T20_Click(object sender, EventArgs e)
        {
            tableCall("T20", btn_T20);
        }

        private void btn_T21_Click(object sender, EventArgs e)
        {
            tableCall("T21", btn_T21);
        }

        private void btn_T22_Click(object sender, EventArgs e)
        {
            tableCall("T22", btn_T22);
        }

        private void btn_T23_Click(object sender, EventArgs e)
        {
            tableCall("T23", btn_T23);
        }

        private void btn_T24_Click(object sender, EventArgs e)
        {
            tableCall("T24", btn_T24);
        }

        private void btn_cancelKotUpdate_Click(object sender, EventArgs e)
        {

            cancelitem(false);



        }
        public void cancelitem(bool flag_del)
        {
            //lbl_ncMenuQty 
            //txtCancelQty
            int exstingMenuQty, newMenuQty;
            exstingMenuQty = int.Parse(lbl_ncMenuQty.Text);
            newMenuQty = int.Parse(txtCancelQty.Text);

            if (newMenuQty <= exstingMenuQty)
            {
                if (!flag_del)
                {
                    db.insert("insert into CancelKOTDetails ([orderId],[tblNo],[menuName],[qty],[rate],[amount] ,[date],[waiterName],cancelKotTime,cancelKotReson) values('" + label_order_id.Text + "','" + txtTableNo.Text + "','" + lbl_nc_menu.Text + "','" + txtCancelQty.Text + "','" + lbl_ncMenuRate.Text + "','" + (float.Parse(lbl_ncMenuRate.Text) * float.Parse(txtCancelQty.Text)) + "','" + Convert.ToDateTime(label_date.Text).ToString("MM/dd/yyyy") + "','" + lbl_name.Text + "','" + Convert.ToDateTime(label_time.Text).ToString("hh:mm:ss") + "','" + cmb_cancelKotReson.Text + "')");

                    //print cancel kot

                    KOtPrintCode_rePrint(NCMenuSalesID + "*Cancel KOT");

                }

                if (exstingMenuQty == newMenuQty)
                {

                    db.insert("delete from sales_item where sales_id='" + NCMenuSalesID + "' and order_id='" + label_order_id.Text + "'");
                }
                else
                {
                    int newQty = exstingMenuQty - newMenuQty; //int.Parse(lbl_ncMenuQty.Text) - int.Parse(txtNcQtyforNC.Text);
                    double newtotalAmt = double.Parse(lbl_ncMenuRate.Text) * newQty;
                    db.update("Update  sales_item set qty='" + newQty + "',total_amount='" + newtotalAmt + "' where  sales_id='" + NCMenuSalesID + "' and order_id='" + label_order_id.Text + "'");

                }

                bind(null);
                //resetNC_MenuUpdateValue();
                txtCancelQty.Text = "0";
                cmb_cancelKotReson.Text = "";
                pnl_ncMenuUpdate.Visible = false;


            }
        }

        private void lbl_ncMenucategory_Click(object sender, EventArgs e)
        {

        }

        private void btn_B16_Click(object sender, EventArgs e)
        {
            tableCall("B16", btn_B16);
        }

        private void btn_B17_Click(object sender, EventArgs e)
        {
            tableCall("B17", btn_B17);
        }

        private void btn_B18_Click(object sender, EventArgs e)
        {
            tableCall("B18", btn_B18);
        }

        private void btn_B19_Click(object sender, EventArgs e)
        {
            tableCall("B19", btn_B19);
        }

        private void btn_G11_Click(object sender, EventArgs e)
        {
            tableCall("G11", btn_G11);
        }

        private void btn_G12_Click(object sender, EventArgs e)
        {
            tableCall("G12", btn_G12);
        }

        private void btn_G14_Click(object sender, EventArgs e)
        {
            tableCall("G14", btn_G14);
        }

        private void btn_G15_Click(object sender, EventArgs e)
        {
            tableCall("G15", btn_G15);
        }

        private void btn_G16_Click(object sender, EventArgs e)
        {
            tableCall("G16", btn_G16);
        }

        private void btn_G17_Click(object sender, EventArgs e)
        {
            tableCall("G17", btn_G17);
        }

        private void btn_G18_Click(object sender, EventArgs e)
        {
            tableCall("G18", btn_G18);
        }

        private void btn_G19_Click(object sender, EventArgs e)
        {
            tableCall("G19", btn_G19);
        }



        private void btn_L14_Click(object sender, EventArgs e)
        {
            tableCall("L14", btn_L14);
        }

        private void btn_L15_Click(object sender, EventArgs e)
        {
            tableCall("L15", btn_L15);
        }

        private void btn_L16_Click(object sender, EventArgs e)
        {
            tableCall("L16", btn_L16);
        }

        private void btn_L17_Click(object sender, EventArgs e)
        {
            tableCall("L17", btn_L17);
        }

        private void btn_L18_Click(object sender, EventArgs e)
        {
            tableCall("L18", btn_L18);
        }

        private void btn_L19_Click(object sender, EventArgs e)
        {
            tableCall("L19", btn_L19);
        }

        private void btn_P1_Click(object sender, EventArgs e)
        {
            tableCall("P1", btn_P1);
        }

        private void btn_P5_Click(object sender, EventArgs e)
        {
            tableCall("P5", btn_P5);
        }

        private void btn_P16_Click(object sender, EventArgs e)
        {
            tableCall("P16", btn_P16);
        }

        private void btn_P8_Click(object sender, EventArgs e)
        {
            tableCall("P8", btn_P8);
        }

        private void btn_P4_Click(object sender, EventArgs e)
        {
            tableCall("P4", btn_P4);
        }

        private void btn_P11_Click(object sender, EventArgs e)
        {
            tableCall("P11", btn_P11);
        }

        private void btn_P3_Click(object sender, EventArgs e)
        {
            tableCall("P3", btn_P3);

        }

        private void btn_P7_Click(object sender, EventArgs e)
        {
            tableCall("P7", btn_P7);
        }

        private void btn_P14_Click(object sender, EventArgs e)
        {
            tableCall("P14", btn_P14);

        }

        private void btn_P15_Click(object sender, EventArgs e)
        {
            tableCall("P15", btn_P15);
        }

        private void btn_P10_Click(object sender, EventArgs e)
        {

            tableCall("P10", btn_P10);        }

        private void btn_P2_Click(object sender, EventArgs e)
        {

            tableCall("P2", btn_P2);

        }

        private void btn_P6_Click(object sender, EventArgs e)
        {

            tableCall("P6", btn_P6);
        }

        private void btn_P12_Click(object sender, EventArgs e)
        {
            tableCall("P12", btn_P12);
        }

        private void btn_P9_Click(object sender, EventArgs e)
        {
            tableCall("P9", btn_P9);
        }

        private void btn_P17_Click(object sender, EventArgs e)
        {
            tableCall("P17", btn_P17);

        }

        private void btn_P18_Click(object sender, EventArgs e)
        {
            tableCall("P18", btn_P18);

        }

        private void btn_P19_Click(object sender, EventArgs e)
        {
            tableCall("P19", btn_P19);
        }

        private void btn_shiftItem_Click(object sender, EventArgs e)
        {
            int exstingMenuQty, newMenuQty;
            exstingMenuQty = int.Parse(lbl_ncMenuQty.Text);
            newMenuQty = int.Parse(txtShiftItemQty.Text);

            string newOrderId, getWaiterID, getTableOrderId,newMenuSalesID;

            if (txtShiftItemQty.Text != "")
            {

                if (cmbShiftitemReson.Text != "")
                {
                    if (newMenuQty <= exstingMenuQty)
                    {
                          newOrderId = lbl_selectedOrderID.Text;
                          newMenuSalesID = NCMenuSalesID;

                        if (chk_newTable.Checked)
                        {
                              getWaiterID = db.getDbstatus_Value("select w_id from sales_item where sales_id='" + NCMenuSalesID + "'");

                            db.insert(@"insert into table_order (order_id,t_id,timeing,w_id,order_type,tableStatus,discValue,discAmt,gst)
                        values( ( select max(order_id)+1 from table_order ),'" + cmb_tablesListforShift.Text + "','" + Convert.ToDateTime(label_date.Text).ToString("MM/dd/yyyy") + "'," + getWaiterID + ",'Novat','Processing','0','0','0'); "
                            + "  update table_status set status='Processing' where t_id='" + cmb_tablesListforShift.Text + "'");

                            

                              getTableOrderId = db.getDbstatus_Value(@"select  MAX(tob.order_id) as OrderID from table_order tob 
 inner join table_status ts on ts.t_id=tob.t_id and  tob.t_id='" + cmb_tablesListforShift.Text + "' and ts.status='Processing' group by tob.t_id ");

                            newOrderId = getTableOrderId;
 
                        }


                        if (exstingMenuQty == newMenuQty)
                        {
                            db.update("Update  sales_item set order_id='" + newOrderId + "' where  sales_id='" + NCMenuSalesID + "' and order_id='" + label_order_id.Text + "'");

                            
                        }
                        else
                        {
                            int newQty = int.Parse(lbl_ncMenuQty.Text) - int.Parse(txtShiftItemQty.Text);
                            double newtotalAmt = double.Parse(lbl_ncMenuRate.Text) * newQty;
                            double shiftItemTotalAmt = double.Parse(lbl_ncMenuRate.Text) * int.Parse(txtShiftItemQty.Text);

                            db.update("Update  sales_item set qty='" + newQty + "',total_amount='" + newtotalAmt + "' where  sales_id='" + NCMenuSalesID + "' and order_id='" + label_order_id.Text + "'");


                            db.insert(@"INSERT INTO [sales_item]([order_id],[menu_id] ,[qty],[rate],[total_amount],[w_id],[kot_id],[Date] )
                                SELECT '" + newOrderId + "',[menu_id] ,'" + txtShiftItemQty.Text + "','" + lbl_ncMenuRate.Text + "','" + shiftItemTotalAmt + "' ,[w_id],[kot_id],[Date]  FROM [sales_item] where sales_id='" + NCMenuSalesID + "' and order_id='" + label_order_id.Text + "'");

                              newMenuSalesID = db.getDbstatus_Value(@"select MAX(sales_id) from sales_item  si inner join menu m on si.menu_id=m.menu_id and m.m_name='"+lbl_nc_menu.Text.Trim()+"'"
                                              + " where  si.order_id='"+newOrderId+"' group by si.menu_id,si.order_id");


 
                        }

                        KOtPrintCode_rePrint(newMenuSalesID + "*Shift KOT*" + newOrderId);
                        
                        bind(null);
                        //resetNC_MenuUpdateValue();
                        txtShiftItemQty.Text = "0";
                        cmbShiftitemReson.Text = "";
                        pnl_ncMenuUpdate.Visible = false;

                    } // if end here
                }
                else MessageBox.Show("Enter Shift Item Reson.");

            }
            else
                MessageBox.Show("Enter Shift Qty.");

        }


        private void cmb_tablesListforShift_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl_selectedTable.Text=cmb_tablesListforShift.SelectedText.ToString();
            lbl_selectedOrderID.Text = cmb_tablesListforShift.SelectedValue.ToString();
        }

        private void txtCancelQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);
        }

        private void txtShiftItemQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);
        }

        private void rbt_categoryWise_CheckedChanged(object sender, EventArgs e)
        {
            //if (rbt_categoryWise.Checked)
            //{
            //    pnl_discCatgoryWise.Visible = true;
            //     txtFoodPerc.Text="0";
            //    txtLiquorPerc.Text="0";
            //    txtBeveragesPerc.Text = "0";
            //    chkFoodPerc.Checked = false;
            //    chkLiquorPerc.Checked = false;
            //    chkBevPerc.Checked = false;
            //}
            //else
            //{
            //    pnl_discCatgoryWise.Visible = false;
            //}


            txtDiscAll.Visible = false;
            if (rbt_categoryWise.Checked)
            {
                pnl_discCatgoryWise.Visible = true;
                txtFoodPerc.Text = "0";
                txtLiquorPerc.Text = "0";
                txtBeveragesPerc.Text = "0";
                chkFoodPerc.Checked = false;
                chkLiquorPerc.Checked = false;
                chkBevPerc.Checked = false;
            }
            else
            {
                pnl_discCatgoryWise.Visible = false;


                rbtFoodPer.Checked = false;
                rbtLiqDisVal.Checked = false;
                rbtBevDisVal.Checked = false;
                rbtFoodAmt.Checked = false;
                rbtLiqDisAmt.Checked = false;
                rbtBevDisAmt.Checked = false;

            }
        }

        private void rbt_allDiscount_CheckedChanged(object sender, EventArgs e)
        {
            txtDiscAll.Visible = true;
        }

        private void txtDiscAll_TextChanged(object sender, EventArgs e)
        {
            if (txtDiscAll.Text != "")
            {
                chkFoodPerc.Checked = true;
                chkLiquorPerc.Checked = true;
                chkBevPerc.Checked = true;

                txtFoodPerc.Text = txtDiscAll.Text;
                txtLiquorPerc.Text = txtDiscAll.Text;
                txtBeveragesPerc.Text = txtDiscAll.Text;
            }
           

        }

        private void btn_searchOrder_Click(object sender, EventArgs e)
        {

            label_order_id.Text = txtSearchOrderID.Text;
            
            txtTableNo.Text = db.getDbstatus_Value("select t_id from table_order where order_id='" + label_order_id.Text + "'");
            bind(null);
            ApplyDisc();
        }

        private void btnSearchClose_Click(object sender, EventArgs e)
        {
            txtSearchOrderID.Text = string.Empty;
            pnl_searchOrder.Visible = false;
        }

        private void chk_newTable_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_newTable.Checked)
            {
                db.comboFill(cmb_tablesListforShift, @"select t_id as tableNO, '0' as OrderID from table_status where status='Empty' and section<>'-' ", "NC_itemDetails", "tableNO", "OrderID");
                cmb_tablesListforShift.Text = "";
            }
            else
            {
                db.comboFill(cmb_tablesListforShift, @"select tob.t_id as tableNO,MAX(tob.order_id) as OrderID from table_order tob 
                                                inner join table_status ts on ts.t_id=tob.t_id and ts.status<>'Empty' 
                                                group by tob.t_id 
                                                ", "NC_itemDetails", "tableNO", "OrderID");
                cmb_tablesListforShift.Text = "";
 
            }
        }

        private void discountReportToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void btn_reSettle_Click(object sender, EventArgs e)
        {
            //if (!db.ChkDb_Value("select * from table_status where t_id='" + txtTableNo.Text + "' and status='Processing'"))
            //{
            //    db.update("update table_status set status='Processing' where t_id='" + txtTableNo.Text + "' ");
            //}
            //else
            //{
            //    MessageBox.Show("Bill can not resettle this time, please try after clear current Table bill.");
 
            //}

            MessageBox.Show("Bill is Avaliable for Re-settle, Note : Changes in resettle bill can not be revart. ");

        }

        private void rbtFoodPer_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtFoodPer.Checked)
            {
                txtFoodPerc.ReadOnly = false;
                txtFoodAmt.Text = "0";
                txtFoodPerc.Text = "0";

            }
             
        }

        private void rbtFoodAmt_CheckedChanged(object sender, EventArgs e)
        {

            if (rbtFoodAmt.Checked)
            {

                txtFoodAmt.ReadOnly = false;
                txtFoodPerc.Text = "0";



            }
        }

        private void rbtLiqDisVal_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtLiqDisVal.Checked)
            {
                txtLiquorPerc.ReadOnly = false;

                txtLiquorPerc.Text = "0";
                txtLiquorAmt.Text = "0";
            }
        }

        private void rbtLiqDisAmt_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtLiqDisVal.Checked)
            {
                txtLiquorAmt.ReadOnly = false;
                txtLiquorPerc.Text = "0";
                txtLiquorAmt.Text = "0";
            }
        }

        private void rbtBevDisVal_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtBevDisVal.Checked)
            {
                txtBeveragesPerc.ReadOnly = false;
                txtBeveragesPerc.Text = "0";
                txtBeveragesAmt.Text = "0";
            }
        }

        private void rbtBevDisAmt_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtBevDisAmt.Checked)
            {
                txtBeveragesAmt.ReadOnly = false;
                txtBeveragesPerc.Text = "0";
                txtBeveragesAmt.Text = "0";
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void updateCustmer_Click(object sender, EventArgs e)
        {
         
            if (panelcustinfo.Visible)
            {
                db.update("update table_order set  Remark='" + txtcustremark.Text + "' ,Custname='" + txtCustname.Text + "',mob='" + txtCustmob.Text + "' ,Custgst='" + txtcustgst.Text + "' where order_id='" + label_order_id.Text + "' and t_id='" + txtTableNo.Text + "'");

              //  db.insert("insert into Custmer(name,phone,remark,) ");
                MessageBox.Show(" Details Save Sucessfully");
             
               txtcustremark.Text="";
               txtCustname.Text="";
               txtCustmob.Text ="";
               txtcustgst.Text = "";

               panelcustinfo.Visible = false;
               

            }
           
        }

        private void linkLabel3_LinkClicked_2(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (label_order_id.Text != "0" && txtTableNo.Text !="0")
            {
                panelcustinfo.Visible = true;

                string qur = "select * from table_order where order_id='" + label_order_id.Text + "' and t_id='" + txtTableNo.Text + "' ";
             
          
                SqlCommand cmd = new SqlCommand(qur, db.cn);
                db.cnopen();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read() == true)
                {
                    txtCustname.Text = rd["Custname"].ToString();
                    txtCustmob.Text = rd["mob"].ToString();
                    txtcustgst.Text = rd["Custgst"].ToString();
                    txtcustremark.Text = rd["Remark"].ToString();

                }
                db.cnclose();

            }
            else
            {
                MessageBox.Show("Please Select Table Order .");
            }
        }

        private void button34_Click(object sender, EventArgs e)
        {
            panelcustinfo.Visible = false;
        }

        private void button35_Click(object sender, EventArgs e)
        {
              //string itemName;
              //  double qty1, rate, amt;
                db.cnopen();
                //int i = dataGridView1.SelectedCells[0].RowIndex;
                // qty1 = double.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
                //itemName = dataGridView1.Rows[i].Cells[2].Value.ToString();
                //rate = double.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString());
                //amt = double.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString());

                DialogResult dlgresult = MessageBox.Show("Are you sure want to Delete?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgresult == DialogResult.Yes)
                {
                    //Reason reason_obj = new Reason(itemName, qty1);
                    //reason_obj.ShowDialog();
                
                    //try
                    //{
                    //    if (reason_obj.reason != "")
                    //    {
                    //        db.insert("insert into DeletedItemMaster (orderId,tblNo,deleteDate,deleteTime,itemName,rate,qty,amount,userName,reason) values('" + label_order_id.Text + "','" + txtTableNo.Text + "','" + Convert.ToDateTime(label_date.Text).ToString("MM/dd/yyyy") + "','" + Convert.ToDateTime(label_time.Text).ToString("hh:mm:ss") + "','" + itemName + "','" + rate + "','" + qty1 + "','" + amt + "','" + lbl_name.Text + "','" + reason_obj.reason + "')");

                    db.DeleteData("delete FROM sales_item where menu_id=(select menu_id from menu where m_name='" + lbl_nc_menu.Text + "') and order_id='" + label_order_id.Text + "'", "sales_item");
                    MessageBox.Show("Item Deleted");

                    name = db.getDbstatus_Value(" Select category from menu where menu_id=(select menu_id from menu where m_name='" + lbl_nc_menu.Text + "') ");
                            if (name == "Hard Drinks")
                            {

                                stockqty = db.getDb_Value("select qty from tbl_stock where item_name='" + lbl_nc_menu.Text + "'").ToString();
                                double sum = double.Parse(stockqty) + double.Parse(lbl_ncMenuQty.Text);
                                db.update("update tbl_stock set qty='" + sum + "'  where item_name='" + lbl_nc_menu.Text + "'");


                            }
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    MessageBox.Show("Error  " + ex.Message);
                    //}
                    db.cnclose();
                    bind(null);
                    btnApplyFunction();
                }
        }

        private void btnDeleteitem_Click(object sender, EventArgs e)
        {

            // delete without insert into cancel item without print
            cancelitem(true);
        }

        private void tab_cancelItem_Click(object sender, EventArgs e)
        {
            //if (db.ChkDb_Value("select *  from tbl_login where User_Name='" + lbl_name.Text + "' and type='admin'"))
            //{
            //    btnDeleteitem.Visible = true;
            //}
        }

        private void panelServicecharge_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitTableOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Splitorder sp = new Splitorder();
            sp.ShowDialog();
        }

        private void PanelParcel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void stockToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Stock_Details dtl = new Stock_Details();
            dtl.ShowDialog();

        }

        private void drinkStockReportToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            drinkStockDetails drink = new drinkStockDetails();
            drink.ShowDialog();
        }

        private void wareHouseStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            warehousestock ws = new warehousestock();
            ws.ShowDialog();
        }

        private void materialTransferMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Material_Transfer mt = new Material_Transfer();
            mt.ShowDialog();
         }

        private void materialTransferLedgerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MaterialTransferDtls md = new MaterialTransferDtls();
            md.ShowDialog();
        }

        private void pnlDiscount_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tabmerge_Click(object sender, EventArgs e)
        {

        }

        private void tabService_Click(object sender, EventArgs e)
        {

        }

        private void cbmergetbl_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblmergeoid.Text = db.getDbstatus_Value("select ISNULL((select top 1 order_id from table_order where t_id='" + cbmergetbl.Text + "'  order by 1 desc ),0)");
        }

        private void button35_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (lblmergeoid.Text != "0")
                {
                    if (dataGridView1.Rows.Count >= 0)
                    {
                        DialogResult dialogResult = MessageBox.Show("Do You Want To Merge Order?", "Hotel", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                        if (dialogResult == DialogResult.Yes)
                        {
                            Reason reason_obj = new Reason();
                            reason_obj.ShowDialog();
                            if (reason_obj.reason != "")
                            {




                                db.insert("insert into Cancel_order ( order_id, t_id, amount, by_whome, date, reason, time) values('" + label_order_id.Text + "','" + txtTableNo.Text + "','" + label_total_bill.Text + "','" + lbl_name.Text + "','" + Convert.ToDateTime(label_date.Text).ToString("MM/dd/yyyy") + "','" + reason_obj.reason + "','" + label_time.Text + "')");
                                // db.insert("insert into CancelKOTDetails ([orderId],[tblNo],[menuName],[qty],[rate],[amount] ,[date],[waiterName],cancelKotTime,cancelKotReson) values('" + label_order_id.Text + "','" + txtTableNo.Text + "','" + lbl_nc_menu.Text + "','" + txtCancelQty.Text + "','" + lbl_ncMenuRate.Text + "','" + (float.Parse(lbl_ncMenuRate.Text) * float.Parse(txtCancelQty.Text)) + "','" + Convert.ToDateTime(label_date.Text).ToString("MM/dd/yyyy") + "','" + lbl_name.Text + "','" + Convert.ToDateTime(label_time.Text).ToString("hh:mm:ss") + "','" + cmb_cancelKotReson.Text + "')");






                                db.insert(@"  insert into CancelKOTDetails ([orderId],[tblNo],[menuName],[qty],[rate],[amount] ,[date],[waiterName],cancelKotTime,cancelKotReson)"
                                          + "select order_id,'" + txtTableNo.Text + "',m.m_name,qty,si.rate,si.total_amount,Date,'" + lbl_name.Text + "','" + label_time.Text + "','" + reason_obj.reason + "' "
                                          + " from sales_item si inner join menu m on si.menu_id=m.menu_id where order_id='" + label_order_id.Text + "'");


                                db.update("UPDATE sales_item SET order_id='" + lblmergeoid.Text + "' WHERE order_id='" + lblcmergeoid.Text + "'");




                                //db.update(" update sales_item set  total_amount=0 where order_id='" + label_order_id.Text + "' ");
                                //db.update("update table_order set foodDiscValue='0',foodDiscAmt='0', liquorDiscValue='0',liquorDiscAmt='0',beveragesDiscValue='0' ,beveragesDiscAmt='0',gst=0 where order_id='" + label_order_id.Text + "' ");
                                //db.update("update table_order set vatAmt=0,serviceTaxVal='0', serviceTaxFoodVal='0', serviceTaxLiquorVal='0', serviceTaxAmt='0',  serviceTaxLiquorAmt='0', serviceTaxFoodAmt='0' where order_id='" + label_order_id.Text + "'");





                                //db.update("Delete from sales_item where order_id='" + label_order_id.Text + "' ");
                                //db.update("Delete from table_order where order_id='" + label_order_id.Text + "' ");


                                btn_add.Enabled = false;
                                btnCancelOrder.Enabled = false;

                                //update status of table here 
                                db.update("UPDATE table_status SET status='Empty' WHERE t_id='" + txtTableNo.Text + "'");
                                db.update("UPDATE table_order SET tableStatus='Empty' WHERE t_id='" + txtTableNo.Text + "' and order_id='" + label_order_id.Text + "'");

                                //table_status = "Empty";

                                //update_tb_status((txtTableNo.Text.ToString()));
                                //chk_tb_status();
                                dataGridView1.DataSource = "";
                                txtTableNo.Text = "0";
                                label_order_id.Text = "";
                                label_total_bill.Text = "0";

                                chkAmt.Checked = false;
                                chkPerc.Checked = false;
                                txtDiscAmt.Text = "0";
                                txtDiscValue.Text = "0";

                                lblStatus.Visible = false;
                                lblTotalAmt.Text = "0";
                                lblDisc.Text = "0";

                                pnlDiscount.Visible = false;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please Add Items", "Hotel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Please Select Merge Table & Check Order ID", "Hotel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            
        }

        private void empSectionAssignToolStripMenuItem_Click(object sender, EventArgs e)
        {

            waiterassign ws = new waiterassign();
            ws.ShowDialog();
            
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void panelcustinfo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void menuUploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MenuUpload mp = new MenuUpload();
            mp.ShowDialog();
        }

        private void button36_Click(object sender, EventArgs e)
        {
            if (label_order_id.Text != "0" && txtTableNo.Text != "0")
            {
                panelcustinfo.Visible = true;

                string qur = "select * from table_order where order_id='" + label_order_id.Text + "' and t_id='" + txtTableNo.Text + "' ";


                SqlCommand cmd = new SqlCommand(qur, db.cn);
                db.cnopen();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read() == true)
                {
                    txtCustname.Text = rd["Custname"].ToString();
                    txtCustmob.Text = rd["mob"].ToString();
                    txtcustgst.Text = rd["Custgst"].ToString();
                    txtcustremark.Text = rd["Remark"].ToString();

                }
                db.cnclose();

            }
            else
            {
                MessageBox.Show("Please Select Table Order .");
            }
        }

        private void btnMore_Click(object sender, EventArgs e)
        {
           // tabControl2.Pages[1].Visible = false;
            tabControl2.TabPages.Remove(tabService);
            if (label_order_id.Text != "0")
            {

                pnlDiscount.Visible = true;
                if (rbt_allDiscount.Checked)
                {
                    txtDiscAll.Visible = true;
                }
                db.comboFill(cmb_discountReson, "select DiscountReson from table_order where DiscountReson<>'' group by DiscountReson", "table_order", "DiscountReson", "DiscountReson");
                cmb_discountReson.Text = "";


                cbCurenttble.Text = txtTableNo.Text;
                lblcmergeoid.Text = label_order_id.Text;

                db.comboFill(cbmergetbl, "select t_id from table_status where   status='Processing' and t_id<>'" + cbCurenttble.Text + "'", "table_status", "t_id", "t_id");

                cbmergetbl.Text = "Select";

                txtFoodPerc.Text = "0";
                txtLiquorPerc.Text = "0";
                txtBeveragesPerc.Text = "0";

                chkFoodPerc.Checked = false;
                chkLiquorPerc.Checked = false;
                chkBevPerc.Checked = false;




            }
            else
            {
                MessageBox.Show("Please Select Table Order .");
            }
        }

        private void tabDisc_Click(object sender, EventArgs e)
        {

        }

        private void Welcome_KeyPress(object sender, KeyPressEventArgs e)
        {
             
        }

        private void tableReserveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reservetable rt = new reservetable();
            rt.ShowDialog();
        }

        private void openingStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openingfoodstock fs = new openingfoodstock();
            fs.ShowDialog();
        }

        private void openingDrinkStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openingdrinkstock DS = new openingdrinkstock();
            DS.ShowDialog();
        }

        private void button37_Click(object sender, EventArgs e)
        {
            panelreserv.Visible = false;
        }

        int sira = 0;
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            bind_itemsList();
        }

        private void listView_items_SelectedIndexChanged(object sender, EventArgs e)
        {
           // panelreserv.Visible = false;
            string seletedItemName = "";
            int qty = 1;
            int get_qty = 0, flag = 0;



            if (listView_items.SelectedItems.Count > 0)
            {
                seletedItemName = listView_items.SelectedItems[0].Text;
                tblBindList(seletedItemName);
            }
        }

        void tblBindList(string tbl_no)
        {
            //yogesh 09052019


           


                string table = null;

                //table = tbl_no.RemoveWhitespace();

                table = string.Join(" ", tbl_no.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
                btn_add.Enabled = true;
                btnCancelOrder.Enabled = true;
                txtTableNo.Text = table.Trim();
                if (db.ChkDb_Value("select status from table_status where  t_id='" + txtTableNo.Text + "' and  status='Reserve'"))
                {
                    // chk_tb_status();

                    chk_tb_statusList();
                    panelreserv.Visible = true;
                    txtremark.Text = db.getDbstatus_Value("select reason from tbl_reservation where  tid='" + txtTableNo.Text + "' ");

                }
                else
                {

                table_status = "Processing";
                get_table_status();

                if (db.ChkDb_Value("select tblStatus from table_status where t_id='" + txtTableNo.Text + "' and  tblStatus='All'"))
                    chk_tb_statusListLoad();

                chk_tb_statusList();
                update_tb_status((txtTableNo.Text.ToString()));
            }
            // }
            if (db.ChkDb_Value("select status from table_status where  t_id='" + tbl_no + "' and  status='Empty'"))
            {
//panelorder.Visible = false;
                btnPayBill.Enabled = false;
                btnCancelOrder.Enabled = false;
                button_print.Enabled = false;
                btn_KOTprint.Enabled = false;
                linkCancelKot.Enabled = false;
            }
            else
            {
               // panelorder.Visible = true;
                btnPayBill.Enabled = true;
                btnCancelOrder.Enabled = true;
                button_print.Enabled = true;
                btn_KOTprint.Enabled = true;
                linkCancelKot.Enabled = true;
            }
            if (db.ChkDb_Value("select status from table_status where  t_id='" + tbl_no + "' and  status='Printing'"))
            {
                linkCancelKot.Enabled = false;
            }

            if (db.ChkDb_Value("select discValue from table_order where t_id='" + tbl_no + "' and tableStatus='Processing' and order_id='" + label_order_id.Text + "'"))
            {
               // panelorder.Visible = true;
                txtDiscValue.Text = db.getDb_Value("select discValue from table_order where t_id='" + tbl_no + "' and tableStatus='Processing' and order_id='" + label_order_id.Text + "'").ToString();

            }
            else
            {
                txtDiscValue.Text = "0";

            }

            if (txtDiscValue.Text != "0")
            {
                chkPerc.Checked = true;
                lblStatus.Visible = true;
            }
            else
            {
                chkPerc.Checked = false;
                lblStatus.Visible = false;

            }

            if (db.ChkDb_Value("select discAmt from table_order where t_id='" + tbl_no + "' and tableStatus='Processing' and order_id='" + label_order_id.Text + "'"))
            {
               // panelorder.Visible = true;
                txtDiscAmt.Text = db.getDb_Value("select discAmt from table_order where t_id='" + tbl_no + "' and tableStatus='Processing' and order_id='" + label_order_id.Text + "'").ToString();
            }
            else
            {
                txtDiscAmt.Text = "0";
                //panelorder.Visible = false;
            }

            if (txtDiscAmt.Text != "0")
            {
                chkAmt.Checked = true;
                lblStatus.Visible = true;

            }
            else
            {
                chkAmt.Checked = false;
                lblStatus.Visible = false;

            }
            ApplyDisc();
        }

        private void listView1_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
            //e.Graphics.DrawRectangle(Pens.Black, e.Bounds);
        }

        private void listView1_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.Graphics.FillRectangle(SystemBrushes.Menu, e.Bounds);
            e.Graphics.DrawRectangle(SystemPens.GradientInactiveCaption,
                new Rectangle(e.Bounds.X, 0, e.Bounds.Width, e.Bounds.Height));

            string text = listView1.Columns[e.ColumnIndex].Text;
            TextFormatFlags cFlag = TextFormatFlags.HorizontalCenter
                                  | TextFormatFlags.VerticalCenter;
            TextRenderer.DrawText(e.Graphics, text, listView1.Font, e.Bounds, Color.Black, cFlag);
        }

        private void listView1_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void categorySettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Category cb = new Category();
            cb.ShowDialog();
        }

        private void button39_Click(object sender, EventArgs e)
        {
            panelorder.Visible = false;
        }

        private void lblDiscountReason_Click(object sender, EventArgs e)
        {

        }

        private void button44_Click(object sender, EventArgs e)
        {

        }

        private void button42_Click(object sender, EventArgs e)
        {
            TodayCounter tc = new TodayCounter();
            tc.ShowDialog();

            
        }

        private void button43_Click(object sender, EventArgs e)
        {
            Menu_Details sd = new Menu_Details();
           // Stock_Details sd = new Stock_Details();
            sd.ShowDialog();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label_hotelName_Click(object sender, EventArgs e)
        {

        }

        private void label93_Click(object sender, EventArgs e)
        {

        }

        private void button44_Click_1(object sender, EventArgs e)
        {
            pnlDiscount.Visible = false;
        }
       //// string status = "", t_id = "";
       // int cnt = 0;
       // public void chk_tb_statusList()
       // {
       //     try
       //     {
       //         sira = listView_items.Items.Count;
       //         if (listView1.SelectedItems.Count >= 1)
       //         {
       //             selected_item = listView1.SelectedItems[0].Text;

       //         }
       //         int i = 0;
       //         SqlCommand cmd = new SqlCommand("select id, t_id,status,lblName from table_status where lblName='" + selected_item + "' and tblStatus='Active'", db.cn);
       //         db.cnopen();
       //         SqlDataReader rd = cmd.ExecuteReader();
       //         while (rd.Read() == true)
       //         {
       //             status = rd["status"].ToString();
       //             t_id = rd["t_id"].ToString();
       //             lbl = rd["lblName"].ToString();
       //             //= int.Parse(rd["id"].ToString());                    
       //             if (status == "Empty")
       //             {
       //                 listView_items.Items[i].BackColor = Color.Green;
       //                 i++;
       //             }
       //             if (status == "Processing")
       //             {
       //                 listView_items.Items[i].BackColor = Color.Red;
       //                 i++;
       //             }
       //             if (status == "Printing")
       //             {
       //                 listView_items.Items[i].BackColor = Color.Pink;
       //                 i++;
       //             }
       //             //}


       //         }


       //     }
       //     catch (Exception ex)
       //     {
       //         MessageBox.Show(ex.Message);
       //     }
       //     finally
       //     {
       //         db.cnclose();
       //     }
       // }
       // public void chk_tb_statusListLoad()
       // {
       //     try
       //     {
       //         //sira = listView_items.Items.Count;

       //         int i = 0;
       //         SqlCommand cmd = new SqlCommand("select id, t_id,status,lblName from table_status where tblStatus='Active' ", db.cn);
       //         db.cnopen();
       //         SqlDataReader rd = cmd.ExecuteReader();
       //         while (rd.Read() == true)
       //         {
       //             status = rd["status"].ToString();
       //             t_id = rd["t_id"].ToString();
       //             lbl = rd["lblName"].ToString();
       //             //= int.Parse(rd["id"].ToString());                    
       //             if (status == "Empty")
       //             {
       //                 listView_items.Items[i].BackColor = Color.Green;
       //                 i++;
       //             }
       //             if (status == "Processing")
       //             {
       //                 listView_items.Items[i].BackColor = Color.Red;
       //                 i++;
       //             }
       //             if (status == "Printing")
       //             {
       //                 listView_items.Items[i].BackColor = Color.Pink;
       //                 i++;
       //             }
       //             //}


       //         }


       //     }
       //     catch (Exception ex)
       //     {
       //         MessageBox.Show(ex.Message);
       //     }
       //     finally
       //     {
       //         db.cnclose();
       //     }
       // }

    }
}