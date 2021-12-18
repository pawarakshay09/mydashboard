using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Hotal_Managment_Syatem
{
    public partial class Supplier_Payment_Master : Form
    {
        Database db = new Database();
        public string userName;
        float dltPaidAmt = 0, masterbalanceAmt = 0, masterpaidAmt = 0;
        bool flag = false;
        public Supplier_Payment_Master()
        {
            InitializeComponent();
        }
        public Supplier_Payment_Master(string userNm)
        {
            InitializeComponent();
            this.userName = userNm;
        }
        private void Supplier_Payment_Master_Load(object sender, EventArgs e)
        {
            db.formFix(this);
            this.CancelButton = button1;
            db.comboFill(cmb_vendor, "select * from supplier_dtls order by sup_name asc", "supplier_dtls", "sup_name", "sup_name");
            cmb_vendor.Text = "All";
            flag = true;
          //  dataGridView1.DataSource = db.Displaygrid("SELECT   PaymentMaster.date as Date, PaymentDetails.paidBills as [Bill No], PaymentMaster.supplierName as [Supplier Name], PaymentMaster.grandTotal as [Grand Total], PaymentMaster.paidAmt as [Paid Amount], PaymentMaster.dueAmt as [Balance Amount],PaymentDetails.paymentType as [Payment Mode], PaymentDetails.bankName as [Bank Name], PaymentDetails.chequeNo as [Cheque No], PaymentDetails.chequeDate as [Cheque Date], PaymentDetails.chequeStatus as [Cheque Status] FROM            PaymentDetails INNER JOIN  PaymentMaster ON PaymentDetails.billNo = PaymentMaster.billNo AND PaymentDetails.voucherNo = PaymentMaster.voucherNo where PaymentMaster.paidAmt<>0");
            dataGridView1.DataSource = db.Displaygrid("SELECT        PaymentDetails.date as Date, PaymentDetails.paidBills as [Bill No], PaymentMaster.supplierName as [Supplier Name], sum(PaymentMaster.grandTotal) as [Grand Total], sum(PaymentDetails.paidAmt) as [Paid Amount], sum(PaymentMaster.dueAmt) as [Balance Amount],   PaymentDetails.paymentType as [Payment Mode], PaymentDetails.bankName as [Bank Name], PaymentDetails.chequeNo as [Cheque No],PaymentDetails.chequeDate as [Cheque Date], PaymentDetails.chequeStatus as [Status] FROM            PaymentDetails INNER JOIN     PaymentMaster ON PaymentDetails.billNo = PaymentMaster.billNo AND PaymentDetails.supplierName = PaymentMaster.supplierName  WHERE        (PaymentDetails.paidAmt <> 0)  group by PaymentDetails.date,PaymentDetails.paidBills ,PaymentMaster.supplierName,PaymentDetails.paymentType,PaymentDetails.bankName,PaymentDetails.chequeNo,PaymentDetails.chequeStatus,PaymentDetails.chequeDate");


            cell_formation();
            serial_no();
            sum();                                                                                                                                                                                                                                                                                  
        }

        void sum()
        {
            double totalAmt=0, paid=0, balance=0;
            for (int k = 0; k < dataGridView1.RowCount; k++)
            {
                totalAmt += double.Parse(dataGridView1.Rows[k].Cells[4].Value.ToString());
                paid += double.Parse(dataGridView1.Rows[k].Cells[5].Value.ToString());
                balance += double.Parse(dataGridView1.Rows[k].Cells[6].Value.ToString());
            }
            txtTotal.Text = totalAmt.ToString();
            txtPaid.Text = paid.ToString();
            txtBalance.Text = balance.ToString();
        }

        private void cmb_pay_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_pay_type.Text == "Cheque")
            {
                panel_chequeDtls.Visible = true;
            }
            else
            {
                panel_chequeDtls.Visible = false;
                cmb_chk_status.Text = "All";
            }
            select_qry();
        }

        private void cmb_vendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(flag)
            select_qry();
        }
        void select_qry()
        {
            int flag = 0;
           // string qry = "SELECT   PaymentMaster.date as Date, PaymentDetails.paidBills as [Bill No], PaymentMaster.supplierName as [Supplier Name], PaymentMaster.grandTotal as [Grand Total], PaymentMaster.paidAmt as [Paid Amount], PaymentMaster.dueAmt as [Balance Amount],PaymentDetails.paymentType as [Payment Mode], PaymentDetails.bankName as [Bank Name], PaymentDetails.chequeNo as [Cheque No], PaymentDetails.chequeDate as [Cheque Date], PaymentDetails.chequeStatus as [Cheque Status] FROM            PaymentDetails INNER JOIN  PaymentMaster ON PaymentDetails.billNo = PaymentMaster.billNo AND PaymentDetails.voucherNo = PaymentMaster.voucherNo where PaymentMaster.paidAmt<>0 and  ";
            string qry = "SELECT        PaymentDetails.date as Date, PaymentDetails.paidBills as [Bill No], PaymentMaster.supplierName as [Supplier Name], sum(PaymentMaster.grandTotal) as [Grand Total], sum(PaymentDetails.paidAmt) as [Paid Amount], sum(PaymentMaster.dueAmt) as [Balance Amount],   PaymentDetails.paymentType as [Payment Mode], PaymentDetails.bankName as [Bank Name], PaymentDetails.chequeNo as [Cheque No],PaymentDetails.chequeDate as [Cheque Date], PaymentDetails.chequeStatus as [Status] FROM            PaymentDetails INNER JOIN     PaymentMaster ON PaymentDetails.billNo = PaymentMaster.billNo AND PaymentDetails.supplierName = PaymentMaster.supplierName  WHERE        (PaymentDetails.paidAmt <> 0) and ";
            if (cmb_vendor.Text != "All")
            {
                if (flag != 0)
                    qry += "and ";
                qry += " (PaymentMaster.supplierName='" + cmb_vendor.Text + "') ";
                flag++;
            }
            if (cmb_chk_status.Text != "All")
            {
                if (flag != 0)
                    qry += "and ";
                qry += " PaymentDetails.chequeStatus='" + cmb_chk_status.Text + "'";
                flag++;
            }
            if (cmb_pay_type.Text != "All")
            {
                if (flag != 0)
                    qry += "and ";
                qry += " PaymentDetails.paymentType='" + cmb_pay_type.Text + "'";
                flag++;
            }
            if (chkboxdt.Checked)
            {
                string datefrm = dTP1.Value.ToString("MM-dd-yyyy");
                string dateto = dTP2.Value.ToString("MM-dd-yyyy");
                if (flag != 0)
                    qry += "and ";
                qry += " PaymentDetails.date between '" + datefrm + "' and '" + dateto + "'";
                flag++;
            }
            if (txt_cheque.Text != "")
            {
                if (flag != 0)
                    qry += "and ";
                qry += " PaymentDetails.chequeNo like '" + txt_cheque.Text + "%'";
                flag++;
            }
            if(flag==0)
                dataGridView1.DataSource = db.Displaygrid("SELECT        PaymentDetails.date as Date, PaymentDetails.paidBills as [Bill No], PaymentMaster.supplierName as [Supplier Name], sum(PaymentMaster.grandTotal) as [Grand Total], sum(PaymentDetails.paidAmt) as [Paid Amount], sum(PaymentMaster.dueAmt) as [Balance Amount],   PaymentDetails.paymentType as [Payment Mode], PaymentDetails.bankName as [Bank Name], PaymentDetails.chequeNo as [Cheque No],PaymentDetails.chequeDate as [Cheque Date], PaymentDetails.chequeStatus as [Status] FROM            PaymentDetails INNER JOIN     PaymentMaster ON PaymentDetails.billNo = PaymentMaster.billNo AND PaymentDetails.supplierName = PaymentMaster.supplierName   WHERE        (PaymentDetails.paidAmt <> 0)  group by PaymentDetails.date,PaymentDetails.paidBills ,PaymentMaster.supplierName,PaymentDetails.paymentType,PaymentDetails.bankName,PaymentDetails.chequeNo,PaymentDetails.chequeStatus,PaymentDetails.chequeDate");

            string grp_by = " group by PaymentDetails.date,PaymentDetails.paidBills ,PaymentMaster.supplierName,PaymentDetails.paymentType,PaymentDetails.bankName,PaymentDetails.chequeNo,PaymentDetails.chequeStatus,PaymentDetails.chequeDate";
            dataGridView1.DataSource = db.Displaygrid(qry + grp_by);
             cell_formation();
            serial_no();
            sum(); 
        }
        void serial_no()
        {
            if (dataGridView1.Rows.Count >= 1)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    dataGridView1.Rows[i].Cells[0].Value = (i + 1).ToString();
                }
            }
        }
        void cell_formation()
         {
             dataGridView1.Columns[0].Width=50;
             dataGridView1.Columns[1].Width = 80;
             dataGridView1.Columns[2].Width = 120;
             dataGridView1.Columns[3].Width = 160;
             dataGridView1.Columns[4].Width = 80;
             dataGridView1.Columns[5].Width = 90;
             dataGridView1.Columns[6].Width = 85;
             dataGridView1.Columns[7].Width = 80;
             dataGridView1.Columns[8].Width = 90;
             dataGridView1.Columns[9].Width = 100;
             dataGridView1.Columns[10].Width = 80;
             dataGridView1.Columns[4].Visible = false;
             dataGridView1.Columns[6].Visible = false;
           //  dataGridView1.Columns[12].Visible = false;
         }
        private void cmb_chk_status_SelectedIndexChanged(object sender, EventArgs e)
        {
            select_qry();
        }

        private void txt_cheque_TextChanged(object sender, EventArgs e)
        {
            select_qry();
        }

        private void dTP1_ValueChanged(object sender, EventArgs e)
        {
            select_qry();
        }

        private void dTP2_ValueChanged(object sender, EventArgs e)
        {
            select_qry();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            int i = dataGridView1.SelectedCells[0].RowIndex;
            string billNo=dataGridView1.Rows[i].Cells[2].Value.ToString();
            Supplier_Payment_Details _supplierPaymentDtls = new Supplier_Payment_Details(billNo,userName);
            _supplierPaymentDtls.ShowDialog();

        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do You Really Want to Delete Record?", "Confirmation Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                string dtl_id, paid_amt;
                 string billNo = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                string supplierName=dataGridView1.CurrentRow.Cells[3].Value.ToString();
            //    dtl_id = dataGridView1.CurrentRow.Cells["detailsId"].Value.ToString();
                string[] bill = billNo.Split(',');
                foreach (string billNo1 in bill)
                {
                
                    if (billNo1 != "")
                    {
                        string total_amt = db.getDb_Value("select grandTotal from PaymentMaster where billNo='" + billNo1 + "' and supplierName='" + supplierName + "' ").ToString();
                        dtl_id = db.getDb_Value("select max(voucherNo) from PaymentDetails where billNo='" + billNo1 + "' and paidBills='"+billNo+"'").ToString();

                          //db.update("update PaymentMaster set paidAmt='" + 0 + "' ,dueAmt='" + total_amt + "' ,paymentStatus='Unpaid' where billNo='" + billNo1 + "' and supplierName='" + supplierName + "' and userName='" + userName + "'");
                         // db.delete_direct("delete from PaymentDetails where billNo='" + billNo1 + "' and voucherNo='" + dtl_id + "'");


                          dltPaidAmt = float.Parse(db.getDb_Value("select paidAmt from PaymentDetails where billNo='" + billNo1 + "' and voucherNo='" + dtl_id + "'  and paidBills='" + billNo + "' ").ToString());
                          masterpaidAmt = float.Parse(db.getDb_Value("select paidAmt from PaymentMaster where billNo='" + billNo1 + "'  and supplierName='" + supplierName + "' ").ToString());
                          masterbalanceAmt = float.Parse(db.getDb_Value("select dueAmt from PaymentMaster where billNo='" + billNo1 + "'  and supplierName='" + supplierName + "' ").ToString());
                          db.DeleteData("delete from PaymentDetails where billNo='" + billNo1 + "' and voucherNo='" + dtl_id + "'  and supplierName='" + supplierName + "'", "PaymentDetails");

                          paid_amt = (masterpaidAmt - dltPaidAmt).ToString();
                          float balanceAmt = (masterbalanceAmt) + (dltPaidAmt);
                          db.update("update PaymentMaster set dueAmt='" + balanceAmt + "' ,paidAmt='" + paid_amt + "' ,paymentStatus='Unpaid' where billNo='" + billNo1 + "' and supplierName='" + supplierName + "'");

                        dataGridView1.DataSource = db.Displaygrid("SELECT        PaymentDetails.date as Date, PaymentDetails.paidBills as [Bill No], PaymentMaster.supplierName as [Supplier Name], sum(PaymentMaster.grandTotal) as [Grand Total], sum(PaymentDetails.paidAmt) as [Paid Amount], sum(PaymentMaster.dueAmt) as [Balance Amount],   PaymentDetails.paymentType as [Payment Mode], PaymentDetails.bankName as [Bank Name], PaymentDetails.chequeNo as [Cheque No],PaymentDetails.chequeDate as [Cheque Date], PaymentDetails.chequeStatus as [Status] FROM            PaymentDetails INNER JOIN     PaymentMaster ON PaymentDetails.billNo = PaymentMaster.billNo AND PaymentDetails.supplierName = PaymentMaster.supplierName WHERE        (PaymentDetails.paidAmt <> 0)  group by PaymentDetails.date,PaymentDetails.paidBills ,PaymentMaster.supplierName,PaymentDetails.paymentType,PaymentDetails.bankName,PaymentDetails.chequeNo,PaymentDetails.chequeStatus,PaymentDetails.chequeDate");
                        serial_no();
                        sum(); 
                    }

                }
                MessageBox.Show("Record Deleted Successfully", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Excel Documents (*.xls)|*.xls";
                sfd.FileName = "Supplier Payment Report.xls";
                  if (sfd.ShowDialog() == DialogResult.OK)
                    //if (true)
                {
                    db.withReportTitle_ToCsV(dataGridView1, sfd.FileName,"Supplier Payment Report of "+System.DateTime.Now.ToString("dd-MM-yyyy")+""); // Here dataGridview1 is your grid view name 
                    MessageBox.Show("File Sucessfully Exported");


                    Process.Start(sfd.FileName);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            cmb_vendor.Text = "All";
            cmb_pay_type.Text = "All";
            chkboxdt.Checked = false;
            dataGridView1.DataSource = db.Displaygrid("SELECT        PaymentDetails.date as Date, PaymentDetails.paidBills as [Bill No], PaymentMaster.supplierName as [Supplier Name], sum(PaymentMaster.grandTotal) as [Grand Total], sum(PaymentDetails.paidAmt) as [Paid Amount], sum(PaymentMaster.dueAmt) as [Balance Amount],   PaymentDetails.paymentType as [Payment Mode], PaymentDetails.bankName as [Bank Name], PaymentDetails.chequeNo as [Cheque No],PaymentDetails.chequeDate as [Cheque Date], PaymentDetails.chequeStatus as [Status] FROM            PaymentDetails INNER JOIN     PaymentMaster ON PaymentDetails.billNo = PaymentMaster.billNo AND PaymentDetails.supplierName = PaymentMaster.supplierName  WHERE        (PaymentDetails.paidAmt <> 0)  group by PaymentDetails.date,PaymentDetails.paidBills ,PaymentMaster.supplierName,PaymentDetails.paymentType,PaymentDetails.bankName,PaymentDetails.chequeNo,PaymentDetails.chequeStatus,PaymentDetails.chequeDate");
            cell_formation();
            serial_no();
            sum(); 

        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            string bill_status = "";
            int i = dataGridView1.SelectedCells[0].RowIndex;
            string bill_no = dataGridView1.Rows[i].Cells[2].Value.ToString();
            string chequeNo = dataGridView1.Rows[i].Cells[9].Value.ToString();

            DialogResult dlgresult = MessageBox.Show("Are you sure want to Clear this Cheque ? Bill No: " + bill_no + "  Cheque Number :" + chequeNo, "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlgresult == DialogResult.Yes)
            {

                string str = dataGridView1.Rows[i].Cells[11].Value.ToString();
                if (str == "Unclear")
                {

                    dataGridView1.Rows[i].Cells[11].Value = "Clear";
                }
                int c = dataGridView1.SelectedCells[0].RowIndex;

                string status = dataGridView1.Rows[c].Cells[11].Value.ToString();
                //  status = "Unclear";
                if (status == "Clear")
                {
                    bill_status = "Paid";
                }
                db.update("update  PaymentDetails set chequeStatus='" + status + "' where chequeNo='" + chequeNo + "'");
                db.update("update PaymentMaster set paymentStatus='" + bill_status + "' where billNo like '" + bill_no + "%' ");
                // dataGridView1.DataSource = db.Displaygrid("SELECT         tbl_Receipt_Details.receiptDetailsId as [Voucher No.], tbl_Receipt_Details.chequeDate AS Date, tbl_Receipt_Details.paid_bills as [Bill No], tbl_Receipt_Master.billtype AS Bill_Type, tbl_Receipt_Master.partyname AS Party_Name,   SUM(tbl_Receipt_Master.tot_amt) AS Total_Amount, SUM(tbl_Receipt_Details.paid_amt) AS Paid_Amount, SUM(tbl_Receipt_Master.balanceAmt) AS [Balance Amount],  tbl_Receipt_Details.modeOfPay AS Payment_Mode, tbl_Receipt_Details.bankName AS Bank_Name,   tbl_Receipt_Details.chequeNumber AS Cheque_No, tbl_Receipt_Details.cheque_status FROM            tbl_Receipt_Details INNER JOIN        tbl_Receipt_Master ON tbl_Receipt_Details.billNo = tbl_Receipt_Master.billNo WHERE        (tbl_Receipt_Details.paid_amt <> 0) and tbl_Receipt_Master.partyname='" + cmb_vendor.Text + "'  GROUP BY tbl_Receipt_Details.chequeDate, tbl_Receipt_Master.billtype, tbl_Receipt_Master.partyname, tbl_Receipt_Details.receiptDetailsId, tbl_Receipt_Details.modeOfPay,  tbl_Receipt_Details.bankName, tbl_Receipt_Details.chequeNumber, tbl_Receipt_Details.cheque_status, tbl_Receipt_Details.paid_bills order by  tbl_Receipt_Details.receiptDetailsId asc");
                dataGridView1.DataSource = db.Displaygrid("SELECT        PaymentDetails.date as Date, PaymentDetails.paidBills as [Bill No], PaymentMaster.supplierName as [Supplier Name], sum(PaymentMaster.grandTotal) as [Grand Total], sum(PaymentDetails.paidAmt) as [Paid Amount], sum(PaymentMaster.dueAmt) as [Balance Amount],   PaymentDetails.paymentType as [Payment Mode], PaymentDetails.bankName as [Bank Name], PaymentDetails.chequeNo as [Cheque No],PaymentDetails.chequeDate as [Cheque Date], PaymentDetails.chequeStatus as [Status] FROM            PaymentDetails INNER JOIN     PaymentMaster ON PaymentDetails.billNo = PaymentMaster.billNo AND PaymentDetails.supplierName = PaymentMaster.supplierName  WHERE        (PaymentDetails.paidAmt <> 0) group by PaymentDetails.date,PaymentDetails.paidBills ,PaymentMaster.supplierName,PaymentDetails.paymentType,PaymentDetails.bankName,PaymentDetails.chequeNo,PaymentDetails.chequeStatus,PaymentDetails.chequeDate");
                serial_no();
                cell_formation();
                    sum(); 
                if (status == "Clear")
                {
                    btn_unclear.Visible = true;
                    btn_clear.Visible = false;
                }
                else
                {
                    btn_unclear.Visible = false;
                    btn_clear.Visible = true;

                }
                //select_qry();
                // for set focus on next row 
                dataGridView1.ClearSelection();
                //int nRowIndex = dataGridView1.Rows.Count - 2;

                //dataGridView1.Rows[nRowIndex].Selected = true;
                //dataGridView1.Rows[nRowIndex+1].Cells[0].Selected = true;
            }
        }

        private void btn_unclear_Click(object sender, EventArgs e)
        {
            string bill_status = "";
            int i = dataGridView1.SelectedCells[0].RowIndex;
            string bill_no = dataGridView1.Rows[i].Cells[2].Value.ToString();
            string chequeNo = dataGridView1.Rows[i].Cells[9].Value.ToString();

            DialogResult dlgresult = MessageBox.Show("Are you sure want to Clear this Cheque ? Bill No: " + bill_no + "  Cheque Number :" + chequeNo, "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlgresult == DialogResult.Yes)
            {

                string str = dataGridView1.Rows[i].Cells[11].Value.ToString();
                if (str == "Clear")
                {

                    dataGridView1.Rows[i].Cells[11].Value = "Unclear";
                }
                int c = dataGridView1.SelectedCells[0].RowIndex;

                string status = dataGridView1.Rows[c].Cells[11].Value.ToString();
                //  status = "Unclear";
                if (status == "Clear")
                {
                    bill_status = "Paid";
                }
                db.update("update  PaymentDetails set chequeStatus='" + status + "' where chequeNo='" + chequeNo + "'");
                db.update("update PaymentMaster set paymentStatus='" + bill_status + "' where billNo like '" + bill_no + "%' ");
                // dataGridView1.DataSource = db.Displaygrid("SELECT         tbl_Receipt_Details.receiptDetailsId as [Voucher No.], tbl_Receipt_Details.chequeDate AS Date, tbl_Receipt_Details.paid_bills as [Bill No], tbl_Receipt_Master.billtype AS Bill_Type, tbl_Receipt_Master.partyname AS Party_Name,   SUM(tbl_Receipt_Master.tot_amt) AS Total_Amount, SUM(tbl_Receipt_Details.paid_amt) AS Paid_Amount, SUM(tbl_Receipt_Master.balanceAmt) AS [Balance Amount],  tbl_Receipt_Details.modeOfPay AS Payment_Mode, tbl_Receipt_Details.bankName AS Bank_Name,   tbl_Receipt_Details.chequeNumber AS Cheque_No, tbl_Receipt_Details.cheque_status FROM            tbl_Receipt_Details INNER JOIN        tbl_Receipt_Master ON tbl_Receipt_Details.billNo = tbl_Receipt_Master.billNo WHERE        (tbl_Receipt_Details.paid_amt <> 0) and tbl_Receipt_Master.partyname='" + cmb_vendor.Text + "'  GROUP BY tbl_Receipt_Details.chequeDate, tbl_Receipt_Master.billtype, tbl_Receipt_Master.partyname, tbl_Receipt_Details.receiptDetailsId, tbl_Receipt_Details.modeOfPay,  tbl_Receipt_Details.bankName, tbl_Receipt_Details.chequeNumber, tbl_Receipt_Details.cheque_status, tbl_Receipt_Details.paid_bills order by  tbl_Receipt_Details.receiptDetailsId asc");
                dataGridView1.DataSource = db.Displaygrid("SELECT        PaymentDetails.date as Date, PaymentDetails.paidBills as [Bill No], PaymentMaster.supplierName as [Supplier Name], sum(PaymentMaster.grandTotal) as [Grand Total], sum(PaymentDetails.paidAmt) as [Paid Amount], sum(PaymentMaster.dueAmt) as [Balance Amount],   PaymentDetails.paymentType as [Payment Mode], PaymentDetails.bankName as [Bank Name], PaymentDetails.chequeNo as [Cheque No],PaymentDetails.chequeDate as [Cheque Date], PaymentDetails.chequeStatus as [Status] FROM            PaymentDetails INNER JOIN     PaymentMaster ON PaymentDetails.billNo = PaymentMaster.billNo  AND PaymentDetails.supplierName = PaymentMaster.supplierName WHERE        (PaymentDetails.paidAmt <> 0)  group by PaymentDetails.date,PaymentDetails.paidBills ,PaymentMaster.supplierName,PaymentDetails.paymentType,PaymentDetails.bankName,PaymentDetails.chequeNo,PaymentDetails.chequeStatus,PaymentDetails.chequeDate");
                serial_no();
                cell_formation();
                sum(); 
                if (status == "Clear")
                {
                    btn_unclear.Visible = true;
                    btn_clear.Visible = false;
                }
                else
                {
                    btn_unclear.Visible = false;
                    btn_clear.Visible = true;

                }
                //select_qry();
                // for set focus on next row 
                dataGridView1.ClearSelection();
                //int nRowIndex = dataGridView1.Rows.Count - 2;

                //dataGridView1.Rows[nRowIndex].Selected = true;
                //dataGridView1.Rows[nRowIndex+1].Cells[0].Selected = true;
            }
        }

        private void txt_cheque_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);
        }

        private void chkboxdt_CheckedChanged(object sender, EventArgs e)
        {
            if (chkboxdt.Checked)
            {
                dTP1.Enabled = true;
                dTP2.Enabled = true;
            }
            else 
            {
                dTP1.Enabled = false;
                dTP2.Enabled = false;
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
