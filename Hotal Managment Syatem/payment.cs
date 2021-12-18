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
    public partial class payment : Form
    {
        Database db = new Database();
        bool ispageload,groupSelected=false;
        float v_id = 0;
        int c=0;
        int[] Bill_numbers = new int[200];
        string[] Bill_status = new string[200];
        string[] Bill_type = new string[200];
        public payment()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void clearText()
        {
            txtAmount.Text = "0";
            //comboGroup.Text = "--Select--";
            //comboPerson.Text = "";
            txt_balance.Text = "0";
            txt_bill.Text = "";
            txt_paid.Text = "0";
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            clearText();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
           // string status = "";
            //if (txtAmount.Text == "")
            //{
            //    MessageBox.Show("Please Enter Amount");
            //}
            //else
            //{
            //    v_id = db.getDb_Value("select sup_id from supplier_dtls where sup_name='" + comboPerson.Text + "'");
            //    string date = dateTimePicker1.Value.ToString("MM'-'dd'-'yyyy");
            //    db.insert("insert into payment (grp,cust,amt,paid_amt,balance,date,bill_no)values('" + comboGroup.Text + "','" + comboPerson.Text + "','" + txtAmount.Text.Trim() + "','" + txt_paid.Text + "','" + txt_balance.Text + "','" + date + "','" + txt_bill.Text + "')");
            //    MessageBox.Show("Record Saved Successfully!!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //    // dataGridView_pay.DataSource = db.Displaygrid("select invoice_number as bill_no,date as Date,amt as Amount,status as Staus from tbl_purchasemaster where v_id='" + v_id + "' and status='" + "Unpaid" + "'");
            //    float max_bill = db.getDb_Value("select max(pay_id) from payment");
            //    float balance = db.getDb_Value("select balance from payment where bill_no='" + txt_bill.Text + "' and pay_id='" + max_bill + "'");
            //    if (balance == 0)
            //    {
            //        status = "Paid";
            //    }
            //    else
            //    {
            //        status = "Unpaid";
            //    }
            //    db.update("update tbl_purchasemaster set paid_amt='" + txt_paid.Text + "',balance='" + txt_balance.Text + "',status='" + status + "' where invoice_number='" + txt_bill.Text + "'");
            //    // dataGridView_pay.DataSource = db.Displaygrid("SELECT bill_no as [Bill_No],cust as [Customer Name], SUM(amt) AS Total, SUM(paid_amt) AS [Paid Amt], (SUM(amt) - SUM(paid_amt)) AS balance FROM payment  where cust='" + comboPerson.Text + "' and bill_no='" + txt_bill.Text + "' GROUP BY cust,bill_no");
            //    dataGridView_pay.DataSource = db.Displaygrid("select invoice_number as bill_no,date as Date,balance as Amount ,status as Staus from tbl_purchasemaster where sup_id='" + v_id + "' and status='" + "Unpaid" + "'");

            //   

            //}
            //*******************************************************************************************************
            float remmingBillAmt = 0;
            float totalPaidAmount, temp_paidAmt;
            string billStatus = "Unpaid";
            string modeOfPay = "";
            int k = 0; bool flagtemp = false;
            //get here total paid amount 
            if (txt_paid.Text != "")
                totalPaidAmount = float.Parse(txt_paid.Text.ToString());
            temp_paidAmt = float.Parse(txt_paid.Text.ToString());
            //get the bill numbers which is selected

            //float amt=dataGridView1.Rows[0].Cells[6].Value.ToString();
            //modeOfPay;


            if (c == 1)
            {

                string billno = Bill_numbers[k].ToString();
                string bill_status_1 = Bill_status[k].ToString();
              //  string billtype = Bill_type[k].ToString();
                if (billno != "0" && bill_status_1 != "")
                {
                    v_id = db.getDb_Value("select sup_id from supplier_dtls where sup_name='" + comboPerson.Text + "'");

                    //get the total bill paid amount && substract from the remming amount 
                    remmingBillAmt = float.Parse(db.getDb_Value("SELECT        amt -  SUM(paid_amt) AS Expr1 FROM            tbl_purchasemaster WHERE        invoice_number = '" + billno + "'  AND  sup_id = '" + v_id + "' group by amt").ToString());// AS remming, billNo, billtype FROM            tbl_Receipt_Master  WHERE        (billNo = '" + billno + "') AND (billtype = '" + billtype + "') GROUP BY tot_amt, billNo, billtype").ToString());

                    //if bill is paid make bill status is paid
                    if (remmingBillAmt <= temp_paidAmt)
                    {
                        billStatus = "Paid";
                        temp_paidAmt -= remmingBillAmt;
                         string date = dateTimePicker1.Value.ToString("MM'-'dd'-'yyyy");
                        db.insert("insert into payment (grp,cust,amt,paid_amt,balance,date,bill_no)values('" + comboGroup.Text + "','" + comboPerson.Text + "','" + txtAmount.Text.Trim() + "','" + remmingBillAmt + "','" + "0" + "','" + date + "','" + billno + "')");


                    }
                    else
                    {
                        remmingBillAmt = remmingBillAmt - temp_paidAmt;
                        billStatus = "Unpaid";
                         string date = dateTimePicker1.Value.ToString("MM'-'dd'-'yyyy");
                        db.insert("insert into payment (grp,cust,amt,paid_amt,balance,date,bill_no)values('" + comboGroup.Text + "','" + comboPerson.Text + "','" + txtAmount.Text.Trim() + "','" + temp_paidAmt + "','" + remmingBillAmt + "','" + date + "','" + billno + "')");



                        temp_paidAmt = 0;
                    }
                    // here for receipt details


                    string paidAmt = db.getDb_Value("select sum(paid_amt) from payment where bill_no='" + billno + "' and cust='" + comboPerson.Text + "' ").ToString();
                    string total_amt = db.getDbstatus_Value("select  amt from tbl_purchasemaster where invoice_number='" + billno + "' and sup_id='" + v_id + "'");
                    string due_amt = (float.Parse(total_amt) - float.Parse(paidAmt)).ToString();

                    db.update("update tbl_purchasemaster set paid_amt='" + paidAmt + "',balance='" + due_amt + "',status='" + billStatus + "' where invoice_number='" + billno + "' and sup_id='"+v_id+"'");
                }
            }
            else
            {
                for (k = 0; k < c; k++)
                {
                    v_id = db.getDb_Value("select sup_id from supplier_dtls where sup_name='" + comboPerson.Text + "'");

                    string billno = Bill_numbers[k].ToString();
                    string bill_status_1 = Bill_status[k].ToString();
                  //  string billtype = Bill_type[k].ToString();
                    if (billno != "0" && bill_status_1 != "")
                    {

                        //get the total bill paid amount && substract from the remming amount 
                        remmingBillAmt = float.Parse(db.getDb_Value("SELECT        amt -  SUM(paid_amt) AS Expr1 FROM            tbl_purchasemaster WHERE        invoice_number = '" + billno + "'  AND  sup_id = '" + v_id + "' group by amt").ToString());// AS remming, billNo, billtype FROM            tbl_Receipt_Master  WHERE        (billNo = '" + billno + "') AND (billtype = '" + billtype + "') GROUP BY tot_amt, billNo, billtype").ToString());

                        //if bill is paid make bill status is paid
                        if (remmingBillAmt <= temp_paidAmt)
                        {
                            billStatus = "Paid";
                            temp_paidAmt -= remmingBillAmt;
                             string date = dateTimePicker1.Value.ToString("MM'-'dd'-'yyyy");
                            db.insert("insert into payment (grp,cust,amt,paid_amt,balance,date,bill_no)values('" + comboGroup.Text + "','" + comboPerson.Text + "','" + txtAmount.Text.Trim() + "','" + remmingBillAmt + "','" + "0" + "','" + date + "','" + billno + "')");


                        }
                        else
                        {
                            remmingBillAmt = remmingBillAmt - temp_paidAmt;
                            billStatus = "Unpaid";
                             string date = dateTimePicker1.Value.ToString("MM'-'dd'-'yyyy");
                            db.insert("insert into payment (grp,cust,amt,paid_amt,balance,date,bill_no)values('" + comboGroup.Text + "','" + comboPerson.Text + "','" + txtAmount.Text.Trim() + "','" + temp_paidAmt + "','" + remmingBillAmt + "','" + date + "','" + billno + "')");



                            temp_paidAmt = 0;
                        }
                        // here for receipt details


                        string paidAmt = db.getDb_Value("select sum(paid_amt) from payment where bill_no='" + billno + "' and cust='" + comboPerson.Text + "' ").ToString();
                        string total_amt = db.getDbstatus_Value("select  amt from tbl_purchasemaster where invoice_number='" + billno + "' and sup_id='" + v_id + "'");
                        string due_amt = (float.Parse(total_amt) - float.Parse(paidAmt)).ToString();

                        db.update("update tbl_purchasemaster set paid_amt='" + paidAmt + "',balance='" + due_amt + "',status='" + billStatus + "' where invoice_number='" + billno+ "' and sup_id='"+v_id+"'");
                    }
                }
            }
            v_id = db.getDb_Value("select sup_id from supplier_dtls where sup_name='" + comboPerson.Text + "'");

            dataGridView_pay.DataSource = db.Displaygrid("select invoice_number as bill_no,date as Date,amt as Amount,paid_amt as Paid,balance as Balance,status as Staus from tbl_purchasemaster where sup_id='" + v_id + "' and status='" + "Unpaid" + "'");
            gridFormation();
            lbl_nm.Text = comboPerson.Text;
            double sum = 0;
            for (int i = 0; i < dataGridView_pay.RowCount; i++)
            {
                sum += double.Parse(dataGridView_pay.Rows[i].Cells[5].Value.ToString());
            }
            txt_tot_remain.Text = sum.ToString();
            clearText();
        MessageBox.Show("Record Saved Successfully!!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        

        private void payment_Load(object sender, EventArgs e)
        {
            this.CancelButton = button3;
            db.comboFill(comboGroup, "select Grp_Name from createGrp", "createGrp", "Grp_Name", "Grp_Name");
            comboGroup.Text = "--Select--";
            //db.comboFill(comboPerson, "select v_name from tbl_vendor ", "tbl_vendor", "v_name", "v_name");
            //comboPerson.Text = "--Select--";
            ispageload = true;
        }
        void gridFormation()
        {
            dataGridView_pay.Columns[2].Width = 70;
            dataGridView_pay.Columns[3].Width = 80;
            dataGridView_pay.Columns[4].Width = 80;
            dataGridView_pay.Columns[5].Width = 80;
            dataGridView_pay.Columns[6].Width = 60;
        }
        private void comboGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ispageload)
            {
                if (comboGroup.Text == "Purchase")
                {
                    db.comboFill(comboPerson, "select sup_name from supplier_dtls ", "supplier_dtls", "sup_name", "sup_name");
                    comboPerson.Text = "--Select--";
                }
                else if (comboGroup.Text == "Salary")
                {
                    db.comboFill(comboPerson, "select wname from waiter_dtls ", "waiter_dtls", "wname", "wname");
                    comboPerson.Text = "--Select--";
                }
                //else if (comboGroup.Text == "Credit")
                //{
                //    db.comboFill(comboPerson, "select * from total_bill where status='Credit' ", "total_bill", "status", "status");
                //    comboPerson.Text = "";
                //}
                //else if (comboGroup.Text == "Cash")
                //{

                //}
                //else
                //{
                //    comboPerson.Visible = false;
                //}
                groupSelected = true;
            }
        }

        private void comboPerson_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (groupSelected)
            {
                float v_id = 0;
                if (comboPerson.Text != "System.Data.DataRowView")
                {
                    //if (db.ChkDb_Value("select sup_id from supplier_dtls where sup_name='" + comboPerson.Text + "'"))
                    v_id = db.getDb_Value("select sup_id from supplier_dtls where sup_name='" + comboPerson.Text + "'");

                    dataGridView_pay.DataSource = db.Displaygrid("select invoice_number as bill_no,date as Date,amt as Amount,paid_amt as Paid,balance as Balance,status as Staus from tbl_purchasemaster where sup_id='" + v_id + "' and status='" + "Unpaid" + "'");
                    lbl_nm.Text = comboPerson.Text;
                    gridFormation();
                }
                double sum = 0;
                for (int i = 0; i < dataGridView_pay.RowCount; i++)
                {
                    sum +=double.Parse(dataGridView_pay.Rows[i].Cells[5].Value.ToString());
                }
                txt_tot_remain.Text = sum.ToString();
            }
        }

        private void dataGridView_pay_MouseClick(object sender, MouseEventArgs e)
        {
            int k = dataGridView_pay.SelectedCells[0].RowIndex;
            float bill_no=float.Parse(dataGridView_pay.Rows[k].Cells[1].Value.ToString());
            float amount=float.Parse(dataGridView_pay.Rows[k].Cells[5].Value.ToString());
            txt_bill.Text = bill_no.ToString();
            txtAmount.Text = amount.ToString();
        
        }

        private void txt_paid_TextChanged(object sender, EventArgs e)
        {
            if(txt_paid.Text!="")
         
            txt_balance.Text =(float.Parse(txtAmount.Text)-float.Parse(txt_paid.Text)).ToString();
        }

        private void dataGridView_pay_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string billNo = "";
            //  int Bill_numbers=0;
            double k = 0.0, k1 = 0.0, k2 = 0.0, sum = 0.0;
            c = 0;

            if (e.ColumnIndex == 0)
            {
                dataGridView_pay.CommitEdit(DataGridViewDataErrorContexts.Commit);
                for (int i = 0; i <= dataGridView_pay.RowCount - 1; i++)
                {


                    //chkcol is checkbox column7
                    if (Convert.ToBoolean(dataGridView_pay.Rows[i].Cells[0].Value) == true)
                    {
                         sum += Convert.ToDouble(dataGridView_pay.Rows[i].Cells[5].Value);
                       // k1 = k1 + Convert.ToDouble(dataGridView_pay.Rows[i].Cells[6].Value);


                        //chk bill status is pad or not 

                        //assign the bill numbers
                        billNo += dataGridView_pay.Rows[i].Cells[1].Value.ToString() + ",";

                        //array to store the bill number
                        Bill_numbers[c] = int.Parse(dataGridView_pay.Rows[i].Cells[1].Value.ToString());// add here bill_ number from the DGV
                        Bill_status[c] = dataGridView_pay.Rows[i].Cells[6].Value.ToString();// add here bill_ status from the DGV
                      //  Bill_type[c] = dataGridView_pay.Rows[i].Cells[3].Value.ToString(); // bill type vat or transport
                        c++;


                    }


                    //This one is total display textbox
                     txtAmount.Text = Convert.ToInt32(sum).ToString();

                    txt_bill.Text = billNo;
                    // end of start code
                }
            }
        }

        private void comboBox_waiter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (groupSelected)
            {

            }

        }

        private void chk_all_CheckedChanged(object sender, EventArgs e)
        {
               string billNo = "";   
            double k = 0.0, k1 = 0.0, k2 = 0.0, sum = 0.0;
            c = 0;
            if (chk_all.Checked)
            {
                for (int i = 0; i < dataGridView_pay.RowCount; i++)
                {
                     dataGridView_pay.Rows[i].Cells[0].Value = true;
                      sum += Convert.ToDouble(dataGridView_pay.Rows[i].Cells[5].Value);
                     //assign the bill numbers
                     billNo += dataGridView_pay.Rows[i].Cells[1].Value.ToString() + ",";

                     //array to store the bill number
                     Bill_numbers[c] = int.Parse(dataGridView_pay.Rows[i].Cells[1].Value.ToString());// add here bill_ number from the DGV
                     Bill_status[c] = dataGridView_pay.Rows[i].Cells[6].Value.ToString();// add here bill_ status from the DGV
                     c++;
                }
                    //This one is total display textbox
                     txtAmount.Text = Convert.ToInt32(sum).ToString();

                     txt_bill.Text = billNo;
            }
            else
            {
                for (int i = 0; i < dataGridView_pay.RowCount; i++)
                {
                    dataGridView_pay.Rows[i].Cells[0].Value = false;
                }
                txt_bill.Text = "0";
                txtAmount.Text = "0";
            }
          
        
            
              


            }


          
        }
    }
 
