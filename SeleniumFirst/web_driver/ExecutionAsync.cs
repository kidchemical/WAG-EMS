/* To Do:
    -Choose XPath OR CssSelector, should have consistency
    -Accurate 'ChromeDriver.exe' termination
    -LaunchReynoldsExecution
    -LaunchDmsExecution
    -LaunchPortalExecution
*/

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Data.SqlClient;

namespace SeleniumFirst
{
    public partial class Form1
    {
        

        private bool LaunchBrowserExecution(bool show, ListViewItem person)
        {
            InitializeBrowser(show);
            /////////////////////////////////////////////////////////////////////////////////////////////////////
            ///     EXECUTION PROCESSES
            /////////////////////////////////////////////////////////////////////////////////////////////////////


            if ((cb_storeEmail.Checked || cb_wiseEmail.Checked) && state)
               LaunchEmailExecutionAsync();
            if (cb_dealerTrack.Checked && state)
                state = LaunchDealerTrackExecution();
           /*
            if (cb_com.Checked && state)
                state = LaunchDmsExecution();
           */
            if (cb_reynolds.Checked && state)
                state = LaunchReynoldsExecution();
            if (state)
            {
                state = LaunchPortalExecution();
            }
            if (state)
            { 
                //SQL Database Entry
                Status("Submitting to Employee database... ");
                Sql_io sql = new Sql_io();
                sql.Write_DB(employee, admin);

                // SpreadhSheet Login Sheet Creation 
                Status("Creating user login sheet... ");
                Excel_io excel = new Excel_io();
                excel.Write_Excel(employee, admin);
            }


            /////////////////////////////////////////////////////////////////////////////////////////////////////
            ///     FINISH EXECUTION
            /////////////////////////////////////////////////////////////////////////////////////////////////////
            if (state)
            {
                person.Text = "Done";
                person.ImageIndex = 2;
            }
            else
            {
                person.Text = "Failed";
                person.ImageIndex = 3;
            }
            Status("Closing driver...");
            _driver.Quit();
            tb_Progress.Value = 0;
            tb_Progress.MarqueeAnimationSpeed = 0;
            Progress("::DONE::");
            return true;

        }

        private bool LaunchPortalExecution()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Directory.GetCurrentDirectory() + @"\resources\portals.xml");
            /*  Handled inside NNAExecution()
            if (tv_portals.Nodes[0].Nodes[0].Checked && state == true) { MessageBox.Show("YOo1"); }
            if (tv_portals.Nodes[0].Nodes[1].Checked && state == true) { MessageBox.Show("YOo2"); }
            if (tv_portals.Nodes[0].Nodes[2].Checked && state == true) { MessageBox.Show("YOo3"); }
            if (tv_portals.Nodes[0].Nodes[3].Checked && state == true) { MessageBox.Show("YOo4"); }
            if (tv_portals.Nodes[0].Nodes[4].Checked && state == true) { MessageBox.Show("YOo"); }
            if (tv_portals.Nodes[0].Nodes[5].Checked && state == true) { MessageBox.Show("YOo"); }
            if (tv_portals.Nodes[0].Nodes[6].Checked && state == true) { MessageBox.Show("YOo"); }
            if (tv_portals.Nodes[0].Nodes[7].Checked && state == true) { MessageBox.Show("YOoL"); }*/

            if (tv_portals.Nodes[0].Checked && state == true) { state = LaunchNNA(xmlDoc); }
            if (tv_portals.Nodes[1].Checked && state == true) { state = LaunchDealerConnect(xmlDoc); }
            if (tv_portals.Nodes[2].Checked && state == true) { state = LaunchGMGlobal(xmlDoc); }
            if (tv_portals.Nodes[3].Checked && state == true) { state = LaunchHyundaiDealer(xmlDoc); }
            if (tv_portals.Nodes[4].Checked && state == true) { state = LaunchKDealer(xmlDoc); }
            if (tv_portals.Nodes[5].Checked && state == true) { state = LaunchHDNet(xmlDoc); }
            if (tv_portals.Nodes[6].Checked && state == true) { state = LaunchVCC(xmlDoc); }
            if (tv_portals.Nodes[7].Checked && state == true) { state = LaunchMXConnect(xmlDoc); }
            if (tv_portals.Nodes[8].Checked && state == true) { state = LaunchCUDL(xmlDoc); }
            if (tv_portals.Nodes[9].Checked && state == true) { state = LaunchOffice365(xmlDoc); }

