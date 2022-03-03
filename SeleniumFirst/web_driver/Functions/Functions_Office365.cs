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
        public bool Office365Login()
        {
            //string title = "Office 365";
            //FetchURL(title);
            //GoToURL(_url);
            GoToURL("https://admin.microsoft.com/Adminportal/Home?source=applauncher#/homepage/:/adduser");
            Sleep(1000);
            if (ElementPresent(_driver, By.XPath("//div[text()='it@wiseautogroup.com']"), 3, false))
            {
                Status("Saved credentials detected...");
                _driver.FindElement(By.XPath("//div[text()='it@wiseautogroup.com']")).Click();
            }

            if (ElementPresent(_driver, By.XPath("//input[@type='email']"), 3, false))
            {
                Status("Inserting credentials...");
                _driver.FindElement(By.XPath("//input[@type='email']")).SendKeys(admin.office365.username);
                _driver.FindElement(By.XPath("//input[@type='email']")).Submit();
                //Sleep(3000);
            }

            if (ElementPresent(_driver, By.XPath("//input[@type='password']"), 3, false))
            {
                //MessageBox.Show(admin.office365.password);
                _driver.FindElement(By.XPath("//input[@type='password']")).SendKeys(admin.office365.password);
                _driver.FindElement(By.XPath("//input[@type='password']")).Submit() ;
               // _driver.FindElement(By.XPath("//input[@type='submit']")).Click();
            }

            // Stay signed in? (No)
            if (ElementPresent(_driver, By.XPath("//input[@id='idBtn_Back']"), 3, false))
            {
                _driver.FindElement(By.XPath("//input[@id='idBtn_Back']")).Click();
            }

            Status("Logged in to Office365");
            Sleep(5000);
            return true;
        }

        public bool Office365AddUser()
        {
            // THis needs to be fixed. Checkboxes seem to be determined on users last selection. 
            // Using the Select property does not reflect whether or not the element check boxes are checked
            // needs a clever solution!


            //_driver.FindElement(By.XPath("//input[@type='password']")).SendKeys(admin.office365.password);
            //Sleep(1000);
            //_driver.FindElement(By.XPath("//input[@id='idSIButton9']")).Click();
            GoToURL("https://admin.microsoft.com/Adminportal/Home?source=applauncher#/homepage/:/adduser");

            _driver.FindElement(By.XPath("//label[text()='First name']/../div/input")).SendKeys(employee.firstname);
            _driver.FindElement(By.XPath("//label[text()='Last name']/../div/input")).SendKeys(employee.lastname + OpenQA.Selenium.Keys.Tab);
            Sleep(5000);
            string username = employee.firstname[0].ToString().ToLower() + employee.lastname.ToLower();
            _driver.FindElement(By.XPath("//label[text()='Username']/../div/input")).SendKeys(username);
            //_driver.FindElement(By.XPath("//span[contains(text(), 'Automatically create a password')]/../div/i")).Click();

            // Require password change check-box
            /*
            if (!_driver.FindElement(By.XPath("//span[contains(text(), 'Require this user to change their password when they first sign in')]/../div/i")).Selected)
            {
                Status("Checking box - require password change");
                _driver.FindElement(By.XPath("//span[contains(text(), 'Require this user to change their password when they first sign in')]/../div/i")).Click();
            }*/


            string password = "Officefor" + employee.firstname.ToLower() + "1914";
            try
            {
                _driver.FindElement(By.XPath("//input[@type='password']")).SendKeys(password);
            }
            catch
            {
                if (!_driver.FindElement(By.XPath("//span[contains(text(), 'Require this user to change their password when they first sign in')]/../div/i")).Selected)
                {
                    Status("Checking box - require password change");
                    _driver.FindElement(By.XPath("//span[contains(text(), 'Require this user to change their password when they first sign in')]/../div/i")).Click();
                    _driver.FindElement(By.XPath("//input[@type='password']")).SendKeys(password);
                }
            }

            _driver.FindElement(By.XPath("//span[contains(text(), 'Next')]")).Click();


            //MessageBox.Show("DEBUG HALT");
            //Log("Office365 is not implemented. Contact administration.");

            // Assign prodcut licenses page


            Sleep(3000);
            try
            {
                _driver.FindElement(By.XPath("//span[text()='Next']/..")).Click();
            }
            catch
            {
                // Must mean we need to select the product license box...
                _driver.FindElement(By.XPath("//div[contains(text(), 'Microsoft 365 Apps for business')]/../../div/i")).Click();
                //Sleep(8000);
                if (ElementPresent(_driver, By.XPath("//span[text()='Yes']"), _timeout, false))
                {
                    _driver.FindElement(By.XPath("//span[text()='Yes']")).Click();
                    Sleep(10000);
                }
            }
            try
            {
                _driver.FindElement(By.XPath("//span[text()='Next']/..")).Click();
                _driver.FindElement(By.XPath("//span[text()='Yes']/..")).Click();
            }
            catch { }

            Sleep(10000);

            try
            {
                _driver.FindElement(By.XPath("//span[text()='Next']/..")).Click();
                Sleep(2000);
                _driver.FindElement(By.XPath("//span[text()='Next']/..")).Click();
                Sleep(2000);
               
            }
            catch
            {
                // derp
            }


            _driver.FindElement(By.XPath("//span[text()='Finish adding']")).Click();

            if (ElementPresent(_driver, By.XPath("//i[@data-icon-name='CompletedSolid']"), _timeout, false))
            {
                Progress("Succefully created Office365 account - " + username + "@wiseautogroup.com : " + password);
                return true;
            }
            else
            {
                Log("Could not create Office365 account [0x01]");
                return false;
            }



        }

        public bool Office365RemoveUser(string username = "")
        {
            if (username == "")
            {
                username = employee.email.username;
            }


            if (!Office365CheckUser()) { return false; }
            
            _driver.FindElement(By.XPath("//span[text()='" + username +  "']")).Click();
            Sleep(1000);
            _driver.FindElement(By.XPath("//span[contains(text(), 'Delete user')]")).Click();
            Sleep(5000);
            _driver.FindElement(By.XPath("(//span[contains(text(), 'Delete user')]) [2]")).Click();

            //MessageBox.Show("DEbug Halt! Finish coding and do manually.");
            return true;
        }

        public bool Office365CheckUser()
        {
            if (employee.email.username == null || employee.email.username == "")
            {
                Log("Employee email invalid");
                return false;
            }

            GoToURL("https://admin.microsoft.com/Adminportal/Home?source=applauncher#/users");
            Sleep(3000);
            _driver.FindElement(By.XPath("//input[contains(@placeholder, 'Search active users list')]")).SendKeys(employee.email.username + OpenQA.Selenium.Keys.Enter);
            if (_driver.FindElement(By.XPath("//span[contains(text(), '" + employee.email.username + "')]")).Enabled)
            {
                Status("Office365 user found");
                return true;
            }
            Log("Office365 user NOT found");
            return false;
        }
    }
}
