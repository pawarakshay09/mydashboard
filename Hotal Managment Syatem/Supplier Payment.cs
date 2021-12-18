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
    public partial class Purchase_Bill_Payment : Form
    {
        Database db = new Database();
        int count = 0;
        List<int> ChkedRow = new List<int>();
          string [] Bill_numbers = new string[200];
        string[] Bill_status = new string[200];
        string[] Bill_type = new string[200];
        bool paymentFlag = false;
        public string vendor_nm, bill_no,bank_nm,cheq_no,amount,date,voucher_no,patType,userNm;
        int c = 0;
        public Purchase_Bill_Payment()
        {
            InitializeComponent();
        }
        public Purchase_Bill_Payment(string userName)
        {
            InitializeComponent();
            this.userNm = userName;
        }
        public Purchase_Bill_Payment(string userName, string vendorNm)
        {
            InitializeComponent();
            this.userNm = userName;
            this.vendor_nm = vendorNm;
            paymentFlag = true;

        }

        private void Purchase_Bill_Payment_Load(object sender, EventArgs e)
        {
            db.formFix(this);
            this.CancelButton = btn_close;


            if (paymentFlag)
                cmb_vendor.Text = vendor_nm;
            else
            {
                db.comboFill(cmb_vendor, "select * from supplier_dtls order by sup_name asc", "supplier_dtls", "sup_name", "sup_name");

                cmb_vendor.Text = "-- Select Name --";
            }

            //db.comboFill(cmb_vendor, "select * from Supplier where type='Supplier' order by supplierName asc", "Supplier", "supplierName", "supplierName");

            //cmb_vendor.Text = "-- Select Name --";


            db.comboFill(cmb_banknm, "select distinct bankName from PaymentDetails ", "PaymentDetails", "bankName", "bankName");

           // count = int.Parse(db.getDb_Value(" SELECT detailsId FROM  tbl_purchase_payment_dtls order by detailsId  desc").ToString());
            count = int.Parse(db.getDb_Value(" SELECT voucherNo FROM  PaymentDetails order by voucherNo  desc").ToString());

            
           count += 1;

            txt_voucherno.Text = count.ToString();
            if (db.ChkDb_Value("select distinct bankName from PaymentDetails"))
            {
                db.comboFill(cmb_banknm, "select distinct bankName from PaymentDetails", "PaymentDetails", "bankName", "bankName");
            }
            //bind Dgv
            //dataGridView1.DataSource = db.Displaygrid("SELECT tbl_purchase_payment_master.date, tbl_purchase_payment_dtls.vendor_nm, tbl_purchase_payment_dtls.purchase_bill_no, tbl_purchase_payment_master.Grand_Total, SUM(tbl_purchase_payment_dtls.paid_amt) AS PaidAmount, tbl_purchase_payment_master.Grand_Total - SUM(tbl_purchase_payment_dtls.paid_amt) AS BalanceAmount, tbl_purchase_payment_master.payment_status FROM            tbl_purchase_payment_master INNER JOIN tbl_purchase_payment_dtls ON tbl_purchase_payment_master.purchase_bill_no = tbl_purchase_payment_dtls.purchase_bill_no GROUP BY tbl_purchase_payment_master.date, tbl_purchase_payment_dtls.vendor_nm, tbl_purchase_payment_dtls.purchase_bill_no,  tbl_purchase_payment_master.Grand_Total, tbl_purchase_payment_master.payment_status"); 
            
            //dataGridView1.DataSource = db.Displaygrid("SELECT tbl_purchase_payment_master.date, tbl_purchase_payment_dtls.vendor_nm, tbl_purchase_payment_dtls.purchase_bill_no,  tbl_purchase_payment_master.Grand_Total, SUM(tbl_purchase_payment_master.paid_amt) AS PaidAmount,  tbl_purchase_payment_master.Grand_Total - SUM(tbl_purchase_payment_master.paid_amt) AS BalanceAmount,  tbl_purchase_payment_master.payment_status FROM tbl_purchase_payment_master INNER JOIN   tbl_purchase_payment_dtls ON tbl_purchase_payment_master.purchase_bill_no = tbl_purchase_payment_dtls.purchase_bill_no GROUP BY tbl_purchase_payment_master.date, tbl_purchase_payment_dtls.vendor_nm, tbl_purchase_payment_dtls.purchase_bill_no,  tbl_purchase_payment_master.Grand_Total, tbl_purchase_payment_master.payment_status");
            dataGridView1.DataSource = db.Displaygrid("SELECT   date as Date, supplierName as [Supplier Name], billNo as [Bill No],grandTotal as [Grand Total], paidAmt as [Paid Amt], dueAmt as [Balance], paymentStatus as [Status] FROM            PaymentMaster where paymentStatus='Unpaid'  and supplierName='" + cmb_vendor.Text + "' GROUP BY billNo, date, supplierName, grandTotal, paidAmt, dueAmt, paymentStatus");
            dataGridView1.Columns[2].Width = 180;
            dataGridView1.Columns[3].Width = 80;
            dataGridView1.Columns[4].Width = 120;
            dataGridView1.Columns[5].Width = 120;
            dataGridView1.Columns[6].Width = 85;
            dataGridView1.Columns[7].Width = 80;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (rdb_cheque.Checked == true && txt_cheque.Text == "")
            {
                errorProvider1.SetError(txt_cheque, "Please Enter Check Number");
            }
            else if (rdb_cheque.Checked == true && cmb_banknm.Text == "")
            {
                if (cmb_banknm.Text == "")
                {
                    errorProvider1.SetError(cmb_banknm, "Please Select Bank Name");
                }
                errorProvider1.SetError(cmb_banknm, "Please Select Bank Name");
            }
            else if (cmb_vendor.Text == "")
            {
                errorProvider1.SetError(cmb_vendor, "Please Select Vendor Name");
            }
            else if (cmb_vendor.Text == "-- Select Name --")
            {
                errorProvider1.SetError(cmb_vendor, "Please Select Vendor Name");
            }
            else
            {
                errorProvider1.Clear();
                int k = 0;
                string modeOfPay = "";
                float remmingBillAmt = 0;
                float totalPaidAmount, temp_paidAmt;
                string billStatus = "Unpaid";

                bool flagtemp = false;
                string date = dtp_payment.Value.ToString("MM-dd-yyyy");

                // //get here total paid amount 

                if (txt_paid.Text != "")
                    totalPaidAmount = float.Parse(txt_paid.Text.ToString());
                temp_paidAmt = float.Parse(txt_paid.Text.ToString());
                //get the bill numbers which is selected
                if (db.confirm())
                {
                    if (c == 1)
                    {
                        
                        string billno = Bill_numbers[k].ToString();
                        string bill_status_1 = Bill_status[k].ToString();
                        //modeOfPay;
                        if (rdb_cash.Checked)
                            modeOfPay = "Cash";
                        else
                            modeOfPay = "Cheque";

                        if (billno != "0" && bill_status_1 != "")
                        {
                            //get the total bill paid amount && substract from the remming amount 


                            remmingBillAmt = float.Parse(db.getDb_Value("SELECT grandTotal -(select SUM (paidAmt) AS Balance FROM   PaymentDetails WHERE billNo = '" + billno + "'  and supplierName='" + cmb_vendor.Text + "' )  from PaymentMaster where billNo='" + billno + "'  and supplierName='" + cmb_vendor.Text + "'  GROUP BY billNo,grandTotal").ToString());
                            
                            //if bill is paid make bill status is paid
                            if (remmingBillAmt <= temp_paidAmt)
                            {
                                billStatus = "Paid";
                                temp_paidAmt -= remmingBillAmt;

                                if (modeOfPay == "Cheque")
                                {
                                    errorProvider1.Clear();

                                    if (txt_cheque.Text == "")
                                    {
                                        errorProvider1.SetError(txt_cheque, "Please Enter cheque No");
                                    }
                                    else if (cmb_banknm.Text == "0")
                                    {
                                        errorProvider1.SetError(cmb_banknm, "Please Enter bank Name");

                                    }
                                  //  string chk_date = dtp_payment.Value.ToString("MM-dd-yyyy");

                                    db.insert("insert into PaymentDetails values('" + modeOfPay + "','" + txt_voucherno.Text + "','" + Bill_numbers[k] + "','" + cmb_vendor.Text + "','" + txtbal_amt.Text + "','" + remmingBillAmt + "','" + temp_paidAmt + "','" + date + "','" + cmb_banknm.Text + "','" + txt_cheque.Text + "','" + date + "','" + billStatus + "','" + "Unclear" + "','" + txt_bill.Text + "')");
                                }
                                else
                                {
                                    db.insert("insert into PaymentDetails values('" + modeOfPay + "','" + txt_voucherno.Text + "','" + Bill_numbers[k] + "','" + cmb_vendor.Text + "','" + txtbal_amt.Text + "','" + remmingBillAmt + "','" + temp_paidAmt + "','" + date + "','" + "0" + "','" + "0" + "','" + date + "','" + billStatus + "','" + "0" + "','" + txt_bill.Text + "')");
                                }
                               
                            }
                            else
                            {
                                remmingBillAmt = remmingBillAmt - temp_paidAmt;
                                billStatus = "Unpaid";


                                if (modeOfPay == "Cheque")
                                {
                                    string chkdate = dtp_payment.Value.ToString("MM/dd/yyyy");
                                    db.insert("insert into PaymentDetails values('" + modeOfPay + "','" + txt_voucherno.Text + "','" + Bill_numbers[k] + "','" + cmb_vendor.Text + "','" + txtbal_amt.Text + "','" + temp_paidAmt + "','" + remmingBillAmt + "','" + date + "','" + cmb_banknm.Text + "','" + txt_cheque.Text + "','" + chkdate + "','" + billStatus + "','" + "Unclear" + "','" + txt_bill.Text + "')");
                                }
                                else
                                {
                                    db.insert("insert into PaymentDetails values('" + modeOfPay + "','" + txt_voucherno.Text + "','" + Bill_numbers[k] + "','" + cmb_vendor.Text + "','" + txtbal_amt.Text + "','" + temp_paidAmt + "','" + remmingBillAmt + "','" + date + "','" + "0" + "','" + "0" + "','" + "0:0" + "','" + billStatus + "','" + "0" + "','" + txt_bill.Text + "')");
                                }
                                temp_paidAmt = 0;
                            }

                            //get the paid amount of bills
                          //  string paidAmt = db.getDb_Value("select sum(paidAmt) from PaymentDetails where billNo='" + billno + "' and supplierName='" + cmb_vendor.Text + "'").ToString();
                            string paidAmt = db.getDb_Value("SELECT        SUM(PaymentDetails.paidAmt) AS Expr1 FROM            PaymentDetails  WHERE        (PaymentDetails.billNo = '" + billno + "') AND (PaymentDetails.supplierName = '" + cmb_vendor.Text + "') ").ToString();
                            
                            string total = db.getDb_Value("select sum(grandTotal) from PaymentMaster where billNo='" + billno + "' and supplierName='" + cmb_vendor.Text + "' ").ToString();
                            // update query for the tbl_Receipt_Master billStatus
                            string paid = (float.Parse(paidAmt) + (temp_paidAmt)).ToString();

                            float dueAmt = (float.Parse(total) - float.Parse(paidAmt));

                            db.update("update PaymentMaster set voucherNo='" + txt_voucherno.Text + "',paymentStatus='" + billStatus + "', paidAmt='" + paidAmt + "',dueAmt='" + dueAmt + "'  WHERE billNo = '" + billno + "'  and supplierName='" + cmb_vendor.Text + "' ");

                        }

                    }

                    else
                    {
                       
                        for (k = 0; k < c; k++)
                        {
                            string billno = Bill_numbers[k].ToString();
                            string bill_status_1 = Bill_status[k].ToString();
                           
 
                            //modeOfPay;
                            if (rdb_cash.Checked)
                                modeOfPay = "Cash";
                            else
                                modeOfPay = "Cheque";
                            if (billno != "0" && bill_status_1 != "")
                            {
                                //get the total bill paid amount && substract from the remming amount 


                                remmingBillAmt = float.Parse(db.getDb_Value("SELECT grandTotal -(select SUM (paidAmt) AS Balance FROM   PaymentDetails WHERE billNo = '" + billno + "'  and supplierName='" + cmb_vendor.Text + "' )  from PaymentMaster where billNo='" + billno + "'  and supplierName='" + cmb_vendor.Text + "'  GROUP BY billNo,grandTotal").ToString());
                            
                                //if bill is paid make bill status is paid
                                if (remmingBillAmt <= temp_paidAmt)
                                {
                                    billStatus = "Paid";
                                    temp_paidAmt -= remmingBillAmt;
                                    string chk_date = dtp_payment.Value.ToString("MM-dd-yyyy");
                                    if (modeOfPay == "Cheque")
                                    {
                                        errorProvider1.Clear();

                                        if (txt_cheque.Text == "")
                                        {
                                            errorProvider1.SetError(txt_cheque, "Please Enter cheque No");
                                        }
                                        else if (cmb_banknm.Text == "0")
                                        {
                                            errorProvider1.SetError(cmb_banknm, "Please Enter bank Name");

                                        }
                                      

                                        db.insert("insert into PaymentDetails values('" + modeOfPay + "','" + txt_voucherno.Text + "','" + Bill_numbers[k] + "','" + cmb_vendor.Text + "','" + txtbal_amt.Text + "','" + remmingBillAmt + "','" + temp_paidAmt + "','" + chk_date + "','" + cmb_banknm.Text + "','" + txt_cheque.Text + "','" + chk_date + "','" + billStatus + "','" + "Unclear" + "','" + txt_bill.Text + "')");
                                    }
                                    else
                                    {
                                        db.insert("insert into PaymentDetails values('" + modeOfPay + "','" + txt_voucherno.Text + "','" + Bill_numbers[k] + "','" + cmb_vendor.Text + "','" + txtbal_amt.Text + "','" + remmingBillAmt + "','" + temp_paidAmt + "','" + chk_date + "','" + "0" + "','" + "0" + "','" + chk_date + "','" + billStatus + "','" + "0" + "','" + txt_bill.Text + "')");
                                    }
                                    ////update in master payment
                                    //db.update("update tbl_purchase_payment_master set payment_status='" + billStatus + "', paid_amt='" + remmingBillAmt + "',due_amt='0'  WHERE purchase_bill_no = '" + billno + "'");


                                }
                                else
                                {
                                    remmingBillAmt = remmingBillAmt - temp_paidAmt;
                                    billStatus = "Unpaid";

                                        string chkdate = dtp_payment.Value.ToString("MM/dd/yyyy");
                                    if (modeOfPay == "Cheque")
                                    {

                                        db.insert("insert into PaymentDetails values('" + modeOfPay + "','" + txt_voucherno.Text + "','" + Bill_numbers[k] + "','" + cmb_vendor.Text + "','" + txtbal_amt.Text + "','" + temp_paidAmt + "','" + remmingBillAmt + "','" + date + "','" + cmb_banknm.Text + "','" + txt_cheque.Text + "','" + date + "','" + billStatus + "','" + "Unclear" + "','" + txt_bill.Text + "')");
                                    }
                                    else
                                    {
                                        db.insert("insert into PaymentDetails values('" + modeOfPay + "','" + txt_voucherno.Text + "','" + Bill_numbers[k] + "','" + cmb_vendor.Text + "','" + txtbal_amt.Text + "','" + temp_paidAmt + "','" + remmingBillAmt + "','" + date + "','" + "0" + "','" + "0" + "','" + date + "','" + billStatus + "','" + "0" + "','" + txt_bill.Text + "')");
                                    }
                                    temp_paidAmt = 0;
                                }

                                //get the paid amount of bills
                                string paidAmt = db.getDb_Value("select sum(paidAmt) from PaymentDetails where billNo='" + billno + "' and supplierName='" + cmb_vendor.Text + "'").ToString();
                                string total = db.getDb_Value("select sum(grandTotal) from PaymentMaster where billNo='" + billno + "' and supplierName='" + cmb_vendor.Text + "' ").ToString();
                                // update query for the tbl_Receipt_Master billStatus
                                string paid = (float.Parse(paidAmt) + (temp_paidAmt)).ToString();

                                float dueAmt = (float.Parse(total) - float.Parse(paidAmt));

                                db.update("update PaymentMaster set voucherNo='" + txt_voucherno.Text + "',paymentStatus='" + billStatus + "', paidAmt='" + paidAmt + "',dueAmt='" + dueAmt + "'  WHERE billNo = '" + billno + "'  and supplierName='" + cmb_vendor.Text + "' ");
                                //and tbl_Receipt_Master.billtype='" + bill_status_1 + "' ");

                                //  dataGridView1.DataSource = db.Displaygrid("  SELECT tbl_Receipt_Details.billNo,tbl_Receipt_Master.partyname,  tbl_Receipt_Master.status, tbl_Receipt_Master.billtype,tbl_Receipt_Master.tot_amt as Total_Amount, sum(tbl_Receipt_Details.paid_amt)as Paid_Amount,tot_amt-sum(tbl_Receipt_Details.paid_amt) as Balance FROM       tbl_Receipt_Master INNER JOIN tbl_Receipt_Details ON tbl_Receipt_Master.billNo = tbl_Receipt_Details.billNo  where tbl_Receipt_Master.partyname='" + cmb_party.Text + "' and billtype='" + cmb_bill_type.Text + "' and status='" + cmbBox_BillStatus.Text + "' and tbl_Receipt_Master.billtype='" + Bill_type[k] + "'  group by tbl_Receipt_Details.billNo,tbl_Receipt_Master.partyname,tbl_Receipt_Master.tot_amt,tbl_Receipt_Master.status,tbl_Receipt_Master.billtype  ");


                            }

                        }
                    }
                        MessageBox.Show("Record inserted successfully");
                        
                        Bind_DGV();

                        clear();
                        //count = int.Parse(db.getDb_Value(" SELECT detailsId FROM  tbl_purchase_payment_dtls order by detailsId  desc").ToString());
                        count = int.Parse(db.getDb_Value(" SELECT voucherNo FROM  PaymentDetails order by voucherNo  desc").ToString());


                        count += 1;

                        txt_voucherno.Text = count.ToString();
                        if (db.ChkDb_Value("select distinct bankName from PaymentDetails"))
                        {
                            db.comboFill(cmb_banknm, "select distinct bankName from PaymentDetails", "PaymentDetails", "bankName", "bankName");
                        }
                        double sum = 0;
                        for (int i = 0; i < dataGridView1.RowCount; i++)
                        {
                            sum += double.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString());
                        }
                        txt_total.Text = sum.ToString();
                    
                }
            }
        }
        void Bind_DGV()
        {

             //dataGridView1.DataSource = db.Displaygrid("SELECT        tbl_purchase_payment_master.date, tbl_purchase_payment_master.vendor_nm, tbl_purchase_payment_master.purchase_bill_no,tbl_purchase_payment_master.bill_type, tbl_purchase_payment_master.Grand_Total, SUM(tbl_purchase_payment_dtls.paid_amt) AS PaidAmount, tbl_purchase_payment_master.Grand_Total - SUM(tbl_purchase_payment_dtls.paid_amt) AS BalanceAmount, tbl_purchase_payment_master.payment_status FROM            tbl_purchase_payment_master INNER JOIN tbl_purchase_payment_dtls ON tbl_purchase_payment_master.purchase_bill_no = tbl_purchase_payment_dtls.purchase_bill_no where tbl_purchase_payment_master.vendor_nm='" + cmb_vendor.Text + "' and  tbl_purchase_payment_master.payment_status='" + "Unpaid" + "'  GROUP BY tbl_purchase_payment_master.date, tbl_purchase_payment_master.vendor_nm, tbl_purchase_payment_master.purchase_bill_no,bill_type,  tbl_purchase_payment_master.Grand_Total, tbl_purchase_payment_master.payment_status");
            dataGridView1.DataSource = db.Displaygrid("SELECT        date as Date, supplierName as [Supplier Name], billNo as [Bill No], grandTotal as [Grand Total], paidAmt AS [Paid Amt], dueAmt AS [Balance], paymentStatus as[Status] FROM            PaymentMaster WHERE        (supplierName = '" + cmb_vendor.Text + "') AND (paymentStatus = 'Unpaid') GROUP BY date, supplierName, billNo, grandTotal, paymentStatus, paidAmt, dueAmt ");
            dataGridView1.Columns[2].Width = 180;
            dataGridView1.Columns[3].Width = 80;
            dataGridView1.Columns[4].Width = 120;
            dataGridView1.Columns[5].Width = 120;
            dataGridView1.Columns[6].Width = 85;
            dataGridView1.Columns[7].Width = 80;

        }
        void clear()
        {
            txtbal_amt.Text = "";
            txt_paid.Text = "";
            txt_bill.Text = "";
            txt_cheque.Text = "";
            cmb_banknm.Text = "";
           // txt_total.Text = "";
           // txt_voucherno.Text = "";
            textBox_reference.Text = "";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (cmb_vendor.Text == "-- Select Name --")
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    dataGridView1.Rows[i].Cells[0].ReadOnly = true;
                    //dataGridView_allChellan.Rows[i].Cells[0].Selected=true;
                }
                MessageBox.Show("Select Vendor Name First");
            }
            else
            {
                string billNo = "";
                //  int Bill_numbers=0;
                double k = 0.0, k1 = 0.0, k2 = 0.0, sum = 0.0;
                c = 0;

                if (e.ColumnIndex == 0)
                {
                    dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    for (int i = 0; i <= dataGridView1.RowCount - 1; i++)
                    {


                        //chkcol is checkbox column7
                        if (Convert.ToBoolean(dataGridView1.Rows[i].Cells["chkcol"].Value) == true)
                        {
                            // k = k + Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value); //4 is total amount column
                            sum += Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value);
                            k1 = k1 + Convert.ToDouble(dataGridView1.Rows[i].Cells[6].Value);


                            //chk bill status is pad or not 

                            //assign the bill numbers
                            billNo += dataGridView1.Rows[i].Cells[3].Value.ToString() + ",";

                            //array to store the bill number
                            Bill_numbers[c] = (dataGridView1.Rows[i].Cells[3].Value.ToString());// add here bill_ number from the DGV
                            Bill_status[c] = dataGridView1.Rows[i].Cells[7].Value.ToString();// add here bill_ status from the DGV
                             c++;


                        }


                        //This one is total display textbox
                       // txt_total.Text = Convert.ToInt32(sum).ToString();
                      txtbal_amt.Text = Convert.ToInt32(k1).ToString();

                        txt_bill.Text = billNo;
                        // end of start code
                    }
                }
            }
        }

        private void cmb_vendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bind_DGV();
            count = int.Parse(db.getDb_Value(" SELECT voucherNo FROM  PaymentDetails order by voucherNo  desc").ToString());


            count += 1;

            txt_voucherno.Text = count.ToString();
            if (cmb_vendor.Text != "-- Select Name --")
            {
                double sum = 0;
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    sum += double.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString());
                }
                txt_total.Text = sum.ToString();
            }
        }

        private void rdb_cheque_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_cheque.Checked)
            {
                panel2.Visible = true;
                label12.Visible = false;
                textBox_reference.Visible = false;
            }
            else
            {

                panel2.Visible = false;
                label12.Visible = true;
                textBox_reference.Visible = true;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            cmb_vendor.Text = "All";
           // dataGridView1.DataSource = db.Displaygrid("SELECT tbl_purchase_payment_master.date, tbl_purchase_payment_dtls.vendor_nm, tbl_purchase_payment_dtls.purchase_bill_no,  tbl_purchase_payment_master.Grand_Total, SUM(tbl_purchase_payment_master.paid_amt) AS PaidAmount,  tbl_purchase_payment_master.Grand_Total - SUM(tbl_purchase_payment_master.paid_amt) AS BalanceAmount,  tbl_purchase_payment_master.payment_status FROM tbl_purchase_payment_master INNER JOIN   tbl_purchase_payment_dtls ON tbl_purchase_payment_master.purchase_bill_no = tbl_purchase_payment_dtls.purchase_bill_no GROUP BY tbl_purchase_payment_master.date, tbl_purchase_payment_dtls.vendor_nm, tbl_purchase_payment_dtls.purchase_bill_no,  tbl_purchase_payment_master.Grand_Total, tbl_purchase_payment_master.payment_status");
            dataGridView1.DataSource = db.Displaygrid("SELECT   date as Date, supplierName as [Supplier Name], billNo as [Bill No],grandTotal as [Grand Total], paidAmt as [Paid Amt], dueAmt as [Balance], paymentStatus as [Status] FROM            PaymentMaster where paymentStatus='Unpaid'  GROUP BY billNo, date, supplierName, grandTotal, paidAmt, dueAmt, paymentStatus");
            dataGridView1.Columns[2].Width = 180;
            dataGridView1.Columns[3].Width = 80;
            dataGridView1.Columns[4].Width = 120;
            dataGridView1.Columns[5].Width = 120;
            dataGridView1.Columns[6].Width = 85;
            dataGridView1.Columns[7].Width = 80;
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void txt_paid_TextChanged(object sender, EventArgs e)
        {


            if (txt_paid.Text != "")
            {
                if (txtbal_amt.Text == "")
                {
                    MessageBox.Show("Please Select At list One CheckBox.");
                    btnSave.Enabled = false;
                }
            }
            if(txt_paid.Text != "" && txtbal_amt.Text!="")
            {
                if (int.Parse(txt_paid.Text) > int.Parse(txtbal_amt.Text))
                {
                    MessageBox.Show("Paid amount is greater than Balance Amount.Please Check it Correctly", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnSave.Enabled = false;
                }
                else
                    btnSave.Enabled = true;
            }
                
        }

        private void txt_paid_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);            

        }

        private void txt_cheque_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);
        }

        private void cmb_vendor_TextChanged(object sender, EventArgs e)
        {
            clear();
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            count = int.Parse(db.getDb_Value(" SELECT voucherNo FROM  PaymentDetails order by voucherNo  desc").ToString());
            count += 1;
            txt_voucherno.Text = count.ToString();
            clear();
        }

        private void txtbal_amt_TextChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
        }
    }
}
