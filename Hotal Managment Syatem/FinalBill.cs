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
    public partial class FinalBill : Form
    {
        Database db = new Database();
        bool flagToken = false;
        int order_id = 0;
        float totalBill = 0;
      
        public Welcome wel;
        public bool billPaidFlag = false, intimateFlag = false;
        float tax,tax_amt,disc,disc_amt;
        string staus = "", table_id="0",mixPaymentMode;
        DataGridView dgv_Material;
        public string str_userName = string.Empty;

        float[] materialQty_array = new float[200];
        string[] materialName_array = new string[200];
        string[] materialUnit_array = new string[200];

        int count = 0;

        public FinalBill()
        {
            InitializeComponent();
        }

        public FinalBill(string o_id,string tbl_id,string t_bill,Welcome wel_1,DataGridView dgv )
        {
            this.order_id = int.Parse(o_id.ToString());
            this.table_id = (tbl_id.ToString());
            this.totalBill = float.Parse(t_bill.ToString());
            InitializeComponent();
            wel = wel_1;
            dgv_Material = dgv;
        }

        public FinalBill(string o_id, string tbl_id, string t_bill, Welcome wel_1, DataGridView dgv,string userName)
        {
            this.order_id = int.Parse(o_id.ToString());
            this.table_id = (tbl_id.ToString());
            this.totalBill = float.Parse(t_bill.ToString());
            InitializeComponent();
            wel = wel_1;
            dgv_Material = dgv;
            str_userName = userName;
        }
        public FinalBill(Welcome wel_1, DataGridView dgv)
        {
            InitializeComponent();
            this.wel = wel_1;
            flagToken = true;
        }
        private void FinalBill_Load(object sender, EventArgs e)
        {

            db.comboFill(cmb_cust_nm, "select * from custmer", "custmer", "name", "name");

            txt_table_no.Focus();
            db.comboFill(cmb_pay_type, "select payMode from tbl_paymentMode", "tbl_paymentMode", "payMode", "payMode");
            //asign the values to the lables 

            db.comboFill(cmb_ncItemReson, "select reason from NC_itemDetails  where reason<>''  group by reason", "NC_itemDetails", "reason", "reason");
            cmb_ncItemReson.Text = "";

            txt_table_no.Text = table_id.ToString();
            txttot_bill.Text = totalBill.ToString();
           // textBox_change.Text = totalBill.ToString();
            label_order_id.Text = order_id.ToString();
           // txt_cashmode.Text = totalBill.ToString();
            applySettings();

            //txt_amt_paid.Focus(); //focus on the amount paid

            //assign  the values to the lbl & txt
            //label_tax_value.Text = tax.ToString();
            label_discountVal.Text = disc.ToString();
           // txt_tax.Text = (float.Parse(txttot_bill.Text) * float.Parse(label_tax_value.Text) / 100).ToString();
            txt_discount.Text = (float.Parse(txttot_bill.Text) * float.Parse(label_discountVal.Text) / 100).ToString();

            cal();
            //label_Date.Text = System.DateTime.Now.ToShortDateString();    
          
        }
        void applySettings()
        {
            string disc_status = db.getDbstatus_Value("select status from tbl_option where grp='" + "Discount" + "'");
            string tax_status = db.getDbstatus_Value("select status from tbl_option where grp='" + "Tax" + "'");

            if (disc_status == "Yes")
            {
                panel_discount.Visible = false;
                disc = db.getDb_Value("select value from tbl_option where grp='" + "Discount" + "'");
            }
            
            if (tax_status == "Yes")
            {
                panel_tax.Visible = false;
                tax = db.getDb_Value("select value from tbl_option where grp='" + "Tax" + "'");
            }
             
        }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           
        private void btnBack_Click(object sender, EventArgs e)
        {
            billPaidFlag = false;
            this.Close();
        }
        public int PayModeChkCount = 0;

        public void paymode()
        {
            DateTime dt = System.DateTime.Now;
            // string date = dt.ToString("MM'-'dd'-'yyyy");
            string ddate = db.getDbstatus_Value("select ddate from tbl_dayend_status");
            string date = Convert.ToDateTime(ddate).ToString("MM'-'dd'-'yyyy");
            string table_status = string.Empty;
            string customerId = "0";
            string remark = string.Empty;

         

            //if exisintg bill paid delete record 
            db.insert("delete from total_bill where order_id='" + label_order_id.Text + "'");

            string paymentMode=string.Empty;

           if (chkCash.Checked)
            {
                paymentMode = "Cash";
                db.InsertData("INSERT INTO total_bill (order_id,Total_bill,datetime,status,Customer_id,transaction_id,remark,billTime) VALUES('" + label_order_id.Text + "','" + txt_cashmode.Text + "','" + date + "','" + paymentMode + "','" + customerId + "','" + "0" + "','" + txtCardNo.Text + "','" + System.DateTime.Now.ToString("hh:mm:ss tt") + "')", "");

                
            }
            if (Chk_debitcard.Checked)
            {
                paymentMode = "Debit Credit Card";
                db.InsertData("INSERT INTO total_bill (order_id,Total_bill,datetime,status,Customer_id,transaction_id,remark,billTime) VALUES('" + label_order_id.Text + "','" + txtCreditMode.Text + "','" + date + "','" + paymentMode + "','" + customerId + "','" + "0" + "','" + txtCardNo.Text + "','" + System.DateTime.Now.ToString("hh:mm:ss tt") + "')", "");

                
            }
            if (chk_nc.Checked)
            {
                txtCardNo.Text = cmb_ncItemReson.Text;

                paymentMode = "NC";
                //update details if bill is NC
                db.update("update table_order set foodDiscValue='0',foodDiscAmt='0', liquorDiscValue='0',liquorDiscAmt='0',beveragesDiscValue='0' ,beveragesDiscAmt='0',gst=0 where order_id='" + label_order_id.Text + "' ");
                db.update("update table_order set vatAmt=0,serviceTaxVal='0', serviceTaxFoodVal='0', serviceTaxLiquorVal='0', serviceTaxAmt='0',  serviceTaxLiquorAmt='0', serviceTaxFoodAmt='0' where order_id='" + label_order_id.Text + "'");

                //get wihout tax amount for NC bill

              string wihtoutTaxBillAMt=  db.getDbstatus_Value("select SUM(total_amount) as amount from sales_item where order_id='"+ label_order_id.Text +"'");

              db.InsertData("INSERT INTO total_bill (order_id,Total_bill,datetime,status,Customer_id,transaction_id,remark,billTime) VALUES('" + label_order_id.Text + "','" + wihtoutTaxBillAMt + "','" + date + "','" + paymentMode + "','" + customerId + "','" + "0" + "','" + txtCardNo.Text + "','" + System.DateTime.Now.ToString("hh:mm:ss tt") + "')", "");
//                db.insert(@"INSERT INTO  [NC_itemDetails]  ([orderId]   ,[tblNo]  ,[ncDate] ,[ncTime],[itemName] ,[rate] ,[qty] ,[amount],[userName] ,[reason] ) 
//                            select si.order_id,'" + txt_table_no.Text + "',si.Date,'" + System.DateTime.Now.ToString("hh:mm:ss tt") + "',m.m_name,m.rate,si.qty,si.total_amount,'" + str_userName + "','" + txtCardNo.Text + "' from sales_item  si inner join menu m on si.menu_id=m.menu_id and si.order_id='" + label_order_id.Text + "'");

              db.insert(@"INSERT INTO  [NC_itemDetails]  ([orderId]   ,[tblNo]  ,[ncDate] ,[ncTime],[itemName] ,[rate] ,[qty] ,[amount],[userName] ,[reason] ) 
                            select si.order_id,'" + txt_table_no.Text + "',si.Date,'" + System.DateTime.Now.ToString("hh:mm:ss") + "',m.m_name,m.rate,si.qty,(m.rate*si.qty),'" + str_userName + "','" + txtCardNo.Text + "' from sales_item  si inner join menu m on si.menu_id=m.menu_id and si.order_id='" + label_order_id.Text + "'");
               
                db.update(" update sales_item set  total_amount=0 where order_id='"+label_order_id.Text+"' ");
            }
            if (chkCreditToCompany.Checked)
            { 
                paymentMode = "Credit To Company";

                if (cmb_cust_nm.Text != "")
                    customerId = db.getDb_Value("select Customer_id from Custmer where  name ='" + cmb_cust_nm.Text + "'").ToString();

                db.InsertData("INSERT INTO total_bill (order_id,Total_bill,datetime,status,Customer_id,transaction_id,remark,billTime) VALUES('" + label_order_id.Text + "','" + txt_creditToComayMode.Text + "','" + date + "','" + paymentMode + "','" + customerId + "','" + cmb_cust_nm.Text + "','" + cmb_cust_nm.Text + "','" + System.DateTime.Now.ToString("hh:mm:ss tt") + "')", "");
                db.insert("insert into tbl_Cust_Receipt values('" + "0" + "','" + date + "','" + customerId + "','" + txt_amt.Text + "','" + cmb_cust_nm.Text + "')");

            }

          



        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            bool ncFlag = true;
            float payamt = float.Parse(txt_cashmode.Text) + float.Parse(txtCreditMode.Text) + float.Parse(txt_creditToComayMode.Text);
            //DateTime dt = System.DateTime.Now;
            //// string date = dt.ToString("MM'-'dd'-'yyyy");
            //string ddate = db.getDbstatus_Value("select ddate from tbl_dayend_status");
            //string date = Convert.ToDateTime(ddate).ToString("MM'-'dd'-'yyyy");
            string table_status = "";
            //string customerId = "0";
            try
            {

                if (label_order_id.Text != "0")
                {
                    //if (float.Parse(txt_amt.Text) <= float.Parse(txt_amt_paid.Text)) 
                  //  if (float.Parse(txt_amt.Text) <= float.Parse(txt_amt_paid.Text) && float.Parse(txt_amt_paid.Text) == float.Parse(payamt.ToString() ) )

                    if(chk_nc.Checked)
                    payamt = float.Parse(txt_amt_paid.Text);
                    
                    if (chk_nc.Checked && cmb_ncItemReson.Text == "" )
                    {
                        MessageBox.Show("Please Select NC Reson", "RestroSoft", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ncFlag = false;
                         
                    }

                    if (float.Parse(txt_amt.Text) <= float.Parse(txt_amt.Text) && float.Parse(txt_amt.Text) == float.Parse(payamt.ToString()) && ncFlag)
                    {

                        if ( PayModeChkCount <= 2 && PayModeChkCount!=0 )
                        {

                           
                                paymode();

                                if (true)
                                {

                                    MessageBox.Show("Bill Paid Successfully...!", "RestroSoft", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    if (db.ChkDb_Value("SELECT        t_id FROM  table_order WHERE       (t_id like '%T%') and order_id='" + label_order_id.Text + "' "))
                                    {

                                        db.update("update tbl_parcel_order set order_status='Paid' where order_id='" + label_order_id.Text + "'");

                                    }


                                    billPaidFlag = true;
                                    //  db.insert("insert into tbl_Cust_Receipt values('" + "0" + "','" + date + "','" + customerId + "','" + txt_amt.Text + "','" + cmb_cust_nm.Text + "')");

                                    //if (flagToken)
                                    //{
                                   
                                    if (db.ChkDb_Value("Select * from total_bill where order_id='" + label_order_id.Text + "'"))
                                    {

                                        // here table bill paid then assign table to empty
                                        table_status = "Empty";
                                        db.update("UPDATE table_status SET status='" + table_status + "' WHERE t_id='" + txt_table_no.Text + "'");
                                        db.update("UPDATE table_order SET tableStatus='Empty' WHERE t_id='" + txt_table_no.Text + "' and order_id='" + label_order_id.Text + "'");
                                        label_order_id.Text = "0";
                                    }

                                    // }

                                    //stock should be reduce after the sale 
                                    txtCardNo.Text = "";
                                    db.DrinkstockUpdate(this.dgv_Material);

                                    wel.green_btns((txt_table_no.Text.ToString()));
                                    wel.chk_tb_status();
                                    this.Close();

                                }  // inner if 


                           


                        }//chk pay out option is only 2
                         else                  
                              MessageBox.Show("Select Only 2 Payment Mode.");


                    } //chk amount eql to given amt
                    else
                        MessageBox.Show("Please Pay the All Amount,", "RestroSoft", MessageBoxButtons.OK, MessageBoxIcon.Information);

                
                } // order id chk if end here 
                  else  
                    MessageBox.Show("Order id Should Not be Zero. ");



            }

            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }


        private void txt_amt_paid_TextChanged(object sender, EventArgs e)
        {
            float total, paid, rem;
            if (txt_amt_paid.Text != "")
            {
                total = float.Parse(txt_amt.Text.ToString());
                paid = float.Parse(txt_amt_paid.Text.ToString());
                rem = paid - total;
               // rem =total-paid;
                textBox_change.Text = rem.ToString();
            }
           
        }

        private void txt_amt_paid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            //only allow one decimal point

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = System.DateTime.Now.ToString("hh:mm:ss tt");
           // lblDate.Text = System.DateTime.Now.ToString("dd-MM-yyyy");
            string date = db.getDbstatus_Value("select ddate from tbl_dayend_status");
            lblDate.Text = date.ToString();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                label2.Visible = true;
                cmb_cust_nm.Visible = true;
                linkLabel_addNewCust.Visible = true;
                //bind customer list
                db.comboFill(cmb_cust_nm, "select * from custmer", "custmer", "name", "name");
            }
            else
            {
                label2.Visible = false;
                cmb_cust_nm.Visible = false;
                linkLabel_addNewCust.Visible = false;

            }
        }

        private void txt_tax_TextChanged(object sender, EventArgs e)
        {
            cal();
        }

        private void txt_disc_TextChanged(object sender, EventArgs e)
        {

        }
        public void cal()
        {


            //calculate and assign the values to txtbox
            //if (txt_tax.Text != "" && txt_discount.Text != "")
            //{

            //    txt_amt.Text = Math.Round((float.Parse(txttot_bill.Text) - float.Parse(txt_discount.Text) + float.Parse(txt_tax.Text))).ToString();
            //}
            ////end here

            txt_amt.Text = totalBill.ToString();

            txt_amt_paid.Text = txt_amt.Text;

        }

        private void linkLabel_addNewCust_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AddCustmer cust = new AddCustmer();
            cust.ShowDialog();
            db.comboFill(cmb_cust_nm, "select * from custmer", "custmer", "name", "name");

        }

        private void txt_discount_TextChanged(object sender, EventArgs e)
        {
            cal();
        }

        private void txt_table_no_Leave(object sender, EventArgs e)
        {
            
        }

        private void cmb_pay_type_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            panel_trans.Visible = false;
            linkLabel_addNewCust.Visible = true;
            panel_Mixed.Visible = false;
            panel_c_nm.Visible = false;
            label_remark.Visible = false;
            
            label_tid.Visible = false;






            if (cmb_pay_type.Text == "Credit To Company")
            {
                label2.Visible = true;
                cmb_cust_nm.Visible = true;
                panel_c_nm.Visible = true;               
                //bind customer list
                db.comboFill(cmb_cust_nm, "select * from custmer", "custmer", "name", "name");
            }
            else if (cmb_pay_type.Text == "Card")
            {
                panel_trans.Visible = true;               
                label_tid.Visible = true;              

            }           
            else if (cmb_pay_type.Text == "Mixed Mode")
            {
                panel_trans.Visible = true;                
                label_tid.Visible = true;               
                panel_Mixed.Visible = true;
              
            }
            else if (cmb_pay_type.Text != "Debit Credit Card" && cmb_pay_type.Text != "Credit To Company" && cmb_pay_type.Text != "Mixed Mode")
            {
                panel_trans.Visible = true;
                label_remark.Visible = true;
               

            }
            else if (cmb_pay_type.Text != "NC" )
            {
                panel_trans.Visible = true;
                label_remark.Visible = true;


            }
            
       

        }

        private void txt_table_no_TextChanged(object sender, EventArgs e)
        {
            if (txt_table_no.Text != "")
            {
                float o_id = db.getDb_Value("select max(order_id) from table_order where t_id='" + txt_table_no.Text + "' and tableStatus='Processing'");
                label_order_id.Text = o_id.ToString();

                float total_bill = db.getDb_Value("SELECT SUM(total_amount) AS Total FROM   sales_item WHERE order_id='" + label_order_id.Text + "'");

                txttot_bill.Text = total_bill.ToString();
              
                //yogesh 22.02.2019  

                txt_amt.Text = total_bill.ToString();


                ////
                label_tax_value.Text = tax.ToString();
                label_discountVal.Text = disc.ToString();

                //txt_tax.Text = (float.Parse(txttot_bill.Text) * float.Parse(label_tax_value.Text) / 100).ToString();
                //txt_discount.Text = (float.Parse(txttot_bill.Text) * float.Parse(label_discountVal.Text) / 100).ToString();

                //applySettings();
                //if (txt_amt.Text == "0")
                //{
                //    cal();
                //}
            }
        }

        private void txtCash_TextChanged(object sender, EventArgs e)
        {
            if (txtCash.Text != "")
            {
                if (float.Parse(txt_amt.Text) < float.Parse(txtCash.Text))
                {
                    MessageBox.Show("Amount Should be Less than Actual Amount...");
                    txtCash.Text = "0";
                    txtCard.Text = "0";

                }
                else
                {
                    txtCard.Text = (float.Parse(txt_amt.Text) - float.Parse(txtCash.Text)).ToString();
                }
            }
        }

        private void txtCard_TextChanged(object sender, EventArgs e)
        {
            if (txtCard.Text != "")
            {
                if (float.Parse(txt_amt.Text) < float.Parse(txtCash.Text))
                {
                    MessageBox.Show("Amount Should be Less than Actual Amount...");
                    txtCash.Text = "0";
                    txtCard.Text = "0";
                }
                else
                {
                    txtCash.Text = (float.Parse(txt_amt.Text) - float.Parse(txtCard.Text)).ToString();
                }
            }
        }

        private void txtCash_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);
        }

        private void txtCard_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            CardPayment cardPay = new CardPayment(txt_amt.Text);
            cardPay.ShowDialog();
        }

        private void rdbPaytm_CheckedChanged(object sender, EventArgs e)
        {
          
                panel_trans.Visible = false;
           
        }

        private void rdbCard_CheckedChanged(object sender, EventArgs e)
        {
            panel_trans.Visible = true;
        }

        private void chkCash_CheckedChanged(object sender, EventArgs e)
        {
            chk_paymodeCounter(chkCash);
            if (chkCash.Checked)
            {

                float remain = float.Parse(txt_amt.Text) - float.Parse(txtCreditMode.Text) - float.Parse(txt_creditToComayMode.Text);

                txt_cashmode.Text = remain.ToString();
                //txt_cashmode.Text = txt_amt.Text;
                PayModeChkCount++;
            }
            else
            {
                txt_cashmode.Text = "0";
                PayModeChkCount--;
            }
        }

        private void Chk_debitcard_CheckedChanged(object sender, EventArgs e)
        {
            chk_paymodeCounter(Chk_debitcard);
            if (Chk_debitcard.Checked)
            {
                float remain = float.Parse(txt_amt.Text) - float.Parse(txt_cashmode.Text) - float.Parse(txt_creditToComayMode.Text);

                txtCreditMode.Text = remain.ToString();

               // txtCreditMode.Text = txt_amt.Text;
                panel_trans.Visible = true;
                PayModeChkCount++;

                //convert amount cash and card
              

            }
            else
            {
                txtCreditMode.Text = "0";
                PayModeChkCount--;
                panel_trans.Visible = false;
            }
        }

        public void chk_paymodeCounter(CheckBox chk)
        {
            //if (PayModeChkCount >= 2)
            //{
            //    MessageBox.Show("Please Select only 2 Payment mode.");
            //    chk.Checked = false;
            //}
               
        }
        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {

            chk_paymodeCounter(chk_nc);
            if (chk_nc.Checked)
            {
                PayModeChkCount++;
                panel_trans.Visible = true;
                chkCash.Checked = false;
            }
            else
            {
                PayModeChkCount--;
                panel_trans.Visible = false;
            }


        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            chk_paymodeCounter(chkCreditToCompany);
            if (chkCreditToCompany.Checked)
            {
                float remain = float.Parse(txt_amt.Text) - float.Parse(txt_cashmode.Text) - float.Parse(txtCreditMode.Text);

                txt_creditToComayMode.Text = remain.ToString();

               // txt_creditToComayMode.Text = txt_amt.Text;
                panel_c_nm.Visible = true;
                PayModeChkCount++;
            }
            else
            {
                txt_creditToComayMode.Text = "0";
                panel_c_nm.Visible = false;
                PayModeChkCount--;
            }
        }

        private void txtCreditMode_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);
        }

        private void txt_creditToComayMode_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);
        }

        private void txt_cashmode_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);
        }

        private void cmb_ncItemReson_SelectedIndexChanged(object sender, EventArgs e)
        {
           // txtCardNo.Text = cmb_ncItemReson.Text;
        }

        private void txt_amt_TextChanged(object sender, EventArgs e)
        {

        }

        private void txttot_bill_TextChanged(object sender, EventArgs e)
        {

        }
    }   
}



