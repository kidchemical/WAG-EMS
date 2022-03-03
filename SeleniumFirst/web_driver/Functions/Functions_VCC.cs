using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SeleniumFirst
{
    public partial class Form1
    {
        public bool VCCLogin()
        {
            /*
            string title = "VCC";
            FetchURL(title);
            GoToURL(_url);
            */
            Log("Volvo VCC is currently not implemented. Please contact IT administration");
            //Status("Succefully created " + title);
            return true;
        }

        public bool VCCAddUser()
        {
            return true;
        }

        public bool VCCRemoveUser()
        {
            return true;
        }

        public bool VCCCheckUser()
        {
            return true;
        }
    }
}
