using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Hotal_Managment_Syatem
{
    public partial class Issue_Kitchen_Details : Form
    {
        Database db = new Database();
        public Issue_Kitchen_Details()
        {
            InitializeComponent();
        }

        private void Issue_Kitchen_Details_Load(object sender, EventArgs e)
        {
            db.formFix(this);
            this.CancelButton = btn_close;
            bind();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            bind();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            bind();
        }
        void bind()
        {
            string datefrm = dateTimePicker1.Value.ToString("MM'-'dd'-'yyyy");
            string dateto = dateTimePicker2.Value.ToString("MM'-'dd'-'yyyy");
            dataGridView1.DataSource = db.Displaygrid("select material_nm as[Material Name],qty as Qty,issue_by as [Issue By] from Issue_Kitchen where date between '" + datefrm + "' and '" + dateto + "'");
            cal();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void cal()
        {
            double sum = 0;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                sum += double.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString());
            }
            txt_total.Text = sum.ToString();
        }
        private void btn_export_Click(object sender, EventArgs e)
        {
            try
            {

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Excel Documents (*.xls)|*.xls";
                sfd.FileName = "IssueKitchenReport.xls";
               
                if (sfd.ShowDialog() == DialogResult.OK)
                {

                    //ToCsV(dataGridView1, @"c:\export.xls";
                    db.ToCsV(dataGridView1, sfd.FileName); // Here dataGridview1 is your grid view name 
                    MessageBox.Show("File Sucessfully Expoted");
                    Process.Start(sfd.FileName);


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
