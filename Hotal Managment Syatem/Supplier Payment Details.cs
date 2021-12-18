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
    public partial class Supplier_Payment_Details : Form
    {
        Database db = new Database();
        string billNo,userName;
        public Supplier_Payment_Details()
        {
            InitializeComponent();
        }
        public Supplier_Payment_Details(string bill_no,string userNm)
        {
            InitializeComponent();
            this.billNo = bill_no;
            this.userName = userNm;
        }

        private void Supplier_Payment_Details_Load(object sender, EventArgs e)
        {
            db.formFix(this);
            this.CancelButton = button1;
            dataGridView1.DataSource = db.Displaygrid("SELECT        PaymentDetails.date as Date, PaymentDetails.paidBills as [Bill No], PaymentMaster.supplierName as [Supplier Name], PaymentMaster.grandTotal as [Grand Total], PaymentDetails.paidAmt as [Paid Amount], PaymentMaster.dueAmt as [Balance Amount],   PaymentDetails.paymentType as [Payment Mode], PaymentDetails.bankName as [Bank Name], PaymentDetails.chequeNo as [Cheque No], PaymentDetails.chequeStatus as [Cheque Date] FROM            PaymentDetails INNER JOIN     PaymentMaster ON PaymentDetails.billNo = PaymentMaster.billNo AND PaymentDetails.supplierName = PaymentMaster.supplierName WHERE        (PaymentDetails.paidAmt <> 0)and PaymentDetails.paidBills='" + billNo + "' ");
            serial_no();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
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
    }
}
