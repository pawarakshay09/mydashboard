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
    public partial class waitr_payment_1 : Form
    {
        Database db = new Database();
        float prsentDay=0, absentDays=0;
       public  string datefrm, dateto;
       float cust_id = 0;
       float emp_id = 0;
       bool cmbFlag = false;
        public waitr_payment_1()
        {
            InitializeComponent();
        }

        private void waitr_payment_1_Load(object sender, EventArgs e)
        {
            _pagload();
        }
        void _pagload()
        {
            this.CancelButton = buttonClose;
            db.comboFill(cmb_empname, "SELECT wname FROM waiter_dtls", "waiter_dtls", "wname", "wname");
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
            BindAllData();

        }
        
        public void getEmployePrsenty()
        {
          
                datefrm = dateTimePickerFrom.Value.ToString("MM'-'dd'-'yyyy");
                dateto = dateTimePickerTo.Value.ToString("MM'-'dd'-'yyyy");
                emp_id = db.getDb_Value("select w_id from waiter_dtls where wname='" + cmb_empname.Text + "'");

                if (!db.ChkDb_Value("select * from Waiter_Payment where W_status='Paid' and date between '" + datefrm + "' and '" + dateto + "'"))
                {
                    prsentDay = db.getDb_Value("select count(*) from waiter_prsenty where date BETWEEN '" + datefrm + "' and '" + dateto + "' and  w_id='" + emp_id + "' and status='Present'");
                    absentDays = db.getDb_Value("select count(*) from waiter_prsenty where date BETWEEN '" + datefrm + "' and '" + dateto + "' and  w_id='" + emp_id + "' and status='A'");

                    textBox_prsenty.Text = prsentDay.ToString();
                    textBox_absent.Text = absentDays.ToString();
                }
            
          
        }

        public void salaryCal()
        {
           
            emp_id = db.getDb_Value("select w_id from waiter_dtls where wname='" + cmb_empname.Text + "'");

            //calculate the sum of salary
            if (!db.ChkDb_Value("select * from Waiter_Payment where W_status='Paid' and date between '" + datefrm + "' and '" + dateto + "' and w_id='"+emp_id+"'"))
            {
                float totalsalary = 0, gTotal = 0;
                totalsalary = (prsentDay) * float.Parse(labelEmpSalary.Text);
                //less amounts rfom salary 
                gTotal = totalsalary;// -float.Parse(textBoxCreditAmt.Text.ToString());

                if (checkBoxAdvPay.Checked) // if want to cut advance amount 
                    gTotal = totalsalary - float.Parse(textBoxCutAdvnce.Text.ToString());//- float.Parse(textBoxCreditAmt.Text.ToString()) -

                txt_salary.Text = totalsalary.ToString();
                textBoxPayAmt.Text = gTotal.ToString();
            }
 
        }
        public void BindAllData()
        {
            //bind here all dgv

            dgv_salaryDtls.DataSource = db.Displaygrid("SELECT  date AS Pay_Date, present_days AS PresentDay, amount AS AmountPay, dt_frm AS From_, dt_to AS To_, w_id,Waiter_Payment_Id FROM  Waiter_Payment where w_id='" + emp_id + "'");
            dgv_salaryDtls.Columns[6].Visible = false;
            dgv_salaryDtls.Columns[5].Visible = false;
            dgv_AdvPaidDtls.DataSource = db.Displaygrid("SELECT        waiter_name as [Employee Name], date as Date, adv_amt as [Advance Amount], remark as Remark,master_id FROM            tbl_advance_master where waiter_name='" + cmb_empname.Text + "'");
            dgv_AdvPaidDtls.Columns[4].Visible=false;
            dgv_AdvPaidDtls.Columns[0].Width = 180;
            dgv_AdvPaidDtls.Columns[2].Width = 180;
            dgv_AdvRecivedDtls.DataSource = db.Displaygrid("SELECT        waiter_name as [Employee Name], date as Date, paid_amt as [Paid Amount], remark as Remark,details_id FROM            tbl_advance_dtl where waiter_name='" + cmb_empname.Text + "' and paid_amt >'0'");
            dgv_AdvRecivedDtls.Columns[4].Visible=false;
            dgv_AdvRecivedDtls.Columns[0].Width = 180;
            dgv_AdvRecivedDtls.Columns[2].Width = 180;
         }

        private void buttonSave_Click(object sender, EventArgs e)
        {
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
                db.insert("insert into Waiter_Payment values('" + emp_id + "','" + date + "','" + textBox_prsenty.Text + "','" + textBoxPayAmt.Text + "','paid','" + datefrm + "','" + dateto + "')");
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

        private void textBox_prsenty_TextChanged(object sender, EventArgs e)
        {
            // cal salary on the daily rate

            if (cmb_empname.Text == "Select")
                MessageBox.Show("Please Select Employee Name First!!!");
            else
            {
                float day_salary = 0;
                if (labelEmpSalary.Text != "-")
                    day_salary = float.Parse(labelEmpSalary.Text);
                if (textBox_prsenty.Text != "")
                {
                    //txt_salary.Text = (float.Parse(textBox_prsenty.Text) * day_salary).ToString();

                    salaryCal();//textBoxPayAmt.Text = (float.Parse(txt_salary.Text) - float.Parse(textBoxCreditAmt.Text)).ToString();
                }
            }
        }

        
      
        private void textBoxCutAdvnce_TextChanged(object sender, EventArgs e)
        {
           
            salaryCal();
        }

        private void textBoxCreditAmt_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePickerFrom_ValueChanged(object sender, EventArgs e)
        {
            datechange();
        }
        void datechange()
        {
            if (cmb_empname.Text == "Select")
                MessageBox.Show("Please Enter Employee First..");
            else
            {
                datefrm = dateTimePickerFrom.Value.ToString("MM'-'dd'-'yyyy");
                dateto = dateTimePickerTo.Value.ToString("MM'-'dd'-'yyyy");

                //show prsenty dtls
                getEmployePrsenty();
                //show cal dtls
                salaryCal();
            }
        }


        private void dateTimePickerTo_ValueChanged(object sender, EventArgs e)
        {
            datechange();

        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dlgresult = MessageBox.Show("Are you sure want to Delete?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlgresult == DialogResult.Yes)
            {
                if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage1"])
                    db.deleteGridRowDirect(dgv_salaryDtls, "Waiter_Payment", "Waiter_Payment_Id", 6);
                if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage2"])
                    db.deleteGridRowDirect(dgv_AdvPaidDtls, "tbl_advance_master", "master_id", 4);
                if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage3"])
                    db.deleteGridRowDirect(dgv_AdvRecivedDtls, "tbl_advance_dtl", "details_id", 4);
                MessageBox.Show("Record Deleted Successfully!!!","Confirm",MessageBoxButtons.OK,MessageBoxIcon.Information);
                BindAllData();
            }
        }

        private void panelSalaryDtls_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBoxPayAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);
        }

        private void txt_salary_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);
        }

        private void textBox_prsenty_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);

        }

        private void textBox_absent_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);

        }

        private void textBoxCutAdvnce_KeyPress(object sender, KeyPressEventArgs e)
        {
            db.onlyNumber(e);

        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBoxPayAmt_TextChanged(object sender, EventArgs e)
        {
            if (textBoxPayAmt.Text != "")
            {
                if (float.Parse(textBoxPayAmt.Text) > float.Parse(txt_salary.Text))
                    MessageBox.Show("Payable Amount should be Less Than Total Salary!!!");
            }
        }

        private void textBox_absent_TextChanged(object sender, EventArgs e)
        {

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
                BindAllData();
            }
        }

       
    }
}
