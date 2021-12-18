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
    public partial class Employmodule : Form
    {
        Database db = new Database();

        float prsentDay = 0, absentDays = 0,HD=0,OT=0;
        public string datefrm, dateto;
        float cust_id = 0;
        float emp_id = 0;
        bool cmbFlag = false;
        string dateFrom;
        int i;
        

        public Employmodule()
        {
            InitializeComponent();
        }

        private void Employmodule_Load(object sender, EventArgs e)
        {
            _pagload();
        }
        void _pagload()
        {
            this.CancelButton = buttonClose;
           // db.comboFill(cmb_empname, "SELECT Distinct driverName FROM tbl_sales", "tbl_sales", "driverName", "driverName");
            db.comboFill(cmb_empname, "SELECT Distinct wname FROM waiter_dtls", "waiter_dtls", "wname", "wname");

           
           
            //db.comboFill(cmb_empname, "SELECT wname FROM waiter_dtls", "waiter_dtls", "wname", "wname");
            db.formFix(this);

            this.MaximumSize = this.MinimumSize = this.Size;

            cmb_empname.Text = "Select";
            cmbFlag = true;
        }

        private void radioButtonAdvs_CheckedChanged(object sender, EventArgs e)
        {
            panelSalaryDtls.Enabled = false;
        }

        private void radioButton_pay_CheckedChanged(object sender, EventArgs e)
        {
            panelSalaryDtls.Enabled = true;
        }

        private void btnShowData_Click(object sender, EventArgs e)
        {
            emp_id = db.getDb_Value("select w_id from waiter_dtls where wname='" + cmb_empname.Text + "'");
            labelEmpSalary.Text = db.getDb_Value("select salary from waiter_dtls where w_id='" + emp_id + "'").ToString();
            if (db.ChkDb_Value("SELECT * FROM tbl_advance_dtl INNER JOIN   tbl_advance_master ON tbl_advance_dtl.master_id = tbl_advance_master.master_id WHERE        (tbl_advance_master.waiter_name = '" + cmb_empname.Text + "')"))
                // label_empPaidAdv.Text = db.getDb_Value("SELECT (tbl_advance_master.adv_amt) - SUM(tbl_advance_dtl.paid_amt) AS Advance FROM            tbl_advance_dtl INNER JOIN   tbl_advance_master ON tbl_advance_dtl.master_id = tbl_advance_master.master_id WHERE        (tbl_advance_master.waiter_name = '" + cmb_empname.Text + "') GROUP BY  tbl_advance_master.adv_amt").ToString();
                label_empPaidAdv.Text = db.getDb_Value("SELECT        SUM(ViewAdvanceMaster.Advance) - SUM(ViewAdvanceDetails.PaidAmt) AS Expr1 FROM   ViewAdvanceDetails INNER JOIN   ViewAdvanceMaster ON ViewAdvanceDetails.date = ViewAdvanceMaster.date WHERE   (ViewAdvanceMaster.waiter_name = '" + cmb_empname.Text + "')").ToString();


            string paid = db.getDbstatus_Value("select sum(paid_amt) from tbl_advance_dtl  where waiter_name ='" + cmb_empname.Text + "' ");

            string amt = db.getDbstatus_Value("select SUM(adv_amt) from tbl_advance_master where waiter_name ='" + cmb_empname.Text + "'");

            double netadv = double.Parse(amt) - double.Parse(paid);
            label_empPaidAdv.Text = netadv.ToString();



            
            BindAllData();
            getEmployePrsenty();

            
            //string emp = cmb_empname.Text;
            //dataGridView1.DataSource = db.Displaygrid(" select Distinct driverName as [EMP_Name] , m_name as [Material] ,sum(qty) as QTY ,sum(grand_total) as AMT,  deliceryDate as [Date] from tbl_sales where driverName='" + emp + "'  group by driverName,m_name,deliceryDate");


        }
        public void getEmployePrsenty()
        {

            datefrm = dateTimePickerFrom.Value.ToString("MM'-'dd'-'yyyy");
            dateto = dateTimePickerTo.Value.ToString("MM'-'dd'-'yyyy");
            emp_id = db.getDb_Value("select w_id from waiter_dtls where wname='" + cmb_empname.Text + "'");

            if (!db.ChkDb_Value("select * from Waiter_Payment where W_status='Paid' and (date between '" + datefrm + "' and '" + dateto + "') and w_id='" + emp_id + "'"))
            {
                LBLPAY.Visible = false;
                prsentDay = db.getDb_Value("select count(*) from waiter_prsenty where date BETWEEN '" + datefrm + "' and '" + dateto + "' and  w_id='" + emp_id + "' and status='Present'");
                absentDays = db.getDb_Value("select count(*) from waiter_prsenty where date BETWEEN '" + datefrm + "' and '" + dateto + "' and  w_id='" + emp_id + "' and status='Absent'");
                HD = db.getDb_Value("select count(*) from waiter_prsenty where date BETWEEN '" + datefrm + "' and '" + dateto + "' and  w_id='" + emp_id + "' and status='Halfday'");
                OT = db.getDb_Value("select count(*) from waiter_prsenty where date BETWEEN '" + datefrm + "' and '" + dateto + "' and  w_id='" + emp_id + "' and status='Overtime'");


                textBox_prsenty.Text = prsentDay.ToString();
                textBox_absent.Text = absentDays.ToString();
                txthd.Text = HD.ToString();

              //  double TOO = double.Parse(OT.ToString()) + double.Parse(OT.ToString());
                //txtot.Text = TOO.ToString();
                txtot.Text = OT.ToString();
                //int trip = dataGridView1.RowCount;
                //txttrip.Text = trip.ToString();

            }
            else
            {
                LBLPAY.Visible = true;
                txtinsentive.Text = "0";
            }


        }
        public void salaryCal()
        {

            emp_id = db.getDb_Value("select w_id from waiter_dtls where wname='" + cmb_empname.Text + "'");

            //calculate the sum of salary
            if (!db.ChkDb_Value("select * from Waiter_Payment where W_status='Paid' and date between '" + datefrm + "' and '" + dateto + "' and w_id='" + emp_id + "'"))
            {
                float totalsalary = 0, gTotal = 0;
                totalsalary = (prsentDay) * float.Parse(labelEmpSalary.Text);
                //less amounts rfom salary 
                gTotal = totalsalary;// -float.Parse(textBoxCreditAmt.Text.ToString());

                if (checkBoxAdvPay.Checked) // if want to cut advance amount 
                    gTotal = totalsalary - float.Parse(textBoxCutAdvnce.Text.ToString());//- float.Parse(textBoxCreditAmt.Text.ToString()) -

                txt_salary.Text = totalsalary.ToString();
                textBoxPayAmt.Text = gTotal.ToString();

                dateFrom = dateTimePickerFrom.Value.ToString("yyyy'-'MM'-'dd");
                dateto = dateTimePickerTo.Value.ToString("yyyy'-'MM'-'dd");

                string emp = cmb_empname.Text;
               // dataGridView1.DataSource = db.Displaygrid(" select Distinct driverName as [EMP_Name] , m_name as [Material] ,sum(qty) as QTY ,sum(grand_total) as AMT,  deliceryDate as [Date] from tbl_sales where driverName='" + emp + "'  group by driverName,m_name,deliceryDate");

            }

        }
        public void BindAllData()
        {
            //bind here all dgv

            dgv_salaryDtls.DataSource = db.Displaygrid("SELECT  date AS Pay_Date, present_days AS PresentDay, amount AS AmountPay, dt_frm AS From_, dt_to AS To_, w_id,Waiter_Payment_Id FROM  Waiter_Payment where w_id='" + emp_id + "'");
            dgv_salaryDtls.Columns[6].Visible = false;
            dgv_salaryDtls.Columns[5].Visible = false;
            dgv_AdvPaidDtls.DataSource = db.Displaygrid("SELECT        waiter_name as [Employee Name], date as Date, adv_amt as [Advance Amount], remark as Remark,master_id FROM            tbl_advance_master where waiter_name='" + cmb_empname.Text + "'");
            dgv_AdvPaidDtls.Columns[4].Visible = false;
            dgv_AdvPaidDtls.Columns[0].Width = 180;
            dgv_AdvPaidDtls.Columns[2].Width = 180;
            dgv_AdvRecivedDtls.DataSource = db.Displaygrid("SELECT        waiter_name as [Employee Name], date as Date, paid_amt as [Paid Amount], remark as Remark,details_id FROM            tbl_advance_dtl where waiter_name='" + cmb_empname.Text + "' and paid_amt >'0'");
            dgv_AdvRecivedDtls.Columns[4].Visible = false;
            dgv_AdvRecivedDtls.Columns[0].Width = 180;
            dgv_AdvRecivedDtls.Columns[2].Width = 180;


            dateFrom = dateTimePickerFrom.Value.ToString("yyyy'-'MM'-'dd");
            dateto = dateTimePickerTo.Value.ToString("yyyy'-'MM'-'dd");

            string emp = cmb_empname.Text; 
          //  dataGridView1.DataSource = db.Displaygrid(" select Distinct driverName as [EMP_Name] , m_name as [Material] ,sum(qty) as QTY ,sum(grand_total) as AMT,  deliceryDate as [Date] from tbl_sales where (deliceryDate between '" + dateFrom + "' and '" + dateto + "') and driverName='" + emp + "' group by driverName,m_name,deliceryDate");
           
            //dataGridView1.DataSource = db.Displaygrid(" select Distinct driverName as [EMP_Name] , m_name as [Material] ,sum(qty) as QTY ,sum(grand_total) as Insentive_rate , total as [Total] from tbl_sales where (deliceryDate between '" + dateFrom + "' and '" + dateto + "') and driverName='" + emp + "' group by driverName,m_name,total");

          //  dataGridView1.DataSource = db.Displaygrid("select Distinct ts.driverName as [EMP_Name] , ts.m_name as [Material] ,sum(ts.qty) as QTY ,ts.deliceryDate as [Date] ,im.insentive , sum(ts.qty)*im.insentive as [Total Insentive]  from tbl_sales ts  inner join Intial_material im  on  ts.m_name =im.m_name where  (ts.deliceryDate between '" + dateFrom + "' and '" + dateto + "')  and ts.driverName='" + emp + "' group by ts.driverName,ts.m_name,ts.deliceryDate,im.insentive ");


            dataGridView2.DataSource = db.Displaygrid("  select date as [Date],present_days as [P],HD as [HD],OT as [OT] ,Insentive as [Insentive],amount as [Paid AMT] ,W_status as [Status],dt_frm as [From Date] ,dt_to as [To Date],total_sal as [Total Salary] from Waiter_Payment where w_id='" + emp_id + "'and (date between '" + dateFrom + "' and '" + dateto + "' )");


            float total = 0;

            //if (cmbFlag)
            //{
            //    for (int i = 0; i <= dataGridView1.RowCount - 1; i++)
            //    {

            //        //string insentive_rate = db.getDbstatus_Value("select ISNULL((select insentive from Intial_material where m_name='" + dataGridView1.Rows[i].Cells["Material"].Value.ToString() + "'),0) ");

            //        //string qty = dataGridView1.Rows[i].Cells["QTY"].Value.ToString();
            //        //dataGridView1.Rows[i].Cells["Insentive_rate"].Value = insentive_rate;

            //        //total = float.Parse(insentive_rate) * float.Parse(qty);

            //        //dataGridView1.Rows[i].Cells["Total"].Value = total;


            //        cal();

            //    }
                cal();
               
           // }
           



        }
        void cal()
        {
            string present, ot, hd;
            float hdtotal;
            double sum = 0, fullsal = 0, totaldays;
            //for (i = 0; i < dataGridView1.RowCount; i++)
            //{
            //    //sum += double.Parse(dataGridView1.Rows[i].Cells["Total"].Value.ToString());
            //    sum += double.Parse(dataGridView1.Rows[i].Cells["Total Insentive"].Value.ToString());
              
            //}
            //txt_total.Text = sum.ToString();



            present=   textBox_prsenty.Text;
            hd=txthd.Text;
            hdtotal = float.Parse(hd )/ 2;
            ot = txtot.Text;
            totaldays = double.Parse(present) + double.Parse(ot) +hdtotal;
            double sal = double.Parse(labelEmpSalary.Text) * totaldays;
            fullsal = sal + double.Parse(txtinsentive.Text);
            txt_salary.Text = fullsal.ToString();









        }

        private void buttonSave_Click(object sender, EventArgs e)
        {

            //DialogResult dlgresult = MessageBox.Show("Print ?", "Confirm Print", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (dlgresult == DialogResult.Yes)
            //{

            //    LPrinter lp = new LPrinter();

            //    lp.present = textBox_prsenty.Text;
            //    lp.ott = txtot.Text;
            //    lp.hdd = txthd.Text;
            //    lp.insen = txtinsentive.Text;
            //    lp.cutadvance = textBoxCutAdvnce.Text;
            //    lp.payment = textBoxPayAmt.Text;
            //    lp.emp = cmb_empname.Text;
            //    lp.datefrom = dateTimePickerFrom.Text;
            //    lp.dateto = dateTimePickerTo.Text;
            //    lp.totsal = labelEmpSalary.Text;
            //    lp.totadv = label_empPaidAdv.Text;
            //    lp.absent = textBox_absent.Text;

            //    double remainad = double.Parse(label_empPaidAdv.Text) - double.Parse(textBoxCutAdvnce.Text);

            //    lp.reamainadv = remainad.ToString();


            //    lp.print_Payment();

            //}


             //DateTime dt =DateTime.Parse(dateTimePickerPayDate.Text.ToString());
            //string date = dt.ToString("MM'-'dd'-'yyyy");
            string date = dateTimePickerPayDate.Value.ToString("MM'-'dd'-'yyyy");


            if (radioButtonAdvs.Checked)
            {  float remainAmt=0;
                if (textBoxCutAdvnce.Text != "0") 
                      remainAmt = float.Parse(label_empPaidAdv.Text.ToString()) - float.Parse(textBoxCutAdvnce.Text.ToString());
                    db.insert("insert into tbl_advance_master values('" + date + "','" + cmb_empname.Text + "','" + textBoxPayAmt.Text + "','" + textBoxRemark.Text + "','" + remainAmt + "')");
                    float master_id = db.getDb_Value("select max(master_id) from tbl_advance_master");
                    db.insert("insert into tbl_advance_dtl values('" + cmb_empname.Text + "','" + date + "','0','" + textBoxRemark.Text + "','" + master_id + "')");
                 
            }

            if (radioButton_pay.Checked)
            {
                db.insert("insert into Waiter_Payment(w_id,date,present_days,amount,W_status,dt_frm,dt_to,total_sal,HD,OT,Insentive) values('" + emp_id + "','" + date + "','" + textBox_prsenty.Text + "','" + textBoxPayAmt.Text + "','paid','" + datefrm + "','" + dateto + "' ,'" + txt_salary.Text + "','" + txthd.Text + "','" + txtot.Text + "','" + txtinsentive .Text+ "')");
                float master_id = db.getDb_Value("select max(master_id) from tbl_advance_master");

                if (checkBoxAdvPay.Checked)
                    db.insert("insert into tbl_advance_dtl values('" + cmb_empname.Text + "','" + date + "','" + textBoxCutAdvnce.Text + "','" + textBoxRemark.Text + "','"+master_id+"')");
            }

            MessageBox.Show("Data Save Sucessfully");
            emp_id = db.getDb_Value("select w_id from waiter_dtls where wname='" + cmb_empname.Text + "'");
            labelEmpSalary.Text = db.getDb_Value("select salary from waiter_dtls where w_id='" + emp_id + "'").ToString();

            if (db.ChkDb_Value("SELECT  * FROM     tbl_advance_dtl INNER JOIN   tbl_advance_master ON tbl_advance_dtl.master_id = tbl_advance_master.master_id WHERE        (tbl_advance_master.waiter_name = '" + cmb_empname.Text + "')"))
                //label_empPaidAdv.Text = db.getDb_Value("SELECT         (tbl_advance_master.adv_amt) - SUM(tbl_advance_dtl.paid_amt) AS Advance FROM            tbl_advance_dtl INNER JOIN   tbl_advance_master ON tbl_advance_dtl.master_id = tbl_advance_master.master_id WHERE        (tbl_advance_master.waiter_name = '" + cmb_empname.Text + "') GROUP BY  tbl_advance_master.adv_amt").ToString();
                label_empPaidAdv.Text = db.getDb_Value("SELECT        SUM(ViewAdvanceMaster.Advance) - SUM(ViewAdvanceDetails.PaidAmt) AS Expr1 FROM   ViewAdvanceDetails INNER JOIN   ViewAdvanceMaster ON ViewAdvanceDetails.date = ViewAdvanceMaster.date WHERE   (ViewAdvanceMaster.waiter_name = '" + cmb_empname.Text + "')").ToString();

            string paid = db.getDbstatus_Value("select sum(paid_amt) from tbl_advance_dtl  where waiter_name ='" + cmb_empname.Text + "' ");

            string amt = db.getDbstatus_Value("select SUM(adv_amt) from tbl_advance_master where waiter_name ='" + cmb_empname.Text + "'");

            double netadv = double.Parse(amt) - double.Parse(paid);
            label_empPaidAdv.Text = netadv.ToString();

            
                

            BindAllData();
            clear();
        
        }
        void clear()
        {
            textBoxPayAmt.Text = "0";
            textBox_prsenty.Text = "0";
            textBox_absent.Text = "0";
            txt_salary.Text = "0";
            textBoxCutAdvnce.Text = "0";
            cmb_empname.Text = "";
            checkBoxAdvPay.Checked = false;

        }

        private void checkBoxAdvPay_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxAdvPay.Checked)
            {
                textBoxCutAdvnce.Enabled = true;
                textBoxCutAdvnce.Text = "0";

            }
            else
                textBoxCutAdvnce.Enabled = false;

        }

        private void cmb_empname_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_empname.Text != "" && cmbFlag)
            {
                emp_id = db.getDb_Value("select w_id from waiter_dtls where wname='" + cmb_empname.Text + "'");
                labelEmpSalary.Text = db.getDb_Value("select salary from waiter_dtls where w_id='" + emp_id + "'").ToString();
                if (db.ChkDb_Value("SELECT * FROM tbl_advance_dtl INNER JOIN   tbl_advance_master ON tbl_advance_dtl.master_id = tbl_advance_master.master_id WHERE        (tbl_advance_master.waiter_name = '" + cmb_empname.Text + "')"))
                    // label_empPaidAdv.Text = db.getDb_Value("SELECT (tbl_advance_master.adv_amt) - SUM(tbl_advance_dtl.paid_amt) AS Advance FROM            tbl_advance_dtl INNER JOIN   tbl_advance_master ON tbl_advance_dtl.master_id = tbl_advance_master.master_id WHERE        (tbl_advance_master.waiter_name = '" + cmb_empname.Text + "') GROUP BY  tbl_advance_master.adv_amt").ToString();
                    label_empPaidAdv.Text = db.getDb_Value("SELECT        SUM(ViewAdvanceMaster.Advance) - SUM(ViewAdvanceDetails.PaidAmt) AS Expr1 FROM   ViewAdvanceDetails INNER JOIN   ViewAdvanceMaster ON ViewAdvanceDetails.date = ViewAdvanceMaster.date WHERE   (ViewAdvanceMaster.waiter_name = '" + cmb_empname.Text + "')").ToString();

                string paid = db.getDbstatus_Value("select sum(paid_amt) from tbl_advance_dtl  where waiter_name ='" + cmb_empname.Text + "' ");

                string amt = db.getDbstatus_Value("select SUM(adv_amt) from tbl_advance_master where waiter_name ='" + cmb_empname.Text + "'");

                double netadv = double.Parse(amt) - double.Parse(paid);
                label_empPaidAdv.Text = netadv.ToString();


                
                BindAllData();
            }
        }

        private void textBoxPayAmt_TextChanged(object sender, EventArgs e)
        {
            if (radioButton_pay.Checked)
            {
            if (textBoxPayAmt.Text != "")
            {
                if (float.Parse(textBoxPayAmt.Text) > float.Parse(txt_salary.Text))
                    MessageBox.Show("Payable Amount should be Less Than Total Salary!!!");
            }
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
        //    DialogResult dlgresult = MessageBox.Show("Are you sure want to Delete?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        //    if (dlgresult == DialogResult.Yes)
        //    {
        //        if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage1"])
        //            db.deleteGridRowDirect(dgv_salaryDtls, "Waiter_Payment", "Waiter_Payment_Id", 6);
        //        if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage2"])
        //            db.deleteGridRowDirect(dgv_AdvPaidDtls, "tbl_advance_master", "master_id", 4);
        //        if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage3"])
        //            db.deleteGridRowDirect(dgv_AdvRecivedDtls, "tbl_advance_dtl", "details_id", 4);
        //        MessageBox.Show("Record Deleted Successfully!!!", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        BindAllData();
        //    }
        }

        private void dateTimePickerFrom_ValueChanged(object sender, EventArgs e)
        {
            LBLPAY.Visible = false;
            BindAllData();
            getEmployePrsenty();
        }

        private void txt_total_TextChanged(object sender, EventArgs e)
        {
          //  txtinsentive.Text = txt_total.Text;
        }

        private void textBoxCutAdvnce_TextChanged(object sender, EventArgs e)
        {
            double advcut=0;
            if (textBoxCutAdvnce.Text !="")
            {
                advcut = double.Parse(txt_salary.Text) - double.Parse(textBoxCutAdvnce.Text);
                textBoxPayAmt.Text = advcut.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LPrinter lp = new LPrinter();

            //lp.present = textBox_prsenty.Text;
            //lp.ott = txtot.Text;
            //lp.hdd = txthd.Text;
            //lp.insen = txtinsentive.Text;
            //lp.cutadvance = textBoxCutAdvnce.Text;
            //lp.payment = textBoxPayAmt.Text;
            //lp.emp = cmb_empname.Text;
            //lp.datefrom = dateTimePickerFrom.Text;
            //lp.dateto = dateTimePickerTo.Text;
            //lp.totsal = labelEmpSalary.Text;
            //lp.totadv = label_empPaidAdv.Text;
            //lp.totalpayment = txt_salary.Text;
            //lp.absent = textBox_absent.Text;


            //double remainad = double.Parse(label_empPaidAdv.Text) - double.Parse(textBoxCutAdvnce.Text);

            //lp.reamainadv=remainad.ToString();


            //lp.print_Payment();

        }

        private void LBLPAY_Click(object sender, EventArgs e)
        {

        }

        private void txt_salary_TextChanged(object sender, EventArgs e)
        {
            textBoxPayAmt.Text = txt_salary.Text;
        }


    }
}
