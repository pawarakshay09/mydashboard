using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;

namespace Hotal_Managment_Syatem
{
    public partial class Excel_Report : Form
    {
        Database db = new Database();
        bool flag = false;
        public Excel_Report()
        {
            InitializeComponent();
            
        }

        private void Excel_Report_Load(object sender, EventArgs e)
        {
            txtPurchase.Text = db.getDb_Value("SELECT        SUM(amt) FROM            tbl_purchasemaster").ToString();
            txtSales.Text = db.getDb_Value("SELECT        SUM(Total_bill) FROM            total_bill").ToString();
            txtexp.Text = db.getDb_Value("SELECT        SUM(amount) FROM            tbl_expenses").ToString();
            txtCancelOrder.Text = db.getDb_Value("SELECT        SUM(amount) FROM            Cancel_order").ToString();
            flag = true;
            cal();
        }
        void cal()
        {
            if (flag && txtexp.Text!="" && txtCancelOrder.Text!="" && txtOpeningBalance.Text!="" && txtPurchase.Text!="" && txtSales.Text!="")
            txtTotal.Text = ((float.Parse(txtOpeningBalance.Text) + float.Parse(txtSales.Text)) - (float.Parse(txtPurchase.Text) + float.Parse(txtexp.Text) + float.Parse(txtCancelOrder.Text))).ToString();
        }

        private void txtOpeningBalance_TextChanged(object sender, EventArgs e)
        {
           
            cal();
        }

        private void txtSales_TextChanged(object sender, EventArgs e)
        {
          
            cal();
        }

        private void txtPurchase_TextChanged(object sender, EventArgs e)
        {
           
            cal();
        }

        private void txtexp_TextChanged(object sender, EventArgs e)
        {
            cal();
        }

        private void txtCancelOrder_TextChanged(object sender, EventArgs e)
        {
            cal();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            //int a, b, c, total;

            //a = Convert.ToInt32(textBox1.Text);
            //b = Convert.ToInt32(textBox2.Text);
            //c = Convert.ToInt32(textBox3.Text);
            //total = (a + b + c);
            //textBox4.Text = total.ToString();


            Microsoft.Office.Interop.Excel.Application xla = new Microsoft.Office.Interop.Excel.Application();

            Workbook wb = xla.Workbooks.Add(XlSheetType.xlWorksheet);

            Worksheet ws = (Worksheet)xla.ActiveSheet;
            xla.Visible = true;
            ws.Cells[1, 1] = "Opening Balance";
            //Range chrtrange;
            //chrtrange.EntireColumn.ColumnWidth = 250;
            
            ws.Cells[1, 2] = "Sales";
           
            ws.Cells[1, 3] = "Purchase";
            ws.Cells[1, 4] = "Expences";
            ws.Cells[1, 5] = "Cancel Order";
            ws.Cells[1, 6] = "Total";
            ws.Columns.ColumnWidth = 20;


            ws.Cells[2, 1] = txtOpeningBalance.Text;
          //  ws.Columns["2:1"].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            ws.Cells[2, 2] = txtSales.Text;
          
            ws.Cells[2, 3] = txtPurchase.Text;
            ws.Cells[2, 4] = txtexp.Text;
            ws.Cells[2, 5] = txtCancelOrder.Text;
            ws.Cells[2, 6] = txtTotal.Text;


            ws.Cells.Style.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            
        }

        private void txtOpeningBalance_TextChanged_1(object sender, EventArgs e)
        {
            if (txtOpeningBalance.Text == "")
            { 
                txtOpeningBalance.Text = "0"; 
            }

            cal();
        }

        private void txtSales_TextChanged_1(object sender, EventArgs e)
        {
            cal();
        }

        private void txtPurchase_TextChanged_1(object sender, EventArgs e)
        {
            cal();
        }

        private void txtexp_TextChanged_1(object sender, EventArgs e)
        {
            cal();
        }

        private void txtCancelOrder_TextChanged_1(object sender, EventArgs e)
        {
            cal();
        }

        private void txtOpeningBalance_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);
        }
    }
}
