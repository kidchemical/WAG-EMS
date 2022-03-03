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
        public bool KDealerLogin()
        {
            Log("KDealer has slow servers! Increasing max timeout...");
            SetTimeOut(180);
            string title = "KDealer";
            FetchURL(title);
            GoToURL(_url);
            
            //Logout (fixes glitch)
            if (ElementPresent(_driver, By.XPath("//a[@class='logout']"), 1, false))
            {
                Status("Logging out of KDealer (fixes bug)");
                //Remove Kia Messages if one exist...
                if (ElementPresent(_driver, By.XPath("//button[@title='Mark as Read']"), 1, false))
                {
                    _driver.FindElement(By.XPath("//button[@title='Mark as Read']/span/span")).Click();
                }
                Sleep(500);
                _driver.FindElement(By.XPath("//a[@title='Profile']")).Click();
                Sleep(500);
                try
                {
                    _driver.FindElement(By.XPath("//a[@class='logout']")).Click();
                }
                catch
                {
                    Error("Could not log out of KDealer, trying again...");
                    state = true;
                    KDealerLogin();
                }
            }

            //Login
            if (ElementPresent(_driver, By.XPath("//input[@title='User Name']"), 10, alert: false)) // maybe instead of _timeout...
            {
                Status("Logging in to KDealer...");
                _driver.FindElement(By.XPath("//input[@title='User Name']")).SendKeys(admin.kdealer.username);
                _driver.FindElement(By.XPath("//input[@title='Password']")).SendKeys(admin.kdealer.password);
                _driver.FindElement(By.XPath("//button[@type='submit']")).Submit();
                Status("Login success!");
                return true;
            }
            else
            {
                Error("Login failure!");
                return false;
            }
            
        }

        public bool KDealerAddUser()
        {
            Status("Adding KDealer");
            //CPanel => Add User
            GoToURL("https://www.kdealer.com/Pages/AddDealerUser.aspx");

            // Remove Kia Messages if one exist...
            if (ElementPresent(_driver, By.XPath("//button[@title='Mark as Read']"), 1, false))
            {
                _driver.FindElement(By.XPath("//button[@title='Mark as Read']/span/span")).Click();
            }

            string kdealer = employee.firstname[0].ToString().ToLower() + employee.lastname.ToLower() + "651";
            //MessageBox.Show("Debug halt [0x123]");
            _driver.FindElement(By.XPath("//input[@id='usrId']")).SendKeys(kdealer);
            _driver.FindElement(By.XPath("//input[@id='firstName']")).SendKeys(employee.firstname);
            _driver.FindElement(By.XPath("//input[@id='lastName']")).SendKeys(employee.lastname);
            _driver.FindElement(By.XPath("//input[@id='email']")).SendKeys(employee.email.username);
            new SelectElement(_driver.FindElement(By.XPath("//select[@onchange='OnDealerShipChange()']"))).SelectByValue("CA305");
            _driver.FindElement(By.XPath("//input[@id='dmsID']")).SendKeys(employee.employeenumber);
            _driver.FindElement(By.XPath("//input[@id='employementDt']")).SendKeys(OpenQA.Selenium.Keys.Enter);
            new SelectElement(_driver.FindElement(By.XPath("//select[@class='jobTitle jobTitleDropDownCheck']"))).SelectByValue(employee.kdealer.roleID);
            _driver.FindElement(By.XPath("//button[@onclick='submitDealerDetails();']")).Click();   // needs test
            Sleep(5000);
            if (!ElementPresent(_driver, By.XPath("//button[@onclick='confirmDealerReg();']"), _timeout)) { return false; }
            if (_driver.FindElement(By.XPath("//h3[@class='showerrorHeader']")).Text.Contains("This User ID has been taken."))
            {
                Log("User appears to be enrolled in KDealer!");
                string tmp = employee.firstname.ToLower() + employee.lastname.ToLower() + "651";
                if (MessageBox.Show("Username " + kdealer + " already exists in KDealer. Would you like to reactivate this account?" + System.Environment.NewLine
                        + "Note: Choosing 'No' will attempt to create account named " + tmp, "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    if (ElementPresent(_driver, By.XPath("//span[text()='Reactivate']"), 0, false))
                    {
                        _driver.FindElement(By.XPath("//span[text()='Reactivate']")).Click() ;
                        Log("DEBUG HALT! (You've reached a point at which the software is not developed)");
                        MessageBox.Show("DEBUG HALT! You've reached a point at which the software is not developed. Please finish KDealer steps THEN press OK.");
                        return true;
                    }
                }
                else
                {
                    kdealer = tmp;
                    Log("User appears to be enrolled in KDealer. Trying " + kdealer);
                    _driver.FindElement(By.XPath("//input[@id='usrId']")).Clear();
                    _driver.FindElement(By.XPath("//input[@id='usrId']")).SendKeys(kdealer + OpenQA.Selenium.Keys.Tab);
                    Sleep(500);
                    _driver.FindElement(By.XPath("//button[@onclick='submitDealerDetails();']/span")).Click();
                    /*
                        if (_driver.FindElement(By.XPath("//h3[@class='showerrorHeader']")).Text.Contains("This User ID has been taken."))
                        {
                            Error("Could not create KDealer. Contact administration.");
                            return false;
                        }
                    */
                }
            }

            Sleep(3000);

            if (ElementPresent(_driver, By.XPath("//h3[contains(text(),'This email has been taken.')]"), 0, false))
            {
                Error(employee.email.username + " already exist on KDealer! (This email has been taken.) Contact administration.");
                return false;
            }
            
            // Duplicate account alert
            if (ElementPresent(_driver, By.XPath("//span[contains(text(), 'Duplicate account. Cancel​')]"), 0, false))
            {
                if (_driver.FindElement(By.XPath("")).Enabled)
                {
                    Screenshot();
                    Log("Duplicate KDeealer account detected! Canceling account creation... ");
                    //MessageBox.Show();
                    return false;
                }
            }

            _driver.FindElement(By.XPath("//button[@onclick='confirmDealerReg();']")).Click();  //Error with browser hidden (non interactable)
            Sleep(5000);
            employee.kdealer.username = kdealer;
            employee.kdealer.password = "Emailed";
            Status("KDealer User ID - [" + kdealer + " : " + employee.kdealer.password + "]");
            Status("Succefully created KDealer");
            SetTimeOut(_timeout);
            return true;
        }

        public bool KDealerRemoveUser(string username)
        {
            if (KDealerCheckUser(username) == true)
            {
                _driver.FindElement(By.XPath("//a[text()='" + username + "']")).Click();
                if (ElementPresent(_driver, By.XPath("//span[text()='Retire']"), 10, false))
                {
                    try
                    {
                        _driver.FindElement(By.XPath("//span[text()='Retire']")).Click();
                    }
                    catch
                    {
                        if (ElementPresent(_driver, By.XPath("//span[text()='Reactivate']"), 0, false))
                        {
                            Log(username + " is already inactive on KDealer! Continuing... [0xCentipede]");
                            return true;
                        }
                    }
                }
                else
                {
                    if (ElementPresent(_driver, By.XPath("//span[text()='Reactivate']"), 0, false))
                    {
                        Log(username + " is already inactive on KDealer! Continuing... [0xCentipede]");
                        return true;
                    }
                    else
                    {
                        Error("Could not locate Retire button [0xRabbit]");
                    }
                }

                /*
                if (ElementPresent(_driver, By.XPath("//button[@id='unlockUsr']"), 3, false))
                {
                    Log(username + " is already inactive on KDealer! Continuing... [0xMilipede]");
                    return true;
                }*/

                _driver.FindElement(By.XPath("//input[@id='ctl00_ctl43_1ea212b5-0028-48b6-beff-c1f63a512ac7_retdate']")).SendKeys(OpenQA.Selenium.Keys.Enter);
                Sleep(3000);
                _driver.FindElement(By.XPath("//span[contains(text(), 'Retire User')]")).Click();
                Sleep(5000);
                if (_driver.FindElement(By.XPath("//span[contains(text(), 'User has been retired. You will  be redirected to Manage Users page.​')]")).Enabled)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                return false;
            }
        }

        public bool KDealerCheckUser(string username)
        {
            GoToURL("https://www.kdealer.com/Pages/ManageUsers.aspx");
            _driver.FindElement(By.XPath("//input[@id='ctl00_ctl43_c0983715-103c-45a6-b369-f9356aa07a48_kmaUserId']")).SendKeys(username);
            _driver.FindElement(By.XPath("//span[text()='Search']")).Click();
            
            if (ElementPresent(_driver, By.XPath("//a[text()='" + username + "']"), 5, false))
            {
                Status(username + " found on KDealer");
                return true;
            }

            if (_driver.FindElement(By.XPath("//div[@class='searchResultDiv']")).Text.Contains("No search results found"))
            {

                Log(username + " NOT found on KDealer");
                return false;
            }

            Error("Unhandled exception: KDealerCheckUser();");
            return false;
        }
    }
}
