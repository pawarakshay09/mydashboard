using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Threading;

namespace Hotal_Managment_Syatem
{
    public partial class MenuUpload : Form
    {
        Database db = new Database();
        DataTable dtLocalC = new DataTable();
        public static string ConString = StringCipher.Decrypt(System.IO.File.ReadAllText("Config.txt"), "5");

        public MenuUpload()
        {
            InitializeComponent();

        }

        private void MenuUpload_Load(object sender, EventArgs e)
        {

            var dt = new DataTable();


        }


        private void btnChoose_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfiledialog1 = new OpenFileDialog();
            openfiledialog1.ShowDialog();
            openfiledialog1.Filter = "allfiles|*.xls";
            txtPath.Text = openfiledialog1.FileName;
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            this.menuGrid.Refresh();
            this.menuGrid.Parent.Refresh();

            pictureBox1.Visible = true;
            pictureBox1.Dock = DockStyle.None;
            backgroundWorker1.RunWorkerAsync();


            string path = txtPath.Text;
            String name = "Sheet1";
            String constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties='Excel 12.0 XML;HDR=YES;';";

            OleDbConnection con = new OleDbConnection(constr);
            OleDbCommand oconn = new OleDbCommand("Select * From [" + name + "$]", con);
            con.Open();

            OleDbDataAdapter sda = new OleDbDataAdapter(oconn);
            DataTable data = new DataTable();
          
            sda.Fill(data);
            menuGrid.DataSource = data;
            menuGrid.Columns["status"].Width = 150;
            menuGrid.Columns[1].Width = 200;
            menuGrid.Columns[2].Width = 200;
            menuGrid.Columns[5].Width = 200;

            Regex regex = new Regex("[0-9]");

            for (int i = 0; i < menuGrid.Rows.Count; i++)
            {

                string menu = menuGrid.Rows[i].Cells["menu_name"].Value.ToString();
                string cat = menuGrid.Rows[i].Cells["category"].Value.ToString();
                string rate = menuGrid.Rows[i].Cells["rate"].Value.ToString();
                string nrate = menuGrid.Rows[i].Cells["non_ACrate"].Value.ToString();
                string Pname = menuGrid.Rows[i].Cells["printName"].Value.ToString();
                string itemcode = menuGrid.Rows[i].Cells["itemcode"].Value.ToString();



                string msgm = "Empty Menu ";
                string msgc = "Empty Catgory  ";
                string msgr = "Empty Rate ";
                string msgn = "Empty NON AC Rate";
                string msgp = "Empty PrintName ";
                string msgitem = "Empty Itemcode ";


                if (menu != "")
                {
                    if (cat != "")
                    {
                        if (rate != "" && regex.IsMatch(rate))
                        {
                            if (nrate != "" && regex.IsMatch(nrate))
                            {
                                if (Pname != "")
                                {
                                    if (itemcode != "" && regex.IsMatch(itemcode))
                                    {

                                        //cell[i] = menuGrid.Cells[i].Value;
                                        // MessageBox.Show( menu    +     " Menu Inserted Successfully");

                                        // MessageBox.Show( menu    +     " Menu Inserted Successfully");

                                        menuGrid.Rows[i].Cells["status"].Value = "Success";

                                    }
                                    else
                                    {
                                        menuGrid.Rows[i].Cells["status"].Value = msgitem;
                                        //MessageBox.Show("Data not in proper Format");
                                    }

                                }
                                else
                                {
                                    menuGrid.Rows[i].Cells["status"].Value = msgp;
                                    //MessageBox.Show("Data not in proper Format");
                                }
                            }
                            else
                            {
                                menuGrid.Rows[i].Cells["status"].Value = msgn;
                                //MessageBox.Show("Data not in proper Format");
                            }
                        }
                        else
                        {
                            menuGrid.Rows[i].Cells["status"].Value = msgr;
                            //MessageBox.Show("Data not in proper Format");
                        }
                    }
                    else
                    {
                        menuGrid.Rows[i].Cells["status"].Value = msgc;
                        //MessageBox.Show("Data not in proper Format");
                    }
                }

                else
                {
                    menuGrid.Rows[i].Cells["status"].Value = msgm;
                    //MessageBox.Show("Data not in proper Format");
                }

                
            }

           
            
        }



        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void menuGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btn_Add_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //string st = cbFilter.Text;

            //BindingSource bs = new BindingSource();
            //bs.DataSource = menuGrid.DataSource;
            //bs.Filter = "[status] like '%" + st + "%'";
            //menuGrid.DataSource = bs;


        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {

          //  string st = cbFilter.Text;

          //  BindingSource bs = new BindingSource();
          //  bs.DataSource = menuGrid.DataSource;
          //  bs.Filter = "[status] like '%" + st + "%'";
          ////  bsData.Filter = "Name like '" & TextBox1.Text & "%'"
          //  menuGrid.DataSource = bs;
          

        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string st = cbFilter.Text;

            BindingSource bs = new BindingSource();
            bs.DataSource = menuGrid.DataSource;
           // bs.Filter = "[status] like '%" + st + "%'";
          //  bs.Filter = "status like '%" + txtFilter.Text + "%'";

         //   bs.Filter = " '"+ menuGrid.Rows[0].Cells[0].Value.ToString() +"' like '%" + txtFilter.Text + "%'";

      

            menuGrid.DataSource = bs;
        }

