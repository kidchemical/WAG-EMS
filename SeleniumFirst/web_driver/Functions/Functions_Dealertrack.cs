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
        private bool DTLogin()
        {
            if (admin.dealertrack.username == "" || admin.dealertrack.username == null)
            {
                Error("You must configure your credentials in the settings!");
                return false;
            }
            GoToURL("https://ww2.dealertrack.com/classic/USERADMIN");
            if (_driver.Title.Contains("My DealerTrack") || _driver.Title.Contains("User Administration"))
            {
                Status("Sign in DealerTrack success");
                return true;
            }
            else
            {
                Status("Logging in... [" + admin.dealertrack.username + ":" + admin.dealertrack.password + "]");
                IWebElement usernameInput = _driver.FindElement(By.Id("username"));
                IWebElement next = _driver.FindElement(By.Id("signIn"));
                usernameInput.Clear();
                usernameInput.SendKeys(admin.dealertrack.username);
                Sleep(500);
                next.Click();
                IWebElement passwordInput = _driver.FindElement(By.Id("password"));
                passwordInput.Clear();
                passwordInput.SendKeys(admin.dealertrack.password);
                Sleep(500);
                // IWebElement submitInput = _driver.FindElement(By.Id("signIn"));  //  Submit is carried out through the password element
                Sleep(500);
                passwordInput.Submit();
                //submitInput.Click();

                Sleep(3000);
                /*
                if (_driver.Title.Contains("Signin"))
                {
                    Status("Still logging in...");
                    Sleep(7000);
                    if (_driver.Title.Contains("Signin"))
                    {
                        Error("Could not sign into DealerTrack (0x01)");
                        return false;
                    }
                }*/

                if (_driver.Title.Contains("Cox Automotive Signin"))
                {
                    Status("Multi Factor Authentication detected");
                    _driver.FindElement(By.Id("button-verify-by-sms")).Click();

                    string value = "";
                    if (InputBoxClass.InputBox("Multi Factor Authentication", "Multi Factor Authentication code required:", ref value) == DialogResult.OK)
                    {
                        _mfc = value;
                    }
                    else
                    {
                        Error("Multi Factor Authentication failed");
                        return false;
                    }

                    _driver.FindElement(By.Id("input-verification-code")).SendKeys(_mfc);
                    _driver.FindElement(By.Id("button-account-recovery-submit")).Click();
                    Status("MFA submitted...");
                }

                if (ElementPresent(_driver, By.ClassName("bb-initials"), _timeout, false))
                {
                    Status("Sign in DealerTrack success");
                    return true;
                }
                else
                {
                    //MessageBox.Show(_driver.Title);
                    Error("Could not sign into DealerTrack (0x02)");
                    return false;
                }
            }
        }

        private bool DTAddUser()
        {
            //DTAddUser()
            GoToURL("https://ww2.dealertrack.com/classic/USERADMIN");
            if (ElementPresent(_driver, By.Id("btnNewUserBig"), _timeout))
            {
                _driver.FindElement(By.Id("btnNewUserBig")).Click();
            }
            else
            {
                Error("Locating 'New User' button failed. Maybe login failed?");
                return false;
            }
            // Dealership New User - Page 1
            Status("Processing DealerTrack page 1...");

            //Fill out new user info
            IWebElement first = _driver.FindElement(By.Id("txtFirstName"));
            IWebElement last = _driver.FindElement(By.Id("txtLastName"));
            IWebElement job = _driver.FindElement(By.Id("cboTitle"));
            IWebElement dealer = _driver.FindElement(By.Id("cboDefaultDealership"));
            IWebElement timezone = _driver.FindElement(By.Id("cboTimeZone"));
            IWebElement phoneA = _driver.FindElement(By.Id("phoneA"));
            IWebElement phoneB = _driver.FindElement(By.Id("phoneB"));
            IWebElement phoneC = _driver.FindElement(By.Id("phoneC"));
            IWebElement txtEmail = _driver.FindElement(By.Id("txtEMail"));
            IWebElement id = _driver.FindElement(By.Id("txtEmployeeID"));
            IWebElement agree = _driver.FindElement(By.Id("chkCertification"));
            IWebElement submit = _driver.FindElement(By.Id("cmdSubmit"));
            first.SendKeys(employee.firstname);
            last.SendKeys(employee.lastname);

            // Select the Dealership Store Location
            new SelectElement(dealer).SelectByValue(employee.dealertrack.storeID);
            // Select the Position Role
            new SelectElement(job).SelectByValue(employee.dealertrack.roleID);
            // Select the Time-zone
            new SelectElement(timezone).SelectByValue(_timezone);

            phoneA.SendKeys(_phoneA);
            phoneB.SendKeys(_phoneB);
            phoneC.SendKeys(_phoneC);
            txtEmail.SendKeys(employee.email.username);
            id.SendKeys(employee.employeenumber);
            agree.Click();
            submit.Click();
            //Sleep(10000);

            // Dealership Access - Page 2
            Status("Processing DealerTrack page 2...");
            if (!ElementPresent(_driver, By.Name("dlrID"), _timeout)) { Error("DealerTrack for this user seems to already exist! (0x01)"); return false; }
            //Give Golden State access to both stores
            if (employee.store.Contains("Golden State Nissan")) { _driver.FindElement(By.XPath("//input[@value='493934']")).Click(); }
            if (employee.store.Contains("Golden State Infiniti")) { _driver.FindElement(By.XPath("//input[@value='794104']")).Click(); }
            //if (ElementPresent(driver, By.XPath("//span[@class='required']"))) { Error("User seems to already exist! (0x02)"); return false; }
            submit = _driver.FindElement(By.Name("cmdSubmit"));
            submit.Click();

            // F & I Permissions - Page 3
            Status("Processing DealerTrack page 3...");
            if (!ElementPresent(_driver, By.Id("cboTitle"), _timeout)) { return false; }
            if (cb_com.Checked)
            {
                Status("Setting .com permissions...");
                _driver.FindElement(By.XPath("//input[@id='chkAllPermissions']")).Click();
                Sleep(100); // May not be required...
            }
            submit = _driver.FindElement(By.Name("cmdSubmit"));
            submit.Click();

            // DMS Permissions - Page 4  ?cboRole_104531
            Status("Processing DealerTrack page 4...");
            if (!ElementPresent(_driver, By.CssSelector("select[id*=cboRole]"), _timeout)) { return false; }
            submit = _driver.FindElement(By.Name("cmdSubmit"));
            submit.Click();

            // Success - Page 5
            Status("Success! Processing DealerTrack page 5...");
            if (!ElementPresent(_driver, By.XPath("//img[@src='/images/icons/icon-success-lg.gif']"), _timeout)) { return false; }
            submit = _driver.FindElement(By.CssSelector("a[onclick*='SentEmail()']"));
            //submit = driver.FindElement(By.XPath("//a[@onclick='SentEmail()']"));
            IWebElement dealertrackPassword = _driver.FindElement(By.Id("pssw"));
            employee.dealertrack.password = dealertrackPassword.GetAttribute("value");
            Status("Credentials emailed to " + employee.email.username); //<input id="pssw" name="pssw" value="fvkp5418" type="hidden"> hidden element that contains password
            submit = _driver.FindElement(By.CssSelector("a[href*='ReadOnly']"));
            //submit = driver.FindElement(By.XPath("a[contains(@href,'ReadOnly')]"));
            submit.Click();

            // Grab Username the Easy way... Page 6
            if (!ElementPresent(_driver, By.XPath("//span[@id='txtLoginID']"), _timeout)) { return false; }
            employee.dealertrack.username = _driver.FindElement(By.XPath("//span[@id='txtLoginID']")).Text.Trim();

            Status("DealerTrack Login - " + employee.dealertrack.username + ":" + employee.dealertrack.password);
            return true;
        }

        private bool DTRemoveUser(string username)
        {
            bool alreadyInactive = false;
            if (DTCheckUser(username) == false)
            {
                Log(username + " not found in DealerTrack! Nothing to remove.");
                return true;
            }

            _driver.FindElement(By.XPath("(//tr/td/a[text()= '" + username + "']/../../td/a)[1]")).Click();

            //
            // NOTE NOTE NOTE NOTE NOTE NOTE NOTE NOTE NOTE NOTE NOTE 
            //
            //
            // We are no longer DELETING users, only marking inactive. :)
            //_driver.FindElement(By.XPath("//button[@id='cmdDelete']")).Click();
            //_driver.SwitchTo().Alert().Accept();



            #region Old method which edits user info
            /*
            _driver.FindElement(By.XPath("//a/img[@alt='Edit']")).Click();
            if (ElementPresent(_driver, By.XPath("//select[@id='cboStatus']"), 5, false))
            {
                //new SelectElement(_driver.FindElement(By.XPath("//select[@id='cboStatus']"))).SelectByValue("I"); // no longer lets us set inactive (9/21/2021)
                new SelectElement(_driver.FindElement(By.XPath("//select[@id='cboStatus']"))).SelectByValue("C");
                _driver.FindElement(By.XPath("//button[@id='cmdSubmit']")).Click();
            }
            else
            {
                Log("Something happened... Trying again....");
                DTRemoveUser(username);
            }
            //_driver.SwitchTo().DefaultContent();
            */
            #endregion

            try
            {
                _driver.FindElement(By.XPath("//button[@id='cmdResetPssw']")).Click();
                if (ElementPresent(_driver, By.XPath("//button[@id='cmdResetBridgeMFA']"), 3, false))
                {
                    _driver.FindElement(By.XPath("//button[@id='cmdResetBridgeMFA']")).Click();
                }
               
                //_driver.FindElement(By.XPath("//button[@id='cmdDelete']")).Click();
            }
            catch
            {

            }

            

            if (ElementPresent(_driver, By.XPath("//td[contains(text(), 'profile has been successfully updated.')]"), _timeout, false))
            {
                Log("DealerTrack user " + username + "removed");
                return true;
            }
            else
            {
                Log("Something happened... Trying again....");
                DTRemoveUser(username);
                return false;
            }
        }

        private bool DTCheckUser(string username)
        {
            if (username == null || username == "" || username.Length <= 5)
            {
                Log("DealerTrack username cannot be blank!");
                return false;
            }

            GoToURL("https://www.dealertrack.com/DTAdministration/User/UserSearch/Search.aspx");
            _driver.FindElement(By.XPath("//input[@id='txtLoginID']")).Clear();
            _driver.FindElement(By.XPath("//input[@id='txtLoginID']")).SendKeys(username);
            Sleep(1000);
            _driver.FindElement(By.XPath("//input[@id='btnSearch']")).Click();
            Sleep(3000);
            if (ElementPresent(_driver, By.XPath("//span[@id='lblMsgNotFound']"), 5, false))    //Fail
            {
                Log(username + " NOT found on DealerTrack");
                return false;
            }
            //Success :: But user is inactive ...
            if (ElementPresent(_driver, By.XPath("//tr/td/a[text()='" + username + "']/../../td[text()='Inactive']"), 1, false))
            {
                Log(username + " found on DealerTrack, but is marked inactive!");
                return false;
            }

            //Success :: UPDATE: Updated to be precise when comparing the string in xPath
            if (ElementPresent(_driver, By.XPath("//td/a[text()='" + username + "']"), 5, false))   // fixed a typo ^.^  
            {
                Status(username + " found on DealerTrack");
                return true;
            }



            Error("An unhandled exception occured when checking DealerTrack user (" + username + "). [0x01]");
            return false;
        }

        private bool DTCheckUserByEmail(string email)
        {
            email = email.Trim().Replace("\r\n", string.Empty)
                .Replace("\n", string.Empty)
                .Replace("\r", string.Empty)
                .Replace(((char)0x2028).ToString(), string.Empty)
                .Replace(((char)0x2029).ToString(), string.Empty);


            if (email == null || email == "" || email.Length <= 5)
            {
                Log("DealerTrack username cannot be blank!");
                return false;
            }

            GoToURL("https://www.dealertrack.com/DTAdministration/User/UserSearch/Search.aspx");
            _driver.FindElement(By.XPath("//input[@id='txtLoginID']")).Clear();
            _driver.FindElement(By.XPath("//input[@id='txtLoginID']")).SendKeys(email);
            Sleep(1000);
            _driver.FindElement(By.XPath("//input[@id='btnSearch']")).Click();
            //Sleep(3000);
            Sleep(1000);
            if (ElementPresent(_driver, By.XPath("//span[@id='lblMsgNotFound']"), 5, false))    //Fail
            {
                Log(email + " NOT found on DealerTrack");
                return false;
            }
            //Success :: But user is inactive ...
            if (ElementPresent(_driver, By.XPath("//tr/td/a[text()='" + email + "']/../../td[text()='Inactive']"), 0, false))
            {
                Log(email + " found on DealerTrack, but is marked inactive!");
                return false;
            }

            //Success :: UPDATE: Updated to be precise when comparing the string in xPath
            if (ElementPresent(_driver, By.XPath("//span[@title='" + email + "']"), 5, false))   // fixed a typo ^.^  
            {
                Status(email + " found on DealerTrack");
                return true;
            }
            else
            {
                Error("An unhandled exception occured when checking DealerTrack user (" + email + "). [0x02]");
                return false;
            }
        }

        private bool DTResetPassword(string username)
        {
            username = username.Trim().Replace("\n", "").Replace("\r", ""); ;
            if (DTCheckUserByEmail(username) == false)
            {
                Log(username + " not found in DealerTrack! Continuing...");
                return true;
            }

            // get here....
            if (username != null || username != "")
            _driver.FindElement(By.XPath("(//*[text()='" + username + "']/../../td/a)[1]")).Click();
            try
            {
                //_driver.FindElement(By.XPath("//button[@id='cmdResetBridgeMFA']")).Click();
                _driver.FindElement(By.XPath("//button[@id='cmdResetPssw']")).Click();
            }
            catch
            {

            }


            if (ElementPresent(_driver, By.XPath("//td[contains(text(), 'profile has been successfully updated.')]"), _timeout, false))
            {
                string password = _driver.FindElement(By.XPath("//td[contains(text(), 'New temporary password')]/span/b")).Text.Trim(); ;

                Log("DealerTrack user " + username + " password has been reset: " + password);

                _driver.FindElement(By.XPath("//a[@onclick='return SentEmail();']")).Click();   // Send temporary password to user...
                Sleep(3000);

                return true;
            }
            else
            {
                Log("Something happened... Trying again... (" + username + ")");
                DTResetPassword(username);
                return false;
            }
        }
    }
}
