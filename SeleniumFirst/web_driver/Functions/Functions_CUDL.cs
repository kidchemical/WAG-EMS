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
        public bool CUDLLogin()
        {
            Status("Logging into CUDL...");
            string title = "CUDL";
            FetchURL(title);
            GoToURL(_url);

            if (ElementPresent(_driver, By.XPath("//span[@id='user-name']"),3, false))
            {
                return true;
            }

            //Login
            _driver.FindElement(By.XPath("//input[@id='email']")).Clear();
            _driver.FindElement(By.XPath("//input[@id='email']")).SendKeys(admin.cudl.username);
            _driver.FindElement(By.XPath("//button[@id='login-btn']")).Click();
            _driver.FindElement(By.XPath("//input[@id='password']")).SendKeys(admin.cudl.password);
            Sleep(3000);
            _driver.FindElement(By.XPath("//button[@id='login-btn']")).Submit();
            Sleep(3000);    // Is this needed?
            Status("Successfully logged into CUDL...");
            return true;
        }

        public bool CUDLAddUser()
        {
            //Create user
            GoToURL(@"https://dealer.cudl.com/Settings/Home");
            Sleep(5000);
            Status("Creating CUDL account...");

            //WebDriverWait wait = new WebDriverWait(_driver, _timeout);
            //wait.until
            //WebDriverWait(driver, 20).until(EC.element_to_be_clickable((By.XPATH, "//*[@id='csv-button']"))).click()
            //((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", _driver.FindElement(By.XPath("")));
            //IWebElement element = _driver.FindElement(By.XPath("//div[@id='footer']"));
            //_driver.SwitchTo().Frame("settings-frame");
                //((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
            //_driver.FindElement(By.XPath("//*[@value='Create New User']")).Click();
            Sleep(1000);
            //((IJavaScriptExecutor)_driver).ExecuteScript("javascript:openDialogUserCheck();");
            _driver.FindElement(By.XPath("//*[@value='Create New User']")).Click(); // Bug : Not detected for some reason
            Sleep(1000);
            ((IJavaScriptExecutor)_driver).ExecuteScript("javascript:openDialogUserCheck();");
                
                _driver.FindElement(By.XPath("//input[@id='userEmail']")).SendKeys(employee.email.username);
                _driver.FindElement(By.XPath("//input[@id='checkForUser']")).Click();   // Click Continue Button





           // User Infromation Tab 
            new SelectElement(_driver.FindElement(By.XPath("//select[@id='Title']"))).SelectByValue("28");  // Chris says: "Always make them a Finance Manager"
            _driver.FindElement(By.XPath("//input[@id='FirstName']")).SendKeys(employee.firstname);
            _driver.FindElement(By.XPath("//input[@id='LastName']")).SendKeys(employee.lastname);
            _driver.FindElement(By.XPath("//input[@id='Phone']")).Click();
            Sleep(1000);
            _driver.FindElement(By.XPath("//input[@id='Phone']")).SendKeys(_phoneA + _phoneB + _phoneC);
            //string fax = "7074554500";
            string fax = _phoneA + _phoneB + _phoneC;
            //InputBoxClass.InputBox("Fax", "Enter Fax Number for " + employee.store, ref fax);
            _driver.FindElement(By.XPath("//input[@id='Fax']")).Click();
            Sleep(1000);
            _driver.FindElement(By.XPath("//input[@id='Fax']")).SendKeys(fax);



            // User Permission Tab
            _driver.FindElement(By.XPath("//a[text()='User Permissions']")).Click();

                    //Input to find location for now....
                    IReadOnlyCollection<IWebElement> elements = _driver.FindElements(By.XPath("//div[@id='dealers']/div[@class='datarow']/span"));
                    List<string> strRoles = new List<string>();
                    foreach (IWebElement element in elements)
                    {
                        strRoles.Add(element.Text);
                    }

                    // DROP DOWN INPUT DIALOG - Job Titles
                    forms.MessageBox_dropdown inputBox = new forms.MessageBox_dropdown("Select the location you wish to assign on CUDL", strRoles);
                    inputBox.ShowDialog();

            try
            {

                string dealerID = _driver.FindElement(By.XPath("//span[text()='" + inputBox.result + "']/..")).GetAttribute("id");
                _driver.FindElement(By.XPath("//input[@id='" + dealerID + ".DealerPortal.CreditUser']")).Click();
                _driver.FindElement(By.XPath("//input[@id='" + dealerID + ".DealerPortal.LendingUser']")).Click();
                _driver.FindElement(By.XPath("//input[@id='" + dealerID + ".DealerPortal.GAPUser']")).Click();
            }
            catch { Error("Could not locate the corresponding dealer in CUDL! " + inputBox.result); }

            

            // Click "Save" Button
            _driver.FindElement(By.XPath("//input[@id='userSave']")).Submit();


            // DONE
            Progress("CUDL created: [" + employee.email.username + ":Emailed]");
            return true;
        }

        public bool CUDLRemoveUser()
        {
            if (CUDLCheckUser(employee.cudl.username) == false)
            {
                Error("Could not remove CUDL user: " + employee.cudl.username);
                return true;
            }

            if (employee.cudl.username != employee.email.username)
            {
                Log("WARNING: Email and CUDL do NOT match! Continue?");
                DialogResult result =  MessageBox.Show("Warning: Email and CUDL do NOT match! Continue anyways?" + Environment.NewLine
                    + "Email: " + employee.email.username + Environment.NewLine
                    + "CUDL: " + employee.cudl.username, "WARNING!", MessageBoxButtons.YesNo);

                if (result != DialogResult.Yes)
                {
                    Error("Stopped removing CUDL!");
                    return false;
                }
                else
                {
                    Status("Continuing...");
                }
            }


            // WARNING : This XPath has the potential to inaccurately select if email.username is blank
            _driver.FindElement(By.XPath("//td[@title='" + employee.email.username + "']/../td/input[@value='Delete']")).Click(); 
            //!!!!!!!!!!!!!!!!!!!!!


                   
            _driver.FindElement(By.XPath("//input[@id='btnUserDeleteOk']")).Click();
            Sleep(10000);

            // if element with div content contains " has been successfully deleted." ...Steven Garrity (sgarrity@vacavilledodge.com) has been successfully deleted.
            //MessageBox.Show("*DEBUG HALT!*");

            Status("Successfully removed CUDL account!");
            return true;

        }

        public bool CUDLCheckUser(string email)
        {
            Status("Checking for user... [" + email + "]");

            if (email.Trim() == "")
            {
                Error("CUDL username cannot be blank! [0xBee]");
                return false;
            }
            GoToURL("https://dealer.cudl.com/CUDL/#/Settings");

            // CUDL displays the users on pages with 25 entries max per page...
            // This code will check for an email on a page, then change pages.
            Status("Switching to iframe... [settings-frame]");
            _driver.SwitchTo().ParentFrame();
            _driver.SwitchTo().Frame(0);
            Status("Switched to iframe... [settings-frame]");
            // Cycle through max of 5 pages...
            for (int i = 0; i < 5; i++)
            {
                if (!ElementPresent(_driver, By.XPath("//td[@title='" + email + "']"), 3, false))
                {
                    _driver.FindElement(By.XPath("//td[@id='next_gridpager']/span")).Click();
                    //Sleep(1000);
                }
                else
                {
                    Log(email + " located on CUDL");
                    return true;
                }
            }

            Log("Could not locate " + email + " on CUDL!");
            return false;
        }
    }
}
