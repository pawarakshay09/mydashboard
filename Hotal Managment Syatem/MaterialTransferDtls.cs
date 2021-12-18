using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Hotal_Managment_Syatem
{
    public partial class MaterialTransferDtls : Form
    {
        Database db = new Database();
        string allMaterialTransferDtlQry = "", userNm, fromDate, toDate;
        bool pageLoad = false;
        string[] name = new string[200];
        float[] qty = new float[200];
        public MaterialTransferDtls()
        {
            InitializeComponent();
        }

        private void MaterialTransferDtls_Load(object sender, EventArgs e)
        {
            db.formFix(this);
            this.CancelButton = button1;
            // db.comboFill(cmbCounterNm, "select * from AddCounter", "AddCounter", "CounterName", "CounterName");
            cmbCounterNm.Text = "All";
            checkBox1.Checked = false;
            dataGridView1.DataSource = db.Displaygrid("select materialTransferMasterId as[Voucher No],transferDate as [Date],trnasferCounterName as[Transfer To],userName as [User Name],transferTime as Time from materialTransferMaster  order by materialTransferMasterId desc");
            dataGridView1.Columns[0].Width = 70;
            dataGridView1.Columns[2].Width = 110;
            dataGridView1.ReadOnly = true;

            pageLoad = true; //used to chk whr pageload event is done or not 
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                dtpfrm.Enabled = true;
                dtpto.Enabled = true;
            }

            else
            {
                dtpfrm.Enabled = false;
                dtpto.Enabled = false;
            }
        }

        private void cmbCounterNm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pageLoad)
            {
                BindgridQry();
                //dataGridView1.ClearSelection();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            checkBox1.Checked = false;
            cmbCounterNm.Text = "All";
            // BindgridQry();
            string fromDate = dtpfrm.Value.ToString("MM-dd-yyyy");
            string toDate = dtpto.Value.ToString("MM-dd-yyyy");
            dataGridView1.DataSource = db.Displaygrid("select materialTransferMasterId as[Voucher No],transferDate as [Date],trnasferCounterName as[Transfer To],userName as [User Name],transferTime as Time from materialTransferMaster where userName='" + userNm + "' order by materialTransferMasterId desc");
            dataGridView1.Columns[0].Width = 70;
            dataGridView1.Columns[2].Width = 110;
            dataGridView1.ReadOnly = true;
            dataGridView1.ClearSelection();
        }

        public void BindgridQry()
        {
            string fromDate = dtpfrm.Value.ToString("MM-dd-yyyy");
            string toDate = dtpto.Value.ToString("MM-dd-yyyy");
            string qry = "select materialTransferMasterId as[Voucher No],transferDate as [Date],trnasferCounterName as[Transfer To],userName as [User Name],transferTime as Time from materialTransferMaster where  ";
            string orderBy = " order by materialTransferMasterId desc";
            int flag = 0;
            if (cmbCounterNm.Text != "All")
            {
                if (flag != 0)
                    qry += " and ";

                qry += "trnasferCounterName='" + cmbCounterNm.Text + "'";
                flag++;
            }

            if (checkBox1.Checked)
            {
                if (flag != 0)
                    qry += " and ";

                qry += " transferDate between '" + fromDate + "' and '" + toDate + "'";
                flag++;
            }
            if (txtBillNo.Text != "")
            {
                if (flag != 0)
                    qry += " and ";

                qry += " materialTransferMasterId like '" + txtBillNo.Text + "%'";
                flag++;
            }
            if (flag == 0)
            {
                dataGridView1.DataSource = db.Displaygrid("select materialTransferMasterId as[Voucher No],transferDate as [Date],trnasferCounterName as[Transfer To],userName as [User Name],transferTime as Time from materialTransferMaster order by materialTransferMasterId desc");

            }

            else
            {
                allMaterialTransferDtlQry = qry + orderBy;
                dataGridView1.DataSource = db.Displaygrid(qry + orderBy);
            }
            dataGridView1.Columns[0].Width = 70;
            dataGridView1.Columns[2].Width = 110;
            dataGridView1.ReadOnly = true;
        }

        private void dtpfrm_ValueChanged(object sender, EventArgs e)
        {
            BindgridQry();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            int i = dataGridView1.SelectedCells[0].RowIndex;
            float voucherNo = float.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());
            dataGridView2.DataSource = db.Displaygrid("select  materialName as[Material Name],materialQty as[Qty],materialTransferDtlsId from materialTransferDetails INNER JOIN materialTransferMaster ON materialTransferDetails.materialTransferMasterId = materialTransferMaster.materialTransferMasterId where materialTransferMaster.materialTransferMasterId ='" + voucherNo + "'");
            dataGridView2.ClearSelection();
            dataGridView2.Columns[2].Visible = false;
            dataGridView2.Columns[0].Width = 180;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDel1_Click(object sender, EventArgs e)
        {

            if (dataGridView2.Rows.Count == 0)
            {
                int i = dataGridView1.SelectedCells[0].RowIndex;
                float voucherNo = float.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());
                db.delete("delete from materialTransferMaster where materialTransferMasterId ='" + voucherNo + "' and userName='" + userNm + "'");
                MessageBox.Show("Recored deleted..");
                //  BindgridQry();
                //stockUpdate();

                //float stockqty = 0;
                //for (int j = 0; i < 200; j++)
                //{
                //    if (name[j] == null)
                //        break;
                //    stockqty = db.getDb_Value("select qty from WarehouseStock where productName='" + name[j] + "'") - (qty[j]);
                //    db.update("update WarehouseStock set qty='" + stockqty + "' where productName='" + name[j] + "'");
                //}


                BindgridQry();
            }

            else
            {
                MessageBox.Show("first delete the material details...");
            }
        }
        void stockUpdate()
        {


            int i = dataGridView1.SelectedCells[0].RowIndex;
            float voucherNo = float.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString());

            int j = 0;
            db.cnopen();
            SqlCommand cmd = new SqlCommand("SELECT        materialTransferDetails.materialName, materialTransferDetails.materialQty  FROM            materialTransferDetails INNER JOIN    materialTransferMaster ON materialTransferDetails.materialTransferMasterId = materialTransferMaster.materialTransferMasterId WHERE        (materialTransferMaster.materialTransferMasterId= '" + voucherNo + "') and  userName='" + userNm + "'", db.cn);
            // SqlCommand cmd = new SqlCommand("SELECT        PurchaseDetails.productName, PurchaseDetails.qty ,PurchaseDetails.discountedQty FROM            PurchaseDetails INNER JOIN    PurchaseMaster ON PurchaseDetails.purchaseMasterId = PurchaseMaster.purchaseMasterId WHERE        (PurchaseMaster.purchaseInvoiceNo= '" + billNo + "') and  userName='" + userNm + "'", db.cn);

            SqlDataReader rd = cmd.ExecuteReader();
            try
            {
                while (rd.Read())
                {
                    name[j] = rd["materialName"].ToString();
                    qty[j] = float.Parse(rd["materialQty"].ToString());
                    // discountedQty[j] = float.Parse(rd["discountedQty"].ToString());
                    j++;
                }
                db.cnclose();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {

            int i = dataGridView2.SelectedCells[0].RowIndex;
            float voucher_No = float.Parse(dataGridView2.Rows[i].Cells[2].Value.ToString());
            string productName = dataGridView2.Rows[i].Cells[0].Value.ToString();
            db.delete("delete from materialTransferDetails where materialTransferDtlsId ='" + voucher_No + "' and materialName='" + productName + "'");
            MessageBox.Show("Recored deleted..");

            int j = dataGridView1.SelectedCells[0].RowIndex;
            float voucherNo = float.Parse(dataGridView1.Rows[j].Cells[0].Value.ToString());
            //dataGridView2.DataSource = db.Displaygrid("select materialTransferDtlsId, materialName as[Product Name],materialQty as[Qty] from materialTransferDetails where materialTransferMasterId ='" + voucherNo + "'");
            //dataGridView2.Columns[0].Visible = false;


            // item delete update stock///
            string productNm = dataGridView2.Rows[i].Cells[0].Value.ToString();
            float qty = float.Parse(dataGridView2.Rows[i].Cells[1].Value.ToString());
            float warehouswoldqty = db.getDb_Value("select qty from WarehouseStock where productName ='" + productNm + "'");
            float shopStockOldqty = db.getDb_Value("select qty from Stock where productName='" + productNm + "' ");
            float newQty = warehouswoldqty + qty;
            float newStockqty = shopStockOldqty - qty;
            db.update("update WarehouseStock set qty='" + newQty + "' where productName='" + productNm + "' ");
            db.update("update Stock set qty='" + newStockqty + "' where productName='" + productNm + "' ");

            dataGridView2.DataSource = db.Displaygrid("select  materialName as[Product Name],materialQty as[Qty],materialTransferDtlsId from materialTransferDetails where materialTransferMasterId ='" + voucherNo + "'");
            dataGridView2.Columns[2].Visible = false;
            dataGridView2.ClearSelection();

            dataGridView2.Columns[0].Width = 180;
        }

        private void txtBillNo_TextChanged(object sender, EventArgs e)
        {
            BindgridQry();
        }
    }
}
