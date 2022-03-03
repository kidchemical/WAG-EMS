using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace SeleniumFirst.forms
{
    public partial class UserPropertiesRemove : Form
    {
        bool state = true;
        public string input = "";

        public bool RemoveEmployee(ListViewItem person)
        {
            if (MessageBox.Show("Are you sure you want to terminate " + person.SubItems[1].Text + "?", "Warning - Terminate Employee", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                // Handles forwarding email addresses instead of deleting them (Sales employees)
                //*old*         if (cb_TermStoreEmail.Checked || cb_TermWiseEmail.Checked)

                if (cb_TermStoreEmail.Checked || cb_TermWiseEmail.Checked)
                {
                    mainForm.forwardEmail = false;
                    if (person.SubItems[2].Text.Contains('@'))
                    {

                        if (MessageBox.Show("Would you like to forward the email address instead of removing? This is typical for Sales employeees.", "Forward Email Address", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            ///SOS
                            InputBoxClass.InputBox("Enter Forward Email Address", "Enter the Email address you wish to forward to:", ref input);
                            mainForm.forwardEmail = true;
                            mainForm.input = this.input;
                        }
                    }
                }

                mainForm.InitializeBrowser(mainForm._showBrowser);
                mainForm.SetAdminParameters();

                // Create Person object based on SQL DB instance
                mainForm.employee = mainForm.sql.Read_DB(person.SubItems[5].Text);
                mainForm.GenerateDomainLogin(employee.store);


                if ((cb_TermStoreEmail.Checked || cb_TermWiseEmail.Checked) && state)
                    state = mainForm.RemoveEmployee_EmailExecution();
                if (cb_TermDealerTrack.Checked && state)
                    state = mainForm.RemoveEmployee_DealerTrackExecution();
                if (cb_TermReynolds.Checked && state)
                    state = mainForm.RemoveEmployee_ReynoldsExecution();
                if (state)
                    state = RemoveEmployee_PortalExecution();

                if (true)       // QUICK FIX
                {
                    mainForm.employee.status = "inactive";    // TEMPORARILY MOVING OUT

                    //SQL Database Entry
                    mainForm.Status("Updating Employee database... ");
                    Sql_io sql = new Sql_io();
                    try
                    {
                        sql.Update_DB_Status(mainForm.employee, mainForm.admin);
                    }
                    catch
                    {
                        mainForm.Log("Error in SQL Update_DB_Status() implementation!");
                    }
                }
                mainForm.Status("Updating user login sheet... ");
                //Log("Not implemented!");
                Excel_io excel = new Excel_io();
                excel.Write_Excel(mainForm.employee, mainForm.admin);
                                //MessageBox.Show("global / local state:  " + mainForm.state.ToString() + "  :  " + state.ToString());
                // FINISH EXECUTION
                if (state)
                {
                    person.Text = "Done";
                    person.ImageIndex = 2;
                    MessageBox.Show("Employee has been terminated.");
                }
                else
                {
                    person.Text = "Failed";
                    person.ImageIndex = 3;
                    MessageBox.Show("Something happened along the way... Please verify employee termination.");
                }

                mainForm.xml.UpdateQueue(person);
                mainForm.TerminateBrowser();

                // clear memory for employee state...
                mainForm.employee = null;
                employee = null;

                return true;
            }
            else
            {
                // Cancel
                person.Remove();
                
                mainForm.employee = null;
                employee = null;
                return false;
            }
        }

        private bool RemoveEmployee_PortalExecution()
        {
            mainForm.Status("RemoveEmployee_PortalExecution [0xSlime]");
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Directory.GetCurrentDirectory() + @"\resources\portals.xml");

            if (tv_TermPortals.Nodes[0].Checked && state == true) { state = mainForm.RemoveEmployee_NNAExecution(); }
            if (tv_TermPortals.Nodes[1].Checked && state == true) { state = mainForm.RemoveEmployee_DealerConnectExecution(); }
            if (tv_TermPortals.Nodes[2].Checked && state == true) { state = mainForm.RemoveEmployee_GMGlobalExecution(); }
            if (tv_TermPortals.Nodes[3].Checked && state == true) { state = mainForm.RemoveEmployee_HyundaiDealerExecution(); }
            if (tv_TermPortals.Nodes[4].Checked && state == true) { state = mainForm.RemoveEmployee_KDealerExecution(); }
            if (tv_TermPortals.Nodes[5].Checked && state == true) { state = mainForm.RemoveEmployee_HDNetExecution(); }
            if (tv_TermPortals.Nodes[6].Checked && state == true) { state = mainForm.RemoveEmployee_VCCExecution(); }
            if (tv_TermPortals.Nodes[7].Checked && state == true) { state = mainForm.RemoveEmployee_MXConnectExecution(); }
            if (tv_TermPortals.Nodes[8].Checked && state == true) { state = mainForm.RemoveEmployee_CUDLExecution(); }
            if (tv_TermPortals.Nodes[9].Checked && state == true) { state = mainForm.RemoveEmployee_Office365Execution(); }

            return state;
        }
    }
}
