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
        public bool MXConnectLogin()
        {
            string title = "MXConnect";
            FetchURL(title);
            GoToURL(_url);
            if (ElementPresent(_driver, By.XPath("//input[@name='username']")))
            {
                try
                {
                    Status("Logging in...");
                    _driver.FindElement(By.XPath("//input[@name='username']")).SendKeys(admin.mxconnect.username);
                    _driver.FindElement(By.XPath("//input[@name='password']")).SendKeys(admin.mxconnect.password + OpenQA.Selenium.Keys.Enter);
                }
                catch { return false; }
            }
            GoToURL("https://portal.mazdausa.com/portal/WSL/DealerPersonAdmin?id=LIST&fromCntryCd=US&language=en");
            return true;
        }

        public bool MXConnectAddUser()
        {
            GoToURL("https://portal.mazdausa.com/portal/WSL/DealerPersonAdmin?id=ADDINIT");

            if (employee.ssn == "")
            {
                InputBoxClass.InputBox("SSN", "Enter Employee Social Security Number", ref employee.ssn);
            }

            _driver.FindElement(By.XPath("//input[@name='personID']")).SendKeys(employee.ssn);
            _driver.FindElement(By.XPath("//input[@name='confirmPersonId']")).SendKeys(employee.ssn);
            _driver.FindElement(By.XPath("//input[@name='employeeID']")).SendKeys(employee.employeenumber.Substring(1, 5));    // Trim first, Max 5 characters
            _driver.FindElement(By.XPath("//input[@name='lastName']")).SendKeys(employee.lastname);
            _driver.FindElement(By.XPath("//input[@name='firstName']")).SendKeys(employee.firstname);
            _driver.FindElement(By.XPath("//input[@name='streetAddress1']")).SendKeys(_address);
            _driver.FindElement(By.XPath("//input[@name='city']")).SendKeys(_city);
            _driver.FindElement(By.XPath("//input[@name='city']")).SendKeys(_city);
            new SelectElement(_driver.FindElement(By.XPath("//select[@name='stateProvince']"))).SelectByValue(_state);
            _driver.FindElement(By.XPath("//input[@name='zipPostalCode']")).SendKeys(_zip);
            _driver.FindElement(By.XPath("//input[@name='busTelephoneNPA']")).SendKeys(_phoneA);
            _driver.FindElement(By.XPath("//input[@name='busTelephoneNXX']")).SendKeys(_phoneB);
            _driver.FindElement(By.XPath("//input[@name='busTelephoneLINE']")).SendKeys(_phoneC);
            _driver.FindElement(By.XPath("//input[@name='eMailAddress']")).SendKeys(employee.email.username);
            new SelectElement(_driver.FindElement(By.XPath("//select[@name='hireCode']"))).SelectByValue("H9");
            new SelectElement(_driver.FindElement(By.XPath("//select[@name='primaryJobCode']"))).SelectByValue(employee.mxconnect.roleID);
            _driver.FindElement(By.XPath("//input[@name='addButton']")).Submit();
            GoToURL("https://portal.mazdausa.com/portal/WSL/DealerPersonAdmin?id=LIST");
            employee.mxconnect.username = _driver.FindElement(By.XPath("//a[text()='" + employee.lastname.ToUpper() + ", " + employee.firstname.ToUpper() + "']/following::font")).Text.Trim();
            if (employee.mxconnect.username == null)
            {
                Error("Could not confirm " + "MXConnect" + " login creation!");
                return false;
            }
            employee.mxconnect.password = "Emailed";
            Status("User ID - " + employee.mxconnect.username + " : " + employee.mxconnect.password);
            Status("Succefully created " + "MXConnect");
            return true;
        }

        public bool MXConnectRemoveUser()
        {
            if (MXConnectCheckUser(employee.mxconnect.username) == true)
            {
                Status("Removing MXConnect account...");
                _driver.FindElement(By.XPath("//a[contains(text(), '" + employee.GuessLastName().ToUpper() + "')]")).Click();

                //Edit user account page
                new SelectElement(_driver.FindElement(By.XPath("//select[@name='termMM']"))).SelectByValue((Int32.Parse(DateTime.Now.ToString("MM")) - 1).ToString());
                _driver.FindElement(By.XPath("//input[@name='termDD']")).SendKeys(DateTime.Now.ToString("dd"));
                _driver.FindElement(By.XPath("//input[@name='termYY']")).SendKeys(DateTime.Now.ToString("yyyy"));
                new SelectElement(_driver.FindElement(By.XPath("//select[@name='termCode']"))).SelectByValue("T1");

                _driver.FindElement(By.XPath("//input[@value='Submit']")).Click();

                Sleep(5000);
                try
                {
                    _driver.SwitchTo().Alert().Accept();
                    _driver.SwitchTo().Alert().Accept();
                }
                catch
                {

                }

                if (ElementPresent(_driver, By.XPath("//font[text()='**TERMINATED**']"), _timeout, false))
                {
                    Status("Successfully removed MXConnect account!");
                    return true;
                }
                else
                {
                    Log("There was an issue verifying termination for MXConnect user " + employee.mxconnect.username);
                    return true;
                }
                // Finish the code here.... everything just kinda stops!

            }
            else
            {
                //MessageBox.Show("DEBUG HALT!* Finish manually and program the rest!");
                Log(employee.mxconnect.username + " is not found on MXConnect");
                return true;
            }
        }

        public bool MXConnectCheckUser(string username )
        {
            GoToURL("https://portal.mazdausa.com/portal/WSL/DealerPersonAdmin?id=LIST&fromCntryCd=US&language=en");
            if (username != null)
            {
                if (ElementPresent(_driver,By.XPath("//font[contains(text(), '" + username + "')]"), 5, false))
                {
                    Status(username + " found on MXConnect");
                    return true;
                }
            }
            Log(username + " NOT found on MXConnect!");
            return false;
        }
    }
}
