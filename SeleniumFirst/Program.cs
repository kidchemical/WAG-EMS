using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeleniumFirst
{
    static class Program
    {


        [STAThread]
        static void Main()
        {
            //AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                Application.Run(new Form1());
            }
            catch
            {
                Application.Run(new Form1());
            }
            // Do something here
        }
    }


}
