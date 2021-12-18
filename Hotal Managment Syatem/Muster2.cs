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
    public partial class Muster2 : Form
    {
        SqlConnection con = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        Database db = new Database();
        string day, name;
       
        public Muster2()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {            
            for(int i=0;i<dataGridView1.Rows.Count;i++)
            {
                DataGridViewCheckBoxCell chkcell0 = (DataGridViewCheckBoxCell)dataGridView1.Rows[i].Cells[0];
                DataGridViewCheckBoxCell chkcell1 = (DataGridViewCheckBoxCell)dataGridView1.Rows[i].Cells[1];

                if(dataGridView1.Rows[i].Cells[0].Selected == true && dataGridView1.Rows[i].Cells[1].Selected == true)// chk both box
                {
                    MessageBox.Show("Cannot check both checkbox");
                    dataGridView1.Rows[i].Cells[2].Value = "";
                    dataGridView1.Rows[i].Cells[2].Value = "";
                }
                if (dataGridView1.Rows[i].Cells[0].Selected == true)
                {
                    dataGridView1.Rows[i].Cells[2].Value = "Present";
                    day = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    name = dataGridView1.Rows[i].Cells[3].Value.ToString();
                    chkcell1.Value = false;
                }
                else if (dataGridView1.Rows[i].Cells[1].Selected == true)
                {
                    dataGridView1.Rows[i].Cells[2].Value = "Absent";
                    day = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    name = dataGridView1.Rows[i].Cells[3].Value.ToString();
                    chkcell0.Value = false;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            db.formFix(this);
            this.CancelButton = btnclose;
             
            dataGridView1.DataSource = db.Displaygrid("select wname as [Waiter Name] from waiter_dtls");
            dataGridView1.Columns[3].Width = 165;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Cells[2].Value = "A";
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string date = System.DateTime.Now.ToString("MM'-'dd'-'yyyy");
            string w_name;
            float w_id;
            int k = dataGridView1.SelectedCells[0].RowIndex;
             w_name = dataGridView1.Rows[k].Cells[3].Value.ToString();
             w_id = db.getDb_Value("select w_id from waiter_dtls where wname='" + w_name + "'");

            if (!db.ChkDb_Value("select date from waiter_prsenty where date='" + date + "' and w_id='"+w_id+"'"))
            {
                for (int i = 0; i <= dataGridView1.RowCount - 1; i++)
                {
                    string dt = dateTimePicker1.Value.ToString("MM'-'dd'-'yyyy");
                     w_name = dataGridView1.Rows[i].Cells[3].Value.ToString();
                     w_id = db.getDb_Value("select w_id from waiter_dtls where wname='" + w_name + "'");
                    string status = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    if(status!="A")
                     db.insert("insert into waiter_prsenty values('" + w_id + "','" + dt + "','" + status + "','" + "0" + "','00:00:00','00:00:00')");

                }

                MessageBox.Show("Record saved Successfully");

            }
            else
            {
                MessageBox.Show("Presenty Fill Already!!!","Confirm",MessageBoxButtons.OK,MessageBoxIcon.Information);
                
            }

            clear();
        }
        void clear()
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                DataGridViewCheckBoxCell chkcell0 = (DataGridViewCheckBoxCell)dataGridView1.Rows[i].Cells[0];
                DataGridViewCheckBoxCell chkcell1 = (DataGridViewCheckBoxCell)dataGridView1.Rows[i].Cells[1];
              // DataGridViewCheckBoxCell chkcell2 = (DataGridViewCheckBoxCell)dataGridView1.Rows[i].Cells[2];
                chkcell0.Value = false;
                chkcell1.Value = false;
//chkcell2.Value = false;
            }

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Cells[2].Value = "A";
            }
        }

       

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();        
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            UpdatePresenty updatePresenty = new UpdatePresenty();
            updatePresenty.ShowDialog();
        }
    }
}
