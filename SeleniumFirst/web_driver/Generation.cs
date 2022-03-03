/*  To-Do:
 *  -Line 46 needs a better decipher method for choosing reynolds (North Bay Nissan)
 * 
 * */

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Firefox;
using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Windows.Forms;

namespace SeleniumFirst
{
    public partial class Form1
    {
        public void GenerateRoles(string role, string location)
        {
            // XML Permissions
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Directory.GetCurrentDirectory() + @"\resources\roles.xml");
            foreach (XmlNode element in xmlDoc.SelectNodes("//allroles/role"))
            {
                string value = element.SelectSingleNode("./@title").InnerText;

                if (value == role)
                {
                    employee.department = element.SelectSingleNode("./@department").InnerText;

                    if (element.SelectSingleNode("./@dtvalue") != null) { employee.dealertrack.roleID = element.SelectSingleNode("./@dtvalue").InnerText; }
                    if (element.SelectSingleNode("./@nnavalue") != null) { employee.nnanet.roleID = element.SelectSingleNode("./@nnavalue").InnerText; }
                    if (element.SelectSingleNode("./@kdvalue") != null) { employee.kdealer.roleID = element.SelectSingleNode("./@kdvalue").InnerText; }
                    if (element.SelectSingleNode("./@mxvalue") != null) { employee.mxconnect.roleID = element.SelectSingleNode("./@mxvalue").InnerText; }
                    if (element.SelectSingleNode("./@hyuvalue") != null) { employee.hyundaidealer.roleID = element.SelectSingleNode("./@hyuvalue").InnerText; }
                    
                    if (element.SelectSingleNode("./@hdvalue") != null) { employee.hdnet.roleID = element.SelectSingleNode("./@hdvalue").InnerText; }
                    if (element.SelectSingleNode("./@hdmvalue") != null) { employee.hdnet.managerID = element.SelectSingleNode("./@hdmvalue").InnerText; }
                    if (element.SelectSingleNode("./@hddvalue") != null) { employee.hdnet.departmentID = element.SelectSingleNode("./@hddvalue").InnerText; }
                    
                    if (element.SelectSingleNode("./@dcvalue") != null) { employee.dealerconnect.roleID = element.SelectSingleNode("./@dcvalue").InnerText; } //dealerconnect
                    if (element.SelectSingleNode("./@gmgvalue") != null) { employee.gmglobal.roleID = element.SelectSingleNode("./@gmgvalue").InnerText; } //gmglobal
                    if (element.SelectSingleNode("./@vccvalue") != null) { employee.vcc.roleID = element.SelectSingleNode("./@vccvalue").InnerText; } //vcc

                    if (element.SelectSingleNode("./permissions/storemail") != null) { cb_storeEmail.Checked = true; }
                    if (element.SelectSingleNode("./permissions/wisemail") != null) { cb_wiseEmail.Checked = true; }
                    if (element.SelectSingleNode("./permissions/dealertrack") != null && location != "North Bay Nissan") { cb_dealerTrack.Checked = true; }
                    if (element.SelectSingleNode("./permissions/dealertrackcom") != null && location != "North Bay Nissan") { cb_com.Checked = true; }
                    if (element.SelectSingleNode("./permissions/reynolds") != null && location == "North Bay Nissan") { cb_reynolds.Checked = true; }
                    if (element.SelectSingleNode("./permissions/office365") != null) { tv_portals.Nodes[9].Checked = true; }


                    //temporary TALON fix
                    if (element.SelectSingleNode("./permissions/talon") != null) { cb_talon.Checked = true; cb_dealerTrack.Checked = false;}
                    if (element.SelectSingleNode("./permissions/dealertrack") != null && location == "Death Valley Harley Davidson") { cb_talon.Checked = true; cb_dealerTrack.Checked = false; }


                    //if (element.SelectSingleNode("./permissions/rpm") != null) { cb_rpm.Checked = true; }
                    //if (element.SelectSingleNode("./permissions/cudl") != null) { cb_cudl.Checked = true; }
                    //if (element.SelectSingleNode("./permissions/alarm") != null) { cb_storeEmail.Checked = true; }

                    if (element.SelectSingleNode("./permissions/portal") != null) 
                    {
                        switch (location.ToLower())
                        {
                            case string a when a.Contains("nissan"): tv_portals.Nodes[0].Checked = true;
                                switch (location.ToLower())
                                {
                                    case string b when b.Contains("vacaville"): tv_portals.Nodes[0].Nodes[0].Checked = true; break;
                                    case string b when b.Contains("north bay"): tv_portals.Nodes[0].Nodes[1].Checked = true; break;
                                    case string b when b.Contains("yuba city"): tv_portals.Nodes[0].Nodes[2].Checked = true; break;
                                    case string b when b.Contains("sacramento"): tv_portals.Nodes[0].Nodes[3].Checked = true; break;
                                    case string b when b.Contains("marin"): tv_portals.Nodes[0].Nodes[4].Checked = true; break;
                                    case string b when b.Contains("vallejo"): tv_portals.Nodes[0].Nodes[5].Checked = true; break;
                                    case string b when b.Contains("golden state"): //Cross-Access
                                        tv_portals.Nodes[0].Nodes[6].Checked = true; 
                                        tv_portals.Nodes[0].Nodes[7].Checked = true; 
                                        break;       
                                }
                                break;
                            case string a when a.Contains("infiniti"): tv_portals.Nodes[0].Checked = true;
                                switch (location.ToLower())
                                {
                                    case string b when b.Contains("golden state"): //Cross-Access
                                        tv_portals.Nodes[0].Nodes[6].Checked = true;
                                        tv_portals.Nodes[0].Nodes[7].Checked = true;
                                        break;
                                    case string b when b.Contains("marin"): tv_portals.Nodes[0].Nodes[4].Checked = true; break;
                                }
                                break;
                            case string a when a.Contains("dodge"): tv_portals.Nodes[1].Checked = true; break;
                            case string a when a.Contains("buick"): tv_portals.Nodes[2].Checked = true; break;
                            case string a when a.Contains("hyundai"): tv_portals.Nodes[3].Checked = true; break;
                            case string a when a.Contains("kia"): tv_portals.Nodes[4].Checked = true; break;
                            case string a when a.Contains("harley"): tv_portals.Nodes[5].Checked = true; 
                                if (a.Contains("reno")) { tv_portals.Nodes[5].Nodes[0].Checked = true; }
                                if (a.Contains("yuba")) { tv_portals.Nodes[5].Nodes[1].Checked = true; }
                                if (a.Contains("redwood")) { tv_portals.Nodes[5].Nodes[2].Checked = true; }
                                if (a.Contains("death valley")) { tv_portals.Nodes[5].Nodes[3].Checked = true; }
                                break;
                            case string a when a.Contains("volvo"): tv_portals.Nodes[6].Checked = true; break;
                            case string a when a.Contains("mazda"): tv_portals.Nodes[7].Checked = true; break;

                        }
                    }
                    break;
                }
            }
        }

