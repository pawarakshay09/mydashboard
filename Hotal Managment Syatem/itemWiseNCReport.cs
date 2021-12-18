using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing.Printing;
using DbBackupDLL;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Diagnostics;


namespace Hotal_Managment_Syatem
{
    public partial class itemWiseNCReport : Form
    {
        Database db = new Database();
         DataTable dt = new DataTable();
        string datefrm, dateto;

        int i;
        public string date1, date2;
        public itemWiseNCReport()
        {
            InitializeComponent();
        }

        private void itemWiseNCReport_Load(object sender, EventArgs e)
        {
            get_rptlistcomb();
            db.formFix(this);
            this.CancelButton = btn_close;
            datechange();
        }

        void datechange()
        {
            datefrm = dtpFromDate.Value.ToString("MM'-'dd'-'yyyy");
            dateto = dtpToDate.Value.ToString("MM'-'dd'-'yyyy");
        //   dgvMenuReport.DataSource = db.Displaygrid(@"  SELECT  [orderId] as OrderID    ,[tblNo] as TableNo     ,[ncDate] as NC_Date      ,[ncTime] as NC_Time      ,[itemName] as Item      ,[rate]       ,[qty]      ,[amount]      ,[userName] as Given_by      ,[reason]       FROM  [NC_itemDetails]       WHERE        (ncDate BETWEEN '" + datefrm + "' and  '" + dateto + "')  order by [orderId]");
          
           
        }

        void BindReportGride()
        {
            //dynamic data bind creatd on date 09/02/2018 tejashri
            this.dgvMenuReport.Refresh();
            dt.Rows.Clear();
            dt.Columns.Clear();
            datefrm = dtpFromDate.Value.ToString("MM'-'dd'-'yyyy");
            dateto = dtpToDate.Value.ToString("MM'-'dd'-'yyyy");
            String rptname = rptlistcomb.SelectedValue.ToString();//get data from combo
            String Spname = db.getDbstatus_Value(@"select StoreProcedureName from Report_Master  where Report_Name='" + rptname + "'");
            String report_idspname = db.getDbstatus_Value(@"select Report_Id from Report_Master  where StoreProcedureName='" + Spname + "'");
            String report_idrname = db.getDbstatus_Value(@"select Report_Id from Report_Master  where Report_Name='" + rptname + "'");
            if (report_idspname.Equals(report_idrname))
            {

                SqlCommand com = new SqlCommand(Spname, db.cn);

                if (rptname.Equals("Cancel KOT Report"))// becouse date format different in cancel_kot table
                {
                    string datefrmm = dtpFromDate.Value.ToString("MM'/'dd'/'yyyy");
                    string datetom = dtpToDate.Value.ToString("MM'/'dd'/'yyyy");
                    com.Parameters.AddWithValue("@startdate", datefrmm);
                    com.Parameters.AddWithValue("@enddate", datetom);
                    com.CommandType = CommandType.StoredProcedure;

                }
                else
                {
                    com.Parameters.AddWithValue("@startdate", datefrm);
                    com.Parameters.AddWithValue("@enddate", dateto);
                    com.CommandType = CommandType.StoredProcedure;

                }

                SqlDataAdapter da = new SqlDataAdapter(com);

                try
                {
                    db.cnopen();
                    da.Fill(dt);

                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (db.cn.State == ConnectionState.Open)
                        db.cnclose();
                }

                dgvMenuReport.DataSource = dt;
            }
        }
        private void dtpFromDate_ValueChanged(object sender, EventArgs e)
        {
            datechange();
        }

        private void dtpToDate_ValueChanged(object sender, EventArgs e)
        {
            datechange();
        }

        private void btn_export_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime dt = DateTime.Now; //dynamic data Export creatd on date 09/02/2018 tejashri
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Excel Documents (*.xls)|*.xls";
                sfd.FileName = dt.ToString("dd-MM-yyyy") + ".xls";

                datefrm = dtpFromDate.Value.ToString("MM'-'dd'-'yyyy");
                dateto = dtpToDate.Value.ToString("MM'-'dd'-'yyyy");
                String rptname = rptlistcomb.SelectedValue.ToString();//get data from combo
                String Spname = db.getDbstatus_Value(@"select StoreProcedureName from Report_Master  where Report_Name='" + rptname + "'");
                String report_idspname = db.getDbstatus_Value(@"select Report_Id from Report_Master  where StoreProcedureName='" + Spname + "'");
                String report_idrname = db.getDbstatus_Value(@"select Report_Id from Report_Master  where Report_Name='" + rptname + "'");

                    if (report_idspname.Equals(report_idrname))
                    {
                        sfd.FileName = rptname + sfd.FileName;

                        if (sfd.ShowDialog() == DialogResult.OK)
                        {
                            db.withReportTitle_ToCsV_data(dgvMenuReport, sfd.FileName, rptname, datefrm, dateto);
                            MessageBox.Show(" Report Sucessfully Expoted");
                            Process.Start(sfd.FileName);
                        }
                    }
                


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("File Sucessfully Expoted");
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.dgvMenuReport.Refresh();
            this.dgvMenuReport.Parent.Refresh();

            BindReportGride();

        }

        private void rptlistcomb_SelectedIndexChanged(object sender, EventArgs e)
        {
               

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        void get_rptlistcomb()
        {
            rptlistcomb.Text = "";
            rptlistcomb.Text = "---Select any one---";
            db.comboFill(rptlistcomb, "select Report_Name from Report_Master  where Status ='Active' order by Report_Name asc", "Report_master", "Report_Name", "Report_Name");
        }
    }
}
