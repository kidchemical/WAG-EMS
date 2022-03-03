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
        public bool HyundaiDealerLogin()
        {
            string title = "HyundaiDealer";
            FetchURL(title);
            //GoToURL(_url);
            GoToURL("https://wdcs.hyundaidealer.com/irj/portal/webdcs#/admin_userEnrollment");
            if (ElementPresent(_driver, By.XPath("//input[@name='dealercode']")))
            {
                Status("Logging in...");
                _driver.FindElement(By.XPath("//input[@name='dealercode']")).SendKeys("CA362");
                _driver.FindElement(By.XPath("//input[@name='j_user']")).SendKeys(admin.hyundaidealer.username);
                _driver.FindElement(By.XPath("//input[@name='j_password']")).SendKeys(admin.hyundaidealer.password);
                _driver.FindElement(By.XPath("//input[@name='j_password']")).Submit();
            }
            GoToURL("https://www.hyundaidealer.com/_layouts/SSOSharepointSolution/SSORedirect.aspx?id=WEBDCS_allowCU_V2");
            return true;
        }

        public bool HyundaiDealerAddUser()
        {
            GoToURL("https://wdcs.hyundaidealer.com/irj/portal/webdcs#/admin_dealerUserInquiry");
            //ExpectedConditions.ElementToBeClickable();
            
            if (ElementPresent(_driver, By.XPath("//button[@ng-click='userInquiry.goCreate()']/i"), _timeout, false))
            {
                Sleep(3000);
                _driver.FindElement(By.XPath("//button[@ng-click='userInquiry.goCreate()']")).Click();  // /i
            }
            else
            {
                Log("Could not locate CreateUser button on HyundaiDealer, tryig again... [0x0A]");
                HyundaiDealerAddUser();
            }

            // Create user page
            if (!ElementPresent(_driver, By.XPath("//select[@name='title']"), _timeout)) { return false; }
            new SelectElement(_driver.FindElement(By.XPath("//select[@name='title']"))).SelectByValue("NOTITLE");
            _driver.FindElement(By.XPath("//input[@name='firstname']")).SendKeys(employee.firstname);
            _driver.FindElement(By.XPath("//input[@name='lastname']")).SendKeys(employee.lastname);
            _driver.FindElement(By.XPath("//input[@name='p_new_email']")).SendKeys(employee.email.username);
            _driver.FindElement(By.XPath("//input[@name='cfm_email']")).SendKeys(employee.email.username);
            new SelectElement(_driver.FindElement(By.XPath("//select[@name='securityquestion']"))).SelectByValue("10");
            _driver.FindElement(By.XPath("//input[@name='securityanswer']")).SendKeys("hyundai");
            //_driver.FindElement(By.XPath("//input[@name='agree_tc']")).SendKeys(OpenQA.Selenium.Keys.Space);
            _driver.FindElement(By.XPath("//a[@ng-click='userEnrollment.termsAndConditonPop()']")).Click();
            Sleep(1000); // Might not need...
            _driver.FindElement(By.XPath("//button[@ng-click='termsAndCondition.closeTermsAndConditonPop()']")).Click();
            _driver.FindElement(By.XPath("//input[@name='primary_jflag'][@value='" + employee.hyundaidealer.roleID + "']/../i")).Click();

            // STAR PAYMENT issue for sales people....requires W9......Chris......
            if (ElementPresent(_driver, By.XPath("//div[contains(text(), 'A Dealer User has to create the Dealer Personnel')]"), 3, false))
            {
                //MessageBox.Show("You may be trying to enter an employee that requires enrollment to STAR Payments. Please fill out the form then press OK to continue...");
                _driver.FindElement(By.XPath("//button[contains(text(), 'Confirm')]")).Click();
                // Fill STAR fields
                _driver.FindElement(By.XPath("//input[@name='addr1']")).SendKeys(_address);
                new SelectElement(_driver.FindElement(By.XPath("//select[@name='state']"))).SelectByValue(_state);
                _driver.FindElement(By.XPath("//input[@name='zipcode']")).SendKeys(_zip);
                _driver.FindElement(By.XPath("//input[@name='city']")).SendKeys(_city);
                _driver.FindElement(By.XPath("//input[@name='mobilephone']")).SendKeys(_phoneA + _phoneB + _phoneC);
                _driver.FindElement(By.XPath("//a[contains(text(), 'W-9 Form')]")).Click();

                _driver.FindElement(By.XPath("//input[@id='ssn1']")).SendKeys(employee.ssn.Substring(0, 3));
                _driver.FindElement(By.XPath("//input[@id='ssn2']")).SendKeys(employee.ssn.Substring(4, 2));
                _driver.FindElement(By.XPath("//input[@id='ssn3']")).SendKeys(employee.ssn.Substring(7, 4));
                _driver.FindElement(By.XPath("//input[@id='cfm_ssn1']")).SendKeys(employee.ssn.Substring(0, 3));
                _driver.FindElement(By.XPath("//input[@id='cfm_ssn2']")).SendKeys(employee.ssn.Substring(4, 2));
                _driver.FindElement(By.XPath("//input[@id='cfm_ssn3']")).SendKeys(employee.ssn.Substring(7, 4));
                _driver.FindElement(By.XPath("//input[@id='w9_check']/../i")).Click();
                _driver.FindElement(By.XPath("//button[@id='w9_save_btn']")).Click();
            }


            _driver.FindElement(By.XPath("//a[@ng-click='userEnrollment.selectJobCode()']")).Click();

            Sleep(3000);

            // Error sometimes....
            try
            {
                _driver.FindElement(By.XPath("//button[@ng-click='userEnrollment.doVerify()']/i")).Click();
            }
            catch
            {
                Error("Could not locate element [0x01]");
                return false;
            }

            Sleep(2000);
            if (ElementPresent(_driver, By.XPath("//div[@class='modal fade in']/div/div/div[@class='modal-body']/div/div/i"), 3, false))
            {
                Log(employee.email.username + " already exist in HyundaiDealer");
                _driver.FindElement(By.XPath("//div[@class='modal-dialog modal-alert modal-dialog-top']//div[@class='modal-footer']/button")).Click();
            }
            else
            {
                _driver.FindElement(By.XPath("//button[@ng-click='userEnrollment.doSubmit()']/i")).Click();
                //Sleep(3000);
            }

            //wait for success message...
            
            GoToURL("https://wdcs.hyundaidealer.com/irj/portal/webdcs#/admin_dealerUserInquiry");
            Sleep(8000);
            _driver.FindElement(By.XPath("//input[@id='username']")).SendKeys(employee.firstname + " " + employee.lastname);
            Sleep(1000);
            try
            {
                _driver.FindElement(By.XPath("//span[@class='search-btn pull-right']/a/i[@class='fa fa-search']")).Click();
            }
            catch
            {
               Log("Could not locate search element, trying again [0x02]");
                return false;
            }


            Sleep(1000);
            try
            {
                employee.hyundaidealer.username = _driver.FindElement(By.XPath("//a[@href='javascript:void(0);']")).Text.Trim(); //gave error
            }
            catch { Error("Error locating HyundaiDealer username. Contact administration [0x01]"); return false; }
            if (employee.hyundaidealer.username == null)
            {
                Error("Could not confirm " + "HyundaiDealer" + " login creation!");
                return false;
            }
            employee.hyundaidealer.password = "Emailed";
            Status("User ID - " + employee.hyundaidealer.username + " : " + employee.hyundaidealer.password);
            Status("Succefully created " + "HyundaiDealer");
            return true;
        }

        public bool HyundaiDealerRemoveUser(string username)
        {
            if (HyundaiDealerCheckUser(username) == false) { Log("User already does not exist on HyundaiDealer!");  return true; }

            _driver.FindElement(By.XPath("//span[@class='responsiveExpander']/../a")).Click();
            _driver.FindElement(By.XPath("//input[@ng-model='myProfileUpdate.contact.temination_jobcd']/../i")).Click();
            _driver.FindElement(By.XPath("//button[text()='Close']")).Click();
            _driver.FindElement(By.XPath("//button[@type='submit']")).Click();
            //MessageBox.Show("*DEBUG HALT*");
            return true;
        }

        public bool HyundaiDealerCheckUser(string username)
        {
            GoToURL("https://wdcs.hyundaidealer.com/irj/portal/webdcs#/admin_dealerUserInquiry");
            Sleep(10000);

            _driver.FindElement(By.XPath("//input[@id='userid']")).SendKeys(username);
            try
            {
                _driver.FindElement(By.XPath("//span[@class='search-btn pull-right']/a/i[@class='fa fa-search']")).Click();
                Sleep(3000);
            }
            catch
            {
                Error("Could not locate element [0x02]");
                return false;
            }


            if (ElementPresent(_driver, By.XPath("//span[@class='responsiveExpander']/../a"), 15, false))
            {
                Log(username + " found on HyundaiDealer");
                if (_driver.FindElement(By.XPath("//span[@class='responsiveExpander']/../a")).Text.Contains(username))
                {
                    return true;
                }
                return false;
            }
            else
            {
                Log(username + " NOT found on HyundaiDealer!");
                return false;
            }
        }
    }
}
