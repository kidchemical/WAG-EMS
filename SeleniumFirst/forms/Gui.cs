/*
 *  Todo: 
 *  
 * 
 */

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Firefox;
using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Drawing;
using System.Windows.Forms;
using System.Text;

namespace SeleniumFirst
{
    public partial class Form1
    {
        public void Status(string str)
        {
            if (_simpleConsole == true)
            {
                ProgressNotch(str, Color.Green, tb_Console.BackColor);    
            }
            else
            {
                PrintConsole(str, tb_Console.ForeColor, tb_Console.BackColor);
            }
            
        }

        private void ProgressNotch(string str, Color foreground, Color background)
        {
            string notch = "█";
                tb_Console.SelectionStart = tb_Console.TextLength;
                tb_Console.SelectionLength = notch.Length;
                tb_Console.SelectionColor = foreground;
                tb_Console.SelectionBackColor = background;


                //string timestamp = "[" + DateTime.Now.ToString() + "] ";
                tb_Console.AppendText(notch);
                tb_Status.Text = notch;
                tb_Console.SelectionStart = tb_Console.Text.Length;
                tb_Console.ScrollToCaret();
                this.Refresh();

                //tb_Progress.Value = 0;
                //tb_Progress.MarqueeAnimationSpeed = 0;

                tb_Console.SelectionStart = tb_Console.TextLength;
                tb_Console.SelectionLength = 0;
                tb_Console.SelectionColor = tb_Console.ForeColor;
                tb_Console.SelectionBackColor = tb_Console.BackColor;

            // LOG THE ORIGINAL STRING!
                StringBuilder sb = new StringBuilder();
                sb.Append(str);
                File.AppendAllText(Directory.GetCurrentDirectory() + @"\dump.log", sb.ToString());
                sb.Clear();
        }

        private string GetStatus()
        {
            ActiveForm.Update();
            return tb_Status.Text;

        }

        public void Error(string str)
        {
            PrintConsole("::ERROR:: " + str, Color.Red, Color.Black);
            Screenshot();
        }

        public void Log(string str)
        {
            PrintConsole(str, Color.Yellow, Color.Black);
        }

        public void Progress(string progress)
        {
            PrintConsole(progress, Color.Yellow, Color.DarkCyan);
        }

        private void PrintConsole(string str, Color foreground, Color background)
        {
            tb_Console.SelectionStart = tb_Console.TextLength;
            tb_Console.SelectionLength = str.Length;
            tb_Console.SelectionColor = foreground;
            tb_Console.SelectionBackColor = background;


            string timestamp = "[" + DateTime.Now.ToString() + "] ";
            tb_Console.AppendText(Environment.NewLine + timestamp + str);
            tb_Status.Text = str;
            tb_Console.SelectionStart = tb_Console.Text.Length;
            tb_Console.ScrollToCaret();
            this.Refresh();

            //tb_Progress.Value = 0;
            //tb_Progress.MarqueeAnimationSpeed = 0;

            tb_Console.SelectionStart = tb_Console.TextLength;
            tb_Console.SelectionLength = 0;
            tb_Console.SelectionColor = tb_Console.ForeColor;
            tb_Console.SelectionBackColor = tb_Console.BackColor;

            StringBuilder sb = new StringBuilder();
            sb.Append(Environment.NewLine + timestamp + str);
            File.AppendAllText(Directory.GetCurrentDirectory() + @"\dump.log", sb.ToString());
            sb.Clear();
        }

        public void ShowConsole(bool toggle)
        {
            if (toggle)
            {
                _showConsole = true;
                splitContainer2.Panel2Collapsed = false;

            }
            else
            {
                _showConsole = false;
                splitContainer2.Panel2Collapsed = true;
            }

        }

        public void ShowLegacyPanel(bool toggle)
        {
            if (toggle)
            {
                _showLegacyPanel = true;
                splitContainer1.Panel1Collapsed = false;
                toolStrip1.Visible = false;
                tabControl.SelectedTab = tabControl.TabPages["tabQueue"];

            }
            else
            {
                _showLegacyPanel = false;
                splitContainer1.Panel1Collapsed = true;
                toolStrip1.Visible = true;
                tabControl.SelectedTab = tabControl.TabPages["tabDatabase"];

            }

        }

        public void Screenshot()
        {
            if (_driver != null)
            {
                try
                {
                    string random = new Random().Next().ToString();
                    Screenshot screenshot = ((ITakesScreenshot)_driver).GetScreenshot();
                    screenshot.SaveAsFile(Directory.GetCurrentDirectory() + @"\logs\screenshot_" + random + ".png");
                    Log("Screenshot saved to logs folder! " + Directory.GetCurrentDirectory() + @"\logs\screenshot_" + random + ".png");
                    System.Diagnostics.Process.Start(Directory.GetCurrentDirectory() + @"\logs\screenshot_" + random + ".png");
                }
                catch (Exception e) { MessageBox.Show(e.ToString()); }
            }
        }

        public void ClearInputField()
        {
            ClearInputFieldTabNew();
            ClearInputFieldTabTransfer();
            ClearInputFieldTabTermination();
        }

        private void ClearInputFieldTabNew()
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
            cb_com.Checked = false;
            cb_reynolds.Checked = false;
            cb_talon.Checked = false;
        }
        private void ClearInputFieldTabTransfer()
        {
            foreach (TreeNode node in tv_TransferPortals.Nodes)
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

            tb_TransferEmail.Clear();
            cb_TransferStoreEmail.Checked = false;
            cb_TransferWiseEmail.Checked = false;
            cb_TransferDealerTrack.Checked = false;
            //cb_TransferCom.Checked = false;
            cb_TransferReynolds.Checked = false;
        }

