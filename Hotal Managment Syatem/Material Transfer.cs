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
    public partial class Material_Transfer : Form
    {

        Database db = new Database();

        bool flag = false;
        string user_name, barcodedataPrint, productMRP, ProductName;
        float godaunStock, shopStock, transeferStock, Qty;

        public Material_Transfer()
        {
            InitializeComponent();
        }

        private void Material_Transfer_Load(object sender, EventArgs e)
        {

            db.comboFill(cmbProductName, "select distinct item_name from tbl_Warehouse where itemtype=''", "tbl_Warehouse", "item_name", "item_name");

            flag = true;
            float voucherNo = db.GetUniqueId("select max(materialTransferMasterId) from materialTransferMaster");
            txtVoucherNo.Text = voucherNo.ToString();
            //cmbCounterNm.Focus();
            pageload = true;
        }

        private void cmbCounterNm_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (flag)
            //{
            //    if (cmbCounterNm.Text != "")
            //        if (db.ChkDb_Value("select qty from tbl_Warehouse where productName='" + cmbProductName.Text + "' and  counterName ='" + cmbCounterNm.Text + "'"))
            //            txtShopStock.Text = db.getDb_Value("select qty from tbl_Warehouse where productName='" + cmbProductName.Text + "' and  counterName ='" + cmbCounterNm.Text + "'").ToString();
            //}
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                //if (checkBox1.Checked)
                //{
                string category = cbtransferfrom.Text;
                string subcategory = cbtransferto.Text;



                string[] rows = { category, subcategory, cmbProductName.Text, txtTransferQty.Text };
                dataGridView1.Rows.Add(rows);
                // lblGodaunStock.Text = txtGodaunStock.Text;

                txtTransferQty.Text = "0";
                //}
                //else
                //{

                //    string[] rows = { "", "", cmbProductName.Text, txtTransferQty.Text, txtShopStock.Text, (float.Parse(txtTransferQty.Text) + float.Parse(txtShopStock.Text)).ToString() };
                //    dataGridView1.Rows.Add(rows);
                //    lblGodaunStock.Text = txtGodaunStock.Text;

                //    txtTransferQty.Text = "0";
                //}

            }

            catch (Exception ex)
            {
                MessageBox.Show("enter values");
            }
            //  cmbProductName.Text = "";
            cmbProductName.Focus();
        }

        public bool pageload = false;
        private void cmbProductName_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbunit.Text = db.getDbstatus_Value("select isnull ((select unit from tbl_Warehouse where item_name='" + cmbProductName.Text + "') ,0)").ToString();

            cbunit.Enabled = false;
        }

        public string itemType = "0";
        public float oldStockQty, newStockQty;
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cbdrink.Checked)
            {
                itemType="drinks";
            }
            else
            {
                itemType = "0"; 
            }

           if ( dataGridView1.RowCount > 0)
           {
            DialogResult dlg = MessageBox.Show("Do you Really want to Transfer Stock?","Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (dlg == DialogResult.Yes)
            {
                string prodName, Transferqty, stockQty,godaunStock;
              //  float oldStockQty, newStockQty;
                string date = dtpDate.Value.ToString("MM-dd-yyyy");

                    db.insert("insert into materialTransferMaster(trnasferCounterName,transferDate,userName,transferTime,maincategory) values ('" + cbtransferto.Text + "','" + date + "','admin','" + System.DateTime.Now.ToString("hh:mm:ss") + "','" + cbtransferfrom.Text + "')");

                     float masterId = db.getDb_Value("select max(materialTransferMasterId) from materialTransferMaster ");

                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    prodName = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    Transferqty = dataGridView1.Rows[i].Cells[3].Value.ToString();
                 //  stockQty = dataGridView1.Rows[i].Cells[4].Value.ToString();
                    stockQty = "0";


                    if (db.ChkDb_Value("select * from tbl_stock where item_name='" + prodName + "' "))
                    {
                        oldStockQty = db.getDb_Value("select qty from tbl_stock where item_name='" + prodName + "' ");

                        newStockQty = oldStockQty + float.Parse(Transferqty);

                        db.update("update tbl_stock set qty='" + newStockQty + "' ,unit='" + cbunit.Text + "',itemType='" + itemType + "' where item_name='" + prodName + "' ");

                        //for warehouse update 
                        oldStockQty = db.getDb_Value("select qty from tbl_Warehouse where item_name='" + prodName + "' ");

                        newStockQty = oldStockQty - float.Parse(Transferqty);

                        db.update(" update tbl_warehouse set qty= '" + newStockQty + "' where item_name='" + prodName + "'");

                        //if (itemType == "drinks")
                        //{
                        //    db.update(" update tbl_warehouse set qty= '" + newStockQty + "' where item_name='" + prodName + "'");

                        //}

                    }


                    else
                    {
                        db.insert("insert into tbl_stock(item_name,qty,counterName,unit,itemType) values('" + prodName + "','" + Transferqty + "','" + cmbCounterNm.Text + "' ,'" + cbunit.Text + "', '" + itemType + "')");

                        //for warehouse update

                        oldStockQty = db.getDb_Value("select ISNULL((select qty from tbl_Warehouse where item_name='" + prodName + "'),0) ");

                        newStockQty = oldStockQty - float.Parse(Transferqty);


                        db.update(" update tbl_warehouse set qty= '" + newStockQty + "' where item_name='" + prodName + "'");
                    }
             



                    db.insert("insert into materialTransferDetails values('" + masterId + "','" + prodName + "','" + Transferqty + "')");

                       txtCounterLimit.Text = "0";
                     txtGodaunStock.Text = "0";
                     txtShopStock.Text = "0";
                     txtTransferQty.Text = "0";
                     cmbCounterNm.Focus();
                }
              
              
            }
            dataGridView1.Rows.Clear();
           }
           else
           {
               MessageBox.Show("Insert data..");
           
           }

           float voucherNo = db.GetUniqueId("select max(materialTransferMasterId) from materialTransferMaster");
           txtVoucherNo.Text = voucherNo.ToString();
        }

        private void cbdrink_CheckedChanged(object sender, EventArgs e)
        {

            db.cnopen();
            //if (flag)
            //{
            if (cbdrink.Checked)
            {
                ///db.comboFill(cmbProductName, "select * from menu where category like '%" + "DRINKS" + "%'", "menu", "m_name", "m_name");
                db.comboFill(cmbProductName, "select * from tbl_drinkgroup ", "tbl_drinkgroup", "grpName", "grpName");

                // drinksStatus = "drinks";
                //  txt_qtyML.Enabled = true;
            }
            else
            {
                //   db.comboFill(txt_material_nm, "select distinct item_name from tbl_material_add", "tbl_material_add", "item_name", "item_name");
                // db.comboFill(cmbProductName, "select distinct material_nm from material_nm", "material_nm", "material_nm", "material_nm");

                db.comboFill(cmbProductName, "select distinct item_name from tbl_Warehouse where itemtype=''", "tbl_Warehouse", "item_name", "item_name");


                //drinksStatus = "-";
                // txt_qtyML.Enabled = false;
            }
            // }
            db.cnclose();
        }

        private void button1_Click(object sender, EventArgs e)
        {


            foreach (DataGridViewRow row in this.dataGridView1.SelectedRows)
                dataGridView1.Rows.RemoveAt(row.Index);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            cmbCounterNm.Text = cmbProductName.Text = "";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        }


    
}
