using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Xml;

namespace SeleniumFirst
{
    public partial class Form1
    {
        // Functions
        public bool InitializeBrowser(bool show)
        {
            TerminateBrowser();
            Progress("::LAUNCHING...::");
            tb_Progress.MarqueeAnimationSpeed = 100;
            tb_Progress.Value = 50;

            ChromeOptions driverOptions = new ChromeOptions();
            driverOptions.BinaryLocation = chromePath;
            //MessageBox.Show(driverOptions.BrowserName + " -- " + driverOptions.BrowserVersion);
            //ChromeDriverService driverService = ChromeDriverService.CreateDefaultService();
            ChromeDriverService driverService = ChromeDriverService.CreateDefaultService(Directory.GetCurrentDirectory() + @"\resources\drivers\", driverPath);
            driverService.LogPath = Directory.GetCurrentDirectory() + @"\resources\drivers\" + driverLogPath;
            //FirefoxOptions driverOptions = new FirefoxOptions();
            //InternetExplorerOptions driverOptions = new InternetExplorerOptions();

            #region ChromeDriver De-Bot and Settings

            //driverOptions.AddArgument($"--user-data-dir={Environment.UserName}");
            //driverOptions.AddArgument("--profile-directory='Profile 1'");

            /* CHROME BINARY PATH */
            //driverOptions.BinaryLocation = chromePath;
            driverOptions.AddArgument("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.110 Safari/537.36");
            driverOptions.AddArgument(@"user-data-dir=C:\Temp\");
            driverOptions.AddArgument("--enable-javascript");
            driverOptions.AddArgument("javascript.enabled=true;");
            driverOptions.AddArgument("javascript.enabled=  true;javascript.enabled=  true;"); // FIX :) IDK HOW....lol
            driverOptions.AddArgument("--allow-scripts"); // FIX :) IDK HOW....lol


            driverOptions.AddArgument("window-size=1024,768");
            //driverOptions.AddArgument("app=http://google.com");
            driverOptions.AddArgument("app=http://sso.secureserver.net/");
            
            driverOptions.AddArgument("--no-sandbox");
            driverOptions.AddArgument("--disable-dev-shm-usage");
            driverOptions.AddArgument("--disable-blink-features");
            driverOptions.AddArgument("--disable-blink-features=AutomationControlled");
            driverOptions.AddArgument("--disable-infobars");
            driverOptions.AddArgument("--disable-session-crashed-bubble");
            driverOptions.AddArgument("--remote-debugging-port=9222");

            //driverOptions.AddLocalStatePreference("browser", new { enabled_labs_experiments = new string[] { "excludeSwitches@2" } });


            //driverOptions.AddArgument("--disable-single-click-autofill");

            driverOptions.AddExcludedArgument("enable-logging");
            #endregion

            if (!show)
            {
                driverService.HideCommandPromptWindow = true;
                driverOptions.AddArgument("--headless");
                //driverOptions.AddArgument("--disable-gpu");

            }

            try
            {
                //_driver = new ChromeDriver(driverOptions);
                _driver = new ChromeDriver(driverService, driverOptions); //Open Chrome
                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(_timeout);
                return true;
            }
            catch
            {
                Error("web driver doesn't exist [0xSnail]");
                TerminateBrowser();
                return false;
            }


        }

        bool ElementPresent(IWebDriver driver, By by, int seconds = 0, bool alert = true)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(seconds);

            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                if (alert)
                {
                    Status("Element is not present - Max timeout reached!");
                }
                return false;
            }
            finally
            {
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(_timeout);
            }
        }

        bool ElementClickable(IWebDriver driver, IWebElement element)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(0));
                wait.Until(ExpectedConditions.ElementToBeClickable(element));
                //MessageBox.Show("Element is clickable!");
                return true;
            }
            catch (Exception e)
            {
                //MessageBox.Show("Element is non-clickable!");
                return false;
            }
        }

        private void Sleep(float miliseconds, bool show = false)
        {
            if (miliseconds >= 1000 && show)
            {
                Status("Sleeping for " + (miliseconds / 1000).ToString() + " seconds...");
            }
            System.Threading.Thread.Sleep((int)miliseconds);
        }

        private void SetTimeOut(int seconds)
        {
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(seconds);
        }

        private void GoToURL(string url)
        {
            Status("Navigating to " + url);
            try
            {
                if (_driver == null)
                { 
                   Log("_driver is null!");
                    TerminateBrowser();
                    InitializeBrowser(_showBrowser);
                    
                }
                if (_driver != null)
                {
                    _driver.Navigate().GoToUrl(url);
                }
                else
                {
                    Error("Could not initialize driver. (Wrong driver version)");
                }
            }
            catch
            {
                Error("Could not navigate to url: " + url);
            }
        }

        private void FetchURL(string title)
        {

            Status("Creating URL for " + title + "...");
            foreach (XmlNode element in _xmlDoc_portals.SelectNodes("//allportals/portal"))
            {
                string value = element.SelectSingleNode("./@title").InnerText;
                if (value == title)
                {
                    _url = element.SelectSingleNode("./@url").InnerText;
                    break; //there shouldn't be multiple....
                }
            }
        }

        public void TerminateBrowser(bool force = true)
        {

            // _domainLogin = null;
            //admin = null;
            // employee = null;


            //employee = new Person();              // had to remove this because it is resetting employee state after second runs
            string allProcesses = "";
            foreach (Process proc in Process.GetProcesses())
            {
                allProcesses += proc.ProcessName.ToString() + ", ";
            }
            //MessageBox.Show(allProcesses);

            if (_driver != null)
            {
                Status("Closing driver...");
                try {_driver.Quit(); Progress("::CHROME DRIVER CLOSED::"); }
                catch {}
                tb_Progress.Value = 0;
                tb_Progress.MarqueeAnimationSpeed = 0;
                
                Progress("::DONE::");
                //Sleep(10000);
            }

            if (true)   // force == true
            {
                foreach (Process proc in Process.GetProcessesByName("chromedriver"))
                {
                    Status("Closing driver...");
                    proc.Kill();
                    Log("Terminated chromedriver process");
                    //Sleep(5000);
                }

                foreach (Process proc in Process.GetProcessesByName("chromedriver.exe"))
                {
                    Status("Closing driver...");
                    proc.Kill();
                    Log("Terminated chromedriver.exe process");
                    //Sleep(5000);
                }

                foreach (Process proc in Process.GetProcessesByName("chrome.exe"))
                {
                    Status("Closing driver...");
                    proc.Kill();
                    Log("Terminated chrome.exe process");
                    //Sleep(5000);
                }


                foreach (Process proc in Process.GetProcessesByName("chromedriver_97.exe"))
                {
                    Status("Closing driver...");
                    proc.Kill();
                    Log("Terminated chromedriver_97.exe process");
                    //Sleep(5000);
                }

                foreach (Process proc in Process.GetProcessesByName("chromedriver_96.exe"))
                {
                    Status("Closing driver...");
                    proc.Kill();
                    Log("Terminated chromedriver_96.exe process");
                    //Sleep(5000);
                }

            }

        }
    }
}
