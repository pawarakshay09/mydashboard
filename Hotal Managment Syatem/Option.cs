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
using System.IO;

namespace Hotal_Managment_Syatem
{
    public partial class rdbTeklogic : Form
    {
        Database db = new Database();
        string discount_status = "";
        string vat_status = "";
        string process = "", order_generate = "", KOT_printStatus = "", dateChangeStatus = "", printFormation = "", notification = "", ParcelDetails = "", printerName = "", grpKotStatus = "", SC_status = "";
        string orderstatus = "", printerSize="";
        public rdbTeklogic()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // ************** chk order type ****************
            if (rdb_mouse.Checked) 
                process = "By Mouse Click";
            
            else if(rdb_code.Checked)
                process = "By Item Code";

             else
                process = "By Item Name";


            // ************chk order no.daywise or continuous *******
            if (rdb_dtwise.Checked) 
                order_generate = "Daywise";
        
            else 
                order_generate = "Continuous";
           

            //*************** chk the kot opetion is click or not ****
            if ( checkBoxPrintKOT.Checked)
                KOT_printStatus = "Yes";
            else
              KOT_printStatus  = "No";
            //*************** chk the kot Snacks,other and Drink opetion is click or not  ****
            if (chkSnacks.Checked || chkOther.Checked || chkDrink.Checked)
                grpKotStatus = "Yes";
            else
                grpKotStatus = "No";


            // **************** chk print format ********************
            if (rdb_simplePrint.Checked)

                printFormation = "Simple";

            else

                printFormation = "Composite";
            
            //yogesh



            //if (Cbsimple.Checked)
            //{
            //    printFormation = "Simple";
            //}
            //else if (cbseperate.Checked)
            //{
            //    printFormation = "Composite";
            //}
            //else
            //{
            //    printFormation = "CombineAll";
            //}

            // **************** chk printer Name ********************
            if (rdbcutPrint.Checked)
                printerName = "Cut Print";
            else
                printerName = "Continuous Print";
             

            // ************** chk date change option is clicked or not ****
            if (chk_date.Checked)
                dateChangeStatus = "Yes";
            else
                dateChangeStatus = "No";

            // ************ chk notification status ******************

            if (chk_notification.Checked)
                notification = "Yes";
            else
                notification = "No";


            // ************ chk Parcel Details ******************

            if (chkParcelDetails.Checked)
                ParcelDetails = "Yes";
            else
                ParcelDetails = "No";

            // **************** chk printer Size ********************
            if (rdb3Inch.Checked)
                printerSize = "3 Inch";
            else
                printerSize = "2 Inch";