//                        //paid the bill and stor it to the db
//                        if (checkBox1.Checked)
//                        {
//                            staus = "Credit";
//                        }
//                        else
//                        {
//                            staus = "Cash";
//                        }

//                        if (rdbCard.Checked)
//                        {
//                            mixPaymentMode = "Card";
//                        }
//                        else
//                        {
//                            mixPaymentMode = "Paytm";
//                            txtCardNo.Text = "0";
//                        }

//                        //get customer id from the db whn customer name is selected

//                        if (cmb_cust_nm.Text != "")
//                            customerId = db.getDb_Value("select Customer_id from Custmer where  name ='" + cmb_cust_nm.Text + "'").ToString();
//                        if (cmb_pay_type.Text == "Card")
//                        {
//                            db.InsertData("INSERT INTO total_bill (order_id,Total_bill,datetime,status,Customer_id,transaction_id,remark,billTime) VALUES('" + label_order_id.Text + "','" + txt_amt.Text + "','" + date + "','" + cmb_pay_type.Text + "','" + customerId + "','" + txtCardNo.Text + "','" + "0" + "','" + System.DateTime.Now.ToString("hh:mm:ss tt") + "')", "");
//                        }
//                        if (cmb_pay_type.Text == "Credit")
//                        {
//                            db.InsertData("INSERT INTO total_bill (order_id,Total_bill,datetime,status,Customer_id,transaction_id,remark,billTime) VALUES('" + label_order_id.Text + "','" + txt_amt.Text + "','" + date + "','" + cmb_pay_type.Text + "','" + customerId + "','" + txtCardNo.Text + "','" + "0" + "','" + System.DateTime.Now.ToString("hh:mm:ss tt") + "')", "");
//                            db.insert("insert into tbl_Cust_Receipt values('" + "0" + "','" + date + "','" + customerId + "','" + txt_amt.Text + "','" + cmb_cust_nm.Text + "')");

