using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using System.Windows.Forms.Design;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo.SqlEnum;
using Microsoft.SqlServer.Management.Sdk.Sfc;
using System.Configuration;
using System.Collections.Specialized;
using System.Windows.Forms;
using System.Security;
using System.Security.Cryptography;
using System.Runtime.InteropServices;


namespace Hotal_Managment_Syatem
{
    class DbBackup
    {
        public void DBBackup(string DbName, string Path)
        {

            string cnstr = StringCipher.Decrypt(System.IO.File.ReadAllText("Config.txt"), "5");
            string instance = cnstr.Substring(12);
            int length = instance.Length;
            int i = instance.IndexOf(';');
            int k = length - i;
            string result = instance.Remove(length - k, k);

            var server = new Server(new ServerConnection { ConnectionString = new SqlConnectionStringBuilder { DataSource = result, IntegratedSecurity = true }.ToString() });
            server.ConnectionContext.Connect();
            var database = server.Databases[DbName];
            var output = new StringBuilder();

            foreach (Table table in database.Tables)
            {
                var scripter = new Scripter(server) { Options = { ScriptData = true } };
                var script = scripter.EnumScript(new SqlSmoObject[] { table });
                foreach (var line in script)
                {
                    output.AppendLine(line);
                    output.Append(Environment.NewLine);
                }
            }


            string fileLoc = Path;
            FileStream fs = null;
            if (!File.Exists(fileLoc))
            {
                using (fs = File.Create(fileLoc))
                {

                }
                using (StreamWriter sw1 = new StreamWriter(fileLoc))
                {
                    sw1.Write(output.ToString());

                    sw1.Close();
                }

            }
            else if (File.Exists(fileLoc))
            {
                using (StreamWriter sw = new StreamWriter(fileLoc))
                {
                    sw.Write(output.ToString());
                    sw.Close();
                }
            }
           // MessageBox.Show("Database Backup Created Successfully..");
        }


        public void EncryptFile(string inputFile, string outputFile)
        {

            try
            {
                string password = @"myKey123"; // Your Key Here
                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] key = UE.GetBytes(password);

                string cryptFile = outputFile;
                FileStream fsCrypt = new FileStream(cryptFile, FileMode.Create);

                RijndaelManaged RMCrypto = new RijndaelManaged();

                CryptoStream cs = new CryptoStream(fsCrypt,
                    RMCrypto.CreateEncryptor(key, key),
                    CryptoStreamMode.Write);

                FileStream fsIn = new FileStream(inputFile, FileMode.Open, FileAccess.Read);

                int data;
                while ((data = fsIn.ReadByte()) != -1)
                    cs.WriteByte((byte)data);


                fsIn.Close();
                cs.Close();
                fsCrypt.Close();
            //   MessageBox.Show("Encryption Successfully", "Success");

                File.Delete(inputFile);
            }
            catch
            {
                MessageBox.Show("Encryption failed!", "Error");
            }
        }
        ///<summary>
        /// Steve Lydford - 12/05/2008.
        ///
        /// Decrypts a file using Rijndael algorithm.
        ///</summary>
        ///<param name="inputFile"></param>
        ///<param name="outputFile"></param>
        public void DecryptFile(string inputFile, string outputFile)
        {
           try
           {
                   string password = @"myKey123"; // Your Key Here

                   UnicodeEncoding UE = new UnicodeEncoding();
                   byte[] key = UE.GetBytes(password);

                   FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);

                   RijndaelManaged RMCrypto = new RijndaelManaged();

                   CryptoStream cs = new CryptoStream(fsCrypt,
                       RMCrypto.CreateDecryptor(key, key),
                       CryptoStreamMode.Read);

                   FileStream fsOut = new FileStream(outputFile, FileMode.Create);

                   int data;
                   while ((data = cs.ReadByte()) != -1)
                       fsOut.WriteByte((byte)data);

                   fsOut.Close();
                   cs.Close();
                   fsCrypt.Close();
                   MessageBox.Show("Dcryption Successfully", "Success");
               }
                 catch
                {
                MessageBox.Show("Dcryption failed!", "Error");
                }
           }
        }
    }
