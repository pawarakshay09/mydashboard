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
    public partial class StockRemainder : Form
    {
        string materialName;
        float stockQty;
        Database db = new Database();
        public StockRemainder()
        {
            InitializeComponent();
        }

        private void StockRemainder_Load(object sender, EventArgs e)
        {
            bind_Dg();
            db.formFix(this);
            this.CancelButton = btn_close;
        }
        void bind_Dg()
        {

            dataGridView1.DataSource = db.Displaygrid("SELECT        tbl_stock.item_name as [Material Name], tbl_stock.qty as Qty, tbl_stock.unit as Unit, material_nm.minimum_stock as [Minimum Stock] FROM            tbl_stock INNER JOIN      material_nm ON tbl_stock.item_name = material_nm.material_nm");
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                materialName = dataGridView1.Rows[i].Cells[0].Value.ToString();
                //if(db.ChkDb_Value("select qty from tbl_stock where item_name='" + materialName + "'"))
                //    stockQty = db.getDb_Value("select qty from tbl_stock where item_name='" + materialName + "'");
                //if (db.ChkDb_Value("select * from material_nm where material_nm='" + materialName + "' and  minimum_stock='" + stockQty + "'"))
                //{
                //    MessageBox.Show(""+materialName+" stock is finished");
                //}


                 if (db.ChkDb_Value("select minimum_stock from material_nm where material_nm='" + materialName + "'"))
                    stockQty = db.getDb_Value("select minimum_stock from material_nm where material_nm='" + materialName + "'");

                if (db.ChkDb_Value("select * from tbl_stock where item_name='" + materialName + "' and  qty<='" + stockQty + "'")) 
                {
                     MessageBox.Show(""+materialName+" stock is finished");
                }
            }
           

        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
