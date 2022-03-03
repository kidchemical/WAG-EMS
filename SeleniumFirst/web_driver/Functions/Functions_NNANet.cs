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
        public bool NNANetLogin()
        {
            string title = "NNANet";
            FetchURL(title);
            //Sleep(1000);
            GoToURL(_url);

            /* Depricated Quarterly Cutoff Detector
            int thisMonth = DateTime.Now.Month;
            if (thisMonth == 3 || thisMonth == 6 || thisMonth == 9 || thisMonth == 12)
            {
                Log("::END OF QTR:: No NNA changes during end of QTR :: Override?");
                if (MessageBox.Show("No NNA changes are to be made during the end of the quarter. Would you like to override?", "No change NNA", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Log("Overriding...");
                    //continue...
                }
                else
                {
                    Log("Canceling NNA execution...");
                    return false;
                }
            }*/


            // Log in to Administrator
            Status("Logging into NNANet...");
            IWebElement username = _driver.FindElement(By.XPath("//input[@placeholder='Username']"));
            IWebElement password = _driver.FindElement(By.XPath("//input[@placeholder='Password']"));
            username.SendKeys(admin.nnanet.username);
            password.SendKeys(admin.nnanet.password);
            IWebElement submit = _driver.FindElement(By.XPath("//input[@type='submit']"));
            submit.Submit();

            // Redirect => Admin Page
            Status("Checking loggin status...");
            if (!ElementPresent(_driver, By.XPath("//span[@class='icon-bar']"), _timeout)) { return false; }
            Status("Login Successfully!");

            return true;
        }

        public bool NNANetAddUser()
        {
            NNANetChangeStore(employee.store);

            // Create User Page
            Status("Filling out new user form...");
            Sleep(1000);
            IWebElement firstName = _driver.FindElement(By.Id("corp-dealer-usr-fName"));
            IWebElement lastName = _driver.FindElement(By.Id("corp-dealer-usr-lName"));
            IWebElement primaryPhone = _driver.FindElement(By.Id("corp-dealer-usr-primPhone"));
            IWebElement primaryEmail = _driver.FindElement(By.Id("corp-dealer-usr-primEmail"));
            IWebElement zip = _driver.FindElement(By.Id("corp-dealer-usr-zip"));
                    // -----IWebElement state = _driver.FindElement(By.XPath("//select[@id='corp-dealer-usr-state']"));
            //IWebElement state = _driver.FindElement(By.XPath("//select[@class='selectArrowNone selectCustArrow1 required']")); //xpath fails for some reason
            IWebElement city = _driver.FindElement(By.Id("corp-dealer-usr-city"));
            IWebElement address = _driver.FindElement(By.Id("corp-dealer-usr-address"));
            IWebElement position = _driver.FindElement(By.Id("corp-dealer-usr-accInfo-position"));
            //IWebElement accessLevel = _driver.FindElement(By.Id("corp-dealer-usr-accInfo-accessLevel")); // Access Level doesnt seem required?
            IWebElement workPhone = _driver.FindElement(By.Id("corp-dealer-usr-accInfo-workPhone"));
            IWebElement workEmail = _driver.FindElement(By.Id("corp-dealer-usr-accInfo-workEmail"));
            IWebElement employeeNumber = _driver.FindElement(By.Id("corp-dealer-usr-accInfo-dealerEmpNo"));
            IWebElement hireDate = _driver.FindElement(By.Id("corp-dealer-usr-accInfo-hireDate")); // Non-text input

            try
            {
                firstName.SendKeys(employee.firstname);
                lastName.SendKeys(employee.lastname);
                primaryPhone.SendKeys(_phoneA + _phoneB + _phoneC);
                primaryEmail.SendKeys(employee.email.username);
                zip.SendKeys(_zip);
                string nnaState;   // Convert State to NNA values
                switch (_state)
                {
                    case "CA": nnaState = "19"; break;
                    case "AZ": nnaState = "18"; break;
                    case "NV": nnaState = "50"; break;
                    default: Error("Unhandled exception choosing a State when creating NNANet"); return false;
                }
                Sleep(250); // BUG: State doesnt select without sleeping
                new SelectElement(_driver.FindElement(By.XPath("//select[@id='corp-dealer-usr-state']"))).SelectByValue(nnaState);
                Sleep(250); // BUG: City has been seen to enter double values
                city.SendKeys(_city);
                Sleep(250); // BUG: City has been seen to enter double values
                address.SendKeys(_address);
                //SetStatus("Setting position to number " + _nna_value + "...");
                Sleep(100); // BUG: City has been seen to enter double values
                try
                {
                    new SelectElement(position).SelectByValue(employee.nnanet.roleID);
                }
                catch
                {
                    string input = "0";
                    InputBoxClass.InputBox("Role", "Enter Role ID", ref input);
                    new SelectElement(position).SelectByValue(input);
                }
                Sleep(100); // BUG: City has been seen to enter double values
                workPhone.SendKeys(_phoneA + _phoneB + _phoneC);
                workEmail.SendKeys(employee.email.username);
                employeeNumber.SendKeys(employee.employeenumber);
                hireDate.SendKeys(OpenQA.Selenium.Keys.Enter);      // Close Calender (auto-enters current date)
                hireDate.SendKeys(OpenQA.Selenium.Keys.Enter);      // Might not need twice!!!
            }
            catch
            {
                Error("Could not fill new user field!");
                return AddEmployee_NNAExecution();    //Try again?
            }

            Status("Looking for user ID...");
            if (!ElementPresent(_driver, By.Id("success-createUsrId"), _timeout))
            {
                Error("Cannot locate the users login name! [0x01]");
                return false;
            }
            Sleep(3000);
            if (_driver.FindElement(By.Id("success-createUsrId")).Text == "")
            {
                Sleep(5000);
                if (_driver.FindElement(By.Id("success-createUsrId")).Text == "")
                {
                    Log("Cannot locate the users login name! [0x02]");
                    return AddEmployee_NNAExecution();   //Try again?
                }
            }

            IWebElement nnaNetLogin = _driver.FindElement(By.Id("success-createUsrId"));
            employee.nnanet.username = nnaNetLogin.Text;
            employee.nnanet.username = employee.nnanet.username.Substring(5, employee.nnanet.username.IndexOf("has been successfully created") - 5).Trim();
            employee.nnanet.password = "Emailed";
            Status("Successfully created NNANet account - " + employee.nnanet.username + " : [Password emailed]");

            if (employee.ssn != "" && !employee.department.Contains("Office"))
            {
                Status("Enrolling user into Virutal Academy...");
                GoToURL("https://www.nnanet.com/content/nnanet/us/common/en_US/searchuser.html");
                Sleep(3000);
                if (!ElementPresent(_driver, By.XPath("//input[@id='corp-searchUser-dealer-uName']"), _timeout)) { return false; }
                //if (!ElementPresent(_driver, By.XPath("//input[@id='corp-searchUser-dealer-uName']"), _timeout)) { return false;}
                IWebElement searchUsername = _driver.FindElement(By.XPath("//input[@id='corp-searchUser-dealer-uName']"));
                searchUsername.SendKeys(employee.nnanet.username);
                searchUsername.SendKeys(OpenQA.Selenium.Keys.Enter);
                if (!ElementPresent(_driver, By.XPath("//a[@class='aUnderLn search-popOver-va']"), _timeout)) { return false; }
                IWebElement registerVA = _driver.FindElement(By.XPath("//a[@class='aUnderLn search-popOver-va']"));
                registerVA.Click();

                if (!ElementPresent(_driver, By.XPath("//button[@class='pop-deleteBut search-registerVA']"), _timeout)) { return false; }
                IWebElement ssn = _driver.FindElement(By.XPath("//*[@id='search-register-regVA']"));
                if (employee.ssn == "")
                {
                    // get enmployee ssn
                }
                ssn.SendKeys(employee.ssn);
                ssn = _driver.FindElement(By.XPath("//*[@id='search-reEnter-register-regVA']"));
                ssn.SendKeys(employee.ssn);
                registerVA = _driver.FindElement(By.XPath("//*[@class='pop-deleteBut search-registerVA']"));
                registerVA.Click();
                Sleep(1000);
                if (!ElementPresent(_driver, By.XPath("//*[@class='success-vaReg']"), 3))
                {
                    Log("Could not verify NNANet Virtual Academy enrollment");
                }
                else
                {
                    Status("Success enrolling user into Virutal Academy");
                }
            }
            // Multiple store enrollment    (Blocked out for some reason)
            // TO DO:
            // STOP DOUBLE ENROLLMENT
            if (tv_portals.Nodes[0].Nodes.Count > 1)
            {
                Status("Enrolling in multiple stores...");
                if (tv_portals.Nodes[0].Nodes[0].Checked && employee.store != "Nissan Vacaville" && employee.store != "Wise Auto Group") { NNAnetMultiStore(0); }
                if (tv_portals.Nodes[0].Nodes[1].Checked && employee.store != "North Bay Nissan") { NNAnetMultiStore(1); }
                if (tv_portals.Nodes[0].Nodes[2].Checked && employee.store != "Nissan Yuba City") { NNAnetMultiStore(2); }
                if (tv_portals.Nodes[0].Nodes[3].Checked && employee.store != "Nissan Sacramento") { NNAnetMultiStore(3); }
                if (tv_portals.Nodes[0].Nodes[4].Checked && employee.store != "Infiniti Marin") { NNAnetMultiStore(4); }
                if (tv_portals.Nodes[0].Nodes[5].Checked && employee.store != "Vallejo Nissan") { NNAnetMultiStore(5); }
                if (tv_portals.Nodes[0].Nodes[6].Checked && employee.store != "Golden State Nissan") { NNAnetMultiStore(6); }
                if (tv_portals.Nodes[0].Nodes[7].Checked && employee.store != "Golden State Infiniti") { NNAnetMultiStore(7); }
            }
            return true;
        }

        public bool NNANetRemoveUser(string username)
        {
            Status("Removing " + username + " from NNANet");
            NNANetChangeStore(employee.store);

            

            if (NNANetCheckUser(username) == false) { Log("Could not locate username " + username + " on NNANet."); return false; }

            _driver.FindElement(By.XPath("//div[@data-serchusr-username='" + username + "']/button[contains(@class,'btn-delete')]")).Click();
            //_driver.FindElement(By.XPath("(//button[@id='emNInfo'][text()='Yes'])[2]"));
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(_timeout));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("(//button[@id='emNInfo'][text()='Yes'])[2]"))).Click();
            wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(_timeout));
            try
            {
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//p[contains(text(),'User successfully Deleted')]")));
            }
            catch
            {
                Error("Could not detect success message removing user from NNANet [0x01]");
                //NNANetRemoveUser(username);
                return false;
            }



            //MessageBox.Show("DEBUG HALT!");
            #region DEPRECATED METHOD WHICH USES EDIT PROFILE
            /*
            _driver.FindElement(By.XPath("//div[@data-serchusr-username='" + username + "']/button")).Click();
            if (_showBrowser)
            {
                try
                {
                    _driver.SwitchTo().Window(_driver.WindowHandles[1]);
                    new SelectElement(_driver.FindElement(By.XPath("//select[@id='corp-dealer-editUsr-accInfo-status']"))).SelectByValue("21");
                    _driver.FindElement(By.XPath("//button[text()='Save']")).Click();
                    Sleep(5000);
                    _driver.SwitchTo().Window(_driver.WindowHandles[1]).Close();
                    _driver.SwitchTo().Window(_driver.WindowHandles[0]);
                }
                catch
                {
                    Log("Unhandled exception - Continuing... [0x86]");
                }
            }
            */
            #endregion
            return true;
        }

        public bool NNANetCheckUser(string username)
        {
            GoToURL("https://www.nnanet.com/content/nnanet/us/common/en_US/searchuser.html");
            _driver.FindElement(By.XPath("//input[@id='corp-searchUser-dealer-uName']")).SendKeys(username + OpenQA.Selenium.Keys.Enter);
            //_driver.FindElement(By.XPath("//input[@id='corp-searchUser-dealer-uName']")).Submit();
            if (ElementPresent(_driver,By.XPath("//div[@data-serchusr-username='" + username + "']"), 3, false))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        // XTRA FEATURES
        public bool NNAnetMultiStore(int id, string existingUser = null, bool login = true)
        {
            GenerateRoles(employee.role, employee.store);
            GoToURL("https://www.nnanet.com/content/nnanet/us/common/en_US/createuser.html");
            if (login)   //log in if needed...
            {
                try
                {
                    if (ElementPresent(_driver,By.XPath("//input[@placeholder='Username']"), 10, false))
                    {
                        employee.nnanet.username = existingUser;    // I wonder why I put this here...
                        NNANetLogin();
                    }
                }
                catch
                {
                    // continue...
                }
            }
            //NNANetLogin();
            Status("Redirecting...");
            GoToURL("https://www.nnanet.com/content/nnanet/us/common/en_US/createuser.html");
            Sleep(3000);
            Status("Transfering " + employee.nnanet.username + " to " + tv_nna.Nodes[0].Nodes[id].Text);
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("document.getElementsByClassName('btn btn-group dealership-DD')[1].setAttribute('class', 'open');"); // Open store location by injecting class using JS

            string nna_value;
            switch (id)
            {
                case 0: nna_value = "1180141"; break;   //Nissan Vacaville
                case 1: nna_value = "1180162"; break;   //North Bay Nissan
                case 2: nna_value = "1180172"; break;   //Nissan Yuba City
                case 3: nna_value = "1180169"; break;   //Nissan Sacramento
                case 4: nna_value = "1180168"; break;   //Infiniti Marin
                case 5: nna_value = "1180170"; break;   //Vallejo Nissan
                case 6: nna_value = "1180160"; break;   //Golden State Nissan
                case 7: nna_value = "1180167"; break;   //Golden State Infiniti
                case 8: nna_value = "1180141"; break;   //Wise Auto
                default: nna_value = "1180141"; break;

            }
            IWebElement whichStore = _driver.FindElement(By.XPath("(//a[@data-value='" + nna_value + "'])[2]")); //[2] is for fullscreen, [1] is for mobile view
            if (!ElementClickable(_driver, whichStore))
            {
                Error("Cannot find the proper element for Store Location on NNANet!");
                return false;
            }
            whichStore.Click();
            Sleep(1000);
            if (!ElementPresent(_driver, By.Id("corp-dealer-usr-fName"), _timeout))
            {
                Error("Cannot locate element with ID corp-dealer-usr-fName");
                return false;
            }
            // Create User Page
            Status("Filling out new user form...");
            _driver.FindElement(By.XPath("//a[@class='pull-right retriveUsr']")).Click();
            _driver.FindElement(By.XPath("//input[@id='corp-admin-search-RetrieveUsr']")).SendKeys(employee.nnanet.username);
            _driver.FindElement(By.XPath("//button[@id='get-user-retrieve-username']")).Click();
            Sleep(5000);


            //if (_driver.FindElement(By.Id("corp-dealer-usr-fName")).Text != employee.firstname) { return false; }


            IWebElement position = _driver.FindElement(By.Id("corp-dealer-usr-accInfo-position"));
            //IWebElement accessLevel = _driver.FindElement(By.Id("corp-dealer-usr-accInfo-accessLevel")); // Access Level doesnt seem required?
            IWebElement workPhone = _driver.FindElement(By.Id("corp-dealer-usr-accInfo-workPhone"));
            IWebElement workEmail = _driver.FindElement(By.Id("corp-dealer-usr-accInfo-workEmail"));
            IWebElement employeeNumber = _driver.FindElement(By.XPath("//input[@data-valid='corpAdminDealerUserDlrEmpNo']"));
            IWebElement hireDate = _driver.FindElement(By.Id("corp-dealer-usr-accInfo-hireDate")); // Non-text input

            new SelectElement(position).SelectByValue(employee.nnanet.roleID);
            Sleep(100); // BUG: City has been seen to enter double values



            if (_phoneA != "")
            {
                workPhone.SendKeys(_phoneA + _phoneB + _phoneC);
            }
            else
            {
                workPhone.SendKeys("7074554500");
            }



            if (employee.email.username == "")
            {
                employee.email.username = _driver.FindElement(By.XPath("//input[@id='corp-dealer-usr-primEmail']")).Text;
            }
            workEmail.SendKeys(employee.email.username);



            if (employee.employeenumber == "")
            {
                employee.employeenumber = "777777";
            }
            employeeNumber.SendKeys(employee.employeenumber);



            hireDate.SendKeys(OpenQA.Selenium.Keys.Enter);      // Close Calender (auto-enters current date)
            hireDate.SendKeys(OpenQA.Selenium.Keys.Enter);      // Might not need twice!!!

            Status("Looking for user ID...");
            if (!ElementPresent(_driver, By.Id("success-createUsrId"), _timeout))
            {
                Error("Cannot locate the users login name! 0x03");
                return false;
            }

            Sleep(3000);

            if (_driver.FindElement(By.Id("success-createUsrId")).Text == "")
            {
                Sleep(5000);
                if (_driver.FindElement(By.Id("success-createUsrId")).Text == "")
                {
                    Error("Cannot locate the users login name! 0x04");
                    return false;
                }
            }

            Status("Enrolled in " + tv_portals.Nodes[0].Nodes[id].Text);
            Sleep(15 * 1000);   // Play with the value
            return true;

        }

        public bool NNANetChangeStore(string store)
        {
            Status("Redirecting...");
            GoToURL("https://www.nnanet.com/content/nnanet/us/common/en_US/createuser.html");
            Sleep(3000);

            Status("Changing dealership to " + store + "...");
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("document.getElementsByClassName('btn btn-group dealership-DD')[1].setAttribute('class', 'open');"); // Open store location by injecting class using JS
            string nna_value;
            //  To Do: Set nna_value in stores.xml
            switch (store)
            {
                case "Nissan Vacaville": nna_value = "1180141"; break;
                case "North Bay Nissan": nna_value = "1180162"; break;
                case "Nissan Yuba City": nna_value = "1180172"; break;
                case "Nissan Sacramento": nna_value = "1180169"; break;
                case "Infiniti Marin": nna_value = "1180168"; break;
                case "Vallejo Nissan": nna_value = "1180170"; break;
                case "Golden State Nissan": nna_value = "1180160"; break;
                case "Golden State Infiniti": nna_value = "1180167"; break;
                case "Wise Auto Group": nna_value = "1180141"; break;
                default: nna_value = "1180141"; break;
            }

            IWebElement whichStore = _driver.FindElement(By.XPath("(//a[@data-value='" + nna_value + "'])[2]")); //[2] is for fullscreen, [1] is for mobile view
            if (!ElementClickable(_driver, whichStore))
            {
                Error("Cannot find the proper element for Store Location on NNANet!");
                return false;
            }

            whichStore.Click();
            Sleep(1000);

            // Buggy?
            if (!ElementPresent(_driver, By.Id("corp-dealer-usr-fName"), _timeout))
            {
                return false;
            }

            return true;
        }



    }
}
