using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Windows.Forms.Design;
using System.Security.Cryptography;


namespace Hotal_Managment_Syatem 
{
    class Database
    {
        // ErrorLog erLog = new ErrorLog();

        public static string ConString = StringCipher.Decrypt(System.IO.File.ReadAllText("Config.txt"), "5");
        // public static string ConString = @"RestrosoftDB";
        // public static string ConString = System.IO.File.ReadAllText("Config.txt");

        //public void database()
        //{




        //    DecryptFile("Config.txt");
        //    ConString = System.IO.File.ReadAllText("Config.txt");
        //}

        public SqlConnection cn = new SqlConnection(ConString);
        LPrinter MyPrinter = new LPrinter();
        //int a=1;
        float a;
        string str;
        //display dgv

        public DataTable Displaygrid(string query)
        {
            DataTable dt = new DataTable();
            try
            {

                cnopen();
                SqlCommand cmd = new SqlCommand(query, cn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //erLog.WriteErrorLog(ex.ToString());
            }
            return dt;
        }

        public void delete(string query)
        {
            try
            {
                if (MessageBox.Show("Do You Really Want to Delete Record?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    cnopen();
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.ExecuteNonQuery();
                    // message("Data Delete Successfully!!!");
                    cnclose();
                }
                else
                {
                    MessageBox.Show("Deletion Discarded");
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
        public void assignDB(string db)
        {
            // ConString = @"Data Source=.\abms;Initial Catalog=" + db + ";Integrated Security=True;User ID=sa;Password=abms@2014R";
            ConString = System.IO.File.ReadAllText("Config.txt");
        }
        //get values from database
        public float getDb_Value(String query)
        {
            cnopen();
            SqlCommand cmd = new SqlCommand(query, cn);

            try
            {
                if (cmd.ExecuteScalar().ToString() == "")
                    a = 0;
                else
                    a = float.Parse(cmd.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //erLog.WriteErrorLog(ex.ToString());
            }

            return a;
        }


        public void DrinkstockUpdate(DataGridView DGV_tblOrder)
        {
            // reduce drink qty in ml

            float MLqty, menuQty, qtyNo, QuantityInML, stockqtyNo, stockqtyML, totalqtyNo = 0, totalMLQty = 0;

            for (int k = 0; k < DGV_tblOrder.RowCount; k++)
            {
                string item_nm = DGV_tblOrder.Rows[k].Cells[2].Value.ToString();
                string drinkGroup = getDbstatus_Value("select drinkGroup from menu where m_name='" + item_nm + "'");
                if (ChkDb_Value("select qty from tbl_stock where item_name='" + drinkGroup + "'"))
                {
                    MLqty = getDb_Value("select qtyInML from tbl_drinkgroup where grpName='" + drinkGroup + "'");
                    stockqtyNo = getDb_Value("select qty from tbl_stock where item_name='" + drinkGroup + "'");
                    totalMLQty = MLqty * stockqtyNo;

                    menuQty = getDb_Value("select MenuQty from menu where m_name='" + item_nm + "'");
                    QuantityInML = totalMLQty - (float.Parse(DGV_tblOrder.Rows[k].Cells[3].Value.ToString()) * menuQty);

                    if (totalMLQty > 0)
                    {

                        totalqtyNo = QuantityInML / MLqty;
                        // float QtyML = QuantityInML * qtyNo;
                        update("update tbl_stock set qty='" + totalqtyNo + "' where item_name='" + drinkGroup + "'");
                    }
                }
            }
        }
        public void DecryptFile(string inputFile)
        {
            try
            {

                string password = @"myKey123"; // Your Key Here

                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] key = UE.GetBytes(password);

                FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);

                RijndaelManaged RMCrypto = new RijndaelManaged();

                CryptoStream cs = new CryptoStream(fsCrypt,
                    RMCrypto.CreateDecryptor(key, key),
                    CryptoStreamMode.Read);

                //FileStream fsOut = new FileStream(outputFile, FileMode.Create);

                //int data;
                //while ((data = cs.ReadByte()) != -1)
                //    fsOut.WriteByte((byte)data);

                //fsOut.Close();
                cs.Close();
                fsCrypt.Close();
                MessageBox.Show("Dcryption Successfully", "Success");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //erLog.WriteErrorLog(ex.ToString());
            }
        }
        public string delLastChar(string str) //fun by sagar delete last char usefull at the time of keyprss event used element last unuse char
        {

            string getnewItemStr = str.TrimEnd(str[str.Length - 1]);
            return getnewItemStr;

        }

        public string getDb_1_Value(String query)
        {
            string str = "";
            cnopen();

            SqlCommand cmd = new SqlCommand(query, cn);
            SqlDataReader rd2 = cmd.ExecuteReader();
            try
            {
                if (cmd.ExecuteScalar().ToString() == "")
                    str = "";
                else
                    str = rd2["timeing"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //erLog.WriteErrorLog(ex.ToString());
            }
            cnclose();
            rd2.Close();
            return str;
        }
        public string getDbstatus_Value(String query)
        {
            cnopen();
            SqlCommand cmd = new SqlCommand(query, cn);
            try
            {
                if (cmd.ExecuteScalar().ToString() == "")
                    str = "0";
                else
                    str = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //erLog.WriteErrorLog(ex.ToString());
            }
            return str;
        }
        public void cnopen()
        {
            if (cn.State == ConnectionState.Open)
            {
                cn.Close();
            }
            cn.Open();
        }

        public void cnclose()
        {
            if (cn.State != ConnectionState.Closed)
            {
                cn.Close();
            }
        }

        public void InsertData(string query, string tblName)
        {

            try
            {
                cnopen();
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //erLog.WriteErrorLog(ex.ToString());
            }
            finally
            {
                cnclose();
            }
        }
        public void insert(string query)
        {
            try
            {
                cnopen();
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.ExecuteNonQuery();
                //  message("Data SAVE Successfully!!!");
                cnclose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //erLog.WriteErrorLog(ex.ToString());
            }
        }
        public void msgbox()
        {
            MessageBox.Show("Data Inserted Sucessfully");
        }
        public bool confirm()
        {
            DialogResult dlgresult = MessageBox.Show("Are you sure want to Save Record?", "Confirm Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlgresult == DialogResult.Yes)
                return true;
            else
                return false;
        }
        public void update(string query)
        {
            try
            {
                cnopen();
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.ExecuteNonQuery();
                //  message("Data Update Successfully!!!");
                cnclose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //erLog.WriteErrorLog(ex.ToString());
            }
        }
        public bool ChkDb_Value(String query)
        {
            //try
            //{
            cnopen();
            SqlCommand cmd = new SqlCommand(query, cn);
            SqlDataReader rd = cmd.ExecuteReader();

            if (rd.Read() == true)
                return true;
            else
                return false;
            //}
            //catch (Exception ex)
            //{
            //    // MessageBox.Show(ex.Message);
            //    erLog.WriteErrorLog(ex.ToString());
            //}

        }
        public void reset(Form f)
        {


            foreach (Control txt in f.Controls)
            {

                // MessageBox.Show(txt.Name.ToString());
                if (txt is TextBox)
                {
                    TextBox txtbox = (TextBox)txt;
                    txtbox.Text = "";
                }

                if (txt is ComboBox)
                {
                    txt.Text = "";
                }


            }
        }

        public void DeleteData(string query, string tblName)
        {
            try
            {
                cnopen();
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //erLog.WriteErrorLog(ex.ToString());
            }
            finally
            {
                cnclose();
            }
        }


        // Fill Combo Box
        public void comboFill(ComboBox cmb, string strSQL, string dTable, string dDisplay, string dValue)
        {
            try
            {
                //cmb.DropDownStyle = ComboBoxStyle.DropDownList;
                cnopen();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(strSQL, cn);
                da.Fill(ds, dTable);
                cmb.DataSource = ds.Tables[dTable].DefaultView;
                cmb.DisplayMember = dDisplay;
                cmb.ValueMember = dValue;
                cnclose();
                // cmb.Attributes.Add("style", "background-color:#111111");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //erLog.WriteErrorLog(ex.ToString());
            }

        }


        public DataSet ShowData(string query, string tblName)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da1 = new SqlDataAdapter(query, cn);
            da1.Fill(ds, tblName);
            return ds;
        }


        public void SelectData(string query, string tblName)
        {
            cnopen();
            SqlCommand cmd = new SqlCommand(query, cn);
            cmd.ExecuteNonQuery();
            cn.Close();
        }


        public void UpdateData(string query)
        {
            try
            {
                cnopen();
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //erLog.WriteErrorLog(ex.ToString());
            }
            finally
            {
                cnclose();
            }
        }
        public void formFix(Form f)
        {
            // f.BackgroundImage = new Bitmap();
            f.MaximumSize = f.MinimumSize = f.Size; //border color for text box
            //FixedSingle
            foreach (Control txt in f.Controls)
            {
                // MessageBox.Show(txt.Name.ToString());
                if (txt is TextBox)
                {
                    TextBox txtbox = (TextBox)txt;
                    txtbox.BorderStyle = BorderStyle.FixedSingle;
                }
                if (txt is Button)
                {
                    Button btn = (Button)txt;
                    btn.BackColor = Color.White;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 1;
                }
                //if (txt is LinkLabel)
                //{
                //    LinkLabel txt = (LinkLabel)txt;
                //    txt.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
                //}
            }
        }

        public int GetID(string colName, string tblName)
        {
            int myID = 1;
            try
            {
                cnopen();
                SqlCommand cmddr = new SqlCommand("select max(" + colName + ") as ids from " + tblName, cn);
                SqlDataReader dr = cmddr.ExecuteReader();

                while (dr.Read())
                {
                    string str = dr["ids"].ToString();

                    if (str == "")
                    {
                        myID = 1;
                    }
                    else
                    {
                        myID = Convert.ToInt32(dr["ids"]) + 1;
                    }
                }
                dr.Close();
                cn.Close();
                cmddr.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //erLog.WriteErrorLog(ex.ToString());
            }
            finally
            {
                cnclose();
            }
            return myID;
        }
        public void deleteGridRowDirect(DataGridView dg, string tbl, string colnm, int cellNo)
        {
            string id;
            int i = dg.SelectedCells[0].RowIndex;
            // valueID = dataGridView1.Rows[i].Cells[3].Value.ToString();
            id = dg.Rows[i].Cells[cellNo].Value.ToString();
            cnopen();
            SqlCommand cmd = new SqlCommand("delete from " + tbl + " where " + colnm + "= '" + id + "'", cn);
            try
            {

                int cnt = (int)cmd.ExecuteNonQuery();
                //if (cnt != 0)
                //    // cmd.ExecuteNonQuery();
                //    MessageBox.Show("Item Deleted", "Hotel", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //erLog.WriteErrorLog(ex.ToString());
            }
            cnclose();
        }
        public void deleteGridRow(DataGridView dg, string tbl, string colnm, int cellNo)
        {
            //DataGrid dg = new DataGrid();
            //DataGridViewCellEventArgs e;
            //if (e.ColumnIndex == dg.Columns[0].Index)
            //{
            string id;
            cnopen();
            int i = dg.SelectedCells[0].RowIndex;
            // valueID = dataGridView1.Rows[i].Cells[3].Value.ToString();
            id = dg.Rows[i].Cells[cellNo].Value.ToString();
            DialogResult dlgresult = MessageBox.Show("Are you sure want to Delete?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlgresult == DialogResult.Yes)
            {
                SqlCommand cmd = new SqlCommand("delete from " + tbl + " where " + colnm + "= '" + id + "'", cn);
                try
                {

                    int cnt = (int)cmd.ExecuteNonQuery();
                    if (cnt != 0)
                        // cmd.ExecuteNonQuery();
                        MessageBox.Show("Record Deleted Successfully!!!", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    //erLog.WriteErrorLog(ex.ToString());
                }
                cnclose();
            }

            //}
        }
        public string get_DataGridValue(DataGridView dg, string tbl, string colnm, int cellNo)
        {
            string id = "";
            try
            {
                cnopen();
                int i = dg.SelectedCells[0].RowIndex;
                // valueID = dataGridView1.Rows[i].Cells[3].Value.ToString();
                id = dg.Rows[i].Cells[cellNo].Value.ToString();
                cnclose();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //erLog.WriteErrorLog(ex.ToString());
            }
            return id;
        }

        //............................................


        public int GetUniqueId(string query)
        {

            SqlCommand cmd = new SqlCommand(query, cn);
            int a = 0;

            try
            {
                cnopen();
                SqlCommand cmd1 = new SqlCommand(query, cn);

                if (cmd1.ExecuteScalar().ToString() == "")
                    a = 0;
                else
                    a = Convert.ToInt32(cmd.ExecuteScalar().ToString());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //erLog.WriteErrorLog(ex.ToString());
            }
            finally
            {
                cnclose();
            }
            return a;
        }
        public void ToCsV(DataGridView dGV, string filename)
        {
            string stOutput = "";


            // Export Col Headers:
            string sHeaders = "";

            for (int j = 0; j < dGV.Columns.Count; j++)
                sHeaders = sHeaders.ToString() + Convert.ToString(dGV.Columns[j].HeaderText) + "\t";
            stOutput += sHeaders + "\r\n";

            // Export data.
            for (int i = 0; i < dGV.RowCount; i++)
            {
                string stLine = "";
                for (int j = 0; j < dGV.Rows[i].Cells.Count; j++)
                    stLine = stLine.ToString() + Convert.ToString(dGV.Rows[i].Cells[j].Value) + "\t";
                stOutput += stLine + "\r\n";
            }
            Encoding utf16 = Encoding.GetEncoding(1254);
            byte[] output = utf16.GetBytes(stOutput);
            FileStream fs = new FileStream(filename, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            string header = "Report";


            bw.Write(output, 0, output.Length); //write the encoded file
            bw.Flush();
            bw.Close();
            fs.Close();
        }
        public void withReportTitle_ToCsV(DataGridView dGV, string filename, string reportTitle)
        {
            string stOutput = "";


            // Export Report titlle:
            string stitle = "";

            stOutput += stitle + "\r\n\n\n\t\t\t\t" + reportTitle + "\r\n\r\n\r\n";

            // Export Col Headers:
            string sHeaders = "";

            for (int j = 0; j < dGV.Columns.Count; j++)
                sHeaders = sHeaders.ToString() + Convert.ToString(dGV.Columns[j].HeaderText) + "\t";
            stOutput += sHeaders + "\r\n";

            // Export data.
            for (int i = 0; i < dGV.RowCount; i++)
            {
                string stLine = "";
                for (int j = 0; j < dGV.Rows[i].Cells.Count; j++)
                    stLine = stLine.ToString() + Convert.ToString(dGV.Rows[i].Cells[j].Value) + "\t";
                stOutput += stLine + "\r\n";
            }

            //export report totals

            Encoding utf16 = Encoding.GetEncoding(1254);
            byte[] output = utf16.GetBytes(stOutput);
            FileStream fs = new FileStream(filename, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            string header = "Report";


            bw.Write(output, 0, output.Length); //write the encoded file
            bw.Flush();
            bw.Close();
            fs.Close();
        }


        // for disc report
        public void withReportTitle_ToCsV(DataGridView dGV, string filename, string reportTitle, string total, string disc, string final)
        {
            string stOutput = "";


            // Export Report titlle:
            string stitle = "";

            stOutput += stitle + "\r\n\n\n\t\t\t\t" + reportTitle + "\r\n\r\n\r\n";

            // Export Col Headers:
            string sHeaders = "";

            for (int j = 0; j < dGV.Columns.Count; j++)
                sHeaders = sHeaders.ToString() + Convert.ToString(dGV.Columns[j].HeaderText) + "\t";
            stOutput += sHeaders + "\r\n";

            // Export data.
            for (int i = 0; i < dGV.RowCount; i++)
            {
                string stLine = "";
                for (int j = 0; j < dGV.Rows[i].Cells.Count; j++)
                    stLine = stLine.ToString() + Convert.ToString(dGV.Rows[i].Cells[j].Value) + "\t";
                stOutput += stLine + "\r\n";
            }

            //export report totals
            stOutput += "\n\n\t\t Amount :\t" + total + "\t Disc Total :" + disc + "\t Final Total :" + final;
            //stOutput += "\n\n\t\t\t" + disc;
            //stOutput += "\n\n\t\t\t" + final;

            Encoding utf16 = Encoding.GetEncoding(1254);
            byte[] output = utf16.GetBytes(stOutput);
            FileStream fs = new FileStream(filename, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            string header = "Report";


            bw.Write(output, 0, output.Length); //write the encoded file
            bw.Flush();
            bw.Close();
            fs.Close();
        }

        public void withReportTitle_ToCsV_data(DataGridView dGV, string filename, string reportTitle, string Footer, string fromDate, string toDate)
        {
            string stOutput = "";


            // Export Report titlle:
            string stitle = "";

            stOutput += stitle + "\r\n\n\n\t\t\t\t\t\t" + reportTitle + "\r\n" + "\t\t\t\t\t\t" + fromDate + "\t" + toDate + "\r\n";





            // Export Col Headers:
            string sHeaders = "";

            for (int j = 0; j < dGV.Columns.Count; j++)
                sHeaders = sHeaders.ToString() + Convert.ToString(dGV.Columns[j].HeaderText) + "\t";


            stOutput += sHeaders + "\r\n";

            // Export data.
            for (int i = 0; i < dGV.RowCount; i++)
            {
                string stLine = "";
                for (int j = 0; j < dGV.Rows[i].Cells.Count; j++)
                    stLine = stLine.ToString() + Convert.ToString(dGV.Rows[i].Cells[j].Value) + "\t";

                stOutput += stLine + "\r\n";
            }

            //export report totals
            // stOutput += "\n\n\n\t\t\t\t\t\t\t Total Amount :" + total + "\t Disc Total :\t" + disc ;
            stOutput += Footer;
            //stOutput += "\n\n\t\t\t" + disc;
            //stOutput += "\n\n\t\t\t" + final;





            Encoding utf16 = Encoding.GetEncoding(1254);
            byte[] output = utf16.GetBytes(stOutput);
            FileStream fs = new FileStream(filename, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            string header = "Report";




            bw.Write(output, 0, output.Length); //write the encoded file



            bw.Flush();
            bw.Close();
            fs.Close();

        }


        public void withReportTitle_ToCsVcc(DataGridView dGV_1, string filename, string fromDate, string toDate)
        {
            string stOutput = "";
            string stitle = "";
            string reportTitle = string.Empty;
            int Food, Beverages, Liquor, Total_Discount, Total_Tax, TotalBill;
            int Food1, Beverages1, Liquor1, Total_Discount1, Total_Tax1, TotalBill1;
            // string Footer = "Here section total for particuler ";




            //get all credit company name here
            DataTable dt_companyWiseData = new DataTable();
            DataTable dt_creditCompanyName = new DataTable();
            DataTable dt_creditCompanyName1 = new DataTable();
            dt_creditCompanyName = Displaygrid(@"select c.name,c.Customer_id from total_bill tb inner join Custmer c  on tb.Customer_id=c.Customer_id where tb.status='Credit To Company' group by c.name,c.Customer_id");

            dt_creditCompanyName1 = Displaygrid(@"SELECT [order_id] As Bill_Number,[t_id] As TableNumber,[datetime] As Date,isnull([Food],0) as Food ,isnull([Beverages],0) as Beverages,isnull([Liquor],0) as Liquor ,isnull((foodDiscAmt+liquorDiscAmt+beveragesDiscAmt),0) As Total_Discount , (serviceTaxAmt+gst)AS Total_Tax ,[total_bill] As TotalBill,[wname] As UserName FROM   
                (select tob.order_id, tb.[t_id],tob.[datetime],cat.FoodSection,tob.status,tb.[foodDiscAmt],tb.[liquorDiscAmt],tb.[beveragesDiscAmt],tb.vatAmt,tb.serviceTaxAmt,tb.gst,si.total_amount,tob.Total_bill,wd.[wname],cc.[name] from 

                      total_bill tob inner join sales_item si on tob.order_id=si.order_id 
                    inner join menu m on si.menu_id=m.menu_id
                inner join category cat on m.category=cat.cat_name inner join table_order tb on tb.order_id=tob.order_id  inner join waiter_dtls wd on wd.w_id=tb.w_id inner join Custmer cc on tob.Customer_id=cc.Customer_id ) tb "
               + " PIVOT  (SUM(total_amount) FOR [FoodSection] IN ( [Food],[Liquor],[Beverages])  ) AS Tab2    WHERE        (datetime BETWEEN '" + fromDate + "' and  '" + toDate + "') and status='Credit To Company'    order by [order_id]");



            //report header here 

            stOutput += stitle + "\r\n\n\t\t\t\t\t   Creadit to Company Report \r\n" + "\t\t\t\t\t  " + fromDate + "\t" + toDate + " \r\n";
            // here is header 
            string sHeaders = "\r\n\n" + "Bill_Number" + " \t" + " Table" + "\t" + " Date" + "\t " + "Food" + "\t " + "Liquor" + "\t " + "Beverages" + "\t" + " Discount " + "\t" + "TotalTax" + "\t" + "Total" + "\t" + "User";

            stOutput += sHeaders + "\r\n";

            // for loop for get company name 1 by 1

            for (int m = 0; m < dt_creditCompanyName.Rows.Count; m++)
            {
                //bind dgv for particuler company 


                dt_companyWiseData = Displaygrid(@"SELECT [order_id] As Bill_Number,[t_id] As TableNumber,[datetime] As Date,isnull([Food],0) as Food ,isnull([Beverages],0) as Beverages,isnull([Liquor],0) as Liquor ,isnull((foodDiscAmt+liquorDiscAmt+beveragesDiscAmt),0) As Total_Discount , (serviceTaxAmt+gst)AS Total_Tax ,[total_bill] As TotalBill,[wname] As UserName FROM   
                (select tob.order_id, tb.[t_id],tob.[datetime],cat.FoodSection,tob.status,tb.[foodDiscAmt],tb.[liquorDiscAmt],tb.[beveragesDiscAmt],tb.vatAmt,tb.serviceTaxAmt,tb.gst,si.total_amount,tob.Total_bill,wd.[wname],cc.[name] from 

                      total_bill tob inner join sales_item si on tob.order_id=si.order_id 
                    inner join menu m on si.menu_id=m.menu_id
                inner join category cat on m.category=cat.cat_name inner join table_order tb on tb.order_id=tob.order_id  inner join waiter_dtls wd on wd.w_id=tb.w_id inner join Custmer cc on tob.Customer_id=cc.Customer_id  and cc.Customer_id='" + dt_creditCompanyName.Rows[m][1].ToString() + "' ) tb "
               + " PIVOT  (SUM(total_amount) FOR [FoodSection] IN ( [Food],[Liquor],[Beverages])  ) AS Tab2    WHERE        (datetime BETWEEN '" + fromDate + "' and  '" + toDate + "') and status='Credit To Company'    order by [order_id]");

                //particuler report header 

                if (dt_companyWiseData.Rows.Count != 0)
                {

                    stOutput += stitle + "\r\n\n\t" + dt_creditCompanyName.Rows[m][0].ToString() + "\r\n";
                    // Export Col Headers:



                    //for (int j = 0; j < dt_companyWiseData.Columns.Count; j++)
                    //    sHeaders = sHeaders.ToString() + Convert.ToString(dt_companyWiseData.Columns[j].) + "\t";
                    //stOutput += sHeaders + "\r\n";

                    // Export data.

                    for (int i = 0; i < dt_companyWiseData.Rows.Count; i++)
                    {
                        string stLine = "";
                        for (int j = 0; j < dt_companyWiseData.Columns.Count; j++)
                            stLine = stLine.ToString() + Convert.ToString(dt_companyWiseData.Rows[i][j].ToString()) + "\t";

                        stOutput += stLine + "\r\n";
                    }

                    //particuler report footer

                    Food = Convert.ToInt32(dt_companyWiseData.Compute("SUM(Food)", string.Empty));
                    Beverages = Convert.ToInt32(dt_companyWiseData.Compute("SUM(Beverages)", string.Empty));
                    Liquor = Convert.ToInt32(dt_companyWiseData.Compute("SUM(Liquor)", string.Empty));
                    Total_Discount = Convert.ToInt32(dt_companyWiseData.Compute("SUM(Total_Discount)", string.Empty));
                    Total_Tax = Convert.ToInt32(dt_companyWiseData.Compute("SUM(Total_Tax)", string.Empty));
                    TotalBill = Convert.ToInt32(dt_companyWiseData.Compute("SUM(TotalBill)", string.Empty));

                    stOutput += "\t\t" + "Total" + "\t" + Food.ToString() + "\t" + Beverages.ToString() + "\t" + Liquor.ToString() + "\t" + Total_Discount.ToString() + "\t" + Total_Tax.ToString() + "\t" + TotalBill.ToString();



                }


            }


            //report footer for all companys here 

            // stOutput += "\t\t\t\t"+"Report footer section ";  
            Food1 = Convert.ToInt32(dt_creditCompanyName1.Compute("SUM(Food)", string.Empty));
            Beverages1 = Convert.ToInt32(dt_creditCompanyName1.Compute("SUM(Beverages)", string.Empty));
            Liquor1 = Convert.ToInt32(dt_creditCompanyName1.Compute("SUM(Liquor)", string.Empty));
            Total_Discount1 = Convert.ToInt32(dt_creditCompanyName1.Compute("SUM(Total_Discount)", string.Empty));
            Total_Tax1 = Convert.ToInt32(dt_creditCompanyName1.Compute("SUM(Total_Tax)", string.Empty));
            TotalBill1 = Convert.ToInt32(dt_creditCompanyName1.Compute("SUM(TotalBill)", string.Empty));

            stOutput += "\r\n\t\t" + "All Total" + "\t" + Food1.ToString() + "\t" + Beverages1.ToString() + "\t" + Liquor1.ToString() + "\t" + Total_Discount1.ToString() + "\t" + Total_Tax1.ToString() + "\t" + TotalBill1.ToString();


            // Export Report titlle:









            //file creation and data binding to excel file 

            Encoding utf16 = Encoding.GetEncoding(1254);
            byte[] output = utf16.GetBytes(stOutput);
            FileStream fs = new FileStream(filename, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            //string header = "Report";

            bw.Write(output, 0, output.Length); //write the encoded file
            bw.Flush();
            bw.Close();
            fs.Close();

        }

        public void withReportTitle_ItemWiserpt(DataGridView dGV_1, string filename, string fromDate, string toDate)
        {
            string stOutput = "";
            string stitle = "";
            string reportTitle = string.Empty;
            // string Footer = "Here section total for particuler ";




            //get all credit company name here
            DataTable dt_ItemwiseData = new DataTable();
            DataTable dt_ItemWiseName = new DataTable();
            DataTable dt_ItemWiseName1 = new DataTable();
            dt_ItemWiseName = Displaygrid(@"select  cat.cat_name, cat.cat_id,cat.FoodSection from   total_bill tob inner join sales_item si on tob.order_id=si.order_id 
             inner join menu m on si.menu_id=m.menu_id 
            inner join category cat on m.category=cat.cat_name left outer join NC_itemDetails nc on tob.order_id=nc.orderId  and si.order_id=nc.orderId group by   cat.cat_name, cat.cat_id,cat.FoodSection  ");


            dt_ItemWiseName1 = Displaygrid(@" select  m.[m_name] As Item_Name,m.[rate] As Rate,(SUM(si.[qty])+sum(isnull(nc.qty,0))) as Total_Qty,SUM(si.[qty]) Paid_Qty,sum(isnull(v.[DiscAmt],0))As Discount,SUM(si.[qty]* m.[rate])As PaidAmount ,sum(isnull(nc.qty,0)) as NC_Qty,(sum(isnull(nc.qty,0))*m.[rate]) as NC_Amount
From total_bill tob inner join sales_item si on tob.order_id=si.order_id inner join menu m on si.menu_id=m.menu_id inner join category cat on m.category=cat.cat_name  " +
       " left join NC_itemDetails nc on m.m_name=nc.itemName   and si.order_id=nc.orderId    and (nc.ncDate BETWEEN '" + fromDate + "' and  '" + toDate + "') " + " left join view_itemWiseDiscount v on m.m_name=v.m_name   and (v.billDate BETWEEN '" + fromDate + "' and  '" + toDate + "') WHERE (datetime BETWEEN '" + fromDate + "' and  '" + toDate + "')  group by m.[m_name],m.rate,v.DiscAmt ");


            //report header here 
            stOutput += stitle + "\r\n\n\t\t\t\t  Item Wise Sales Report \r\n" + "\t\t\t\t" + fromDate + "\t" + toDate + " \r\n";

            // here is header 
            //string sHeaders = "\r\n\n" + " Item Name" + "\t" + "Total_Qty" + "\t" + " Paid Qty" + "\t " + "Rate" + "\t " + "Total" + "\t " + "Nc_Qty" + "\t" + " Nc_Amount";
            string sHeaders = "\r\n\n" + " Item Name" + "\t" + "Rate" + "\t" + " Total Qty" + "\t " + " Paid Qty " + "\t" + "Discount" + "\t " + "Paid Amount" + "\t " + "Nc_Qty" + "\t" + " Nc_Amount";

            stOutput += sHeaders + "\r\n";

            // for loop for get company name 1 by 1

            for (int m = 0; m < dt_ItemWiseName.Rows.Count; m++)
            {
                //bind dgv for particuler company 


                //                dt_ItemwiseData = Displaygrid(@"select  m.[m_name] As Item_Name,SUM(si.[qty]) Paid_Qty,si.[rate] As Rate,SUM(si.[qty]* si.[rate])As Total,SUM(isnull(nc.[qty],0)) As Nc_Qty,SUM(isnull(nc.[amount],0)) As Nc_Amount  From total_bill tob inner join sales_item si on tob.order_id=si.order_id 
                //             inner join menu m on si.menu_id=m.menu_id 
                //            inner join category cat on m.category=cat.cat_name left outer join NC_itemDetails nc on tob.order_id=nc.orderId  and si.order_id=nc.orderId and cat.cat_id='" + dt_ItemWiseName.Rows[m][1].ToString() + "'   WHERE (datetime BETWEEN '" + fromDate + "' and  '" + toDate + "')   group by m.[m_name],si.rate");


                //                 dt_ItemwiseData = Displaygrid(@" select  m.[m_name] As Item_Name,(SUM(si.[qty])+sum(isnull(nc.qty,0))) as Total_Qty, SUM(si.[qty]) Paid_Qty,si.[rate] As Rate,SUM(si.[qty]* si.[rate])As Total ,sum(isnull(nc.qty,0)) as NC_Qty,(sum(isnull(nc.qty,0))*si.[rate]) as NC_Amount
                //From total_bill tob inner join sales_item si on tob.order_id=si.order_id inner join menu m on si.menu_id=m.menu_id inner join category cat on m.category=cat.cat_name and cat.cat_id='" + dt_ItemWiseName.Rows[m][1].ToString() + "' "  +
                //            " left join NC_itemDetails nc on m.m_name=nc.itemName   and si.order_id=nc.orderId    and (nc.ncDate BETWEEN '" + fromDate + "' and  '" + toDate + "')  WHERE (datetime BETWEEN '" + fromDate + "' and  '" + toDate + "')  group by m.[m_name],si.rate ");





                dt_ItemwiseData = Displaygrid(@" select  m.[m_name] As Item_Name,m.[rate] As Rate,(SUM(si.[qty])+sum(isnull(nc.qty,0))) as Total_Qty,SUM(si.[qty]) Paid_Qty,sum(isnull(v.[DiscAmt],0))As Discount,SUM(si.[qty]* m.[rate])As PaidAmount ,sum(isnull(nc.qty,0)) as NC_Qty,(sum(isnull(nc.qty,0))*m.[rate]) as NC_Amount
From total_bill tob inner join sales_item si on tob.order_id=si.order_id inner join menu m on si.menu_id=m.menu_id inner join category cat on m.category=cat.cat_name and cat.cat_id='" + dt_ItemWiseName.Rows[m][1].ToString() + "' " +
           " left join NC_itemDetails nc on m.m_name=nc.itemName   and si.order_id=nc.orderId    and (nc.ncDate BETWEEN '" + fromDate + "' and  '" + toDate + "') " + " left join view_itemWiseDiscount v on m.m_name=v.m_name   and (v.billDate BETWEEN '" + fromDate + "' and  '" + toDate + "') WHERE (datetime BETWEEN '" + fromDate + "' and  '" + toDate + "')  group by m.[m_name],m.rate,v.DiscAmt ");


                //particuler report header 

                if (dt_ItemwiseData.Rows.Count != 0)
                {

                    stOutput += stitle + "\r\n\t" + dt_ItemWiseName.Rows[m][0].ToString() + "\r\n";
                    stOutput += stitle + "\r\n\t" + dt_ItemWiseName.Rows[m][2].ToString() + "\r\n\r\n";
                    // Export Col Headers:


                    // Export data.

                    for (int i = 0; i < dt_ItemwiseData.Rows.Count; i++)
                    {
                        string stLine = "";
                        for (int j = 0; j < dt_ItemwiseData.Columns.Count; j++)
                            stLine = stLine.ToString() + Convert.ToString(dt_ItemwiseData.Rows[i][j].ToString()) + "\t";

                        stOutput += stLine + "\r\n";
                    }

                    //particuler report footer

                    int Paid_Qty = Convert.ToInt32(dt_ItemwiseData.Compute("SUM(Paid_Qty)", string.Empty));
                    int PaidAmount = Convert.ToInt32(dt_ItemwiseData.Compute("SUM(PaidAmount)", string.Empty));
                    int Nc_Qty = Convert.ToInt32(dt_ItemwiseData.Compute("SUM( Nc_Qty)", string.Empty));
                    int Nc_Amount = Convert.ToInt32(dt_ItemwiseData.Compute("SUM(Nc_Amount)", string.Empty));
                    int Total_Qty = Convert.ToInt32(dt_ItemwiseData.Compute("SUM(Total_Qty)", string.Empty));
                    int Discount = Convert.ToInt32(dt_ItemwiseData.Compute("SUM(Discount)", string.Empty));


                    // stOutput += "\n" + "Total" + "\t" + Total_Qty.ToString() + "\t" + Paid_Qty.ToString() + "\t\t" + PaidAmount.ToString() + "\t" + Nc_Qty.ToString() + "\t" + Nc_Amount.ToString();
                    stOutput += "\n" + "Total" + "\t\t" + Total_Qty.ToString() + "\t" + Paid_Qty.ToString() + "\t" + Discount.ToString() + "\t" + PaidAmount.ToString() + "\t" + Nc_Qty.ToString() + "\t" + Nc_Amount.ToString();

                }

            }


            //report footer for all companys here 

            int Paid_Qty1 = Convert.ToInt32(dt_ItemWiseName1.Compute("SUM(Paid_Qty)", string.Empty));
            int PaidAmount1 = Convert.ToInt32(dt_ItemWiseName1.Compute("SUM(PaidAmount)", string.Empty));
            int Nc_Qty1 = Convert.ToInt32(dt_ItemWiseName1.Compute("SUM( Nc_Qty)", string.Empty));
            int Nc_Amount1 = Convert.ToInt32(dt_ItemWiseName1.Compute("SUM(Nc_Amount)", string.Empty));
            int Total_Qty1 = Convert.ToInt32(dt_ItemWiseName1.Compute("SUM(Total_Qty)", string.Empty));
            int Discount1 = Convert.ToInt32(dt_ItemWiseName1.Compute("SUM(Discount)", string.Empty));


            // stOutput += "\n" + "Total" + "\t" + Total_Qty.ToString() + "\t" + Paid_Qty.ToString() + "\t\t" + PaidAmount.ToString() + "\t" + Nc_Qty.ToString() + "\t" + Nc_Amount.ToString();
            stOutput += "\r\n\r\n" + "All Total" + "\t\t" + Total_Qty1.ToString() + "\t" + Paid_Qty1.ToString() + "\t" + Discount1.ToString() + "\t" + PaidAmount1.ToString() + "\t" + Nc_Qty1.ToString() + "\t" + Nc_Amount1.ToString();


            // Export Report titlle:



            //file creation and data binding to excel file 

            Encoding utf16 = Encoding.GetEncoding(1254);
            byte[] output = utf16.GetBytes(stOutput);
            FileStream fs = new FileStream(filename, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            //string header = "Report";

            bw.Write(output, 0, output.Length); //write the encoded file
            bw.Flush();
            bw.Close();
            fs.Close();

        }
        public void withReportTitle_ToCsV_DSR(DataGridView dGV, string filename, string reportTitle, string Footer, string fromDate, string toDate)
        {
            string stOutput = "";


            // Export Report titlle:
            string stitle = "";

            stOutput += stitle + "\r\n\n\n\t\t\t\t\t\t\t\t" + reportTitle + "\r\n" + "\t\t\t\t\t\t\t\t" + fromDate + "\t" + toDate + "\r\n";
            string sHeaders = "";
            string sHeaderss = "";


            sHeaderss = "\r\n\n\t" + " Total" + "\t\t\t" + "Category Sales" + "\t\t\t\t\t" + "Category Discounts" + "\t\t\t \t" + " Category sale After Discounts" + "\t\t\t\t" + "VAT Calculations" + "\t\t\t\t " + "CGST Calculations" + "\t\t\t\t" + "SGST Calculations" + "\t\t\t\t" + " Service Charge Calculations" + "\t\t\t" + "Total of Service Tax ,Service Charge, CGST, SGST, and VAT" + "\r\n";
            stOutput += sHeaderss + "\r\n";







            // Export Col Headers:


            //for (int j = 0; j < dGV.Columns.Count; j++)
            //    sHeaders = sHeaders.ToString() + Convert.ToString(dGV.Columns[j].HeaderText) + "\t";

            for (int j = 0; j < dGV.Columns.Count; j++)
            {
                if (Convert.ToString(dGV.Columns[j].HeaderText).Contains('-'))
                    sHeaders = sHeaders.ToString() + " " + "\t";
                else
                 sHeaders = sHeaders.ToString() + Convert.ToString(dGV.Columns[j].HeaderText) + "\t";  
            }
            stOutput += sHeaders + "\r\n";

            // Export data.
            for (int i = 0; i < dGV.RowCount; i++)
            {
                string stLine = "";
                for (int j = 0; j < dGV.Rows[i].Cells.Count; j++)
                    stLine = stLine.ToString() + Convert.ToString(dGV.Rows[i].Cells[j].Value) + "\t";

                stOutput += stLine + "\r\n";
            }

            //export report totals
            // stOutput += "\n\n\n\t\t\t\t\t\t\t Total Amount :" + total + "\t Disc Total :\t" + disc ;
            stOutput += Footer;
            //stOutput += "\n\n\t\t\t" + disc;
            //stOutput += "\n\n\t\t\t" + final;





            Encoding utf16 = Encoding.GetEncoding(1254);
            byte[] output = utf16.GetBytes(stOutput);
            FileStream fs = new FileStream(filename, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            string header = "Report";




            bw.Write(output, 0, output.Length); //write the encoded file



            bw.Flush();
            bw.Close();
            fs.Close();

        }
        public void autosuggest_Cell(string tbl_nm, string col_nm, TextBox txt)
        {


            SqlDataReader dreader;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = System.Data.CommandType.Text;
            AutoCompleteStringCollection acBusIDSorce = new AutoCompleteStringCollection();
            cmd.CommandText = "Select " + col_nm + " from " + tbl_nm + "";
            cnopen();
            dreader = cmd.ExecuteReader();
            if (dreader.HasRows == true)
            {
                while (dreader.Read())
                    acBusIDSorce.Add(dreader[col_nm].ToString());
            }
            else
            {
                MessageBox.Show("Data not Found");
            }
            dreader.Close();


            if (txt != null)
            {
                txt.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txt.AutoCompleteCustomSource = acBusIDSorce;
                txt.AutoCompleteSource = AutoCompleteSource.CustomSource;

            }

        }

        public void withgrp_ToCsV(DataGridView dGV, string filename)
        {
            string stOutput = "";


            // Export Report titlle:
            string stitle = "";

            // stOutput += stitle + "\r\n\n\n\t\t\t\t" + reportTitle + "\r\n\r\n\r\n";

            // Export Col Headers:
            string sHeaders = "";

            for (int j = 0; j < dGV.Columns.Count; j++)

                sHeaders = sHeaders.ToString() + Convert.ToString(dGV.Columns[j].HeaderText) + "\t";
            stOutput += sHeaders + "\r\n";

            // Export data.
            for (int i = 0; i < dGV.RowCount; i++)
            {
                string stLine = "";
                for (int j = 0; j < dGV.Rows[i].Cells.Count; j++)
                    if (dGV.Columns[0] == dGV.Columns[0])
                    {
                        sHeaders = sHeaders.ToString();
                    }
                    else
                    {
                        stLine = stLine.ToString() + Convert.ToString(dGV.Rows[i].Cells[j].Value) + "\t";
                        stOutput += stLine + "\r\n";
                    }
            }

            //export report totals

            Encoding utf16 = Encoding.GetEncoding(1254);
            byte[] output = utf16.GetBytes(stOutput);
            FileStream fs = new FileStream(filename, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            string header = "Report";


            bw.Write(output, 0, output.Length); //write the encoded file
            bw.Flush();
            bw.Close();
            fs.Close();
        }

        public void reduceMaterialFromStock(DataGridView dgv_Material)
        {
            //stock should be reduce after the sale
            string menuid, qty;
            int count = 0;
            float[] materialQty_array = new float[200];
            string[] materialName_array = new string[200];
            string[] materialUnit_array = new string[200];

            for (int i = 0; i <= dgv_Material.RowCount - 1; i++)
            {
                menuid = getDb_Value("select menu_id from menu where m_name='" + dgv_Material.Rows[i].Cells[2].Value.ToString() + "' ").ToString(); //get manu id
                qty = dgv_Material.Rows[i].Cells[3].Value.ToString(); // get the menu qty from  the DGV

                string item_nm = dgv_Material.Rows[i].Cells[2].Value.ToString();

                if (ChkDb_Value("select * from tbl_material_use where Menu_name='" + item_nm + "' "))
                {
                    //get all used material into the current order
                    string materialName = "";
                    float useMaterialQty = 0;

                    //  string item_nm = DGV_tblOrder.Rows[i].Cells[1].Value.ToString();
                    SqlCommand cmd = new SqlCommand("select * from tbl_material_use where Menu_name='" + item_nm + "' ", cn);
                    cnopen();
                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        materialName = rd["Material_name"].ToString();
                        useMaterialQty = float.Parse(rd["material_qty"].ToString());

                        materialName_array[count] = materialName;
                        materialQty_array[count] = useMaterialQty * int.Parse(qty);
                        materialUnit_array[count] = rd["material_unit"].ToString();


                    }

                    cnclose();

                    //end
                    count++;

                }
            }

            int materialIndex = 0;
            float new_qty;
            while (materialIndex != count)
            {
                //if (db.ChkDb_Value("select qty from tbl_stock where item_name='" + materialName_array[count] + "'"))
                //{
                float old_qty = getDb_Value("select qty from tbl_stock where item_name='" + materialName_array[materialIndex] + "'");

                if (materialUnit_array[materialIndex] != "Nos")
                    new_qty = old_qty - (float.Parse(materialQty_array[materialIndex].ToString()) / 1000);
                else
                    new_qty = old_qty - materialQty_array[materialIndex];

                update("update tbl_stock set qty='" + new_qty + "' where item_name='" + materialName_array[materialIndex] + "'");
                // }
                materialIndex++;
            }


        }
        public void onlyNumber(KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
        public string MenuFormation(string menuString, int menuCharSize)
        {
            if (menuString.Length < menuCharSize)
            {
                while (menuString.Length != menuCharSize)
                {
                    menuString += " ";
                }
            }
            else
            { menuString = menuString.Substring(0, menuCharSize); }
            return menuString;
        }

        internal bool confirm(string p)
        {
            throw new NotImplementedException();
        }
        public void withReportTitle_ToCsV_data(DataGridView dGV, string filename, string reportTitle, string fromDate, string toDate)
        {
            string stOutput = "";


            // Export Report titlle:
            string stitle = "";

            stOutput += stitle + "\r\n\n\n\t\t\t\t\t\t" + reportTitle + "\r\n" + "\t\t\t\t\t\t" + fromDate + "\t" + toDate + "\r\n";





            // Export Col Headers:
            string sHeaders = "";

            for (int j = 0; j < dGV.Columns.Count; j++)
                sHeaders = sHeaders.ToString() + Convert.ToString(dGV.Columns[j].HeaderText) + "\t";


            stOutput += sHeaders + "\r\n";

            // Export data.
            for (int i = 0; i < dGV.RowCount; i++)
            {
                string stLine = "";
                for (int j = 0; j < dGV.Rows[i].Cells.Count; j++)
                    stLine = stLine.ToString() + Convert.ToString(dGV.Rows[i].Cells[j].Value) + "\t";

                stOutput += stLine + "\r\n";
            }

            //export report totals
            // stOutput += "\n\n\n\t\t\t\t\t\t\t Total Amount :" + total + "\t Disc Total :\t" + disc ;

            //stOutput += "\n\n\t\t\t" + disc;
            //stOutput += "\n\n\t\t\t" + final;





            Encoding utf16 = Encoding.GetEncoding(1254);
            byte[] output = utf16.GetBytes(stOutput);
            FileStream fs = new FileStream(filename, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            string header = "Report";




            bw.Write(output, 0, output.Length); //write the encoded file



            bw.Flush();
            bw.Close();
            fs.Close();

        }
    }
}
