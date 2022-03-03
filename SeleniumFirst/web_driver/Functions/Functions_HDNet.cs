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
        // ERROR WHEN HIDDEN.... ALSO HDNET GIVES ERROR WHEN LOGGING OUT SOMETIMES....
        public bool HDNetLogin(int loginID = 0) //0 is employee.store based, Reno - 1, Yuba - 2, Redwood - 3, DeathValley - 4
        {
            string title = "H-D Net";
            FetchURL(title);
            GoToURL("https://www.h-dnet.com/system/sling/logout.html"); // Must logout incase switching stores!...
            GoToURL("https://www.h-dnet.com/system/sling/logout.html"); // Must logout incase switching stores!...
            //GoToURL("https://www.h-dnet.com/employeemgmt/?dlrNumber=4385#/home");

            
            if (loginID == 0)
            {
                Status("Logging in to " + employee.store + "...");
                switch (employee.store)
                {
                    case "Reno Harley Davidson":
                        _driver.FindElement(By.XPath("//input[@id='username']")).SendKeys(admin.hdnet.username);
                        _driver.FindElement(By.XPath("//input[@id='password']")).SendKeys(admin.hdnet.password);
                        break;
                    case "Harley Davidson Yuba City":
                        _driver.FindElement(By.XPath("//input[@id='username']")).SendKeys(admin.hdnet1.username);
                        _driver.FindElement(By.XPath("//input[@id='password']")).SendKeys(admin.hdnet1.password);
                        break;
                    case "Redwood Harley Davidson":
                        _driver.FindElement(By.XPath("//input[@id='username']")).SendKeys(admin.hdnet2.username);
                        _driver.FindElement(By.XPath("//input[@id='password']")).SendKeys(admin.hdnet2.password);
                        break;
                    case "Death Valley Harley Davidson":
                        _driver.FindElement(By.XPath("//input[@id='username']")).SendKeys(admin.hdnet2.username);
                        _driver.FindElement(By.XPath("//input[@id='password']")).SendKeys(admin.hdnet2.password);
                        break;
                    default:
                        Error("Unhandled Exception: Could not decide store location");
                        return false;
                }
            }
            else
            {
                Status("Force logging in to store ID " + loginID.ToString() + "...");
                switch (loginID)
                {
                    case 1:
                        _driver.FindElement(By.XPath("//input[@id='username']")).SendKeys(admin.hdnet.username);
                        _driver.FindElement(By.XPath("//input[@id='password']")).SendKeys(admin.hdnet.password);
                        break;
                    case 2:
                        _driver.FindElement(By.XPath("//input[@id='username']")).SendKeys(admin.hdnet1.username);
                        _driver.FindElement(By.XPath("//input[@id='password']")).SendKeys(admin.hdnet1.password);
                        break;
                    case 3:
                        _driver.FindElement(By.XPath("//input[@id='username']")).SendKeys(admin.hdnet2.username);
                        _driver.FindElement(By.XPath("//input[@id='password']")).SendKeys(admin.hdnet2.password);
                        break;
                    case 4:
                        _driver.FindElement(By.XPath("//input[@id='username']")).SendKeys(admin.hdnet3.username);
                        _driver.FindElement(By.XPath("//input[@id='password']")).SendKeys(admin.hdnet3.password);
                        break;
                    default:
                        Error("Unhandled Exception: Could not decide store location");
                        return false;
                }
            }
            _driver.FindElement(By.XPath("//button[@id='loginBtn']")).Submit();

            return true;
        }

        public bool HDNetAddUser(int loginID = 0)
        {
            GoToURL("https://www.h-dnet.com/employeemgmt/?dlrNumber=#/user/new");
            Sleep(5000);
            _driver.FindElement(By.XPath("//input[@id='firstName']")).SendKeys(employee.firstname);
            _driver.FindElement(By.XPath("//input[@id='lastName']")).SendKeys(employee.lastname);
            _driver.FindElement(By.XPath("//input[@id='employeeTypeY']")).Click();
            _driver.FindElement(By.XPath("//input[@id='employeeClassF']")).Click();
            _driver.FindElement(By.XPath("//input[@id='email']")).SendKeys(employee.email.username);

            string input;
            // Select Manager ID
            if (employee.hdnet.managerID == null)
            {
                input = "0";
                new SelectElement(_driver.FindElement(By.XPath("//select[@ng-model='user.manager']"))).SelectByValue(input);   //Manager
            }
            else
            {
                try
                {
                    new SelectElement(_driver.FindElement(By.XPath("//select[@ng-model='user.manager']"))).SelectByValue(employee.hdnet.managerID);   //Manager
                }
                catch
                {
                    input = "0";
                    InputBoxClass.InputBox("Manager", "Enter Manager ID", ref input);
                    new SelectElement(_driver.FindElement(By.XPath("//select[@ng-model='user.manager']"))).SelectByValue(input);   //Manager
                }
            }

            // Select Department ID
            if (employee.hdnet.departmentID == null)
            {
                input = "GENMS";
                InputBoxClass.InputBox("Department", "Enter Department ID", ref input);
                new SelectElement(_driver.FindElement(By.XPath("//select[@ng-model='departmentRole.deptName']"))).SelectByValue(input);   //Department
            }
            else
            {
                new SelectElement(_driver.FindElement(By.XPath("//select[@ng-model='departmentRole.deptName']"))).SelectByValue(employee.hdnet.departmentID);   //Department
            }

            // Select Role ID
            if (employee.hdnet.roleID == null)
            {
                input = "0";
                InputBoxClass.InputBox("Role", "Enter Role ID", ref input);
                try
                {
                    new SelectElement(_driver.FindElement(By.XPath("//select[@ng-model='departmentRole.roleName']"))).SelectByValue(input);   //Role    =+= GAVE BUG - Cannot locate value 7 role
                }
                catch
                {
                    Log("Error locating role selection with value of " + input + ". Trying again recursively...");
                    HDNetAddUser(loginID);
                }
            }
            else
            {
                new SelectElement(_driver.FindElement(By.XPath("//select[@ng-model='departmentRole.roleName']"))).SelectByValue(employee.hdnet.roleID);   //Role
            }
            Sleep(1000);
            _driver.FindElement(By.XPath("//input[@id='continueButton']")).Submit();

            // POSSIBLY A MISSING STEP!?!!! 
            //MessageBox.Show("Debug HAlt");

            // Confirm Creation (if it appears)
            Sleep(1000);
            if (ElementPresent(_driver, By.XPath("//span[@class='ng-scope']/input[@ng-click='confirmAdd()']"), _timeout, false))
            {
                try
                {
                    _driver.FindElement(By.XPath("//span[@class='ng-scope']/input[@ng-click='confirmAdd()']")).Click();
                }
                catch { }   // Do nothing, continue...
            }

            // Manual Access
            _driver.FindElement(By.XPath("//input[@ng-click='selectManualAssign()']")).Click();
            Sleep(500);
            _driver.FindElement(By.XPath("//input[@ng-click='saveAccess()']")).Click();
            Sleep(8000);

            string username = _driver.FindElement(By.XPath("//div[text()='Username:']/../../td/span[@class='outputText ng-binding']")).Text;
            string password = _driver.FindElement(By.XPath("//td[text()='Password:']/../td/span[@class='outputText ng-binding']")).Text;

            // Will become obsolete
            switch (loginID)
            {
                case 0:
                    employee.hdnet.username = username;
                    employee.hdnet.password = password;
                    employee.hdnet.addDate = DateTime.Now.ToShortDateString();
                    employee.hdnet.addBy = admin.fullname;
                    break;
                case 1:
                    employee.hdnet.username = username;
                    employee.hdnet.password = password;
                    employee.hdnet.addDate = DateTime.Now.ToShortDateString();
                    employee.hdnet.addBy = admin.fullname;
                    break;
                case 2:
                    employee.hdnet1.username = username;
                    employee.hdnet1.password = password;
                    employee.hdnet1.addDate = DateTime.Now.ToShortDateString();
                    employee.hdnet1.addBy = admin.fullname;
                    break;
                case 3:
                    employee.hdnet2.username = username;
                    employee.hdnet2.password = password;
                    employee.hdnet2.addDate = DateTime.Now.ToShortDateString();
                    employee.hdnet2.addBy = admin.fullname;
                    break;
                case 4:
                    employee.hdnet3.username = username;
                    employee.hdnet3.password = password;
                    employee.hdnet3.addDate = DateTime.Now.ToShortDateString();
                    employee.hdnet3.addBy = admin.fullname;
                    break;
                default:
                    employee.hdnet.username = username;
                    employee.hdnet.password = password;
                    employee.hdnet.addDate = DateTime.Now.ToShortDateString();
                    employee.hdnet.addBy = admin.fullname;
                    break;

            }
            
            // Does not take into consideration of above switch... needs bug fixin'
            /*
            if (employee.hdnet.username == "" || employee.hdnet.username == null)
            {
                Log("Error grabbing credentials. Please note the credintials from screenshot log!");
                Screenshot();
                MessageBox.Show("Error grabbing credentials. Please note the credintials from screenshot log!");
            }
            */

            
            Status("Succefully created " + "HDNet");
            return true;
        }

        public bool HDNetRemoveUser(string username)
        {
            if (HDNetCheckUser(username) == false) {
                Log("Nothing to Remove...");
                return true; 
            }
            Status("Removing HDNNet account...");
            _driver.FindElement(By.XPath("//td[contains(text(),'" + username + "')]/../td/span/a")).Click();
            //Sleep(3000);
            GoToURL(_driver.Url + "/edit");
            Sleep(3000); // A MUST!!
            //Set "End Date" using Calander... span...
            Status("Setting termination date on calander...");
            _driver.FindElement(By.XPath("(//a[@ng-click='toggleCalendar()'])[1]/div")).Click();
            Sleep(500);
            _driver.FindElement(By.XPath("//input[@on-tab='onDateInputTab()']")).SendKeys(DateTime.Today.ToShortDateString() + OpenQA.Selenium.Keys.Enter);
            _driver.FindElement(By.XPath("//input[@ng-click='submituserEdit()']")).Click(); ////a[@ng-click='toggleCalendar()']/span


            //MessageBox.Show("DEBUG HALT! * finish manually and program the rest!");
            Sleep(3000);

            // Pop-up alert - Simply asks to confirm termination dates
            try
            {
                Status("Accepting pop-up alert...");
                Sleep(1000);
                _driver.SwitchTo().Alert().Accept();

            }
            catch
            {
                Log("interraction failed, contact administration...");
                return false;
            }
            Sleep(20000);
            GoToURL("https://www.h-dnet.com/system/sling/logout.html"); // Must logout incase switching stores!...
            return true;
        }


        public bool HDNetCheckUser(string username)
        {
            if (username == null || username == "")
            {
                Log("HDNetCheckUser() username cannot be blank [0x01]");
                return false; 
            }

            GoToURL("https://www.h-dnet.com/employeemgmt/?dlrNumber=4385#/home");
            Sleep(3000);

            // the best and most accurate way to find an employee on H-DNet is by searching for their user name. 
            // This is spanned over numerous pages. This cycle accounts for a maximum of 5 pages (seems appropriate).
            for (int i = 0; i <= 5; i++)
            {
                if (ElementPresent(_driver, By.XPath("//td[contains(text(), '" + username + "')]"), 3, false))
                {
                    Status(username + " located");
                    return true;
                }
                else
                {
                    //Next page
                    if (_driver.FindElement(By.XPath("//span[contains(text(), 'Next>')]")).Enabled)
                    {
                        try
                        {
                            _driver.FindElement(By.XPath("//span[contains(text(), 'Next>')]")).Click();
                            Sleep(1000);
                        }
                        catch
                        {
                            break;
                        }
                    }
                }
            }

            Log("User not found on HDNet [0x01]");
            return false;
        }

        /*
        public bool HDNetCheckUser_byLastName(string lastname = "")
        {
            GoToURL("https://www.h-dnet.com/employeemgmt/?dlrNumber=4385#/home");
            Sleep(3000);

            if (lastname == "")     //Lets try and guess the last name...
            {

                lastname = employee.GuessLastName();
                Log("Searching by last name " + lastname + "...");
            }

            _driver.FindElement(By.XPath("//input[@name='lastName']")).SendKeys(lastname + OpenQA.Selenium.Keys.Enter);
            //_driver.FindElement(By.XPath("//input[@type='submit']")).Click() ;
            Sleep(3000);

            // Employee Summary 
            if (ElementPresent(_driver, By.XPath("//a[@ng-click='viewUser(user.erglobalId)']"), 5, false))
            {

                if (_driver.FindElement(By.XPath("//a[@ng-click='viewUser(user.erglobalId)']")).Text.ToLower().Contains(lastname.ToLower()))
                {
                    Status("similar user found, verifying...");
                    _driver.FindElement(By.XPath("//a[@ng-click='viewUser(user.erglobalId)']")).Click();
                    Sleep(2000);

                    if (ElementPresent(_driver, By.XPath("//span[contains(text(),'" + employee.email.username + "')]"), 10, false))
                    {
                        Status("User found on HDNet");
                        return true;
                    }
                    else
                    {
                        Log("User not found on HDNet [0x02]");
                    }
                }
                //MessageBox.Show(_driver.FindElement(By.XPath("//a[@ng-click='viewUser(user.erglobalId)']")).Text);
            }

            Log("User not found on HDNet [0x01]");
            return false;
        }
        */

    }
}

