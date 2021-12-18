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
    public partial class EMPPRESENTY : Form
    {
        Database db = new Database();
        SqlConnection con = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
    
        string day, name;
        public EMPPRESENTY()
        {
            InitializeComponent();
        }

        private void EMPPRESENTY_Load(object sender, EventArgs e)
        {
            db.formFix(this);
            this.CancelButton = btnclose;

            dataGridView1.DataSource = db.Displaygrid("select wname as [EMP Name] from waiter_dtls");
            dataGridView1.Columns[5].Width = 165;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Cells[4].Value = "Absent";
            }
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                DataGridViewCheckBoxCell chkcell0 = (DataGridViewCheckBoxCell)dataGridView1.Rows[i].Cells[0];
                DataGridViewCheckBoxCell chkcell1 = (DataGridViewCheckBoxCell)dataGridView1.Rows[i].Cells[1];
                DataGridViewCheckBoxCell chkcell2 = (DataGridViewCheckBoxCell)dataGridView1.Rows[i].Cells[2];
                DataGridViewCheckBoxCell chkcell3 = (DataGridViewCheckBoxCell)dataGridView1.Rows[i].Cells[3];


                if (dataGridView1.Rows[i].Cells[0].Selected == true && dataGridView1.Rows[i].Cells[1].Selected == true && dataGridView1.Rows[i].Cells[2].Selected == true && dataGridView1.Rows[i].Cells[3].Selected == true)// chk both box
                {
                    MessageBox.Show("Cannot check both checkbox");
                    dataGridView1.Rows[i].Cells[4].Value = "";
                    dataGridView1.Rows[i].Cells[4].Value = "";

                }
                if (dataGridView1.Rows[i].Cells[0].Selected == true)
                {
                    dataGridView1.Rows[i].Cells[4].Value = "Present";
                    day = dataGridView1.Rows[i].Cells[4].Value.ToString();
                    name = dataGridView1.Rows[i].Cells[5].Value.ToString();
                    chkcell1.Value = false;
                    chkcell2.Value = false;
                    chkcell3.Value = false;


                }
                else if (dataGridView1.Rows[i].Cells[1].Selected == true)
                {
                    dataGridView1.Rows[i].Cells[4].Value = "Absent";
                    day = dataGridView1.Rows[i].Cells[4].Value.ToString();
                    name = dataGridView1.Rows[i].Cells[5].Value.ToString();
                    chkcell0.Value = false;
                    chkcell2.Value = false;
                    chkcell3.Value = false;

                }
                else if (dataGridView1.Rows[i].Cells["HD"].Selected == true)
                {
                    dataGridView1.Rows[i].Cells[4].Value = "Halfday";
                    day = dataGridView1.Rows[i].Cells[4].Value.ToString();
                    name = dataGridView1.Rows[i].Cells[5].Value.ToString();
                    chkcell1.Value = false;
                    chkcell0.Value = false;
                    chkcell3.Value = false;


                }
                else if (dataGridView1.Rows[i].Cells["OT"].Selected == true)
                {
                    dataGridView1.Rows[i].Cells[4].Value = "Overtime";
                    day = dataGridView1.Rows[i].Cells[4].Value.ToString();
                    name = dataGridView1.Rows[i].Cells[5].Value.ToString();
                    chkcell1.Value = false;
                    chkcell0.Value = false;
                    chkcell2.Value = false;


                }

            }
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            string date = System.DateTime.Now.ToString("MM'-'dd'-'yyyy");
            string date1 = dateTimePicker1.Value.ToString("MM'-'dd'-'yyyy");

            string w_name;
            float w_id;
            int k = dataGridView1.SelectedCells[0].RowIndex;
            w_name = dataGridView1.Rows[k].Cells[5].Value.ToString();
            w_id = db.getDb_Value("select w_id from waiter_dtls where wname='" + w_name + "'");

            if (date == date1)
            {

                if (!db.ChkDb_Value("select date from waiter_prsenty where date='" + date + "' and w_id='" + w_id + "'"))
                {
                    for (int i = 0; i <= dataGridView1.RowCount - 1; i++)
                    {
                        string dt = dateTimePicker1.Value.ToString("MM'-'dd'-'yyyy");
                        w_name = dataGridView1.Rows[i].Cells[5].Value.ToString();
                        w_id = db.getDb_Value("select w_id from waiter_dtls where wname='" + w_name + "'");
                        string status = dataGridView1.Rows[i].Cells[4].Value.ToString();

                        //if (status != "Absent")
                        //{
                            if (status != "Overtime")
                            {

                                db.insert("insert into waiter_prsenty(w_id,date,status,remark,InTime,OutTime) values('" + w_id + "','" + dt + "','" + status + "','" + "0" + "','00:00:00','00:00:00')");
                            }
                            else
                            {
                                db.insert("insert into waiter_prsenty(w_id,date,status,remark,InTime,OutTime) values('" + w_id + "','" + dt + "','" + status + "','" + "0" + "','00:00:00','00:00:00')");
                              //  db.insert("insert into waiter_prsenty(w_id,date,status,remark,InTime,OutTime) values('" + w_id + "','" + dt + "','"+"Present"+"','" + "0" + "','00:00:00','00:00:00')");

   
                            }
                          //}
                    }

                    MessageBox.Show("Record saved Successfully");

                }
                else
                {


                    MessageBox.Show("Presenty Fill Already!!!", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            else
            {

                DialogResult dlgresult = MessageBox.Show("Are you sure want to Update?", "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgresult == DialogResult.Yes)
                    {

                        for (int i = 0; i <= dataGridView1.RowCount - 1; i++)
                        {
                            string dt = dateTimePicker1.Value.ToString("MM'-'dd'-'yyyy");
                            w_name = dataGridView1.Rows[i].Cells[5].Value.ToString();
                            w_id = db.getDb_Value("select w_id from waiter_dtls where wname='" + w_name + "'");
                            string status = dataGridView1.Rows[i].Cells[4].Value.ToString();

                            if (status != "Absent")
                            {
                                if (status != "Overtime")
                                {

                                    db.insert("insert into waiter_prsenty(w_id,date,status,remark,InTime,OutTime) values('" + w_id + "','" + dt + "','" + status + "','" + "0" + "','00:00:00','00:00:00')");
                                }
                                else
                                {
                                    db.insert("insert into waiter_prsenty(w_id,date,status,remark,InTime,OutTime) values('" + w_id + "','" + dt + "','" + status + "','" + "0" + "','00:00:00','00:00:00')");
                                    db.insert("insert into waiter_prsenty(w_id,date,status,remark,InTime,OutTime) values('" + w_id + "','" + dt + "','" + "Present" + "','" + "0" + "','00:00:00','00:00:00')");


                                }
                              //  db.insert("insert into waiter_prsenty(w_id,date,status,remark,InTime,OutTime) values('" + w_id + "','" + dt + "','" + status + "','" + "0" + "','00:00:00','00:00:00')");
                            }
                        }

                        MessageBox.Show("Record Updated Successfully");
                    }

            }


            clear();
        }

        void clear()
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                DataGridViewCheckBoxCell chkcell0 = (DataGridViewCheckBoxCell)dataGridView1.Rows[i].Cells[0];
                DataGridViewCheckBoxCell chkcell1 = (DataGridViewCheckBoxCell)dataGridView1.Rows[i].Cells[1];
                DataGridViewCheckBoxCell chkcell2 = (DataGridViewCheckBoxCell)dataGridView1.Rows[i].Cells[2];
                DataGridViewCheckBoxCell chkcell3 = (DataGridViewCheckBoxCell)dataGridView1.Rows[i].Cells[3];
                chkcell0.Value = false;
                chkcell1.Value = false;
                chkcell2.Value = false;
                chkcell3.Value = false;
                //chkcell2.Value = false;
            }

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Cells[4].Value = "Absent";
            }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
             string date = System.DateTime.Now.ToString("MM'-'dd'-'yyyy");
            string date1 = dateTimePicker1.Value.ToString("MM'-'dd'-'yyyy");

            string w_name;
            float w_id;
            int k = dataGridView1.SelectedCells[0].RowIndex;
            w_name = dataGridView1.Rows[k].Cells[5].Value.ToString();
            w_id = db.getDb_Value("select w_id from waiter_dtls where wname='" + w_name + "'");

            if (date == date1)
            {
                MessageBox.Show("Plz Click On Save Button !");
               
            }
            else
            {

                DialogResult dlgresult = MessageBox.Show("Are you sure want to Update?", "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgresult == DialogResult.Yes)
                    {

                        for (int i = 0; i <= dataGridView1.RowCount - 1; i++)
                        {
                            string dt = dateTimePicker1.Value.ToString("MM'-'dd'-'yyyy");
                            w_name = dataGridView1.Rows[i].Cells[5].Value.ToString();
                            w_id = db.getDb_Value("select w_id from waiter_dtls where wname='" + w_name + "'");
                            string status = dataGridView1.Rows[i].Cells[4].Value.ToString();

                            //if (status != "Absent")
                            //{
                                if (status != "Overtime")
                                {

                                    db.insert("insert into waiter_prsenty(w_id,date,status,remark,InTime,OutTime) values('" + w_id + "','" + dt + "','" + status + "','" + "0" + "','00:00:00','00:00:00')");
                                }
                                else
                                {
                                    db.insert("insert into waiter_prsenty(w_id,date,status,remark,InTime,OutTime) values('" + w_id + "','" + dt + "','" + status + "','" + "0" + "','00:00:00','00:00:00')");
                                   // db.insert("insert into waiter_prsenty(w_id,date,status,remark,InTime,OutTime) values('" + w_id + "','" + dt + "','"+"Present"+"','" + "0" + "','00:00:00','00:00:00')");


                                }
                                          // }
                        }

                        MessageBox.Show("Record Updated Successfully");
                    }

            }


            clear();
        }

        


    }
}