        private void cbFilter_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string st = cbFilter.Text;

            BindingSource bs = new BindingSource();
            bs.DataSource = menuGrid.DataSource;
            // bs.Filter = "[status] like '%" + st + "%'";
            bs.Filter = "status like '%" + st + "%'";

            menuGrid.DataSource = bs;

            if (cbFilter.Text == "Success")
            {
                btnSave.Visible = true;
                this.menuGrid.Refresh();
                this.menuGrid.Parent.Refresh();

                pictureBox1.Visible = true;
                pictureBox1.Dock = DockStyle.None;
                backgroundWorker1.RunWorkerAsync();
            }
            else
            {
                btnSave.Visible = false;
                pictureBox1.Visible = false;
            }



        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            Regex regex = new Regex("[0-9]");
            string cat="",insertcat="";
            for (int i = 0; i < menuGrid.Rows.Count; i++)
            {
                string menu = menuGrid.Rows[i].Cells["menu_name"].Value.ToString();
                cat = menuGrid.Rows[i].Cells["category"].Value.ToString();
                string rate = menuGrid.Rows[i].Cells["rate"].Value.ToString();
                string nrate = menuGrid.Rows[i].Cells["non_ACrate"].Value.ToString();
                string Pname = menuGrid.Rows[i].Cells["printName"].Value.ToString();
                string itemcode = menuGrid.Rows[i].Cells["itemcode"].Value.ToString();



                //string msgm = "Empty Menu name ";
                //string msgc = "Empty Catgory name ";
                //string msgr = "Empty Rate name ";
                //string msgn = "Empty NON AC Rate name ";
                //string msgp = "Empty PrintName name ";

                //if (menu != "")
                //{
                //    if (cat != "")
                //    {
                //        if (rate != "" && regex.IsMatch(rate))
                //        {
                //            if (nrate != "")
                //            {
                //                if (Pname != "")
                //                {

                                                                               
                db.insert(@"INSERT INTO menu(m_name,category,rate,non_ACrate,printName,item_code)  VALUES ('" + menu + "', '" + cat + "' , '" + rate + "' , '" + nrate + "',  '" + Pname + "','" + itemcode + "')"); // DGV_tblOrder.Rows[i].Cells[1].Value.ToString()

                if(cat!="")
                {
                    if (insertcat!=cat)
                    {
                     db.insert("Insert into category(menu_id,cat_name,descr,kotprintStatus,type) values('0' ,'" + cat + "','null','Yes','Table Order') ");
                    }
                    insertcat = cat;
                }
                // MessageBox.Show( menu    +     " Menu Inserted Successfully");
                //                    menuGrid.Rows[i].Cells["status"].Value = "SUCCESS";



                //                }
                //                else
                //                {
                //                    menuGrid.Rows[i].Cells["status"].Value = msgp;
                //                    //MessageBox.Show("Data not in proper Format");
                //                }
                //            }
                //            else
                //            {
                //                menuGrid.Rows[i].Cells["status"].Value = msgn;
                //                //MessageBox.Show("Data not in proper Format");
                //            }
                //        }
                //        else
                //        {
                //            menuGrid.Rows[i].Cells["status"].Value = msgr;
                //            //MessageBox.Show("Data not in proper Format");
                //        }
                //    }
                //    else
                //    {
                //        menuGrid.Rows[i].Cells["status"].Value = msgc;
                //        //MessageBox.Show("Data not in proper Format");
                //    }
                //}

                //else
                //{
                //    menuGrid.Rows[i].Cells["status"].Value = msgm;
                //    //MessageBox.Show("Data not in proper Format");
                //}


                // bimd msg here 



            }
            MessageBox.Show("Menu Inserted Successfully......!");

        }


      

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            //if (isloginsucess)
            //{



            //}
            //else
            //{

              
            //}

        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.menuGrid.Refresh();
            this.menuGrid.Parent.Refresh();

            pictureBox1.Visible = true;
            pictureBox1.Dock = DockStyle.None;
            backgroundWorker1.RunWorkerAsync();
            try
            {
                string fileExcel;
                fileExcel = @"D:\Restrosoft\Book1.xlsx";
           //     Microsoft.Office.Interop.Excel.Application xlapp;
           //     Microsoft.Office.Interop.Excel.Workbook xlworkbook;
          //      xlapp = new Microsoft.Office.Interop.Excel.Application();

         //       xlworkbook = xlapp.Workbooks.Open(fileExcel, 0, false, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

         //       xlapp.Visible = true;
            }
            catch
            {
                MessageBox.Show("File Not Found.Path:- D:Restrosoft:Book1.xlsx");
            }
        }

        private void backgroundWorker1_DoWork_1(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(400);
        }

        private void backgroundWorker1_RunWorkerCompleted_1(object sender, RunWorkerCompletedEventArgs e)
        {
            pictureBox1.Visible = false;
        }
    }
}