//                        }                        

//                        if (cmb_pay_type.Text == "Mixed Mode")
//                        {
//                            if (txtCardNo.Text == "")
//                            {
//                                MessageBox.Show("Please Enter Transaction Card.");
//                                intimateFlag = true;
//                            }
//                            else if (txtCard.Text == "0" || txtCash.Text == "0")
//                            {
//                                MessageBox.Show("Please Enter all Amount...");
//                                intimateFlag = true;
//                            }

//                            else
//                            {
//                                db.InsertData("INSERT INTO total_bill (order_id,Total_bill,datetime,status,Customer_id,transaction_id,remark,billTime) VALUES('" + label_order_id.Text + "','" + txtCash.Text + "','" + date + "','Cash','" + customerId + "','" + "0" + "','" + "0" + "','" + System.DateTime.Now.ToString("hh:mm:ss tt") + "')", "");
//                                db.InsertData("INSERT INTO total_bill (order_id,Total_bill,datetime,status,Customer_id,transaction_id,remark,billTime) VALUES('" + label_order_id.Text + "','" + txtCard.Text + "','" + date + "','" + mixPaymentMode + "','" + customerId + "','" + txtCardNo.Text + "','" + "0" + "','" + System.DateTime.Now.ToString("hh:mm:ss tt") + "')", "");
//                                intimateFlag = false;
//                            }
//                        }