            return state;
        } 

        private bool LaunchEmailExecutionAsync()
        {
            //status("Creating Email: " + employee.email.username + " Password: " + employee.email.password);
            try
            {
                GoToURL("https://wcc.secureserver.net/");
                //status("Filling login form...");
                Sleep(500);
                IWebElement usernameInput = _driver.FindElement(By.Id("username"));
                IWebElement passwordInput = _driver.FindElement(By.Id("password"));
                IWebElement rememberInput = _driver.FindElement(By.Id("remember-me"));
                IWebElement submitInput = _driver.FindElement(By.Id("submitBtn"));

                if (usernameInput.GetAttribute("value") != _domainLogin)
                {
                    //status("Sending username...");
                    usernameInput.Clear();
                    usernameInput.SendKeys(_domainLogin);
                }
                //status("Sending password...");
                passwordInput.SendKeys("large-blank-inbox");
                //MessageBox.Show(usernameInput.GetAttribute("value"));
                Sleep(500);
                //status("Logging in...");
                rememberInput.Click();
                submitInput.Click();

                //status("Checking for login success...");

                while (!_driver.Title.ToLower().Contains("workspace control center"))
                {
                    this.Refresh();
                    Sleep(500);
                }

                //status("Login success!");
                Sleep(500); //is this required?
                //GoToURL("https://wcc.secureserver.net/");
                IWebElement createButton = _driver.FindElement(By.Id("createButton"));
                createButton.Click();
                //status("Looking for input fields...");

                if (!ElementPresent(_driver, By.Id("createAccountEmailAddress"), 10)) return false;

                //status("Filling input fields...");
                IWebElement emailInput = _driver.FindElement(By.Id("createAccountEmailAddress"));
                IWebElement password1Input = _driver.FindElement(By.Id("createAccountPassword1"));
                IWebElement password2Input = _driver.FindElement(By.Id("createAccountPassword2"));
                //IWebElement saveInput = driver.FindElement(By.Id("saveButton"));
                //IWebElement saveInput = driver.FindElement(By.ClassName("create-account-button"));
                emailInput.SendKeys(employee.email.username); //TEMPORARY
                password1Input.SendKeys(employee.email.password); //TEMPORARY
                password2Input.SendKeys(employee.email.password); //TEMPORARY
                
                ////status("Sleeping to prevent driver crash...");
                Sleep(1000, false);    //required to prevent crashing

                if (ElementPresent(_driver, By.Id("saveButton"), 5))
                { 
                    try
                    {
                        //status("Saving Input...");
                        IWebElement saveInput = _driver.FindElement(By.Id("saveButton"));
                        saveInput.Click();
                        //saveInput.Submit();
                    }
                    catch
                    {
                        //error("Clicking Save failed");
                        return false;
                    }
                }

                // Error Detection
                ////status("Sleeping to prevent driver crash...");
                Sleep(3000, false);    //might be required to prevent false error flag
                //status("Looking for Error notification...");
                if (ElementPresent(_driver, By.Id("componentCreateAccountErrorDomain"), alert: false))
                {

                    if (_driver.FindElement(By.Id("componentCreateAccountErrorDomain")).Text.Contains("already in use"))
                    {
                        //error("Email creation failed - Already in use");
                        return false;
                    }
                }
                if (ElementPresent(_driver, By.Id("componentCreateAccountErrorMisc"), alert: false))
                {
                    if (_driver.FindElement(By.Id("componentCreateAccountErrorMisc")).Text.Contains("No account type checkbox has been selected."))
                    {
                        //error("Email creation failed - Domain may be full");
                        return false;
                    }
                }

                //status("Looking for Success notification...");
                //Success Detection
                if (!ElementPresent(_driver, By.ClassName("sf_growl_title"), 10)) return false;
                if (_driver.FindElement(By.ClassName("sf_growl_title")).Text.ToLower().Contains("success"))   //<div class="sf_growl_title">Success</div>
                {
                    Progress("Success! - Email created");
                    employee.email.addDate = DateTime.Now.ToString();
                    return true;
                }

                //log("Probable Success! - Email created (Please verify, as an exception was unhandled)");
                employee.email.addDate = DateTime.Now.ToString();
                return true;
            }
            catch (Exception e)
            {
                //error("Email execution failed" + Environment.NewLine + e.ToString());
                return false;
            }

        }

        private async Task<bool> LaunchEmailAsync()
        {
            Status("Email Async...");
            bool state = await Task.Run(() => LaunchEmailExecutionAsync());
            Status("Post Async...");
            return state;
        }

        private bool LaunchReynoldsExecution()
        {
            Log("Reynolds is not implemented. Contact Administration.");
            return true;
        }

        // To Do : Needs com permission function
        private bool LaunchDealerTrackExecution()
        {
            Status("Creating DealerTrack...");
            GoToURL("https://ww2.dealertrack.com/classic/USERADMIN");
            if (_driver.Title.Contains("My DealerTrack") || _driver.Title.Contains("User Administration"))
            {
                Status("Sign in DealerTrack success");
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
                if (_driver.Title.Contains("Signin"))
                {
                    Status("Still logging in...");
                    Sleep(7000);
                    if (_driver.Title.Contains("Signin"))
                    {
                        Error("Could not sign into DealerTrack (0x01)");
                        return false;
                    }
                }

                if (_driver.Title.Contains("MFA"))
                {
                    Status("Multi Factor Authentication detected");
                    //sms - smssetup
                    //email - emailsetup
                    IWebElement sms = _driver.FindElement(By.Id("smssetup"));
                    sms.Click();
                    IWebElement sendcode = _driver.FindElement(By.Id("SendSmsCode"));
                    sendcode.Click();


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

                    IWebElement code = _driver.FindElement(By.Id("PassCodeTextSms"));
                    code.SendKeys(_mfc);
                    IWebElement verify = _driver.FindElement(By.Id("ActivateSms"));
                    verify.Click();
                }

                if (ElementPresent(_driver, By.ClassName("bb-user-dealer-name"), _timeout))
                {
                    Status("Sign in DealerTrack success");
                    GoToURL("https://ww2.dealertrack.com/classic/USERADMIN");
                }
                else
                {
                    MessageBox.Show(_driver.Title);
                    Error("Could not sign into DealerTrack (0x02)");
                    return false;
                }     
            }

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
            employee.dealertrack.username = _driver.FindElement(By.XPath("//span[@id='txtLoginID']")).Text;

            Status("DealerTrack Login - " + employee.dealertrack.username + ":" + employee.dealertrack.password);
            employee.dealertrack.addDate = DateTime.Now.ToString();
            return true;
        }

    }
}
