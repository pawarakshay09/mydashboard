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
    public partial class Purchase_Form : Form
    {
        Database db = new Database();
        int id;
        float amount;
        bool flag = false,cmbflg=false;
        float amt;
        string status = "";
        float discount, tax;
        float quantity;
        string drinksStatus = "";
        public Purchase_Form()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Unit unit = new Unit();
            unit.Show();
            db.comboFill(cmb_unit, "select distinct unit_name from tbl_unit", "tbl_unit", "unit_name", "unit_name");

        }

        private void Purchase_Form_Load(object sender, EventArgs e)
        {
            
            db.formFix(this);
            this.CancelButton = btn_close;
            fillcombo();
            cmbflg = true;
            
        }
        public void fillcombo()
        {
            cmbflg = false;
            db.comboFill(cmb_v_name, "select  distinct sup_name from supplier_dtls", "supplier_dtls", "sup_name", "sup_name");
            cmb_v_name.Text="--Select--";
            db.comboFill(cmb_unit, "select distinct unit_name from tbl_unit", "tbl_unit", "unit_name", "unit_name");
            cmb_unit.Text = "--Select--";
            db.comboFill(txt_material_nm, "select distinct material_nm from material_nm", "material_nm", "material_nm", "material_nm");
            txt_material_nm.Text = "--Select--";
            flag = true;
        }
       
        public void clear()
        {
            txt_invoiceno.Text = "";
            txt_material_nm.Text = "--Select--";
            txt_Qty.Text = "0";
            cmb_unit.Text = "";
            txt_rate.Text = "0";
            txt_amt.Text = "0";
            txt_desc.Text = "";
            cmb_v_name.Text = "";
            cmb_unit.Text = "";
            txt_paid.Text = "0";
            textBox_Discount.Text = "0";
            textBoxTax.Text = "0";
            txt_amount.Text = "0";
            label_grandTotal.Text = "0";
            txt_balance.Text = "0";
          //  dataGridView1.DataSource = "";
            dataGridView1.Rows.Clear();
            txt_invoiceno.Focus();
        }


        // code by  yogesh    11 04 2019
        private void btn_save_Click(object sender, EventArgs e)
        {
            float totalQty, Menuqty;

            try
            {
                if (txt_invoiceno.Text == "")
                {
                    MessageBox.Show("Please Enter Invoice No.");
                    txt_invoiceno.Focus();

                }
                else if (cmb_v_name.Text == "--Select--" || cmb_v_name.Text == "")
                {
                    MessageBox.Show("Please Enter Supplier Name.");
                    cmb_v_name.Focus();
                }

                else if (dataGridView1.RowCount == 0)
                    MessageBox.Show("Please Enter At Least One Item");

                else
                {

                    if (txt_balance.Text == "0")
                    {
                        status = "Paid";
                    }
                    else
                    {
                        status = "Unpaid";
                    }

                    string date = dateTimePicker1.Value.ToString("MM'-'dd'-'yyyy");
                    string v_id = db.getDb_Value("select sup_id from supplier_dtls where sup_name='" + cmb_v_name.Text + "'").ToString();
                    db.insert("insert into tbl_purchasemaster values ('" + v_id + "','" + txt_invoiceno.Text + "','" + label_grandTotal.Text + "','" + txt_paid.Text + "','" + txt_balance.Text + "','" + date + "','" + txt_desc.Text + "','" + status + "','" + tax + "','" + discount + "')");
                    db.insert("insert into payment (grp,cust,amt,paid_amt,balance,date,bill_no)values('Purchase','" + cmb_v_name.Text + "','" + label_grandTotal.Text.Trim() + "','" + txt_paid.Text + "','" + txt_balance.Text + "','" + date + "','" + txt_invoiceno.Text + "')");

                    string master_id = db.getDb_Value("select max(purchasemasterid) from tbl_purchasemaster").ToString();
                    for (int i = 0; i <= dataGridView1.Rows.Count - 1; i++)
                    {
                        string material_name = dataGridView1.Rows[i].Cells[0].Value.ToString();
                        string qty = dataGridView1.Rows[i].Cells[1].Value.ToString();
                        string unit = dataGridView1.Rows[i].Cells[2].Value.ToString();
                        string rate = dataGridView1.Rows[i].Cells[3].Value.ToString();
                        string t_amt = dataGridView1.Rows[i].Cells[4].Value.ToString();
                        float qtyMl = float.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString());

                        db.insert("insert into tbl_PurchaseDetail values('" + master_id + "','" + material_name + "','" + qty + "','" + unit + "','" + t_amt + "','" + rate + "')");

                        string pur_id = db.getDb_Value("select max(pur_id) from tbl_PurchaseDetail").ToString();

                        float new_qty = 0, newQtyML = 0, old_qtyML = 0;
                        if (db.ChkDb_Value("select * from tbl_Warehouse where item_name='" + material_name + "'"))
                        {

                            float old_qty = db.getDb_Value("select qty from tbl_Warehouse where item_name='" + material_name + "'");
                            quantity = float.Parse(qty.ToString());
                            if (chk_drink.Checked)
                            {
                                //  float Menuqty = db.getDb_Value("select MenuQty from menu where drinkGroup='" + material_name + "'");
                                Menuqty = float.Parse(qtyMl.ToString());
                                totalQty = Menuqty * quantity;
                                new_qty = (old_qty) + (quantity);

                                //old_qtyML = db.getDb_Value("select qty_in_ML from tbl_stock where item_name='" + material_name + "'");
                                //newQtyML = old_qtyML + totalQty;
                            }
                            else
                                new_qty = (old_qty) + (quantity);

                            db.update("update tbl_Warehouse set qty='" + new_qty + "' where item_name='" + material_name + "'");
                        }
                        else
                        {
                            if (chk_drink.Checked)
                            {
                                //float Menuqty = db.getDb_Value("select MenuQty from menu where drinkGroup='" + material_name + "' ");
                                Menuqty = float.Parse(qtyMl.ToString());

                                totalQty = Menuqty * float.Parse(qty);
                                // new_qty = (old_qty) + (totalQty);
                                db.insert("insert into tbl_Warehouse (pur_id,item_name,qty,unit,itemType) values('" + pur_id + "','" + material_name + "','" + qty + "','" + unit + "','" + drinksStatus + "')");
                            }
                            else

                                db.insert("insert into tbl_Warehouse (pur_id,item_name,qty,unit,itemType) values('" + pur_id + "','" + material_name + "','" + qty + "','" + unit + "','" + drinksStatus + "')");
                        }


                    }
                    //payment form entries
                    db.insert("insert into PaymentMaster values('" + date + "','0','" + txt_invoiceno.Text + "','" + cmb_v_name.Text + "','" + label_grandTotal.Text + "','" + status + "','" + txt_paid.Text + "','" + txt_balance.Text + "','admin')");
                    db.insert("insert into PaymentDetails values('Cash','0','" + txt_invoiceno.Text + "','" + cmb_v_name.Text + "','" + label_grandTotal.Text + "','" + txt_paid.Text + "','" + txt_balance.Text + "','" + date + "','" + "0" + "','" + "0" + "','" + date + "','" + "0" + "','" + "0" + "','" + txt_invoiceno.Text + "')");

                    MessageBox.Show("Record Inserted Successfully");

                    fillcombo();
                    clear();
                }
                //}

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        // old code for save btn
        //private void btn_save_Click(object sender, EventArgs e)
        //{
        //    float totalQty, Menuqty;
           
        //    try
        //    {
        //        if (txt_invoiceno.Text == "")
        //        {
        //            MessageBox.Show("Please Enter Invoice No.");
        //            txt_invoiceno.Focus();

        //        }
        //        else if (cmb_v_name.Text == "--Select--" || cmb_v_name.Text == "")
        //        {
        //            MessageBox.Show("Please Enter Supplier Name.");
        //            cmb_v_name.Focus();
        //        }
                   
        //        else if(dataGridView1.RowCount==0)
        //            MessageBox.Show("Please Enter At Least One Item");
                
        //        else
        //        {
                   
        //            if (txt_balance.Text == "0")
        //            {
        //                status = "Paid";
        //            }
        //            else
        //            {
        //                status = "Unpaid";
        //            }

        //            string date = dateTimePicker1.Value.ToString("MM'-'dd'-'yyyy");
        //            string v_id = db.getDb_Value("select sup_id from supplier_dtls where sup_name='" + cmb_v_name.Text + "'").ToString();
        //            db.insert("insert into tbl_purchasemaster values ('" + v_id + "','" + txt_invoiceno.Text + "','" + label_grandTotal.Text + "','" + txt_paid.Text + "','" + txt_balance.Text + "','" + date + "','" + txt_desc.Text + "','" + status + "','" + tax + "','" + discount + "')");
        //            db.insert("insert into payment (grp,cust,amt,paid_amt,balance,date,bill_no)values('Purchase','" + cmb_v_name.Text + "','" + label_grandTotal.Text.Trim() + "','" + txt_paid.Text + "','" + txt_balance.Text + "','" + date + "','" + txt_invoiceno.Text + "')");

        //            string master_id = db.getDb_Value("select max(purchasemasterid) from tbl_purchasemaster").ToString();
        //            for (int i = 0; i <= dataGridView1.Rows.Count - 1; i++)
        //            {
        //                string material_name = dataGridView1.Rows[i].Cells[0].Value.ToString();
        //                string qty = dataGridView1.Rows[i].Cells[1].Value.ToString();
        //                string unit = dataGridView1.Rows[i].Cells[2].Value.ToString();
        //                string rate = dataGridView1.Rows[i].Cells[3].Value.ToString();
        //                string t_amt = dataGridView1.Rows[i].Cells[4].Value.ToString();
        //                float qtyMl = float.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString());
                       
        //                db.insert("insert into tbl_PurchaseDetail values('" + master_id + "','" + material_name + "','" + qty + "','" + unit + "','" + t_amt + "','" + rate + "')");

        //                string pur_id = db.getDb_Value("select max(pur_id) from tbl_PurchaseDetail").ToString();

        //                float new_qty = 0, newQtyML = 0, old_qtyML = 0;
        //                if (db.ChkDb_Value("select * from tbl_stock where item_name='" + material_name + "'"))
        //                {

        //                    float old_qty = db.getDb_Value("select qty from tbl_stock where item_name='" + material_name + "'");
        //                    quantity = float.Parse(qty.ToString());
        //                    if (chk_drink.Checked)
        //                    {
        //                        //  float Menuqty = db.getDb_Value("select MenuQty from menu where drinkGroup='" + material_name + "'");
        //                        Menuqty = float.Parse(qtyMl.ToString());
        //                        totalQty = Menuqty * quantity;
        //                        new_qty = (old_qty) + (quantity);

        //                        //old_qtyML = db.getDb_Value("select qty_in_ML from tbl_stock where item_name='" + material_name + "'");
        //                        //newQtyML = old_qtyML + totalQty;
        //                    }
        //                    else
        //                        new_qty = (old_qty) + (quantity);

        //                    db.update("update tbl_stock set qty='" + new_qty + "' where item_name='" + material_name + "'");
        //                }
        //                else
        //                {
        //                    if (chk_drink.Checked)
        //                    {
        //                        //float Menuqty = db.getDb_Value("select MenuQty from menu where drinkGroup='" + material_name + "' ");
        //                        Menuqty = float.Parse(qtyMl.ToString());

        //                        totalQty = Menuqty * float.Parse(qty);
        //                        // new_qty = (old_qty) + (totalQty);
        //                        db.insert("insert into tbl_stock (pur_id,item_name,qty,unit,itemType) values('" + pur_id + "','" + material_name + "','" + qty + "','" + unit + "','" + drinksStatus + "')");
        //                    }
        //                    else

        //                        db.insert("insert into tbl_stock (pur_id,item_name,qty,unit,itemType) values('" + pur_id + "','" + material_name + "','" + qty + "','" + unit + "','" + drinksStatus + "')");
        //                }


        //            }
        //            //payment form entries
        //            db.insert("insert into PaymentMaster values('" + date + "','0','" + txt_invoiceno.Text + "','" + cmb_v_name.Text + "','" + label_grandTotal.Text + "','" + status + "','" + txt_paid.Text + "','" + txt_balance.Text + "','admin')");
        //            db.insert("insert into PaymentDetails values('Cash','0','" + txt_invoiceno.Text + "','" + cmb_v_name.Text + "','" + label_grandTotal.Text + "','" + txt_paid.Text + "','" + txt_balance.Text + "','" + date + "','" + "0" + "','" + "0" + "','" + date + "','" + "0" + "','" + "0" + "','"+txt_invoiceno.Text+"')");

        //            MessageBox.Show("Record Inserted Successfully");

        //            fillcombo();
        //            clear();
        //        }
        //        //}
               
        //    }

        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            
            
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            clear();
            dataGridView1.DataSource = "";
           
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        

        private void txt_rate_TextChanged(object sender, EventArgs e)
        {


            //cal_amt();
            //txt_amt.Text = amt.ToString();
         }

        private void txt_amt_TextChanged(object sender, EventArgs e)
        {
            
        }
       
        private void txt_Qty_TextChanged(object sender, EventArgs e)
        {
            rateQtyCal();

        }

        private void btn_add_Click(object sender, EventArgs e)
        {
           
            if (txt_material_nm.Text == "" || txt_material_nm.Text == "--Select--")
                MessageBox.Show("Enter Material Name");
            else if (txt_Qty.Text == "")
                MessageBox.Show("Enter Qty");
            else if (cmb_unit.Text == "" || cmb_unit.Text == "--Select--")
                MessageBox.Show("Enter Unit");
            else if (textBox_rate.Text == "")
                MessageBox.Show("Enter Rate");
            
            else
            {
                string qty = "";
                               
                try
                {
                    

                    string[] add = { txt_material_nm.Text, txt_Qty.Text, cmb_unit.Text, textBox_rate.Text, txt_amount.Text, txt_qtyML.Text };

                  
                        dataGridView1.Rows.Add(add);
                        dgv_cal();
                        db.update("update tbl_drinkgroup set qtyInML='" + txt_qtyML.Text + "' where grpName='" + txt_material_nm.Text + "'");
                        //  txt_material_nm.Text = "";
                        txt_Qty.Text = "0";
                        txt_rate.Text = "0";
                        cmb_unit.Text = "--Select--";
                        label10.Text = "";
                        //textBox_rate.Text = "";
                        txt_amount.Text = "0";
                        txt_qtyML.Text = "0";

                        textBox_rate.Text = "0";
                 
                }

                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            txt_material_nm.Focus();
        }

        private void txt_rate_Leave(object sender, EventArgs e)
        {
           
        }
        public void dgv_cal()
        {
             amount = 0;
            for (int i = 0; i <= dataGridView1.Rows.Count - 1; i++)
            {
                amount += float.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString());
            }
            txt_amt.Text = amount.ToString();
            label_grandTotal.Text = amount.ToString();
            txt_balance.Text = amount.ToString();
            TaxDiscount_cal();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clear();
            fillcombo();
            dgv_cal();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                

                DataGridViewRow dr = dataGridView1.SelectedRows[0];
                //int amount = int.Parse(dr.Cells[4].Value.ToString());
                //int amount1 = int.Parse(lblTotalAmt.Text);
                //lblTotalAmt.Text = (amount1 - amount).ToString();
                foreach (DataGridViewRow item in this.dataGridView1.SelectedRows)
                {
                    dataGridView1.Rows.RemoveAt(item.Index);
                }
                dgv_cal();



               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txt_material_nm_TextChanged(object sender, EventArgs e)
        {
            if(db.ChkDb_Value("select unit from tbl_material_add where item_name = '"+txt_material_nm.Text+"'"))
            {
                cmb_unit.Text = db.getDbstatus_Value("select unit from tbl_material_add where item_name ='"+txt_material_nm.Text+"'");
            }
          
        }

       
       

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!chk_drink.Checked)
            {
                Material am = new Material();
                am.ShowDialog();
                db.comboFill(txt_material_nm, "select distinct material_nm from material_nm", "material_nm", "material_nm", "material_nm");
               

            }
            else
            {
                Add_Drink_Group grp = new Add_Drink_Group();
                grp.ShowDialog();
                db.comboFill(txt_material_nm, "select * from tbl_drinkgroup ", "tbl_drinkgroup", "grpName", "grpName");

            }
         
        }

        private void chk_drink_CheckedChanged(object sender, EventArgs e)
        {
            db.cnopen();
            if (flag)
            {
                if (chk_drink.Checked)
                {
                    // db.comboFill(txt_material_nm, "select * from menu where category like '%" + "DRINKS" + "%'", "menu", "m_name", "m_name");
                    db.comboFill(txt_material_nm, "select * from tbl_drinkgroup ", "tbl_drinkgroup", "grpName", "grpName");

                    drinksStatus = "drinks";
                    txt_qtyML.Enabled = true;
                }
                else
                {
                    //   db.comboFill(txt_material_nm, "select distinct item_name from tbl_material_add", "tbl_material_add", "item_name", "item_name");
                    db.comboFill(txt_material_nm, "select distinct material_nm from material_nm", "material_nm", "material_nm", "material_nm");

                    drinksStatus = "-";
                    txt_qtyML.Enabled = false;
                }
            }
            db.cnclose();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void txt_paid_TextChanged(object sender, EventArgs e)
        {
            if (txt_paid.Text != "")
            {


                float amt = float.Parse(label_grandTotal.Text) - float.Parse(txt_paid.Text);
                txt_balance.Text = Math.Round(amt).ToString();
            }
            else
            {
                txt_paid.Text = "0";
            }

            int tot = Convert.ToInt32(label_grandTotal.Text.ToString());
            int pd = Convert.ToInt32(txt_paid.Text.ToString());
            if (pd > tot)
            {
                MessageBox.Show("Paid Amount Should be Less than Total Amount");
                btn_save.Enabled = false;
            }
            else
            {
                btn_save.Enabled = true;
            }
          
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
            if (textBox_rate.Text != "")
            {
                txt_amount.Text = (float.Parse(txt_Qty.Text.ToString()) * float.Parse(textBox_rate.Text.ToString())).ToString();
            }
            //else

               
        }

        private void textBox_Discount_TextChanged(object sender, EventArgs e)
        {
            if (float.Parse(textBox_Discount.Text) > 100)
                MessageBox.Show("You can't give too much Discount!!","Confirm",MessageBoxButtons.OK,MessageBoxIcon.Information);
            else
            TaxDiscount_cal();
        }

        void TaxDiscount_cal()
        {
            if (textBoxTax.Text != "" && textBox_Discount.Text != "" && txt_amt.Text != "")
            {
                 amount = float.Parse(txt_amt.Text.ToString());
                 tax=float.Parse(textBoxTax.Text.ToString()) * float.Parse(txt_amt.Text.ToString()) / 100;
                discount=float.Parse(textBox_Discount.Text.ToString()) * float.Parse(txt_amt.Text.ToString()) / 100;


                label_grandTotal.Text =(amount+tax-discount).ToString();
               label_grandTotal.Text=  Math.Round(Convert.ToDecimal(amount + tax - discount)).ToString();
               txt_balance.Text = label_grandTotal.Text;
            }
        }

        private void textBoxTax_TextChanged(object sender, EventArgs e)
        {
            TaxDiscount_cal();
        }

        

        private void textBox_rate_Leave(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Supplier_Add vendor = new Supplier_Add();
            vendor.ShowDialog();
            db.comboFill(cmb_v_name, "select  distinct sup_name from supplier_dtls", "supplier_dtls", "sup_name", "sup_name");

        }

        private void label_grandTotal_Click(object sender, EventArgs e)
        {
           // txt_balance.Text = label_grandTotal.Text;
        }

        private void txt_material_nm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txt_material_nm.Text != "System.Data.DataRowView" && txt_material_nm.Text != "")
            {
               // db.comboFill(cmb_unit, "select * from material_nm where material_nm='" + txt_material_nm.Text + "' ", "material_nm", "unit", "unit");
                if (chk_drink.Checked)
                {
                    if (db.ChkDb_Value("select qtyInML from tbl_drinkgroup where  grpName='" + txt_material_nm.Text + "'"))
                        txt_qtyML.Text = db.getDb_Value("select qtyInML from tbl_drinkgroup where  grpName='" + txt_material_nm.Text + "'").ToString();
                    else
                        txt_qtyML.Text = "0";
                }
            }
        }

        private void txt_Qty_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);
           
        }

        private void textBox_rate_TextChanged(object sender, EventArgs e)
        {
            rateQtyCal();
        }
        void rateQtyCal()
        {
            if (textBox_rate.Text != "" && txt_Qty.Text!="")
            {

                txt_amount.Text = (float.Parse(txt_Qty.Text) * float.Parse(textBox_rate.Text)).ToString();
            }
        }

        private void textBoxTax_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);

        }

        private void textBox_Discount_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);

        }

        private void textBox_rate_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);

        }

        private void txt_paid_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);

        }

        private void txt_material_nm_Leave(object sender, EventArgs e)
        {
            //if (!db.ChkDb_Value("select * from material_nm where material_nm='" + txt_material_nm.Text + "'"))
            //{
            //   if(MessageBox.Show("Do You want to Add this Material?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes)
            //   {
            //       Material add=new Material();
            //       add.ShowDialog();
            //   }
            //}

        }

        private void txt_Qty_Leave(object sender, EventArgs e)
        {
            //if (txt_Qty.Text.Contains('.'))
            //{
            //    txt_Qty.Text = "0" + (txt_Qty.Text);
            //    txt_amount.Text = (float.Parse(txt_Qty.Text.ToString()) * float.Parse(textBox_rate.Text.ToString())).ToString();

            //}
        }

        private void txt_paid_Leave(object sender, EventArgs e)
        {
            
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            if (!chk_drink.Checked)
            {
                Material am = new Material();
                am.ShowDialog();
                db.comboFill(txt_material_nm, "select distinct material_nm from material_nm", "material_nm", "material_nm", "material_nm");


            }
            else
            {
                Add_Drink_Group grp = new Add_Drink_Group();
                grp.ShowDialog();
                db.comboFill(txt_material_nm, "select * from tbl_drinkgroup ", "tbl_drinkgroup", "grpName", "grpName");

            }
        }

        private void cmb_v_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbflg)
                invoiceChk();
        }
        void invoiceChk()
        {
            if (cmb_v_name.Text != "--Select--" || cmb_v_name.Text != "")
            {
                string v_id = db.getDb_Value("select sup_id from supplier_dtls where sup_name='" + cmb_v_name.Text + "'").ToString();

                if (db.ChkDb_Value("select * from tbl_purchasemaster where sup_id='" + v_id + "' and invoice_number='" + txt_invoiceno.Text + "'"))
                {
                    MessageBox.Show("Invoice Number Already Exists!!!", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txt_invoiceno.Focus();
                }
            }
        }

        private void txt_invoiceno_TextChanged(object sender, EventArgs e)
        {
            if (cmbflg)
                invoiceChk();
        }
      
   
     

        
    }
}
