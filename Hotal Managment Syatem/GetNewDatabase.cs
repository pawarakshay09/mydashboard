using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace Hotal_Managment_Syatem
{
    class GetNewDatabase
    {
        //static string cnstr = @"Data Source=103.235.104.24;Initial Catalog=companyInfo;User ID=companyUser;Password=abms@2014"; // Online DB Path
       // public static string ConString = System.IO.File.ReadAllText("Config.txt"); // Local DB Path
        public static string ConString = StringCipher.Decrypt(System.IO.File.ReadAllText("Config.txt"), "5");

        
        string cmpnyName, address, mob, dnsName = "";
        public string conString;

        public SqlConnection cn = new SqlConnection(cnstr);
        public void cnopen()
        {
            if (cn.State == ConnectionState.Open)
            {
                cn.Close();
            }
            cn.Open();
        }

        public void cnclose()
        {
            if (cn.State != ConnectionState.Closed)
            {
                cn.Close();
            }
        }
        public void insert(string query)
        {
            try
            {
                cnopen();
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.ExecuteNonQuery();
                //  message("Data SAVE Successfully!!!");
                cnclose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public bool ChkDb_Value(String query)
        {

            cnopen();
            SqlCommand cmd = new SqlCommand(query, cn);
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read() == true)
                return true;
            else
                return false;
        }
        public void chkOnDbSt()
        {
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead("http://www.google.com"))// here check PC is online or not
                {

                    IPHostEntry host;
                    string localIP = "?";
                    host = Dns.GetHostEntry(Dns.GetHostName());
                    foreach (IPAddress ip in host.AddressList)
                    {
                        if (ip.AddressFamily == AddressFamily.InterNetwork)
                        {
                            localIP = ip.ToString(); // Get IP address of machine
                        }
                    }
                    // get name of PC

                    string netBiosName = System.Environment.MachineName;
                    string dnsName = System.Net.Dns.GetHostName();

                    // get company details

                   SqlConnection con = new SqlConnection(ConString);
                   con.Open();
                   SqlCommand cmd = new SqlCommand("select * from tbl_receiptFormat", con);
                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        cmpnyName = rd["hotelName"].ToString();
                        address = rd["address"].ToString();
                        mob = rd["mobile"].ToString();
                    }

                    con.Close();


                    // insert into online databse table
                    if (!ChkDb_Value("select * from userInfo where companyName='" + cmpnyName + "' and compAddress='"+address+"' and Contact='"+mob+"' "))
                    {
                        insert("insert into userInfo values('Restrosoft','" + cmpnyName + "','" + address + "','" + mob + "','" + netBiosName + "','" + localIP + "','"+System.DateTime.Now.ToString("MM/dd/yyyy")+"','')");
                       // MessageBox.Show("Record Inserted Successfully");
                    }

                }
            }
            catch (Exception ex)
            {
              //  MessageBox.Show("Internet is Not active");
               // MessageBox.Show(ex.Message);
            }
        }
    }
}