        private void ClearInputFieldTabTermination()
        {
            foreach (TreeNode node in tv_TermPortals.Nodes)
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

            tb_TermEmail.Clear();
            cb_TermStoreEmail.Checked = false;
            cb_TermWiseEmail.Checked = false;
            cb_TermDealerTrack.Checked = false;
            //cb_TermCom.Checked = false;
            cb_TermReynolds.Checked = false;
        }

        public void PopulateInputField()
        {
            Status("Populating... [GUI.cs]");
            XmlDocument xmlDoc = new XmlDocument();


            // Store Location ComboBox
            xmlDoc.Load(Directory.GetCurrentDirectory() + @"\resources\stores.xml");
            foreach (XmlNode element in xmlDoc.SelectNodes("//allstores/store"))
            {
                string value = element.SelectSingleNode("./@title").InnerText;
                cb_Location.Items.Add(value);

                // legacy
                cb_TransferLocation.Items.Add(value);
            }


            // Role Position ComboBox
            xmlDoc.Load(Directory.GetCurrentDirectory() + @"\resources\roles.xml");
            foreach (XmlNode element in xmlDoc.SelectNodes("//allroles/role"))
            {
                string role = element.SelectSingleNode("./@title").InnerText;
                cb_Role.Items.Add(role);

                // legacy
                cb_TransferRole.Items.Add(role);
                cb_tools_role.Items.Add(role);
            }




            // Apply load settings from variables defined by state.XML
            //cb_show.Checked = _showBrowser;
            ShowConsole(_showConsole);
            ShowLegacyPanel(_showLegacyPanel);
        }

        public void TerminationSearch()
        {
            lv_search.Items.Clear();
            if (tb_TermName.Text != "")
            {

                Person[] people = sql.QueryUserByName_DB(tb_TermName.Text, false);
                foreach (Person person in people)
                {
                    if (person != null)
                    {
                        if (person.fullname != null)
                        {
                            ListViewItem item = lv_search.Items.Add(person.fullname + " (" + person.tableID + ")");
                            item.ImageIndex = 0;
                            item.SubItems.Add(person.tableID);
                            item.ToolTipText = person.tableID;

                            if (person.status == "active")
                            {
                                item.BackColor = Color.MediumSpringGreen;
                            }
                            else
                            {
                                item.BackColor = Color.IndianRed;
                            }
                            //MessageBox.Show(person.tableID);
                        }
                    }
                }
            }
        }

        public void TransferSearch()
        {
            lv_TransferSearch.Items.Clear();
            if (tb_TransferName.Text != "")
            {

                Person[] people = sql.QueryUserByName_DB(tb_TransferName.Text, false);
                foreach (Person person in people)
                {
                    if (person != null)
                    {
                        if (person.fullname != null)
                        {
                            ListViewItem item = lv_TransferSearch.Items.Add(person.fullname + " (" + person.tableID + ")");
                            item.ImageIndex = 0;
                            item.SubItems.Add(person.tableID);
                            item.ToolTipText = person.tableID;

                            if (person.status == "active")
                            {
                                item.BackColor = Color.MediumSpringGreen;
                            }
                            else
                            {
                                item.BackColor = Color.IndianRed;
                            }
                            //MessageBox.Show(person.tableID);
                        }
                    }
                }
            }
        }

        public void DatabaseSearch(string text)
        {
            lv_database.Items.Clear();

            if (text == "" || text == " ")
            {
                lv_database.Items.Add("Search box cannot be empty!").Font = new Font(this.Font, FontStyle.Italic);
                Log("Search box cannot be empty!");
                return;
            }

            // This creates search progress feedback within the listview database window..
            lv_database.Items.Add("Searching employee database for " + text + "...").Font = new Font(this.Font, FontStyle.Italic);
            Status("Searching for " + text + "...");

            int count = 0;
            Person[] people = sql.QueryUserByName_DB(text, false);  // This takes time depending on the user's search input...
            lv_database.Items.Clear();
            foreach (Person person in people)
            {
                if (person != null)
                {
                    if (person.fullname != null)
                    {
                        ListViewItem item = lv_database.Items.Add(person.fullname);
                        item.ImageIndex = 0;
                        item.SubItems.Add(person.email.username);
                        item.SubItems.Add(person.store);
                        item.SubItems.Add(person.role);
                        item.SubItems.Add(person.modifyDate);
                        item.SubItems.Add(person.tableID);
                        item.ToolTipText = person.tableID;

                        if (person.status == "active")
                        {
                            item.BackColor = Color.MediumSpringGreen;
                        }
                        else
                        {
                            item.BackColor = Color.IndianRed;
                        }

                        count++;
                       // MessageBox.Show(person.tableID);
                    }
                }
            }

            if (count == 0)
            {
                lv_database.Items.Clear();
                lv_database.Items.Add("No results found.").Font = new Font(this.Font, FontStyle.Italic);
            }
            Progress("(" + count.ToString() + ") results found");
        }

        public void ProcessInputField()
        {
            //Status("Processing...");
            SetEmployeeParameters();
            ClearInputField();
            GenerateRoles(employee.role, employee.store);
            if (!GenerateEmail(employee.firstname, employee.lastname, employee.store)) return;
            GeneratePassword();

            if (new Sql_io().UserExist_DB(employee.fullname))
            {
                Log(employee.fullname + " already exists in database!");
                return;
            }
            if (new Sql_io().EmailExist_DB(employee.email.username))
            {
                Log(employee.email.username + " already exists in database!");
            }
        }
    }
}
