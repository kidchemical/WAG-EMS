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
        public bool DealerConnectLogin()
        {
            string title = "DealerConnect";
            //FetchURL(title);
            //GoToURL(_url);

            Log("DealerConnect is not implemented. Contact administration.");

            //Status("Succefully created " + title);
            return true;
        }

        public bool DealerConnectAddUser(string username)
        {
            return true;
        }

        public bool DealerConnectRemoveUser(string username)
        {
            return true;
        }

        public bool DealerConnectCheckUser(string username)
        {
            return true;
        }
    }
}
