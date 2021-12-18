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
    public partial class waiter_payment : Form
    {
        Database db = new Database();

        float day_wegs;
        string qury;
        int p=0, a=0, wID;
        string w_id = "0"; string st_status = "";


        public waiter_payment()
        {
            InitializeComponent();
        }

        private void waiter_payment_Load(object sender, EventArgs e)
        {
            this.CancelButton = btnBack;
            db.comboFill(cmbWaiterName, "SELECT wname FROM waiter_dtls", "waiter_dtls", "waiter_dtls", "wname");
            DateTime dt = System.DateTime.Now;
            string date = dt.ToString("MM'-'dd'-'yyyy");
            label_date.Text = date;
            this.MaximumSize = this.MinimumSize = this.Size;
            textBoxw_nm.Text = "";
            textBox_wType.Text = "";
            textBox_salary.Text = "";
            cmbWaiterName.Text = "";
        }

        void get_waiter_dtls()
        {
            string query = "SELECT w_id FROM waiter_dtls WHERE wname='"+ cmbWaiterName.Text +"'";
            wID = db.GetUniqueId(query);

            if (cmbWaiterName.Text != "")
                w_id=cmbWaiterName.Text;
            try
            {
                qury = "select * from waiter_dtls where w_id=" + wID + "";

                SqlCommand cmd = new SqlCommand(qury, db.cn);
                db.cnopen();
                SqlDataReader rd = cmd.ExecuteReader();

                if (rd.Read() == true)
                {
                    textBoxw_nm.Text = rd["wname"].ToString();
                    textBox_salary.Text = rd["salary"].ToString();
                    textBox_wType.Text = rd["work_type"].ToString();
                    db.cnclose();
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        void get_waiter_p_a()
        {           
            if (cmbWaiterName.SelectedItem != null)
                w_id = cmbWaiterName.SelectedValue.ToString();
            try
            {
                string datefrm = dateTimePicker1.Value.ToString("MM'-'dd'-'yyyy");
                string dateto = dateTimePicker2.Value.ToString("MM'-'dd'-'yyyy");
                qury = "select * from waiter_prsenty where date BETWEEN '" + datefrm + "' and '" + dateto + "' and  w_id=" + wID + "";

                SqlCommand cmd = new SqlCommand(qury, db.cn);
                db.cnopen();
                SqlDataReader rd = cmd.ExecuteReader();
                p = 0;
                a = 0;
                while (rd.Read() == true)
                {

                    if (rd["status"].ToString() == "Present")
                        p++;
                    else
                        a++;
                }
                db.cnclose();
            
                }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
            textBox_prsenty.Text = p.ToString();
            textBox_absent.Text = a.ToString();
        }

        void pay_waiter_salary()
        {

            string datefrm = dateTimePicker1.Value.ToString("MM'-'dd'-'yyyy");
            string dateto = dateTimePicker2.Value.ToString("MM'-'dd'-'yyyy");
           // string date = label_date.Text.ToString("MM-'-'dd'-'yyyy");
            DateTime dt = System.DateTime.Now;
            string date = dt.ToString("MM'-'dd'-'yyyy");
            label_date.Text = date;

            string query = "insert into Waiter_Payment(w_id,date,present_days,amount,W_status,dt_frm,dt_to) values(" + wID + ",'" + label_date.Text + "','" + textBox_prsenty.Text + "','" + txt_paid_amt.Text + "','" + st_status + "','" + datefrm  + "','" + dateto  + "') ";
                       
            SqlCommand cmd = new SqlCommand(query, db.cn);
            db.cnopen();
            try
            {
                int cnt = (int)cmd.ExecuteNonQuery();
                if (cnt != 0)
                {
                    MessageBox.Show("Added Sucessfully", "Status Message");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                db.cnclose(); 
            }
        }

        void payment_bind()
        {
            string datefrm = dateTimePicker1.Value.ToString("MM'-'dd'-'yyyy");
            string dateto = dateTimePicker2.Value.ToString("MM'-'dd'-'yyyy");//w_id=(select w_id from waiter_dtls where wname='" + cmbWaiterName.Text + "')AND (dt_frm BETWEEN '"+datefrm+"' AND '"+dateto+"')
            try
            {

                SqlDataAdapter da = new SqlDataAdapter("SELECT  date AS Date, present_days AS Presenty, amount AS Amount, W_status  AS Status FROM            Waiter_Payment WHERE        w_id=(select w_id from waiter_dtls where wname='" + cmbWaiterName.Text + "')  ORDER BY Waiter_Payment_Id DESC", db.cn);
                db.cnopen();
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

               
                db.cnclose();
               
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void btnShowData_Click(object sender, EventArgs e)
        {
            string datefrm = dateTimePicker1.Value.ToString("MM'-'dd'-'yyyy");
            string dateto = dateTimePicker2.Value.ToString("MM'-'dd'-'yyyy");
           // txt_advance.Text = "0";
            if (cmbWaiterName.Text == "")
            {
                MessageBox.Show("Please select waiter name", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            else
            {
               
                get_waiter_dtls();
                payment_bind();
                float cust_id = db.getDb_Value("select Customer_id from Custmer where name='"+cmbWaiterName.Text+"'");
               txt_credit.Text= db.getDb_Value("select Total_bill from total_bill where status='Credit' and datetime between '" + datefrm + "' and '" + dateto + "' and Customer_id='"+cust_id+"'").ToString();
               txt_advance.Text = "0";

            //   txt_advance.Text = db.getDb_Value("select sum(adv_amt from tbl_advance_master where waiter_name='"+cmbWaiterName.Text+"'").ToString();

              if(db.ChkDb_Value("SELECT  * FROM   tbl_advance_dtl INNER JOIN    tbl_advance_master ON tbl_advance_dtl.waiter_name = tbl_advance_master.waiter_name WHERE        (tbl_advance_master.waiter_name = '"+cmbWaiterName.Text+"')"))
                 txt_advance.Text = db.getDb_Value("SELECT  sum( tbl_advance_master.adv_amt) - SUM(tbl_advance_dtl.paid_amt) AS Advance FROM   tbl_advance_dtl INNER JOIN    tbl_advance_master ON tbl_advance_dtl.waiter_name = tbl_advance_master.waiter_name WHERE (tbl_advance_master.waiter_name = '"+cmbWaiterName.Text+"') GROUP BY tbl_advance_master.waiter_name").ToString();
                // day_wegs = float.Parse(textBox_salary.Text.ToString()) / 30;

                // get waiter prsenty and absenty
                get_waiter_p_a();

                dgv_credit.DataSource = db.Displaygrid("SELECT   total_bill.Total_bill as Amount, total_bill.datetime as Date, Custmer.name as [Customer Name] FROM  Custmer INNER JOIN   total_bill ON Custmer.Customer_id = total_bill.Customer_id WHERE (Custmer.name = '"+cmbWaiterName.Text+"')");
                dgv_credit.Columns[2].Width = 150;
                //assign the day wise salary to the waiter
             //   textBox_amt.Text = (day_wegs * p).ToString();
                //string query = "SELECT w_id FROM waiter_dtls WHERE wname='" + cmbWaiterName.Text + "'";
                //wID = db.GetUniqueId(query);
                float w_id = db.getDb_Value("select w_id from waiter_dtls where wname='" + cmbWaiterName.Text + "'");
                //if (db.ChkDb_Value("select * from Waiter_Payment where w_id='" + w_id + "' and date between '" + datefrm + "' and '" + dateto + "'"))
                //{
                //    MessageBox.Show("Payment Is Already Done", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
               
                payment_bind();
            }
        }
       
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnWaiterAdd_Click(object sender, EventArgs e)
        {
            //if(textBoxw_nm.Text =="" || textBox_wType.Text == ""||cmbWaiterName.Text ==  "")
            //{
            //    MessageBox.Show("Enter All Field","",MessageBoxButtons.OK,MessageBoxIcon.Warning );
            //    return;
            //}
           
                if  (radioButton_pay.Checked == true)
                {
                    st_status = "Payment";
                    pay_waiter_salary();
                    payment_bind();
                }
                
                    st_status = "Advance";
                    if (radioButtonAdvs.Checked)
                    {
                        db.insert("insert into tbl_advance_master values('" + label_date.Text + "','" + cmbWaiterName.Text + "','" + textBox_amt.Text + "')");
                        db.insert("insert into tbl_advance_dtl values('" + cmbWaiterName.Text + "','" + label_date.Text + "','0')");

                    }

                    if(chk_adv.Checked)
                    db.insert("insert into tbl_advance_dtl values('" + cmbWaiterName.Text + "','" + label_date.Text + "','" + lbl_advance.Text + "')");

                pay_waiter_salary();// add the waiter salary
                payment_bind();// bind the data
               // MessageBox.Show("Record save successfully");
                cmbWaiterName.Text = "";
                textBoxw_nm.Text = "";
                    textBox_wType.Text = "";
                    textBox_salary.Text = "";
                    //payment_bind();
                   
             
                clear();
            //this.Close();
        }

        void clear()
        {
            txt_advance.Text = "";
            txt_credit.Text = "";
            txt_paid_amt.Text = "";
            txt_salary.Text = "";
            textBox_amt.Text = "";
            textBox_prsenty.Text = "";
            textBox_salary.Text = "";
            textBox_wType.Text = "";
            textBoxw_nm.Text = "";
            cmbWaiterName.Text = "";
            
        }
        private void cmbWaiterName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //get_waiter_dtls();
        }

        private void radioButton_pay_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButtonAdvs_CheckedChanged(object sender, EventArgs e)
        {
            //textBoxw_nm.Text = cmbWaiterName.Text;
            //textBox_prsenty.Text = "0";
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox_prsenty_TextChanged(object sender, EventArgs e)
        {
           // textBox_absent.Text =(int.Parse( label_NOdays.Text) -int.Parse ( textBox_prsenty.Text )).ToString();
            if (textBox_prsenty.Text != "")
            {
                double present_days = double.Parse(textBox_prsenty.Text);
                double salary = Double.Parse(textBox_salary.Text);
                double total = present_days * salary;
                txt_salary.Text = total.ToString();

                float sal = float.Parse(txt_salary.Text);
                float credit = float.Parse(txt_credit.Text);
                float tot_amt = sal - credit;
                textBox_amt.Text = tot_amt.ToString();
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void chk_adv_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_adv.Checked)
            {
                    float paid = float.Parse(txt_paid_amt.Text);
                    float amt = float.Parse(textBox_amt.Text);
                    float total = amt - paid;
                    lbl_advance.Text = total.ToString();

               

                float adv_remain = float.Parse(txt_advance.Text) - float.Parse(lbl_advance.Text);
                txt_advance.Text = adv_remain.ToString();
            }
        }
    }
}