        private void GeneratePassword()
        {
            try
            {
                string path = Directory.GetCurrentDirectory() + @"\resources\wordlist.txt";
                if (File.Exists(path))
                {
                    //StreamReader sr = File.OpenText(path);
                    string[] allLines = File.ReadAllLines(path);
                    Random rnd = new Random();
                    employee.email.password = allLines[rnd.Next(0, allLines.Count() - 1)] + "-" + allLines[rnd.Next(0, allLines.Count() - 1)] + "-" + allLines[rnd.Next(0, allLines.Count() - 1)];
                    employee.email.password = employee.email.password.ToLower();
                    tb_Password.Text = employee.email.password;
                    Status("Password generated: " + employee.email.password); ;
                }
                else
                {
                    Error("Couldn't locate path: " + path);
                }
            }
            catch
            {
                Error("Parsing directory for wordlist failed");
            }
        }

        private bool GenerateEmail(string firstName, string lastName, string location)
        {
            
            if (firstName != "" && lastName != "" && (cb_storeEmail.Checked || cb_wiseEmail.Checked))
            {
                Log("Generating Email... [0xSalamander]");
                if (cb_storeEmail.Checked)
                {
                    //append email domain
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(Directory.GetCurrentDirectory() + @"\resources\stores.xml");
                    foreach (XmlNode element in xmlDoc.SelectNodes("//allstores/store"))
                    {
                        string value = element.SelectSingleNode("./@title").InnerText;
                        if (value == location)
                        {
                            _domain = element.SelectSingleNode("./@domain").InnerText;
                            _domainLogin = element.SelectSingleNode("./@login").InnerText;
                            break; //there shouldn't be multiple....
                        }

                    }
                }
                if (cb_wiseEmail.Checked)
                {
                    _domain = "wiseautogroup.com";
                    _domainLogin = "vacanissan";
                }

                employee.email.username = firstName.Trim().First<char>().ToString() + lastName.Trim().ToString() + "@" + _domain;
                employee.email.username = employee.email.username.ToLower();
                tb_Email.Text = employee.email.username;
                Status("Email generated: " + employee.email.username);
                return true;
            }
            else
            {
                Log("Email address not generated, is the name field filled out?");
                return false;
            }
        }

        public string GenerateDomainLogin(string location)
        {
            Log("Generating Domain Login... [0xNewt]");

            if (employee.email.username.Contains("@wiseautogroup.com"))
            {
                location = "Wise Auto Group";
            }
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Directory.GetCurrentDirectory() + @"\resources\stores.xml");


            foreach (XmlNode element in xmlDoc.SelectNodes("//allstores/store"))
            {
                string value = element.SelectSingleNode("./@title").InnerText;
                if (value == location)
                {
                    _domain = element.SelectSingleNode("./@domain").InnerText;
                    _domainLogin = element.SelectSingleNode("./@login").InnerText;
                    break; //there shouldn't be multiple....
                }
            }
            //MessageBox.Show(_domainLogin);


            Log("Domain generated: " + _domain + "  - Domain Login: " + _domainLogin);
            return _domainLogin;
        }
    }
}