            DialogResult dlg = MessageBox.Show("Do you want to save Record?","Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (dlg == DialogResult.Yes)
            {

                //************ Update printer Size **********
                if (db.ChkDb_Value("select * from tbl_option where grp='PrintSize'"))
                    db.update("update tbl_option set grp='PrintSize',status='Yes',value='" + printerSize + "',process_type='" + process + "' where grp='" + "PrintSize" + "'");
                else
                    db.insert("insert into tbl_option values('PrintSize','Yes','" + printerSize + "','" + process + "')");


                //********** Update Tax and Discount ***************

                if (db.ChkDb_Value("select * from tbl_option where grp='Discount'"))
                    db.update("update tbl_option set grp='" + "Discount" + "',status='" + discount_status + "',value='" + txt_disc.Text + "',process_type='" + process + "'  where grp='" + "Discount" + "'");
                else
                    db.insert("insert into tbl_option values('Discount','" + discount_status + "','" + txt_disc.Text + "','" + process + "')");

                if (db.ChkDb_Value("select * from tbl_option where grp='Tax'"))
                    db.update("update tbl_option set grp='" + "Tax" + "',status='" + vat_status + "',value='" + txt_tax.Text + "',process_type='" + process + "'  where grp='" + "Tax" + "'");
                else
                    db.insert("insert into tbl_option values('Tax','" + vat_status + "','" + txt_tax.Text + "','" + process + "')");

                //********** Update KOT status ***************
                if (db.ChkDb_Value("select * from tbl_option where grp='KotPrint'"))
                    db.update("update tbl_option set status='" + KOT_printStatus + "' where grp='" + "KotPrint" + "'");
                else
                    db.insert("insert into tbl_option values('KotPrint','" + KOT_printStatus + "','0','" + process + "')");

                //********** Update Printer for KOT and Bill ***************
                if (db.ChkDb_Value("select * from tbl_option where grp='KOT Printer'"))
                    db.update("update tbl_option set grp='KOT Printer',status='Yes',value='" + txt_kot.Text + "' where grp='" + "Kot Printer" + "'");
                else
                    db.insert("insert into tbl_option values('KOT Printer','Yes','" + txt_kot.Text + "','" + process + "')");

                if (db.ChkDb_Value("select * from tbl_option where grp='Bill Printer'"))
                    db.update("update tbl_option set grp='Bill Printer',status='Yes',value='" + txt_bill.Text + "'  where grp='" + "Bill Printer" + "'");
                else
                    db.insert("insert into tbl_option values('Bill Printer','Yes','" + txt_bill.Text + "','" + process + "')");


                //********** Update Printer for KOT Snacks ,Other and Drink ***************
                if (db.ChkDb_Value("select * from tbl_option where grp='KOT Printer_Snacks'"))
                    db.update("update tbl_option set grp='KOT Printer_Snacks',status='" + grpKotStatus + "',value='" + txtkotSnacks.Text + "'  where grp='" + "KOT Printer_Snacks" + "'");
                else
                    db.insert("insert into tbl_option values('KOT Printer_Snacks','Yes','" + txtkotSnacks.Text + "','" + process + "')");

                if (db.ChkDb_Value("select * from tbl_option where grp='KOT Printer_Other'"))
                    db.update("update tbl_option set grp='KOT Printer_Other',status='" + grpKotStatus + "',value='" + txtkotOther.Text + "'  where grp='" + "KOT Printer_Other" + "'");
                else
                    db.insert("insert into tbl_option values('KOT Printer_Other','Yes','" + txtkotOther.Text + "','" + process + "')");

                if (db.ChkDb_Value("select * from tbl_option where grp='KOT Printer_Drink'"))
                    db.update("update tbl_option set grp='KOT Printer_Drink',status='" + grpKotStatus + "',value='" + txtkotDrink.Text + "'   where grp='" + "KOT Printer_Drink" + "'");
                else
                    db.insert("insert into tbl_option values('KOT Printer_Drink','Yes','" + txtkotDrink.Text + "','" + process + "')");


                //**BY YOGESH ----For Service Charge ,SC panel----////
                if (db.ChkDb_Value("select * from tbl_option where grp='ServiceTaxvalue'"))
                {
                    db.update("update tbl_option set grp='ServiceTaxvalue', status='" + SC_status + "',value='" + txtSC.Text + "',process_type='" + process + "' where grp='" + "ServiceTaxvalue" + "'");
                    //db.update("ALTER TABLE [dbo].[table_order] ADD  CONSTRAINT [table_order_serviceTaxVal]  DEFAULT (('" + txtSC.Text + "')) FOR [serviceTaxVal]");

                    //db.update("ALTER TABLE [dbo].[table_order] ADD  CONSTRAINT [table_order_serviceTaxfood]  DEFAULT (('" + txtSC.Text + "')) FOR [serviceTaxFoodVal]");
                    //db.update("ALTER TABLE [dbo].[table_order] ADD  CONSTRAINT [table_order_serviceTaxliq]  DEFAULT (('" + txtSC.Text + "')) FOR [serviceTaxLiquorVal]");
                
                
                }

                //yogesh 21.02.2019

                if (db.ChkDb_Value("select * from tbl_option where grp='OperationMode'"))
                    db.update("update tbl_option set grp='OperationMode',status='" + click + "' where grp='" + "OperationMode" + "'");
                else
                    db.update("update tbl_option set grp='OperationMode',status='" + click + "' where grp='" + "OperationMode" + "'");
           



                //********** Update Report Backup Location ***************

                string drive = Path.GetPathRoot(txt_locaion.Text);   // e.g. K:\

                if (File.Exists(drive))
                {
                    MessageBox.Show("Drive " + drive + " not found or inaccessible",
                                    "Error");
                    return;
                }

                if (db.ChkDb_Value("select * from tbl_option where grp='Report Location'"))
                    db.update("update tbl_option set grp='Report Location',status='Yes',value='" + txt_locaion.Text + "',process_type='" + process + "' where grp='" + "Report Location" + "'");
                else
                    db.insert("insert into tbl_option values('Report Location','Yes','" + txt_locaion.Text + "','" + process + "')");

                // *********** Update date in dbBackup_status ************

              //  string Changedate=dtp_date.Value.ToString("MM-dd-yyyy");
                if(chk_date.Checked)
                 db.update("update tbl_dayend_status set ddate='" + dtp_date.Text + "'");

                //************ Update print status **********
                if (db.ChkDb_Value("select * from tbl_option where grp='Print formation'"))
                    db.update("update tbl_option set grp='Print formation',status='Yes',value='" + printFormation + "',process_type='" + process + "' where grp='" + "Print formation" + "'");
                else
                    db.insert("insert into tbl_option values('Print formation','Yes','" + printFormation + "','" + process + "')");

                //************ Update printer Name **********
                if (db.ChkDb_Value("select * from tbl_option where grp='PrinterName'"))
                    db.update("update tbl_option set grp='PrinterName',status='Yes',value='" + printerName + "'  where grp='" + "PrinterName" + "'");
                else
                    db.insert("insert into tbl_option values('PrinterName','Yes','" + printerName + "','" + process + "')");



                //************* Update notification status **********
                if (db.ChkDb_Value("select * from tbl_option where grp='Notification'"))
                  db.update("update tbl_option set grp='Notification',status='" + notification + "',value='" + notification + "',process_type='" + process + "' where grp='" + "Notification" + "'");
                else
                    db.insert("insert into tbl_option values('Notification','" + notification + "','" + notification + "','" + process + "')");

                //************* Update Order status **********
                if (db.ChkDb_Value("select * from tbl_option where grp='Counter Order'"))
                {
                    if (chk_counter.Checked)
                    {
                        db.update("update tbl_option set grp='Counter Order',status='Yes',value='0',process_type='" + process + "' where grp='" + "Counter Order" + "'");
                    }
                    else
                        db.update("update tbl_option set grp='Counter Order',status='No',value='0',process_type='" + process + "' where grp='" + "Counter Order" + "'");
                }
                else
                    db.insert("insert into tbl_option values('Counter Order','" + notification + "','" + notification + "','" + process + "')");

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

                if(chk_kotOrder.Checked)
                    db.update("update tbl_option set grp='Kot Print Button',status='Yes',value='0',process_type='" + process + "' where grp='" + "Kot Print Button" + "'");
                else
                    db.update("update tbl_option set grp='Kot Print Button',status='No',value='0',process_type='" + process + "' where grp='" + "Kot Print Button" + "'");

                if(chkParcelDetails.Checked)
                    db.update("update tbl_option set grp='Parcel Details',status='Yes',value='0',process_type='" + process + "' where grp='Parcel Details'");
                else
                    db.update("update tbl_option set grp='Parcel Details',status='No',value='0',process_type='" + process + "' where grp='Parcel Details'");

                if (chk_pay.Checked)
                    db.update("update tbl_option set grp='Pay Button',status='Yes',value='0',process_type='" + process + "' where grp='Pay Button'");
                else
                    db.update("update tbl_option set grp='Pay Button',status='No',value='0',process_type='" + process + "' where grp='Pay Button'");

                // Set Logo Status
                string logoStatus;
                if (chkLogo.Checked)
                    logoStatus = "Yes";
                else
                    logoStatus = "No";
                db.update("update tbl_option set grp='LogoPrint',status='" + logoStatus + "',value='"+txtImagePath.Text+"',process_type='" + process + "' where grp='" + "LogoPrint" + "'");

                MessageBox.Show("Record Inserted Successfully", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //  MessageBox.Show("Record Inserted Successfully", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txt_disc.Enabled = true;
                discount_status = "Yes";
               
            vat_status = "Yes";
            }
            else
            {
                discount_status = "No";
                vat_status = "No";
                //txt_disc.Text = "0";
                //txt_tax.Text = "0";
                txt_disc.Enabled = false; ;
            }

        }
        public string click;
        private void Option_Load(object sender, EventArgs e)
        {



            string clicktype = db.getDbstatus_Value("select status from tbl_option where grp='OperationMode'");
            if (clicktype == "By Mouse Click")
            {
                radiomouse.Checked = true;
                click = "By Mouse Click";
                radiokeybord.Checked = false;
            }
            else
            {
                radiomouse.Checked = false;
                radiokeybord.Checked = true;
                click = "By Item Code ";

            }

           

           //get the pervious vales from the db and assign it
            float disc_value=db.getDb_Value("select value from tbl_option where grp='"+"Discount"+"'");
           txt_disc.Text = disc_value.ToString();
           float tax_value = db.getDb_Value("select value from tbl_option where grp='" + "Tax" + "'");
           txt_tax.Text = tax_value.ToString();


            string disc_status=db.getDbstatus_Value("select status from tbl_option where grp='"+"Discount"+"'");
            string tax_status=db.getDbstatus_Value("select status from tbl_option where grp='"+"Tax"+"'");


            //*For SC
            string SC = db.getDbstatus_Value(" select status from tbl_option where grp='" + "ServiceTaxvalue" + "'");
            float txtsc = db.getDb_Value("select value from tbl_option where grp='" + "ServiceTaxvalue" + "'");
            txtSC.Text = txtsc.ToString();



            // Set Existing printer values
            string bill_Printer = db.getDbstatus_Value("select value from tbl_option where grp='" + "Bill Printer" + "'");
            string Kot_Printer = db.getDbstatus_Value("select value from tbl_option where grp='" + "KOT Printer" + "'");

            txt_bill.Text = bill_Printer;
            txt_kot.Text = Kot_Printer;

            // Set Existing printer values for snacks,other,Drink
            string Kotsnacks = db.getDbstatus_Value("select value from tbl_option where grp='" + "KOT Printer_Snacks" + "'");
            string KotOther = db.getDbstatus_Value("select value from tbl_option where grp='" + "KOT Printer_Other" + "'");
            string KotDrink = db.getDbstatus_Value("select value from tbl_option where grp='" + "KOT Printer_Drink" + "'");

            txtkotSnacks.Text = Kotsnacks;
            txtkotOther.Text = KotOther;
            txtkotDrink.Text = KotDrink;

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

            // ********** Display Existing Order staus ****************

            string pro_type = db.getDbstatus_Value("select process_type from tbl_option ");//where process_type='" + "By Item Code" + "'");
            string pro_type_1 = db.getDbstatus_Value("select process_type from tbl_option");// where process_type='" + "By Mouce Click" + "'");


            // ********** Display Printer Size ****************
            string printersize = db.getDbstatus_Value("select value from tbl_option where grp='" + "PrintSize" + "'").ToString();

            if (printersize == "3 Inch")
            {
                rdb3Inch.Checked = true;
                rdb2Inch.Checked = false;
            }
            else
            {
                rdb3Inch.Checked = false;
                rdb2Inch.Checked = true;
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
                checkBox2.Checked = true;
            }
            else
            {
                checkBox2.Checked = false;
            }

            //Yogesh
            //****** for SC existing status **********

            if(SC == "Yes")
            {
                checkBox3.Checked = true;
                SC_status = "Yes";


            }
            else
            {
                checkBox3.Checked = false;
                SC_status = "No";
            }



            // ************ Display Existing KOT status *****************
            if (db.getDbstatus_Value("select status from tbl_option where grp='" + "KotPrint" + "'").ToString() == "Yes")
                checkBoxPrintKOT.Checked = true;
            else
              checkBoxPrintKOT.Checked=false;

            // ************ Display Existing KOT Printer status *****************
            if (db.getDbstatus_Value("select status from tbl_option where grp='" + "KOT Printer" + "'").ToString() == "Yes")
                checkBox_KotPrinter.Checked = true;
            else
                checkBox_KotPrinter.Checked = false;

            // ************ Display Existing Bill Printer status *****************
            if (db.getDbstatus_Value("select status from tbl_option where grp='" + "Bill Printer" + "'").ToString() == "Yes")
                checkBox_billPrinter.Checked = true;
            else
                checkBox_billPrinter.Checked = false;

            // ************ Display Kot Printer for Snacks *****************
            if (db.getDbstatus_Value("select status from tbl_option where grp='" + "KOT Printer_Snacks" + "'").ToString() == "Yes")
                chkSnacks.Checked = true;
            else
                chkSnacks.Checked = false;
            // ************ Display Kot Printer for Other *****************
            if (db.getDbstatus_Value("select status from tbl_option where grp='" + "KOT Printer_Other" + "'").ToString() == "Yes")
                chkOther.Checked = true;
            else
                chkOther.Checked = false;
            // ************ Display Kot Printer for Drink *****************
            if (db.getDbstatus_Value("select status from tbl_option where grp='" + "KOT Printer_Drink" + "'").ToString() == "Yes")
                chkDrink.Checked = true;
            else
                chkDrink.Checked = false;


            this.CancelButton = button2;

            // ********** Display Existing File Location of report backup ****************

            string fileLocation = db.getDbstatus_Value("select value from tbl_option where grp='" + "Report Location" + "'");
            txt_locaion.Text = fileLocation;


            // ********** Display Existing print formation of bill ****************
            string printype = db.getDbstatus_Value("select value from tbl_option where grp='" + "Print formation" + "'").ToString();

            if (printype == "Simple")
            {
                rdb_simplePrint.Checked = true;
                rdb_compositePrint.Checked = false;
            }
            else
            {
                rdb_simplePrint.Checked = false;
                rdb_compositePrint.Checked = true;
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
            if (db.getDbstatus_Value("select status from tbl_option where grp='"+"Parcel Details"+"'").ToString() == "Yes")
                chkParcelDetails.Checked = true;
            else
                chkParcelDetails.Checked = false;
            //****Pay Button******************************
            if (db.getDbstatus_Value("select status from tbl_option where grp='"+"Pay Button"+"'").ToString() == "Yes")
                chk_pay.Checked = true;
            else
                chk_pay.Checked = false;



            // here get logo name

            if (db.ChkDb_Value("select value from tbl_option where grp='LogoPrint'"))
            {
                txtImagePath.Text = db.getDbstatus_Value("select value from tbl_option where grp='LogoPrint'");
            }

            db.formFix(this);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                txt_tax.Enabled = true;
                // discount_status = "Yes";
                vat_status = "Yes";
            }
            else
            {
             
                vat_status = "No";
                txt_tax.Enabled = false; ;
            }

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonSetOrderNO_Click(object sender, EventArgs e)
        {
            //set the new order id starting at evry month
            if (textBoxUpdateOrderNo.Text != "" && textBoxUpdateOrderNo.Text != "0" && float.Parse(textBoxUpdateOrderNo.Text) > float.Parse(textBoxCurrentOrder.Text))
            {
                
                  DialogResult dlg = MessageBox.Show("Do you want to Set the new order number?","Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
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

        private void chk_date_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_date.Checked)
                dtp_date.Enabled = true;
            else
                dtp_date.Enabled = false;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void rdb_dtwise_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txt_disc_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);
        }

        private void txt_tax_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);
        }

        private void rdb_simplePrint_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxPrintKOT_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rdb_mouse_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkLogo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLogo.Checked)
                panelImagePath.Visible = true;
            else
                panelImagePath.Visible = false;

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
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
            }

        }

        private void rdb_code_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radiomouse_CheckedChanged(object sender, EventArgs e)
        {
            if (radiomouse.Checked)
            {
               
                click = "By Mouse Click";
               
            }
            else
            {
                
                click = "By Item Code ";

            }
        }

        private void rdb_compositePrint_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Cbsimple_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }
    }
}
