/* Note: There has been some issues when pulling from Git with missing references. Hoping the newest visual studio update fixes this!
 * 
 */


using AutoUpdaterDotNET;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace SeleniumFirst
{
    public partial class Form1 : Form
    {
        // Global Variables
        public string _version = "1.1.4.0";         //Total version, sub version, NewWorkFlow, OldWorkflow
        public IWebDriver _driver;
        public bool state = true; // Refers to the fail-status of the execution processes, not to be confused with _state
        public string _mfc = "";
        public string _console = "";
        public string _url = "";


        string startupMessage = "";
        string motivationalQuote = "'Success in not final, Failure is not fatal, it is the courage to continue that counts.'";

        
        //public string driverPath = "chromedriver_97.exe";
        public string driverPath = @"Win_942610_chromedriver_win32\chromedriver.exe";
        public string driverLogPath = "chromedriver.log";
        //public string chromePath = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
        public string chromePath = Directory.GetCurrentDirectory() + @"\resources\drivers\Win_942610_chromedriver_win32\chrome-win\chrome.exe";
        //public string chromePath = Directory.GetCurrentDirectory() + @"\resources\drivers\Win_938292_chrome-win\chrome-win\chrome.exe";


        bool _admin = false; // DEBUG TOGGLE SWITCH


        //Settings
        public int _timeout = 60;
        public bool _showBrowser = true;
        public bool _showConsole = true;
        public bool _showLegacyPanel = false;
        public bool _simpleConsole = false;

        // Objects
        public Sql_io sql;
        public Xml_io xml;
        public Excel_io excel = new Excel_io();

        // Forms
        public forms.Settings settings;
        forms.UserPropertiesAdd UserPropertiesAdd;
        forms.UserPropertiesEdit UserPropertiesEdit;
        forms.UserPropertiesRemove UserPropertiesRemove;
        Form debugPanel;
        Form toolDTPassResetter;
        Form toolNNANetCrossEnroller;


        // Stores.XML
        public string _domain = "";
        public string _domainLogin = "";
        //public string _dd_value = "";       //dealertrack dealer (store location)
        //public string _dt_value = "";       //dealertrack title (position)
        //public string _nna_value = "";      //nna dealer (store location)
        public string _timezone = "PST";
        public string _phoneA, _phoneB, _phoneC = "";
        public string _address, _city, _state, _zip;

        // Portals.XML
        XmlDocument _xmlDoc_portals = new XmlDocument();

        // Admin and Employee instances
        public Person admin = new Person();
        public Person employee = new Person();


        public Form1()
        {
            InitializeComponent();



            //----
            // Form Drawing enhancements, fixes gradient background glitch when redrawing, also seems to improve UI responsiveness
            // found from https://stackoverflow.com/questions/24088860/painting-issue-after-resizing-winform
            //----
            this.SetStyle(ControlStyles.AllPaintingInWmPaint
              | ControlStyles.OptimizedDoubleBuffer
              | ControlStyles.ResizeRedraw
              | ControlStyles.DoubleBuffer
              | ControlStyles.UserPaint
              , true);


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TerminateBrowser();

            // Check for Updates
            AutoUpdater.InstalledVersion = new Version(_version);
            AutoUpdater.Start(@"file://10.205.203.16/it\main\4. Useful Things\Software\Rockwell Automation\.meta\version.xml");

            // Objects
            sql = new Sql_io();
            xml = new Xml_io(this);

            // Forms
            UserPropertiesAdd = new forms.UserPropertiesAdd(this);
            UserPropertiesEdit = new forms.UserPropertiesEdit(this);
            UserPropertiesRemove = new forms.UserPropertiesRemove(this);

            debugPanel = new forms.DebugPanel(this);
            toolDTPassResetter = new forms.DTPassResetter(this);
            toolNNANetCrossEnroller = new forms.ToolsNNANetCrossEnroller(this);

            // Portals.XML
            _xmlDoc_portals.Load(Directory.GetCurrentDirectory() + @"\resources\portals.xml");

            // Queue
            List<ListViewItem> personList = xml.LoadState();
            foreach (ListViewItem person in personList)
            {
                lv_queue.Items.Add(person);
            }

            PopulateInputField();
            SetAdminParameters();
            Status(motivationalQuote);
            Progress("Ready (ver." + _version + ")");



            if (startupMessage != "")
            {
                MessageBox.Show(startupMessage, "Version Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            TerminateBrowser();

        }

        public bool WebMailDebugger()
        {
            Status("Running WebMail Debugger...");
            SetAdminParameters();
            InitializeBrowser(_showBrowser);
            //GoToURL("tokenURLHERE");
            Log("Browser will be opened for manual attempts. You must log in successfully in order to pass automation requirements.");
            Log("If logged in successfully, Press OK!");
            MessageBox.Show("Browser will be opened for manual attempts. You must log in successfully in order to pass automation requirements."
                +System.Environment.NewLine + "If logged in successfully, Press OK!");
            for (int i = 0; i < 1; i++)
            {
                _domainLogin = "vacanissan";
                if (EmailLogin() == true)
                {
                    Progress("Your ChromeDriver browser passed automation requirements!");
                    TerminateBrowser();
                    return true;
                }
                else
                {
                    TerminateBrowser();
                    Error("Your ChromeDriver browser failed automation requirements!");
                    if (MessageBox.Show("Your ChromeDriver browser failed automation requirements! Would you like to Retry?", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                    {
                        Status("Reattempting...");
                        return WebMailDebugger();
                    }
                    else
                    {
                        InitializeBrowser(true);
                        Log("Browser will be opened for manual attempts. You must log in successfully in order to pass automation requirements.");
                        Log("If logged in successfully, close everything and try again!");
                        return false;
                    }
                }

            }

            return false;
        }

        private void SetEmployeeParameters()
        {
            employee = new Person();          //Might need to enable, I've had employee object return null after new hire with newly specified role
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
                    _timezone = element.SelectSingleNode("./@timezone").InnerText;
                    _phoneA = element.SelectSingleNode("./@phoneA").InnerText;
                    _phoneB = element.SelectSingleNode("./@phoneB").InnerText;
                    _phoneC = element.SelectSingleNode("./@phoneC").InnerText;
                    _address = element.SelectSingleNode("./@address").InnerText;
                    _city = element.SelectSingleNode("./@city").InnerText;
                    _state = element.SelectSingleNode("./@state").InnerText;
                    _zip = element.SelectSingleNode("./@zip").InnerText;
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
        }

        public void SetAdminParameters()
        {
            if (_admin)
            {
***REMOVED***
***REMOVED***
***REMOVED***
***REMOVED***
***REMOVED***
***REMOVED***
***REMOVED***
***REMOVED***
***REMOVED***
***REMOVED***
***REMOVED***
***REMOVED***
***REMOVED***
***REMOVED***
***REMOVED***
***REMOVED***
***REMOVED***
***REMOVED***
***REMOVED***
***REMOVED***
***REMOVED***
***REMOVED***
***REMOVED***
***REMOVED***
***REMOVED***
***REMOVED***
***REMOVED***
***REMOVED***
***REMOVED***
***REMOVED***
            }
        }

        private void button1_Click(object sender, EventArgs e)  // DEBUG BUTTON :)
        {
            WebMailDebugger();
        }

        // Background gradient Re-draw fix for main form 
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            try
            {
                using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle,
                                                                           Color.DarkGray,
                                                                           Color.Gray,
                                                                           90F))
                {
                    e.Graphics.FillRectangle(brush, this.ClientRectangle);
                }
            }
            catch { }
        }
        




        private void btn_Process_Click(object sender, EventArgs e)
        {
            /* REMVOE ME - NOT USED
            ProcessInputField();
            */
        }
        private void btn_Commit_Click(object sender, EventArgs e)
        {
            /* REMOVE ME - NOT USED
            SetEmployeeParameters();
            SetAdminParameters();

            if (tb_FirstName.Text == "" || tb_LastName.Text == "" || tb_SSN.Text == "" || cb_Location.Text == "" || cb_Role.Text == "" || tb_EmployeeNumber.Text == "" ||
                tb_Email.Text == "" || tb_Password.Text == "")
            {
                Error("All fields must be filled out!");
                return;
            }


            if (sql.UserExist_DB(employee.fullname))
            {
                Log("[Name Conflict] " + employee.fullname + " already exists in database! Continuing...");
                //return;
            }

            // I'm honestly not sure in the difference of this method and the above right now...
            // I may want to look this over again....
            if (sql.EmailExist_DB(employee.email.username) && (cb_storeEmail.Checked || cb_wiseEmail.Checked))
            {
                Error("[Email Conflict] " + employee.email.username + " already exists in database! Stopped.");
                return;
            } 
            
            ListViewItem person = xml.AddToQueue("In Progress", employee.fullname, employee.email.username, "Add", 1, null);
            lv_queue.Items.Add(person);

            state = true;
             AddEmployee(person);
            */
        }

        private void tv_portals_AfterCheck(object sender, TreeViewEventArgs e)
        {
            SelectParents(e.Node, e.Node.Checked);
            UnselectChildren(e.Node, e.Node.Checked);
        }

        #region TreeView Node Corrections
        public void SelectParents(TreeNode node, Boolean isChecked)
        {
            var parent = node.Parent;

            if (parent == null)
                return;

            if (isChecked)
            {
                parent.Checked = true; // we should always check parent
                SelectParents(parent, true);
            }
            else
            {
                if (parent.Nodes.Cast<TreeNode>().Any(n => n.Checked))
                    return; // do not uncheck parent if there other checked nodes

                SelectParents(parent, false); // otherwise uncheck parent
            }
        }

        public void UnselectChildren(TreeNode node, Boolean isChecked)
        {
            var children = node.Nodes;
            if (children.Count > 0 && !isChecked)
            {
                foreach (TreeNode child in children)
                {
                    child.Checked = false;
                }
            }
        }
        #endregion

        private void btn_Start_Click(object sender, EventArgs e)            // NNA Multi enrollment tool
        {
            
        }               

        private void listView2_MouseClick(object sender, MouseEventArgs e)      // Right-Click Menu (Queue)
        {
            if (e.Button == MouseButtons.Right)
            {
                var focusedItem = lv_queue.FocusedItem;
                if (focusedItem != null && focusedItem.Bounds.Contains(e.Location))
                {
                    contextMenuStrip3.Show(Cursor.Position);
                }
            }
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ClearInputField();
            xml.ClearQueue();
            lv_queue.Items.Clear();
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void nNANetMultEntrollToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabTools;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TerminateBrowser();
            Application.Exit();
        }

        private void cToolStripMenuItem_Click(object sender, EventArgs e)       // Right-Click Menu (Console) -> Clear Console
        {
            tb_Console.Text = "";
        }

        private void splitContainer2_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)    // Menu-Strip Tools -> Settings
        {
            //SetEmployeeParameters();
            //SetAdminParameters();
            settings = new forms.Settings(this, xml);
            settings.ShowDialog();
            
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Project Rockwell Automation (" + _version + ")" + Environment.NewLine + "Copyright 2021 - Ian McCambridge - Wise Auto Group - IT Department" + Environment.NewLine + "GitHub: https://github.com/kidchemical/Project-Rockwell-Automation" + Environment.NewLine + "Tool for automation of New Hires, Terminations, and Transfers of employees.");
        }           // Menu-Strip Help -> About

        private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)     // Menu-Strip Help -> Update
        {
            AutoUpdater.Start(@"file://10.205.203.16/it\main\4. Useful Things\Software\Rockwell Automation\.meta\version.xml");
            //MessageBox.Show("This software is currently up to date");
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)   // Menu-Strip Help -> Support
        {
            System.Diagnostics.Process.Start("http://ticket.wiseautoit.com");
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void btn_TermSearch_Click(object sender, EventArgs e)   // Remove me - not used
        {
            /*
            ClearInputFieldTabTermination();
            TerminationSearch();
            */
        } 

        private void lv_search_MouseClick(object sender, MouseEventArgs e)      // Right-Click Menu (Search) for lv_search item
        {
            if (e.Button == MouseButtons.Right)
            {
                var focusedItem = lv_search.FocusedItem;
                if (focusedItem != null && focusedItem.Bounds.Contains(e.Location))
                {
                    contextMenuStrip2.Show(Cursor.Position);
                }
                
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)   // Open dump.log
        {
            System.Diagnostics.Process.Start(Directory.GetCurrentDirectory() + @"\dump.log");
        }

        private void tb_TermName_KeyDown(object sender, KeyEventArgs e)     //remove me -not used
        {
            /*
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                TerminationSearch();
            }
            */
        }

        private void tb_EmployeeNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                ProcessInputField();
            }
        }

        private void btn_TransferSearch_Click(object sender, EventArgs e)           //remove me -not used
        {
            //TransferSearch();
        }

        private void lv_TransferSearch_MouseDoubleClick(object sender, MouseEventArgs e)    // REMOVE ME - not needed
        {
            /*
            if (e.Button == MouseButtons.Left)  // Double-Click Search Queue item (Transfer Tab)
            {
                ClearInputField();
                var focusedItem = lv_TransferSearch.FocusedItem;
                int focusedIndex = 0; //focusedItem.Index;
                if (focusedItem != null && focusedItem.Bounds.Contains(e.Location))
                {
                    // SQL Query and Populate Access fields
                    Person[] people = sql.QueryUserByName_DB(focusedItem.Text.Remove(focusedItem.Text.IndexOf("(") - 1).Trim(), false);
                    //MessageBox.Show(focusedItem.Text + "  " + focusedIndex.ToString());
                    //MessageBox.Show(people[focusedIndex].fullname);
                    if (people[0] == null) { return; }

                    employee = people[0];

                    tb_TransferName.Text = employee.fullname;
                    tb_TransferEmail.Text = employee.email.username;

                    cb_TransferLocation.Text = employee.store;
                    cb_TransferRole.Text = employee.role;

                    //figure domain
                    if (employee.email.username.Contains("@wiseautogroup.com")) { cb_TransferWiseEmail.Checked = true; cb_TransferStoreEmail.Checked = false; }
                    else
                    {
                        if (employee.email.username.Contains("@")) { cb_TransferStoreEmail.Checked = true; cb_TransferWiseEmail.Checked = false; }
                    }

                    //figure portals
                    if (employee.dealertrack.username != "") { cb_TransferDealerTrack.Checked = true; }
                    if (employee.reynolds.username != "") { cb_TransferReynolds.Checked = true; }
                    if (employee.nnanet.username != "") { tv_TransferPortals.Nodes[0].Checked = true; }
                    if (employee.dealerconnect.username != "") { tv_TransferPortals.Nodes[1].Checked = true; }
                    if (employee.gmglobal.username != "") { tv_TransferPortals.Nodes[2].Checked = true; }
                    if (employee.hyundaidealer.username != "") { tv_TransferPortals.Nodes[3].Checked = true; }
                    if (employee.kdealer.username != "") { tv_TransferPortals.Nodes[4].Checked = true; }

                    if (employee.hdnet.username != "") { tv_TransferPortals.Nodes[5].Checked = true; }
                    if (employee.hdnet1.username != "") { tv_TransferPortals.Nodes[5].Checked = true; }
                    if (employee.hdnet2.username != "") { tv_TransferPortals.Nodes[5].Checked = true; }
                    if (employee.hdnet3.username != "") { tv_TransferPortals.Nodes[5].Checked = true; }

                    if (employee.vcc.username != "") { tv_TransferPortals.Nodes[6].Checked = true; }
                    if (employee.mxconnect.username != "") { tv_TransferPortals.Nodes[7].Checked = true; }
                    if (employee.cudl.username != "") { tv_TransferPortals.Nodes[8].Checked = true; }
                    if (employee.office365.username != "") { tv_TransferPortals.Nodes[9].Checked = true; }

                    lv_TransferSearch.Items.Clear();
                    foreach (Person person in people)
                    {
                        if (person != null)
                        {
                            ListViewItem item = lv_TransferSearch.Items.Add(person.fullname + " (" + person.tableID + ")");
                            item.ImageIndex = 0;
                            item.SubItems.Add(person.tableID);

                            if (person.status == "active")
                            {
                                item.BackColor = Color.MediumSpringGreen;
                            }
                            else
                            {
                                item.BackColor = Color.IndianRed;
                            }
                        }
                    }
                }
            }
            */
        }  

        private void lv_TransferSearch_MouseClick(object sender, MouseEventArgs e)  // Click Search Queue item (Transfer Tab)
        {
            /*
            if (e.Button == MouseButtons.Right)
            {
                var focusedItem = lv_search.FocusedItem;
                if (focusedItem != null && focusedItem.Bounds.Contains(e.Location))
                {
                    contextMenuStrip2.Show(Cursor.Position);
                }

            }
            */
        }

        private void exportAsSpreadsheetToolStripMenuItem_Click(object sender, EventArgs e) // Export as Spreadsheet... (Login sheet)
        {
            
            if (lv_database.FocusedItem.SubItems.Count <= 1)
            {
                Error("Cannot be null! [0xBuffalo]");
                return;
            }; 


            string tableID = lv_database.FocusedItem.SubItems[5].Text;
            Log("Fetching tableID " + tableID +  "...");


            //Person[] person = sql.QueryUserByName_DB(lv_database.FocusedItem.SubItems[1].Text);
            Person person = sql.Read_DB(lv_database.FocusedItem.SubItems[5].Text);




            if (person != null)
            {
                

                excel.Write_Excel(person, admin);
                Log("Login sheet created! (" + person.fullname + ")");

            }
            else
            {
                Error(lv_queue.FocusedItem.SubItems[1].Text + " not found in database!");
            }
        }

        private void toolStripComboBox1_Click(object sender, EventArgs e) //remove me -not used
        {
            //lv_queue.SelectedItems.Clear();
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lv_queue.SelectedItems)
            {
                item.Remove();
            }            
        }

        private void toolStripButton1_Click(object sender, EventArgs e)  //remove me -not used
        {

        }

        private void toolStripLabel1_Click(object sender, EventArgs e)      // ADD Button - Tool strip
        {

            cmAdd.Show(Cursor.Position);

        }

        private void toolStripLabel3_Click(object sender, EventArgs e)      // EDIT Button - Tool strip
        {
            EditButton();
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)      // DEL button - Tool Strip
        {
            cmDelete.Show(Cursor.Position);

        }

        private void tv_portals_AfterSelect(object sender, TreeViewEventArgs e)  //remove me -not used
        {

        }

        private void tb_name_KeyDown(object sender, KeyEventArgs e)  //remove me -not used
        {

        }

        private void ts_btn_search_Click(object sender, EventArgs e)
        {
            DatabaseSearch(ts_tb_search.Text);
        }

        private void lv_database_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)  // Double-Click Search Queue item (Transfer Tab)
            {
                var focusedItem = lv_database.FocusedItem;
                int focusedIndex = 0; //focusedItem.Index;
                if (focusedItem != null && focusedItem.Bounds.Contains(e.Location))
                {
                    EditButton();
                    /*
                    ts_tb_search.Text = focusedItem.Text;
                    
                    DatabaseSearch(ts_tb_search.Text);
                    */
                    
                }
            }
        }

        private void ts_tb_search_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                DatabaseSearch(ts_tb_search.Text);
            }
        }

        private void insertToolStripMenuItem_Click(object sender, EventArgs e) // Navigation Menu -> Tools -> Employee DB -> Insert into DB
        {
            employee = new Person();            

            if (UserPropertiesEdit.ShowDialog() == DialogResult.OK)
            {
                if (employee.fullname == "" || employee.fullname == null) 
                {
                    Error("An error has occured [0xGuppy]"); 
                    return; 
                }
                try
                {

                    sql.Write_DB(employee, admin);
                    Progress("Employee inserted into DB successfully");
                }
                catch (Exception x)
                {
                    Error("An error has occured [0xBass]");
                    MessageBox.Show(x.ToString());
                }
            }

            //probably best to reset...
            employee = new Person();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            DatabaseSearch(ts_tb_search.Text);
        }

        private void addAutomaticlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            employee = new Person();
            UserPropertiesAdd.ShowDialog();
        }

        private void addManuallyToolStripMenuItem_Click(object sender, EventArgs e)
        {

            employee = new Person();

            //Esape if null
            if (UserPropertiesEdit.ShowDialog() == DialogResult.OK)
            {
                if (employee.fullname == "" || employee.fullname == null) { return; }
                try
                {

                    sql.Write_DB(employee, admin);
                    Progress("Employee inserted into DB");
                }
                catch (Exception x)
                {
                    Log("An error has occured [0x03]");
                    MessageBox.Show(x.ToString());
                }
            }


            //probably best to reset...
            employee = new Person();

        }

        private void addEmployeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            employee = new Person();
            UserPropertiesAdd.ShowDialog();
        }

        private void removeEmployeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            
            //Esape if null
            if (lv_database.FocusedItem == null || lv_database.FocusedItem.Font.Italic)
            {
                MessageBox.Show("No employee selected");
                employee = new Person() ;
                return;
            }

            /*
            //Beta mode warning
            MessageBox.Show("ALERT: This is a brand new feature to the beta. Some features are not yet complete." + System.Environment.NewLine +
                "If you must manually edit the database, contact Ian McCambridge." + System.Environment.NewLine + System.Environment.NewLine + System.Environment.NewLine +
                " Enjoy your stay.", "BETA TESTING ALERT");*/


            if (MessageBox.Show("Are you sure you want to remove " + lv_database.FocusedItem.Text.Trim() + "? Doing so will completely terminate all accesss.", "WARNING", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                state = true;
                try
                {
                    employee = sql.Read_DB(lv_database.FocusedItem.SubItems[5].Text);
                    UserPropertiesRemove.ShowDialog();
                }
                catch
                {
                    Error("Something happened.... [0xGoldfish]");
                    return;
                }
                ListViewItem person = xml.AddToQueue("In Progress", lv_database.FocusedItem.Text, lv_database.FocusedItem.SubItems[1].Text, "Remove", 1, lv_database.FocusedItem.SubItems[5].Text);
                lv_queue.Items.Add(person);
                UserPropertiesRemove.RemoveEmployee(person);
                DatabaseSearch(ts_tb_search.Text);

            }
        }

        private void removeUsingAutomationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Esape if null
            if (lv_database.FocusedItem == null || lv_database.FocusedItem.Font.Italic)
            {
                MessageBox.Show("No employee selected");
                employee = new Person();
                return;
            }

            if (MessageBox.Show("Are you sure you want to remove " + lv_database.FocusedItem.Text.Trim() + "? Doing so will completely terminate all accesss.", "WARNING", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                state = true;
                try
                {
                    employee = sql.Read_DB(lv_database.FocusedItem.SubItems[5].Text);
                    UserPropertiesRemove.ShowDialog();
                }
                catch
                {
                    Error("Something happened.... [0xTrout]");
                    return;
                }
                try
                {
                    ListViewItem person = xml.AddToQueue("In Progress", lv_database.FocusedItem.Text, lv_database.FocusedItem.SubItems[1].Text, "Remove", 1, lv_database.FocusedItem.SubItems[5].Text);
                    lv_queue.Items.Add(person);
                    UserPropertiesRemove.RemoveEmployee(person);
                    DatabaseSearch(ts_tb_search.Text);
                }
                catch
                {
                    // Basically if the user clicks 'Delete using Automation' and then exits the window...
                }

            }
        }

        private void removeManuallyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lv_database.FocusedItem == null || lv_database.FocusedItem.Font.Italic)
            {
                employee = new Person();
                MessageBox.Show("No employee selected");
                return;
            }


            // Set 'Person employee' to currently selected listview item (grabs the tableID)
            try
            {
                employee = sql.Read_DB(lv_database.FocusedItem.SubItems[5].Text);
            }
            catch (Exception x)
            {
                MessageBox.Show(x.ToString());
                return;
            }

            //Esape if null
            if (employee.fullname == "" || employee.fullname == null) { return; }

            if (UserPropertiesEdit.ShowDialog() == DialogResult.OK)
            {
                if (MessageBox.Show("Save changes made to " + employee.fullname + "?", "SAVE", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {

                    try
                    {
                        sql.Update_DB(employee, admin);
                        Status("Updated the properties of " + employee.fullname + " (" + employee.status + ")");
                        DatabaseSearch(ts_tb_search.Text);     //Refresh the search to update any status changes...
                    }
                    catch (Exception x)
                    {
                        Log("An error has occured [0x04]");
                        MessageBox.Show(x.ToString());
                    }
                }
            }
        }

        private void lv_database_MouseClick(object sender, MouseEventArgs e)
        {

            if (lv_database.FocusedItem.SubItems.Count <= 1) return;

            if (e.Button == MouseButtons.Right)
                {
                    var focusedItem = lv_database.FocusedItem;
                    if (focusedItem != null && focusedItem.Bounds.Contains(e.Location))
                    {
                        contextMenuStrip3.Show(Cursor.Position);
                    }
                }


        }

        private void toolsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolsToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void office365CreatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            employee = new Person();
            string firstName = "";
            string lastName = "";
            if (InputBoxClass.InputBox("Name", "Enter employee first name:", ref firstName) == DialogResult.OK && (InputBoxClass.InputBox("Name", "Enter employee last name:", ref lastName) == DialogResult.OK))
            {

                SetAdminParameters();
                //SetEmployeeParameters();
                employee.firstname = firstName;
                employee.lastname = lastName;
                InitializeBrowser(true);
                AddEmployee_Office365Execution();
                TerminateBrowser();
                Log("Remember to update user credentials! (under construction, sorry!)");
                Progress("::DONE::");
            }
            else
            {
                Log("Office365 Creator canceled ");
            }

            //probably best to reset...
            employee = new Person();

        }

        private void automationToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void lv_search_MouseDoubleClick(object sender, MouseEventArgs e)    //Double-Click Search Queue item (Termination Tab)
        {
            if (e.Button == MouseButtons.Left)
            {
                
                var focusedItem = lv_search.FocusedItem;
                int focusedIndex = 0; //focusedItem.Index;
                if (focusedItem != null && focusedItem.Bounds.Contains(e.Location))
                {
                    //MessageBox.Show(focusedItem.Text.Substring(0, focusedItem.Text.IndexOf('(')-1));
                    ClearInputFieldTabTermination();
                    // SQL Query and Populate Access fields
                    Person[] people = sql.QueryUserByName_DB(focusedItem.Text.Substring(0, focusedItem.Text.IndexOf('(') - 1), false);
                    //MessageBox.Show(focusedItem.Text + "  " + focusedIndex.ToString());
                    //MessageBox.Show(people[focusedIndex].fullname);
                    if (people[0] == null) { return; }
                    tb_TermName.Text = people[focusedIndex].fullname;
                    tb_TermEmail.Text = people[focusedIndex].email.username;

                    //figure domain
                    if (people[focusedIndex].email.username.Contains("@wiseautogroup.com")) { cb_TermWiseEmail.Checked = true; cb_TermStoreEmail.Checked = false; }
                    else
                    { 
                        if (people[focusedIndex].email.username.Contains("@")) { cb_TermStoreEmail.Checked = true; cb_TermWiseEmail.Checked = false; }
                    }

                    //figure portals
                    if (people[focusedIndex].dealertrack.username != "") { cb_TermDealerTrack.Checked = true; }
                    if (people[focusedIndex].reynolds.username != "") { cb_TermReynolds.Checked = true; }
                    if (people[focusedIndex].nnanet.username != "") { tv_TermPortals.Nodes[0].Checked = true; }
                    if (people[focusedIndex].dealerconnect.username != "") { tv_TermPortals.Nodes[1].Checked = true; }
                    if (people[focusedIndex].gmglobal.username != "") { tv_TermPortals.Nodes[2].Checked = true; }
                    if (people[focusedIndex].hyundaidealer.username != "") { tv_TermPortals.Nodes[3].Checked = true; }
                    if (people[focusedIndex].kdealer.username != "") { tv_TermPortals.Nodes[4].Checked = true; }

                    if (people[focusedIndex].hdnet.username != "") { tv_TermPortals.Nodes[5].Checked = true; }
                    if (people[focusedIndex].hdnet1.username != "") { tv_TermPortals.Nodes[5].Checked = true; }
                    if (people[focusedIndex].hdnet2.username != "") { tv_TermPortals.Nodes[5].Checked = true; }
                    if (people[focusedIndex].hdnet3.username != "") { tv_TermPortals.Nodes[5].Checked = true; }

                    if (people[focusedIndex].vcc.username != "") { tv_TermPortals.Nodes[6].Checked = true; }
                    if (people[focusedIndex].mxconnect.username != "") { tv_TermPortals.Nodes[7].Checked = true; }
                    if (people[focusedIndex].cudl.username != "") { tv_TermPortals.Nodes[8].Checked = true; }
                    if (people[focusedIndex].office365.username != "") { tv_TermPortals.Nodes[9].Checked = true; }

                    lv_search.Items.Clear();
                    foreach (Person person in people)
                    {
                        if (person != null)
                        {
                            ListViewItem item = lv_search.Items.Add(person.fullname + " (" + person.tableID + ")");
                            item.ImageIndex = 0;
                            item.SubItems.Add(person.tableID);

                            if (person.status == "active")
                            {
                                item.BackColor = Color.MediumSpringGreen;
                            }
                            else
                            {
                                item.BackColor = Color.IndianRed;
                            }
                        }
                    }
                }
            }
        }

        private void editEmployeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditButton();
        }

        private void removeEmployeeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("To remove an employee, set their status to INACTIVE. However, you must remove their account manually using this method");
            EditButton();
        }


        private void clearToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ResetState();
            Status("All values are now cleared.");
        }

        private void btn_Terminate_Click(object sender, EventArgs e)            // Click Terminate Button
        {
            if (admin.email.username != "" && admin.dealertrack.username != "")
            {
                if (lv_search.FocusedItem != null)
                {
                    state = true;
                    ListViewItem person = xml.AddToQueue("In Progress", lv_search.FocusedItem.Text, tb_TermEmail.Text, "Remove", 1, lv_search.FocusedItem.SubItems[1].Text);
                    lv_queue.Items.Add(person);
                    RemoveEmployee(person);
                }
                else
                {
                    MessageBox.Show("No employee selected");
                }
            }
            else
            {
                Error("You must have your admin credentials filled out in the settings!");
            }
        }



        private void dealerTrackBatchPasswordResetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolDTPassResetter.ShowDialog();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            TerminateBrowser(true);
        }

        private void terminateToolStripMenuItem_Click(object sender, EventArgs e)   // Click Terminate in Right-Click Menu
        {
            if (lv_search.FocusedItem != null)
            {
                state = true;
                ListViewItem person = xml.AddToQueue("In Progress", lv_search.FocusedItem.Text, tb_TermEmail.Text, "Remove", 1, lv_search.FocusedItem.SubItems[1].Text);
                lv_queue.Items.Add(person);
                RemoveEmployee(person);
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditButton();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cmDelete.Show(Cursor.Position); 
        }



        private void webMailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WebMailDebugger();
        }

        private void cUDLToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Status("Running CUDL Debugger...");
            SetAdminParameters();
            InitializeBrowser(_showBrowser);
            CUDLLogin();
            CUDLCheckUser("none@none.com");
            TerminateBrowser();
        }

        private void debugPanelToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (debugPanel == null)
                debugPanel = new forms.DebugPanel(this);
            debugPanel.Show();
        }

        private void dealerConnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Status("Running DealerConnect Debugger...");
            SetAdminParameters();
            InitializeBrowser(_showBrowser);
            DealerConnectLogin();
            DealerConnectCheckUser("none@none.com");
            TerminateBrowser();
        }

        private void gMGlobalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Status("Running GMGlobal Debugger...");
            SetAdminParameters();
            InitializeBrowser(_showBrowser);
            GMGlobalLogin();
            GMGlobalCheckUser("none@none.com");
            TerminateBrowser();
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {

            //InitializeBrowser(_showBrowser); 
            System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=xvFZjo5PgG0");



            //GoToURL("https://youtu.be/dQw4w9WgXcQ?t=1");

        }

        private void nNACrossEnrollerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolNNANetCrossEnroller.ShowDialog();
        }

        private void cb_show_CheckedChanged(object sender, EventArgs e)
        {
            //_showBrowser = cb_show.Checked;
        }

        private void viewHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        // FORM CONTROLS
        private void ResetState()
        {
            #region Global Variables
            _domain = "";
            _domainLogin = "";
            _timezone = "";
            _phoneA = "";
            _phoneB = "";
            _phoneC = "";
            _address = "";
            _city = "";
            _state = "";
            _zip = "";
            #endregion

            employee = new Person();
            
        }

        // EZ MAP FUNCTIONS
        private void EditButton()
        {
            employee = new Person();

            if (lv_database.FocusedItem == null || lv_database.FocusedItem.Font.Italic)
            {
                //employee = new Person();
                MessageBox.Show("No employee selected");
                return;
            }


            // Set 'Person employee' to currently selected listview item (grabs the tableID)
            try
            {
                employee = sql.Read_DB(lv_database.FocusedItem.SubItems[5].Text);
            }
            catch (Exception x)
            {
                Error("SQL DB Error - Contact administraton [0xCamel]");
                MessageBox.Show(x.ToString());
                return;
            }

            //Esape if null
            if (employee.fullname == "" || employee.fullname == null) { Error("Something strange happened... [0xOwl]"); return; }


            //Create a NEW form
            UserPropertiesEdit = new forms.UserPropertiesEdit(this);


            if (UserPropertiesEdit.ShowDialog() == DialogResult.OK)
            {


                if (MessageBox.Show("Save changes made to " + employee.fullname + "?", "SAVE", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {

                    try
                    {
                        sql.Update_DB(employee, admin);
                        Status("Updated the properties of " + employee.fullname + " (" + employee.status + ")");
                        DatabaseSearch(ts_tb_search.Text);     //Refresh the search to update any status changes...
                    }
                    catch (Exception x)
                    {
                        Error("SQL DB Error - Contact administraton [0xBighorn]");
                        MessageBox.Show(x.ToString());
                    }
                }



            }
        }



       

    }
}
