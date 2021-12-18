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
    public partial class Deleted_Item_Details : Form
    {
        Database db = new Database();
        string dateFrm, dateTo;
        public Deleted_Item_Details()
        {
            InitializeComponent();
        }

      

        private void Deleted_Item_Details_Load(object sender, EventArgs e)
        {
            db.formFix(this);
            this.CancelButton = btnClose;
            dataGridView1.DataSource = db.Displaygrid("SELECT        orderId AS [Order Id], tblNo AS [Table No.], deleteDate AS Date, deleteTime AS Time,  itemName AS [Item Name], rate AS Rate, qty AS Qty,  amount AS Amount,reason as Reason, userName AS [User Name] FROM            DeletedItemMaster");
            cellFormation();
        }
        void cellFormation()
        {
            dataGridView1.Columns[0].Width = 60;
            dataGridView1.Columns[1].Width = 60;
            dataGridView1.Columns[2].Width =80;
            dataGridView1.Columns[3].Width = 70;
            dataGridView1.Columns[4].Width = 240;
            dataGridView1.Columns[5].Width = 70;
            dataGridView1.Columns[6].Width = 70;
            dataGridView1.Columns[7].Width = 70;
            dataGridView1.Columns[8].Width = 110;
            dataGridView1.Columns[9].Width = 80;

        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkDate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDate.Checked)
            {
                dtpFrom.Enabled = true;
                dtpTo.Enabled = true;
                datechange();

            }
            else
            {
                dtpFrom.Enabled = false;
                dtpTo.Enabled = false;
                dataGridView1.DataSource = db.Displaygrid("SELECT        orderId AS [Order Id], tblNo AS [Table No.], deleteDate AS Date, deleteTime AS Time,  itemName AS [Item Name], rate AS Rate, qty AS Qty,  amount AS Amount,reason as Reason, userName AS [User Name] FROM            DeletedItemMaster");
                cellFormation();

            }

        }

        private void dtpFrom_ValueChanged(object sender, EventArgs e)
        {
            datechange();
        }
        void datechange()
        {
            dateFrm = dtpFrom.Value.ToString("MM/dd/yyyy");
            dateTo = dtpTo.Value.ToString("MM/dd/yyyy");
            dataGridView1.DataSource = db.Displaygrid("SELECT        orderId AS [Order Id], tblNo AS [Table No.], deleteDate AS Date, deleteTime AS Time, itemName AS [Item Name], rate AS Rate, qty AS Qty,  amount AS Amount, reason as Reason,userName AS [User Name] FROM            DeletedItemMaster where deleteDate between '" + dateFrm + "' and '" + dateTo + "' ");
            cellFormation();

        }

        private void dtpTo_ValueChanged(object sender, EventArgs e)
        {
            datechange();

        }

        private void txtOrderId_TextChanged(object sender, EventArgs e)
        {
            if (db.ChkDb_Value("SELECT        orderId AS [Order Id], tblNo AS [Table No.], deleteDate AS Date, deleteTime AS Time,  itemName AS [Item Name], rate AS Rate, qty AS Qty,  amount AS Amount,reason as Reason, userName AS [User Name] FROM            DeletedItemMaster where orderId like '" + txtOrderId.Text + "%'"))
            {
                dataGridView1.DataSource = db.Displaygrid("SELECT        orderId AS [Order Id], tblNo AS [Table No.], deleteDate AS Date, deleteTime AS Time,  itemName AS [Item Name], rate AS Rate, qty AS Qty,  amount AS Amount,reason as Reason, userName AS [User Name] FROM            DeletedItemMaster where orderId like '" + txtOrderId.Text + "%'");
                cellFormation();
            }
            else
            {
                MessageBox.Show("No Record Found");
                dataGridView1.DataSource = "";
            }
        }

        private void txtOrderId_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Excel Documents (*.xls)|*.xls";
                sfd.FileName = "DeletedItemDetails.xls";

                if (sfd.ShowDialog() == DialogResult.OK)
                {

                    //ToCsV(dataGridView1, @"c:\export.xls";
                    db.withReportTitle_ToCsV(dataGridView1, sfd.FileName, "Deleted Item Report"); // Here dataGridview1 is your grid view name 
                    MessageBox.Show("File Sucessfully Exported");
                    Process.Start(sfd.FileName);


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

      
    }
}
