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
        public bool GMGlobalLogin()
        {
            // https://dealer.autopartners.net/portal/us/_layouts/15/gm/nggc/pages/profile/edit.aspx
            string title = "GMGlobal";
            //FetchURL(title);
            GoToURL("https://gcadmin.autopartners.net/");
            _driver.FindElement(By.XPath("//input[@id='IDToken1']")).SendKeys(admin.gmglobal.username);
            _driver.FindElement(By.XPath("//input[@id='IDToken2']")).SendKeys(admin.gmglobal.password);
            _driver.FindElement(By.XPath("//input[@name='Login.Submit']")).Click();

            return true;
        }

        public bool GMGlobalAddUser()
        {
            Log("GMGlobal is not implemented. Contact administration.");
            //Status("Succefully created " + title);
            return true;
        }

        public bool GMGlobalRemoveUser()
        {
            
            if (GMGlobalCheckUser(employee.gmglobal.username) == false)
            {
                Log("Nothing to remove, continuing...");
                return true;
            }

            _driver.FindElement(By.XPath("//a[text()='aawad1']/../../td/div/button[@id='dot']")).Click();
            Sleep(1000);
            _driver.FindElement(By.XPath("//a[text()='aawad1']/../../td/div/button[@id='dot']/../div/div/a[text()='Deactivate User']")).Click();
            _driver.FindElement(By.XPath("//textarea[@id='user_Comments']")).SendKeys("User termination -- " + admin.fullname + " -- " + DateTime.Now);
            Sleep(1000);
            _driver.FindElement(By.XPath("//input[@value='Deactivate']")).Click();
            Sleep(8000);
            Status(employee.gmglobal.username + " removed from GMGlobal");

            return false;
        }

        public bool GMGlobalCheckUser(string username)
        {
            Status("Checking for GMGlobal user... [" + username + "]");
            GoToURL("https://gcadmin.autopartners.net/User/SearchDealerUser");

            //Log("GMGlobal is not implemented. Contact administration.");
            if (username == "" || username == null || username.Length <= 3)
            {
                Error("Invalid username for GMGlobal, exiting...");
                return false;
            }

            if (ElementPresent(_driver, By.XPath("//a[text()='" + username + "']"), 5, false))
            {
                Status(username + " found on GMGlobal");
                return true;
            }

            Log("GMGlobal user not found... [" + username + "]");
            return false;
        }
    }
}
