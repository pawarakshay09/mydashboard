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
    public partial class ConfigurationSetting : Form
    {
        Database db = new Database();
        string printFormation = "", discount_status = "", vat_status = "", process = "", KOT_printStatus, notification, printerName = "", parcelStatus = "", SC_status = "", Customer = "", custPrint = "", waitercumpl = "", printWaiterDetails = "",gst;
        string header = "";
        string vat="",Printbill="",nopfprints,afterprint="",printtoday="";
        public ConfigurationSetting()
        {
            InitializeComponent();
        }

        public void processType()
        {
            // ************** chk order type ****************
            if (rdb_mouse.Checked)
                process = "By Mouse Click";

            else
                process = "By Item Code";
            
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            processType();
            // **************** chk print format ********************
            //if (rdb_simplePrint.Checked)
            //    printFormation = "Simple";
            //else
            //    printFormation = "Composite";






            if (Cbsimple.Checked)
            {
                printFormation = "Simple";
            }
            else if(cbcombine.Checked)
            {
                printFormation = "Composite";
            }
            else
            {
                printFormation = "CombineAll";
            }


            if (cbsecond.Checked)
            {
                header = "Yes";
            }
            else

                header = "No";
             
            // ************** chk order type ****************
            if (cbgst.Checked)
            {
                gst = "Yes";
            }
            else
            {
                gst = "No";
                txtgst.Text = "0";
            }
            if (checkBox3.Checked)
            {
                SC_status = "Yes";
            }
            else
            {
                SC_status = "No";
                txtSC.Text = "0";
            }
            if (checkBox2.Checked)
            {
                txt_tax.Enabled = true;
                vat_status = "Yes";

                vat = "Yes";
            }
            else
            {
                vat_status = "No";
                txt_tax.Text = "0";
                vat = "No";
                txt_tax.Enabled = false;
            }
            if (checkBox1.Checked)
            {
                txt_disc.Enabled = true;
                discount_status = "Yes";
                //vat_status = "Yes";
            }
            else
            {
                discount_status = "No";
                txt_disc.Text="0";

                txt_disc.Enabled = false;
            }

            if (chkParcelDetails.Checked)
                parcelStatus = "Yes";
            else
                parcelStatus = "No";

             DialogResult dlg = MessageBox.Show("Do you want to save Record?","Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (dlg == DialogResult.Yes)
            {
                //********** Update Tax and Discount ***************select status from tbl_option where grp='" + "Parcel Details" + "
                // db.update("update tbl_option set grp='Discount',status='" + discount_status + "',value='" + txt_disc.Text + "',process_type='" + process + "'  where grp='" + "Discount" + "'");
                //db.update("update tbl_option set grp='" + "Tax" + "',status='" + vat_status + "',value='" + txt_tax.Text + "',process_type='" + process + "'  where grp='" + "Tax" + "'");
                if (db.ChkDb_Value("select * from tbl_option where grp='DiscountReason'"))
                {
                    db.update("update tbl_option set status='" + discount_status + "',value='" + txtdiscount.Text + "'  where grp='" + "DiscountReason" + "'");

                }
                //else
                //    db.insert("insert into tbl_option values('DiscountReason','" + discount_status + "','" + txt_disc.Text + "','" + process + "')");

                if (db.ChkDb_Value("select * from tbl_option where grp='Vatvalue'"))
                {
                    db.update("update tbl_option set grp='" + "Vatvalue" + "',status='" + vat + "',value='" + txt_tax.Text + "',process_type='" + process + "'  where grp='" + "Vatvalue" + "'");

                }
                else
                   db.insert("insert into tbl_option values('Tax','" + vat + "','" + txt_tax.Text + "','" + process + "')");

                if (db.ChkDb_Value("select * from tbl_option where grp='Parcel Details'"))
                    db.update("update tbl_option set grp='" + "Parcel Details" + "',status='" + parcelStatus + "',value='0',process_type='" + process + "'  where grp='" + "Parcel Details" + "'");
                else
                    db.insert("insert into tbl_option values('Parcel Details','" + parcelStatus + "','0','" + process + "')");


                if (db.ChkDb_Value("select * from tbl_option where grp='Tax'"))
                {
                    db.update("update tbl_option set grp='Tax',status='" + gst + "',value='" + txtgst.Text + "',process_type='" + process + "'  where grp='" + "Tax" + "'");

                }
                if (db.ChkDb_Value("select * from tbl_option where grp='Discountpercentage'"))
                {
                    db.update("update tbl_option set status='" + discount_status + "',value='" + txt_disc.Text + "'  where grp='" + "Discountpercentage" + "'");

                }

                if (db.ChkDb_Value("select * from tbl_option where grp='FoodDiscount'"))
                {
                   if (checkBox7.Checked)
                    db.update("update tbl_option set status='Yes',value='" + txt_disc.Text + "'  where grp='" + "FoodDiscount" + "'");
                    else
                    db.update("update tbl_option set status='No',value='0'  where grp='" + "FoodDiscount" + "'");


                }
                if (db.ChkDb_Value("select * from tbl_option where grp='BeveragesDiscount'"))
                {
                    if (checkBox9.Checked)
                        db.update("update tbl_option set status='Yes',value='" + txt_disc.Text + "'  where grp='" + "BeveragesDiscount" + "'");
                    else
                        db.update("update tbl_option set status='No',value='0'  where grp='" + "BeveragesDiscount" + "'");


                }
                if (db.ChkDb_Value("select * from tbl_option where grp='LiquorDiscount'"))
                {
                    if (checkBox8.Checked)
                        db.update("update tbl_option set status='Yes',value='" + txt_disc.Text + "'  where grp='" + "LiquorDiscount" + "'");
                    else
                        db.update("update tbl_option set status='No',value='0'  where grp='" + "LiquorDiscount" + "'");


                }

                //if (db.ChkDb_Value("select * from tbl_option where grp='DiscountReason'"))
                //{


                //    if (chkDiscount.Checked)
                //    {

                //        db.update("update tbl_option set status='Yes',value='" + txtdiscount.Text + "'  where   grp='DiscountReason'");
                //    }
                //    else
                //    {

                //        db.update("update tbl_option set status='No',value='" + txtdiscount.Text + "'  where   grp='DiscountReason'");

                //    }


                //}



            }

            //**BY YOGESH ----For Service Charge ,SC panel----////
            if (db.ChkDb_Value("select * from tbl_option where grp='ServiceTaxvalue'"))
            {
                db.update("update tbl_option set grp='ServiceTaxvalue', status='" + SC_status + "',value='" + txtSC.Text + "',process_type='" + process + "' where grp='" + "ServiceTaxvalue" + "'");
                //db.update("ALTER TABLE [dbo].[table_order] ADD  CONSTRAINT [table_order_serviceTaxVal]  DEFAULT (('" + txtSC.Text + "')) FOR [serviceTaxVal]");

                //db.update("ALTER TABLE [dbo].[table_order] ADD  CONSTRAINT [table_order_serviceTaxfood]  DEFAULT (('" + txtSC.Text + "')) FOR [serviceTaxFoodVal]");
                //db.update("ALTER TABLE [dbo].[table_order] ADD  CONSTRAINT [table_order_serviceTaxliq]  DEFAULT (('" + txtSC.Text + "')) FOR [serviceTaxLiquorVal]");


            }

             //************ Update print status **********

                   db.update("update tbl_option set grp='Print formation',status='Yes',value='" + printFormation + "',process_type='" + process + "' where grp='" + "Print formation" + "'");


                   db.update("update tbl_option set grp='SecondHeader',status='" + header + "' where grp='" + "SecondHeader" + "'");

                   MessageBox.Show("Setting Updated Succesfully");




        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txt_disc.Enabled = true;
                discount_status = "Yes";
                panelreason.Visible = true;
              //  vat_status = "Yes";
                String status = db.getDbstatus_Value("Select status from tbl_option where grp='DiscountReason'");
                String value = db.getDbstatus_Value("Select value from tbl_option where grp='DiscountReason'");
                if (status.Equals("Yes"))
                {

                    chkDiscount.Checked = true;
                    txtdiscount.Text = value;

                }
            }
            else
            {
                discount_status = "No";
              //  vat_status = "No";
               
                txt_disc.Enabled = false;
                txt_disc.Text = "0";
                panelreason.Visible = false ;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
       {
            
           if (checkBox2.Checked)
           {
               txt_tax.Enabled = true;
              // vat_status = "Yes";
               vat = "Yes";
           }
           else
           {
              // vat_status = "No";
               vat="No";
               txt_tax.Enabled = false;
           }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            processType();
            //*************** chk the kot opetion is click or not ****
            if (checkBoxPrintKOT.Checked)
                KOT_printStatus = "Yes";
            else
                KOT_printStatus = "No";


            if(cbprintbill.Checked)
            {
                Printbill = "Yes";
            }
            else
            {
                Printbill = "No";
            }



            // **************** chk print format ********************
            //if (rdb_simplePrint.Checked)
            //    printFormation = "Simple";
            //else
            //    printFormation = "Composite";

            // **************** chk printer Name ********************
            if (rdbcutPrint.Checked)
                printerName = "Cut Print";
            else
                printerName = "Continuous Print";
             
                   if (rdb_singleKOT.Checked)
                       db.update("update tbl_option set status='" + KOT_printStatus + "',value='" + txt_kot.Text + "',process_type='" + process + "' where grp='KotPrint'");

                   //********** Update Printer for KOT and Bill ***************
                   //db.update("update tbl_option set grp='KOT Printer_Snacks',status='Yes',value='" + txtKot1.Text + "',process_type='" + process + "' where grp='" + "Kot Printer" + "'");
                   //db.update("update tbl_option set grp='KOT Printer_Drink',status='Yes',value='" + txtKot2.Text + "',process_type='" + process + "' where grp='" + "Kot Printer" + "'");
                   //db.update("update tbl_option set grp='KOT Printer_Other',status='Yes',value='" + txtKot3.Text + "',process_type='" + process + "' where grp='" + "Kot Printer" + "'");

                   if (rdb_doubleKOT.Checked)
                   {
                       //********** Update Printer for KOT Snacks ,Other and Drink ***************
                       if (db.ChkDb_Value("select * from tbl_option where grp='KOT Printer_Snacks'"))
                           db.update("update tbl_option set grp='KOT Printer_Snacks',status='Yes',value='" + txtKot1.Text + "',process_type='" + process + "' where grp='" + "KOT Printer_Snacks" + "'");
                       else
                           db.insert("insert into tbl_option values('KOT Printer_Snacks','Yes','" + txtKot1.Text + "','" + process + "')");

                       if (db.ChkDb_Value("select * from tbl_option where grp='KOT Printer_Other'"))
                           db.update("update tbl_option set grp='KOT Printer_Other',status='Yes',value='" + txtKot3.Text + "',process_type='" + process + "' where grp='" + "KOT Printer_Other" + "'");
                       else
                           db.insert("insert into tbl_option values('KOT Printer_Other','Yes','" + txtKot3.Text + "','" + process + "')");

                       if (db.ChkDb_Value("select * from tbl_option where grp='KOT Printer_Drink'"))
                           db.update("update tbl_option set grp='KOT Printer_Drink',status='Yes',value='" + txtKot2.Text + "',process_type='" + process + "' where grp='" + "KOT Printer_Drink" + "'");
                       else
                           db.insert("insert into tbl_option values('KOT Printer_Drink','Yes','" + txtKot2.Text + "','" + process + "')");

                   }

                   //****************print No of times***********************
                   if (chkPrintNo.Checked)                  
                       db.update("update tbl_option set grp='BillPrintNo',status='Yes',value='"+txtNoTime.Text+"',process_type='" + process + "' where grp='" + "BillPrintNo" + "'");                  
                   else
                       db.update("update tbl_option set grp='BillPrintNo',status='No',value='1',process_type='" + process + "' where grp='" + "BillPrintNo" + "'");


                   //************ Update printer Name **********
                   if (db.ChkDb_Value("select * from tbl_option where grp='PrinterName'"))
                       db.update("update tbl_option set grp='PrinterName',status='Yes',value='" + printerName + "',process_type='" + process + "' where grp='" + "PrinterName" + "'");
                   else
                       db.insert("insert into tbl_option values('PrinterName','Yes','" + printerName + "','" + process + "')");


                   db.update("update tbl_option set grp='Bill Printer',status='Yes',value='" + txt_bill.Text + "',process_type='" + process + "' where grp='" + "Bill Printer" + "'");
                   //************ Update print status **********

                  // db.update("update tbl_option set grp='Print formation',status='Yes',value='" + printFormation + "',process_type='" + process + "' where grp='" + "Print formation" + "'");

                   DialogResult dlg = MessageBox.Show("Do you want to save Record?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                   if (dlg == DialogResult.Yes)
                   {
                   if (db.ChkDb_Value("select * from tbl_option where grp='Printcompulsary'"))
                   {

                       db.update("update tbl_option set grp='Printcompulsary',status='" + Printbill + "' ,process_type='" + process + "' where grp='" + "Printcompulsary" + "'");

                   }

                   if (db.ChkDb_Value("select * from tbl_option where grp='BillPrintNo'"))
                   {

                       db.update("update tbl_option set grp='BillPrintNo', value='"+txtnop.Text+"',process_type='" + process + "' where grp='" + "BillPrintNo" + "'");

                   }

                   if (db.ChkDb_Value("select * from tbl_option where grp='DiscountReason'"))
                   {


                       if (chkDiscount.Checked)
                       {

                           db.update("update tbl_option set status='Yes',value='" + txtdiscount.Text + "'  where   grp='DiscountReason'");
                       }
                       else
                       {

                           db.update("update tbl_option set status='No',value='" + txtdiscount.Text + "'  where   grp='DiscountReason'");

                       }


                   }
                   }

               
              /// }
        }

        private void rdb_singleKOT_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_singleKOT.Checked)
            {
                txtKot1.Enabled = false;
                txtKot2.Enabled = false;
                txtKot3.Enabled = false;
                panelSingleKOT.Visible = true;
                panelMultipleKOT.Visible = false;

                string Kot_Printer = db.getDbstatus_Value("select value from tbl_option where grp='" + "KotPrint" + "'");
                txt_kot.Text = Kot_Printer;
            }
            else
            {
                txtKot1.Enabled = true;
                txtKot2.Enabled = true;
                txtKot3.Enabled = true;
                panelSingleKOT.Visible = false;
                panelMultipleKOT.Visible = true;


                string Kotsnacks = db.getDbstatus_Value("select value from tbl_option where grp='" + "KOT Printer_Snacks" + "'");
                string KotOther = db.getDbstatus_Value("select value from tbl_option where grp='" + "KOT Printer_Other" + "'");
                string KotDrink = db.getDbstatus_Value("select value from tbl_option where grp='" + "KOT Printer_Drink" + "'");

                txtKot1.Text = Kotsnacks;
                txtKot3.Text = KotOther;
                txtKot2.Text = KotDrink;

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            processType();

               DialogResult dlg = MessageBox.Show("Do you want to save Record?","Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
               if (dlg == DialogResult.Yes)
               {
                   //************* Update Order status **********

                   if (chk_counter.Checked)
                   {
                       db.update("update tbl_option set grp='Counter Order',status='Yes',value='0',process_type='" + process + "' where grp='" + "Counter Order" + "'");
                   }
                   else
                       db.update("update tbl_option set grp='Counter Order',status='No',value='0',process_type='" + process + "' where grp='" + "Counter Order" + "'");

                   if (chk_parcel.Checked)
                   {
                       db.update("update tbl_option set grp='Parcel Order',status='Yes',value='0',process_type='" + process + "' where grp='" + "Parcel Order" + "'");
                   }
                   else
                       db.update("update tbl_option set grp='Parcel Order',status='No',value='0',process_type='" + process + "' where grp='" + "Parcel Order" + "'");

                   if (chk_cancelOrder.Checked)
                   {
                       db.update("update tbl_option set grp='Cancel Order',status='Yes',value='0',process_type='" + process + "' where grp='" + "Cancel Order" + "'");
                   }
                   else
                       db.update("update tbl_option set grp='Cancel Order',status='No',value='0',process_type='" + process + "' where grp='" + "Cancel Order" + "'");

                   if (chk_kotOrder.Checked)
                       db.update("update tbl_option set grp='Kot Print Button',status='Yes',value='0',process_type='" + process + "' where grp='" + "Kot Print Button" + "'");
                   else
                       db.update("update tbl_option set grp='Kot Print Button',status='No',value='0',process_type='" + process + "' where grp='" + "Kot Print Button" + "'");

                   if (chk_pay.Checked)
                       db.update("update tbl_option set grp='Pay Button',status='Yes',value='0',process_type='" + process + "' where grp='Pay Button'");
                   else
                       db.update("update tbl_option set grp='Pay Button',status='No',value='0',process_type='" + process + "' where grp='Pay Button'");
               }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            processType();
            // ************ chk notification status ******************
            if (checkBox6.Checked)
            {
                afterprint = "Yes";
            }
            else
            {
                afterprint = "No";
            }

            if (printtodays.Checked)
            {
                printtoday = "Yes";
            }
            else
            {
                printtoday = "No";
            }




            if (chk_notification.Checked)
                notification = "Yes";
            else
                notification = "No";
               DialogResult dlg = MessageBox.Show("Do you want to save Record?","Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
               if (dlg == DialogResult.Yes)
               {

                   //************* Update notification status **********
                   db.update("update tbl_option set grp='Notification',status='" + notification + "',value='" + notification + "',process_type='" + process + "' where grp='" + "Notification" + "'");

                   //  string Changedate=dtp_date.Value.ToString("MM-dd-yyyy");
                   db.update("update tbl_dayend_status set ddate='" + dtp_date.Text + "'");

                   //********** Update Report Backup Location ***************
                   db.update("update tbl_option set grp='Report Location',status='Yes',value='" + txt_locaion.Text + "',process_type='" + process + "' where grp='" + "Report Location" + "'");

                   db.update("update tbl_option set status='" + afterprint + "' where grp='" + "AfterPrint" + "'");

                   db.update("update tbl_option set status='" + printtoday + "' where grp='" + "printtoday" + "'");

               
               
               }
        }

        private void rdb_foodDrinkReceipt_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void printersetting_Click(object sender, EventArgs e)
        {
           
        }

        private void ConfigurationSetting_Load(object sender, EventArgs e)
        {

            //**yogesh  ///For Customer Details

            
          

            string noprints = db.getDbstatus_Value("select value from tbl_option where grp='" + "BillPrintNo" + "'");
            txtnop.Text = noprints;


            string printtoday = db.getDbstatus_Value("select status from tbl_option where grp='" + "printtoday" + "'");
            if (printtoday=="Yes")
            {
                printtodays.Checked = true;
            }
            else
            {
                printtodays.Checked = false;
            }
          


            string afprint = db.getDbstatus_Value("select status from tbl_option where grp='" + "AfterPrint" + "'");

            if (afprint=="Yes")
            {
                checkBox6.Checked = true;
            }
            else
            {
                checkBox6.Checked = false;
            }
            string sc = db.getDbstatus_Value("select status from tbl_option where grp='" + "ServiceTaxvalue" + "'");
            if (sc=="Yes")
            {
                checkBox3.Checked = true;

            }
            else
            {
                checkBox3.Checked = false;
            }
            string scamt = db.getDbstatus_Value("select value from tbl_option where grp='" + "ServiceTaxvalue" + "'");
            txtSC.Text = scamt;
         
          
          
            string printbiill = db.getDbstatus_Value("select status from tbl_option where grp='" + "Printcompulsary" + "'");
            if (printbiill=="Yes")
            {
                cbprintbill.Checked = true;
            }
            else
            {
                cbprintbill.Checked = false;
            }

            string Customer = db.getDbstatus_Value("select status from tbl_option where grp='" + "CustomerDetails" + "'");
            if (Customer == "Yes")
            {
                checkBox4.Checked = true;
            }
            else
            {
                checkBox4.Checked = false;
            }
            string custprint = db.getDbstatus_Value("select value from tbl_option where grp='" + "CustomerDetails" + "'");
            if (custprint == "Yes")
            {
                cbcustprint.Checked = true;
            }
            else
            {
                cbcustprint.Checked = false;
            }
            string waitercmpl = db.getDbstatus_Value("select status from tbl_option where grp='" + "waiterDetails" + "'");
            if (waitercmpl == "Yes")
            {
                cmwaitercpl.Checked = true;
            }
            else
            {
                cmwaitercpl.Checked = false;
            }
            string printWaiterD = db.getDbstatus_Value("select value from tbl_option where grp='" + "waiterDetails" + "'");
            if (printWaiterD == "Yes")
            {
                printWaiter.Checked = true;
            }
            else
            {
                printWaiter.Checked = false;
            }




            //get the pervious vales from the db and assign it
            float disc_value = db.getDb_Value("select value from tbl_option where grp='" + "Discount" + "'");
            txt_disc.Text = disc_value.ToString();
            float tax_value = db.getDb_Value("select value from tbl_option where grp='" + "Vatvalue" + "'");


            txt_tax.Text = tax_value.ToString();
            string  vats = db.getDbstatus_Value("select status from tbl_option where grp='" + "Vatvalue" + "'");




            txtNoTime.Text = db.getDb_Value("select value from tbl_option where grp='" + "BillPrintNo" + "' ").ToString();

            string disc_status = db.getDbstatus_Value("select status from tbl_option where grp='" + "Discount" + "'");
            string tax_status = db.getDbstatus_Value("select status from tbl_option where grp='" + "Tax" + "'");
            float gst = db.getDb_Value("select value from tbl_option where grp='" + "Tax" + "'");
            txtgst.Text = gst.ToString();



            // Set Existing printer values
            string bill_Printer = db.getDbstatus_Value("select value from tbl_option where grp='" + "Bill Printer" + "'");



            //*For SC
            string SC = db.getDbstatus_Value(" select status from tbl_option where grp='" + "ServiceTaxvalue" + "'");
            float txtsc = db.getDb_Value("select value from tbl_option where grp='" + "ServiceTaxvalue" + "'");
            txtSC.Text = txtsc.ToString();


            txt_bill.Text = bill_Printer;


            // Set Existing printer values for snacks,other,Drink
            if (rdb_singleKOT.Checked)
            {
                string Kot_Printer = db.getDbstatus_Value("select value from tbl_option where grp='" + "KOT Printer" + "'");
                txt_kot.Text = Kot_Printer;
            }
            else
            {
                string Kotsnacks = db.getDbstatus_Value("select value from tbl_option where grp='" + "KOT Printer_Snacks" + "'");
                string KotOther = db.getDbstatus_Value("select value from tbl_option where grp='" + "KOT Printer_Other" + "'");
                string KotDrink = db.getDbstatus_Value("select value from tbl_option where grp='" + "KOT Printer_Drink" + "'");

                txtKot1.Text = Kotsnacks;
                txtKot3.Text = KotOther;
                txtKot2.Text = KotDrink;
            }

            //get the current order number from the table_order
            textBoxCurrentOrder.Text = db.getDb_Value("select max(order_id) from table_order").ToString();
            // ********** Display Existing Discount staus ****************

            if (disc_status == "Yes")
            {
                checkBox1.Checked = true;
            }
            else
            {
                checkBox1.Checked = false;
            }
            if (chkLogo.Checked)
                panelImagePath.Visible = true;
            else
                panelImagePath.Visible = false;

            // ********** Display Existing Order staus ****************

            string pro_type = db.getDbstatus_Value("select process_type from tbl_option ");//where process_type='" + "By Item Code" + "'");
            string pro_type_1 = db.getDbstatus_Value("select process_type from tbl_option");// where process_type='" + "By Mouce Click" + "'");

            if (vats=="Yes")
            {
                checkBox2.Checked = true;
            }
            else
            {
                checkBox2.Checked = false;
            }

            if (pro_type == "By Item Code")
            {
                rdb_code.Checked = true;
                rdb_mouse.Checked = false;
            }
            else
            {
                rdb_mouse.Checked = true;
                rdb_code.Checked = false;
            }
            // **************** Display Existing Tax Status **************
            if (tax_status == "Yes")
            {
                cbgst.Checked = true;
            }
            else
            {
                cbgst.Checked = false;
            }
            // ************ Display Existing KOT status *****************
            if (db.getDbstatus_Value("select status from tbl_option where grp='" + "KotPrint" + "'").ToString() == "Yes")
                checkBoxPrintKOT.Checked = true;
            else
                checkBoxPrintKOT.Checked = false;

            // ************ Display Existing KOT Printer status *****************
            if (db.getDbstatus_Value("select status from tbl_option where grp='" + "KOT Printer" + "'").ToString() == "Yes")
                checkBox_KotPrinter.Checked = true;
            else
                checkBox_KotPrinter.Checked = false;
            // ************ Display Existing  Printer No of times *****************
            if (db.getDbstatus_Value("select status from tbl_option where grp='" + "BillPrintNo" + "'").ToString() == "Yes")
                chkPrintNo.Checked = true;
            else
                chkPrintNo.Checked = false;

            if (chkPrintNo.Checked)
                txtNoTime.Enabled = true;
            else
                txtNoTime.Enabled = false;


            this.CancelButton = btnClose;

            // ********** Display Existing File Location of report backup ****************

            string fileLocation = db.getDbstatus_Value("select value from tbl_option where grp='" + "Report Location" + "'");
            txt_locaion.Text = fileLocation;


            // ********** Display Existing print formation of bill ****************
            string printype = db.getDbstatus_Value("select value from tbl_option where grp='" + "Print formation" + "'").ToString();

            //if (printype == "Simple")
            //{
            //    rdb_simplePrint.Checked = true;
            //    rdb_compositePrint.Checked = false;
            //}
            //else
            //{
            //    rdb_simplePrint.Checked = false;
            //    rdb_compositePrint.Checked = true;
            //}


            if (printype == "Simple")
            {
                Cbsimple.Checked = true;

            }
            else if (printype == "Composite")
            {
                cbcombine.Checked = true;
            }
            else
            {
                cbseperate.Checked = true;
            }

            string printheader = db.getDbstatus_Value("select status from tbl_option where grp='" + "SecondHeader" + "'").ToString();
            if (printheader == "Yes")
            {
                cbsecond.Checked = true;

            }
            else
            {
                cbsecond.Checked = false;
            }




            // ********** Display Printer Name ****************
            string printerName = db.getDbstatus_Value("select value from tbl_option where grp='" + "PrinterName" + "'").ToString();

            if (printerName == "Cut Print")
            {
                rdbcutPrint.Checked = true;
                rdbContinuousPrint.Checked = false;
            }
            else
            {
                rdbcutPrint.Checked = false;
                rdbContinuousPrint.Checked = true;
            }





            //*********** Display notification status **********************
            if (db.getDbstatus_Value("select status from tbl_option where grp='" + "Notification" + "'").ToString() == "Yes")
                chk_notification.Checked = true;
            else
                chk_notification.Checked = false;

            // ********** Display ordering status ****************

            if (db.getDbstatus_Value("select status from tbl_option where grp='" + "Counter Order" + "'").ToString() == "Yes")
                chk_counter.Checked = true;
            else
                chk_counter.Checked = false;

            if (db.getDbstatus_Value("select status from tbl_option where grp='" + "Parcel Order" + "'").ToString() == "Yes")
                chk_parcel.Checked = true;
            else
                chk_parcel.Checked = false;

            if (db.getDbstatus_Value("select status from tbl_option where grp='" + "Cancel Order" + "'").ToString() == "Yes")
                chk_cancelOrder.Checked = true;
            else
                chk_cancelOrder.Checked = false;

            if (db.getDbstatus_Value("select status from tbl_option where grp='" + "Kot Print Button" + "'").ToString() == "Yes")
                chk_kotOrder.Checked = true;
            else
                chk_kotOrder.Checked = false;


            if (db.getDbstatus_Value("select status from tbl_option where grp='" + "LogoPrint" + "'").ToString() == "Yes")
                chkLogo.Checked = true;
            else
                chkLogo.Checked = false;


            // ************ Display Existing Parcel Setting *****************
            if (db.getDbstatus_Value("select status from tbl_option where grp='" + "Parcel Details" + "'").ToString() == "Yes")
                chkParcelDetails.Checked = true;
            else
                chkParcelDetails.Checked = false;
            //****Pay Button******************************
            if (db.getDbstatus_Value("select status from tbl_option where grp='" + "Pay Button" + "'").ToString() == "Yes")
                chk_pay.Checked = true;
            else
                chk_pay.Checked = false;

            // ************ Display Existing Excel report status *****************
            if (db.getDbstatus_Value("select status from tbl_option where grp='" + "ExcelReport" + "'").ToString() == "Yes")
                chkExcel.Checked = true;
            else
                chkExcel.Checked = false;

            // ************ Display Existing Pdf report status *****************
            if (db.getDbstatus_Value("select status from tbl_option where grp='" + "Pdf Report" + "'").ToString() == "Yes")
                chkPdf.Checked = true;
            else
                chkPdf.Checked = false;
            // ************ Display Existing Excel Export reports status *****************

            if (db.getDbstatus_Value("select status from tbl_option where grp='" + "TodaysSalesReport" + "'").ToString() == "Yes")
                ChkTodySales.Checked = true;
            else
                ChkTodySales.Checked = false;

            if (db.getDbstatus_Value("select status from tbl_option where grp='" + "ExpencesReport" + "'").ToString() == "Yes")
                chkExpence.Checked = true;
            else
                chkExpence.Checked = false;

            if (db.getDbstatus_Value("select status from tbl_option where grp='" + "ItemWiseSalesReport" + "'").ToString() == "Yes")
                chkItemwise.Checked = true;
            else
                chkItemwise.Checked = false;

            if (db.getDbstatus_Value("select status from tbl_option where grp='" + "StockReport" + "'").ToString() == "Yes")
                chkStock.Checked = true;
            else
                chkStock.Checked = false;

            if (db.getDbstatus_Value("select status from tbl_option where grp='" + "ParcelOrderReport" + "'").ToString() == "Yes")
                chkParcelOrd.Checked = true;
            else
                chkParcelOrd.Checked = false;

            if (db.getDbstatus_Value("select status from tbl_option where grp='" + "CancelKOTReport" + "'").ToString() == "Yes")
                chkKotRpt.Checked = true;
            else
                chkKotRpt.Checked = false;

            if (db.getDbstatus_Value("select status from tbl_option where grp='" + "DeletedItemReport" + "'").ToString() == "Yes")
                chkDelRpt.Checked = true;
            else
                chkDelRpt.Checked = false;

            if (db.getDbstatus_Value("select status from tbl_option where grp='" + "TableSalesReport" + "'").ToString() == "Yes")
                chkTblSale.Checked = true;
            else
                chkTblSale.Checked = false;

            if (db.getDbstatus_Value("select status from tbl_option where grp='" + "SalesReport" + "'").ToString() == "Yes")
                chksaleRpt.Checked = true;
            else
                chksaleRpt.Checked = false;



            // here get logo name

            if (db.ChkDb_Value("select value from tbl_option where grp='LogoPrint'"))
            {
                txtImagePath.Text = db.getDbstatus_Value("select value from tbl_option where grp='LogoPrint'");
            }



            string disc = db.getDbstatus_Value("select status from tbl_option where grp='" + "DiscountReason" + "' ");
            string offer = db.getDbstatus_Value("select value from tbl_option where grp='" + "DiscountReason" + "' ");

            if (disc == "Yes")
            {
                checkBox1.Checked = true;
                txtdiscount.Text = offer.ToString();
            }
            else
            {
                checkBox1.Checked = false;
            }
            

            db.formFix(this);
        }

        private void buttonSetOrderNO_Click(object sender, EventArgs e)
        {
            if (textBoxUpdateOrderNo.Text != "" && textBoxUpdateOrderNo.Text != "0" && float.Parse(textBoxUpdateOrderNo.Text) > float.Parse(textBoxCurrentOrder.Text))
            {

                DialogResult dlg = MessageBox.Show("Do you want to Set the new order number?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlg == DialogResult.Yes)
                {

                    db.insert("insert into table_order (order_id,t_id,timeing,w_id,order_type) values ('" + textBoxUpdateOrderNo.Text + "','0','01-01-2014','0','Novat')");
                    MessageBox.Show(" Order Number Set Sucessfully");
                }
            }
            else
            {
                MessageBox.Show("Invalid Order Number please cheack Order Number");
            }
        }

        private void chkLogo_CheckedChanged(object sender, EventArgs e)
        {            

            if (chkLogo.Checked)
                panelImagePath.Visible = true;
            else
                panelImagePath.Visible = false;
        }

        private void receiptsetting_Click(object sender, EventArgs e)
        {          
           
        }

        private void chk_date_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_date.Checked)
                dtp_date.Enabled = true;
            else
                dtp_date.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            processType();
              DialogResult dlg = MessageBox.Show("Do you want to save Record?","Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
              if (dlg == DialogResult.Yes)
              {
                  if (chkExcel.Checked)
                      db.update("update tbl_option set grp='ExcelReport',status='Yes',value='0',process_type='" + process + "' where grp='" + "ExcelReport" + "'");
                  else
                      db.update("update tbl_option set grp='ExcelReport',status='No',value='0',process_type='" + process + "' where grp='" + "ExcelReport" + "'");

                  if (chkPdf.Checked)
                      db.update("update tbl_option set grp='Pdf Report',status='Yes',value='0',process_type='" + process + "' where grp='" + "Pdf Report" + "'");
                  else
                      db.update("update tbl_option set grp='Pdf Report',status='No',value='0',process_type='" + process + "' where grp='" + "Pdf Report" + "'");

                  if (ChkTodySales.Checked)
                      db.update("update tbl_option set grp='TodaysSalesReport',status='Yes',value='0',process_type='" + process + "' where grp='" + "TodaysSalesReport" + "'");
                  else
                      db.update("update tbl_option set grp='TodaysSalesReport',status='No',value='0',process_type='" + process + "' where grp='" + "TodaysSalesReport" + "'");

                  if (chkExpence.Checked)
                      db.update("update tbl_option set grp='ExpencesReport',status='Yes',value='0',process_type='" + process + "' where grp='" + "ExpencesReport" + "'");
                  else
                      db.update("update tbl_option set grp='ExpencesReport',status='No',value='0',process_type='" + process + "' where grp='" + "ExpencesReport" + "'");

                  if (chkItemwise.Checked)
                      db.update("update tbl_option set grp='ItemWiseSalesReport',status='Yes',value='0',process_type='" + process + "' where grp='" + "ItemWiseSalesReport" + "'");
                  else
                      db.update("update tbl_option set grp='ItemWiseSalesReport',status='No',value='0',process_type='" + process + "' where grp='" + "ItemWiseSalesReport" + "'");

                  if (chkStock.Checked)
                      db.update("update tbl_option set grp='StockReport',status='Yes',value='0',process_type='" + process + "' where grp='" + "StockReport" + "'");
                  else
                      db.update("update tbl_option set grp='StockReport',status='No',value='0',process_type='" + process + "' where grp='" + "StockReport" + "'");

                  if (chkParcelOrd.Checked)
                      db.update("update tbl_option set grp='ParcelOrderReport',status='Yes',value='0',process_type='" + process + "' where grp='" + "ParcelOrderReport" + "'");
                  else
                      db.update("update tbl_option set grp='ParcelOrderReport',status='No',value='0',process_type='" + process + "' where grp='" + "ParcelOrderReport" + "'");

                  if (chkKotRpt.Checked)
                      db.update("update tbl_option set grp='CancelKOTReport',status='Yes',value='0',process_type='" + process + "' where grp='" + "CancelKOTReport" + "'");
                  else
                      db.update("update tbl_option set grp='CancelKOTReport',status='No',value='0',process_type='" + process + "' where grp='" + "CancelKOTReport" + "'");

                  if (chkDelRpt.Checked)
                      db.update("update tbl_option set grp='DeletedItemReport',status='Yes',value='0',process_type='" + process + "' where grp='" + "DeletedItemReport" + "'");
                  else
                      db.update("update tbl_option set grp='DeletedItemReport',status='No',value='0',process_type='" + process + "' where grp='" + "DeletedItemReport" + "'");

                  if (chkTblSale.Checked)
                      db.update("update tbl_option set grp='TableSalesReport',status='Yes',value='0',process_type='" + process + "' where grp='" + "TableSalesReport" + "'");
                  else
                      db.update("update tbl_option set grp='TableSalesReport',status='No',value='0',process_type='" + process + "' where grp='" + "TableSalesReport" + "'");

                  if (chksaleRpt.Checked)
                      db.update("update tbl_option set grp='SalesReport',status='Yes',value='0',process_type='" + process + "' where grp='" + "SalesReport" + "'");
                  else
                      db.update("update tbl_option set grp='SalesReport',status='No',value='0',process_type='" + process + "' where grp='" + "SalesReport" + "'");

              }
        }

        private void chkPrintNo_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkPdf_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                txtSC.Enabled = true;
                // discount_status = "Yes";
                SC_status = "Yes";

            }
            else
            {

                SC_status = "No";
                txtSC.Enabled = false; ;
                txtSC.Text = "0";
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                Customer = "Yes";
            }
            else
            {
                Customer = "No";
            }
        }

      

        private void cbcustprint_CheckedChanged(object sender, EventArgs e)
        {
            if (cbcustprint.Checked)
            {
                custPrint = "Yes";
            }
            else
            {
                custPrint = "No";
            }



        }
        private void updateCustmer_Click(object sender, EventArgs e)
        {
            DialogResult dlg = MessageBox.Show("Do you want to save Record?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlg == DialogResult.Yes)
            {
                if (checkBox4.Checked)
                {
                    db.update("update tbl_option set grp='CustomerDetails',status='Yes',process_type='" + process + "' where grp='" + "CustomerDetails" + "'");
                }
                else
                {
                    db.update("update tbl_option set grp='CustomerDetails',status='No',process_type='" + process + "' where grp='" + "CustomerDetails" + "'");

                }
                if (cbcustprint.Checked)
                {
                    db.update("update tbl_option set grp='CustomerDetails',value='Yes',process_type='" + process + "' where grp='" + "CustomerDetails" + "'");
                }
                else
                {
                    db.update("update tbl_option set grp='CustomerDetails',value='No',process_type='" + process + "' where grp='" + "CustomerDetails" + "'");
                }
                if (cmwaitercpl.Checked)
                {
                    db.update("update tbl_option set grp='waiterDetails',status='Yes',process_type='" + process + "' where grp='" + "waiterDetails" + "'");
                }
                else
                {
                    db.update("update tbl_option set grp='waiterDetails',status='No',process_type='" + process + "' where grp='" + "waiterDetails" + "'");
                }
                if (printWaiter.Checked)
                {
                    db.update("update tbl_option set grp='waiterDetails',value='Yes',process_type='" + process + "' where grp='" + "waiterDetails" + "'");
                }
                else
                {
                    db.update("update tbl_option set grp='waiterDetails',value='No',process_type='" + process + "' where grp='" + "waiterDetails" + "'");
                }

                MessageBox.Show("update Successfully");
            
            
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmwaitercpl_CheckedChanged(object sender, EventArgs e)
        {
            if (cmwaitercpl.Checked)
            {
                waitercumpl = "Yes";
            }
            else
            {
                waitercumpl = "No";
            }
        }

        private void printWaiter_CheckedChanged(object sender, EventArgs e)
        {
            if (printWaiter.Checked)
            {
                printWaiterDetails = "Yes";
            }
            else
            {
                printWaiterDetails = "No";
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void Cbsimple_CheckedChanged(object sender, EventArgs e)
        {
            if (Cbsimple.Checked)
            {
                cbcombine.Checked = false;
                cbseperate.Checked = false;
            }
        }

        private void cbcombine_CheckedChanged(object sender, EventArgs e)
        {
            if (cbcombine.Checked)
            {
                Cbsimple.Checked = false;
                cbseperate.Checked = false;
            }
        }

        private void cbseperate_CheckedChanged(object sender, EventArgs e)
        {
            if (cbseperate.Checked)
            {
                Cbsimple.Checked = false;
                cbcombine.Checked = false;
                cbsecond.Visible = true;


            }
            else
            {
                cbsecond.Visible = false;
            }
        

        }

        private void cbsecond_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (cbgst.Checked)
            {
                txtgst.Enabled = true;
                // discount_status = "Yes";
                gst = "Yes";
            }
            else
            {

                gst = "No";
                txtgst.Enabled = false; ;
                txtgst.Text = "0";
            }
        }

        private void cbprintbill_CheckedChanged(object sender, EventArgs e)
        {
            if(cbprintbill.Checked)
            {
                Printbill = "Yes";
            }
            else
            {
                Printbill = "No";
            }
        }

        private void cbnoprints_CheckedChanged(object sender, EventArgs e)
        {
            if (cbnoprints.Checked)
            {

                txtnop.Enabled = true;
            }
            else
            {
                txtnop.Enabled = false;
            }
        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            db.insert("insert into tbl_option(grp,status,value,process_type) values('" + txtdisp.Text + "', 'Yes', '" + txtPrintername .Text+ "','kp')");

            MessageBox.Show("Prnter Added Sucssfully.......!");
        }

        private void checkBox6_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox6.Checked)
            {
                afterprint = "Yes";
            }
            else
            {
                afterprint = "No";
            }
        }

        private void chkDiscount_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void printtodays_CheckedChanged(object sender, EventArgs e)
        {
            if (printtodays.Checked)
            {
                printtoday = "Yes";
            }
            else
            {
                printtoday = "No";
            }
        }

        private void othersetting_Click(object sender, EventArgs e)
        {

        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtgst_TextChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TableManage tm = new TableManage();
            tm.ShowDialog();
        }

      
    }
}
