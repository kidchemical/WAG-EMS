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
        private bool EmailLogin()
        {
            if (_driver == null) { Error("Driver has failed to start. [0xSlug]"); return false; }

            GoToURL("https://sso.secureserver.net/logout");

            /*
            Log("Trying to bypass webMail detection...");
            //GoToURL("https://sso.secureserver.net/149e9513-01fa-4fb0-aad4-566afd725d1b/2d206a39-8ed7-437e-a3be-862e0f06eea3/fp");
            //Sleep(3000);
            string tokenURL = _driver.FindElement(By.XPath("//iframe[contains(@src, '/fp')]")).GetAttribute("src");
            GoToURL(tokenURL);
            //MessageBox.Show("Debug Halt!");
            Sleep(5000);

            GoToURL("https://sso.secureserver.net/logout");
            Sleep(5000);
            //Status("Executing javascript helper...");
            //((IJavaScriptExecutor)_driver).ExecuteScript("javascript: window.KPSDK = { message: 'KPSDK:DONE:03voAKBXjDG6aRHd0YrrUPZYykRDGHtELF3v4LMFsbPSTc4I84znhasB1kdOglMtQ4mmCXGfm5IaWNVe1gsrwIdg1ASxRazlWgYCDaSjEaGI2CABHwUzy68mp1EnC9BcOZG4XboMzR0BSqFs4FZX1KRlXQa::false:2:1637435035372' }; if (window.parent) { window.parent.postMessage(window.KPSDK.message, '*'); }");
            //Sleep(2000);

            */
            //Status("Filling login form...");

            /*
            if (ElementPresent(_driver, By.XPath("//input[@id='username']"), _timeout))
            {
            */

            /*
            }
            else
            {
                Log("Issues detecting login field...");

            }

            */

            Status("Logging in to webMail CP...");
            if (_domainLogin == "") // Just in case...
            {
                
                if (InputBoxClass.InputBox("WebMail Admin", "Enter WebMail admin login for " + employee.store, ref _domainLogin) != DialogResult.OK)
                {
                    Error("WebMail admin access is required to continue");
                    return false;
                }
            }

            IWebElement usernameInput = _driver.FindElement(By.XPath("//input[@id='username']"));
            IWebElement passwordInput = _driver.FindElement(By.XPath("//input[@id='password']"));
            IWebElement rememberInput = _driver.FindElement(By.XPath("//input[@id='remember-me']"));
            IWebElement submitInput = _driver.FindElement(By.XPath("//button[@id='submitBtn']"));

            if (usernameInput.GetAttribute("value") != _domainLogin)
            {
                Status("Sending username...");
                usernameInput.Clear();
                Sleep(1000);
                usernameInput.SendKeys(_domainLogin);
                Sleep(3000);
            }

            Status("Sending password...");
            passwordInput.SendKeys("large-blank-inbox");
            Sleep(500);
            Status("Logging in...");
            rememberInput.Click();
            submitInput.Click();

            Status("Checking for login success...");
            if (ElementPresent(_driver, By.XPath("//h2[contains(text(), 'a bit unusual...')]"), 5, false))
            {
                Log("Selenium drivers have been detected!");
                
                // Lets just exit...
                return false;

                
                _driver.FindElement(By.XPath("//span[text()='Okay']")).Click();
                _driver.FindElement(By.XPath("//a[contains(text(), 'your username')]")).Click();
                Sleep(3000);
                _driver.Navigate().Back();


                //recursion except the first parts....

                #region RECURSION
                try
                {
                    usernameInput = _driver.FindElement(By.Id("username"));
                    passwordInput = _driver.FindElement(By.Id("password"));
                    rememberInput = _driver.FindElement(By.Id("remember-me"));
                    submitInput = _driver.FindElement(By.Id("submitBtn"));

                    if (usernameInput.GetAttribute("value") != _domainLogin)
                    {
                        Status("Sending username...");
                        usernameInput.Clear();
                        usernameInput.SendKeys(_domainLogin);
                    }
                    Status("Sending password...");
                    passwordInput.SendKeys("large-blank-inbox");
                    //MessageBox.Show(usernameInput.GetAttribute("value"));
                    Sleep(500);
                    Status("Logging in...");
                    rememberInput.Click();
                    submitInput.Click();

                    Status("Checking for login success...");
                    if (ElementPresent(_driver, By.XPath("//*[contains(text(), 'a bit unuaul')]"), 5, false))
                    {
                        Log("Reattempt has failed! Resetting...");
                        EmailLogin();

                    }
                }
                catch
                {
                    EmailLogin();
                }
                #endregion
            }


            if (ElementPresent(_driver, By.XPath("//span[text()='Okay']"), 5, false))
            {
                _driver.FindElement(By.XPath("//span[text()='Okay']")).Click();
                Sleep(1000);
            }
            GoToURL("https://wcc.secureserver.net/email?cmd=planlistplan&selectedplan=all&override=1");
            while (!_driver.Title.ToLower().Contains("workspace control center"))
            {
                this.Refresh();
                Sleep(1000);
            }

            Status("Login success!");
            Sleep(500); //is this required?
            return true;
        }

        private bool EmailAddUser()
        {
            if (EmailCheckUser(employee.email.username) == true)
            {

            }


            IWebElement createButton = _driver.FindElement(By.XPath("//a[@id='createButton']"));
            createButton.Click();
            Status("Adding email account...");

            if (!ElementPresent(_driver, By.XPath("//label[@for='createAccountEmailAddress']"), _timeout, false))
            {
                Error("Could not locate label: @for='createAccountEmailAddress' [0xWoodpecker]");
                return false;
            }

            Status("Filling input fields...");
            IWebElement emailInput = _driver.FindElement(By.XPath("//input[@id='createAccountEmailAddress']"));
            IWebElement password1Input = _driver.FindElement(By.XPath("//input[@id='createAccountPassword1']"));
            IWebElement password2Input = _driver.FindElement(By.XPath("//input[@id='createAccountPassword2']"));
            //IWebElement saveInput = driver.FindElement(By.Id("saveButton"));
            //IWebElement saveInput = driver.FindElement(By.ClassName("create-account-button"));
            emailInput.SendKeys(employee.email.username); //TEMPORARY
            password1Input.SendKeys(employee.email.password); //TEMPORARY
            password2Input.SendKeys(employee.email.password); //TEMPORARY

            //Status("Sleeping to prevent driver crash...");
            Sleep(1000, false);    //required to prevent crashing

            if (ElementPresent(_driver, By.Id("saveButton"), 5))
            {
                try
                {
                    Status("Submitting...");
                    IWebElement saveInput = _driver.FindElement(By.Id("saveButton"));
                    saveInput.Click();
                    //saveInput.Submit();
                }
                catch
                {
                    Error("Clicking Save failed! [0xAnt]");
                    return false;
                }
            }

            // Error Detection
            //Status("Sleeping to prevent driver crash...");
            Sleep(5000);    //might be required to prevent false error flag
            Status("Looking for Error notification...");
            if (ElementPresent(_driver, By.Id("componentCreateAccountErrorDomain"), alert: false))
            {
                if (_driver.FindElement(By.Id("componentCreateAccountErrorDomain")).Text != null)   // STALE REQUESTS?
                {
                    if (_driver.FindElement(By.Id("componentCreateAccountErrorDomain")).Text.Contains("already in use"))
                    {
                        Log("Email creation failed - Already in use");
                        if (MessageBox.Show("Email already in use. Would you like to associate the existing email with this employee?" + Environment.NewLine + "(Click YES only if you are 100% sure, password will be reset)", "Email exists", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            //proceed
                            if (MessageBox.Show("Would you like to reset the webMail password for " + employee.email.username + "?", "Reset Password", MessageBoxButtons.YesNo) == DialogResult.Yes )
                            {
                                //change pw
                                _driver.FindElement(By.XPath("//a[@id='cancelButton']")).Click();
                                _driver.FindElement(By.XPath("//a[text()='" + employee.email.username + "']")).Click();
                                Sleep(5000);
                                _driver.FindElement(By.XPath("//input[@id='editAccountPassword1']")).SendKeys(employee.email.password);
                                _driver.FindElement(By.XPath("//input[@id='editAccountPassword2']")).SendKeys(employee.email.password);
                                Sleep(1000);
                                _driver.FindElement(By.XPath("//a[@id='editSaveButton']")).Click();
                                Sleep(5000);
                                Log("Email Password has been changed! [" + employee.email.username + ":" + employee.email.password + "]");
                                return true;
                            }
                            else
                            {
                                //do nothing, continue
                                Log("WebMail password is unkown! Continuing...");
                                employee.email.password = "???";
                                return true;
                            }
                            
                            
                        }
                        else
                        {
                            //abort
                            Error("Email creation aborted - Already in use");
                            return false;
                        }
                    }
                }
            }
            if (ElementPresent(_driver, By.Id("componentCreateAccountErrorMisc"), alert: false))
            {
                if (_driver.FindElement(By.Id("componentCreateAccountErrorMisc")).Text.Contains("No account type checkbox has been selected."))
                {
                    Log("Email creation failed - Domain may be full");
                    if (MessageBox.Show("Domain may be full, would you like to open WebMail cPanel?" + Environment.NewLine + "(Clicking No will abort new hire process)", "Domain Full", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        var process = System.Diagnostics.Process.Start("https://account.secureserver.net/products?plid=257393");
                        process.WaitForExit();
                        EmailAddUser(); //recursion
                    }
                    else
                    {
                        Error("Email creation failed - Domain may be full");
                        return false;
                    }
                }
            }

            Status("Looking for Success notification...");
            //Success Detection
            if (!ElementPresent(_driver, By.ClassName("sf_growl_title"), 10))
            {
                Error("Could not locate growl title (status) [0xHedgehog]!");
                return false;
            }
            if (_driver.FindElement(By.ClassName("sf_growl_title")).Text.ToLower().Contains("success"))   //<div class="sf_growl_title">Success</div>
            {
                Progress("Success! - Email created " + employee.email.username + " : " + employee.email.password);
                employee.email.addDate = DateTime.Now.ToShortDateString();
                employee.email.addBy = admin.fullname;
                Log("Sleeping for 60 seconds to ensure email account is initiated.");
                Sleep(60000);   //Sleep for 60 seconds...
                return true;
            }

            Log("Probable Success! - Email created (Please verify, as an exception was unhandled)");
            Log("Sleeping for 90 seconds to ensure email account is initiated.");
            Sleep(90000);   //Sleep for 60 seconds...
            return true;
        }

        private bool EmailRemoveUser(string username, bool forward = false, string forwardAddress = "")
        {
            if (EmailCheckUser(username) == false)
            {
                Log(username + " not found in WebMail!");
                //Log(_driver.PageSource);
                return true;
            }

            _driver.FindElement(By.XPath("//div/a[contains(text(), '" + username + "')]/../../../td[@class='showChevron']/input[@type='checkbox']/..")).Click();
            Sleep(2000);
            //MessageBox.Show("CLOSE EVERYTHING NOW");
            _driver.FindElement(By.XPath("//li[@class='delete']/a[@class='actionButton']")).Click();
            Sleep(2000);
            _driver.FindElement(By.XPath("//div[@class='popin-button-container']/a[text()='Ok']")).Click();
            Sleep(3000);

            if (ElementPresent(_driver, By.XPath("//div[@id='componentConfirmationDiv']"), _timeout, false))
            {
                if (!_driver.FindElement(By.XPath("//div[@id='componentConfirmationDiv']")).Text.Contains("The specified accounts have been marked for deletion."))
                {
                    Error("Could not verify WebMail removal!");
                    return false;

                }
            }
            else
            {
                Error("Could not locate Delete popup alert [0x03]");
                return false;
            }

            Log(username + " removed from WebMail");
            
            if (forward == true) // || employee.department == "Sales"
            {
                Log("Sleeping for 60 seconds to ensure email is removed before forwarding..."); // WebMail servers begain to process forward addresses slower...
                Sleep(60000);

                _driver.FindElement(By.XPath("//a[@onclick='WCC.components.componentConfirmation.submitOk(); return false;']")).Click();
                Status("Creating forwarding address...");
                _driver.FindElement(By.XPath("//div[contains(text(),'Create Forward')]/..")).Click();
                _driver.FindElement(By.XPath("//input[@id='forwardThisAddress']")).SendKeys(username + OpenQA.Selenium.Keys.Tab);
                /*
                string input = "";
                if (InputBoxClass.InputBox("Foward Address", "Enter the email address you would like to forward all incoming mail from " + username + " to:", ref input) != DialogResult.OK)
                {
                    Log("Could not setup Forwarding address for " + username);
                }
                */
                _driver.FindElement(By.XPath("//textarea[@id='createForwardToAddresses']")).SendKeys(", " + forwardAddress);
                Sleep(500);
                _driver.FindElement(By.XPath("//a[@id='saveButton']")).Click();
                if (_driver.FindElement(By.XPath("//div[@class='sf_growl_title'][contains(text(), 'Success')]")).Displayed)
                {
                    Status("Email forwarding success! (" + forwardAddress + ")");
                    return true;
                }
                else
                {
                    Log("Email forward error, please verify [0x01]");
                    return false;
                }
            }

            return true;

            
        }

        private bool EmailCheckUser(string username)
        {
            Status("Checking for email...");
            try
            {
                _driver.FindElement(By.XPath("//input[@id='searchField']")).SendKeys(username);
            }
            catch
            {
                Error("Error has occured [0xTuna]");
            }
            Sleep(1000);
            _driver.FindElement(By.XPath("//input[@id='search-button']")).Click();
            Sleep(5000);
            if (ElementPresent(_driver, By.XPath("//div/a[contains(text(), '" + username + "')]"), 10, false))
            {
                Status(username + " found on webMail!");
                return true;
            }
            else
            {
                Status(username + " NOT found on webMail!");
                return false;
            }
        }
    }
}
