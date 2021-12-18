using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Data.SqlClient;
using System.Net.Mail;
//using Microsoft.Office.Interop.Excel;
using System.Diagnostics;

namespace Hotal_Managment_Syatem
{
    
    public partial class DayEnd_bkupServices : Form
    {
        DbBackup bc = new DbBackup();
        Database db = new Database();
        public DayEnd_bkupServices()
        {
            InitializeComponent();
        }

        private void DayEnd_bkupServices_Load(object sender, EventArgs e)
        {

            db.formFix(this);
            lblDate.Text = db.getDbstatus_Value("select ddate from tbl_dayend_status");
            this.CancelButton = buttonclose;
           
            
            //code added by sagar for hide data clear option for customer
            if (db.ChkDb_Value("select status from tbl_option where grp='DataClearOption' and status='Yes'"))
            {
                rdb_date_bkup.Visible = true;
            }
            else
                rdb_date_bkup.Visible = false;
        }

        private void buttonclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonBkupExit_Click(object sender, EventArgs e)
        {
            String path = db.getDbstatus_Value("select top 1 path from tbl_dbBackup   order by 1 Asc");
            String databaseName = db.getDbstatus_Value("select top 1 databasename from tbl_dbBackup   order by 1 Asc");
            bc.DBBackup(databaseName, path);         

            //edit by harsha            
            if (rdb_only_dt.Checked)
            {
                sendmail();
                daychange(); //only date change
            }
            else
            {
                date_and_backup(); // both date and backup
            }

            db.update("truncate table  tbl_temporder");
            db.update("truncate table tbl_orderSuggestion ");
         
            
            string getIISServicePath = db.getDbstatus_Value("select value from tbl_option where grp='IISServicePath' and status='Yes'");
            if (getIISServicePath != "")
                Process.Start(@getIISServicePath, "/noforce");



        }
        //***************************** DATE CHANGE ONLY *****************************************
            public void daychange()
            {
                  string status = db.getDbstatus_Value("select count(*) from table_status where status<>'Empty'");
                  if (int.Parse(status) == 0)
                  {
                      string ddate = db.getDbstatus_Value("select ddate from tbl_dayend_status");
                      DateTime dtd = Convert.ToDateTime(ddate);
                      dtd = dtd.AddDays(1);
                      string newdate = dtd.ToString("dd'/'MM'/'yyyy");
                      db.update("update tbl_dayend_status set ddate='" + newdate + "'");
                      System.Windows.Forms.Application.Exit();
                  }
                  else
                  {
                      MessageBox.Show("Please Pay And Clear All Bills!!!");
                  }
            }

