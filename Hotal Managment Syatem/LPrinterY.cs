/*
 *  LPrinter - A simple line printer class in C#
 *  ============================================
 *  
 *  Written by Antonino Porcino, iz8bly@yahoo.it
 *
 *  26-sep-2008, public domain.
 *
 * 
 *  some useful print codes:
 *  ========================
 *    12 = FF (form feed)
 *    14 = enlarged on
 *    20 = enlarged off
 *    15 = compress on
 *    18 = compress off
 *    ESC + "E" = bold on
 *    ESC + "F" = bold off
 *    
 * 
 */


using System;
using System.Drawing.Printing;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Data.SqlClient;
using System.Data;

using System.Windows.Forms.Design;
using System.Linq;

namespace Hotal_Managment_Syatem
{

    class LPrinterY
    {
        public string rptDateFrom, rptDateTO;  //for recive dates

        public float tax_amt = 0, disc_amt = 0;
        float grand_total = 0;
        public string _getCounterNo = "";
        // Structure and API declarions:
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public class DOCINFOA
        {
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDocName;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pOutputFile;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDataType;
        }
        [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, out IntPtr hPrinter, IntPtr pd);

        [DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartDocPrinter(IntPtr hPrinter, Int32 level, [In, MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);

        [DllImport("winspool.Drv", EntryPoint = "EndDocPrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndDocPrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "EndPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "WritePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, Int32 dwCount, out Int32 dwWritten);

        /*=================================================*/

        private IntPtr HandlePrinter;
        PrinterSettings ps;
        PrintDocument pdoc = null;
        public int qty;
        public string date, wname, time, counterNo, datefrm, date_To, tableno, discountReason;
        public float billid, rate, GrandTot, discAmt, TotalAmt, Cgstamt, Sgstamt;
        public string item_nm, date1, date2, connection_str, amt, billDate, billtime, GTotal;
        public int kotID = 0;
        public bool tokenKot = false, grupwseflg = false, flgSales = false;
        // Database db = new Database();
        string hotelNam, tagline, address, mob, footer1, footer2, companyNm, grpName;
        DataGridView koGridview;
        double foodSum = 0, drinkSum = 0, totalAmt = 0, grandTotal = 0;
        float subtotal = 0;
        int hotelNmfont_size, taglineFont_size, addressFont_size, mobileFont_size, footer2Font_size, cmpnyNameFont_size, itemNameFontSize, tblNoFontSize, billNoFontSize, dateFontSize, totalFontSize, discFontSize, GrandFontSize;
        public bool insentiveFlag = true;
        public bool cancelKOTFlag = false;
        // static string ConString = @"Data Source=.\abms;Initial Catalog=RestrosoftDB;Integrated Security=True;User ID=sa;Password=abms@2014R";

        // Database db = new Database();

        //array for food
        string[] food_m_name = new string[100];
        string[] food_Qty = new string[100];
        string[] food_rt = new string[100];
        string[] food_amount = new string[100];
        //array for drink
        string[] drink_m_name = new string[100];
        string[] drink_Qty = new string[100];
        string[] drink_rt = new string[100];
        string[] drink_amount = new string[100];
        int counter = 0;


        public static string ConString = StringCipher.Decrypt(System.IO.File.ReadAllText("Config.txt"), "5");

        public SqlConnection cn = new SqlConnection(ConString);
        // Database db = new Database();
        public LPrinterY()
        {
            HandlePrinter = IntPtr.Zero;
            ps = new PrinterSettings();
        }
        public LPrinterY(string con_string)
        {
            HandlePrinter = IntPtr.Zero;
            ps = new PrinterSettings();
            // ConString = @"Data Source=.\abms;Initial Catalog=" + con_string + ";Integrated Security=True;User ID=sa;Password=abms@2014R";
            ConString = System.IO.File.ReadAllText("Config.txt");
        }
        public LPrinterY(string table_no, string date, float billid, string name, string time)
        {
            this.tableno = table_no;
            this.date = date;
            this.billid = billid;
            this.wname = name;
            this.time = time;
        }
        public LPrinterY(float g_total)
        {
            this.GrandTot = g_total;

        }
        public string PrinterName
        {
            get
            {
                return ps.PrinterName;
            }
            set
            {
                ps.PrinterName = value;
            }
        }

        public bool ChoosePrinter()
        {
            PrintDialog pd = new PrintDialog();
            pd.PrinterSettings = ps;
            if (pd.ShowDialog() == DialogResult.OK)
            {
                ps = pd.PrinterSettings;
                return true;
            }
            else return false;
        }

        public bool Open(string DocName)
        {
            // see if printer is already open
            if (HandlePrinter != IntPtr.Zero) return false;

            // opens the printer
            bool risp = OpenPrinter(ps.PrinterName, out HandlePrinter, IntPtr.Zero);
            if (risp == false) return false;

            // starts a print job
            DOCINFOA MyDocInfo = new DOCINFOA();
            MyDocInfo.pDocName = DocName;
            MyDocInfo.pOutputFile = null;
            MyDocInfo.pDataType = "RAW";

            if (StartDocPrinter(HandlePrinter, 1, MyDocInfo))
            {
                StartPagePrinter(HandlePrinter); //starts a page       
                return true;
            }
            else return false;
        }

        public bool Close()
        {
            if (HandlePrinter == IntPtr.Zero) return false;
            if (!EndPagePrinter(HandlePrinter)) return false;
            if (!EndDocPrinter(HandlePrinter)) return false;
            if (!ClosePrinter(HandlePrinter)) return false;
            HandlePrinter = IntPtr.Zero;
            return true;
        }

        public bool Print(string outputstring)
        {
            if (HandlePrinter == IntPtr.Zero) return false;

            IntPtr buf = Marshal.StringToCoTaskMemAnsi(outputstring);

            Int32 done = 0;
            bool ok = WritePrinter(HandlePrinter, buf, outputstring.Length, out done);

            Marshal.FreeCoTaskMem(buf);

            if (!ok) return false;
            else return true;
        }

        public bool drinkItemPrint = false; // added by sagar for seprate print drink bill on 24-07-2018

        public float getFoodItemCount = 0;
        public float getDrinkItemCount = 0;
        bool CombineAll = false;
        bool foodPrintDone=false;
        bool header = false;

        public void print()
        {

            //Get Printer name here
            cn.Open();
            SqlCommand cmd = new SqlCommand("select value from tbl_option where grp='Bill Printer'", cn);
            try
            {
                if (cmd.ExecuteScalar().ToString() == "")
                    PrinterName = "0";
                else
                    PrinterName = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {

            }
            cn.Close();

            PrintDialog pd = new PrintDialog();
            pdoc = new PrintDocument();

            PrinterSettings ps = new PrinterSettings();
            Font font = new Font("Courier New", 12);


            PaperSize psize = new PaperSize("Custom", 820, 5000000);//100,200
            //ps.DefaultPageSettings.PaperSize = psize;



            pd.Document = pdoc;

            pd.Document.DefaultPageSettings.PaperSize = psize;
            //pdoc.DefaultPageSettings.PaperSize.Height =320;
            pdoc.DefaultPageSettings.PaperSize.Height = 5000000;

            pdoc.DefaultPageSettings.PaperSize.Width = 520;

            //Assign Printer name here

            PrinterSettings printer = new PrinterSettings();
            pdoc.PrinterSettings.PrinterName = (PrinterName);


            pdoc.OriginAtMargins = false;

            Database db = new Database();
            //float getFoodItemCount = db.getDb_Value("SELECT count(1) FROM menu INNER JOIN sales_item ON menu.menu_id = sales_item.menu_id WHERE (sales_item.order_id = '" + this.billid + "') and (category<>'HARD DRINKS' or  category<>'Hard Drinks' )");
            //float getDrinkItemCount = db.getDb_Value("SELECT count(1) FROM menu INNER JOIN sales_item ON menu.menu_id = sales_item.menu_id WHERE (sales_item.order_id = '" + this.billid + "') and (category='HARD DRINKS' or  category='Hard Drinks' )");

            getFoodItemCount = db.getDb_Value(@" 
 SELECT count(1) FROM menu INNER JOIN sales_item ON menu.menu_id = sales_item.menu_id 
 inner join category cat on cat.cat_name=menu.category
  WHERE (sales_item.order_id = '" + this.billid + "') and (cat.BillPrinter=1 )");

            getDrinkItemCount = db.getDb_Value(@" SELECT count(1) FROM menu INNER JOIN sales_item ON menu.menu_id = sales_item.menu_id 
 inner join category cat on cat.cat_name=menu.category
  WHERE (sales_item.order_id = '" + this.billid + "') and (cat.BillPrinter=2 )");





            //FoodDrinkSum = 0;
            //drinkItemPrint = false;
            //printGrandTotal = false;
            //if (getFoodItemCount != 0)
            //{

                pdoc.PrintPage += new PrintPageEventHandler(this.PrintBillN);
               
                pdoc.Print();

               // bool CombineAll = false;
                if(CombineAll)
                {
                    foodPrintDone = true;
                     pdoc.PrintPage += new PrintPageEventHandler(this.PrintBillN);
                     pdoc.Print();
                }

            //}

            //drinkItemPrint = true;
            //printGrandTotal = true;
            //if (getDrinkItemCount != 0)
            //{
            //    pdoc.PrintPage += new PrintPageEventHandler(this.PrintBillN);  //test the new code 12-1-2014 by sagar
            //    pdoc.Print();
            //}

        }
        public String underLine = "----------------------------------";
        public float FoodDrinkSum = 0;
        public bool printGrandTotal = false;
        public string reciprtFontName = string.Empty;
        public string logoPath = string.Empty;
        public float serviceTaxAmt, vatAmt;
        public string custN;
        public string mobn;
        public string custgst;
        public string remark;

        void PrintBillN(object sender, PrintPageEventArgs e)
        {
            reciprtFontName = "Courier New";
            int amtAlign_X = 180;

            Database db = new Database();
            Graphics graphics = e.Graphics;
            Font font = new Font("Courier New ", 10);
            string fontType = "Courier New ";

            if (db.ChkDb_Value("select value from tbl_option where grp='" + "CustomerDetails" + "' and status='Yes'"))
            {

                custN = db.getDbstatus_Value("SELECT ISNULL((SELECT Custname FROM table_order  WHERE order_id = '" + this.billid + "'),0)");
                mobn = db.getDbstatus_Value("SELECT ISNULL((SELECT mob FROM table_order   WHERE order_id = '" + this.billid + "'),0)");
                custgst = db.getDbstatus_Value("SELECT ISNULL((SELECT Custgst FROM table_order   WHERE order_id = '" + this.billid + "'),0)");

                remark = db.getDbstatus_Value("SELECT ISNULL((SELECT Remark FROM table_order   WHERE order_id = '" + this.billid + "'),0)");


            }



            float fontHeight = font.GetHeight();
            int startX = 0;
            int startY = 30;//30
            int Offset = 0;
            int fontSize = 9;
            string billDate = "";
            string prefix = string.Empty;

            float tax_amt = 0, disc_amt = 0, grand_total = 0, tax, disc;
            bool flag_grandtotal = false;
            string date;

            cn.Open();
            string query = "select ddate from tbl_dayend_status";
            string str;
            SqlCommand cmd3 = new SqlCommand(query, cn);


            if (cmd3.ExecuteScalar().ToString() == "")
                str = "0";
            else
                str = cmd3.ExecuteScalar().ToString();

            cn.Close();

            cn.Open();

            string qury = "select * from tbl_receiptFormat";
            SqlCommand comnd = new SqlCommand(qury, cn);
            SqlDataReader reader123 = comnd.ExecuteReader();
           
            while (reader123.Read())
            {
                hotelNmfont_size = int.Parse(reader123["hotelNmFontSize"].ToString());
                taglineFont_size = int.Parse(reader123["taglineFontSize"].ToString());
                addressFont_size = int.Parse(reader123["addressFontSize"].ToString());
                mobileFont_size = int.Parse(reader123["mobileFontSize"].ToString());
                footer2Font_size = int.Parse(reader123["footer2FontSize"].ToString());
                cmpnyNameFont_size = int.Parse(reader123["companyNameFontSize"].ToString());
                itemNameFontSize = int.Parse(reader123["itemNmFontSize"].ToString());
                totalFontSize = int.Parse(reader123["totalFontSize"].ToString());
                GrandFontSize = int.Parse(reader123["grandfontsize"].ToString());
               prefix = reader123["prefix"].ToString();

            }
            cn.Close();
            var rightformat = new StringFormat() { Alignment = StringAlignment.Far };
            var leftformat = new StringFormat() { Alignment = StringAlignment.Near };
            var centerformat = new StringFormat() { Alignment = StringAlignment.Center };
            var rect = new RectangleF(0, 0, 265, 20);

            get_ReceiptFormt();

            if (!header)
            {
                if (db.ChkDb_Value("select value from tbl_option where grp='LogoPrint' and status='Yes'"))
                {
                //    logoPath = db.getDbstatus_Value("select value from tbl_option where grp='LogoPrint' and status='Yes'");
                //    int logoHight = int.Parse(db.getDbstatus_Value("select value from tbl_option where grp='logoHight' and status='Yes'"));
                //    using (Image logo = Image.FromFile(@logoPath))
                //    {
                //        graphics.DrawImage(logo, new Point(0, 0));
                //    }
                //    Offset = Offset + logoHight;

                }
                else
                {
                    rect = new RectangleF(startX, startY + Offset, 265, 30);
                    graphics.DrawString(hotelNam.Trim(), new Font(reciprtFontName, hotelNmfont_size, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, centerformat);

                    Offset = Offset + 25;

                }
                if (tagline != "")
                {
                    rect = new RectangleF(startX, startY + Offset, 265, 30);
                    graphics.DrawString(tagline.Trim(), new Font(reciprtFontName, taglineFont_size, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, centerformat);
                    Offset = Offset + 16;
                }
                rect = new RectangleF(startX, startY + Offset, 265, 30); //Address2
                graphics.DrawString(address.Trim(), new Font(reciprtFontName, addressFont_size, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, centerformat);
                Offset = Offset + 16;
                Address2 = Address2.Trim();
                if (Address2 != "")
                {
                    rect = new RectangleF(startX, startY + Offset, 265, 30);
                    graphics.DrawString(Address2.Trim(), new Font(reciprtFontName, addressFont_size, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, centerformat);
                    Offset = Offset + 16;
                }
                rect = new RectangleF(startX, startY + Offset, 265, 30);
                graphics.DrawString(mob.Trim(), new Font(reciprtFontName, addressFont_size, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, centerformat);
            }
            Offset = Offset + 16;
            graphics.DrawString("----------------------------------\r", new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 14;

            graphics.DrawString("Tbl No.:" + this.tableno + "  B.No:" + prefix + "" + this.billid + "  \r\n", new Font(reciprtFontName, 12, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString(" Date :" + str + "  " + DateTime.Now.ToString("HH:mm:ss") + "\r", new Font(reciprtFontName, 11, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 16;

            graphics.DrawString(underLine, new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 16;
            graphics.DrawString(" Item Name         Qty  Rate  Amt \r", new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 14;
            graphics.DrawString(underLine, new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

            int i = 1;
            float subtotal = 0, totalAmountOfFood = 0;
            string mname, qty, rate, amt;
            //array for food
            string[] food_m_name = new string[100];
            string[] food_Qty = new string[100];
            string[] food_rt = new string[100];
            string[] food_amount = new string[100];
            //array for drink
            string[] drink_m_name = new string[100];
            string[] drink_Qty = new string[100];
            string[] drink_rt = new string[100];
            string[] drink_amount = new string[100];
            int counter = 0;

            string Grosstotal = "";
            int foodCounter = 0, drinkCounter = 0;


            string qur = @" SELECT menu.printName as nm, min(sales_item.sales_id),sum(sales_item.qty) as qty, sales_item.rate as rt, 
              sum(sales_item.total_amount) as amt,menu.category,cat.BillPrinter as billPrinter
             FROM menu INNER JOIN sales_item ON menu.menu_id = sales_item.menu_id 
             inner join category cat on menu.category=cat.cat_name
             WHERE sales_item.order_id = '" + this.billid + "' group by menu.printName,sales_item.rate,menu.category,cat.BillPrinter  ORDER BY min(sales_item.sales_id)";


            totalAmountOfFood = db.getDb_Value("SELECT SUM(sales_item.total_amount) AS Expr1 FROM  menu INNER JOIN  sales_item ON menu.menu_id = sales_item.menu_id INNER JOIN  category ON menu.category = category.cat_name WHERE        (sales_item.order_id = '" + this.billid + "') AND (category.BillPrinter =1)");


            ///*****
            string Billvalue = "";
            Billvalue = db.getDbstatus_Value("select value from tbl_option where grp='Print formation' and status='Yes'");

            #region simpleBillFormat

            // ************************ simple bill format code starthere *****************************
            if (Billvalue == "Simple")
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(qur, cn);
                SqlDataReader rd = cmd.ExecuteReader();
                string sr = i.ToString();
                string tem_qty = "";
                while (rd.Read() == true)
                {
                    mname = rd["nm"].ToString();
                    qty = rd["qty"].ToString();
                    rate = rd["rt"].ToString();
                    amt = rd["amt"].ToString();
                    tem_qty = qty;
                    subtotal += float.Parse(rd["amt"].ToString());
                    Offset = Offset + 20;
                    graphics.DrawString(" " + MenuFormation(mname, "left", 16) + " " + MenuFormation(tem_qty, "left", 3) + " " + MenuFormation(rate, "left", 4) + " " + MenuFormation(amt, "left", 7) + "\r\n", new Font(reciprtFontName, itemNameFontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                    i++;

                }
                Offset = Offset + 20;
                Grosstotal = subtotal.ToString();
                Offset = Offset + 20;
                graphics.DrawString(underLine, new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 15;
                graphics.DrawString("                Total: " + Grosstotal + ".00", new Font(reciprtFontName, 14, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
                cn.Close();

            }  
            // ************************ simple bill format code end here *****************************
            #endregion
             
            else

            // if (Billvalue == "Composite")
                if (!CombineAll)
                 {
                 
                 //food nad drink bill seprate code 


                 cn.Open();
                 SqlCommand cmd = new SqlCommand(qur, cn);
                 SqlDataReader rd = cmd.ExecuteReader();
                 string sr = i.ToString();
                 string tem_qty = "";
                 string billPrinter = string.Empty;
                 string category = string.Empty;

                 while (rd.Read() == true)
                 {
                     mname = rd["nm"].ToString().Trim();
                     qty = rd["qty"].ToString();
                     rate = rd["rt"].ToString();
                     amt = rd["amt"].ToString();

                     category = rd["category"].ToString();
                     billPrinter = rd["billPrinter"].ToString();

                    
                     if (billPrinter == "1")  
                     {
                         food_m_name[foodCounter] = rd["nm"].ToString();
                         food_Qty[foodCounter] = rd["qty"].ToString();
                         food_rt[foodCounter] = rd["rt"].ToString();
                         food_amount[foodCounter] = rd["amt"].ToString();
                         foodCounter++;
                     }
                     else
                     {
                         drink_m_name[drinkCounter] = rd["nm"].ToString();
                         drink_Qty[drinkCounter] = rd["qty"].ToString();
                         drink_rt[drinkCounter] = rd["rt"].ToString();
                         drink_amount[drinkCounter] = rd["amt"].ToString();
                         drinkCounter++;
                     }

                     i++;

                     counter++;
                 }

                 cn.Close();

                 if (!drinkItemPrint && getFoodItemCount != 0)
                 {
                     for (int k = 0; k < foodCounter; k++)
                     {

                         mname = food_m_name[k];
                         qty = food_Qty[k];
                         rate = food_rt[k];
                         amt = food_amount[k];
                         if (mname != null)
                         {
                             foodSum += double.Parse(amt.ToString());
                             Offset = Offset + 20;
                             rect = new RectangleF(startX, startY + Offset, 160, 30);
                             graphics.DrawString(mname, new Font(reciprtFontName, itemNameFontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, leftformat);
                             graphics.DrawString(" " + MenuFormation(" ", "left", 19) + "" + MenuFormation(qty, "left", 3) + " " + MenuFormation(rate, "left", 5) + " " + MenuFormation(amt, "left", 7) + "\r\n", new Font(reciprtFontName, itemNameFontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                             if (mname.Trim().Length > 19)
                             Offset = Offset + 12;
                         }
                     }
                     Offset = Offset + 30;
                     graphics.DrawString(underLine, new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                     Offset = Offset + 15;
                     Grosstotal = foodSum + ".00" + " \r\n";

                     graphics.DrawString(MenuFormation("Total Amt :", "right", 30), new Font(fontType, totalFontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                     graphics.DrawString(" " + Grosstotal, new Font(fontType, totalFontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), amtAlign_X, startY + Offset);
                     Offset = Offset + 15;
                     graphics.DrawString(underLine, new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                     Offset = Offset + 20;
                 }


             }
                 //old else end start here for composite bill print 



            if (!CombineAll && !drinkItemPrint && getFoodItemCount != 0)
            {
                string gstAmt = "";
                string qry = "select * from tbl_option where grp='" + "Tax" + "' and status='Yes'";
                string value = "", value_1 = "", getDisc = "";
                float getDisAmt = 0;
                float getDisvalue = 0;

                getDisvalue = db.getDb_Value("select  ISNULL(value,0) as value from tbl_option where grp='Discount'");
                getDisAmt = db.getDb_Value("select discAmt from table_order where order_id='" + this.billid + "'");

                if (getDisAmt != 0 || getDisvalue != 0)
                {
                    flag_grandtotal = true;

                    if (getDisvalue != 0)
                    {
                        string discValue = "Dis " + getDisvalue + "% :";//7
                        graphics.DrawString(MenuFormation(discValue, "right", 20), new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                        getDisAmt = (float.Parse(foodSum.ToString()) * getDisvalue / 100);

                    }
                    else
                        graphics.DrawString(MenuFormation("Discount:", "right", 20), new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);


                    graphics.DrawString(" " + getDisAmt.ToString() + ".00", new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), amtAlign_X, startY + Offset);

                    Offset = Offset + 20;
                    grand_total = float.Parse(foodSum.ToString()) - getDisAmt;
                    FoodDrinkSum += grand_total;
                    foodSum = grand_total;
                }


                float foodDisVal = db.getDb_Value("select isnull(foodDiscValue,0)    from table_order where order_id='" + this.billid + "'");
                float foodDisAmt = db.getDb_Value("select isnull(foodDiscAmt,0)    from table_order where order_id='" + this.billid + "'");

                int x_val = int.Parse(db.getDbstatus_Value("select status from tbl_option where grp='dislineFormat'"));
                int x_val_1 = int.Parse(db.getDbstatus_Value("select value from tbl_option where grp='dislineFormat'"));
                int x_val_2 = int.Parse(db.getDbstatus_Value("select process_type from tbl_option where grp='dislineFormat'"));

                float getFoodServieTax = 0;
                getFoodServieTax = db.getDb_Value("select serviceTaxFoodVal from table_order where order_id='" + this.billid + "'");


                string reciprtFontName_1 = "Arial Narrow";


                if (foodDisVal != 0 && foodDisAmt != 0)
                {
                    flag_grandtotal = true;


                    rect = new RectangleF(x_val, startY + Offset, 100, 30);
                    graphics.DrawString("Food Dis  :", new Font(reciprtFontName_1, fontSize + 1, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, rightformat);
                    rect = new RectangleF(x_val_1, startY + Offset, 60, 30);
                    graphics.DrawString(replcePoint(foodDisVal.ToString()) + " %", new Font(reciprtFontName_1, fontSize + 1, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, rightformat);

                    rect = new RectangleF(x_val_2, startY + Offset, 50, 30);
                    graphics.DrawString(Math.Round(foodDisAmt).ToString() + ".00", new Font(reciprtFontName_1, fontSize + 1, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, rightformat);
                    Offset = Offset + 20;
                    grand_total = float.Parse(foodSum.ToString()) - foodDisAmt;
                    FoodDrinkSum += grand_total;
                    foodSum = grand_total;
                }

                //Beverage
                float BeverageDisVal = db.getDb_Value("select isnull(beveragesDiscValue,0)    from table_order where order_id='" + this.billid + "'");
                float BeverageDisAmt = db.getDb_Value("select isnull(beveragesDiscAmt,0)   from table_order where order_id='" + this.billid + "'");


                if (BeverageDisVal != 0 && BeverageDisAmt != 0)
                {
                    flag_grandtotal = true;


                    if (BeverageDisVal != 0)
                    {
                        rect = new RectangleF(x_val, startY + Offset, 100, 30);
                        graphics.DrawString("Bev. Dis  :", new Font(reciprtFontName_1, fontSize + 1, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, rightformat);
                        rect = new RectangleF(x_val_1, startY + Offset, 60, 30);
                        graphics.DrawString(replcePoint(BeverageDisVal.ToString()) + " %", new Font(reciprtFontName_1, fontSize + 1, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, rightformat);
                        rect = new RectangleF(x_val_2, startY + Offset, 50, 30);
                        graphics.DrawString(Math.Round(BeverageDisAmt).ToString() + ".00", new Font(reciprtFontName_1, fontSize + 1, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, rightformat);
                        Offset = Offset + 20;
                        grand_total = float.Parse(foodSum.ToString()) - BeverageDisAmt;
                        FoodDrinkSum += grand_total;
                        foodSum = grand_total;
                    }
                }
                cn.Open();
                SqlCommand cmd1 = new SqlCommand(qry, cn);
                SqlDataReader rd1 = cmd1.ExecuteReader();
                if (rd1.Read())
                {
                    flag_grandtotal = true;
                    value = rd1["value"].ToString();

                    tax_amt = (float.Parse(value) / 2 * float.Parse(foodSum.ToString())) / 100;
                    tax = subtotal + tax_amt;
                    getDisc = System.Math.Round(disc_amt, 2).ToString(); 

                    gstAmt = System.Math.Round(tax_amt, 2).ToString();

                    serviceTaxAmt = float.Parse(foodSum.ToString()) * getFoodServieTax / 100;

                    rect = new RectangleF(x_val, startY + Offset, 100, 30);
                    graphics.DrawString(" CGST  : ", new Font(reciprtFontName_1, fontSize + 1, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, rightformat);
                    rect = new RectangleF(x_val_1, startY + Offset, 60, 30);
                    graphics.DrawString((float.Parse(value) / 2) + " %", new Font(reciprtFontName_1, fontSize + 1, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, rightformat);
                    rect = new RectangleF(x_val_2, startY + Offset, 50, 30);
                    graphics.DrawString(gstAmt.ToString(), new Font(reciprtFontName_1, fontSize + 1, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, rightformat);

                    Offset = Offset + 20;

                    rect = new RectangleF(x_val, startY + Offset, 100, 30);
                    graphics.DrawString(" SGST  : ", new Font(reciprtFontName_1, fontSize + 1, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, rightformat);
                    rect = new RectangleF(x_val_1, startY + Offset, 60, 30);
                    graphics.DrawString((float.Parse(value) / 2) + " %", new Font(reciprtFontName_1, fontSize + 1, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, rightformat);
                    rect = new RectangleF(x_val_2, startY + Offset, 50, 30);
                    graphics.DrawString(gstAmt.ToString(), new Font(reciprtFontName_1, fontSize + 1, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, rightformat);

                    Offset = Offset + 20;

                    if (getFoodServieTax != 0 && serviceTaxAmt != 0)
                    {
                        rect = new RectangleF(x_val, startY + Offset, 100, 30);
                        graphics.DrawString(" S.C  : ", new Font(reciprtFontName_1, fontSize + 1, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, rightformat);
                        rect = new RectangleF(x_val_1, startY + Offset, 60, 30);
                        graphics.DrawString(getFoodServieTax.ToString() + ".0 %", new Font(reciprtFontName_1, fontSize + 1, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, rightformat);
                        rect = new RectangleF(x_val_2, startY + Offset, 50, 30);
                        graphics.DrawString(serviceTaxAmt.ToString(), new Font(reciprtFontName_1, fontSize + 1, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, rightformat);
                        Offset = Offset + 20;
                    }

                }
                cn.Close();
                FoodDrinkSum += float.Parse(foodSum.ToString());

                if (flag_grandtotal)
                {
                    grand_total = float.Parse(foodSum.ToString()) + tax_amt + tax_amt - disc_amt + serviceTaxAmt;

                    //FoodDrinkSum += grand_total;
                    FoodDrinkSum = grand_total;

                    graphics.DrawString(underLine, new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 20;
                    graphics.DrawString("  Grand Total", new Font(reciprtFontName, GrandFontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                    graphics.DrawString(System.Math.Round(grand_total).ToString() + ".00", new Font(reciprtFontName, GrandFontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), amtAlign_X, startY + Offset);

                   
               
                    Offset = Offset + 20;
                }



                graphics.DrawString(underLine, new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;

                if (db.ChkDb_Value("select value  from tbl_option where grp='Print formation' and value='CombineAll'"))
                {
                    CombineAll=true;
                  
                }
                if (db.ChkDb_Value("select status  from tbl_option where grp='SecondHeader' and status='No'"))
                {
                    header = true;
                }
            }

                 double drksum = 0;
                 //if (drinkItemPrint && printGrandTotal && drinkCounter != 0)
                 //{
                 if (drinkCounter != 0 && Billvalue == "Composite")
                 {

                     //    // for loop for drink  
                     for (int j = 0; j < drinkCounter; j++)
                     {

                         mname = drink_m_name[j];
                         qty = drink_Qty[j];
                         rate = drink_rt[j];
                         amt = drink_amount[j];
                         if (mname != null)
                         {
                             // drinkSum += double.Parse(amt.ToString());
                             drksum += double.Parse(amt.ToString());


                             Offset = Offset + 20;
                             //graphics.DrawString(" " + MenuFormation(mname, "left", 16) + "  " + MenuFormation(qty, "left", 3) + " " + MenuFormation(rate, "left", 4) + " " + MenuFormation(amt, "left", 7) + "\r\n", new Font(reciprtFontName, itemNameFontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                             rect = new RectangleF(startX, startY + Offset, 160, 30);
                             graphics.DrawString(mname, new Font(reciprtFontName, itemNameFontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, leftformat);
                             graphics.DrawString(" " + MenuFormation(" ", "left", 19) + " " + MenuFormation(qty, "left", 3) + " " + MenuFormation(rate, "left", 5) + " " + MenuFormation(amt, "left", 7) + "\r\n", new Font(reciprtFontName, itemNameFontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                             if (mname.Trim().Length > 19)
                                 Offset = Offset + 12;


                         }
                     }

                     //if (printGrandTotal && drinkCounter != 0) // if drink is not in bill
                     //{
                     Offset = Offset + 40;
                     graphics.DrawString(underLine, new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                     Offset = Offset + 20;
                     graphics.DrawString("     Sub Total ", new Font(reciprtFontName, 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                     graphics.DrawString(" " + drksum.ToString(), new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), amtAlign_X, startY + Offset);
                     Offset = Offset + 20;
                     graphics.DrawString(underLine, new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                     Offset = Offset + 20;


                     //********************** code pending 

                     //liquor
                     float liquorDisVal = db.getDb_Value("select isnull(liquorDiscValue,0)   from table_order where order_id='" + this.billid + "'");
                     float liquorDisAmt = db.getDb_Value("select isnull(liquorDiscAmt,0)   from table_order where order_id='" + this.billid + "'");


                     if (liquorDisVal != 0 && liquorDisAmt != 0)
                     {
                         flag_grandtotal = true;



                         graphics.DrawString(MenuFormation("    LiquorDis: " + replcePoint(liquorDisVal.ToString()) + " %", "right", 20), new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                         graphics.DrawString(" " + liquorDisAmt.ToString(), new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), amtAlign_X, startY + Offset);

                         Offset = Offset + 20;
                         // grand_total = float.Parse(foodSum.ToString()) - foodDisAmt;
                         drksum -= liquorDisAmt;
                     }


                     float serviceTaxLiquorVal = 0;
                     serviceTaxLiquorVal = db.getDb_Value("select serviceTaxLiquorVal from table_order where order_id='" + this.billid + "'");

                     serviceTaxAmt = float.Parse(drksum.ToString()) * serviceTaxLiquorVal / 100;

                     if (serviceTaxLiquorVal != 0 && serviceTaxAmt != 0)
                     {


                         graphics.DrawString("           S.C " + serviceTaxLiquorVal.ToString() + " % ", new Font(reciprtFontName, 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                         graphics.DrawString(" " + serviceTaxAmt.ToString(), new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), amtAlign_X, startY + Offset);
                     }
                     Offset = Offset + 10;
                     //vat amt
                       if (db.ChkDb_Value("select * from tbl_option where grp='" + "Tax" + "' and status='Yes'"))
                    {
                     
                     vatAmt = float.Parse(drksum.ToString()) * 5 / 100;

                     graphics.DrawString("           VAT 5 % ", new Font(reciprtFontName, 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                     graphics.DrawString(" " + vatAmt.ToString(), new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), amtAlign_X, startY + Offset);
                     Offset = Offset + 20;
                       }



                     graphics.DrawString(underLine, new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                     Offset = Offset + 20;

                     graphics.DrawString("       Total  :", new Font(reciprtFontName, GrandFontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                     graphics.DrawString(" " + System.Math.Round(drksum + vatAmt + serviceTaxAmt).ToString() + ".00", new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), amtAlign_X, startY + Offset);

                     Offset = Offset + 20;
                     graphics.DrawString(underLine, new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                     Offset = Offset + 20;

                     string str_foodDrinkSum = FoodDrinkSum.ToString();
                     graphics.DrawString("  Grand Total", new Font(reciprtFontName, GrandFontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                     //graphics.DrawString(System.Math.Round(drksum + FoodDrinkSum + vatAmt + serviceTaxAmt).ToString() + ".00", new Font(reciprtFontName, GrandFontSize + 2, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), amtAlign_X, startY + Offset);
                     graphics.DrawString(GTotal + ".00", new Font(reciprtFontName, GrandFontSize + 2, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), amtAlign_X, startY + Offset);

                     
                     Offset = Offset + 20;

                     graphics.DrawString(underLine, new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);


                 }

                 if (CombineAll  && foodPrintDone)
                 {

                     cn.Open();
                     SqlCommand cmd = new SqlCommand(qur, cn);
                     SqlDataReader rd = cmd.ExecuteReader();
                     string sr = i.ToString();
                     string tem_qty = "";
                     string billPrinter = string.Empty;
                     string category = string.Empty;

                     while (rd.Read() == true)
                     {
                         mname = rd["nm"].ToString().Trim();
                         qty = rd["qty"].ToString();
                         rate = rd["rt"].ToString();
                         amt = rd["amt"].ToString();

                         category = rd["category"].ToString();
                         billPrinter = rd["billPrinter"].ToString();


                         if (billPrinter == "2")
                         {
                             drink_m_name[drinkCounter] = rd["nm"].ToString();
                             drink_Qty[drinkCounter] = rd["qty"].ToString();
                             drink_rt[drinkCounter] = rd["rt"].ToString();
                             drink_amount[drinkCounter] = rd["amt"].ToString();
                             drinkCounter++;
                         }
                        
                         i++;

                         counter++;
                     }
                     cn.Close();


                     //if (drinkItemPrint && printGrandTotal && drinkCounter != 0)
                     //{
                     if (drinkCounter != 0 )
                     {

                         //    // for loop for drink  
                         for (int j = 0; j < drinkCounter; j++)
                         {

                             mname = drink_m_name[j];
                             qty = drink_Qty[j];
                             rate = drink_rt[j];
                             amt = drink_amount[j];
                             if (mname != null)
                             {
                                 // drinkSum += double.Parse(amt.ToString());
                                 drksum += double.Parse(amt.ToString());


                                 Offset = Offset + 20;
                                 //graphics.DrawString(" " + MenuFormation(mname, "left", 16) + "  " + MenuFormation(qty, "left", 3) + " " + MenuFormation(rate, "left", 4) + " " + MenuFormation(amt, "left", 7) + "\r\n", new Font(reciprtFontName, itemNameFontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                                 rect = new RectangleF(startX, startY + Offset, 160, 30);
                                 graphics.DrawString(mname, new Font(reciprtFontName, itemNameFontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, leftformat);
                                 graphics.DrawString(" " + MenuFormation(" ", "left", 19) + " " + MenuFormation(qty, "left", 3) + " " + MenuFormation(rate, "left", 5) + " " + MenuFormation(amt, "left", 7) + "\r\n", new Font(reciprtFontName, itemNameFontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                                 if (mname.Trim().Length > 19)
                                     Offset = Offset + 12;


                             }
                         }

                         //if (printGrandTotal && drinkCounter != 0) // if drink is not in bill
                         //{
                         Offset = Offset + 40;
                         graphics.DrawString(underLine, new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                         Offset = Offset + 20;
                         graphics.DrawString("     Sub Total ", new Font(reciprtFontName, 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                         graphics.DrawString(" " + drksum.ToString(), new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), amtAlign_X, startY + Offset);
                         Offset = Offset + 20;
                         graphics.DrawString(underLine, new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                         Offset = Offset + 20;


                         //********************** code pending 

                         //liquor
                         float liquorDisVal = db.getDb_Value("select isnull(liquorDiscValue,0)   from table_order where order_id='" + this.billid + "'");
                         float liquorDisAmt = db.getDb_Value("select isnull(liquorDiscAmt,0)   from table_order where order_id='" + this.billid + "'");


                         if (liquorDisVal != 0 && liquorDisAmt != 0)
                         {
                             flag_grandtotal = true;



                             graphics.DrawString(MenuFormation("    LiquorDis: " + replcePoint(liquorDisVal.ToString()) + " %", "right", 20), new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                             graphics.DrawString(" " + liquorDisAmt.ToString(), new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), amtAlign_X, startY + Offset);

                             Offset = Offset + 20;
                             // grand_total = float.Parse(foodSum.ToString()) - foodDisAmt;
                             drksum -= liquorDisAmt;
                         }


                         float serviceTaxLiquorVal = 0;
                         serviceTaxLiquorVal = db.getDb_Value("select serviceTaxLiquorVal from table_order where order_id='" + this.billid + "'");

                         serviceTaxAmt = float.Parse(drksum.ToString()) * serviceTaxLiquorVal / 100;

                         if (serviceTaxLiquorVal != 0 && serviceTaxAmt != 0)
                         {


                             graphics.DrawString("           S.C " + serviceTaxLiquorVal.ToString() + " % ", new Font(reciprtFontName, 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                             graphics.DrawString(" " + serviceTaxAmt.ToString(), new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), amtAlign_X, startY + Offset);
                         }
                         Offset = Offset + 20;
                         //vat amt

                         vatAmt = float.Parse(drksum.ToString()) * 5 / 100;

                         graphics.DrawString("           VAT 5 % ", new Font(reciprtFontName, 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                         graphics.DrawString(" " + vatAmt.ToString(), new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), amtAlign_X, startY + Offset);
                         Offset = Offset + 20;



                         graphics.DrawString(underLine, new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                         Offset = Offset + 20;

                         graphics.DrawString("       Total  :", new Font(reciprtFontName, GrandFontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                         graphics.DrawString(" " + System.Math.Round(drksum + vatAmt + serviceTaxAmt).ToString() + ".00", new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), amtAlign_X, startY + Offset);

                         Offset = Offset + 20;
                         graphics.DrawString(underLine, new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                         Offset = Offset + 20;

                         string str_foodDrinkSum = FoodDrinkSum.ToString();
                         graphics.DrawString("  Grand Total", new Font(reciprtFontName, GrandFontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                        // graphics.DrawString(System.Math.Round(drksum + FoodDrinkSum + vatAmt + serviceTaxAmt).ToString() + ".00", new Font(reciprtFontName, GrandFontSize + 2, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), amtAlign_X, startY + Offset);
                         graphics.DrawString(GTotal + ".00", new Font(reciprtFontName, GrandFontSize + 2, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), amtAlign_X, startY + Offset);

                         Offset = Offset + 20;

                         graphics.DrawString(underLine, new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                     }

                     
                



                 }
             //****************** For Parcel display customer name,address and contact No ****************************

                 if (ChkDb_Value("SELECT        table_order.t_id, Custmer.name, Custmer.addr, tbl_parcel_order.order_id FROM            tbl_parcel_order INNER JOIN     Custmer ON tbl_parcel_order.Customer_id = Custmer.Customer_id INNER JOIN    table_order ON tbl_parcel_order.order_id = table_order.order_id WHERE        (table_order.t_id = '" + this.tableno + "') and  tbl_parcel_order.order_id='" + this.billid + "' "))
                 {
                     cn.Close();
                     string query1 = "SELECT        table_order.t_id, Custmer.name, Custmer.addr,Custmer.phone, tbl_parcel_order.order_id FROM            tbl_parcel_order INNER JOIN     Custmer ON tbl_parcel_order.Customer_id = Custmer.Customer_id INNER JOIN    table_order ON tbl_parcel_order.order_id = table_order.order_id WHERE        (table_order.t_id = '" + this.tableno + "') and tbl_parcel_order.order_id='" + this.billid + "'";
                     string Name = "", Address = "", contactNo = "";
                     cn.Open();
                     SqlCommand command = new SqlCommand(query1, cn);
                     SqlDataReader reader = command.ExecuteReader();
                     if (reader.Read())
                     {
                         //  flag_grandtotal = true;
                         Name = reader["name"].ToString();
                         Address = reader["addr"].ToString();
                         contactNo = reader["phone"].ToString();

                         //graphics.DrawString("\n ", new Font(reciprtFontName, 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                         //Offset = Offset + 20;
                         graphics.DrawString("\n ", new Font(reciprtFontName, 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                         Offset = Offset + 20;
                         graphics.DrawString("----------------------------------\r", new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                         Offset = Offset + 20;
                         graphics.DrawString("Parcel Order Details ", new Font(reciprtFontName, 14, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                         Offset = Offset + 25;
                         graphics.DrawString("Name :" + Name, new Font(reciprtFontName, 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                         Offset = Offset + 20;
                         graphics.DrawString("Address :" + Address, new Font(reciprtFontName, 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                         Offset = Offset + 20;
                         graphics.DrawString("Mobile :" + contactNo, new Font(reciprtFontName, 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                         Offset = Offset + 20;

                     }
                     cn.Close();


                 }
                 cn.Close(); // close 
                 Offset = Offset + 7;
                 graphics.DrawString(footer1, new Font("Courier New", 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                 Offset = Offset + 20;
                 graphics.DrawString(footer2, new Font("Courier New", footer2Font_size, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                 Offset = Offset + 20;
                 graphics.DrawString(companyNm, new Font(reciprtFontName, cmpnyNameFont_size, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                 Offset = Offset + 20;
                 //Yogesh For Customer Print on Bill ......./////
                 if (db.ChkDb_Value(" select * from tbl_option where  grp='CustomerDetails'and status='Yes'and value='Yes'"))
                 {

                     graphics.DrawString("Customer Details :   \r", new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                     Offset = Offset + 16;
                     graphics.DrawString(" Customer Name :" + custN + "  \r", new Font(reciprtFontName, 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                     Offset = Offset + 16;
                     graphics.DrawString(" Mobile No : " + mobn + "   \r", new Font(reciprtFontName, 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                     Offset = Offset + 16;
                     graphics.DrawString(" GSTIN : " + custgst + "   \r", new Font(reciprtFontName, 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                     Offset = Offset + 16;
                     //tejashri 13-04-2019
                     if (remark != "0")
                     {
                         rect = new RectangleF(30, startY + Offset, 160, 30);
                         graphics.DrawString(remark, new Font(reciprtFontName, itemNameFontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, leftformat);
                     }
                     if (remark.Trim().Length > 25)
                         Offset = Offset + 12;

                     //graphics.DrawString(" " + remark + "   \r", new Font(reciprtFontName, 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 16;

                    


                 }
                 //tejashri 13-04-2019 for discount reason
                 if (db.ChkDb_Value(" select * from tbl_option where  grp='DiscountReason'and status='Yes'"))
                 {
                     graphics.DrawString(underLine, new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                     Offset = Offset + 16;

                     rect = new RectangleF(30, startY + Offset, 160, 30);
                     graphics.DrawString(discountReason, new Font(reciprtFontName, itemNameFontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, centerformat);

                     //graphics.DrawString(discountReason, new Font(reciprtFontName, GrandFontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), amtAlign_X, startY + Offset);
                     Offset = Offset + 16;
                     graphics.DrawString(underLine, new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                     Offset = Offset + 20;
                 }

                
                 Offset = Offset + 10;
                 graphics.DrawString(db.getDbstatus_Value("select value from tbl_option where grp='OnBillFooter'"), new Font("Courier New", footer2Font_size, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                 Offset = Offset + 20;
             

                
            


            //////////////////////////////////////////////









        }


        //public bool ChkDb_Value(String query)
        //{

        //    cn.Open();
        //    SqlCommand cmd = new SqlCommand(query, cn);
        //    SqlDataReader rd = cmd.ExecuteReader();

        //    if (rd.Read() == true)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }


        //}


        void temp_pdoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            reciprtFontName = "Courier New";
            int amtAlign_X = 180;

            Database db = new Database();
            Graphics graphics = e.Graphics;
            Font font = new Font("Courier New ", 10);
            string fontType = "Courier New ";


            //if (db.ChkDb_Value("select value from tbl_option where grp='" + "CustomerDetails" + "' and status='Yes'"))
            //{

            //    custN = db.getDbstatus_Value("SELECT ISNULL((SELECT Custname FROM table_order  WHERE order_id = '" + this.billid + "'),0)");
            //    mobn = db.getDbstatus_Value("SELECT ISNULL((SELECT mob FROM table_order   WHERE order_id = '" + this.billid + "'),0)");
            //    custgst = db.getDbstatus_Value("SELECT ISNULL((SELECT Custgst FROM table_order   WHERE order_id = '" + this.billid + "'),0)");

            //    remark = db.getDbstatus_Value("SELECT ISNULL((SELECT Remark FROM table_order   WHERE order_id = '" + this.billid + "'),0)");


            //}




            float fontHeight = font.GetHeight();
            int startX = 0;
            int startY = 30;//30
            int Offset = 0;
            int fontSize = 9;
            string billDate = "";
         
            float tax_amt = 0, disc_amt = 0, grand_total = 0, tax, disc;
            bool flag_grandtotal = false;
            string date;

            cn.Open();
            string query = "select ddate from tbl_dayend_status";
            string str;
            SqlCommand cmd3 = new SqlCommand(query, cn);


            if (cmd3.ExecuteScalar().ToString() == "")
                str = "0";
            else
                str = cmd3.ExecuteScalar().ToString();

            cn.Close();


            // get font size from database

            cn.Open();
            string qury = "select * from tbl_receiptFormat";
            SqlCommand comnd = new SqlCommand(qury, cn);
            SqlDataReader reader123 = comnd.ExecuteReader();
            while (reader123.Read())
            {
                hotelNmfont_size = int.Parse(reader123["hotelNmFontSize"].ToString());
                taglineFont_size = int.Parse(reader123["taglineFontSize"].ToString());
                addressFont_size = int.Parse(reader123["addressFontSize"].ToString());
                mobileFont_size = int.Parse(reader123["mobileFontSize"].ToString());
                footer2Font_size = int.Parse(reader123["footer2FontSize"].ToString());
                cmpnyNameFont_size = int.Parse(reader123["companyNameFontSize"].ToString());
                itemNameFontSize = int.Parse(reader123["itemNmFontSize"].ToString());
                totalFontSize = int.Parse(reader123["totalFontSize"].ToString());
                GrandFontSize = int.Parse(reader123["grandfontsize"].ToString());
                //
            }

            //if (comnd.ExecuteScalar().ToString() == "")
            //    font_size = 0;
            //else
            //    font_size =int.Parse(comnd.ExecuteScalar().ToString());

            cn.Close();

            //get foods item for particuler bill    //"HARD DRINKS" && category!="Hard Drinks"

            var rightformat = new StringFormat() { Alignment = StringAlignment.Far };
            var leftformat = new StringFormat() { Alignment = StringAlignment.Near };
            var centerformat = new StringFormat() { Alignment = StringAlignment.Center };

            var rect = new RectangleF(0, 0, 265, 20);
            //graphics.DrawString("123123", font, new SolidBrush(Color.Black), rect, centerformat);    


            //    bill header section start here 

            get_ReceiptFormt();

            if (db.ChkDb_Value("select value from tbl_option where grp='LogoPrint' and status='Yes'"))
            {
                logoPath = db.getDbstatus_Value("select value from tbl_option where grp='LogoPrint' and status='Yes'");
                int logoHight = int.Parse(db.getDbstatus_Value("select value from tbl_option where grp='logoHight' and status='Yes'"));
                using (Image logo = Image.FromFile(@logoPath))
                {
                    graphics.DrawImage(logo, new Point(0, 0));
                }
                Offset = Offset + logoHight;
            }
            else
            {
                rect = new RectangleF(startX, startY + Offset, 265, 30);

                //graphics.DrawString(hotelNam, new Font("Courier New", hotelNmfont_size, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                graphics.DrawString(hotelNam.Trim(), new Font(reciprtFontName, hotelNmfont_size, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, centerformat);

                Offset = Offset + 25;
            }

            if (tagline != "")
            {
                rect = new RectangleF(startX, startY + Offset, 265, 30);
                //graphics.DrawString(tagline, new Font(reciprtFontName, taglineFont_size, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                graphics.DrawString(tagline.Trim(), new Font(reciprtFontName, taglineFont_size, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, centerformat);

                Offset = Offset + 16;
            }
            rect = new RectangleF(startX, startY + Offset, 265, 30); //Address2
            //  graphics.DrawString(address, new Font(reciprtFontName, addressFont_size, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            graphics.DrawString(address.Trim(), new Font(reciprtFontName, addressFont_size, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, centerformat);
            Offset = Offset + 16;
            Address2 = Address2.Trim();
            if (Address2 != "")
            {
                rect = new RectangleF(startX, startY + Offset, 265, 30);
                graphics.DrawString(Address2.Trim(), new Font(reciprtFontName, addressFont_size, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, centerformat);
                Offset = Offset + 16;
            }
            // graphics.DrawString(mob.Trim(), new Font(reciprtFontName, mobileFont_size, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset,centerformat);
            rect = new RectangleF(startX, startY + Offset, 265, 30);
            graphics.DrawString(mob.Trim(), new Font(reciprtFontName, addressFont_size, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, centerformat);

            Offset = Offset + 16;
            graphics.DrawString("----------------------------------\r", new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 14;

            graphics.DrawString("Tbl No.:" + this.tableno + "  B.No:" + this.billid + "  \r\n", new Font(reciprtFontName, 12, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString(" Date :" + str + "  " + DateTime.Now.ToString("HH:mm:ss") + "\r", new Font(reciprtFontName, 11, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 16;




            graphics.DrawString(underLine, new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 16;
            //  graphics.DrawString(" Item Name      Qty  Rate  Amount \r", new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            graphics.DrawString(" Item Name         Qty  Rate  Amt \r", new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

            Offset = Offset + 14;
            graphics.DrawString(underLine, new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);



            int i = 1;
            float subtotal = 0, totalAmountOfFood = 0;
            // float tax, tax_amt, disc, disc_amt;
            string mname, qty, rate, amt;
            //array for food
            string[] food_m_name = new string[100];
            string[] food_Qty = new string[100];
            string[] food_rt = new string[100];
            string[] food_amount = new string[100];
            //array for drink
            string[] drink_m_name = new string[100];
            string[] drink_Qty = new string[100];
            string[] drink_rt = new string[100];
            string[] drink_amount = new string[100];
            int counter = 0;

            string Grosstotal = "";
            int foodCounter = 0, drinkCounter = 0;

            //hide code    // string qur = "SELECT menu.m_name as nm, sum(sales_item.qty) as qty, sales_item.rate as rt, sum(sales_item.total_amount) as amt,menu.category FROM menu INNER JOIN sales_item ON menu.menu_id = sales_item.menu_id WHERE (sales_item.order_id = '" + this.billid + "')group by menu.m_name,sales_item.rate,menu.category";

            /*string qur = @"SELECT menu.printName as nm, sum(sales_item.qty) as qty, sales_item.rate as rt, sum(sales_item.total_amount) as amt,menu.category,cat.BillPrinter as billPrinter
 FROM menu INNER JOIN sales_item ON menu.menu_id = sales_item.menu_id 
 inner join category cat on menu.category=cat.cat_name
 WHERE (sales_item.order_id = '" + this.billid + "')  group by menu.printName,sales_item.rate,menu.category,cat.BillPrinter";*/


            string qur = @" SELECT menu.printName as nm, min(sales_item.sales_id),sum(sales_item.qty) as qty, sales_item.rate as rt, 
 sum(sales_item.total_amount) as amt,menu.category,cat.BillPrinter as billPrinter
 FROM menu INNER JOIN sales_item ON menu.menu_id = sales_item.menu_id 
 inner join category cat on menu.category=cat.cat_name
 WHERE sales_item.order_id = '" + this.billid + "' group by menu.printName,sales_item.rate,menu.category,cat.BillPrinter  ORDER BY min(sales_item.sales_id)";



            // new code added by sagar as on 08-08-2018 for set 2 seprate bill print for food and drink bill 


            // hide code ---      // totalAmountOfFood = db.getDb_Value("SELECT SUM(sales_item.total_amount) AS Expr1 FROM  menu INNER JOIN  sales_item ON menu.menu_id = sales_item.menu_id INNER JOIN  category ON menu.category = category.cat_name WHERE        (sales_item.order_id = '" + this.billid + "') AND (category.cat_name <> 'Hard Drinks')");
            totalAmountOfFood = db.getDb_Value("SELECT SUM(sales_item.total_amount) AS Expr1 FROM  menu INNER JOIN  sales_item ON menu.menu_id = sales_item.menu_id INNER JOIN  category ON menu.category = category.cat_name WHERE        (sales_item.order_id = '" + this.billid + "') AND (category.BillPrinter =1)");



            string Billvalue = "";

            // hide code
            //string query12 = "select * from tbl_option where grp='Print formation' and status='Yes'";
            //cn.Open();
            //SqlCommand command1 = new SqlCommand(query12, cn);
            //SqlDataReader reader1 = command1.ExecuteReader();
            //if (reader1.Read())
            //{
            //    //flag_grandtotal = true;
            //    Billvalue = reader1["value"].ToString();
            //}
            //cn.Close();

            Billvalue = db.getDbstatus_Value("select value from tbl_option where grp='Print formation' and status='Yes'");

            #region simpleBillFormat


            if (Billvalue == "Simple")
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(qur, cn);
                SqlDataReader rd = cmd.ExecuteReader();
                string sr = i.ToString();
                string tem_qty = "";
                while (rd.Read() == true)
                {
                    mname = rd["nm"].ToString();
                    qty = rd["qty"].ToString();
                    rate = rd["rt"].ToString();
                    amt = rd["amt"].ToString();

                    tem_qty = qty;

                    subtotal += float.Parse(rd["amt"].ToString());





                    Offset = Offset + 20;
                    graphics.DrawString(" " + MenuFormation(mname, "left", 16) + " " + MenuFormation(tem_qty, "left", 3) + " " + MenuFormation(rate, "left", 4) + " " + MenuFormation(amt, "left", 7) + "\r\n", new Font(reciprtFontName, itemNameFontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                    i++;

                }


                Offset = Offset + 20;
                //graphics.DrawString("----------------------------------\r\n", new Font(reciprtFontName, 10), new SolidBrush(Color.Black), startX, startY + Offset);
                Grosstotal = subtotal.ToString();// items:" + (i - 1) + "  


                Offset = Offset + 20;
                graphics.DrawString(underLine, new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 15;

                graphics.DrawString("                Total: " + Grosstotal + ".00", new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
                cn.Close();

            }   // ************************ simple bill format code end here *****************************
            #endregion

            #region food&DrinkBillFormat
            else if (Billvalue == "Composite")
            {

                //food nad drink bill seprate code 


                cn.Open();
                SqlCommand cmd = new SqlCommand(qur, cn);
                SqlDataReader rd = cmd.ExecuteReader();
                string sr = i.ToString();
                string tem_qty = "";
                string billPrinter = string.Empty;
                string category = string.Empty;

                while (rd.Read() == true)
                {
                    mname = rd["nm"].ToString().Trim();
                    qty = rd["qty"].ToString();
                    rate = rd["rt"].ToString();
                    amt = rd["amt"].ToString();

                    category = rd["category"].ToString();
                    billPrinter = rd["billPrinter"].ToString();

                    // if (category != "HARD DRINKS" && category != "Hard Drinks")
                    if (billPrinter == "1")    // 1 for food and 2 for drink 
                    {
                        food_m_name[foodCounter] = rd["nm"].ToString();
                        food_Qty[foodCounter] = rd["qty"].ToString();
                        food_rt[foodCounter] = rd["rt"].ToString();
                        food_amount[foodCounter] = rd["amt"].ToString();
                        foodCounter++;
                    }
                    else
                    {
                        drink_m_name[drinkCounter] = rd["nm"].ToString();
                        drink_Qty[drinkCounter] = rd["qty"].ToString();
                        drink_rt[drinkCounter] = rd["rt"].ToString();
                        drink_amount[drinkCounter] = rd["amt"].ToString();
                        drinkCounter++;
                    }

                    i++;

                    counter++;
                }

                cn.Close();

                if (!drinkItemPrint && getFoodItemCount != 0)
                {
                    for (int k = 0; k < foodCounter; k++)
                    {

                        mname = food_m_name[k];
                        qty = food_Qty[k];
                        rate = food_rt[k];
                        amt = food_amount[k];
                        if (mname != null)
                        {
                            foodSum += double.Parse(amt.ToString());



                            Offset = Offset + 20;
                            //  graphics.DrawString("" + mname + "" + qty + "" + rate + "" + amt + "\r\n", new Font(fontType, itemNameFontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                            rect = new RectangleF(startX, startY + Offset, 160, 30);
                            graphics.DrawString(mname, new Font(reciprtFontName, itemNameFontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, leftformat);
                            graphics.DrawString(" " + MenuFormation(" ", "left", 19) + "" + MenuFormation(qty, "left", 3) + " " + MenuFormation(rate, "left", 5) + " " + MenuFormation(amt, "left", 7) + "\r\n", new Font(reciprtFontName, itemNameFontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                            if (mname.Trim().Length > 19)
                                Offset = Offset + 12;
                        }
                    }
                    Offset = Offset + 30;
                    graphics.DrawString(underLine, new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 15;
                    Grosstotal = foodSum + ".00" + " \r\n";

                    graphics.DrawString(MenuFormation("Total Amt :", "right", 30), new Font(fontType, totalFontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                    graphics.DrawString(" " + Grosstotal, new Font(fontType, totalFontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), amtAlign_X, startY + Offset);
                    Offset = Offset + 15;
                    graphics.DrawString(underLine, new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 20;
                }



                //old else end start here for composite bill print 

            #endregion

                if (!drinkItemPrint && getFoodItemCount != 0)
                {
                    string gstAmt = "";
                    string qry = "select * from tbl_option where grp='" + "Tax" + "' and status='Yes'";
                    string value = "", value_1 = "", getDisc = "";


                    float getDisAmt = 0;
                    float getDisvalue = 0;

                    getDisvalue = db.getDb_Value("select  ISNULL(value,0) as value from tbl_option where grp='Discount'");
                    getDisAmt = db.getDb_Value("select discAmt from table_order where order_id='" + this.billid + "'");



                    if (getDisAmt != 0 || getDisvalue != 0)
                    {
                        flag_grandtotal = true;

                        if (getDisvalue != 0)
                        {
                            string discValue = "Dis " + getDisvalue + "% :";//7
                            graphics.DrawString(MenuFormation(discValue, "right", 20), new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                            getDisAmt = (float.Parse(foodSum.ToString()) * getDisvalue / 100);

                        }
                        else
                            graphics.DrawString(MenuFormation("Discount:", "right", 20), new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);


                        graphics.DrawString(" " + getDisAmt.ToString() + ".00", new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), amtAlign_X, startY + Offset);

                        Offset = Offset + 20;
                        grand_total = float.Parse(foodSum.ToString()) - getDisAmt;
                        FoodDrinkSum += grand_total;
                        foodSum = grand_total;
                    }


                    float foodDisVal = db.getDb_Value("select isnull(foodDiscValue,0)    from table_order where order_id='" + this.billid + "'");
                    float foodDisAmt = db.getDb_Value("select isnull(foodDiscAmt,0)    from table_order where order_id='" + this.billid + "'");

                    int x_val = int.Parse(db.getDbstatus_Value("select status from tbl_option where grp='dislineFormat'"));
                    int x_val_1 = int.Parse(db.getDbstatus_Value("select value from tbl_option where grp='dislineFormat'"));
                    int x_val_2 = int.Parse(db.getDbstatus_Value("select process_type from tbl_option where grp='dislineFormat'"));

                    float getFoodServieTax = 0;
                    getFoodServieTax = db.getDb_Value("select serviceTaxFoodVal from table_order where order_id='" + this.billid + "'");


                    string reciprtFontName_1 = "Arial Narrow";


                    if (foodDisVal != 0 && foodDisAmt != 0)
                    {
                        flag_grandtotal = true;


                        rect = new RectangleF(x_val, startY + Offset, 100, 30);
                        graphics.DrawString("Food Dis  :", new Font(reciprtFontName_1, fontSize + 1, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, rightformat);
                        rect = new RectangleF(x_val_1, startY + Offset, 60, 30);
                        //graphics.DrawString(foodDisVal.ToString() + ".0" + " %", new Font(reciprtFontName_1, fontSize+1, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, rightformat);
                        graphics.DrawString(replcePoint(foodDisVal.ToString()) + " %", new Font(reciprtFontName_1, fontSize + 1, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, rightformat);

                        rect = new RectangleF(x_val_2, startY + Offset, 50, 30);
                        graphics.DrawString(Math.Round(foodDisAmt).ToString() + ".00", new Font(reciprtFontName_1, fontSize + 1, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, rightformat);

                        // graphics.DrawString(" " + Math.Round(foodDisAmt).ToString() + ".00", new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), amtAlign_X, startY + Offset);

                        Offset = Offset + 20;

                        // grand_total = float.Parse(foodSum.ToString()) - foodDisAmt;
                        grand_total = float.Parse(foodSum.ToString()) - foodDisAmt;
                        FoodDrinkSum += grand_total;
                        foodSum = grand_total;
                    }

                    //Beverage
                    float BeverageDisVal = db.getDb_Value("select isnull(beveragesDiscValue,0)    from table_order where order_id='" + this.billid + "'");
                    float BeverageDisAmt = db.getDb_Value("select isnull(beveragesDiscAmt,0)   from table_order where order_id='" + this.billid + "'");


                    if (BeverageDisVal != 0 && BeverageDisAmt != 0)
                    {
                        flag_grandtotal = true;


                        if (BeverageDisVal != 0)
                        {
                            rect = new RectangleF(x_val, startY + Offset, 100, 30);
                            graphics.DrawString("Bev. Dis  :", new Font(reciprtFontName_1, fontSize + 1, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, rightformat);
                            rect = new RectangleF(x_val_1, startY + Offset, 60, 30);
                            graphics.DrawString(replcePoint(BeverageDisVal.ToString()) + " %", new Font(reciprtFontName_1, fontSize + 1, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, rightformat);
                            rect = new RectangleF(x_val_2, startY + Offset, 50, 30);
                            graphics.DrawString(Math.Round(BeverageDisAmt).ToString() + ".00", new Font(reciprtFontName_1, fontSize + 1, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, rightformat);


                            Offset = Offset + 20;
                            // grand_total = float.Parse(foodSum.ToString()) - foodDisAmt;
                            grand_total = float.Parse(foodSum.ToString()) - BeverageDisAmt;
                            FoodDrinkSum += grand_total;
                            foodSum = grand_total;
                        }
                    }




                    cn.Open();
                    SqlCommand cmd1 = new SqlCommand(qry, cn);
                    SqlDataReader rd1 = cmd1.ExecuteReader();
                    if (rd1.Read())
                    {
                        flag_grandtotal = true;
                        value = rd1["value"].ToString();

                        //  tax_amt = (float.Parse(value) / 2 * totalAmountOfFood) / 100;  //foodSum
                        tax_amt = (float.Parse(value) / 2 * float.Parse(foodSum.ToString())) / 100;
                        tax = subtotal + tax_amt;
                        getDisc = System.Math.Round(disc_amt, 2).ToString(); //disc_amt.ToString(); //Math.Round(disc_amt).ToString();

                        //gstAmt = Math.Round(tax_amt).ToString();
                        gstAmt = System.Math.Round(tax_amt, 2).ToString();// tax_amt.ToString();  //amtAlign_X


                        serviceTaxAmt = float.Parse(foodSum.ToString()) * getFoodServieTax / 100;



                        rect = new RectangleF(x_val, startY + Offset, 100, 30);
                        graphics.DrawString(" CGST  : ", new Font(reciprtFontName_1, fontSize + 1, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, rightformat);
                        rect = new RectangleF(x_val_1, startY + Offset, 60, 30);
                        graphics.DrawString((float.Parse(value) / 2) + " %", new Font(reciprtFontName_1, fontSize + 1, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, rightformat);
                        rect = new RectangleF(x_val_2, startY + Offset, 50, 30);
                        graphics.DrawString(gstAmt.ToString(), new Font(reciprtFontName_1, fontSize + 1, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, rightformat);

                        Offset = Offset + 20;



                        rect = new RectangleF(x_val, startY + Offset, 100, 30);
                        graphics.DrawString(" SGST  : ", new Font(reciprtFontName_1, fontSize + 1, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, rightformat);
                        rect = new RectangleF(x_val_1, startY + Offset, 60, 30);
                        graphics.DrawString((float.Parse(value) / 2) + " %", new Font(reciprtFontName_1, fontSize + 1, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, rightformat);
                        rect = new RectangleF(x_val_2, startY + Offset, 50, 30);
                        graphics.DrawString(gstAmt.ToString(), new Font(reciprtFontName_1, fontSize + 1, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, rightformat);

                        Offset = Offset + 20;

                        //graphics.DrawString(MenuFormation("S.C : 6 % ", "right", 20), new Font(reciprtFontName, 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                        //graphics.DrawString(" " + serviceTaxAmt.ToString(), new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), amtAlign_X, startY + Offset);


                        if (getFoodServieTax != 0 && serviceTaxAmt != 0)
                        {
                            rect = new RectangleF(x_val, startY + Offset, 100, 30);
                            graphics.DrawString(" S.C  : ", new Font(reciprtFontName_1, fontSize + 1, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, rightformat);
                            rect = new RectangleF(x_val_1, startY + Offset, 60, 30);
                            graphics.DrawString(getFoodServieTax.ToString() + ".0 %", new Font(reciprtFontName_1, fontSize + 1, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, rightformat);
                            rect = new RectangleF(x_val_2, startY + Offset, 50, 30);
                            graphics.DrawString(serviceTaxAmt.ToString(), new Font(reciprtFontName_1, fontSize + 1, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, rightformat);
                            Offset = Offset + 20;
                        }



                    }
                    cn.Close();






                    //test end
                    FoodDrinkSum += float.Parse(foodSum.ToString());

                    if (flag_grandtotal)
                    {
                        grand_total = float.Parse(foodSum.ToString()) + tax_amt + tax_amt - disc_amt + serviceTaxAmt;

                        //FoodDrinkSum += grand_total;
                        FoodDrinkSum = grand_total;

                        graphics.DrawString(underLine, new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                        Offset = Offset + 20;
                        graphics.DrawString("  Grand Total", new Font(reciprtFontName, GrandFontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                        graphics.DrawString(System.Math.Round(grand_total).ToString() + ".00", new Font(reciprtFontName, GrandFontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), amtAlign_X, startY + Offset);

                        Offset = Offset + 20;
                    }



                    graphics.DrawString(underLine, new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 20;
                }


                double drksum = 0;
                if (drinkItemPrint && printGrandTotal && drinkCounter != 0)
                {
                    // for loop for drink  
                    for (int j = 0; j < drinkCounter; j++)
                    {

                        mname = drink_m_name[j];
                        qty = drink_Qty[j];
                        rate = drink_rt[j];
                        amt = drink_amount[j];
                        if (mname != null)
                        {
                            // drinkSum += double.Parse(amt.ToString());
                            drksum += double.Parse(amt.ToString());


                            Offset = Offset + 20;
                            //graphics.DrawString(" " + MenuFormation(mname, "left", 16) + "  " + MenuFormation(qty, "left", 3) + " " + MenuFormation(rate, "left", 4) + " " + MenuFormation(amt, "left", 7) + "\r\n", new Font(reciprtFontName, itemNameFontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                            rect = new RectangleF(startX, startY + Offset, 160, 30);
                            graphics.DrawString(mname, new Font(reciprtFontName, itemNameFontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, leftformat);
                            graphics.DrawString(" " + MenuFormation(" ", "left", 19) + " " + MenuFormation(qty, "left", 3) + " " + MenuFormation(rate, "left", 5) + " " + MenuFormation(amt, "left", 7) + "\r\n", new Font(reciprtFontName, itemNameFontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                            if (mname.Trim().Length > 19)
                                Offset = Offset + 12;


                        }
                    }


                    if (printGrandTotal && drinkCounter != 0) // if drink is not in bill
                    {
                        Offset = Offset + 40;
                        graphics.DrawString(underLine, new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                        Offset = Offset + 20;
                        graphics.DrawString("     Sub Total ", new Font(reciprtFontName, 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                        graphics.DrawString(" " + drksum.ToString(), new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), amtAlign_X, startY + Offset);
                        Offset = Offset + 20;
                        graphics.DrawString(underLine, new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                        Offset = Offset + 20;


                        //********************** code pending 

                        //liquor
                        float liquorDisVal = db.getDb_Value("select isnull(liquorDiscValue,0)   from table_order where order_id='" + this.billid + "'");
                        float liquorDisAmt = db.getDb_Value("select isnull(liquorDiscAmt,0)   from table_order where order_id='" + this.billid + "'");


                        if (liquorDisVal != 0 && liquorDisAmt != 0)
                        {
                            flag_grandtotal = true;



                            graphics.DrawString(MenuFormation("    LiquorDis: " + replcePoint(liquorDisVal.ToString()) + " %", "right", 20), new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                            graphics.DrawString(" " + liquorDisAmt.ToString(), new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), amtAlign_X, startY + Offset);

                            Offset = Offset + 20;
                            // grand_total = float.Parse(foodSum.ToString()) - foodDisAmt;
                            drksum -= liquorDisAmt;
                        }


                        float serviceTaxLiquorVal = 0;
                        serviceTaxLiquorVal = db.getDb_Value("select serviceTaxLiquorVal from table_order where order_id='" + this.billid + "'");

                        serviceTaxAmt = float.Parse(drksum.ToString()) * serviceTaxLiquorVal / 100;

                        if (serviceTaxLiquorVal != 0 && serviceTaxAmt != 0)
                        {


                            graphics.DrawString("           S.C " + serviceTaxLiquorVal.ToString() + " % ", new Font(reciprtFontName, 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                            graphics.DrawString(" " + serviceTaxAmt.ToString(), new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), amtAlign_X, startY + Offset);
                        }
                        Offset = Offset + 20;
                        //vat amt

                        vatAmt = float.Parse(drksum.ToString()) * 5 / 100;

                        graphics.DrawString("           VAT 5 % ", new Font(reciprtFontName, 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                        graphics.DrawString(" " + vatAmt.ToString(), new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), amtAlign_X, startY + Offset);
                        Offset = Offset + 20;



                        graphics.DrawString(underLine, new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                        Offset = Offset + 20;

                        graphics.DrawString("       Total  :", new Font(reciprtFontName, GrandFontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                        graphics.DrawString(" " + System.Math.Round(drksum + vatAmt + serviceTaxAmt).ToString() + ".00", new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), amtAlign_X, startY + Offset);

                        Offset = Offset + 20;
                        graphics.DrawString(underLine, new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                        Offset = Offset + 20;

                        string str_foodDrinkSum = FoodDrinkSum.ToString();
                        graphics.DrawString("  Grand Total", new Font(reciprtFontName, GrandFontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                        graphics.DrawString(System.Math.Round(drksum + FoodDrinkSum + vatAmt + serviceTaxAmt).ToString() + ".00", new Font(reciprtFontName, GrandFontSize + 2, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), amtAlign_X, startY + Offset);

                        Offset = Offset + 20;

                        graphics.DrawString(underLine, new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);


                    }
                }



                //****************** For Parcel display customer name,address and contact No ****************************

                if (ChkDb_Value("SELECT        table_order.t_id, Custmer.name, Custmer.addr, tbl_parcel_order.order_id FROM            tbl_parcel_order INNER JOIN     Custmer ON tbl_parcel_order.Customer_id = Custmer.Customer_id INNER JOIN    table_order ON tbl_parcel_order.order_id = table_order.order_id WHERE        (table_order.t_id = '" + this.tableno + "') and  tbl_parcel_order.order_id='" + this.billid + "' "))
                {
                    cn.Close();
                    string query1 = "SELECT        table_order.t_id, Custmer.name, Custmer.addr,Custmer.phone, tbl_parcel_order.order_id FROM            tbl_parcel_order INNER JOIN     Custmer ON tbl_parcel_order.Customer_id = Custmer.Customer_id INNER JOIN    table_order ON tbl_parcel_order.order_id = table_order.order_id WHERE        (table_order.t_id = '" + this.tableno + "') and tbl_parcel_order.order_id='" + this.billid + "'";
                    string Name = "", Address = "", contactNo = "";
                    cn.Open();
                    SqlCommand command = new SqlCommand(query1, cn);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        //  flag_grandtotal = true;
                        Name = reader["name"].ToString();
                        Address = reader["addr"].ToString();
                        contactNo = reader["phone"].ToString();

                        //graphics.DrawString("\n ", new Font(reciprtFontName, 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                        //Offset = Offset + 20;
                        graphics.DrawString("\n ", new Font(reciprtFontName, 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                        Offset = Offset + 20;
                        graphics.DrawString("----------------------------------\r", new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                        Offset = Offset + 20;
                        graphics.DrawString("Parcel Order Details ", new Font(reciprtFontName, 14, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                        Offset = Offset + 25;
                        graphics.DrawString("Name :" + Name, new Font(reciprtFontName, 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                        Offset = Offset + 20;
                        graphics.DrawString("Address :" + Address, new Font(reciprtFontName, 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                        Offset = Offset + 20;
                        graphics.DrawString("Mobile :" + contactNo, new Font(reciprtFontName, 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                        Offset = Offset + 20;

                    }
                    cn.Close();


                }
                cn.Close(); // close 
                Offset = Offset + 7;
                graphics.DrawString(footer1, new Font("Courier New", 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
                graphics.DrawString(footer2, new Font("Courier New", footer2Font_size, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
                graphics.DrawString(companyNm, new Font(reciprtFontName, cmpnyNameFont_size, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
                //Yogesh For Customer Print on Bill ......./////
                if (db.ChkDb_Value(" select * from tbl_option where  grp='CustomerDetails'and status='Yes'and value='Yes'"))
                {

                    graphics.DrawString("Customer Details :   \r", new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 16;
                    graphics.DrawString(" Customer Name :" + custN + "  \r", new Font(reciprtFontName, 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 16;
                    graphics.DrawString(" Mobile No : " + mobn + "   \r", new Font(reciprtFontName, 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 16;
                    graphics.DrawString(" GSTIN : " + custgst + "   \r", new Font(reciprtFontName, 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 16;


                    //tejashri 13-04-2019
                    if (remark != "0")
                    {
                        rect = new RectangleF(30, startY + Offset, 160, 30);
                        graphics.DrawString(remark, new Font(reciprtFontName, itemNameFontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, leftformat);
                    }
                        if (remark.Trim().Length > 25)
                        Offset = Offset + 12;

                    //graphics.DrawString(" " + remark + "   \r", new Font(reciprtFontName, 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 16;
                    

                }
                //tejashri 13-04-2019 for discount reason
                if (db.ChkDb_Value(" select * from tbl_option where  grp='DiscountReason'and status='Yes'"))
                {


                    graphics.DrawString(underLine, new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 16;


                    rect = new RectangleF(30, startY + Offset, 160, 30);
                    graphics.DrawString(discountReason, new Font(reciprtFontName, itemNameFontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, centerformat);

                    //  graphics.DrawString(discountReason, new Font(reciprtFontName, GrandFontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), amtAlign_X, startY + Offset);
                    Offset = Offset + 16;
                    graphics.DrawString(underLine, new Font(reciprtFontName, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                    Offset = Offset + 20;
                }

                Offset = Offset + 10;
                graphics.DrawString(db.getDbstatus_Value("select value from tbl_option where grp='OnBillFooter'"), new Font("Courier New", footer2Font_size, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
            }



        }





        public string replcePoint(string getVal)
        {
            if (!getVal.Contains('.'))
                getVal += ".0";

            return getVal;
        }


        public string MenuFormation(string menuString, string menuAlignment, int menuCharSize)
        {
            switch (menuAlignment)
            {
                case "left":

                    if (menuString.Length < menuCharSize)
                    {
                        while (menuString.Length != menuCharSize)
                        {
                            menuString += " ";
                        }
                    }
                    else
                    { menuString = menuString.Substring(0, menuCharSize); }

                    break;

                case "middle":
                    while (menuString.Length < menuCharSize)
                    {
                        //while (menuString.Length != menuCharSize)
                        //{
                        menuString = " " + menuString + " ";
                        // }
                    }


                    break;
                case "right":

                    if (menuString.Length < menuCharSize)
                    {
                        while (menuString.Length != menuCharSize)
                        {
                            menuString = " " + menuString;
                        }
                    }
                    else
                    { menuString = menuString.Substring(0, menuCharSize); }
                    break;


            }
            return menuString;
        }

        //temp print formation 
        string add2Size = "", Address2 = "";
        void get_ReceiptFormt()
        {
            string qur = "select * from tbl_receiptFormat";
            cn.Open();
            SqlCommand cmd = new SqlCommand(qur, cn);
            SqlDataReader rd_rcFormat = cmd.ExecuteReader();

            while (rd_rcFormat.Read() == true)
            {
                hotelNam = rd_rcFormat["hotelName"].ToString();
                tagline = rd_rcFormat["tagline"].ToString();
                address = rd_rcFormat["address"].ToString();
                mob = rd_rcFormat["mobile"].ToString();
                footer1 = rd_rcFormat["footer1"].ToString();
                footer2 = rd_rcFormat["footer2"].ToString();
                companyNm = rd_rcFormat["companyName"].ToString();
                hotelNmfont_size = int.Parse(rd_rcFormat["hotelNmFontSize"].ToString());
                addressFont_size = int.Parse(rd_rcFormat["addressFontSize"].ToString());
                mobileFont_size = int.Parse(rd_rcFormat["mobileFontSize"].ToString());
                footer2Font_size = int.Parse(rd_rcFormat["footer2FontSize"].ToString());
                taglineFont_size = int.Parse(rd_rcFormat["taglineFontSize"].ToString());
                itemNameFontSize = int.Parse(rd_rcFormat["itemNmFontSize"].ToString());
                cmpnyNameFont_size = int.Parse(rd_rcFormat["companyNameFontSize"].ToString());

                add2Size = (rd_rcFormat["AddressSize2"].ToString());
                Address2 = (rd_rcFormat["address2"].ToString());

                while (hotelNam.Length <= 30)
                {
                    hotelNam = " " + hotelNam + " ";
                }

                while (tagline.Length <= 30)
                {
                    tagline = " " + tagline + " ";
                }
                while (address.Length <= 30)
                {
                    address = " " + address + " ";
                }
                while (mob.Length <= 30)
                {
                    mob = " " + mob + " ";
                }
                while (footer1.Length <= 30)
                {
                    footer1 = " " + footer1 + " ";
                }
                while (footer2.Length <= 30)
                {
                    footer2 = " " + footer2 + " ";
                }
                while (companyNm.Length <= 30)
                {
                    companyNm = " " + companyNm + " ";
                }
                while (Address2.Length <= 30)
                {
                    Address2 = " " + Address2 + " ";
                }
                while (add2Size.Length <= 30)
                {
                    add2Size = " " + add2Size + " ";
                }
            }

            cn.Close();
        }


        //fianl kot after all order cmplt
        public void print_kotFinalTable()
        {
            //Get Printer name here
            cn.Open();
            SqlCommand cmd = new SqlCommand("select value from tbl_option where grp='KOT Printer'", cn);
            try
            {
                if (cmd.ExecuteScalar().ToString() == "")
                    PrinterName = "0";
                else
                    PrinterName = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {

            }
            cn.Close();
            PrintDialog pd = new PrintDialog();
            pdoc = new PrintDocument();

            PrinterSettings ps = new PrinterSettings();
            Font font = new Font("Courier New", 15);


            PaperSize psize = new PaperSize("Custom", 100, 5000000);
            //ps.DefaultPageSettings.PaperSize = psize;



            pd.Document = pdoc;
            pd.Document.DefaultPageSettings.PaperSize = psize;
            //pdoc.DefaultPageSettings.PaperSize.Height =320;
            pdoc.DefaultPageSettings.PaperSize.Height = 5000000;

            pdoc.DefaultPageSettings.PaperSize.Width = 520;


            //Assign Printer name here

            PrinterSettings printer = new PrinterSettings();
            pdoc.PrinterSettings.PrinterName = (PrinterName);

            pdoc.PrintPage += new PrintPageEventHandler(kot_finalTable); //******************************************************

            pdoc.Print();

        }



        //kot for the final table item chk for per order
        public void kot_finalTable(object sender, PrintPageEventArgs e)
        {
            //get_ReceiptFormt(); //get the receipt format from the db

            Database db = new Database();
            string get_suggestion = "";
            string sugg = "";
            string fontName = "Microsoft Sans Serif";//Courier New
            string fontnameC = "Lucida Fax";
            Graphics graphics = e.Graphics;
            string mname, qty, discrip;
            //string billId = "";
            int c = 1;
            // LPrinter MyPrinter = new LPrinter();
            //string qur = "SELECT menu.m_name as nm, sales_item.qty as qty FROM menu INNER JOIN sales_item ON menu.menu_id = sales_item.menu_id WHERE (sales_item.order_id = '" + billId + "')";
            //SqlConnection cn = new SqlConnection();
            string kot_id = db.getDb_Value("select max(kot_id) from sales_item where order_id='" + billid + "'").ToString();
            string qur = " SELECT     sales_item.menu_id, sales_item.qty, menu.m_name, sales_item.w_id, sales_item.order_id ,sales_item.order_sugg FROM         sales_item INNER JOIN     menu ON sales_item.menu_id = menu.menu_id WHERE sales_item.order_id = '" + billid + "'";
            string Covers = db.getDb_Value("select noOfGuest from table_order where order_id='" + billid + "'").ToString();
            sugg = db.getDb_Value("select order_sugg from sales_item where kot_id='" + kot_id + "'").ToString();

            // get_suggestion = db.getDbstatus_Value("select description from tbl_orderSuggestion where kot_id='" + kot_id + "'").ToString();
            //get_suggestion = "";
            cn.Open();
            SqlCommand cmd = new SqlCommand(qur, cn);
            SqlDataReader rd = cmd.ExecuteReader();
            int i = 1;
            int startX = 0;
            int startD = 30;
            int startY = 30;
            int Offset = 0;
            var centerformat = new StringFormat() { Alignment = StringAlignment.Center };
            var leftformat = new StringFormat() { Alignment = StringAlignment.Near };
            var rect = new RectangleF(0, 0, 265, 20);

            if (db.ChkDb_Value("select value from tbl_option where grp='PrintSize' and value='3 Inch'"))
            {
                //graphics.DrawString(" T:" + this.tableno + "\t            B:" + this.billid + "          KOT:" + kot_id + "  \r\n", new Font(fontName, 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                //Offset = Offset + 20;
                graphics.DrawString("T: ", new Font(fontName, 11, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                graphics.DrawString("     " + this.tableno + "", new Font(fontName, 11, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                graphics.DrawString("                       B:", new Font(fontName, 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                graphics.DrawString("                         " + this.billid + "", new Font(fontName, 11, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                graphics.DrawString("                                          KOT:", new Font(fontName, 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                graphics.DrawString("                                              " + kot_id + " ", new Font(fontName, 11, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);






                Offset = Offset + 20;
                graphics.DrawString(" Date :" + System.DateTime.Now.ToString("dd/MM/yyyy") + "       Time : " + System.DateTime.Now.ToString("hh:mm:ss") + " \r\n", new Font(fontName, 9, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                Offset = Offset + 20;
                graphics.DrawString("  By  : " + this.wname + "              Covers:  " + Covers + "    \r\n", new Font(fontName, 9, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);


                Offset = Offset + 15;
                graphics.DrawString("-------------------------------------------------------------------\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 10;
                graphics.DrawString(" Item Name                                                         Qty  \r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 10;
                graphics.DrawString("-------------------------------------------------------------------\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

            }
            if (db.ChkDb_Value("select value from tbl_option where grp='PrintSize' and value='2 Inch'"))
            {
                graphics.DrawString(" Table NO :" + this.tableno + "          Bill No:" + this.billid + " \r\n", new Font(fontName, 7, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                Offset = Offset + 20;
                graphics.DrawString(" Date :" + System.DateTime.Now.ToString("dd/MM/yyyy") + "    Time : " + System.DateTime.Now.ToString("hh:mm:ss") + " \r\n", new Font(fontName, 7, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                Offset = Offset + 20;
                graphics.DrawString(" Waiter Name : " + this.wname + "\r\n", new Font(fontName, 7, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);


                Offset = Offset + 15;
                graphics.DrawString("---------------------------------------------------------------------\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 10;
                graphics.DrawString(" Item Name                                Qty  \r\n", new Font(fontName, 7, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 10;
                graphics.DrawString("---------------------------------------------------------------------\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

            }
            string sr = i.ToString();

            while (rd.Read() == true)
            {
                mname = rd["m_name"].ToString();
                qty = rd["qty"].ToString();
                discrip = rd["order_sugg"].ToString();


                //if (mname.Length <= 40)
                //{
                //    while (mname.Length <= 40)
                //    {
                //        mname += " ";
                //    }
                //}
                //else
                //{ mname = mname.Substring(0, 40); }


                while (qty.Length <= 3)
                {

                    qty = " " + qty;
                }

                Offset = Offset + 22;
                if (db.ChkDb_Value("select value from tbl_option where grp='PrintSize' and value='3 Inch'"))
                {

                    rect = new RectangleF(startX, startY + Offset, 240, 38);

                    //var rect = new RectangleF(0, 0, 265, 20);

                    // graphics.DrawString(hotelNam.Trim(), new Font(reciprtFontName, hotelNmfont_size, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, centerformat);

                    //Offset = Offset + 25;
                    graphics.DrawString(mname + "\r\n", new Font(fontName, 11, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, leftformat);

                    if (discrip != "")
                    {
                        Offset = Offset + 18;
                        graphics.DrawString(discrip + "\r\n", new Font(fontnameC, 10, System.Drawing.FontStyle.Regular), new SolidBrush(Color.Black), startD, startY + Offset);

                    }
                    // graphics.DrawString(    discrip + "\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, centerformat);

                    graphics.DrawString("                                                   " + qty + "\r\n", new Font(fontName, 12, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                    // Offset = Offset + 20;
                    if (mname.Length > 25)
                    {
                        Offset = Offset + 18;
                    }



                }


                if (db.ChkDb_Value("select value from tbl_option where grp='PrintSize' and value='2 Inch'"))
                {
                    graphics.DrawString(" " + mname + "\r\n", new Font(fontName, 6, System.Drawing.FontStyle.Regular), new SolidBrush(Color.Black), startX, startY + Offset);

                    graphics.DrawString("                                                              " + qty + "\r\n", new Font(fontName, 6, System.Drawing.FontStyle.Regular), new SolidBrush(Color.Black), startX, startY + Offset);
                }
                i++;
            }

            Offset = Offset + 10;
            if (db.ChkDb_Value("select value from tbl_option where grp='PrintSize' and value='3 Inch'"))
            {
                graphics.DrawString("---------------------------------------------------------------------\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
                graphics.DrawString("Note:\r\n", new Font(fontName, 7, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 8;
                graphics.DrawString("     " + get_suggestion, new Font("Courier New", 11, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            }
            if (db.ChkDb_Value("select value from tbl_option where grp='PrintSize' and value='2 Inch'"))
            {
                graphics.DrawString("---------------------------------------------------------------------\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
                graphics.DrawString("Note:\r\n", new Font(fontName, 7, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
                graphics.DrawString("      " + get_suggestion, new Font("Courier New", 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            }
            cn.Close();

        }
        //here dummmy bill pprint

        //here dummmy bill pprint

        public void dummyBill()
        {

            //Get Printer name here
            cn.Open();
            SqlCommand cmd = new SqlCommand("select value from tbl_option where grp='Bill Printer'", cn);
            try
            {
                if (cmd.ExecuteScalar().ToString() == "")
                    PrinterName = "0";
                else
                    PrinterName = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {

            }
            cn.Close();

            PrintDialog pd = new PrintDialog();
            pdoc = new PrintDocument();

            PrinterSettings ps = new PrinterSettings();
            Font font = new Font("Courier New", 12);


            PaperSize psize = new PaperSize("Custom", 820, 5000000);//100,200
            //ps.DefaultPageSettings.PaperSize = psize;



            pd.Document = pdoc;

            pd.Document.DefaultPageSettings.PaperSize = psize;
            //pdoc.DefaultPageSettings.PaperSize.Height =320;
            pdoc.DefaultPageSettings.PaperSize.Height = 5000000;

            pdoc.DefaultPageSettings.PaperSize.Width = 520;

            //Assign Printer name here

            PrinterSettings printer = new PrinterSettings();
            pdoc.PrinterSettings.PrinterName = (PrinterName);


            pdoc.OriginAtMargins = false;


            pdoc.PrintPage += new PrintPageEventHandler(this.dummyBillPrint);  //test the new code 12-1-2014 by sagar


            pdoc.Print();

        }
        void dummyBillPrint(object sender, PrintPageEventArgs e)
        {

            Graphics graphics = e.Graphics;
            Font font = new Font("Courier New", 10);
            string fontType = "Courier New";
            float fontHeight = font.GetHeight();
            int startX = 0;
            int startY = 30;//30
            int Offset = 0;
            int fontSize = 9;
            //string billDate = "";
            //string date = DateTime.Now.ToShortDateString().ToString();
            float tax_amt = 0, disc_amt = 0, grand_total = 0, tax, disc;
            bool flag_grandtotal = false;
            string date;

            cn.Open();
            string query = "select ddate from tbl_dayend_status";
            string str;
            SqlCommand cmd3 = new SqlCommand(query, cn);


            if (cmd3.ExecuteScalar().ToString() == "")
                str = "0";
            else
                str = cmd3.ExecuteScalar().ToString();

            cn.Close();


            // get font size from database
            int grpNameSize = 0;
            cn.Open();
            string qury = "select * from tbl_receiptFormat";
            SqlCommand comnd = new SqlCommand(qury, cn);
            SqlDataReader reader123 = comnd.ExecuteReader();
            while (reader123.Read())
            {
                hotelNmfont_size = int.Parse(reader123["hotelNmFontSize"].ToString());
                taglineFont_size = int.Parse(reader123["taglineFontSize"].ToString());
                addressFont_size = int.Parse(reader123["addressFontSize"].ToString());
                mobileFont_size = int.Parse(reader123["mobileFontSize"].ToString());
                footer2Font_size = int.Parse(reader123["footer2FontSize"].ToString());
                cmpnyNameFont_size = int.Parse(reader123["companyNameFontSize"].ToString());
                itemNameFontSize = int.Parse(reader123["itemNmFontSize"].ToString());
                tblNoFontSize = int.Parse(reader123["tblNoFontSize"].ToString());
                billNoFontSize = int.Parse(reader123["billNoFontSize"].ToString());
                dateFontSize = int.Parse(reader123["dateFontSize"].ToString());
                totalFontSize = int.Parse(reader123["totalFontSize"].ToString());
                discFontSize = int.Parse(reader123["discFontSize"].ToString());
                GrandFontSize = int.Parse(reader123["GrandFontSize"].ToString());
                //grpName = (reader123["groupName"].ToString());
                //grpNameSize = int.Parse(reader123["grpFontSize"].ToString());





            }

            //if (comnd.ExecuteScalar().ToString() == "")
            //    font_size = 0;
            //else
            //    font_size =int.Parse(comnd.ExecuteScalar().ToString());

            cn.Close();

            get_ReceiptFormt();

            // ********** here print image code *************
            string LogoName = "";
            cn.Open();
            string quryLogo = "select value from tbl_option where grp='LogoPrint' and status='Yes' ";
            SqlCommand cmdLogo = new SqlCommand(quryLogo, cn);
            SqlDataReader rdLogo = cmdLogo.ExecuteReader();
            while (rdLogo.Read())
            {
                LogoName = rdLogo["value"].ToString();
                using (Image logo = Image.FromFile(@"D:\\img\\" + LogoName + ""))
                {
                    graphics.DrawImage(logo, new Point(0, 0));
                }
                Offset = Offset + 160;
            }
            cn.Close();


            //graphics.DrawString("              " + grpName, new Font("Courier New", grpNameSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            //Offset = Offset + 20;
            graphics.DrawString(hotelNam, new Font("Courier New", hotelNmfont_size, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString(tagline, new Font("Courier New", taglineFont_size, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString(address, new Font("Courier New", addressFont_size, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString(mob, new Font("Courier New", mobileFont_size, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;

            graphics.DrawString("----------------------------------\r", new Font("Courier New", fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;

            //graphics.DrawString(" Table NO.:" + this.tableno + "  Bill No:" + this.billid + "  \r\n", new Font("Courier New", 11, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            //Offset = Offset + 20;



            graphics.DrawString(" Table NO.:" + this.tableno + "   \r", new Font("Courier New", tblNoFontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            //Offset = Offset + 20;
            graphics.DrawString("                Bill No:" + this.billid + "  \r\n", new Font("Courier New", billNoFontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;

            graphics.DrawString(" Date :" + billDate + "  " + DateTime.Now.ToString("hh:mm:ss tt") + "\r", new Font("Courier New", dateFontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            String underLine = "----------------------------------";
            graphics.DrawString(underLine, new Font("Courier New", fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;


            string custName = "", Custaddress = "", phone = "", ParcelSettingStatus = "";
            cn.Open();
            SqlCommand cmdCustDtl = new SqlCommand("select name,addr,phone from Custmer where Customer_id=(select Customer_id from tbl_parcel_order where order_id='" + billid + "')", cn);
            SqlDataReader rdCustdtl = cmdCustDtl.ExecuteReader();
            while (rdCustdtl.Read())
            {
                custName = rdCustdtl["name"].ToString();
                Custaddress = rdCustdtl["addr"].ToString();
                phone = rdCustdtl["phone"].ToString();

            }
            cn.Close();

            // get parcel Details Setting
            cn.Open();
            SqlCommand cmdParcelSetting = new SqlCommand("select * from tbl_option where grp='Parcel Details'", cn);
            SqlDataReader rdParcelSetting = cmdParcelSetting.ExecuteReader();
            while (rdParcelSetting.Read())
            {
                ParcelSettingStatus = rdParcelSetting["status"].ToString();
            }
            cn.Close();


            if (tableno == "100")// if (ChkDb_Value("SELECT        table_order.t_id, Custmer.name, Custmer.addr, tbl_parcel_order.order_id FROM            tbl_parcel_order INNER JOIN     Custmer ON tbl_parcel_order.Customer_id = Custmer.Customer_id INNER JOIN    table_order ON tbl_parcel_order.order_id = table_order.order_id WHERE        (table_order.t_id like '%P%') and  tbl_parcel_order.order_id='" + this.billid + "' "))
            {
                if (ParcelSettingStatus == "Yes")
                {
                    graphics.DrawString(" Customer Details  \r\n", new Font("Courier New", 14, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 20;

                    graphics.DrawString(" Customer Name:" + custName + "\r\n", new Font("Courier New", 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 20;

                    string value1 = "", value2 = "", value3 = "", value4 = "";
                    string[] strarr = (Custaddress).Split(' ');
                    int cnt = strarr.Count();
                    for (int k = 0; k < cnt; k++)
                    {
                        if (k < 3)
                        {
                            value1 += strarr[k] + " ";
                        }
                        else if (k >= 3 && k < 5)
                        {
                            value2 += strarr[k] + " ";
                        }
                        else if (k >= 5 && k < 7)
                        {
                            value3 += strarr[k] + " ";
                        }
                        else
                            value4 += strarr[k] + " ";

                    }

                    graphics.DrawString(" Address : \r", new Font("Courier New", 11, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 15;
                    graphics.DrawString(" " + value1, new Font("Courier New", 11, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                    Offset = Offset + 15;
                    graphics.DrawString(" " + value2, new Font("Courier New", 11, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 15;
                    graphics.DrawString(" " + value3, new Font("Courier New", 11, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 15;
                    graphics.DrawString(" " + value4, new Font("Courier New", 11, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 20;

                    graphics.DrawString(" Mobile No:" + phone + "\r\n", new Font("Courier New", 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 20;
                    graphics.DrawString("---------------------------------------", new Font("Courier New", fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 15;
                }
            }

            graphics.DrawString(" Item Name     Qty   Rate  Amount \r", new Font("Courier New", fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

            Offset = Offset + 15;
            graphics.DrawString(underLine, new Font("Courier New", fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            //String Source = this.item_nm;
            //graphics.DrawString("Item Name " + item_nm + " Qty " + Destination, new Font("Courier New", 10), new SolidBrush(Color.Black), startX, startY + Offset);
            int i = 1;
            float subtotal = 0;
            // float tax, tax_amt, disc, disc_amt;
            string mname, qty, rate, amt;
            //array for food
            string[] food_m_name = new string[100];
            string[] food_Qty = new string[100];
            string[] food_rt = new string[100];
            string[] food_amount = new string[100];
            //array for drink
            string[] drink_m_name = new string[100];
            string[] drink_Qty = new string[100];
            string[] drink_rt = new string[100];
            string[] drink_amount = new string[100];
            int counter = 0;
            string Grosstotal = "";
            int foodCounter = 0, drinkCounter = 0;

            // string qur = "SELECT menu.m_name as nm, sum(qty) as qty, dummySalesItem.rate as rt, sum(total_amount) as amt,menu.category,category.type  FROM menu INNER JOIN dummySalesItem ON menu.menu_id = dummySalesItem.menu_id INNER JOIN  category ON menu.category = category.cat_name WHERE (dummySalesItem.order_id = '" + this.billid + "')group by menu.m_name,dummySalesItem.rate,menu.category,category.type";
            string qur = "SELECT  menu.m_name AS nm, SUM(dummySalesItem.qty) AS qty, dummySalesItem.rate AS rt, SUM(dummySalesItem.total_amount) AS amt, menu.category, category.type FROM    menu INNER JOIN   dummySalesItem ON menu.menu_id = dummySalesItem.menu_id INNER JOIN  category ON menu.category = category.cat_name INNER JOIN  dummyTableOrder ON dummySalesItem.orderDate = dummyTableOrder.timeing AND dummySalesItem.order_id = dummyTableOrder.order_id WHERE        (dummySalesItem.order_id = '" + this.billid + "') AND (dummyTableOrder.timeing = '" + Convert.ToDateTime(billDate).ToString("MM/dd/yyyy") + "') GROUP BY menu.m_name, dummySalesItem.rate, menu.category, category.type";
            string query12 = "select * from tbl_option where grp='Print formation' and status='Yes'";
            string Billvalue = "";
            cn.Close();
            cn.Open();
            SqlCommand command1 = new SqlCommand(query12, cn);
            SqlDataReader reader1 = command1.ExecuteReader();
            if (reader1.Read())
            {
                //flag_grandtotal = true;
                Billvalue = reader1["value"].ToString();
            }
            cn.Close();

            if (Billvalue == "Simple")
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(qur, cn);
                SqlDataReader rd = cmd.ExecuteReader();
                string sr = i.ToString();
                string tem_qty = "";
                while (rd.Read() == true)
                {
                    mname = rd["nm"].ToString();
                    qty = rd["qty"].ToString();
                    rate = rd["rt"].ToString();
                    amt = rd["amt"].ToString();

                    tem_qty = qty;

                    subtotal += float.Parse(rd["amt"].ToString());
                    //while (sr.Length <= 2)
                    //    sr += " ";
                    if (mname.Length < 16)//14
                    {
                        while (mname.Length != 15)
                        {
                            mname += " ";
                        }
                    }
                    else
                    { mname = mname.Substring(0, 15); }



                    while (tem_qty.Length <= 4)
                    {
                        tem_qty = " " + tem_qty;
                    }

                    while (rate.Length <= 5)
                    {
                        rate = " " + rate;
                    }
                    while (amt.Length <= 7)
                    {
                        amt = " " + amt;
                    }
                    Offset = Offset + 20;
                    graphics.DrawString(" " + mname + "" + tem_qty + "" + rate + "" + amt + "\r\n", new Font("Courier New", itemNameFontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                    i++;

                }


                Offset = Offset + 20;
                //graphics.DrawString("----------------------------------\r\n", new Font("Courier New", 10), new SolidBrush(Color.Black), startX, startY + Offset);
                Grosstotal = subtotal.ToString();// items:" + (i - 1) + "  


                Offset = Offset + 20;
                underLine = "----------------------------------";
                graphics.DrawString(underLine, new Font("Courier New", fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;

                graphics.DrawString("        Total: " + Grosstotal + ".00", new Font("Courier New", totalFontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
                cn.Close();

            }
            else
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(qur, cn);
                SqlDataReader rd = cmd.ExecuteReader();
                string sr = i.ToString();
                string tem_qty = "";
                while (rd.Read() == true)
                {
                    mname = rd["nm"].ToString().Trim();
                    qty = rd["qty"].ToString();
                    rate = rd["rt"].ToString();
                    amt = rd["amt"].ToString();



                    //string item_code = rd["item_code"].ToString();
                    string category = rd["category"].ToString();
                    string type = rd["type"].ToString();

                    // if (category != "HARD DRINKS" && category!="Hard Drinks")
                    if (type == "Table Order")
                    {
                        food_m_name[foodCounter] = rd["nm"].ToString();
                        food_Qty[foodCounter] = rd["qty"].ToString();
                        food_rt[foodCounter] = rd["rt"].ToString();
                        food_amount[foodCounter] = rd["amt"].ToString();
                        foodCounter++;
                    }
                    else
                    {
                        drink_m_name[drinkCounter] = rd["nm"].ToString();
                        drink_Qty[drinkCounter] = rd["qty"].ToString();
                        drink_rt[drinkCounter] = rd["rt"].ToString();
                        drink_amount[drinkCounter] = rd["amt"].ToString();
                        drinkCounter++;
                    }

                    i++;

                    counter++;
                }
                for (int k = 0; k < foodCounter; k++)
                {

                    mname = food_m_name[k];
                    qty = food_Qty[k];
                    rate = food_rt[k];
                    amt = food_amount[k];
                    if (mname != null)
                    {
                        foodSum += double.Parse(amt.ToString());

                        if (mname.Length < 14)
                        {
                            while (mname.Length != 13)
                            {
                                mname += " ";
                            }
                        }
                        else
                        { mname = mname.Substring(0, 13); }



                        while (qty.Length <= 3)
                        {
                            qty = " " + qty;
                        }

                        while (rate.Length <= 5)
                        {
                            rate = " " + rate;
                        }
                        while (amt.Length <= 6)
                        {
                            amt = " " + amt;
                        }

                        Offset = Offset + 20;
                        //  graphics.DrawString("" + mname + "" + tem_qty + "" + rate + "" + amt + "\r\n", new Font(fontType, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                        graphics.DrawString("" + mname + "" + qty + "" + rate + "" + amt + "\r\n", new Font(fontType, itemNameFontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                    }
                }
                Offset = Offset + 20;
                //graphics.DrawString("----------------------------------\r\n", new Font("Courier New", 10), new SolidBrush(Color.Black), startX, startY + Offset);
                Grosstotal = "Rs :" + foodSum + ".00" + " \r\n";

                graphics.DrawString("*** Food ***  " + Grosstotal, new Font(fontType, 12, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                Offset = Offset + 20;
                graphics.DrawString("----------------------------------\r\n", new Font("Courier New", 10), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
                // for loop for drink  
                for (int j = 0; j < drinkCounter; j++)
                {

                    mname = drink_m_name[j];
                    qty = drink_Qty[j];
                    rate = drink_rt[j];
                    amt = drink_amount[j];
                    if (mname != null)
                    {
                        drinkSum += double.Parse(amt.ToString());


                        if (mname.Length < 14)
                        {
                            while (mname.Length != 13)
                            {
                                mname += " ";
                            }
                        }
                        else
                        { mname = mname.Substring(0, 13); }



                        while (qty.Length <= 3)
                        {
                            qty = " " + qty;
                        }

                        while (rate.Length <= 4)
                        {
                            rate = " " + rate;
                        }
                        while (amt.Length <= 7)
                        {
                            amt = " " + amt;
                        }

                        Offset = Offset + 20;
                        //  graphics.DrawString("" + mname + "" + tem_qty + "" + rate + "" + amt + "\r\n", new Font(fontType, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                        graphics.DrawString("" + mname + "" + qty + "" + rate + "" + amt + "\r\n", new Font(fontType, itemNameFontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                    }
                }



                //Offset = Offset + 20;
                ////graphics.DrawString("----------------------------------\r\n", new Font("Courier New", 10), new SolidBrush(Color.Black), startX, startY + Offset);
                //  Grosstotal = "items:" + (i - 1) + "      Total: " + foodSum + ".00" + " \r\n";
                //drinkFlag = true;


                if (drinkCounter != 0) // if drink is not in bill
                {
                    Offset = Offset + 20;
                    //graphics.DrawString("----------------------------------\r\n", new Font("Courier New", 10), new SolidBrush(Color.Black), startX, startY + Offset);
                    Grosstotal = "Rs :" + drinkSum + ".00" + " \r\n";
                    graphics.DrawString("*** Drink *** " + Grosstotal, new Font(fontType, 12, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 20;

                    underLine = "---------------------------------------------";
                    graphics.DrawString(underLine, new Font(fontType, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                }

                Offset = Offset + 20;
                //graphics.DrawString("----------------------------------\r\n", new Font("Courier New", 10), new SolidBrush(Color.Black), startX, startY + Offset);
                Grosstotal = "" + (foodSum + drinkSum) + ".00" + " \r\n";
                graphics.DrawString("      Total :" + Grosstotal, new Font(fontType, GrandFontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                graphics.DrawString("\n", new Font("Courier New", fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;

                //  graphics.DrawString(Grosstotal, new Font(fontType, 12, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                cn.Close();

            }
            //test 
            //Tax apply
            string qry = "select * from tbl_option where grp='" + "Tax" + "' and status='Yes'";
            string value = "", value_1 = ""; ;
            cn.Open();
            SqlCommand cmd1 = new SqlCommand(qry, cn);
            SqlDataReader rd1 = cmd1.ExecuteReader();
            if (rd1.Read())
            {
                flag_grandtotal = true;
                value = rd1["value"].ToString();

                tax_amt = (float.Parse(value) * subtotal) / 100;
                tax = subtotal + tax_amt;

                graphics.DrawString("                    Tax: " + value + "%", new Font("Courier New", fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;


                //graphics.DrawString("                  Tax_Amount: " + tax + "%", new Font("Courier New", 12), new SolidBrush(Color.Black), startX, startY + Offset);
                //Offset = Offset + 20;
            }
            cn.Close();

            //discount apply
            cn.Open();
            SqlCommand discCmd = new SqlCommand("select discAmt from table_order where order_id='" + billid + "'", cn);
            SqlDataReader discRd = discCmd.ExecuteReader();
            while (discRd.Read())
            {
                disc_amt = float.Parse(discRd["discAmt"].ToString());
            }
            cn.Close();
            if (disc_amt != 0)
            {
                graphics.DrawString(MenuFormation("Discount:", "left", 25) + (disc_amt) + "  Rs", new Font("Courier New", discFontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;


                //test end
                //if (flag_grandtotal)
                //{
                grand_total = float.Parse(Grosstotal) - disc_amt;

                graphics.DrawString("      Grand Total: " + Math.Round(grand_total).ToString() + ".00", new Font("Courier New", GrandFontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
            }
            graphics.DrawString("\n ", new Font("Courier New", 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;

            graphics.DrawString(footer1, new Font("Courier New", 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString(footer2, new Font("Courier New", footer2Font_size, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;


            graphics.DrawString(companyNm, new Font("Courier New", cmpnyNameFont_size, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
        }


        public void totalBillPrint()
        {
            //Get Printer name here
            cn.Open();
            SqlCommand cmd = new SqlCommand("select value from tbl_option where grp='Bill Printer'", cn);
            try
            {
                if (cmd.ExecuteScalar().ToString() == "")
                    PrinterName = "0";
                else
                    PrinterName = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {

            }
            cn.Close();
            PrintDialog pd = new PrintDialog();
            pdoc = new PrintDocument();

            PrinterSettings ps = new PrinterSettings();
            Font font = new Font("Courier New", 12);


            PaperSize psize = new PaperSize("Custom", 100, 800);
            //ps.DefaultPageSettings.PaperSize = psize;



            pd.Document = pdoc;
            pd.Document.DefaultPageSettings.PaperSize = psize;
            //pdoc.DefaultPageSettings.PaperSize.Height =320;
            pdoc.DefaultPageSettings.PaperSize.Height = 820;

            pdoc.DefaultPageSettings.PaperSize.Width = 520;

            //Assign Printer name here

            PrinterSettings printer = new PrinterSettings();
            pdoc.PrinterSettings.PrinterName = (PrinterName);

            pdoc.PrintPage += new PrintPageEventHandler(BillPrint);  //test the new code 12-1-2014 by sagar


            pdoc.Print();
        }

        // total bill of combining all counter bills
        void BillPrint(object sender, PrintPageEventArgs e)
        {
            cn.Close();
            Database db = new Database();
            Graphics graphics = e.Graphics;
            Font font = new Font("Courier New", 10);
            string fontType = "Microsoft Sans Serif";
            float fontHeight = font.GetHeight();
            int startX = 0, add2Size = 0;

            int startY = 30;//30
            int Offset = 0;
            int fontSize = 9;
            subtotal = 0;
            //string date = DateTime.Now.ToShortDateString().ToString();
            string date = System.DateTime.Now.ToString("dd-MM-yyyy");
            cn.Open();
            string qury = "select * from tbl_receiptFormat";
            SqlCommand comnd = new SqlCommand(qury, cn);
            SqlDataReader reader123 = comnd.ExecuteReader();
            while (reader123.Read())
            {
                hotelNmfont_size = int.Parse(reader123["hotelNmFontSize"].ToString());
                taglineFont_size = int.Parse(reader123["taglineFontSize"].ToString());
                addressFont_size = int.Parse(reader123["addressFontSize"].ToString());
                mobileFont_size = int.Parse(reader123["mobileFontSize"].ToString());
                footer2Font_size = int.Parse(reader123["footer2FontSize"].ToString());
                cmpnyNameFont_size = int.Parse(reader123["companyNameFontSize"].ToString());
                itemNameFontSize = int.Parse(reader123["itemNmFontSize"].ToString());
                tblNoFontSize = int.Parse(reader123["tblNoFontSize"].ToString());
                billNoFontSize = int.Parse(reader123["billNoFontSize"].ToString());
                dateFontSize = int.Parse(reader123["dateFontSize"].ToString());
                totalFontSize = int.Parse(reader123["totalFontSize"].ToString());
                discFontSize = int.Parse(reader123["discFontSize"].ToString());
                GrandFontSize = int.Parse(reader123["GrandFontSize"].ToString());
                add2Size = int.Parse(reader123["AddressSize2"].ToString());
            }

            cn.Close();
            String underLine = "---------------------------------------------------------------------";
            get_ReceiptFormt();
            if (db.ChkDb_Value("select value from tbl_option where grp='PrintSize' and value='3 Inch'"))
            {
                graphics.DrawString("      " + hotelNam, new Font(fontType, hotelNmfont_size, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
                graphics.DrawString("    " + tagline, new Font(fontType, taglineFont_size, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
                graphics.DrawString(address, new Font(fontType, addressFont_size, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
                if (Address2 != "0")
                {
                    graphics.DrawString(Address2, new Font(fontType, add2Size, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 10;
                }
                graphics.DrawString("   " + mob, new Font(fontType, mobileFont_size, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 15;

                graphics.DrawString("---------------------------------------------------------------------\r", new Font(fontType, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;

                graphics.DrawString(" Bill No : " + this.billid + "   \r", new Font(fontType, tblNoFontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;

                graphics.DrawString(" Date : " + date + "\t\t Time : " + DateTime.Now.ToString("hh:mm:ss") + "\r", new Font(fontType, dateFontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 10;
                graphics.DrawString(underLine, new Font(fontType, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 10;
                graphics.DrawString(" Item Name                               Qty    Rate  Amt \r", new Font(fontType, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 10;
                graphics.DrawString(underLine, new Font(fontType, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

            }
            if (db.ChkDb_Value("select value from tbl_option where grp='PrintSize' and value='2 Inch'"))
            {
                graphics.DrawString(hotelNam, new Font(fontType, hotelNmfont_size, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
                graphics.DrawString("  " + tagline, new Font(fontType, taglineFont_size, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
                graphics.DrawString(address, new Font(fontType, addressFont_size, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
                if (Address2 != "0")
                {
                    graphics.DrawString(Address2, new Font(fontType, add2Size, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 10;
                }
                graphics.DrawString("   " + mob, new Font(fontType, mobileFont_size, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 15;

                graphics.DrawString("---------------------------------------------------------------------\r", new Font(fontType, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;

                graphics.DrawString("Bill No : " + this.billid + "   \r", new Font(fontType, tblNoFontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;

                graphics.DrawString(" Date : " + date + "    Time : " + DateTime.Now.ToString("hh:mm:ss") + "\r", new Font(fontType, dateFontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 10;
                graphics.DrawString(underLine, new Font(fontType, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 10;
                graphics.DrawString(" Item Name                   Qty    Rate  Amt \r", new Font(fontType, 7, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 10;
                graphics.DrawString(underLine, new Font(fontType, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

            }

            string qur = "SELECT menu.m_name as nm, sales_item.qty as qty, sales_item.rate as rt, sales_item.total_amount as amt,menu.category FROM menu INNER JOIN sales_item ON menu.menu_id = sales_item.menu_id WHERE (sales_item.order_id = '" + this.billid + "') ";//group by menu.m_name , sales_item.qty , sales_item.rate, sales_item.total_amount ,menu.category

            cn.Open();
            SqlCommand cmd = new SqlCommand(qur, cn);
            SqlDataReader rd = cmd.ExecuteReader();
            string tem_qty = "";
            int i = 1;
            string sr = i.ToString();
            string mname, qty, rate, amt;
            while (rd.Read() == true)
            {
                mname = rd["nm"].ToString();
                qty = rd["qty"].ToString();
                rate = rd["rt"].ToString();
                amt = rd["amt"].ToString();

                tem_qty = qty;

                subtotal += float.Parse(rd["amt"].ToString());
                if (mname.Length < 14)//20
                {
                    while (mname.Length != 14)
                    {
                        mname += " ";
                    }
                }
                else
                { mname = mname.Substring(0, 13); }



                while (tem_qty.Length <= 2)
                {
                    tem_qty = " " + tem_qty;
                }

                while (rate.Length <= 3)
                {
                    rate = " " + rate;
                }
                while (amt.Length <= 3)
                {
                    amt = " " + amt;
                }

                //  graphics.DrawString(" " + mname, new Font("Bharat01", 13), new SolidBrush(Color.Black), startX, startY + Offset);
                // graphics.DrawString("  " + mname + " " + tem_qty + "  " + rate + "  " + amt + "\r\n", new Font("Courier New", itemNameFontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                if (db.ChkDb_Value("select value from tbl_option where grp='PrintSize' and value='3 Inch'"))
                {
                    Offset = Offset + 20;
                    graphics.DrawString(" " + mname + "\r\n", new Font(fontType, itemNameFontSize, System.Drawing.FontStyle.Regular), new SolidBrush(Color.Black), startX, startY + Offset);
                    graphics.DrawString("                                                              " + tem_qty + "\r\n", new Font(fontType, itemNameFontSize, System.Drawing.FontStyle.Regular), new SolidBrush(Color.Black), startX, startY + Offset);
                    graphics.DrawString("                                                                         " + rate + "\r\n", new Font(fontType, itemNameFontSize, System.Drawing.FontStyle.Regular), new SolidBrush(Color.Black), startX, startY + Offset);
                    graphics.DrawString("                                                                                 " + amt + "\r\n", new Font(fontType, itemNameFontSize, System.Drawing.FontStyle.Regular), new SolidBrush(Color.Black), startX, startY + Offset);
                }
                if (db.ChkDb_Value("select value from tbl_option where grp='PrintSize' and value='2 Inch'"))
                {
                    Offset = Offset + 15;
                    graphics.DrawString("" + mname + "\r\n", new Font(fontType, itemNameFontSize, System.Drawing.FontStyle.Regular), new SolidBrush(Color.Black), startX, startY + Offset);

                    graphics.DrawString("                                                  " + tem_qty + "\r\n", new Font(fontType, itemNameFontSize, System.Drawing.FontStyle.Regular), new SolidBrush(Color.Black), startX, startY + Offset);
                    graphics.DrawString("                                                              " + rate + "\r\n", new Font(fontType, itemNameFontSize, System.Drawing.FontStyle.Regular), new SolidBrush(Color.Black), startX, startY + Offset);
                    graphics.DrawString("                                                                        " + amt + "\r\n", new Font(fontType, itemNameFontSize, System.Drawing.FontStyle.Regular), new SolidBrush(Color.Black), startX, startY + Offset);
                }
                //  graphics.DrawString(" " + mname + " " + tem_qty + "   " + rate + "   " + amt + "\r\n", new Font("Bharat01", 12, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                i++;

            }
            cn.Close();
            if (db.ChkDb_Value("select value from tbl_option where grp='PrintSize' and value='3 Inch'"))
            {
                Offset = Offset + 20;
                String Grosstotal = " items:" + (i - 1) + "            Total: " + subtotal + ".00" + " \r\n";


                Offset = Offset + 20;
                String underLin = "----------------------------------";
                graphics.DrawString(underLin, new Font(fontType, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;

                graphics.DrawString(Grosstotal, new Font(fontType, totalFontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
            }

            if (db.ChkDb_Value("select value from tbl_option where grp='PrintSize' and value='2 Inch'"))
            {
                Offset = Offset + 20;
                String Grosstotal = " items:" + (i - 1) + "        Total: " + subtotal + ".00" + " \r\n";


                Offset = Offset + 20;
                String underLin = "-----------------------------------------------------------------------";
                graphics.DrawString(underLin, new Font(fontType, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;

                graphics.DrawString(Grosstotal, new Font(fontType, totalFontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
            }
            string value = "", value_1 = "", getDisc = "", gstAmt = "", discper = "", TotNet = "";
            float tax;
            //Tax apply
            //Database db = new Database();
            string qry = "select * from tbl_option where grp='" + "POS GST" + "' and status='Yes'";
            gstAmt = "0";
            cn.Open();
            SqlCommand cmd1 = new SqlCommand(qry, cn);
            SqlDataReader rd1 = cmd1.ExecuteReader();
            if (rd1.Read())
            {
                value = rd1["value"].ToString();

                tax_amt = (float.Parse(value) / 2 * (subtotal)) / 100;
                tax = subtotal + tax_amt;
                //getDisc = Math.Round(disc_amt).ToString();

                gstAmt = Math.Round(tax_amt).ToString();
                // Offset = Offset + 20;  
                if (db.ChkDb_Value("select value from tbl_option where grp='PrintSize' and value='3 Inch'"))
                {
                    graphics.DrawString("                   CGST " + (float.Parse(value) / 2) + "% : " + gstAmt + ".00", new Font("Courier New", fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 20;
                    graphics.DrawString("                   SGST " + (float.Parse(value) / 2) + "% : " + gstAmt + ".00", new Font("Courier New", fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 20;
                    graphics.DrawString(" -------------------------------------------------------", new Font(fontType, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                }
                if (db.ChkDb_Value("select value from tbl_option where grp='PrintSize' and value='2 Inch'"))
                {
                    graphics.DrawString("           CGST " + (float.Parse(value) / 2) + "% : " + gstAmt + ".00", new Font("Courier New", 7, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 20;
                    graphics.DrawString("           SGST " + (float.Parse(value) / 2) + "% : " + gstAmt + ".00", new Font("Courier New", 7, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 20;
                    graphics.DrawString(" -------------------------------------------------------", new Font(fontType, fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                }
                Offset = Offset + 20;

                //graphics.DrawString("                  Tax_Amount: " + tax + "%", new Font("Courier New", 12), new SolidBrush(Color.Black), startX, startY + Offset);
                //Offset = Offset + 20;
            }
            cn.Close();

            grand_total = subtotal + float.Parse(gstAmt) * 2;//grand_total = float.Parse(Grosstotal) - disc_amt;
            if (db.ChkDb_Value("select value from tbl_option where grp='PrintSize' and value='3 Inch'"))
            {
                graphics.DrawString(" \t \t \t    Grand Total : " + Math.Round(grand_total).ToString(), new Font(fontType, GrandFontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 10;

                graphics.DrawString(footer1, new Font(fontType, 10, System.Drawing.FontStyle.Regular), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
                graphics.DrawString(" \t          " + footer2, new Font(fontType, footer2Font_size, System.Drawing.FontStyle.Regular), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
                graphics.DrawString(" \t        " + companyNm, new Font(fontType, cmpnyNameFont_size, System.Drawing.FontStyle.Regular), new SolidBrush(Color.Black), startX, startY + Offset);


            }
            if (db.ChkDb_Value("select value from tbl_option where grp='PrintSize' and value='2 Inch'"))
            {
                graphics.DrawString(" \t   Grand Total : " + Math.Round(grand_total).ToString(), new Font(fontType, GrandFontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 10;


                graphics.DrawString(footer1, new Font(fontType, 10, System.Drawing.FontStyle.Regular), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
                graphics.DrawString("          " + footer2, new Font(fontType, footer2Font_size, System.Drawing.FontStyle.Regular), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;


                graphics.DrawString("        " + companyNm, new Font(fontType, cmpnyNameFont_size, System.Drawing.FontStyle.Regular), new SolidBrush(Color.Black), startX, startY + Offset);

            }
            Offset = Offset + 20;
            cn.Close();
            // cn.Open();

        }

        //temp print formation 
        public void printToken(string getCounterNumber)
        {
            _getCounterNo = getCounterNumber;

            //Get Printer name here
            cn.Open();
            SqlCommand cmd = new SqlCommand("select value from tbl_option where grp='Bill Printer'", cn);
            try
            {
                if (cmd.ExecuteScalar().ToString() == "")
                    PrinterName = "0";
                else
                    PrinterName = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {

            }
            cn.Close();
            PrintDialog pd = new PrintDialog();
            pdoc = new PrintDocument();

            PrinterSettings ps = new PrinterSettings();
            Font font = new Font("Courier New", 12);


            PaperSize psize = new PaperSize("Custom", 100, 800);
            //ps.DefaultPageSettings.PaperSize = psize;



            pd.Document = pdoc;
            pd.Document.DefaultPageSettings.PaperSize = psize;
            //pdoc.DefaultPageSettings.PaperSize.Height =320;
            pdoc.DefaultPageSettings.PaperSize.Height = 820;

            pdoc.DefaultPageSettings.PaperSize.Width = 520;

            //Assign Printer name here

            PrinterSettings printer = new PrinterSettings();
            pdoc.PrinterSettings.PrinterName = (PrinterName);

            pdoc.PrintPage += new PrintPageEventHandler(token_PrintPage);  //test the new code 12-1-2014 by sagar


            pdoc.Print();

        }
        void token_PrintPage(object sender, PrintPageEventArgs e)
        {

            printReciptBYCounter(_getCounterNo, e);//send the counter name to the print
            //printTotalBill(_getCounterNo, e);
        }
        void printReciptBYCounter(string getCounterNo, PrintPageEventArgs e)
        {
            cn.Close();
            Graphics graphics = e.Graphics;
            Font font = new Font("Courier New", 10);

            float fontHeight = font.GetHeight();
            int startX = 0;

            int startY = 30;//30
            int Offset = 0;
            //  int height = Offset +40; 
            int fontSize = 14;
            subtotal = 0;

            //string date = DateTime.Now.ToShortDateString().ToString();

            string date = System.DateTime.Now.ToString("dd-MM-yyyy");

            get_ReceiptFormt();


            graphics.DrawString(" " + getCounterNo + " \r\n", new Font("Bharat01", 22, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("  \r\n", new Font("Bharat01", 22, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("o_id:" + this.billid + "  \r\n", new Font("Courier New", 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;

            graphics.DrawString("----------------------------------\r", new Font("Courier New", fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;


            graphics.DrawString(" Date :" + date + "  " + DateTime.Now.ToString("HH:mm:ss") + "\r", new Font("Courier New", 11, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            String underLine = "----------------------------------";
            graphics.DrawString(underLine, new Font("Courier New", fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;

            graphics.DrawString(" Item Name    Qty\r", new Font("Courier New", 12, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString(underLine, new Font("Courier New", fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            int i = 1;



            string qur = "SELECT menu.m_name as nm, sales_item.qty as qty, sales_item.rate as rt, sales_item.total_amount as amt,menu.category FROM menu INNER JOIN sales_item ON menu.menu_id = sales_item.menu_id WHERE (sales_item.order_id = '" + this.billid + "' and menu.category='" + getCounterNo + "') group by menu.m_name , sales_item.qty , sales_item.rate, sales_item.total_amount ,menu.category";

            cn.Open();
            SqlCommand cmd = new SqlCommand(qur, cn);
            SqlDataReader rd = cmd.ExecuteReader();
            string sr = i.ToString();
            string tem_qty = "";
            string mname, qty, rate, amt;
            while (rd.Read() == true)
            {
                mname = rd["nm"].ToString();
                qty = rd["qty"].ToString();
                rate = rd["rt"].ToString();
                amt = rd["amt"].ToString();

                tem_qty = qty;

                subtotal += float.Parse(rd["amt"].ToString());
                if (mname.Length < 20)
                {
                    while (mname.Length != 20)
                    {
                        mname += " ";
                    }
                }
                else
                { mname = mname.Substring(0, 19); mname += "     "; }



                while (tem_qty.Length <= 2)
                {
                    tem_qty = " " + tem_qty;
                }

                while (rate.Length <= 3)
                {
                    rate = " " + rate;
                }
                while (amt.Length <= 3)
                {
                    amt = " " + amt;
                }
                Offset = Offset + 30;
                graphics.DrawString(" " + mname, new Font("Bharat01", 15), new SolidBrush(Color.Black), startX, startY + Offset);
                graphics.DrawString("          " + tem_qty + "\r\n", new Font("Courier New", 15, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                i++;

            }
            //Offset = Offset + 20;
            // String Grosstotal = " items:" + (i - 1) + "            Total: " + subtotal + ".00" + " \r\n";

            graphics.DrawString("\n\r", new Font("Courier New", fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            Offset = Offset + 20;
            underLine = "----------------------------------";
            graphics.DrawString(underLine, new Font("Courier New", fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("\n\r", new Font("Courier New", fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            //graphics.DrawString(Grosstotal, new Font("Courier New", fontSize, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            //Offset = Offset + 20; 


            //graphics.DrawString("\n ", new Font("Courier New", 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            //Offset = Offset + 20;

            //graphics.DrawString(footer1, new Font("Courier New", 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            //Offset = Offset + 20;
            //graphics.DrawString(footer2, new Font("Courier New", 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            //Offset = Offset + 20; 

            //graphics.DrawString("     [c]Restrosoft Pune.", new Font("Courier New", 9, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            //Offset = Offset + 20;
            cn.Close();
            // cn.Open();
        }

        public bool ChkDb_Value(String query)
        {

            cn.Open();
            SqlCommand cmd = new SqlCommand(query, cn);
            SqlDataReader rd = cmd.ExecuteReader();

            if (rd.Read() == true)
            {
                return true;
            }
            else
            {
                return false;
            }


        }

        // Today Counter For Teklogik
        DataGridView dgvTodayCounter;
        public void print_todaysCounter(string dateFrm, string dateTo, DataGridView dgvTodayCnter)
        {
            datefrm = dateFrm;
            date_To = dateTo;
            dgvTodayCounter = dgvTodayCnter;
            //Get Printer name here
            cn.Open();
            SqlCommand cmd = new SqlCommand("select value from tbl_option where grp='Bill Printer'", cn);
            try
            {
                if (cmd.ExecuteScalar().ToString() == "")
                    PrinterName = "0";
                else
                    PrinterName = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {

            }
            cn.Close();
            PrintDialog pd = new PrintDialog();
            pdoc = new PrintDocument();

            PrinterSettings ps = new PrinterSettings();
            Font font = new Font("Courier New", 15);


            PaperSize psize = new PaperSize("Custom", 100, 500000);
            //ps.DefaultPageSettings.PaperSize = psize;



            pd.Document = pdoc;
            pd.Document.DefaultPageSettings.PaperSize = psize;
            //pdoc.DefaultPageSettings.PaperSize.Height =320;
            pdoc.DefaultPageSettings.PaperSize.Height = 500000;

            pdoc.DefaultPageSettings.PaperSize.Width = 520;
            //pdoc_PrintPage();
            //pdoc.PrintPage += new PrintPageEventHandler(pdoc_PrintPage);

            //Assign Printer name here

            PrinterSettings printer = new PrinterSettings();
            pdoc.PrinterSettings.PrinterName = (PrinterName);

            pdoc.PrintPage += new PrintPageEventHandler(today_counter); //******************************************************

            pdoc.Print();

        }



        public void today_counter(object sender, PrintPageEventArgs e)
        {
            // get_ReceiptFormt(); //get the receipt format from the db
            Database db = new Database();


            Graphics graphics = e.Graphics;
            string o_id, t_no, total;
            string fontName = "Microsoft Sans Serif";
            int c = 1;
            string date1 = db.getDbstatus_Value("select ddate from tbl_dayend_status");

            DateTime dt = Convert.ToDateTime(date1);
            string date = dt.ToString("MM/dd/yyyy");
            string qur = " SELECT   total_bill.order_id AS[Bill No.], table_order.t_id as [Table No], total_bill.Total_bill AS[Total Amount],table_order.discAmt AS [Discount Amt] FROM            total_bill INNER JOIN   table_order ON total_bill.order_id = table_order.order_id where datetime between '" + Convert.ToDateTime(datefrm).ToString("MM/dd/yyyy") + "' and '" + Convert.ToDateTime(date_To).ToString("MM/dd/yyyy") + "'";

            cn.Open();
            SqlCommand cmd = new SqlCommand(qur, cn);
            SqlDataReader rd = cmd.ExecuteReader();
            int i = 1;
            int startX = 0;
            int startY = 30;
            int Offset = 0;
            string dt1 = dt.ToString("dd-MM-yyyy");
            Offset = Offset + 20;
            if (db.ChkDb_Value("select value from tbl_option where grp='PrintSize' and value='3 Inch'"))
            {
                graphics.DrawString("            Todays Counter Report of\r\n", new Font(fontName, 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
                graphics.DrawString("             " + datefrm + " and " + date_To + " \r\n", new Font(fontName, 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);


                Offset = Offset + 20;
                graphics.DrawString("---------------------------------------------------------------------\r\n", new Font(fontName, 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
                graphics.DrawString(" Bill No        Table No         Total Amount \r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
                graphics.DrawString("---------------------------------------------------------------------\r\n", new Font(fontName, 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

            }
            if (db.ChkDb_Value("select value from tbl_option where grp='PrintSize' and value='2 Inch'"))
            {
                graphics.DrawString(" Todays Counter Report of\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
                graphics.DrawString("" + datefrm + " and " + date_To + " \r\n", new Font(fontName, 7, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);


                Offset = Offset + 20;
                graphics.DrawString("---------------------------------------------------------------------\r\n", new Font(fontName, 7, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
                graphics.DrawString(" Bill No      Table No      Total Amount \r\n", new Font(fontName, 7, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
                graphics.DrawString("---------------------------------------------------------------------\r\n", new Font(fontName, 7, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

            }
            string sr = i.ToString();

            while (rd.Read() == true)
            {
                o_id = rd["Bill No."].ToString();

                t_no = rd["Table No"].ToString();
                total = rd["Total Amount"].ToString();



                while (o_id.Length <= 4)
                {
                    o_id += " ";
                }

                while (t_no.Length <= 2)
                {
                    t_no += " ";
                }

                while (total.Length <= 4)
                {
                    total = " " + total;
                }

                Offset = Offset + 20;
                if (db.ChkDb_Value("select value from tbl_option where grp='PrintSize' and value='3 Inch'"))
                {
                    graphics.DrawString("     " + o_id + "                " + t_no + "                    " + total + "\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Regular), new SolidBrush(Color.Black), startX, startY + Offset);
                }
                if (db.ChkDb_Value("select value from tbl_option where grp='PrintSize' and value='2 Inch'"))
                {
                    graphics.DrawString("     " + o_id + "\r\n", new Font(fontName, 7, System.Drawing.FontStyle.Regular), new SolidBrush(Color.Black), startX, startY + Offset);
                    graphics.DrawString("                        " + t_no + "\r\n", new Font(fontName, 7, System.Drawing.FontStyle.Regular), new SolidBrush(Color.Black), startX, startY + Offset);
                    graphics.DrawString("                                           " + total + "\r\n", new Font(fontName, 7, System.Drawing.FontStyle.Regular), new SolidBrush(Color.Black), startX, startY + Offset);

                }
                i++;


            }

            Offset = Offset + 20;
            if (db.ChkDb_Value("select value from tbl_option where grp='PrintSize' and value='3 Inch'"))
            {
                graphics.DrawString("---------------------------------------------------------------------\r\n", new Font(fontName, 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;

                graphics.DrawString(" \t              Total : " + (this.GrandTot + this.discAmt - (this.Cgstamt * 2)) + "\r\n", new Font(fontName, 9, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
                graphics.DrawString("  \t        Discount : " + this.discAmt + "\r\n", new Font(fontName, 9, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
                graphics.DrawString(" \t              CGST : " + (this.Cgstamt) + "\r\n", new Font(fontName, 9, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
                graphics.DrawString("\t               SGST : " + (this.Sgstamt) + "\r\n", new Font(fontName, 9, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
                graphics.DrawString("     \t  Grand Total : " + this.GrandTot + "\r\n", new Font(fontName, 9, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                //second gridview
                Offset = Offset + 20;
                graphics.DrawString("---------------------------------------------------------------------\r\n", new Font(fontName, 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);



                Offset = Offset + 20;
                graphics.DrawString(" Groupwise Report  \r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                Offset = Offset + 20;
                graphics.DrawString("---------------------------------------------------------------------\r\n", new Font(fontName, 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            }

            if (db.ChkDb_Value("select value from tbl_option where grp='PrintSize' and value='2 Inch'"))
            {
                graphics.DrawString("---------------------------------------------------------------------\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;

                graphics.DrawString(" \t             Total : " + (this.GrandTot + this.discAmt - (this.Cgstamt * 2)) + "\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
                graphics.DrawString(" \t       Discount : " + this.discAmt + "\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
                graphics.DrawString("\t            CGST : " + (this.Cgstamt) + "\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
                graphics.DrawString("\t            SGST : " + (this.Sgstamt) + "\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
                graphics.DrawString("\t   Grand Total : " + this.GrandTot + "\r\n", new Font(fontName, 9, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                //second gridview
                Offset = Offset + 20;
                graphics.DrawString("---------------------------------------------------------------------\r\n", new Font(fontName, 7, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);



                Offset = Offset + 20;
                graphics.DrawString(" Groupwise Report  \r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                Offset = Offset + 20;
                graphics.DrawString("---------------------------------------------------------------------\r\n", new Font(fontName, 7, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            }

            int count = dgvTodayCounter.RowCount - 1;
            string grpName, amount;
            while (count >= 0)
            {
                // cn.Close();
                grpName = dgvTodayCounter.Rows[count].Cells[0].Value.ToString();
                amount = dgvTodayCounter.Rows[count].Cells[1].Value.ToString();

                if (grpName.Length <= 13)//13
                {
                    while (grpName.Length <= 13)
                    {
                        grpName += " ";
                    }
                }
                else
                { grpName = grpName.Substring(0, 13); }


                while (amount.Length <= 6)
                {
                    amount = " " + amount;
                }

                count--;

                Offset = Offset + 20;
                if (db.ChkDb_Value("select value from tbl_option where grp='PrintSize' and value='3 Inch'"))
                {
                    graphics.DrawString("   " + grpName + "  \r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                    graphics.DrawString("                                                   " + amount + "\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                }
                if (db.ChkDb_Value("select value from tbl_option where grp='PrintSize' and value='2 Inch'"))
                {
                    graphics.DrawString("   " + grpName + "  \r\n", new Font(fontName, 7, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                    graphics.DrawString("                                                 " + amount + "\r\n", new Font(fontName, 7, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                }


            }

            Offset = Offset + 20;
            graphics.DrawString("---------------------------------------------------------------------\r\n", new Font(fontName, 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

            cn.Close();

        }
        string todayCounterQuery = "";
        public void print_todaysCounterEpson(string counterQuery, string dateFrm, string dateTo, DataGridView dgvTodayCnter)
        {
            datefrm = dateFrm;
            date_To = dateTo;
            dgvTodayCounter = dgvTodayCnter;
            todayCounterQuery = counterQuery; //
            //Get Printer name here
            cn.Open();
            SqlCommand cmd = new SqlCommand("select value from tbl_option where grp='Bill Printer'", cn);
            try
            {
                if (cmd.ExecuteScalar().ToString() == "")
                    PrinterName = "0";
                else
                    PrinterName = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {

            }
            cn.Close();
            PrintDialog pd = new PrintDialog();
            pdoc = new PrintDocument();

            PrinterSettings ps = new PrinterSettings();
            Font font = new Font("Courier New", 15);


            // PaperSize psize = new PaperSize("Custom", 80000, 200);
            PaperSize psize = new PaperSize("Custom", 100, 15000000);
            //ps.DefaultPageSettings.PaperSize = psize;



            pd.Document = pdoc;
            pd.Document.DefaultPageSettings.PaperSize = psize;
            //pdoc.DefaultPageSettings.PaperSize.Height =320;
            pdoc.DefaultPageSettings.PaperSize.Height = 15000000;
            pdoc.DefaultPageSettings.PaperSize.Width = 520;
            PrinterSettings printer = new PrinterSettings();
            pdoc.PrinterSettings.PrinterName = (PrinterName);

            pdoc.PrintPage += new PrintPageEventHandler(today_counterEpson); //******************************************************

            pdoc.Print();

        }
        // Today Counters
        public void today_counterEpson(object sender, PrintPageEventArgs e)
        {
            // get_ReceiptFormt(); //get the receipt format from the db
            Database db = new Database();
            string get_suggestion = "";
            string fontName = "Microsoft Sans Serif";
            Graphics graphics = e.Graphics;
            string o_id, t_no, total;

            int c = 1;
            string date1 = db.getDbstatus_Value("select ddate from tbl_dayend_status");

            DateTime dt = Convert.ToDateTime(date1);
            string date = dt.ToString("MM/dd/yyyy");
            //string qur = " SELECT   total_bill.order_id AS[Bill No.], table_order.t_id as [Table No], total_bill.Total_bill AS[Total Amount] FROM            total_bill INNER JOIN   table_order ON total_bill.order_id = table_order.order_id where datetime='" + date + "'";


            cn.Open();
            SqlCommand cmd = new SqlCommand(todayCounterQuery, cn);//here chnges done for the counter in section wise
            SqlDataReader rd = cmd.ExecuteReader();
            int i = 1;
            int startX = 0;
            int startY = 30;
            int Offset = 0;
            string dt1 = dt.ToString("dd-MM-yyyy");

            Offset = Offset + 20;
            if (db.ChkDb_Value("select value from tbl_option where grp='PrintSize' and value='3 Inch'"))
            {
                graphics.DrawString("            Todays Counter Report of\r\n", new Font(fontName, 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
                graphics.DrawString("             " + datefrm + " and " + date_To + " \r\n", new Font(fontName, 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);


                Offset = Offset + 20;
                graphics.DrawString("---------------------------------------------------------------------\r\n", new Font(fontName, 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
                graphics.DrawString(" Bill No         Table No         Total Amount \r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
                graphics.DrawString("---------------------------------------------------------------------\r\n", new Font(fontName, 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            }
            if (db.ChkDb_Value("select value from tbl_option where grp='PrintSize' and value='2 Inch'"))
            {
                graphics.DrawString(" Todays Counter Report of\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
                graphics.DrawString("" + datefrm + " and " + date_To + " \r\n", new Font(fontName, 7, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);


                Offset = Offset + 20;
                graphics.DrawString("---------------------------------------------------------------------\r\n", new Font(fontName, 7, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
                graphics.DrawString(" Bill No      Table No      Total Amount \r\n", new Font(fontName, 7, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
                graphics.DrawString("---------------------------------------------------------------------\r\n", new Font(fontName, 7, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

            }
            string sr = i.ToString();

            while (rd.Read() == true)
            {
                o_id = rd["Bill No."].ToString();

                t_no = rd["Table No"].ToString();
                total = rd["Total Amount"].ToString();



                while (o_id.Length <= 4)
                {
                    o_id += " ";
                }

                while (t_no.Length <= 2)
                {
                    t_no += " ";
                }

                while (total.Length <= 4)
                {
                    total = " " + total;
                }


                if (db.ChkDb_Value("select value from tbl_option where grp='PrintSize' and value='3 Inch'"))
                {
                    Offset = Offset + 20;
                    graphics.DrawString("     " + o_id + "\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Regular), new SolidBrush(Color.Black), startX, startY + Offset);
                    graphics.DrawString("                                 " + t_no + "\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Regular), new SolidBrush(Color.Black), startX, startY + Offset);
                    graphics.DrawString("                                                               " + total + "\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Regular), new SolidBrush(Color.Black), startX, startY + Offset);
                }
                if (db.ChkDb_Value("select value from tbl_option where grp='PrintSize' and value='2 Inch'"))
                {
                    Offset = Offset + 15;
                    graphics.DrawString("     " + o_id + "\r\n", new Font(fontName, 7, System.Drawing.FontStyle.Regular), new SolidBrush(Color.Black), startX, startY + Offset);
                    graphics.DrawString("                        " + t_no + "\r\n", new Font(fontName, 7, System.Drawing.FontStyle.Regular), new SolidBrush(Color.Black), startX, startY + Offset);
                    graphics.DrawString("                                           " + total + "\r\n", new Font(fontName, 7, System.Drawing.FontStyle.Regular), new SolidBrush(Color.Black), startX, startY + Offset);

                }
                i++;


            }

            Offset = Offset + 20;
            graphics.DrawString("---------------------------------------------------------------------\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            if (grupwseflg)
            {
                if (db.ChkDb_Value("select value from tbl_option where grp='PrintSize' and value='3 Inch'"))
                {


                    graphics.DrawString(" \t              Total : " + (this.GrandTot + this.discAmt - (this.Cgstamt * 2)) + "\r\n", new Font(fontName, 9, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 20;
                    graphics.DrawString("  \t        Discount : " + this.discAmt + "\r\n", new Font(fontName, 9, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 20;
                    graphics.DrawString(" \t              CGST : " + (this.Cgstamt) + "\r\n", new Font(fontName, 9, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 20;
                    graphics.DrawString("\t               SGST : " + (this.Sgstamt) + "\r\n", new Font(fontName, 9, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 20;
                    graphics.DrawString("     \t  Grand Total : " + this.GrandTot + "\r\n", new Font(fontName, 9, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                    //second gridview
                    Offset = Offset + 20;
                    graphics.DrawString("---------------------------------------------------------------------\r\n", new Font(fontName, 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                }

                if (db.ChkDb_Value("select value from tbl_option where grp='PrintSize' and value='2 Inch'"))
                {


                    graphics.DrawString(" \t             Total : " + (this.GrandTot + this.discAmt - (this.Cgstamt * 2)) + "\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 20;
                    graphics.DrawString(" \t       Discount : " + this.discAmt + "\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 20;
                    graphics.DrawString("\t            CGST : " + (this.Cgstamt) + "\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 20;
                    graphics.DrawString("\t            SGST : " + (this.Sgstamt) + "\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 20;
                    graphics.DrawString("\t   Grand Total : " + this.GrandTot + "\r\n", new Font(fontName, 9, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                    //second gridview
                    Offset = Offset + 20;
                    graphics.DrawString("---------------------------------------------------------------------\r\n", new Font(fontName, 7, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                }
                Offset = Offset + 20;

                if (db.ChkDb_Value("select value from tbl_option where grp='PrintSize' and value='3 Inch'"))
                {
                    graphics.DrawString(" Groupwise Report  \r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                    Offset = Offset + 20;
                    graphics.DrawString("---------------------------------------------------------------------\r\n", new Font(fontName, 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                }
                if (db.ChkDb_Value("select value from tbl_option where grp='PrintSize' and value='2 Inch'"))
                {
                    graphics.DrawString(" Groupwise Report  \r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                    Offset = Offset + 20;
                    graphics.DrawString("---------------------------------------------------------------------\r\n", new Font(fontName, 7, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                }

                int count = dgvTodayCounter.RowCount - 1;
                string grpName, amount;
                while (count >= 0)
                {
                    // cn.Close();
                    grpName = dgvTodayCounter.Rows[count].Cells[0].Value.ToString();
                    amount = dgvTodayCounter.Rows[count].Cells[1].Value.ToString();

                    if (grpName.Length <= 13)//13
                    {
                        while (grpName.Length <= 13)
                        {
                            grpName += " ";
                        }
                    }
                    else
                    { grpName = grpName.Substring(0, 13); }


                    while (amount.Length <= 6)
                    {
                        amount = " " + amount;
                    }

                    count--;

                    Offset = Offset + 20;
                    if (db.ChkDb_Value("select value from tbl_option where grp='PrintSize' and value='3 Inch'"))
                    {
                        graphics.DrawString("   " + grpName + "  \r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                        graphics.DrawString("                                                   " + amount + "\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                    }
                    if (db.ChkDb_Value("select value from tbl_option where grp='PrintSize' and value='2 Inch'"))
                    {
                        graphics.DrawString("   " + grpName + "  \r\n", new Font(fontName, 7, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                        graphics.DrawString("                                                 " + amount + "\r\n", new Font(fontName, 7, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                    }

                }

                Offset = Offset + 20;
                graphics.DrawString("---------------------------------------------------------------------\r\n", new Font(fontName, 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

            }


            cn.Close();

        }

        //*** Expences Print
        public void print_Expences(string datefrom, string dateTo)
        {
            datefrm = datefrom;
            date_To = dateTo;
            //Get Printer name here
            cn.Open();
            SqlCommand cmd = new SqlCommand("select value from tbl_option where grp='Bill Printer'", cn);
            try
            {
                if (cmd.ExecuteScalar().ToString() == "")
                    PrinterName = "0";
                else
                    PrinterName = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {

            }
            cn.Close();
            PrintDialog pd = new PrintDialog();
            pdoc = new PrintDocument();

            PrinterSettings ps = new PrinterSettings();
            Font font = new Font("Courier New", 15);


            PaperSize psize = new PaperSize("Custom", 100, 500000);
            //ps.DefaultPageSettings.PaperSize = psize;



            pd.Document = pdoc;
            pd.Document.DefaultPageSettings.PaperSize = psize;
            //pdoc.DefaultPageSettings.PaperSize.Height =320;
            pdoc.DefaultPageSettings.PaperSize.Height = 500000;

            pdoc.DefaultPageSettings.PaperSize.Width = 520;
            //pdoc_PrintPage();
            //pdoc.PrintPage += new PrintPageEventHandler(pdoc_PrintPage);

            //Assign Printer name here

            PrinterSettings printer = new PrinterSettings();
            pdoc.PrinterSettings.PrinterName = (PrinterName);

            pdoc.PrintPage += new PrintPageEventHandler(Expences); //******************************************************

            pdoc.Print();

        }
        // Expences
        public void Expences(object sender, PrintPageEventArgs e)
        {
            // get_ReceiptFormt(); //get the receipt format from the db
            Database db = new Database();


            Graphics graphics = e.Graphics;
            string mName, qty, total;

            int c = 1;
            string date1 = db.getDbstatus_Value("select ddate from tbl_dayend_status");

            DateTime dt = Convert.ToDateTime(date1);
            string date = dt.ToString("MM/dd/yyyy");
            //string qur = " SELECT   total_bill.order_id AS[Bill No.], table_order.t_id as [Table No], total_bill.Total_bill AS[Total Amount] FROM            total_bill INNER JOIN   table_order ON total_bill.order_id = table_order.order_id where datetime between '" + dateFrm + "' and '" + date_To + "'";
            string qur = "select material_nm as Material_Name,qty as Qty,unit as Unit,amount as Amount,date as Date,remark As Remark from tbl_expenses where date between '" + datefrm + "' and '" + date_To + "'";

            cn.Open();
            SqlCommand cmd = new SqlCommand(qur, cn);
            SqlDataReader rd = cmd.ExecuteReader();
            int i = 1;
            int startX = 0;
            int startY = 30;
            int Offset = 0;
            string dt1 = dt.ToString("dd-MM-yyyy");

            Offset = Offset + 20;
            graphics.DrawString(" Expences Report of  \r\n", new Font("Courier New", 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString(" " + datefrm + " and " + date_To + " \r\n", new Font("Courier New", 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);


            Offset = Offset + 20;
            graphics.DrawString("------------------------\r\n", new Font("Courier New", 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString(" Expences   Total \r\n", new Font("Courier New", 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("-------------------------\r\n", new Font("Courier New", 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);


            string sr = i.ToString();

            while (rd.Read() == true)
            {
                mName = rd["Material_Name"].ToString();

                // qty = rd["Qty"].ToString();
                total = rd["Amount"].ToString();



                //while (mName.Length <=11)
                //{                
                //    mName += " ";

                //}
                if (mName.Length < 11)
                {
                    while (mName.Length != 10)
                    {
                        mName += " ";
                    }
                }
                else
                { mName = mName.Substring(0, 10); }
                //while (qty.Length <= 2)
                //{
                //    qty += " ";
                //}

                while (total.Length <= 4)
                {
                    total = " " + total;
                }

                Offset = Offset + 20;
                graphics.DrawString(" " + mName + " " + total + "\r\n", new Font("Courier New", 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                i++;


            }

            Offset = Offset + 20;
            graphics.DrawString("-------------------------\r\n", new Font("Courier New", 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("        Total=" + this.GrandTot + "\r\n", new Font("Courier New", 11, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            //Offset = Offset + 20;
            //graphics.DrawString("    " + get_suggestion, new Font("Courier New", 11, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

            cn.Close();

        }


        public void print_kot(DataGridView dgv)
        {
            koGridview = dgv;
            //Get Printer name here
            cn.Open();
            SqlCommand cmd = new SqlCommand("select value from tbl_option where grp='KOT Printer'", cn);
            try
            {
                if (cmd.ExecuteScalar().ToString() == "")
                    PrinterName = "0";
                else
                    PrinterName = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {

            }
            cn.Close();
            PrintDialog pd = new PrintDialog();
            pdoc = new PrintDocument();

            PrinterSettings ps = new PrinterSettings();
            Font font = new Font("Courier New", 15, System.Drawing.FontStyle.Bold);


            PaperSize psize = new PaperSize("Custom", 100, 500000);
            //ps.DefaultPageSettings.PaperSize = psize;



            pd.Document = pdoc;
            pd.Document.DefaultPageSettings.PaperSize = psize;
            //pdoc.DefaultPageSettings.PaperSize.Height =320;
            pdoc.DefaultPageSettings.PaperSize.Height = 500000;

            pdoc.DefaultPageSettings.PaperSize.Width = 520;
            //pdoc_PrintPage();
            //pdoc.PrintPage += new PrintPageEventHandler(pdoc_PrintPage);

            //Assign Printer name here

            PrinterSettings printer = new PrinterSettings();
            pdoc.PrinterSettings.PrinterName = (PrinterName);

            pdoc.PrintPage += new PrintPageEventHandler(kot);

            //DialogResult result = pd.ShowDialog();
            //if (result == DialogResult.OK)
            //{
            //    PrintPreviewDialog pp = new PrintPreviewDialog();
            //    pp.Document = pdoc;
            //    result = pp.ShowDialog();
            //    if (result == DialogResult.OK)
            //    {
            //        pdoc.Print();
            //    }
            //}
            pdoc.Print();

        }

        public void kot(object sender, PrintPageEventArgs e)
        {
            get_ReceiptFormt(); //get the receipt format from the db
            Database db = new Database();
            string get_suggestion = "";
            string fontName = "Microsoft Sans Serif";//Courier New
            Graphics graphics = e.Graphics;
            string mname, qty;
            //string billId = "";
            int c = 1;
            // LPrinter MyPrinter = new LPrinter();
            //string qur = "SELECT menu.m_name as nm, sales_item.qty as qty FROM menu INNER JOIN sales_item ON menu.menu_id = sales_item.menu_id WHERE (sales_item.order_id = '" + billId + "')";
            //SqlConnection cn = new SqlConnection();
            string kot_id = db.getDb_Value("select max(kot_id) from sales_item where order_id='" + billid + "'").ToString();
            string qur = " SELECT     sales_item.menu_id, sales_item.qty, menu.m_name, sales_item.w_id, sales_item.order_id FROM         sales_item INNER JOIN     menu ON sales_item.menu_id = menu.menu_id WHERE sales_item.order_id = '" + billid + "' and kot_id='" + kot_id + "'";

            get_suggestion = db.getDbstatus_Value("select description from tbl_orderSuggestion where kot_id='" + kot_id + "'").ToString();

            cn.Open();
            SqlCommand cmd = new SqlCommand(qur, cn);
            SqlDataReader rd = cmd.ExecuteReader();
            int i = 1;
            int startX = 0;
            int startY = 30;
            int Offset = 0;
            if (cancelKOTFlag)
            {
                Offset = Offset + 20;
                if (db.ChkDb_Value("select value from tbl_option where grp='PrintSize' and value='3 Inch'"))
                {
                    graphics.DrawString(" X ", new Font(fontName, 60, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Red), startX, startY + Offset);
                    Offset = Offset + 50;
                }

                if (db.ChkDb_Value("select value from tbl_option where grp='PrintSize' and value='2 Inch'"))
                {
                    graphics.DrawString(" X ", new Font(fontName, 50, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Red), startX, startY + Offset);
                    Offset = Offset + 50;
                }
                Offset = Offset + 20;
            }

            if (db.ChkDb_Value("select value from tbl_option where grp='PrintSize' and value='3 Inch'"))
            {
                graphics.DrawString("  Table NO : " + this.tableno + "\t\t Bill No : " + this.billid + " \r\n", new Font(fontName, 9, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                Offset = Offset + 20;
                graphics.DrawString("  Date.:" + System.DateTime.Now.ToString("dd/MM/yyyy") + "\t Time : " + System.DateTime.Now.ToString("hh:mm:ss") + " \r\n", new Font(fontName, 9, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                Offset = Offset + 20;
                graphics.DrawString("  Waiter Name:" + this.wname + "\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                Offset = Offset + 15;
                graphics.DrawString("---------------------------------------------------------------------\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                Offset = Offset + 10;
                graphics.DrawString("   Item Name                                                   Qty  \r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                Offset = Offset + 10;
                graphics.DrawString("----------------------------------------------------------------------\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            }
            //******************** For 2 inch************************************
            if (db.ChkDb_Value("select value from tbl_option where grp='PrintSize' and value='2 Inch'"))
            {
                graphics.DrawString(" Table NO :" + this.tableno + "          Bill No:" + this.billid + " \r\n", new Font(fontName, 7, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                Offset = Offset + 20;
                graphics.DrawString(" Date :" + System.DateTime.Now.ToString("dd/MM/yyyy") + "    Time : " + System.DateTime.Now.ToString("hh:mm:ss") + " \r\n", new Font(fontName, 7, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                Offset = Offset + 20;
                graphics.DrawString(" Waiter Name : " + this.wname + "\r\n", new Font(fontName, 7, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);


                Offset = Offset + 15;
                graphics.DrawString("---------------------------------------------------------------------\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 10;
                graphics.DrawString(" Item Name                                Qty  \r\n", new Font(fontName, 7, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 10;
                graphics.DrawString("---------------------------------------------------------------------\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

            }

            string sr = i.ToString();


            int count = koGridview.RowCount - 1;

            while (count >= 0)
            {
                // cn.Close();
                if (tokenKot)
                {
                    mname = koGridview.Rows[count].Cells[2].Value.ToString();
                    qty = koGridview.Rows[count].Cells[4].Value.ToString();
                }
                else
                {
                    mname = koGridview.Rows[count].Cells[1].Value.ToString();
                    qty = koGridview.Rows[count].Cells[2].Value.ToString();
                }

                if (mname.Length <= 40)//13
                {
                    while (mname.Length <= 40)
                    {
                        mname += " ";
                    }
                }
                else
                { mname = mname.Substring(0, 40); }


                while (qty.Length < 3)
                {
                    qty = " " + qty;
                }

                Offset = Offset + 20;
                if (db.ChkDb_Value("select value from tbl_option where grp='PrintSize' and value='3 Inch'"))
                {
                    graphics.DrawString("   " + mname + "\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Regular), new SolidBrush(Color.Black), startX, startY + Offset);

                    graphics.DrawString("                                                                                " + qty + "\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Regular), new SolidBrush(Color.Black), startX, startY + Offset);
                }
                //******************** For 2 inch************************************
                if (db.ChkDb_Value("select value from tbl_option where grp='PrintSize' and value='2 Inch'"))
                {
                    graphics.DrawString(" " + mname + "\r\n", new Font(fontName, 6, System.Drawing.FontStyle.Regular), new SolidBrush(Color.Black), startX, startY + Offset);

                    graphics.DrawString("                                                              " + qty + "\r\n", new Font(fontName, 6, System.Drawing.FontStyle.Regular), new SolidBrush(Color.Black), startX, startY + Offset);
                }
                // i++;

                count--;
            }

            Offset = Offset + 10;
            if (db.ChkDb_Value("select value from tbl_option where grp='PrintSize' and value='3 Inch'"))
            {
                graphics.DrawString("----------------------------------------------------------------------\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
                graphics.DrawString("  Note:\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
                graphics.DrawString("    " + get_suggestion, new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            }
            //******************** For 2 inch************************************
            if (db.ChkDb_Value("select value from tbl_option where grp='PrintSize' and value='2 Inch'"))
            {
                graphics.DrawString("---------------------------------------------------------------------\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
                graphics.DrawString("Note:\r\n", new Font(fontName, 7, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
                graphics.DrawString("   " + get_suggestion, new Font("Courier New", 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            }
            cn.Close();

        }

        public string menuQry = "", fromDate, toDate, categoryType;
        public void print_report(string qry, string grandTot, string dateFrm, string dateTo, string category)
        {
            fromDate = dateFrm;
            toDate = dateTo;
            menuQry = qry;
            GrandTot = float.Parse(grandTot.ToString());
            categoryType = category;
            //Get Printer name here
            cn.Open();
            SqlCommand cmd = new SqlCommand("select value from tbl_option where grp='Bill Printer'", cn);
            try
            {
                if (cmd.ExecuteScalar().ToString() == "")
                    PrinterName = "0";
                else
                    PrinterName = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {

            }
            cn.Close();
            PrintDialog pd = new PrintDialog();
            pdoc = new PrintDocument();

            PrinterSettings ps = new PrinterSettings();
            Font font = new Font("Courier New", 12, System.Drawing.FontStyle.Bold);


            PaperSize psize = new PaperSize("Custom", 100, 5000000);

            //ps.DefaultPageSettings.PaperSize = psize;

            rptDateFrom = date1; // get date
            rptDateTO = date2;

            pd.Document = pdoc;
            pd.Document.DefaultPageSettings.PaperSize = psize;
            //pdoc.DefaultPageSettings.PaperSize.Height =320;
            pdoc.DefaultPageSettings.PaperSize.Height = 5000000;

            pdoc.DefaultPageSettings.PaperSize.Width = 520;
            //pdoc_PrintPage();
            //pdoc.PrintPage += new PrintPageEventHandler(pdoc_PrintPage);

            //Assign Printer name here

            PrinterSettings printer = new PrinterSettings();
            pdoc.PrinterSettings.PrinterName = (PrinterName);
            pdoc.PrintPage += new PrintPageEventHandler(report);


            pdoc.Print();

        }

        public void report(object sender, PrintPageEventArgs e)
        {
            get_ReceiptFormt(); //get the receipt format from the db
            Database db = new Database();
            string get_suggestion = "";
            string fontName = "Microsoft Sans Serif";
            Graphics graphics = e.Graphics;
            string mname, qty, total;
            db.cnopen();
            SqlCommand cmd = new SqlCommand(menuQry, cn);
            cn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            int i = 1;
            int startX = 0;
            int startY = 30;
            int Offset = 0;
            if (db.ChkDb_Value("select value from tbl_option where grp='PrintSize' and value='3 Inch'"))
            {
                graphics.DrawString("\t  Sales Report\r\n", new Font(fontName, 13, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
                graphics.DrawString(" \t\t Item Wise\r\n", new Font(fontName, 9, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                Offset = Offset + 20;
                graphics.DrawString("      Category : " + categoryType + "\t Time : " + System.DateTime.Now.ToString("hh:mm:ss") + "\r\n", new Font(fontName, 9, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                Offset = Offset + 20;
                graphics.DrawString("   Date From : " + fromDate + "    To : " + toDate + "\r\n", new Font(fontName, 9, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);


                Offset = Offset + 20;
                graphics.DrawString("---------------------------------------------------------------------\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                Offset = Offset + 10;
                graphics.DrawString(" Item Name                                            Qty     Amt \r\n", new Font(fontName, 8, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 10;
                graphics.DrawString("---------------------------------------------------------------------\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

            }
            if (db.ChkDb_Value("select value from tbl_option where grp='PrintSize' and value='2 Inch'"))
            {
                graphics.DrawString("  Sales Report\r\n", new Font(fontName, 9, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 15;
                graphics.DrawString("    Item Wise\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                Offset = Offset + 15;
                graphics.DrawString(" Category : " + categoryType + "   Time : " + System.DateTime.Now.ToString("hh:mm:ss") + "\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                Offset = Offset + 20;
                graphics.DrawString("Date:" + fromDate + "  To:" + toDate + "\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);


                Offset = Offset + 15;
                graphics.DrawString("---------------------------------------------------------------------\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                Offset = Offset + 10;
                graphics.DrawString(" Item Name               Qty     Amt \r\n", new Font(fontName, 8, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 10;
                graphics.DrawString("---------------------------------------------------------------------\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

            }

            string sr = i.ToString();

            while (rd.Read() == true)
            {
                mname = rd["Menu Name"].ToString();
                qty = rd["qty"].ToString();
                total = rd["Amount"].ToString();

                if (mname.Length <= 44)
                {
                    while (mname.Length < 44)
                    {
                        mname += " ";
                    }
                }
                else
                { mname = mname.Substring(0, 44); }


                while (qty.Length <= 3)
                {
                    qty = " " + qty;
                }
                while (total.Length <= 3)
                {
                    total = " " + total;
                }
                Offset = Offset + 15;
                if (db.ChkDb_Value("select value from tbl_option where grp='PrintSize' and value='3 Inch'"))
                {
                    graphics.DrawString(" " + mname + "\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Regular), new SolidBrush(Color.Black), startX, startY + Offset);
                    graphics.DrawString("                                                                     " + qty + "\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Regular), new SolidBrush(Color.Black), startX, startY + Offset);
                    graphics.DrawString("                                                                               " + total + "\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Regular), new SolidBrush(Color.Black), startX, startY + Offset);
                }
                if (db.ChkDb_Value("select value from tbl_option where grp='PrintSize' and value='2 Inch'"))
                {
                    graphics.DrawString(" " + mname + "\r\n", new Font(fontName, 6, System.Drawing.FontStyle.Regular), new SolidBrush(Color.Black), startX, startY + Offset);
                    graphics.DrawString("                                                 " + qty + "\r\n", new Font(fontName, 6, System.Drawing.FontStyle.Regular), new SolidBrush(Color.Black), startX, startY + Offset);
                    graphics.DrawString("                                                                  " + total + "\r\n", new Font(fontName, 6, System.Drawing.FontStyle.Regular), new SolidBrush(Color.Black), startX, startY + Offset);
                }
                i++;


            }

            Offset = Offset + 20;
            graphics.DrawString("---------------------------------------------------------------------\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            //Offset = Offset + 20;
            //graphics.DrawString("         Total:   " + (discAmt + TotalAmt) + "\r\n", new Font("Courier New", 11, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

            //if (!insentiveFlag)
            //{
            //    Offset = Offset + 20;
            //    graphics.DrawString("      Disc.:   " + this.discAmt + "\r\n", new Font("Courier New", 11, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

            //    Offset = Offset + 20;
            //    graphics.DrawString(" Grand Total:  " + this.TotalAmt + "\r\n", new Font("Courier New", 11, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            //}
            cn.Close();

        }


        //Sale Report for printer Epson
        string menudtlqry = "", frmDt, ToDt;
        public void print_reportEpson(string menuqry, string dtfrm, string dtto)
        {
            frmDt = dtfrm;
            ToDt = dtto;
            //Get Printer name here
            menudtlqry = menuqry;
            cn.Open();
            SqlCommand cmd = new SqlCommand("select value from tbl_option where grp='Bill Printer'", cn);
            try
            {
                if (cmd.ExecuteScalar().ToString() == "")
                    PrinterName = "0";
                else
                    PrinterName = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {

            }
            cn.Close();
            PrintDialog pd = new PrintDialog();
            pdoc = new PrintDocument();

            PrinterSettings ps = new PrinterSettings();
            Font font = new Font("Courier New", 12, System.Drawing.FontStyle.Bold);


            PaperSize psize = new PaperSize("Custom", 100, 15000000);

            //ps.DefaultPageSettings.PaperSize = psize;

            rptDateFrom = date1; // get date
            rptDateTO = date2;

            pd.Document = pdoc;
            pd.Document.DefaultPageSettings.PaperSize = psize;

            pdoc.DefaultPageSettings.PaperSize.Height = 15000000;

            pdoc.DefaultPageSettings.PaperSize.Width = 520;


            //Assign Printer name here

            PrinterSettings printer = new PrinterSettings();
            pdoc.PrinterSettings.PrinterName = (PrinterName);

            pdoc.PrintPage += new PrintPageEventHandler(reportMenuDetail);


            pdoc.Print();

        }

        public void reportMenuDetail(object sender, PrintPageEventArgs e)
        {
            get_ReceiptFormt(); //get the receipt format from the db
            Database db = new Database();
            string get_suggestion = "";
            string fontName = "Microsoft Sans Serif";
            double totamt = 0;
            Graphics graphics = e.Graphics;
            string mname, qty, salestotal;
            //string billId = "";
            int c = 1;
            //string qur = "SELECT   menu.m_name as Menu_Name, SUM(sales_item.qty) AS Qty ,SUM(sales_item.total_amount) as Total FROM sales_item INNER JOIN  menu ON sales_item.menu_id = menu.menu_id INNER JOIN   total_bill ON sales_item.order_id = total_bill.order_id WHERE (total_bill.datetime >= '" + rptDateFrom + "' and total_bill.datetime <= '" + rptDateTO + "') GROUP BY menu.m_name";


            db.cnopen();
            SqlCommand cmd = new SqlCommand(menudtlqry, cn);
            cn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            int i = 1;
            int startX = 0;
            int startY = 30;
            int Offset = 0;

            if (db.ChkDb_Value("select value from tbl_option where grp='PrintSize' and value='3 Inch'"))
            {
                graphics.DrawString("                 Sales Report Item Wise\r\n", new Font(fontName, 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
                //graphics.DrawString("                           Item Wise\r\n", new Font(fontName, 9, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                //Offset = Offset + 20;

                Offset = Offset + 20;
                graphics.DrawString("             " + frmDt + " and " + ToDt + " \r\n", new Font(fontName, 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                Offset = Offset + 20;
                graphics.DrawString("---------------------------------------------------------------------\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                Offset = Offset + 20;
                graphics.DrawString(" Item Name                                Qty               Total\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
                graphics.DrawString("---------------------------------------------------------------------\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            }
            if (db.ChkDb_Value("select value from tbl_option where grp='PrintSize' and value='2 Inch'"))
            {
                graphics.DrawString("  Sales Report\r\n", new Font(fontName, 9, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                Offset = Offset + 20;
                graphics.DrawString("Date:" + fromDate + "  To:" + toDate + "\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);


                Offset = Offset + 15;
                graphics.DrawString("---------------------------------------------------------------------\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                Offset = Offset + 10;
                graphics.DrawString(" Item Name               Qty     Amt \r\n", new Font(fontName, 8, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 10;
                graphics.DrawString("---------------------------------------------------------------------\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

            }
            string sr = i.ToString();

            while (rd.Read() == true)
            {
                mname = rd["Menu_Name"].ToString();
                qty = rd["qty"].ToString();
                salestotal = rd["Total"].ToString();

                if (mname.Length <= 40)
                {
                    while (mname.Length < 40)
                    {
                        mname += " ";
                    }
                }
                else
                { mname = mname.Substring(0, 40); }


                while (qty.Length <= 3)
                {
                    qty = " " + qty;
                }

                while (salestotal.Length <= 3)
                {
                    salestotal = " " + salestotal;
                }
                totamt += double.Parse(salestotal);

                if (db.ChkDb_Value("select value from tbl_option where grp='PrintSize' and value='3 Inch'"))
                {
                    Offset = Offset + 20;
                    graphics.DrawString("   " + mname + "\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Regular), new SolidBrush(Color.Black), startX, startY + Offset);
                    graphics.DrawString("                                                         " + qty + "\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Regular), new SolidBrush(Color.Black), startX, startY + Offset);
                    graphics.DrawString("                                                                          " + salestotal + "\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Regular), new SolidBrush(Color.Black), startX, startY + Offset);
                }
                if (db.ChkDb_Value("select value from tbl_option where grp='PrintSize' and value='2 Inch'"))
                {
                    Offset = Offset + 15;
                    graphics.DrawString(" " + mname + "\r\n", new Font(fontName, 6, System.Drawing.FontStyle.Regular), new SolidBrush(Color.Black), startX, startY + Offset);
                    graphics.DrawString("                                                 " + qty + "\r\n", new Font(fontName, 6, System.Drawing.FontStyle.Regular), new SolidBrush(Color.Black), startX, startY + Offset);
                    graphics.DrawString("                                                                  " + salestotal + "\r\n", new Font(fontName, 6, System.Drawing.FontStyle.Regular), new SolidBrush(Color.Black), startX, startY + Offset);
                }
                i++;


            }
            if (flgSales)
            {
                if (db.ChkDb_Value("select value from tbl_option where grp='PrintSize' and value='3 Inch'"))
                {
                    Offset = Offset + 20;
                    graphics.DrawString("---------------------------------------------------------------------\r\n", new Font(fontName, 9), new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 20;
                    graphics.DrawString("\t\t    Total=" + totamt + "\r\n", new Font(fontName, 9, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                }
                if (db.ChkDb_Value("select value from tbl_option where grp='PrintSize' and value='3 Inch'"))
                {
                    Offset = Offset + 15;
                    graphics.DrawString("---------------------------------------------------------------------\r\n", new Font(fontName, 9), new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 15;
                    graphics.DrawString("         Total=" + totamt + "\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                }
            }
            cn.Close();

        }



        string dtfrm, dtTo;
        public void printCategoryWiseReport(string datefrom, string dateTo)
        {
            dtfrm = datefrom;
            dtTo = dateTo;
            cn.Open();
            SqlCommand cmd = new SqlCommand("select value from tbl_option where grp='KOT Printer'", cn);
            try
            {
                if (cmd.ExecuteScalar().ToString() == "")
                    PrinterName = "0";
                else
                    PrinterName = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {

            }
            cn.Close();

            PrintDialog pd = new PrintDialog();
            pdoc = new PrintDocument();

            PrinterSettings ps = new PrinterSettings();
            Font font = new Font("Courier New", 12, System.Drawing.FontStyle.Bold);


            PaperSize psize = new PaperSize("Custom", 100, 5000000);

            //ps.DefaultPageSettings.PaperSize = psize;

            rptDateFrom = date1; // get date
            rptDateTO = date2;

            pd.Document = pdoc;
            pd.Document.DefaultPageSettings.PaperSize = psize;
            //pdoc.DefaultPageSettings.PaperSize.Height =320;
            pdoc.DefaultPageSettings.PaperSize.Height = 5000000;

            pdoc.DefaultPageSettings.PaperSize.Width = 520;

            //Assign Printer name here

            PrinterSettings printer = new PrinterSettings();
            pdoc.PrinterSettings.PrinterName = (PrinterName);
            pdoc.PrintPage += new PrintPageEventHandler(categorywiseRpt);



            pdoc.Print();

        }



        public void categorywiseRpt(object sender, PrintPageEventArgs e)
        {
            get_ReceiptFormt(); //get the receipt format from the db
            Database db = new Database();
            string get_suggestion = "";

            Graphics graphics = e.Graphics;
            string mname, qty, total;
            //string billId = "";
            int c = 1;
            string Grosstotal = "";
            // string qur = "SELECT   menu.m_name as Menu_Name, SUM(sales_item.qty) AS Qty FROM sales_item INNER JOIN  menu ON sales_item.menu_id = menu.menu_id INNER JOIN   total_bill ON sales_item.order_id = total_bill.order_id WHERE (total_bill.datetime >= '"+ rptDateFrom+"' and total_bill.datetime <= '" + rptDateTO + "') GROUP BY menu.m_name";

            cn.Open();
            string query = "select ddate from tbl_dayend_status";
            string str;
            SqlCommand cmd3 = new SqlCommand(query, cn);


            if (cmd3.ExecuteScalar().ToString() == "")
                str = "0";
            else
                str = cmd3.ExecuteScalar().ToString();

            cn.Close();


            string qry = "SELECT        menu.m_name AS Menu_Name, SUM(sales_item.qty) AS Qty, SUM(sales_item.total_amount) AS Amount,category,FoodSection FROM            sales_item INNER JOIN    menu ON sales_item.menu_id = menu.menu_id INNER JOIN                          total_bill ON sales_item.order_id = total_bill.order_id INNER JOIN  category ON menu.category = category.cat_name where total_bill.datetime between '" + Convert.ToDateTime(dtfrm).ToString("MM/dd/yyyy") + "' and '" + Convert.ToDateTime(dtTo).ToString("MM/dd/yyyy") + "' GROUP BY menu.m_name,category,FoodSection ORDER BY menu.category";//where category.grpHeader='"+Grpheader+"' 


            db.cnopen();
            SqlCommand cmd = new SqlCommand(qry, cn);
            cn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            int i = 1;
            int startX = 0;
            int startY = 30;
            int Offset = 0;
            string rate, amt;
            int categoryCounter = 0, counter = 0;


            string[] Menucategory = new string[500];
            string[] menuItem_Mname = new string[500];
            string[] menuItem_qty = new string[500];
            string[] menuItem_amt = new string[500];
            string[] FoodSection = new string[900];
            string[] count = new string[500];


            graphics.DrawString("    CategoryWise Report\r\n", new Font("Courier New", 11, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;

            graphics.DrawString(" Date From :" + dtfrm + "\r", new Font("Courier New", 11, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString(" To:" + dtTo + "\r", new Font("Courier New", 11, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;

            graphics.DrawString(" " + DateTime.Now.ToString("hh:mm:ss") + "\r", new Font("Courier New", 11, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;


            Offset = Offset + 20;
            graphics.DrawString("---------------------------------------\r\n", new Font("Courier New", 8, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);



            Offset = Offset + 20;
            graphics.DrawString(" Item Name                  Qty  Amount \r\n", new Font("Courier New", 8, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("----------------------------------------\r\n", new Font("Courier New", 8, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

            //Offset = Offset + 20;
            //graphics.DrawString("" + Grpheader + "\r\n", new Font("Courier New", 12, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);


            string sr = i.ToString();

            while (rd.Read() == true)
            {
                mname = rd["Menu_Name"].ToString().Trim();
                qty = rd["qty"].ToString();

                amt = rd["Amount"].ToString();


                Menucategory[categoryCounter] = rd["category"].ToString();

                menuItem_Mname[categoryCounter] = rd["Menu_Name"].ToString();
                menuItem_qty[categoryCounter] = rd["qty"].ToString();

                menuItem_amt[categoryCounter] = rd["Amount"].ToString();
                FoodSection[categoryCounter] = rd["FoodSection"].ToString();
                // Menucounter++;


                categoryCounter++;
                i++;

                counter++;
            }



            // Offset = Offset + 20;
            //graphics.DrawString(""+Menucategory[categoryCounter]+" \r\n", new Font("Courier New", 12, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            //Offset = Offset + 20;

            string[] distinctCategory = Menucategory.Distinct().ToArray();
            //  string[] distinctFoodSection = FoodSection.Distinct().ToArray();

            int countTotalCategory = distinctCategory.Count();


            string categoryWiseMenu = "", foodSection = "";


            for (int k = 0; k < countTotalCategory - 1; k++)
            {
                // i = 0;

                Offset = Offset + 20;

                foodSection = db.getDbstatus_Value("select FoodSection from category where cat_name='" + distinctCategory[k] + "'");

                graphics.DrawString("" + foodSection + " \r\n", new Font("Courier New", 12, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;

                graphics.DrawString("" + distinctCategory[k] + " \r\n", new Font("Courier New", 12, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;



                categoryWiseMenu = "SELECT        menu.m_name AS Menu_Name, SUM(sales_item.qty) AS Qty, SUM(sales_item.total_amount) AS Amount,category FROM            sales_item INNER JOIN    menu ON sales_item.menu_id = menu.menu_id INNER JOIN                          total_bill ON sales_item.order_id = total_bill.order_id where category='" + distinctCategory[k] + "' and total_bill.datetime between '" + Convert.ToDateTime(dtfrm).ToString("MM/dd/yyyy") + "' and '" + Convert.ToDateTime(dtTo).ToString("MM/dd/yyyy") + "' GROUP BY menu.m_name,category ORDER BY menu.category";//where category.grpHeader='"+Grpheader+"' 


                db.cnopen();
                SqlCommand sqlcmd = new SqlCommand(categoryWiseMenu, db.cn);
                // cn.Open();
                SqlDataReader sqlreader = sqlcmd.ExecuteReader();
                totalAmt = 0;
                while (sqlreader.Read())
                {

                    mname = sqlreader["Menu_Name"].ToString(); //menuItem_Mname[k];
                    qty = sqlreader["Qty"].ToString();  //menuItem_qty[k];
                    //rate = food_rt[k];
                    amt = sqlreader["Amount"].ToString(); //menuItem_amt[k];



                    if (mname != null)
                    {
                        totalAmt += double.Parse(amt.ToString());
                        grandTotal += double.Parse(amt.ToString());

                        if (mname.Length < 24)
                        {
                            while (mname.Length != 23)
                            {
                                mname += " ";
                            }
                        }
                        else
                        { mname = mname.Substring(0, 23); }



                        while (qty.Length <= 3)
                        {
                            qty = " " + qty;
                        }

                        //while (rate.Length <= 4)
                        //{
                        //    rate = " " + rate;
                        //}
                        while (amt.Length <= 7)
                        {
                            amt = " " + amt;
                        }


                        Offset = Offset + 20;
                        graphics.DrawString("" + mname + "" + qty + "" + amt + "\r\n", new Font("Courier New", 9, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                        // Offset = Offset + 20;


                    }

                    // i++;
                }

                Offset = Offset + 20;
                graphics.DrawString("----------------------------------\r\n", new Font("Courier New", 10), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
                Grosstotal = "Rs :" + totalAmt + ".00" + " \r\n";

                graphics.DrawString("Total Amount          " + Grosstotal, new Font("Courier New", 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;

                graphics.DrawString("----------------------------------\r\n", new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

            }





            Offset = Offset + 20;
            //graphics.DrawString("----------------------------------\r\n", new Font("Courier New", 10), new SolidBrush(Color.Black), startX, startY + Offset);
            Grosstotal = "" + (grandTotal) + ".00" + " \r\n";
            graphics.DrawString("Grand Total         " + Grosstotal, new Font("Courier New", 12, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

            graphics.DrawString("\n", new Font("Courier New", 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;


            cn.Close();



            Offset = Offset + 20;
            graphics.DrawString("----------------------------------------\r\n", new Font("Courier New", 9, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);



            cn.Close();

        }


        //.......
        public void print_lodge()
        {

            PrintDialog pd = new PrintDialog();
            pdoc = new PrintDocument();

            PrinterSettings ps = new PrinterSettings();
            Font font = new Font("Courier New", 12);


            PaperSize psize = new PaperSize("Custom", 100, 200);
            //ps.DefaultPageSettings.PaperSize = psize;


            pd.Document = pdoc;
            pd.Document.DefaultPageSettings.PaperSize = psize;
            pdoc.DefaultPageSettings.PaperSize.Height = 1500;//820

            pdoc.DefaultPageSettings.PaperSize.Width = 720;//520

            pdoc.PrintPage += new PrintPageEventHandler(PrintPage);  //test the new code 12-1-2014 by sagar


            pdoc.Print();

        }


        void PrintPage(object sender, PrintPageEventArgs e)
        {

            Graphics graphics = e.Graphics;
            Font font = new Font("Courier New", 10);

            float fontHeight = font.GetHeight();
            int startX = 0;
            int startY = 30;//30
            int Offset = 0;
            int fontSize = 9;
            string billDate = "";
            string date = DateTime.Now.ToShortDateString().ToString();
            double tax_amt = 0, disc_amt = 0, grand_total = 0, tax, disc;
            bool flag_grandtotal = false;
            //string hotelName, tagline1, address1, mob1, footer1, footer2;

            get_ReceiptFormt();




            graphics.DrawString("--------------------------------------------------\r", new Font("Courier New", fontSize), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString(hotelNam, new Font("Courier New", 14), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString(tagline, new Font("Courier New", 11), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString(address, new Font("Courier New", 11), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString(mob, new Font("Courier New", 11), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;

            //graphics.DrawString("        ", new Font("Courier New", 11), new SolidBrush(Color.Black), startX, startY + Offset);
            //Offset = Offset + 20;
            //graphics.DrawString("        ", new Font("Courier New", 14), new SolidBrush(Color.Black), startX, startY + Offset);
            //Offset = Offset + 20;
            //graphics.DrawString("        ", new Font("Courier New", 11), new SolidBrush(Color.Black), startX, startY + Offset);
            //Offset = Offset + 20;
            //graphics.DrawString("        ", new Font("Courier New", 11), new SolidBrush(Color.Black), startX, startY + Offset);
            //Offset = Offset + 20;
            //graphics.DrawString("        ", new Font("Courier New", 11), new SolidBrush(Color.Black), startX, startY + Offset);
            //Offset = Offset + 20;

            graphics.DrawString("---------------------------------------------------\r", new Font("Courier New", fontSize), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString(" Sr NO.:" + this.tableno + "             Bill No:" + this.billid + "  \r\n", new Font("Courier New", 12), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("---------------------------------------------------\r", new Font("Courier New", fontSize), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString(" Date " + "   Time  " + "  T/R No." + " " + "  WTR" + " " + "  CAPT" + " " + "  PAX" + "\r", new Font("Courier New", 11), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;

            graphics.DrawString(date + " " + DateTime.Now.ToString("HH:mm:ss") + "  " + "T/R No." + "  " + "WTR" + "  " + "CAPT" + "  " + "PAX" + "\r", new Font("Courier New", 11), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            String underLine = "---------------------------------------------------";
            graphics.DrawString(underLine, new Font("Courier New", fontSize), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString(" Description             Qty     Rate     Amount \r", new Font("Courier New", fontSize), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString(underLine, new Font("Courier New", fontSize), new SolidBrush(Color.Black), startX, startY + Offset);



            int i = 1;
            float subtotal = 0;
            // float tax, tax_amt, disc, disc_amt;

            string mname, qty, rate, amt;
            string qur = "SELECT menu.printName as nm, sales_item.qty as qty, sales_item.rate as rt, sales_item.total_amount as amt FROM menu INNER JOIN sales_item ON menu.menu_id = sales_item.menu_id WHERE (sales_item.order_id = '" + this.billid + "')";
            cn.Open();
            SqlCommand cmd = new SqlCommand(qur, cn);
            SqlDataReader rd = cmd.ExecuteReader();
            string sr = i.ToString();
            string tem_qty = "";
            int rowCount = 20;
            while (rd.Read() == true)
            {
                mname = rd["nm"].ToString();
                qty = rd["qty"].ToString();
                rate = rd["rt"].ToString();
                amt = rd["amt"].ToString();

                tem_qty = qty;

                subtotal += float.Parse(rd["amt"].ToString());
                //while (sr.Length <= 2)
                //    sr += " ";
                if (mname.Length < 11)
                {
                    while (mname.Length != 11)
                    {
                        mname += " ";
                    }
                }
                else
                { mname = mname.Substring(0, 11); }



                while (tem_qty.Length <= 4)
                {
                    tem_qty = " " + tem_qty;
                }

                while (rate.Length <= 6)
                {
                    rate = " " + rate;
                }
                while (amt.Length <= 7)
                {
                    amt = " " + amt;
                }
                Offset = Offset + 20;
                graphics.DrawString(" " + mname + "         " + tem_qty + "    " + rate + "   " + amt + "\r\n", new Font("Courier New", fontSize), new SolidBrush(Color.Black), startX, startY + Offset);
                i++;

                rowCount--;//reduce used rows 
            }
            //print remming blank rows
            for (int j = 0; j < rowCount; j++)
            {
                Offset = Offset + 20;
                graphics.DrawString("        ", new Font("Courier New", 10), new SolidBrush(Color.Black), startX, startY + Offset);

            }



            //graphics.DrawString("----------------------------------\r\n", new Font("Courier New", 10), new SolidBrush(Color.Black), startX, startY + Offset);
            String Grosstotal = " items:" + (i - 1) + "                           SUB TOTAL: " + subtotal + ".00" + " \r\n";


            Offset = Offset + 20;
            underLine = "------------------------------------------------------";
            graphics.DrawString(underLine, new Font("Courier New", fontSize), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;

            graphics.DrawString(Grosstotal, new Font("Courier New", fontSize), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            cn.Close();
            //test 
            //Tax apply
            string qry = "select tax_amt from tbl_tax where tax_type='Service Charge'";
            string value = "", value_1 = "";
            cn.Open();
            SqlCommand cmd1 = new SqlCommand(qry, cn);
            SqlDataReader rd1 = cmd1.ExecuteReader();
            if (rd1.Read())
            {
                flag_grandtotal = true;
                value = rd1["tax_amt"].ToString();

                tax_amt = (double.Parse(value) * subtotal) / 100;
                //  tax = subtotal + tax_amt;

                graphics.DrawString("        Service Charge:" + value + "%" + "                    " + tax_amt, new Font("Courier New", fontSize), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;


                //graphics.DrawString("                  Tax_Amount: " + tax + "%", new Font("Courier New", 12), new SolidBrush(Color.Black), startX, startY + Offset);
                //Offset = Offset + 20;
            }
            cn.Close();

            //discount apply
            string qry1 = "select tax_amt from tbl_tax where tax_type='Service Tax'";
            cn.Open();
            SqlCommand cmd2 = new SqlCommand(qry1, cn);
            SqlDataReader rd2 = cmd2.ExecuteReader();
            //int count = int.Parse(cmd2.ExecuteScalar().ToString());
            //if (count!=0)
            if (rd2.Read())
            {
                flag_grandtotal = true;
                value_1 = rd2["tax_amt"].ToString();
                // value_1 = cmd2.ExecuteScalar().ToString();
                disc_amt = (double.Parse(value_1) * subtotal) / 100;
                graphics.DrawString("         Service Tax:" + value_1 + "%" + "                  " + disc_amt, new Font("Courier New", fontSize), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;


                disc = subtotal - disc_amt;

                //graphics.DrawString("             Disc_Amount: " + disc + "%", new Font("Courier New", 12), new SolidBrush(Color.Black), startX, startY + Offset);
                //Offset = Offset + 20;
            }
            cn.Close();

            //test end
            if (flag_grandtotal)
            {
                // grand_total = subtotal + tax_amt + disc_amt;
                grand_total = Math.Round(subtotal + tax_amt + disc_amt);
                graphics.DrawString("SIGNATURE           Grand Total: " + grand_total.ToString() + ".00", new Font("Courier New", 12), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
            }
            graphics.DrawString("\n ", new Font("Courier New", 10), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;

            underLine = "------------------------------------------------------";
            graphics.DrawString(underLine, new Font("Courier New", fontSize), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("S.T.No.           ", new Font("Courier New", fontSize), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            underLine = "------------------------------------------------------";
            graphics.DrawString(underLine, new Font("Courier New", fontSize), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;

            graphics.DrawString(footer1, new Font("Courier New", 10), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString(footer2, new Font("Courier New", 10), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;


            // cn.Close();
        }

        string category;
        public string printKotType = string.Empty;
        public string kotItemReprint = string.Empty;

        public void printOrder_kot(string category, string grp, string kotReprint)
        {
            kotItemReprint = kotReprint;
            //Get Printer name for Snacks here
            Database db = new Database();
            this.category = category;
            db.cnopen();
            SqlCommand cmd = new SqlCommand("select value from tbl_option where grp='" + grp + "'", db.cn);
            try
            {
                if (cmd.ExecuteScalar().ToString() == "")
                    PrinterName = "0";
                else
                    PrinterName = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Printer Not Found");
            }
            db.cnclose();



            PrintDialog pd = new PrintDialog();
            pdoc = new PrintDocument();

            PrinterSettings ps = new PrinterSettings();
            Font font = new Font("Courier New", 15, System.Drawing.FontStyle.Bold);


            PaperSize psize = new PaperSize("Custom", 100, 200);
            //ps.DefaultPageSettings.PaperSize = psize;



            pd.Document = pdoc;
            pd.Document.DefaultPageSettings.PaperSize = psize;
            //pdoc.DefaultPageSettings.PaperSize.Height =320;
            pdoc.DefaultPageSettings.PaperSize.Height = 5000;

            pdoc.DefaultPageSettings.PaperSize.Width = 520;
            //pdoc_PrintPage();
            //pdoc.PrintPage += new PrintPageEventHandler(pdoc_PrintPage);

            //Assign Printer name here

            PrinterSettings printer = new PrinterSettings();
            pdoc.PrinterSettings.PrinterName = (PrinterName);

            pdoc.PrintPage += new PrintPageEventHandler(printkotorder);

            //DialogResult result = pd.ShowDialog();
            //if (result == DialogResult.OK)
            //{
            //    PrintPreviewDialog pp = new PrintPreviewDialog();
            //    pp.Document = pdoc;
            //    result = pp.ShowDialog();
            //    if (result == DialogResult.OK)
            //    {
            //        pdoc.Print();
            //    }
            //}
            pdoc.Print();

        }


        public void printkotorder(object sender, PrintPageEventArgs e)
        {
            try
            {

                //get_ReceiptFormt(); //get the receipt format from the db

                Database db = new Database();
                string get_suggestion = "";
                string sugg = "";
                string fontName = "Microsoft Sans Serif";//Courier New
                string fontnameC = "Lucida Fax";
                Graphics graphics = e.Graphics;
                string mname, qty, discrip;
                //string billId = "";
                int c = 1;
                // LPrinter MyPrinter = new LPrinter();
                //string qur = "SELECT menu.m_name as nm, sales_item.qty as qty FROM menu INNER JOIN sales_item ON menu.menu_id = sales_item.menu_id WHERE (sales_item.order_id = '" + billId + "')";
                //SqlConnection cn = new SqlConnection();
                string kot_id = db.getDb_Value("select max(kot_id) from sales_item where order_id='" + billid + "'").ToString();

                //string qur = " SELECT     sales_item.menu_id, sales_item.qty, menu.m_name, sales_item.w_id, sales_item.order_id ,sales_item.order_sugg FROM         sales_item INNER JOIN     menu ON sales_item.menu_id = menu.menu_id WHERE sales_item.order_id = '" + billid + "'";
                string qur = this.category;

                if (kotItemReprint != null)
                {
                    qur = "SELECT     sales_item.menu_id, sales_item.qty, menu.m_name, sales_item.w_id, sales_item.order_id ,sales_item.order_sugg FROM         sales_item INNER JOIN     menu ON sales_item.menu_id = menu.menu_id WHERE sales_item.order_id = '" + billid + "' and sales_item.sales_id='" + kotItemReprint.Split('*')[0] + "'";
                }

                if (kotItemReprint != null)
                {
                    if (kotItemReprint.Split('*')[1] == "Cancel KOT")
                    {
                        //  qur = "SELECT     sales_item.menu_id, sales_item.qty, menu.m_name, sales_item.w_id, sales_item.order_id ,sales_item.order_sugg FROM         sales_item INNER JOIN     menu ON sales_item.menu_id = menu.menu_id WHERE sales_item.order_id = '" + billid + "' and sales_item.sales_id='" + kotItemReprint.Split('*')[0] + "'";
                        qur = " select menuName as m_name,qty,cancelKotReson as order_sugg from CancelKOTDetails ckd  where orderId='" + billid + "'  and id= (select MAX(id) from CancelKOTDetails where orderId='" + billid + "'  )";
                    }
                }


                string Covers = db.getDb_Value("select noOfGuest from table_order where order_id='" + billid + "'").ToString();

                string waiterN = db.getDbstatus_Value("select Waiter from table_order where order_id='" + billid + "'").ToString();

                string getTableNO = db.getDbstatus_Value("select t_id from table_order where order_id='" + billid + "'");
                //----- sugg = db.getDb_Value("select order_sugg from sales_item where kot_id='" + kot_id + "'").ToString();

                // get_suggestion = db.getDbstatus_Value("select description from tbl_orderSuggestion where kot_id='" + kot_id + "'").ToString();
                //get_suggestion = "";
                cn.Open();
                SqlCommand cmd = new SqlCommand(qur, cn);
                SqlDataReader rd = cmd.ExecuteReader();
                int i = 1;
                int startX = 0;
                int startD = 30;
                int startY = 30;
                int Offset = 0;
                var centerformat = new StringFormat() { Alignment = StringAlignment.Center };
                var leftformat = new StringFormat() { Alignment = StringAlignment.Near };
                var rect = new RectangleF(0, 0, 265, 20);

                if (db.ChkDb_Value("select value from tbl_option where grp='PrintSize' and value='3 Inch'"))
                {
                    //graphics.DrawString(" T:" + this.tableno + "\t            B:" + this.billid + "          KOT:" + kot_id + "  \r\n", new Font(fontName, 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                    //Offset = Offset + 20;

                    if (kotItemReprint != null)
                    {
                        graphics.DrawString("--------   " + kotItemReprint.Split('*')[1] + "  -------", new Font(fontName, 11, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                        Offset = Offset + 30;
                    }

                    graphics.DrawString("              KOT :", new Font(fontName, 14, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                    graphics.DrawString("                        " + kot_id + " ", new Font(fontName, 14, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                    Offset = Offset + 30;
                    graphics.DrawString("Table No: ", new Font(fontName, 11, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                    graphics.DrawString("                 " + getTableNO + "", new Font(fontName, 11, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                    graphics.DrawString("                                 Bill No :", new Font(fontName, 10, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                    graphics.DrawString("                                         " + this.billid + "", new Font(fontName, 11, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);






                    Offset = Offset + 20;
                    graphics.DrawString(" Date :" + System.DateTime.Now.ToString("dd/MM/yyyy") + "       Time : " + System.DateTime.Now.ToString("hh:mm:ss") + " \r\n", new Font(fontName, 9, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);


                    if (db.ChkDb_Value("select value from tbl_option where grp='waiterDetails' and value='Yes'"))
                    {
                        Offset = Offset + 20;
                        graphics.DrawString(" C : " + this.wname + "   W:=" + waiterN + "       Covers:  " + Covers + "    \r\n", new Font(fontName, 9, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                    }

                    Offset = Offset + 15;
                    graphics.DrawString("-------------------------------------------------------------------\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 10;
                    graphics.DrawString(" Item Name                                                       Qty  \r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 10;
                    graphics.DrawString("-------------------------------------------------------------------\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                }
                if (db.ChkDb_Value("select value from tbl_option where grp='PrintSize' and value='2 Inch'"))
                {
                    graphics.DrawString(" Table NO :" + this.tableno + "          Bill No:" + this.billid + " \r\n", new Font(fontName, 7, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                    Offset = Offset + 20;
                    graphics.DrawString(" Date :" + System.DateTime.Now.ToString("dd/MM/yyyy") + "    Time : " + System.DateTime.Now.ToString("hh:mm:ss") + " \r\n", new Font(fontName, 7, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                    Offset = Offset + 20;
                    graphics.DrawString(" Waiter Name : " + this.wname + "\r\n", new Font(fontName, 7, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);


                    Offset = Offset + 15;
                    graphics.DrawString("---------------------------------------------------------------------\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 10;
                    graphics.DrawString(" Item Name                                Qty  \r\n", new Font(fontName, 7, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 10;
                    graphics.DrawString("---------------------------------------------------------------------\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                }
                string sr = i.ToString();

                while (rd.Read() == true)
                {
                    mname = rd["m_name"].ToString();
                    qty = rd["qty"].ToString();
                    discrip = rd["order_sugg"].ToString();


                    //if (mname.Length <= 40)
                    //{
                    //    while (mname.Length <= 40)
                    //    {
                    //        mname += " ";
                    //    }
                    //}
                    //else
                    //{ mname = mname.Substring(0, 40); }


                    while (qty.Length <= 3)
                    {

                        qty = " " + qty;
                    }

                    Offset = Offset + 22;
                    if (db.ChkDb_Value("select value from tbl_option where grp='PrintSize' and value='3 Inch'"))
                    {

                        rect = new RectangleF(startX, startY + Offset, 240, 38);

                        //var rect = new RectangleF(0, 0, 265, 20);

                        // graphics.DrawString(hotelNam.Trim(), new Font(reciprtFontName, hotelNmfont_size, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, centerformat);

                        //Offset = Offset + 25;
                        graphics.DrawString(mname + "\r\n", new Font(fontName, 11, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, leftformat);

                        if (discrip != "")
                        {
                            Offset = Offset + 18;
                            graphics.DrawString(discrip + "\r\n", new Font(fontnameC, 10, System.Drawing.FontStyle.Regular), new SolidBrush(Color.Black), startD, startY + Offset);

                        }
                        // graphics.DrawString(    discrip + "\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, centerformat);

                        graphics.DrawString("                                                   " + qty + "\r\n", new Font(fontName, 12, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);

                        // Offset = Offset + 20;
                        if (mname.Length > 25)
                        {
                            Offset = Offset + 18;
                        }



                    }


                    if (db.ChkDb_Value("select value from tbl_option where grp='PrintSize' and value='2 Inch'"))
                    {
                        graphics.DrawString(" " + mname + "\r\n", new Font(fontName, 6, System.Drawing.FontStyle.Regular), new SolidBrush(Color.Black), startX, startY + Offset);

                        graphics.DrawString("                                                              " + qty + "\r\n", new Font(fontName, 6, System.Drawing.FontStyle.Regular), new SolidBrush(Color.Black), startX, startY + Offset);
                    }
                    i++;
                }

                Offset = Offset + 10;
                if (db.ChkDb_Value("select value from tbl_option where grp='PrintSize' and value='3 Inch'"))
                {
                    graphics.DrawString("---------------------------------------------------------------------\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 20;
                    graphics.DrawString("Note:\r\n", new Font(fontName, 7, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 8;
                    graphics.DrawString("     " + get_suggestion, new Font("Courier New", 11, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                }
                if (db.ChkDb_Value("select value from tbl_option where grp='PrintSize' and value='2 Inch'"))
                {
                    graphics.DrawString("---------------------------------------------------------------------\r\n", new Font(fontName, 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 20;
                    graphics.DrawString("Note:\r\n", new Font(fontName, 7, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 20;
                    graphics.DrawString("      " + get_suggestion, new Font("Courier New", 8, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                }
                cn.Close();

            }




            catch (Exception ex)
            {
            }




        }


        public string reportFromdate, reportToDate;

        public void dailySummeryReport(string getFromDate, string getTodate)
        {
            reportFromdate = getFromDate;
            reportToDate = getTodate;

            Database db = new Database();

            db.cnopen();
            SqlCommand cmd = new SqlCommand("select value from tbl_option where grp='Bill Printer'", db.cn);
            try
            {
                if (cmd.ExecuteScalar().ToString() == "")
                    PrinterName = "0";
                else
                    PrinterName = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Printer Not Found");
            }
            db.cnclose();
            PrintDialog pd = new PrintDialog();
            pdoc = new PrintDocument();
            PrinterSettings ps = new PrinterSettings();
            Font font = new Font("Courier New", 15, System.Drawing.FontStyle.Bold);
            PaperSize psize = new PaperSize("Custom", 100, 200);
            pd.Document = pdoc;
            pd.Document.DefaultPageSettings.PaperSize = psize;
            pdoc.DefaultPageSettings.PaperSize.Height = 5000;
            pdoc.DefaultPageSettings.PaperSize.Width = 520;
            PrinterSettings printer = new PrinterSettings();
            pdoc.PrinterSettings.PrinterName = (PrinterName);
            pdoc.PrintPage += new PrintPageEventHandler(print_DailyReportSummery);
            pdoc.Print();

        }

        public void print_DailyReportSummery(object sender, PrintPageEventArgs e)
        {
            Database db = new Database();

            string fontName = "Microsoft Sans Serif";//Courier New

            Graphics graphics = e.Graphics;

            int i = 1;
            int startX = 0;
            int startD = 30;
            int startY = 30;
            int Offset = 0;
            var centerformat = new StringFormat() { Alignment = StringAlignment.Center };
            var leftformat = new StringFormat() { Alignment = StringAlignment.Near };
            var rightformat = new StringFormat() { Alignment = StringAlignment.Far };
            var rect = new RectangleF(0, 0, 265, 20);

            rect = new RectangleF(startX, startY + Offset, 280, 38);
            string getCompanyName = db.getDbstatus_Value("select distinct hotelName from  tbl_receiptFormat");
            graphics.DrawString(getCompanyName, new Font(fontName, 14, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, centerformat);

            Offset = Offset + 40;
            rect = new RectangleF(startX, startY + Offset, 280, 38);
            graphics.DrawString("Date : " + DateTime.Now.ToString("dd/MM/yyyy hh:mm"), new Font(fontName, 11, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, centerformat);

            Offset = Offset + 20;
            rect = new RectangleF(startX, startY + Offset, 280, 38);
            graphics.DrawString("From :" + reportFromdate + " To. : " + reportToDate, new Font(fontName, 11, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, centerformat);

            Offset = Offset + 20;
            rect = new RectangleF(startX, startY + Offset, 280, 38);
            graphics.DrawString("Daily Manager Report", new Font(fontName, 14, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, centerformat);


            Offset = Offset + 20;
            rect = new RectangleF(startX, startY + Offset, 280, 25);
            graphics.DrawString("-----------------------------------------------------------", new Font(fontName, 11, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, centerformat);



            DataTable getValueDetails = new DataTable();

            getValueDetails = db.Displaygrid(@" select 'Total Billing :' as Header,SUM(total_bill)  as amount from total_bill where  datetime between '" + Convert.ToDateTime(reportFromdate).ToString("MM-dd-yyyy") + "' AND '" + Convert.ToDateTime(reportToDate).ToString("MM-dd-yyyy") + "'  AND status<>'NC' "
                                + " union all select 'Total '+status+' :' as Header,SUM(total_bill) as amount from total_bill where  datetime between '" + Convert.ToDateTime(reportFromdate).ToString("MM-dd-yyyy") + "' AND '" + Convert.ToDateTime(reportToDate).ToString("MM-dd-yyyy") + "' AND status<>'NC'   group by status");
            // + " union all select 'Total NC :' as Header,SUM(amount) as amount from NC_itemDetails where  ncDate between '" + Convert.ToDateTime(reportFromdate).ToString("MM-dd-yyyy") + "' AND '" + Convert.ToDateTime(reportToDate).ToString("MM-dd-yyyy") + "'  ");

            //select SUM(amount) from NC_itemDetails where ncDate='2019-01-06'

            if (getValueDetails.Rows.Count != 0)
            {
                for (int k = 0; k < getValueDetails.Rows.Count; k++)
                {
                    Offset = Offset + 20;
                    rect = new RectangleF(startX, startY + Offset, 280, 30);
                    graphics.DrawString(getValueDetails.Rows[k]["Header"].ToString(), new Font(fontName, 11, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, leftformat);

                    //status amount
                    rect = new RectangleF(startX, startY + Offset, 280, 30);
                    graphics.DrawString(getValueDetails.Rows[k]["amount"].ToString(), new Font(fontName, 11, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, rightformat);
                }
            }


            //NC Total seprate

            Offset = Offset + 30;
            getValueDetails = db.Displaygrid(@" select 'Total NC :' as Header,SUM(amount) as amount from NC_itemDetails where  ncDate between '" + Convert.ToDateTime(reportFromdate).ToString("MM-dd-yyyy") + "' AND '" + Convert.ToDateTime(reportToDate).ToString("MM-dd-yyyy") + "'  ");

            //select SUM(amount) from NC_itemDetails where ncDate='2019-01-06'

            if (getValueDetails.Rows.Count != 0)
            {
                for (int k = 0; k < getValueDetails.Rows.Count; k++)
                {
                    Offset = Offset + 20;
                    rect = new RectangleF(startX, startY + Offset, 280, 30);
                    graphics.DrawString(getValueDetails.Rows[k]["Header"].ToString(), new Font(fontName, 11, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, leftformat);

                    //status amount
                    rect = new RectangleF(startX, startY + Offset, 280, 30);
                    graphics.DrawString(getValueDetails.Rows[k]["amount"].ToString(), new Font(fontName, 11, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, rightformat);
                }
            }



            //total discount
            Offset = Offset + 10;
            getValueDetails = db.Displaygrid(@" select 'Total Discount : ' as Header ,SUM(foodDiscAmt)+sum(liquorDiscAmt)+SUM(beveragesDiscAmt) as amount from table_order  tob   inner join total_bill tb on tob.order_id=tb.order_id where   timeing between  '" + Convert.ToDateTime(reportFromdate).ToString("MM/dd/yyyy") + "' AND '" + Convert.ToDateTime(reportToDate).ToString("MM/dd/yyyy") + "'");

            if (getValueDetails.Rows.Count != 0)
            {
                for (int k = 0; k < getValueDetails.Rows.Count; k++)
                {
                    Offset = Offset + 20;
                    rect = new RectangleF(startX, startY + Offset, 280, 30);
                    graphics.DrawString(getValueDetails.Rows[k]["Header"].ToString(), new Font(fontName, 11, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, leftformat);

                    //status amount
                    rect = new RectangleF(startX, startY + Offset, 280, 30);
                    graphics.DrawString(getValueDetails.Rows[k]["amount"].ToString(), new Font(fontName, 11, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, rightformat);
                }
            }


            // get section wise sales here 

            getValueDetails.Rows.Clear();
            Offset = Offset + 40;

            //            getValueDetails = db.Displaygrid(@"select 'Total '+cat.FoodSection +' Sales :' as Header,sum(total_amount) as amount from sales_item si inner join menu m on si.menu_id=m.menu_id
            //inner join category cat on m.category=cat.cat_name 
            //inner join total_bill tob on tob.order_id=si.order_id   and datetime between '" + Convert.ToDateTime(reportFromdate).ToString("MM-dd-yyyy") + "' AND '" + Convert.ToDateTime(reportToDate).ToString("MM-dd-yyyy") + "' and tob.status<>'NC' group by cat.FoodSection    ");

            //            getValueDetails = db.Displaygrid(@"select 'Total '+cat.FoodSection +' Sales :' as Header,sum(total_amount) as amount from sales_item si inner join menu m on si.menu_id=m.menu_id
            //inner join category cat on m.category=cat.cat_name  
            //inner join (select distinct order_id  from total_bill where datetime between  '" + Convert.ToDateTime(reportFromdate).ToString("MM-dd-yyyy") + "' AND '" + Convert.ToDateTime(reportToDate).ToString("MM-dd-yyyy") + "'  and status<>'NC') v  on si.order_id=v.order_id  group by cat.FoodSection");


            getValueDetails = db.Displaygrid(@" select 'Total '+cat.FoodSection +' Sales :' as Header,sum(total_amount)-dis.disAmt as amount  from sales_item si inner join menu m on si.menu_id=m.menu_id
inner join category cat on m.category=cat.cat_name  inner join (select distinct order_id  from total_bill "
 + " where datetime between  '" + Convert.ToDateTime(reportFromdate).ToString("MM-dd-yyyy") + "' AND '" + Convert.ToDateTime(reportFromdate).ToString("MM-dd-yyyy") + "'  and status<>'NC') v  on si.order_id=v.order_id  "
 + " inner join ( select sum(foodDiscAmt) as disAmt, 'Food' as ty from table_order tb inner join     ( select distinct order_id  from total_bill where datetime between  '" + Convert.ToDateTime(reportFromdate).ToString("MM-dd-yyyy") + "' AND '" + Convert.ToDateTime(reportFromdate).ToString("MM-dd-yyyy") + "' "
 + " and status<>'NC') v  on tb.order_id=v.order_id  union all  select   sum(beveragesDiscAmt) as disAmt, 'Beverages' as ty from table_order tb inner join "
 + " ( select distinct order_id  from total_bill where datetime between  '" + Convert.ToDateTime(reportFromdate).ToString("MM-dd-yyyy") + "' AND '" + Convert.ToDateTime(reportFromdate).ToString("MM-dd-yyyy") + "' "
 + " and status<>'NC') v  on tb.order_id=v.order_id  union all  select  sum(liquorDiscAmt) as disAmt, 'Liquor'  as ty from table_order tb inner join  "
  + " ( select distinct order_id  from total_bill where datetime between  '" + Convert.ToDateTime(reportFromdate).ToString("MM-dd-yyyy") + "' AND '" + Convert.ToDateTime(reportFromdate).ToString("MM-dd-yyyy") + "'  and status<>'NC') v  on tb.order_id=v.order_id "
  + " ) dis on dis.ty=cat.FoodSection  group by cat.FoodSection,dis.disAmt");


            if (getValueDetails.Rows.Count != 0)
            {
                for (int k = 0; k < getValueDetails.Rows.Count; k++)
                {
                    Offset = Offset + 20;
                    rect = new RectangleF(startX, startY + Offset, 280, 30);
                    graphics.DrawString(getValueDetails.Rows[k]["Header"].ToString(), new Font(fontName, 11, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, leftformat);

                    //status amount
                    rect = new RectangleF(startX, startY + Offset, 280, 30);
                    graphics.DrawString(getValueDetails.Rows[k]["amount"].ToString(), new Font(fontName, 11, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, rightformat);
                }
            }



            Offset = Offset + 20;
            rect = new RectangleF(startX, startY + Offset, 280, 25);
            graphics.DrawString("-----------------------------------------------------------", new Font(fontName, 11, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, centerformat);



            object sumObject;
            sumObject = getValueDetails.Compute("Sum(amount)", string.Empty);

            Offset = Offset + 20;
            rect = new RectangleF(startX, startY + Offset, 280, 30);
            graphics.DrawString("Sub Total :", new Font(fontName, 11, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, leftformat);

            rect = new RectangleF(startX, startY + Offset, 280, 30);
            graphics.DrawString(sumObject.ToString(), new Font(fontName, 11, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, rightformat);


            Offset = Offset + 40;

            //get covers here 
            string Covers = db.getDb_Value("select sum(noOfGuest) from table_order where timeing between  '" + Convert.ToDateTime(reportFromdate).ToString("MM/dd/yyyy") + "' AND '" + Convert.ToDateTime(reportToDate).ToString("MM/dd/yyyy") + "'").ToString();



            rect = new RectangleF(startX, startY + Offset, 280, 30);
            graphics.DrawString("Total Day Cover :", new Font(fontName, 11, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, leftformat);

            rect = new RectangleF(startX, startY + Offset, 280, 30);
            graphics.DrawString(Covers, new Font(fontName, 11, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, rightformat);

            Offset = Offset + 50;

            // get section wise sales here 

            getValueDetails.Rows.Clear();
            //  Offset = Offset + 60;

            //            getValueDetails = db.Displaygrid(@" select 'Total Service Charge :'  as Header,(SUM(serviceTaxFoodAmt) + SUM(serviceTaxLiquorAmt)) as amount  from table_order tob 
            //                inner join total_bill tb on tob.order_id=tb.order_id and tb.status<>'NC'   where timeing between  '" + Convert.ToDateTime(reportFromdate).ToString("MM/dd/yyyy") + "' AND '" + Convert.ToDateTime(reportToDate).ToString("MM/dd/yyyy") + "'" +
            //                " union all select 'Total VAT :' as Header, SUM(vatAmt)   as amount   from table_order tob  inner join total_bill tb on tob.order_id=tb.order_id  and tb.status<>'NC'  where timeing between  '" + Convert.ToDateTime(reportFromdate).ToString("MM/dd/yyyy") + "' AND '" + Convert.ToDateTime(reportToDate).ToString("MM/dd/yyyy") + "'" +
            //                " union all  select 'Total CGST :' as Header, SUM(gst)/2  as amount   from table_order tob  inner join total_bill tb on tob.order_id=tb.order_id  and tb.status<>'NC'   where timeing between  '" + Convert.ToDateTime(reportFromdate).ToString("MM/dd/yyyy") + "' AND '" + Convert.ToDateTime(reportToDate).ToString("MM/dd/yyyy") + "' " +
            //                " union all  select 'Total SGST :' as Header, SUM(gst)/2   as amount  from table_order tob  inner join total_bill tb on tob.order_id=tb.order_id   and tb.status<>'NC'   where timeing between  '" + Convert.ToDateTime(reportFromdate).ToString("MM/dd/yyyy") + "' AND '" + Convert.ToDateTime(reportToDate).ToString("MM/dd/yyyy") + "'");

            getValueDetails = db.Displaygrid(@" select 'Total Service Charge :'  as Header, SUM(serviceTaxFoodAmt) + SUM(serviceTaxLiquorAmt) as amount  from table_order tob inner join (select distinct order_id  from total_bill where datetime between  '" + Convert.ToDateTime(reportFromdate).ToString("MM/dd/yyyy") + "' AND '" + Convert.ToDateTime(reportToDate).ToString("MM/dd/yyyy") + "' ) v  on tob.order_id=v.order_id  " +
" union all  select 'Total VAT :' as Header, SUM(vatAmt)   as amount  from table_order tob inner join (select distinct order_id  from total_bill where datetime between  '" + Convert.ToDateTime(reportFromdate).ToString("MM/dd/yyyy") + "' AND '" + Convert.ToDateTime(reportToDate).ToString("MM/dd/yyyy") + "' ) v  on tob.order_id=v.order_id  " +
 " union all  select    'Total CGST :' as Header, SUM(gst)/2  as amount   from table_order tob inner join (select distinct order_id  from total_bill where datetime between  '" + Convert.ToDateTime(reportFromdate).ToString("MM/dd/yyyy") + "' AND '" + Convert.ToDateTime(reportToDate).ToString("MM/dd/yyyy") + "' ) v  on tob.order_id=v.order_id  " +
" union all  select    'Total CGST :' as Header, SUM(gst)/2  as amount   from table_order tob inner join (select distinct order_id  from total_bill where datetime between  '" + Convert.ToDateTime(reportFromdate).ToString("MM/dd/yyyy") + "' AND '" + Convert.ToDateTime(reportToDate).ToString("MM/dd/yyyy") + "' ) v  on tob.order_id=v.order_id   ");

            if (getValueDetails.Rows.Count != 0)
            {
                for (int k = 0; k < getValueDetails.Rows.Count; k++)
                {
                    Offset = Offset + 20;
                    rect = new RectangleF(startX, startY + Offset, 280, 30);
                    graphics.DrawString(getValueDetails.Rows[k]["Header"].ToString(), new Font(fontName, 11, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, leftformat);

                    //status amount
                    rect = new RectangleF(startX, startY + Offset, 280, 30);
                    graphics.DrawString(getValueDetails.Rows[k]["amount"].ToString(), new Font(fontName, 11, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, rightformat);
                }
            }


            Offset = Offset + 50;


            rect = new RectangleF(startX, startY + Offset, 280, 25);
            graphics.DrawString("   *****   ", new Font(fontName, 11, System.Drawing.FontStyle.Bold), new SolidBrush(Color.Black), rect, centerformat);


        }
    }
}


