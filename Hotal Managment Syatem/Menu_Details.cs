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

namespace Hotal_Managment_Syatem
{
    public partial class Menu_Details : Form
    {
        Database db = new Database();
        string datefrm, dateto;

        int i;
        public string date1, date2;
        public Menu_Details()
        {
            InitializeComponent();
        }

        private void Menu_Details_Load(object sender, EventArgs e)
        {

            db.formFix(this);
            this.CancelButton = btn_close;
            datechange();

        }

        private void DTP1_ValueChanged(object sender, EventArgs e)
        {

            datechange();
        }

        private void DTP2_ValueChanged(object sender, EventArgs e)
        {
            datechange();

        }
        void datechange()
        {
            datefrm = DTP1.Value.ToString("MM'-'dd'-'yyyy");
            dateto = DTP2.Value.ToString("MM'-'dd'-'yyyy");
            //  dataGridView1.DataSource = db.Displaygrid("SELECT        menu.m_name AS Menu_Name, SUM(sales_item.qty) AS Qty, SUM(sales_item.total_amount) AS Amount FROM            sales_item INNER JOIN   menu ON sales_item.menu_id = menu.menu_id INNER JOIN   table_order ON sales_item.order_id = table_order.order_id WHERE    timeing between '" + datefrm + "' and  '" + dateto + "' and    (sales_item.order_id  IN  (SELECT        order_id    FROM            total_bill)) GROUP BY menu.m_name");
            dataGridView1.DataSource = db.Displaygrid("SELECT        menu.m_name AS [Menu Name], SUM(sales_item.qty) AS Qty, SUM(sales_item.total_amount) AS Amount FROM            sales_item INNER JOIN   menu ON sales_item.menu_id = menu.menu_id INNER JOIN( select distinct order_id,datetime from total_bill) tob    ON sales_item.order_id = tob.order_id INNER JOIN  category ON menu.category = category.cat_name WHERE        (tob.datetime BETWEEN '" + datefrm + "' and  '" + dateto + "') GROUP BY menu.m_name order by menu.m_name ");
            dataGridView1.Columns[0].Width = 200;
            dataGridView1.Columns[1].Width = 90;
            cal();
        }
        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void cal()
        {
            double sum = 0;
            for (i = 0; i < dataGridView1.RowCount; i++)
            {
                sum += double.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString());
            }
            txt_total.Text = sum.ToString();
            //lblAppliedDisc.Text = db.getDb_Value("SELECT        SUM(table_order.discAmt) AS Expr1 FROM            total_bill INNER JOIN   table_order ON total_bill.order_id = table_order.order_id where  (total_bill.datetime between '" + datefrm + "' and '" + dateto + "')").ToString();
            //lblGrand.Text =Math.Round(float.Parse(txt_total.Text) - float.Parse(lblAppliedDisc.Text)).ToString();