        //************************* BOTH DATE AND BACKUP *************************************************
            public void date_and_backup()
            {
                string status = db.getDbstatus_Value("select count(*) from table_status where status<>'Empty'");
                if (int.Parse(status) == 0)
                {
                    string dt1 = db.getDbstatus_Value("select ddate from tbl_dayend_status");
                    DateTime dt = new DateTime();
                    dt = Convert.ToDateTime(dt1);
                  //  string path = "f:\\Today_sales\\";
                    string path = db.getDbstatus_Value("select value from tbl_option where grp='Report Location'");

                    try
                    {
                        if (!File.Exists(path))
                        {
                            System.IO.Directory.CreateDirectory(path);
                        }

                        try
                        {

                            //SaveFileDialog sfd = new SaveFileDialog();
                            //sfd.Filter = "Excel Documents (*.xls)|*.xls";//"Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";



                            //  if (sfd.ShowDialog() == DialogResult.OK)
                            if (true)
                            {
                                excelExport(path);

                                // ******************* waiter wise sale *****************
                                //sfd.FileName = path + "WaiterSalesReport_" + dt.ToString("MM-dd-yyyy") + ".xls";
                                //dataGridView_ReportHolder.DataSource = db.Displaygrid(" SELECT  waiter_dtls.wname, SUM(total_bill.Total_bill) AS TotalAmount FROM            waiter_dtls INNER JOIN table_order ON waiter_dtls.w_id = table_order.w_id INNER JOIN  total_bill ON table_order.order_id = total_bill.order_id   GROUP BY waiter_dtls.wname");
                                //db.withReportTitle_ToCsV(dataGridView_ReportHolder, sfd.FileName, "Waiter Wise Sales Report for date :" + dt.ToString()); // Here dataGridview1 is your grid view name 


                               
                                // *********************** Order Cancel Report *************************
                                //sfd.FileName = "TableSalesReport_" + dt.ToString() + ".xls";
                                //dataGridView_ReportHolder.DataSource = db.Displaygrid(" SELECT  waiter_dtls.wname, SUM(total_bill.Total_bill) AS TotalAmount FROM            waiter_dtls INNER JOIN table_order ON waiter_dtls.w_id = table_order.w_id INNER JOIN  total_bill ON table_order.order_id = total_bill.order_id   GROUP BY waiter_dtls.wname");
                                //db.withReportTitle_ToCsV(dataGridView_ReportHolder, sfd.FileName, "Table Wise Sales Report for date :" + dt.ToString()); // Here dataGridview1 is your grid view name 

                               
                                MessageBox.Show("File Sucessfully Exported");
                                //db.insert("insert into dummySalesItem(order_id,menu_id,qty,rate,total_amount,w_id,kot_id,KOTStatus,captain_id,orderDate) SELECT  sales_item.order_id, sales_item.menu_id, sales_item.qty, sales_item.rate, sales_item.total_amount, sales_item.w_id, sales_item.kot_id, sales_item.KOTStatus,  '" + dt.ToString("MM-dd-yyyy") + "'  FROM            sales_item INNER JOIN    total_bill ON sales_item.order_id = total_bill.order_id INNER JOIN   table_order ON sales_item.order_id = table_order.order_id WHERE (total_bill.status = 'Credit')");
                                db.insert("insert into dummySalesItem(order_id,menu_id,qty,rate,total_amount,w_id,kot_id,orderDate) SELECT  sales_item.order_id, sales_item.menu_id, sales_item.qty, sales_item.rate, sales_item.total_amount, sales_item.w_id, sales_item.kot_id, '" + dt.ToString("MM-dd-yyyy") + "'  FROM            sales_item INNER JOIN    total_bill ON sales_item.order_id = total_bill.order_id INNER JOIN   table_order ON sales_item.order_id = table_order.order_id WHERE        (total_bill.status = 'Credit')");

                                db.insert("insert into dummyTableOrder(order_id,t_id,timeing,w_id,order_type,tableStatus,discValue,discAmt) SELECT        table_order.order_id, table_order.t_id, table_order.timeing, table_order.w_id, table_order.order_type, table_order.tableStatus, table_order.discValue, table_order.discAmt FROM table_order INNER JOIN   total_bill ON table_order.order_id = total_bill.order_id WHERE (total_bill.status = 'Credit') ");
                                db.insert("insert into dummytotal_bill(order_id,Total_bill,datetime,status,Customer_id,transaction_id,remark,billTime) SELECT order_id,Total_bill,datetime,status,Customer_id,transaction_id,remark,billTime FROM total_bill WHERE status = 'Credit' ");
                                //sendsms();
                                sendmail();
                                db.DeleteData("truncate table total_bill", "total_bill"); //sales_item
                                db.DeleteData("truncate table sales_item", "sales_item");
                                db.DeleteData("truncate table table_order", "table_order");
                                db.DeleteData("truncate table tbl_parcel_order", "tbl_parcel_order");
                                db.DeleteData("truncate table CancelKOTDetails", "CancelKOTDetails");
                                db.DeleteData("truncate table Cancel_order", "Cancel_order");
                                db.DeleteData("truncate table tbl_orderSuggestion", "tbl_orderSuggestion");



                                daychange();
                                //************ Reset (PK) id to 1 *************
                                db.insert("DBCC CHECKIDENT (sales_item, RESEED, 0)");
                                db.insert("DBCC CHECKIDENT (total_bill, RESEED, 0)");
                                db.insert("DBCC CHECKIDENT (tbl_parcel_order, RESEED, 0)");
                                db.insert("DBCC CHECKIDENT (CancelKOTDetails, RESEED, 0)");
                                db.insert("DBCC CHECKIDENT (Cancel_order, RESEED, 0)");
                                db.insert("DBCC CHECKIDENT (tbl_orderSuggestion, RESEED, 0)");

                                //Application.Exit();
                                System.Windows.Forms.Application.Exit();

                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Report Location not found , please check or set Report Location in Setting");
                    }


                }
                else
                {
                    MessageBox.Show("Please Pay And Clear All Bills!!!");
                }
            }

        //****************Send SMS*************************************************
            string mobileno = "";
            string msg = "";
            string name = "";
            public void sendsms()
            {
                string[] mobi = new string[200];
                string[] name = new string[200];

                string dt1 = db.getDbstatus_Value("select ddate from tbl_dayend_status");
                //string dt1 = System.DateTime.Now.ToString("dd/MM/yyyy");
                DateTime dt = new DateTime();
                dt = Convert.ToDateTime(dt1);

                int count = 0;
                count = 0;
                float salesTotal = 0, purchaseTotal = 0, expTotal = 0;
                salesTotal = db.getDb_Value("select sum(Total_bill) from total_bill where datetime='" + dt.ToString("MM-dd-yyyy") + "'");
                purchaseTotal = db.getDb_Value("select sum(amt) from tbl_purchasemaster where date='" + dt.ToString("MM-dd-yyyy") + "'");
                expTotal = db.getDb_Value("select sum(amount) from tbl_expenses where date='" + dt.ToString("MM-dd-yyyy") + "'");

                msg = "\n Date :" + dt.ToString("dd/MM/yyyy") + "\n\n  Sales Total :" + salesTotal + "\n Purchase Total :" + purchaseTotal + " \n Expences Total :" + expTotal + ""; //Hello " + name + ",

                db.cnopen();

                SqlCommand cmdowner = new SqlCommand("select * from OwnerDetails", db.cn);
                SqlDataReader rdow = cmdowner.ExecuteReader();
                while (rdow.Read())
                {
                    name[count] = rdow["Name"].ToString();
                    mobi[count] = rdow["MobileNo"].ToString();
                    count++;
                }
                db.cnclose();
                for (int i = 0; i < count; i++)
                {
                    smssendingcode(mobi[i], name[i], msg);
                }


            }

            public void smssendingcode(string mobileno, string name, string msg)
            {
                string urlName = "", senderName = "", smsType = "", apiKey = "";
                try
                {
                    this.mobileno = mobileno;
                    this.msg = msg;
                    this.name = name;
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

                    string url = urlName + name.Trim() + "," + msg.Trim() + "&sendername=" + senderName + "&smstype=" + smsType + "&numbers=" + mobileno + "&apikey=" + apiKey + "";

                    smsApiCall(url);

                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                    MessageBox.Show("Unable To Send SMS");
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
           
            void sendmail()
            {
               
                string ownerId = "";
                try
                {
                    SmtpClient client = new SmtpClient("smtp.gmail.com");
                    client.Port = 587;
                    client.EnableSsl = true;
                    client.Timeout = 100000;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;                     //email id       , passward
                    client.Credentials = new System.Net.NetworkCredential("hotel.restrosoft@gmail.com", "abms@123");
                    MailMessage msg = new MailMessage();
                    SaveFileDialog sfd = new SaveFileDialog();
                    string dt1 = db.getDbstatus_Value("select ddate from tbl_dayend_status");
                    DateTime dt = new DateTime();

                    dt = Convert.ToDateTime(dt1);
                    string rptDt = Convert.ToDateTime(dt).ToString("MM-dd-yyyy");
                    string path = "d:\\MailReport\\";
                    excelExport(path);

                    if (db.ChkDb_Value("select status from tbl_option where grp='" + "ExcelReport" + "' and status='Yes'"))//Prathmesh New Code added
                    {
                        string[] pdfFiles = GetFileNames(@"D:\\MailReport", "*.xls", rptDt);
                         for (int i = 0; i < pdfFiles.Length; i++)
                        {
                            string result = pdfFiles[i];                            
                            msg.Attachments.Add(new Attachment(@"D:\\MailReport\" + result));
                        }
                    }
                   

                    float salesTotal = 0, purchaseTotal = 0, expTotal = 0;
                    salesTotal = db.getDb_Value("select sum(Total_bill) from total_bill where datetime='" + dt.ToString("MM-dd-yyyy") + "'");
                    purchaseTotal = db.getDb_Value("select sum(amt) from tbl_purchasemaster where date='" + dt.ToString("MM-dd-yyyy") + "'");
                    expTotal = db.getDb_Value("select sum(amount) from tbl_expenses where date='" + dt.ToString("MM-dd-yyyy") + "'");

                //*************Pdf Report Genration and attach to mail********************************

                if (db.ChkDb_Value("select status from tbl_option where grp='" + "Pdf Report" + "' and status='Yes'"))
                {
                    string[] pdfFiles = GetFileNames(@"D:\\MailReport", "*.xls", rptDt);

                    for (int i = 0; i < pdfFiles.Length; i++)
                    {
                        string result = Path.ChangeExtension(pdfFiles[i], ".pdf");
                        Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                        Workbook wkb = app.Workbooks.Open(@"D:\\MailReport\" + pdfFiles[i]);
                        wkb.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, @"D:\\MailReport\" + result);
                        msg.Attachments.Add(new Attachment(@"D:\\MailReport\" + result));
                    }
                }
                string companyNm = "";
                    db.cnopen();
                    SqlCommand cmd = new SqlCommand("Select * from tbl_CompanyInfo", db.cn);
                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {                       
                        companyNm = rd["companyName"].ToString();
                    }
                    db.cnclose();



                    db.cnopen();
                    SqlCommand cmdEmail = new SqlCommand("Select * from UserEmailID", db.cn);
                    SqlDataReader rdEmail = cmdEmail.ExecuteReader();

                    while (rdEmail.Read())
                    {
                        ownerId = rdEmail["emailId"].ToString();

                        msg.To.Add(ownerId);// sagarsawant19@yahoo.com
                        msg.From = new MailAddress("hotel.restrosoft@gmail.com");
                        msg.Subject = companyNm + " Transaction Details Report For : " + dt.ToString("dd/MM/yyyy") + "";
                        msg.Body = "Date :" + dt.ToString("dd/MM/yyyy") + "\n\n  Sales Total :" + salesTotal + "\n Purchase Total :" + purchaseTotal + " \n Expences Total :" + expTotal + "\n\n\n\n \n\n Thanks And Regards,\n\n " + companyNm + "";// "Thanks for visiting our Hotel...Come again And Enjoy Your Service : \n\n    Your Total Reward Points Are :" + txtTotalReward.Text + " \n\n Thanks And Regards,\n\n " + companyNm+ "";

                        client.Send(msg);
                    }
                    

                    db.cnclose();


                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.ToString());
                    MessageBox.Show("Unable to Send Mail !!!!");
                }
            }
            //string filePath = "";
            void excelExport(string filePath)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                string dt1 = db.getDbstatus_Value("select ddate from tbl_dayend_status");
                DateTime dt = new DateTime();

                dt = Convert.ToDateTime(dt1);
                string rptDt = Convert.ToDateTime(dt).ToString("MM-dd-yyyy");

                string path = filePath;// "d:\\MailReport\\";
                //string path = db.getDbstatus_Value("select value from tbl_option where grp='Report Location'");
                if (!File.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }


                if (db.ChkDb_Value("select status from tbl_option where grp='" + "TodaysSalesReport" + "' and status='Yes'"))
                {
                    sfd.FileName = path + "TodaysSalesReport_" + dt.ToString("MM-dd-yyyy") + ".xls";
                    dataGridView_ReportHolder.DataSource = db.Displaygrid("SELECT   total_bill.order_id AS[Order ID], table_order.t_id as [Table No], total_bill.Total_bill AS[Total Amount ] FROM            total_bill INNER JOIN   table_order ON total_bill.order_id = table_order.order_id WHERE total_bill.datetime='" + dt.ToString("MM-dd-yyyy") + "'");
                    db.withReportTitle_ToCsV(dataGridView_ReportHolder, sfd.FileName, "Todays Sales Report for date :" + dt.ToString()); // Here dataGridview1 is your grid view name 
                }
               // msg.Attachments.Add(new Attachment(sfd.FileName));
                if (db.ChkDb_Value("select status from tbl_option where grp='" + "ExpencesReport" + "' and status='Yes'"))
                {
                    sfd.FileName = path + "ExpencesReport" + dt.ToString("MM-dd-yyyy") + ".xls";
                    dataGridView_ReportHolder.DataSource = db.Displaygrid("select material_nm as Material_Name,qty as Qty,unit as Unit,amount as Amount,date as Date,remark As Remark from tbl_expenses where date='" + dt.ToString("MM-dd-yyyy") + "' ");//where material_nm='" + cmb_category.Text + "' and date between '" + datefrm + "' and '" + dateto + "'");
                    db.withReportTitle_ToCsV(dataGridView_ReportHolder, sfd.FileName, "Expences Report for date :" + dt.ToString()); // Here dataGridview1 is your grid view name 
                }
                //msg.Attachments.Add(new Attachment(sfd.FileName));
                if (db.ChkDb_Value("select status from tbl_option where grp='" + "ItemWiseSalesReport" + "' and status='Yes'"))
                {
                    sfd.FileName = path + "ItemWiseSalesReport_" + dt.ToString("MM-dd-yyyy") + ".xls";
                    dataGridView_ReportHolder.DataSource = db.Displaygrid("SELECT        menu.m_name AS [Item Name], SUM(sales_item.qty) AS Qty, SUM(sales_item.total_amount) AS Total FROM            total_bill INNER JOIN sales_item ON total_bill.order_id = sales_item.order_id INNER JOIN menu ON sales_item.menu_id = menu.menu_id WHERE total_bill.datetime='" + dt.ToString("MM-dd-yyyy") + "' GROUP BY menu.m_name");
                    db.withReportTitle_ToCsV(dataGridView_ReportHolder, sfd.FileName, "Item Wise Sales Report for date :" + dt.ToString()); // Here dataGridview1 is your grid view name 
                }

                if (db.ChkDb_Value("select status from tbl_option where grp='" + "StockReport" + "' and status='Yes'"))
                {
                    sfd.FileName = path + "StockReport_" + dt.ToString("MM-dd-yyyy") + ".xls";
                    dataGridView_ReportHolder.DataSource = db.Displaygrid("select item_name as[Material Name],sum(qty) as Quantity,unit as Unit from tbl_stock where itemType!='drinks' GROUP BY item_name, unit");
                    db.withReportTitle_ToCsV(dataGridView_ReportHolder, sfd.FileName, "Stock Report for date :" + dt.ToString()); // Here dataGridview1 is your grid view name 
                }

                if (db.ChkDb_Value("select status from tbl_option where grp='" + "ParcelOrderReport" + "' and status='Yes'"))
                {
                    sfd.FileName = path + "ParcelOrderReport" + dt.ToString("MM-dd-yyyy") + ".xls";
                    dataGridView_ReportHolder.DataSource = db.Displaygrid("SELECT   tbl_parcel_order.Customer_id, Custmer.name AS [Customer Name], tbl_parcel_order.order_id AS [Order Id], tbl_parcel_order.order_status AS Status,   tbl_parcel_order.w_name AS [Delivery Boy], tbl_parcel_order.date as [Order Date], tbl_parcel_order.order_time as [Order Time]  FROM            tbl_parcel_order INNER JOIN     Custmer ON tbl_parcel_order.Customer_id = Custmer.Customer_id where  tbl_parcel_order.date='" + dt.ToString("MM-dd-yyyy") + "' ");//where material_nm='" + cmb_category.Text + "' and date between '" + datefrm + "' and '" + dateto + "'");
                    db.withReportTitle_ToCsV(dataGridView_ReportHolder, sfd.FileName, "Stock Report for date :" + dt.ToString()); // Here dataGridview1 is your grid view name 
                }
               

                // ***************** Cancel KOT Reports ******************
                if (db.ChkDb_Value("select status from tbl_option where grp='" + "CancelKOTReport" + "' and status='Yes'"))
                {
                    sfd.FileName = path + "CancelKOTReport" + dt.ToString("MM-dd-yyyy") + ".xls";
                    dataGridView_ReportHolder.DataSource = db.Displaygrid("SELECT        date AS Date, orderId AS [Order ID], tblNo AS [Table No.], menuName AS [Menu Name], qty AS Qty, rate AS Rate, amount AS Amount, waiterName AS [Waiter Name] FROM            CancelKOTDetails where date='" + Convert.ToDateTime(lblDate.Text).ToString("MM/dd/yyyy") + "' ");//where material_nm='" + cmb_category.Text + "' and date between '" + datefrm + "' and '" + dateto + "'");
                    db.withReportTitle_ToCsV(dataGridView_ReportHolder, sfd.FileName, "Cancel KOT Report for date :" + dt.ToString()); // Here dataGridview1 is your grid view name 
                }
                

                // ***************** Deleted Item Reports ******************
                if (db.ChkDb_Value("select status from tbl_option where grp='" + "DeletedItemReport" + "' and status='Yes'"))
                {
                    sfd.FileName = path + "DeletedItemReport" + dt.ToString("MM-dd-yyyy") + ".xls";
                    dataGridView_ReportHolder.DataSource = db.Displaygrid("SELECT        orderId AS [Order Id], tblNo AS [Table No.], deleteDate AS Date, deleteTime AS Time,  itemName AS [Item Name], rate AS Rate, qty AS Qty,  amount AS Amount,reason as Reason, userName AS [User Name] FROM            DeletedItemMaster where  deleteDate='" + dt.ToString("MM-dd-yyyy") + "' ");//where material_nm='" + cmb_category.Text + "' and date between '" + datefrm + "' and '" + dateto + "'");
                    db.withReportTitle_ToCsV(dataGridView_ReportHolder, sfd.FileName, "Deleted Item Report for date :" + dt.ToString()); // Here dataGridview1 is your grid view name 
                }
                
                // ************* table wise sale ************************  
                if (db.ChkDb_Value("select status from tbl_option where grp='" + "TableSalesReport" + "' and status='Yes'"))
                {
                    sfd.FileName = path + "TableSalesReport_" + dt.ToString("MM-dd-yyyy") + ".xls";
                    dataGridView_ReportHolder.DataSource = db.Displaygrid(" SELECT  waiter_dtls.wname, SUM(total_bill.Total_bill) AS TotalAmount FROM            waiter_dtls INNER JOIN table_order ON waiter_dtls.w_id = table_order.w_id INNER JOIN  total_bill ON table_order.order_id = total_bill.order_id WHERE total_bill.datetime='" + dt.ToString("MM-dd-yyyy") + "'  GROUP BY waiter_dtls.wname");
                    db.withReportTitle_ToCsV(dataGridView_ReportHolder, sfd.FileName, "Table Wise Sales Report for date :" + dt.ToString()); // Here dataGridview1 is your grid view name 
                }
                //msg.Attachments.Add(new Attachment(sfd.FileName));
                // *************** today sales report ***************
                if (db.ChkDb_Value("select status from tbl_option where grp='" + "SalesReport" + "' and status='Yes'"))
                {
                    sfd.FileName = path + "SalesReport_" + dt.ToString("MM-dd-yyyy") + ".xls";
                    dataGridView_ReportHolder.DataSource = db.Displaygrid("SELECT order_id, datetime, Total_bill, status as OrderType, Customer_id,  remark as Remark  FROM   total_bill where total_bill.datetime='" + dt.ToString("MM-dd-yyyy") + "'");
                    db.withReportTitle_ToCsV(dataGridView_ReportHolder, sfd.FileName, "Sales Report for date :" + dt.ToString()); // Here dataGridview1 is your grid view name 
                }
                //msg.Attachments.Add(new Attachment(sfd.FileName));
            }


            private static string[] GetFileNames(string path, string filter, string reportDate)
            {
                string[] files = Directory.GetFiles(path, filter);
                string[] selctFile = new string[200];
                string fName = string.Empty;
                for (int i = 0; i < files.Length; i++)
                {
                    fName = Path.GetFileName(files[i]);
                    //chk file name with todays date if its match
                    if (fName.Contains(reportDate))
                        selctFile[i] = Path.GetFileName(files[i]);
                }
                return selctFile.Where(c => c != null).ToArray();
            }
            private void rdb_only_dt_CheckedChanged(object sender, EventArgs e)
            {

            }

    }
}
