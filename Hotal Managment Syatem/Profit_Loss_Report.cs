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
    public partial class Profit_Loss_Report : Form
    {
        Database db = new Database();
        decimal sum, total, totalexpencess, stockamt, stockrate;
        public Profit_Loss_Report()
        {
            InitializeComponent();
        }

        private void Profit_Loss_Report_Load(object sender, EventArgs e)
        {
            db.formFix(this);
            this.CancelButton = button2;
         //  dataGridView1.DataSource = db.Displaygrid("select item_name as Iten_Name,qty as Qty,Amount from tbl_PurchaseDetail");

         //   display();
            //dataGridView1.DataSource = db.Displaygrid("SELECT distinct tbl_PurchaseDetails.ProductName as PurchaseProductName, tbl_PurchaseDetails.barcode as PurchaseBarcode, tbl_PurchaseDetails.Qty as PurchaseQuantity, tbl_PurchaseDetails.Amount as PurchaseAmount, tbl_SalesDetails.ProductName AS SalesProductName,tbl_SalesMaster.billNo,tbl_SalesDetails.salesDetailsId ,tbl_SalesDetails.barcode AS SalesBarcode, tbl_SalesDetails.Rate as SalesRate, tbl_SalesDetails.qty AS SalesQuantity, tbl_SalesMaster.totalamount as SalesTotalAmount, tbl_SalesMaster.paidAmount as PaidAmount, tbl_SalesMaster.remainingAmount as RemainingAmount FROM tbl_SalesDetails INNER JOIN tbl_SalesMaster ON tbl_SalesDetails.salesMasterId = tbl_SalesMaster.salesMasterId CROSS JOIN tbl_PurchaseDetails INNER JOIN  tbl_PurchaseMaster ON tbl_PurchaseDetails.PurchaseMasterId = tbl_PurchaseMaster.PurchaseMasterId"); //purchase & sales
           // dataGridView1.DataSource = db.Displaygrid("SELECT distinct tbl_PurchaseDetails.PurchaseDetailsId,tbl_PurchaseMaster.PurchaseMasterId,tbl_PurchaseDetails.ProductName as PurchaseProductName, tbl_PurchaseDetails.barcode as Barcode, tbl_PurchaseDetails.Qty as PurchaseQuantity ,tbl_PurchaseDetails.PurchaseRate , tbl_PurchaseMaster.Tax, tbl_PurchaseMaster.BillDiscount, tbl_PurchaseMaster.Date FROM tbl_PurchaseDetails INNER JOIN tbl_PurchaseMaster ON tbl_PurchaseDetails.PurchaseMasterId = tbl_PurchaseMaster.PurchaseMasterId");   //purchase report barcodewise
           // dataGridView1.Columns[0].Visible = false;
           // dataGridView1.Columns[1].Visible = false;
        }
        public void display()
        {
             
           //bind the DGV sales Dtls
            dataGridView_salesDtls.DataSource = db.Displaygrid("SELECT  datetime AS Date, SUM(Total_bill) AS CounterAmount FROM  total_bill GROUP BY datetime ");// WHERE (datetime between '" + fdate + "' and '" + sdate + "') 
            dataGridView_salesDtls.Columns[0].Width = 150;
            Salesratecalculate();

            //bind the DGV purchase
            dataGridViewPurchaseDtls.DataSource = db.Displaygrid("SELECT supplier_dtls.sup_name, tbl_purchasemaster.invoice_number, tbl_purchasemaster.amt, tbl_purchasemaster.paid_amt, tbl_purchasemaster.balance,  tbl_purchasemaster.date, tbl_purchasemaster.status FROM            supplier_dtls INNER JOIN  tbl_purchasemaster ON supplier_dtls.sup_id = tbl_purchasemaster. sup_id");
            Purchaseratecalculate();

            //bind the DGV Expencess Dtls
            dataGridViewExpencessDtls.DataSource = db.Displaygrid("select * from tbl_expenses ");
            dataGridViewExpencessDtls.Columns[0].Visible = false;
            //dataGridViewExpencessDtls.Columns[5].Visible = false;
            //dataGridViewExpencessDtls.Columns[6].Visible = false;
            expcal();
           
           
          //  dataGridView4.DataSource = db.Displaygrid("SELECT   item_nm, qty FROM   item_stock2");
           
        }

        public void stockcal()
        {
            try
            {
                stockamt = 0;
                stockrate = 0;
                foreach (DataGridViewRow r in this.dataGridView4.Rows)
                {
                    if (r.Cells[0].Value != null && r.Cells[1].Value != null)
                    {
                        stockrate = (Convert.ToDecimal(r.Cells[0].Value)*(Convert.ToDecimal(r.Cells[1].Value)));
                        stockamt += stockrate;
                    }
                }
                txt_stkamt.Text = stockamt.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void expcal()
        {
            try
            {
                totalexpencess = 0;

                foreach (DataGridViewRow r in this.dataGridViewExpencessDtls.Rows)
                {
                    if (r.Cells[2].Value != null)
                    {
                        totalexpencess += Convert.ToDecimal(r.Cells[2].Value);
                    }
                }
                txt_expencess.Text = totalexpencess.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void Purchaseratecalculate()
        {
            try
            {
                sum = 0;
                
                foreach (DataGridViewRow r in this.dataGridViewPurchaseDtls.Rows)
                {
                    if (r.Cells[2].Value != null)
                    {
                        sum += Convert.ToDecimal(r.Cells[2].Value);
                    }
                }
                txt_purchaseamt.Text = sum.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void Salesratecalculate()
        {
            try
            {
                total = 0;

                foreach (DataGridViewRow r in this.dataGridView_salesDtls.Rows)
                {
                    if (r.Cells[1].Value != null)
                    {
                        total += Convert.ToDecimal(r.Cells[1].Value);
                    }
                }
                txt_salesrate.Text = total.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dtp_from_ValueChanged(object sender, EventArgs e)
        {
            datebet();
        }
        public void datebet()
        {
            string fdate = dtp_from.Value.ToString("MM'-'dd'-'yyyy");
            string sdate = dtp_to.Value.ToString("MM'-'dd'-'yyyy");


            dataGridViewPurchaseDtls.DataSource = db.Displaygrid("SELECT tbl_PurchaseDetail.item_name, tbl_PurchaseDetail.qty, tbl_PurchaseDetail.Amount FROM            tbl_PurchaseDetail INNER JOIN tbl_purchasemaster ON tbl_PurchaseDetail.purchasemasterid = tbl_purchasemaster.purchasemasterid WHERE (tbl_purchasemaster.date BETWEEN '"+fdate+"' AND '"+sdate+"')"); //purchase report only productnamewise
            Purchaseratecalculate();
           
            dataGridView_salesDtls.DataSource = db.Displaygrid("SELECT   menu.m_name, sales_item.qty, sales_item.rate, sales_item.total_amount FROM sales_item INNER JOIN   menu ON sales_item.menu_id = menu.menu_id ");
            Salesratecalculate();
            
            dataGridViewExpencessDtls.DataSource = db.Displaygrid("select * from tbl_expenses where date between '"+fdate+"' and '"+sdate+"'");
            dataGridViewExpencessDtls.Columns[0].Visible = false;
            expcal();


            //bind the DGV sales Dtls
            dataGridView_salesDtls.DataSource = db.Displaygrid("SELECT  datetime AS Date, SUM(Total_bill) AS CounterAmount FROM  total_bill  where datetime between '" + fdate + "' and '" + sdate + "' GROUP BY datetime ");// WHERE (datetime between '" + fdate + "' and '" + sdate + "') 
            dataGridView_salesDtls.Columns[0].Width = 150;
            Salesratecalculate();

            //bind the DGV purchase
            dataGridViewPurchaseDtls.DataSource = db.Displaygrid("SELECT supplier_dtls.sup_name, tbl_purchasemaster.invoice_number, tbl_purchasemaster.amt, tbl_purchasemaster.paid_amt, tbl_purchasemaster.balance,  tbl_purchasemaster.date, tbl_purchasemaster.status FROM    supplier_dtls INNER JOIN  tbl_purchasemaster ON supplier_dtls.sup_id = tbl_purchasemaster.sup_id where tbl_purchasemaster.date between '" + fdate + "' and '" + sdate + "'");
            Purchaseratecalculate();

            //bind the DGV Expencess Dtls
            dataGridViewExpencessDtls.DataSource = db.Displaygrid("select id,material_nm,amount from tbl_expenses where date between '" + fdate + "' and '" + sdate + "'");
            dataGridViewExpencessDtls.Columns[0].Visible = false;
            //dataGridViewExpencessDtls.Columns[4].Visible = false;
            //dataGridViewExpencessDtls.Columns[5].Visible = false;
            expcal();
           
        }

        private void dtp_to_ValueChanged(object sender, EventArgs e)
        {
            datebet();
            //dataGridView1.DataSource = db.Displaygrid("SELECT distinct tbl_PurchaseMaster.PurchaseMasterId, tbl_PurchaseDetails.ProductName, tbl_PurchaseDetails.PurchaseRate, tbl_PurchaseDetails.Qty, tbl_PurchaseMaster.Tax, tbl_PurchaseMaster.BillDiscount, tbl_PurchaseMaster.GrandTotal ,tbl_PurchaseMaster.Date  FROM  tbl_PurchaseDetails INNER JOIN tbl_PurchaseMaster ON tbl_PurchaseDetails.PurchaseMasterId = tbl_PurchaseMaster.PurchaseMasterId where tbl_PurchaseMaster.Date between '" + dtp_from.Text + "' and '" + dtp_to.Text + "' "); //purchase report only productnamewise
            //Purchaseratecalculate();
            //dataGridView2.DataSource = db.Displaygrid("SELECT tbl_SalesDetails.ProductName, tbl_SalesDetails.qty, tbl_SalesDetails.Rate, tbl_SalesMaster.billNo, tbl_SalesMaster.VAT, tbl_SalesMaster.billDiscount, tbl_SalesMaster.totalamount, tbl_SalesMaster.paidAmount, tbl_SalesMaster.remainingAmount,tbl_SalesMaster.date FROM tbl_SalesMaster INNER JOIN tbl_SalesDetails ON tbl_SalesMaster.salesMasterId = tbl_SalesDetails.salesMasterId where tbl_SalesMaster.date between'" + dtp_from.Text + "' and '" + dtp_to.Text + "'");
            //Salesratecalculate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            float purchaserate = float.Parse(txt_purchaseamt.Text);
            float expencess = float.Parse(txt_expencess.Text);
            float salesrate = float.Parse(txt_salesrate.Text);
            float input = purchaserate + expencess;
            float profit = salesrate - input;
            txt_profit.Text = profit.ToString();
            /*
             where [date ]*/

            //bind the dgv sales dtls
            //dataGridView_salesDtls.DataSource = db.Displaygrid("SELECT  datetime AS Date, SUM(Total_bill) AS CounterAmount FROM  total_bill WHERE (datetime between '" + fdate + "' and '" + sdate + "') GROUP BY datetime ");// AND (invoice_dtls.invoice_id = @id)");
            //dataGridView_salesDtls.Columns[0].Width = 150;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Excel Documents (*.xls)|*.xls";
                sfd.FileName = "ProfitLossReport.xls";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    //ToCsV(dataGridView1, @"c:\export.xls";
                    db.ToCsV(dataGridViewPurchaseDtls, sfd.FileName); // Here dataGridview1 is your grid view name 
                    db.ToCsV(dataGridView_salesDtls, sfd.FileName);
                    db.ToCsV(dataGridViewExpencessDtls, sfd.FileName); 
                    MessageBox.Show("File Sucessfully Expoted");

                    // string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Solution.xls");
                    // string filePath = Path.Combine(Environment.GetFolderPath(sfd.Selected), sfd.FileName);

                    Process.Start(sfd.FileName);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        
    }
}
