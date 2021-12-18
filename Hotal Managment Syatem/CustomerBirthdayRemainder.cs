using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Data.SqlClient;

namespace Hotal_Managment_Syatem
{
    public partial class customerBirthdayRemainder : Form
    {
        Database db = new Database();
        string currentTime ;
        string materialName;
        float stockQty;
        public customerBirthdayRemainder()
        {
            InitializeComponent();
        }

        private void customerBirthdayRemainder_Load(object sender, EventArgs e)
        {
            // Birthday Reminder
                  bind_Dg();
                  if (dgv_birthday.Rows.Count < 1)
                  {
                      panel_birthday.Visible = false;
                      if (panel_birthday.Visible == false)
                      {
                          btn_close.Location = new Point(200, 250);
                          // customerBirthdayRemainder custReminder = new customerBirthdayRemainder();
                          this.Size = new Size(354, 300);
                      }
                  }
                // db.formFix(this);
                 this.CancelButton = btn_close;
                  
            // stock Reminder
                 bind_Dg_stock();
                 if (dgv_stock.Rows.Count < 1)
                 {
                     panelStock.Visible = false;
                 }
                 if (panel_birthday.Visible == false)
                 {
                     panelStock.Location = new Point(14, 46);
                     btn_close.Location = new Point(200, 250);
                    // customerBirthdayRemainder custReminder = new customerBirthdayRemainder();
                     this.Size = new Size(354, 300);

                 }

                // db.formFix(this);
                 this.CancelButton = btn_close;
        }
        void bind_Dg()
        {
            DateTime dtmain = System.DateTime.Now;
            string date = Convert.ToDateTime(dtmain).ToString("MM-dd-yyyy");
            dgv_birthday.DataSource = db.Displaygrid("select name as [Customer Name],phone as [Contact No] from Custmer where dateOfBirth = '" + date + "'");
           dgv_birthday.Columns[0].Width = 180;

        }

        void bind_Dg_stock()
        {


            for (int i = 0; i < dgv_stock.RowCount; i++)
            {
                materialName = dgv_stock.Rows[i].Cells[0].Value.ToString();
               
                if (db.ChkDb_Value("select minimum_stock from material_nm where material_nm='" + materialName + "'"))
                    stockQty = db.getDb_Value("select minimum_stock from material_nm where material_nm='" + materialName + "'");

                if (db.ChkDb_Value("select * from tbl_stock where item_name='" + materialName + "' and  qty<='" + stockQty + "'"))
                {
                    MessageBox.Show("" + materialName + " stock is finished");
                }
                dgv_stock.DataSource = db.Displaygrid("select item_name as [Material Name],qty as Qty from tbl_stock where item_name='" + materialName + "' and  qty<='" + stockQty + "'");

            }


        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
