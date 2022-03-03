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
            GoToURL("https://wcc.secureserver.net/");
            Status("Filling login form...");
            Sleep(500);
            IWebElement usernameInput = _driver.FindElement(By.Id("username"));
            IWebElement passwordInput = _driver.FindElement(By.Id("password"));
            IWebElement rememberInput = _driver.FindElement(By.Id("remember-me"));
            IWebElement submitInput = _driver.FindElement(By.Id("submitBtn"));

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

            while (!_driver.Title.ToLower().Contains("workspace control center"))
            {
                this.Refresh();
                Sleep(500);
            }

            Status("Login success!");
            Sleep(500); //is this required?
            return true;
        }

        private bool EmailAddUser()
        {
            //GoToURL("https://wcc.secureserver.net/");
            IWebElement createButton = _driver.FindElement(By.Id("createButton"));
            createButton.Click();
            Status("Looking for input fields...");

            if (!ElementPresent(_driver, By.Id("createAccountEmailAddress"), 10)) return false;

            Status("Filling input fields...");
            IWebElement emailInput = _driver.FindElement(By.Id("createAccountEmailAddress"));
            IWebElement password1Input = _driver.FindElement(By.Id("createAccountPassword1"));
            IWebElement password2Input = _driver.FindElement(By.Id("createAccountPassword2"));
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
                    Status("Saving Input...");
                    IWebElement saveInput = _driver.FindElement(By.Id("saveButton"));
                    saveInput.Click();
                    //saveInput.Submit();
                }
                catch
                {
                    Error("Clicking Save failed");
                    return false;
                }
            }

            // Error Detection
            //Status("Sleeping to prevent driver crash...");
            Sleep(3000, false);    //might be required to prevent false error flag
            Status("Looking for Error notification...");
            if (ElementPresent(_driver, By.Id("componentCreateAccountErrorDomain"), alert: false))
            {

                if (_driver.FindElement(By.Id("componentCreateAccountErrorDomain")).Text.Contains("already in use"))
                {
                    Error("Email creation failed - Already in use");
                    return false;
                }
            }
            if (ElementPresent(_driver, By.Id("componentCreateAccountErrorMisc"), alert: false))
            {
                if (_driver.FindElement(By.Id("componentCreateAccountErrorMisc")).Text.Contains("No account type checkbox has been selected."))
                {
                    Error("Email creation failed - Domain may be full");
                    return false;
                }
            }

            Status("Looking for Success notification...");
            //Success Detection
            if (!ElementPresent(_driver, By.ClassName("sf_growl_title"), 10)) return false;
            if (_driver.FindElement(By.ClassName("sf_growl_title")).Text.ToLower().Contains("success"))   //<div class="sf_growl_title">Success</div>
            {
                Progress("Success! - Email created " + employee.email.username + " : " + employee.email.password);
                employee.email.addDate = DateTime.Now.ToString();
                employee.email.addBy = admin.fullname;
                return true;
            }

            Log("Probable Success! - Email created (Please verify, as an exception was unhandled)");
            employee.email.addDate = DateTime.Now.ToString();
            employee.email.addBy = admin.fullname;
            return true;
        }

        private bool EmailRemoveUser(string username)
        {

        }

        private bool EmailCheckUser(string username)
        {
            _driver.FindElement(By.XPath("//input[@id='searchField']")).SendKeys(username);
            _driver.FindElement(By.XPath("//input[@id='searchField']")).Submit();
        }
    }
}
