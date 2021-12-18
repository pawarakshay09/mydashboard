using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
//using SecureApp;

namespace Hotal_Managment_Syatem
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //string abc = @"Software\Mahesh\Protection";
            //Secure scr = new Secure();
            
            //bool logic = scr.Algorithm("magic4321@2012", abc,30);
            //if (logic == true)
            //{
                Application.Run(new login_test());
        		// Application.Run(new SMS());
            // }
              
        }
    }
}