            //lblAppliedDisc.Text = db.getDb_Value("SELECT        SUM(table_order.discAmt) AS Expr1 FROM            total_bill INNER JOIN   table_order ON total_bill.order_id = table_order.order_id where  (total_bill.datetime between '" + datefrm + "' and '" + dateto + "')").ToString();
            //lblGrand.Text = db.getDb_Value("select sum(Total_bill) from total_bill").ToString();//Math.Round(float.Parse(txt_total.Text) - float.Parse(lblAppliedDisc.Text)).ToString();
            //txt_total.Text = Math.Round(float.Parse(lblGrand.Text) + float.Parse(lblAppliedDisc.Text)).ToString();//sum.ToString();

        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            int start, end;
            string qur = "";
            string datefrm = DTP1.Value.ToString("dd'-'MM'-'yyyy");
            string dateto = DTP2.Value.ToString("dd'-'MM'-'yyyy");
            DialogResult dlgresultF_kot = MessageBox.Show("Do You want to Print this Report ?", "Printing Report", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlgresultF_kot == DialogResult.Yes)
            {
                LPrinter lp = new LPrinter();

                lp.salesto = txt_total.Text;
                //if (db.ChkDb_Value("select * from tbl_option where grp='PrinterName' and value='Continuous Print'"))
                //{
                //    if (panelDiscShow.Visible == true)
                //    {
                //        lp.GrandTot = float.Parse(txt_total.Text.ToString());
                //        lp.discAmt = float.Parse(lblAppliedDisc.Text.ToString());
                //        lp.TotalAmt = float.Parse(lblGrand.Text.ToString());
                //    }
                //    else
                //    {
                //        lp.GrandTot = float.Parse(txt_total.Text.ToString());
                //        lp.discAmt = 0;
                //        lp.TotalAmt = float.Parse(txt_total.Text.ToString());
                //        lblGrand.Text = txt_total.Text;
                //    }
                //    // lp.print_report(datefrm,dateto);
                //    string qry = "";//"SELECT        menu.m_name AS Menu_Name, SUM(sales_item.qty) AS qty FROM            sales_item INNER JOIN   menu ON sales_item.menu_id = menu.menu_id INNER JOIN   table_order ON sales_item.order_id = table_order.order_id INNER JOIN   total_bill ON table_order.order_id = total_bill.order_id WHERE    (total_bill.datetime between  '" + Convert.ToDateTime(rptDateFrom).ToString("MM/dd/yyyy") + "' and '" + Convert.ToDateTime(rptDateTO).ToString("MM/dd/yyyy") + "') and  (table_order.t_id NOT LIKE 'P%') GROUP BY menu.m_name";

                //    if (cmb_type.Text == "Menu")
                //    {
                //        //qry = "SELECT        menu.m_name AS Menu_Name, SUM(sales_item.qty) AS Qty, SUM(sales_item.total_amount) AS Amount FROM            sales_item INNER JOIN  menu ON sales_item.menu_id = menu.menu_id INNER JOIN   table_order ON sales_item.order_id = table_order.order_id  INNER JOIN  category ON menu.category = category.cat_name WHERE        (category.type = 'Food') AND (table_order.timeing BETWEEN '" + Convert.ToDateTime(datefrm).ToString("MM/dd/yyyy") + "' AND '" + Convert.ToDateTime(dateto).ToString("MM/dd/yyyy") + "') and    (sales_item.order_id  IN  (SELECT        order_id    FROM            total_bill)) GROUP BY menu.m_name";
                //        qry = "SELECT        menu.m_name AS [Menu Name], SUM(sales_item.qty) AS Qty, SUM(sales_item.total_amount) AS Amount FROM            sales_item INNER JOIN   menu ON sales_item.menu_id = menu.menu_id INNER JOIN    total_bill ON sales_item.order_id = total_bill.order_id INNER JOIN  category ON menu.category = category.cat_name WHERE      (category.cat_name <> 'Hard Drinks') AND   (total_bill.datetime BETWEEN '" + Convert.ToDateTime(datefrm).ToString("MM/dd/yyyy") + "' and  '" + Convert.ToDateTime(dateto).ToString("MM/dd/yyyy") + "') GROUP BY menu.m_name";
                          
                //    }
                //    else if (cmb_type.Text == "Drink")
                //    {
                //        // qry = "SELECT        menu.m_name AS Menu_Name, SUM(sales_item.qty) AS Qty, SUM(sales_item.total_amount) AS Amount FROM            sales_item INNER JOIN  menu ON sales_item.menu_id = menu.menu_id INNER JOIN   table_order ON sales_item.order_id = table_order.order_id INNER JOIN  category ON menu.category = category.cat_name  WHERE        (menu.category = 'Drink') AND (table_order.timeing BETWEEN '" + Convert.ToDateTime(datefrm).ToString("MM/dd/yyyy") + "' AND '" + Convert.ToDateTime(dateto).ToString("MM/dd/yyyy") + "') and    (sales_item.order_id  IN  (SELECT        order_id    FROM            total_bill))GROUP BY menu.m_name";
                //        qry = "SELECT        menu.m_name AS [Menu Name], SUM(sales_item.qty) AS Qty, SUM(sales_item.total_amount) AS Amount FROM            sales_item INNER JOIN   menu ON sales_item.menu_id = menu.menu_id INNER JOIN    total_bill ON sales_item.order_id = total_bill.order_id INNER JOIN  category ON menu.category = category.cat_name WHERE    (category.cat_name = 'Hard Drinks') AND    (total_bill.datetime BETWEEN '" + Convert.ToDateTime(datefrm).ToString("MM/dd/yyyy") + "' and  '" + Convert.ToDateTime(dateto).ToString("MM/dd/yyyy") + "') GROUP BY menu.m_name";
                //    }
                //    else
                //    {
                //        // qry = "SELECT        menu.m_name AS Menu_Name, SUM(sales_item.qty) AS Qty, SUM(sales_item.total_amount) AS Amount FROM            sales_item INNER JOIN  menu ON sales_item.menu_id = menu.menu_id INNER JOIN   table_order ON sales_item.order_id = table_order.order_id INNER JOIN  category ON menu.category = category.cat_name WHERE   (table_order.timeing BETWEEN '" + Convert.ToDateTime(datefrm).ToString("MM/dd/yyyy") + "' AND '" + Convert.ToDateTime(dateto).ToString("MM/dd/yyyy") + "') and    (sales_item.order_id  IN  (SELECT        order_id    FROM            total_bill)) GROUP BY menu.m_name";
                //        qry = "SELECT        menu.m_name AS [Menu Name], SUM(sales_item.qty) AS Qty, SUM(sales_item.total_amount) AS Amount FROM            sales_item INNER JOIN   menu ON sales_item.menu_id = menu.menu_id INNER JOIN    total_bill ON sales_item.order_id = total_bill.order_id INNER JOIN  category ON menu.category = category.cat_name WHERE        (total_bill.datetime BETWEEN '" + Convert.ToDateTime(datefrm).ToString("MM/dd/yyyy") + "' and  '" + Convert.ToDateTime(dateto).ToString("MM/dd/yyyy") + "') GROUP BY menu.m_name";
                //        lp.insentiveFlag = false;
                //    }

                //    // lp.print_report(datefrm,dateto);
                //    lp.print_report(qry, lblGrand.Text, datefrm, dateto, cmb_type.Text);
                //}
                //else
                //{
                    //// print();
                    int TotalRecords = 0;//int.Parse(db.getDb_Value(" SELECT        COUNT(*) AS total FROM            (SELECT        COUNT(*) AS Expr1     FROM            sales_item INNER JOIN  menu ON sales_item.menu_id = menu.menu_id INNER JOIN     total_bill ON sales_item.order_id = total_bill.order_id       GROUP BY menu.m_name) AS total").ToString());
                    //// int TotalRecords = dataGridView1.Rows.Count;
                    //if (cmb_type.Text == "Menu")
                    //    TotalRecords = int.Parse(db.getDb_Value(" SELECT        COUNT(*) AS total FROM            (SELECT        COUNT(*) AS Expr1     FROM            sales_item INNER JOIN  menu ON sales_item.menu_id = menu.menu_id INNER JOIN     total_bill ON sales_item.order_id = total_bill.order_id  WHERE        (menu.category <> 'Hard Drinks')      GROUP BY menu.m_name) AS total").ToString());
                    //else if (cmb_type.Text == "Drink")
                    //    TotalRecords = int.Parse(db.getDb_Value(" SELECT        COUNT(*) AS total FROM            (SELECT        COUNT(*) AS Expr1     FROM            sales_item INNER JOIN  menu ON sales_item.menu_id = menu.menu_id INNER JOIN     total_bill ON sales_item.order_id = total_bill.order_id   WHERE        (menu.category = 'Hard Drinks')    GROUP BY menu.m_name) AS total").ToString());
                    //else
                    //    TotalRecords = int.Parse(db.getDb_Value(" SELECT        COUNT(*) AS total FROM            (SELECT        COUNT(*) AS Expr1     FROM            sales_item INNER JOIN  menu ON sales_item.menu_id = menu.menu_id INNER JOIN     total_bill ON sales_item.order_id = total_bill.order_id       GROUP BY menu.m_name) AS total").ToString());
                   

                TotalRecords =dataGridView1.RowCount;

                    int counterLoops = 1;
                    if (TotalRecords >= 45)
                    {
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
                                lp.flgSales = true;
                            }
                         
                            //if (cmb_type.Text == "Menu")
                            //    qur = "SELECT        View_menuDtl.order_id, View_menuDtl.Menu_Name, View_menuDtl.Qty, View_menuDtl.Total, View_menuDtl.Date, menu.category  FROM            View_menuDtl INNER JOIN                           menu ON View_menuDtl.Menu_Name = menu.m_name  WHERE        (View_menuDtl.order_id BETWEEN '" + start + "' and '" + end + "') AND (View_menuDtl.Date BETWEEN '02/05/2018' AND '02/05/2018') AND (menu.category <> 'Hard Drinks')";
                            //else if (cmb_type.Text == "Drink")
                            //    qur = "SELECT        View_menuDtl.order_id, View_menuDtl.Menu_Name, View_menuDtl.Qty, View_menuDtl.Total, View_menuDtl.Date, menu.category  FROM            View_menuDtl INNER JOIN                           menu ON View_menuDtl.Menu_Name = menu.m_name  WHERE        (View_menuDtl.order_id BETWEEN '" + start + "' and '" + end + "') AND (View_menuDtl.Date BETWEEN '02/05/2018' AND '02/05/2018') AND (menu.category = 'Hard Drinks')";
                            //else
                            //    qur = "SELECT   *  FROM View_menuDtl WHERE order_id between '" + start + "' and '" + end + "' and (Date BETWEEN '" + Convert.ToDateTime(datefrm).ToString("MM/dd/yyyy") + "' AND '" + Convert.ToDateTime(dateto).ToString("MM/dd/yyyy") + "')";

                           lp.print_reportEpson(qur, datefrm, dateto,dataGridView1,start,end);
                            start = end ;
                            end = start + 45;

                            if (end >= TotalRecords)
                                end = TotalRecords;




                        }
                    }
                    else
                    {
                        start = 0;
                        end = TotalRecords;
                        lp.flgSales = true;
                    //    if (cmb_type.Text == "Menu")
                    //        qur = "SELECT   menu.m_name as Menu_Name, SUM(sales_item.qty) AS Qty ,SUM(sales_item.total_amount) as Total FROM sales_item INNER JOIN  menu ON sales_item.menu_id = menu.menu_id INNER JOIN   total_bill ON sales_item.order_id = total_bill.order_id WHERE (total_bill.datetime BETWEEN '" + Convert.ToDateTime(datefrm).ToString("MM/dd/yyyy") + "' AND '" + Convert.ToDateTime(dateto).ToString("MM/dd/yyyy") + "') AND (menu.category <> 'Hard Drinks') GROUP BY menu.m_name";                       
                    //    else if (cmb_type.Text == "Drink")
                    //        qur = "SELECT   menu.m_name as Menu_Name, SUM(sales_item.qty) AS Qty ,SUM(sales_item.total_amount) as Total FROM sales_item INNER JOIN  menu ON sales_item.menu_id = menu.menu_id INNER JOIN   total_bill ON sales_item.order_id = total_bill.order_id WHERE (total_bill.datetime BETWEEN '" + Convert.ToDateTime(datefrm).ToString("MM/dd/yyyy") + "' AND '" + Convert.ToDateTime(dateto).ToString("MM/dd/yyyy") + "') AND (menu.category = 'Hard Drinks') GROUP BY menu.m_name";                        
                    //    else
                    //        qur = "SELECT   menu.m_name as Menu_Name, SUM(sales_item.qty) AS Qty ,SUM(sales_item.total_amount) as Total FROM sales_item INNER JOIN  menu ON sales_item.menu_id = menu.menu_id INNER JOIN   total_bill ON sales_item.order_id = total_bill.order_id WHERE (total_bill.datetime BETWEEN '" + Convert.ToDateTime(datefrm).ToString("MM/dd/yyyy") + "' AND '" + Convert.ToDateTime(dateto).ToString("MM/dd/yyyy") + "') GROUP BY menu.m_name";
                     lp.print_reportEpson(qur, datefrm, dateto,dataGridView1,start,end);
                    }

                }

            //}
        }

        void print()
        {
            // ------if Dayend Changes Only Date and Order id Start from 1 --------------------------------------------------

            // int TotalRecords = int.Parse(db.getDb_Value(" SELECT        COUNT(*) AS total FROM            (SELECT        COUNT(*) AS Expr1     FROM            sales_item INNER JOIN  menu ON sales_item.menu_id = menu.menu_id INNER JOIN     total_bill ON sales_item.order_id = total_bill.order_id   WHERE        (total_bill.datetime BETWEEN '" + Convert.ToDateTime(datefrm).ToString("MM/dd/yyyy") + "' AND '" + Convert.ToDateTime(dateto).ToString("MM/dd/yyyy") + "')     GROUP BY menu.m_name, total_bill.datetime) AS total").ToString());
            //int counterLoops = 1;
            //if (TotalRecords >= 45)
            //{
            //    counterLoops = TotalRecords / 45;
            //    if (TotalRecords % 45 != 0)
            //    {
            //        counterLoops++;
            //    }


            //    start = 0;
            //    end = 45;
            //    for (int j = 1; j <= counterLoops; j++)
            //    {

            //        db.DeleteData("truncate table salesItemPrint", "salesItemPrint");  //  imp note : Add the table created on Gajant Hotel Database
            //        db.insert("insert into salesItemPrint(menuName,qty,amount) SELECT  menu.m_name AS Menu_Name, SUM(sales_item.qty) AS Qty, SUM(sales_item.total_amount) AS Total FROM            sales_item INNER JOIN  menu ON sales_item.menu_id = menu.menu_id INNER JOIN   total_bill ON sales_item.order_id = total_bill.order_id WHERE        (total_bill.datetime BETWEEN '" + Convert.ToDateTime(datefrm).ToString("MM/dd/yyyy") + "' AND '" + Convert.ToDateTime(dateto).ToString("MM/dd/yyyy") + "') GROUP BY menu.m_name");
            //        // qur = "SELECT  ROW_NUMBER() OVER (ORDER BY menu.menu_id DESC) AS order_id,  dbo.menu.m_name AS Menu_Name, SUM(dbo.sales_item.qty) AS Qty, SUM(dbo.sales_item.total_amount) AS Total,  dbo.total_bill.datetime AS Date FROM            dbo.sales_item INNER JOIN   dbo.menu ON dbo.sales_item.menu_id = dbo.menu.menu_id INNER JOIN  dbo.total_bill ON dbo.sales_item.order_id = dbo.total_bill.order_id INNER JOIN   dbo.table_order ON dbo.sales_item.order_id = dbo.table_order.order_id WHERE menu.menu_id between '" + start + "' and '" + end + "' and (datetime BETWEEN '" + Convert.ToDateTime(datefrm).ToString("MM/dd/yyyy") + "' AND '" + Convert.ToDateTime(dateto).ToString("MM/dd/yyyy") + "') GROUP BY dbo.menu.m_name, dbo.menu.menu_id, dbo.total_bill.datetime, dbo.table_order.newOrderId";
            //        qur = "select menuName as Menu_Name,qty,amount as Total from salesItemPrint where tempId between '" + start + "' and '" + end + "'";
            //        lp.print_report(qur, datefrm, dateto);
            //        start = end + 1;
            //        end = start + 45;
            //    }
            //}
            //else
            //{
            //    qur = "SELECT   menu.m_name as Menu_Name, SUM(sales_item.qty) AS Qty ,SUM(sales_item.total_amount) as Total FROM sales_item INNER JOIN  menu ON sales_item.menu_id = menu.menu_id INNER JOIN   total_bill ON sales_item.order_id = total_bill.order_id WHERE (total_bill.datetime BETWEEN '" + Convert.ToDateTime(datefrm).ToString("MM/dd/yyyy") + "' AND '" + Convert.ToDateTime(dateto).ToString("MM/dd/yyyy") + "') GROUP BY menu.m_name";
            //    lp.print_report(qur, datefrm, dateto);
            //}

        }

        private void cmb_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            string datefrm = DTP1.Value.ToString("MM'-'dd'-'yyyy");
            string dateto = DTP2.Value.ToString("MM'-'dd'-'yyyy");
            if (cmb_type.Text == "Menu")
            {
                dataGridView1.DataSource = db.Displaygrid("SELECT        menu.m_name AS [Menu Name], SUM(sales_item.qty) AS Qty, SUM(sales_item.total_amount) AS Amount FROM            sales_item INNER JOIN   menu ON sales_item.menu_id = menu.menu_id INNER JOIN  ( select distinct order_id, datetime from  total_bill ) tob ON sales_item.order_id = tob.order_id INNER JOIN  category ON menu.category = category.cat_name WHERE      (category.cat_name <> 'Hard Drinks') AND   (tob.datetime BETWEEN '" + datefrm + "' and  '" + dateto + "') GROUP BY menu.m_name order by menu.m_name ");

                dataGridView1.Columns[0].Width = 200;
                dataGridView1.Columns[1].Width = 90;
                panelDiscShow.Visible = false;
                lblAppliedDisc.Text = "0";
                lblGrand.Text = txt_total.Text;
            }
            else if (cmb_type.Text == "Drink")
            {
                dataGridView1.DataSource = db.Displaygrid("SELECT        menu.m_name AS [Menu Name], SUM(sales_item.qty) AS Qty, SUM(sales_item.total_amount) AS Amount FROM            sales_item INNER JOIN   menu ON sales_item.menu_id = menu.menu_id INNER JOIN  ( select distinct order_id, datetime from   total_bill ) tob ON sales_item.order_id = tob.order_id INNER JOIN  category ON menu.category = category.cat_name WHERE    (category.cat_name = 'Hard Drinks') AND    (tob.datetime BETWEEN '" + datefrm + "' and  '" + dateto + "') GROUP BY menu.m_name order by menu.m_name ");

                dataGridView1.Columns[0].Width = 200;
                dataGridView1.Columns[1].Width = 90;
                panelDiscShow.Visible = false;
                lblAppliedDisc.Text = "0";
                lblGrand.Text = txt_total.Text;
            }
            else
            {
                dataGridView1.DataSource = db.Displaygrid("SELECT        menu.m_name AS [Menu Name], SUM(sales_item.qty) AS Qty, SUM(sales_item.total_amount) AS Amount FROM            sales_item INNER JOIN   menu ON sales_item.menu_id = menu.menu_id INNER JOIN   ( select distinct order_id, datetime from   total_bill ) tob  ON sales_item.order_id = tob.order_id INNER JOIN  category ON menu.category = category.cat_name WHERE    (tob.datetime BETWEEN '" + datefrm + "' and  '" + dateto + "') GROUP BY menu.m_name order by menu.m_name");

                dataGridView1.Columns[0].Width = 200;
                dataGridView1.Columns[1].Width = 90;
                //  panelDiscShow.Visible = true;

            }
            cal();
        }
    }
}
