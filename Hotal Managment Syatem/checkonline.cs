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
    
    class checkonline
    {  
        string date;
        Database db=new Database();
        //static string cnstr = @"Data Source=103.235.104.24;Initial Catalog=companyInfo;User ID=companyUser;Password=abms@2014"; // Online DB Path
        static string cnstr = @"Data Source=103.235.104.24;Initial Catalog=abmstech_myRestrosoft;User ID=myrestrosoft; Password=abms@2014";

        // Data Source=103.235.104.24;Initial Catalog=abmstech_myRestrosoft;User ID=myrestrosoft; Password=abms@2014



        string cmpnyName, address, mob, dnsName = "";
        public string conString;
        string str;
        public SqlConnection cn = new SqlConnection(cnstr);
      
        public void cnopen()
        {
            if (cn.State == ConnectionState.Open)
            {
                cn.Close();
            }
            cn.Open();
        }

        public string getDbstatus_Value(String query)
        {
            cnopen();
            SqlCommand cmd = new SqlCommand(query, cn);
            try
            {
                if (cmd.ExecuteScalar().ToString() == "")
                    str = "0";
                else
                    str = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //erLog.WriteErrorLog(ex.ToString());
            }
            return str;
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

        public void Rdetails()
        {
            string RDID = db.getDbstatus_Value("Select RDetailsId from OwnerDetails");
              string days =getDbstatus_Value("select date +'*'+ noofDays  from RestroOwnerMembership where RDetailsId='" + RDID + "'");


             date = days.Split('*')[0].ToString();
              days = days.Split('*')[1].ToString();



            //    DateTime trDt = new DateTime();
            //    /////
              date = date.Replace('/', '-');


              string dt = date.Split('-')[2] + '-' + date.Split('-')[0] + '-' + date.Split('-')[1];
              db.update("update TrialPeriodSet set NoOfDays='" + days + "' ,TrialDate='" + dt + "'");

              
        }



    }
      

}
