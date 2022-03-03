using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace SeleniumFirst.forms
{
    public partial class UserPropertiesAdd : Form
    {
        public Form1 mainForm;
        Person employee;

        public UserPropertiesAdd(Form1 mainForm)
        {
            this.mainForm = mainForm;
            //this.employee = mainForm.employee;
            this.employee = new Person();
            InitializeComponent();
            PopulateInputField();

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void tb_EmployeeNumber_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void btn_Commit_Click(object sender, EventArgs e)
        {

        }

        private void btn_Process_Click(object sender, EventArgs e)
        {

        }

        private void btn_Process_Click_1(object sender, EventArgs e)
        {
            if (ProcessInputField())
            {
                gbAccess.Enabled = true;
                btn_Commit.Enabled = true;
            }
            else
            {
                //mainForm.Error("Something happened...");
            }
        }

        private void UserProperties_Load(object sender, EventArgs e)
        {
            this.employee = mainForm.employee;
        }




        // GUI Methods and Functions 
        public void PopulateInputField()
        {
            mainForm.Status("Populating... [UserPropertiesAdd.cs]");
            XmlDocument xmlDoc = new XmlDocument();


            // Store Location ComboBox
            xmlDoc.Load(Directory.GetCurrentDirectory() + @"\resources\stores.xml");
            foreach (XmlNode element in xmlDoc.SelectNodes("//allstores/store"))
            {
                string value = element.SelectSingleNode("./@title").InnerText;
                cb_Location.Items.Add(value);
                //cb_TransferLocation.Items.Add(value);
            }


            // Role Position ComboBox
            xmlDoc.Load(Directory.GetCurrentDirectory() + @"\resources\roles.xml");
            foreach (XmlNode element in xmlDoc.SelectNodes("//allroles/role"))
            {
                string role = element.SelectSingleNode("./@title").InnerText;
                cb_Role.Items.Add(role);
                //cb_TransferRole.Items.Add(role);
            }



            // Apply load settings from variables defined by state.XML
            //cb_show.Checked = _showBrowser;
            mainForm.ShowConsole(mainForm._showConsole);
            //mainForm.ShowLegacyPanel(mainForm._showLegacyPanel);
        }
        public bool ProcessInputField()
        {
            //Status("Processing...");
            SetEmployeeParameters();
            ClearInputField();
            GenerateRoles(mainForm.employee.role, mainForm.employee.store);
            if (!GenerateEmail(mainForm.employee.firstname, mainForm.employee.lastname, mainForm.employee.store)) return false;
            if (!GeneratePassword()) return false;


            /*  Kinda laggy so lets not do this here......
            if (new Sql_io().UserExist_DB(employee.fullname))
            {
                mainForm.Log(employee.fullname + " already exists in database!");
                //return false;
            }
            */
            /*
            if (new Sql_io().EmailExist_DB(employee.email.username))
            {
                mainForm.Log(employee.email.username + " already exists in database!");
            }*/


            return true;
        }
        private void SetEmployeeParameters()
        {
            //var employee = mainForm.employee;
            employee.firstname = tb_FirstName.Text;
            employee.lastname = tb_LastName.Text;
            employee.fullname = employee.firstname + " " + employee.lastname;
            employee.role = cb_Role.Text;
            employee.department = "";
            employee.store = cb_Location.Text;
            employee.email.password = tb_Password.Text;
            employee.ssn = tb_SSN.Text;
            employee.employeenumber = tb_EmployeeNumber.Text;
            employee.email.username = tb_Email.Text;


            //Set dd_value based on location from Stores.XML
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Directory.GetCurrentDirectory() + @"\resources\stores.xml");
            foreach (XmlNode element in xmlDoc.SelectNodes("//allstores/store"))
            {

                string value = element.SelectSingleNode("./@title").InnerText;
                if (value == employee.store)
                {
                    //_domainLogin = element.SelectSingleNode("./@login").InnerText;    // Value is set in GenerateEmail() method.
                    employee.dealertrack.storeID = element.SelectSingleNode("./@ddvalue").InnerText;
                    mainForm._timezone = element.SelectSingleNode("./@timezone").InnerText;
                    mainForm._phoneA = element.SelectSingleNode("./@phoneA").InnerText;
                    mainForm._phoneB = element.SelectSingleNode("./@phoneB").InnerText;
                    mainForm._phoneC = element.SelectSingleNode("./@phoneC").InnerText;
                    mainForm._address = element.SelectSingleNode("./@address").InnerText;
                    mainForm._city = element.SelectSingleNode("./@city").InnerText;
                    mainForm._state = element.SelectSingleNode("./@state").InnerText;
                    mainForm._zip = element.SelectSingleNode("./@zip").InnerText;
                    break; //there shouldn't be multiple....
                }

            }

            //Set Department based on role from Roles.XML
            xmlDoc = new XmlDocument();
            xmlDoc.Load(Directory.GetCurrentDirectory() + @"\resources\roles.xml");
            foreach (XmlNode element in xmlDoc.SelectNodes("//allroles/role"))
            {

                string value = element.SelectSingleNode("./@title").InnerText;
                if (value == employee.role)
                {
                    //_domainLogin = element.SelectSingleNode("./@login").InnerText;    // Value is set in GenerateEmail() method.
                    employee.department = element.SelectSingleNode("./@department").InnerText;
                    break; //there shouldn't be multiple....
                }
            }

            // DETLA TANGO ALPHA BRAVO
            mainForm.employee = employee;
        }
        public void ClearInputField()
        {
            foreach (TreeNode node in tv_portals.Nodes)
            {
                node.Checked = false;

                if (node.Nodes.Count > 0) //reset children
                {
                    foreach (TreeNode child in node.Nodes)
                    {
                        child.Checked = false;
                    }
                }

            }

            tb_Email.Clear();
            cb_storeEmail.Checked = false;
            cb_wiseEmail.Checked = false;
            cb_dealerTrack.Checked = false;
            //cb_Com.Checked = false;
            cb_reynolds.Checked = false;
        }
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
                    /*
                    if (element.SelectSingleNode("./permissions/dealertrack") != null && location != "North Bay Nissan") { cb_dealerTrack.Checked = true; }
                    if (element.SelectSingleNode("./permissions/dealertrackcom") != null && location != "North Bay Nissan") { cb_com.Checked = true; }
                    if (element.SelectSingleNode("./permissions/reynolds") != null && location == "North Bay Nissan") { cb_reynolds.Checked = true; }
                    */
                    if (element.SelectSingleNode("./permissions/dealertrack") != null) { cb_dealerTrack.Checked = true; }
                    if (element.SelectSingleNode("./permissions/dealertrackcom") != null) { cb_com.Checked = true; }
                    //if (element.SelectSingleNode("./permissions/reynolds") != null && location == "North Bay Nissan") { cb_reynolds.Checked = true; }


                    //temporary TALON fix
                    if (element.SelectSingleNode("./permissions/talon") != null) { cb_talon.Checked = true; cb_dealerTrack.Checked = false; }
                    if (element.SelectSingleNode("./permissions/dealertrack") != null && location == "Death Valley Harley Davidson") { cb_talon.Checked = true; cb_dealerTrack.Checked = false; }


                    //if (element.SelectSingleNode("./permissions/rpm") != null) { cb_rpm.Checked = true; }
                    //if (element.SelectSingleNode("./permissions/cudl") != null) { cb_cudl.Checked = true; }
                    //if (element.SelectSingleNode("./permissions/alarm") != null) { cb_storeEmail.Checked = true; }

                    if (element.SelectSingleNode("./permissions/portal") != null)
                    {
                        switch (location.ToLower())
                        {
                            case string a when a.Contains("nissan"):
                                tv_portals.Nodes[0].Checked = true;
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
                            case string a when a.Contains("infiniti"):
                                tv_portals.Nodes[0].Checked = true;
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
                            case string a when a.Contains("harley"):
                                tv_portals.Nodes[5].Checked = true;
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

            //DETLA TANGO ALPHA BRAVO
            mainForm.employee = employee;
        }
        private bool GeneratePassword()
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
                    mainForm.Status("Password generated: " + employee.email.password);
                    return true;
                }
                else
                {
                    mainForm.Error("Couldn't locate path: " + path);
                    return false;
                }
            }
            catch
            {
                mainForm.Error("Parsing directory for wordlist failed");
                return false;
            }
        }
        private bool GenerateEmail(string firstName, string lastName, string location)
        {
            mainForm.Log("Generating Email... [0xCrab]");
            if (firstName != "" && lastName != "" && (cb_storeEmail.Checked || cb_wiseEmail.Checked))
            {
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
                            mainForm._domain = element.SelectSingleNode("./@domain").InnerText;
                            mainForm._domainLogin = element.SelectSingleNode("./@login").InnerText;
                            break; //there shouldn't be multiple....
                        }

                    }
                }
                if (cb_wiseEmail.Checked)
                {
                    mainForm._domain = "wiseautogroup.com";
                    mainForm._domainLogin = "vacanissan";
                }

                employee.email.username = firstName.Trim().First<char>().ToString() + lastName.Trim().ToString() + "@" + mainForm._domain;
                employee.email.username = employee.email.username.ToLower();
                tb_Email.Text = employee.email.username;
                mainForm.Status("Email generated: " + employee.email.username);
                return true;
            }
            else
            {
                mainForm.Log("Email address not generated, is the name field filled out?");
                return false;
            }
        }
        private string GenerateDomainLogin(string location)
        {
            mainForm.Log("Generating Domain Login... [0xHen]");
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Directory.GetCurrentDirectory() + @"\resources\stores.xml");
            foreach (XmlNode element in xmlDoc.SelectNodes("//allstores/store"))
            {
                string value = element.SelectSingleNode("./@title").InnerText;
                if (value == location)
                {
                    mainForm._domain = element.SelectSingleNode("./@domain").InnerText;
                    mainForm._domainLogin = element.SelectSingleNode("./@login").InnerText;
                    break; //there shouldn't be multiple....
                }
            }
            //MessageBox.Show(_domainLogin);
            return mainForm._domainLogin;
        }
        private void btn_Commit_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            SetEmployeeParameters();
            mainForm.employee = this.employee;
            mainForm.SetAdminParameters();

            // DEBUG TRACE 
            // DEBUG TRACE 
            // DEBUG TRACE 
            mainForm.Log("DEBUG TRACE :: [0xPhantom] :: employee.email.username=" + employee.email.username);
            mainForm.Log("DEBUG TRACE :: [0xPhantom] :: mainForm.employee.email.username=" + mainForm.employee.email.username);


            if (tb_FirstName.Text == "" || tb_LastName.Text == "" || tb_SSN.Text == "" || cb_Location.Text == "" || 
                cb_Role.Text == "" || tb_EmployeeNumber.Text == "" || tb_Email.Text == "" || tb_Password.Text == "")
            {
                mainForm.Error("All fields must be filled out!");
                return;
            }


            if (mainForm.sql.UserExist_DB(employee.fullname))
            {
                mainForm.Log(employee.fullname + " already exists in database!");
                //return;
            }
            if (mainForm.sql.EmailExist_DB(employee.email.username) && (cb_storeEmail.Checked || cb_wiseEmail.Checked))
            {
                mainForm.Error(employee.email.username + " is already active in employee database!");
                return;
            }

            ListViewItem person = mainForm.xml.AddToQueue("In Progress", employee.fullname, employee.email.username, "Add", 1, null);
            //mainForm.lv_queue.Items.Add(person);


            mainForm.state = true;
            AddEmployee(person);
        }
        private void tv_portals_AfterCheck(object sender, TreeViewEventArgs e)
        {
            mainForm.SelectParents(e.Node, e.Node.Checked);
            mainForm.UnselectChildren(e.Node, e.Node.Checked);
        }
    }



}
