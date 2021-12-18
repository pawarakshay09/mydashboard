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
    public partial class TodayCounter : Form
    {
        
        Database db = new Database();
        string dateFrm, dateTo;
        double cash=0,card=0;
        public TodayCounter()
        {
          
            InitializeComponent();

        }
       
        private void TodayCounter_Load(object sender, EventArgs e)
        {
            this.CancelButton = btn_close;
            pageLoad();

          
           
        }

        void pageLoad()
        {
            try
            {
               // dgvcreditpay.DataSource = db.Displaygrid(@"select c.name as ComanyName,tb.Total_bill from total_bill tb inner join Custmer c on tb.Customer_id=c.Customer_id  and tb.Customer_id<>0 and tb.status='Credit to Company' ");
               

                string date1 = db.getDbstatus_Value("select ddate from tbl_dayend_status");
                date.Text = date1.ToString();
                DateTime dt = Convert.ToDateTime(date1);

                //dgvcreditpay.DataSource = db.Displaygrid(@"select c.name as ComanyName,tb.Total_bill from total_bill tb inner join Custmer c on tb.Customer_id=c.Customer_id  and tb.Customer_id<>0 and tb.status='Credit to Company'   where datetime='" + dt.ToString("MM/dd/yyyy") + "'  ");


                if (db.ChkDb_Value("select * from tbl_option where  grp='" + "printtoday" + "' and status='Yes'"))
                {
                    btn_print.Visible = true;
                }
                else
                {
                    btn_print.Visible = false;
                }


                //dataGridView1.DataSource = db.Displaygrid("SELECT   status as[Status], SUM(Total_bill) AS Total FROM  total_bill  where datetime='" + dt.ToString("MM/dd/yyyy") + "' GROUP BY status");

                dataGridView1.DataSource = db.Displaygrid("SELECT   status as[Status], SUM(Total_bill) AS Total FROM  total_bill  where datetime='" + dt.ToString("MM/dd/yyyy") + "'  and order_id <>'200' GROUP BY status");
          
               
                
              // dgv.DataSource = db.Displaygrid("SELECT   total_bill.order_id AS[Bill No.], table_order.t_id as [Table No], sum(total_bill.Total_bill) AS[Total Amount ], table_order.discAmt AS [Discount Amt],(table_order.gst/2) as[CGST] ,(table_order.gst/2) AS [SGST],status,remark as Remark FROM            total_bill INNER JOIN   table_order ON total_bill.order_id = table_order.order_id where datetime='" + dt.ToString("MM/dd/yyyy") + "' GROUP BY total_bill.order_id, table_order.t_id, table_order.discAmt,table_order.gst,status,remark order by  total_bill.order_id  asc");

                dgv.DataSource = db.Displaygrid("SELECT   total_bill.order_id AS[Order No.], table_order.t_id as [Table No], sum(total_bill.Total_bill) AS[Total AMT], (table_order.foodDiscAmt+table_order.beveragesDiscAmt+table_order.liquorDiscAmt) AS [Discount Amt],(table_order.gst/2) as[CGST] ,(table_order.gst/2) AS [SGST],status,total_bill.remark as [BY] FROM            total_bill INNER JOIN   table_order ON total_bill.order_id = table_order.order_id where datetime='" + dt.ToString("MM/dd/yyyy") + "'  and total_bill.order_id <>'200'  GROUP BY total_bill.order_id, table_order.t_id, table_order.foodDiscAmt,table_order.beveragesDiscAmt,table_order.liquorDiscAmt,table_order.gst,status,total_bill.remark order by  total_bill.order_id  asc");
            
                
                dgv.Columns[5].DefaultCellStyle.Format = "N2";
                dgv.Columns[4].DefaultCellStyle.Format = "N2";
                assignCellColor();
               

                dgvcreditpay.DataSource = db.Displaygrid(@"select c.name as [Credit On],sum(tb.Total_bill) as [AMT] from total_bill tb inner join Custmer c on tb.Customer_id=c.Customer_id 
                and tb.Customer_id<>0 and tb.status='Credit to Company'  and  datetime='" + dt.ToString("MM/dd/yyyy") + "' group by c.name");

                lbl_runningTotal.Text = db.getDbstatus_Value(@"select SUM(si.total_amount) from sales_item si  where order_id in (
                                            select max( order_id) from table_order tob 
                                            inner join table_status ts on tob.t_id=ts.t_id and ts.status='Processing' and tob.timeing='"+dt.ToString("MM/dd/yyyy")+"' group by tob.t_id )");



                DVGcreditRec.DataSource = db.Displaygrid("select c.name as [Credit On],sum(tb.Total_bill) as AMT , tb.status as [BY] from total_bill tb inner join Custmer c on tb.Customer_id=c.Customer_id and tb.Customer_id<>0 and tb.order_id='200' and tb.datetime='" + dt.ToString("MM/dd/yyyy") + "'group by c.name ,tb.status");

                cal();
                gridCell();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void assignCellColor()
        {
            try
            {
                int i = 0;
                string[] orderIdArray = new string[200];
                db.cnopen();
                SqlCommand cmd = new SqlCommand("SELECT        order_id FROM            total_bill GROUP BY order_id HAVING        (COUNT(order_id) > 1)", db.cn);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    orderIdArray[i] = rd["order_id"].ToString();//db.getDb_Value("SELECT        order_id FROM            total_bill GROUP BY order_id HAVING        (COUNT(order_id) > 1)").ToString();
                    i++;
                }
                for (int k = 0; k <= i; k++)
                {
                    for (int j = 0; j < dgv.RowCount; j++)
                    {
                        if (dgv.Rows[j].Cells[0].Value.ToString() == orderIdArray[k])
                            dgv.Rows[j].DefaultCellStyle.BackColor = Color.Silver;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void gridCell()
        {
            dgv.Columns[0].Width=80;
            dgv.Columns[1].Width = 80;
            dgv.Columns[3].Width = 80;
            dgv.Columns[5].Width = 80;
            dgv.Columns[4].Width = 80;

        }
        void cal()
        {
            cash = 0;
            card = 0;
            double sum = 0, discSum = 0, cgstsum = 0, sgstsum = 0, creditamt = 0;
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                sum += Convert.ToDouble(dgv.Rows[i].Cells[2].Value);
                discSum += Convert.ToDouble(dgv.Rows[i].Cells[3].Value);
                cgstsum += Convert.ToDouble(dgv.Rows[i].Cells[4].Value);
                sgstsum += Convert.ToDouble(dgv.Rows[i].Cells[5].Value);
            }

            for (int i = 0; i < DVGcreditRec.Rows.Count; i++)
            {
                creditamt += Convert.ToDouble(DVGcreditRec.Rows[i].Cells[1].Value);
            }


            for (int i = 0; i < DVGcreditRec.RowCount; i++)
            {
                
              string  status = DVGcreditRec.Rows[i].Cells["Status"].Value.ToString();
              if (status == "Cash")
              {
                  cash += Convert.ToDouble(DVGcreditRec.Rows[i].Cells[1].Value);
              }
              else if (status == "Card")
              {
                  card += Convert.ToDouble(DVGcreditRec.Rows[i].Cells[1].Value);
              }
                //else if (status == "Overtime")
                //    overtime++;
                //else
                //    halfDays++;

            }
            LPrinter lp = new LPrinter();
            lp.card = card.ToString();
            lp.cash = cash.ToString();


            
            string credit = Math.Round(creditamt).ToString();
            txt_total.Text = Math.Round(sum + creditamt).ToString();
            txtDiscAmt.Text = Math.Round( discSum).ToString();
            txtCgst.Text = cgstsum.ToString();
            txtSgst.Text = sgstsum.ToString();
            txtGrandTotal.Text = Math.Round(float.Parse(txt_total.Text) + float.Parse(txtDiscAmt.Text) + float.Parse(credit.ToString())).ToString();
        }
      

        private void dtp_ValueChanged(object sender, EventArgs e)
        {
            txt_total.Text = 0.ToString();
           
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            datechange();
        }

        void datechange()
        {
           
            string fromdate = dtp1.Value.ToString("MM/dd/yyyy");
            string todate = dtp2.Value.ToString("MM/dd/yyyy");
           // dgv.DataSource = db.Displaygrid("SELECT   total_bill.order_id AS[Bill No.], table_order.t_id as [Table No], sum(total_bill.Total_bill) AS[Total Amount ], table_order.discAmt AS [Discount Amt],(table_order.gst/2) as[CGST] ,(table_order.gst/2) AS [SGST],status,remark as Remark FROM            total_bill INNER JOIN   table_order ON total_bill.order_id = table_order.order_id where datetime between '" + fromdate + "' and '" + todate + "' GROUP BY total_bill.order_id, table_order.t_id, table_order.discAmt,table_order.gst,status,remark order by  total_bill.order_id  asc");
            dgv.DataSource = db.Displaygrid("SELECT   total_bill.order_id AS[Bill No.], table_order.t_id as [Table No], sum(total_bill.Total_bill) AS[Total Amount ], (table_order.foodDiscAmt+table_order.beveragesDiscAmt+table_order.liquorDiscAmt) AS [Discount Amt],(table_order.gst/2) as[CGST] ,(table_order.gst/2) AS [SGST],status,total_bill.remark as Remark FROM            total_bill INNER JOIN   table_order ON total_bill.order_id = table_order.order_id where datetime between '" + fromdate + "' and '" + todate + "'  and total_bill.order_id <>'200'  GROUP BY total_bill.order_id, table_order.t_id, table_order.foodDiscAmt,table_order.beveragesDiscAmt,table_order.liquorDiscAmt,table_order.gst,status,total_bill.remark order by  total_bill.order_id  asc");
          
            
            assignCellColor();
           // dataGridView1.DataSource = db.Displaygrid("SELECT   status as[Status], SUM(Total_bill) AS Total FROM  total_bill  where datetime between '" + fromdate + "' and '" + todate + "' GROUP BY status");
            dataGridView1.DataSource = db.Displaygrid("SELECT   status as[Status], SUM(Total_bill) AS Total FROM  total_bill  where ( datetime between '" + fromdate + "' and '" + todate + "'  )    and  order_id <>'200'   GROUP BY status");
        
            
       
            dgv.Columns[5].DefaultCellStyle.Format = "N2";
            dgv.Columns[4].DefaultCellStyle.Format = "N2";

            DVGcreditRec.DataSource = db.Displaygrid("select c.name as ComanyName,sum(tb.Total_bill) as AMT , tb.status from total_bill tb inner join Custmer c on tb.Customer_id=c.Customer_id and tb.Customer_id<>0 and tb.order_id='200' and tb.datetime between '" + fromdate + "' and '" + todate + "' group by c.name ,tb.status");

            cal();
            gridCell();
        }
        private void dtp2_ValueChanged(object sender, EventArgs e)
        {
            datechange();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            int start, end;
            string qur = "";
            double creditamt = 0;
            LPrinter lp = new LPrinter();
            dateFrm = dtp1.Value.ToString("dd-MM-yy");
            dateTo = dtp2.Value.ToString("dd-MM-yy");
            lp.GrandTot = float.Parse(txt_total.Text);
            lp.discAmt = float.Parse(txtDiscAmt.Text.ToString());
            lp.Cgstamt = float.Parse(txtCgst.Text);
            lp.Sgstamt = float.Parse(txtSgst.Text);

            for (int i = 0; i < DVGcreditRec.Rows.Count; i++)
            {
                creditamt += Convert.ToDouble(DVGcreditRec.Rows[i].Cells[1].Value);
            }
            lp.creditrec = creditamt;
           // lp.TotalAmt = float.Parse(lblGrand.Text.ToString());

            if (db.ChkDb_Value("select * from tbl_option where grp='PrinterName' and value='Continuous Print'"))
            {
                lp.print_todaysCounter(dateFrm, dateTo, dataGridView1);
            }
            else
            {
                /*** new code for today counter start**/
               
                
                
                
                int TotalRecords = int.Parse(db.getDb_Value("select count(*) from total_bill").ToString());

                if (TotalRecords>=45)
                {
                int counterLoops = 1;
                counterLoops = TotalRecords / 45;
                if (TotalRecords % 45 != 0)
                {
                    counterLoops++;
                }


                start = 0;
                end = 45;
                for (int j = 1; j <= counterLoops; j++)
                {
                    if (j == counterLoops)
                    {
                        lp.grupwseflg = true;
                    }
                    //datetime='" + date + "' and
                    qur = " SELECT   total_bill.order_id AS[Bill No.], table_order.t_id as [Table No], total_bill.Total_bill AS[Total Amount],status as [Payment Mode] FROM            total_bill INNER JOIN   table_order ON total_bill.order_id = table_order.order_id where  total_bill.order_id between '" + start + "' and '" + end + "'  AND (total_bill.datetime BETWEEN '" + Convert.ToDateTime(dateFrm).ToString("MM/dd/yyyy") + "' AND '" + Convert.ToDateTime(dateTo).ToString("MM/dd/yyyy") + "') order by total_bill.order_id";

                    lp.print_todaysCounterEpson(qur, dateFrm, dateTo, dataGridView1);
                    start = end + 1;
                    end = start + 45;
                }
                }
                else
                {
                    start = 0;
                    end = TotalRecords;
                    lp.grupwseflg = true;
                    qur = " SELECT   total_bill.order_id AS[Bill No.], table_order.t_id as [Table No], total_bill.Total_bill AS[Total Amount],status as [Payment Mode] FROM            total_bill INNER JOIN   table_order ON total_bill.order_id = table_order.order_id where  total_bill.order_id between '" + start + "' and '" + end + "'  AND (total_bill.datetime BETWEEN '" + Convert.ToDateTime(dateFrm).ToString("MM/dd/yyyy") + "' AND '" + Convert.ToDateTime(dateTo).ToString("MM/dd/yyyy") + "') order by total_bill.order_id";

                    lp.print_todaysCounterEpson(qur, dateFrm, dateTo, dataGridView1);
          

                }

                /**code end here**/
            }
        }

        private void dgv_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                int i = dgv.SelectedCells[0].RowIndex;
                int billNo = int.Parse(dgv.Rows[i].Cells[0].Value.ToString());
                Bill_Print _editbill = new Bill_Print(billNo);
                _editbill.ShowDialog();
                pageLoad();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
     
        private void dgv_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                int i = dgv.SelectedCells[0].RowIndex;
                string billNo = dgv.Rows[i].Cells[0].Value.ToString();
              
                if (db.ChkDb_Value("SELECT order_id FROM total_bill GROUP BY order_id HAVING (COUNT(order_id) > 1) and order_id='" + billNo + "'"))
                {
                    panelDetails.Visible = true;
                    txtCash.Text = db.getDb_Value("select Total_bill from total_bill where order_id='" + billNo + "' and status='Cash'").ToString();
                    if (db.ChkDb_Value("select Total_bill from total_bill where order_id='" + billNo + "' and status='Card'"))
                    {
                        lblcard.Visible = true;
                        lblpayTm.Visible = false;
                        txtCard.Text = db.getDb_Value("select Total_bill from total_bill where order_id='" + billNo + "' and status='Card'").ToString();
                        txtTransactionID.Text = db.getDbstatus_Value("select transaction_id from total_bill where order_id='" + billNo + "' and status='Card'");
                    }
                    if (db.ChkDb_Value("select Total_bill from total_bill where order_id='" + billNo + "' and status='Paytm'"))
                    {
                        lblcard.Visible = false;
                        lblpayTm.Visible = true;
                        txtCard.Text = db.getDb_Value("select Total_bill from total_bill where order_id='" + billNo + "' and status='Paytm'").ToString();
                        txtTransactionID.Text = db.getDbstatus_Value("select transaction_id from total_bill where order_id='" + billNo + "' and status='Paytm'");
                    }
                }
                else
                {
                    panelDetails.Visible = false;
                    txtCash.Text = "0";
                    txtCard.Text = "0";
                    txtTransactionID.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvcreditpay_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btn_dailyReport_Click(object sender, EventArgs e)
        {
            LPrinter lp = new LPrinter();

            dateFrm = dtp1.Value.ToString("dd-MM-yy");
            dateTo = dtp2.Value.ToString("dd-MM-yy");


            string date1 = db.getDbstatus_Value("select ddate from tbl_dayend_status");
            DateTime dt = Convert.ToDateTime(date1);
            string date = dt.ToString("MM/dd/yyyy");
            string dt1 = dt.ToString("dd-MM-yyyy");



            lp.card = card.ToString();
            lp.cash = cash.ToString();


            lp.dailySummeryReport(dateFrm, dateTo);
          
        }

        private void txtSgst_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {









        }
    }
}