//                        if (cmb_pay_type.Text != "Card" && cmb_pay_type.Text != "Credit" && cmb_pay_type.Text != "Mixed Mode")
//                        {
//                            db.InsertData("INSERT INTO total_bill (order_id,Total_bill,datetime,status,Customer_id,transaction_id,remark,billTime) VALUES('" + label_order_id.Text + "','" + txt_amt.Text + "','" + date + "','" + cmb_pay_type.Text + "','" + customerId + "','" + "0" + "','" + txtCardNo.Text + "','" + System.DateTime.Now.ToString("hh:mm:ss tt") + "')", "");
//                        }


//                        //add NC order to NC Item Details

//                        if (cmb_pay_type.Text == "NC")
//                        {
//                           db.insert(@"INSERT INTO  [NC_itemDetails]  ([orderId]   ,[tblNo]  ,[ncDate] ,[ncTime],[itemName] ,[rate] ,[qty] ,[amount],[userName] ,[reason] ) 
//                            select si.order_id,'" + txt_table_no.Text + "',si.Date,'" + System.DateTime.Now.ToString("hh:mm:ss tt") + "',m.m_name,si.rate,si.qty,si.total_amount,'" + str_userName + "','" + txtCardNo.Text + "' from sales_item  si inner join menu m on si.menu_id=m.menu_id and si.order_id='" + label_order_id.Text + "'");

//                        }