using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace SeleniumFirst
{
    public partial class Form1
    {
        public bool forwardEmail = false;
        public string input = "";


        public bool RemoveEmployee(ListViewItem person)
        {
            if (MessageBox.Show("Are you sure you want to terminate " + person.SubItems[1].Text + "?", "Warning - Terminate Employee", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                // Handles forwarding email addresses instead of deleting them (Sales employees)
                if (cb_TermStoreEmail.Checked || cb_TermWiseEmail.Checked)
                {
                    if (MessageBox.Show("Would you like to forward the email address instead of removing? This is typical for Sales employeees.", "Forward Email Address", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        ///SOS
                        InputBoxClass.InputBox("Enter Forward Email Address", "Enter the Email address you wish to forward to:", ref input);
                        if (input != null && input != "")
                        {
                            forwardEmail = true;
                        }
                        else
                        {
                            forwardEmail = false;
                        }
                    }
                    else
                    {
                        forwardEmail = false;
                    }
                }
                else
                {
                    Error("Something strange happened... [0xShrimp]");
                }

                InitializeBrowser(_showBrowser);
                SetAdminParameters();
                
                // Create Person object based on SQL DB instance

                employee = sql.Read_DB(person.SubItems[5].Text);
                // MessageBox.Show(person.SubItems[5].Text + "    " + employee.status);
                //MessageBox.Show(employee.store);
                GenerateDomainLogin(employee.store);
                

                if ((cb_TermStoreEmail.Checked || cb_TermWiseEmail.Checked) && state)
                {
                    state = RemoveEmployee_EmailExecution();
                }
                if (cb_TermDealerTrack.Checked && state)
                    state = RemoveEmployee_DealerTrackExecution();

                if (cb_TermReynolds.Checked && state)
                    state = RemoveEmployee_ReynoldsExecution();
                if (state)
                {
                    state = RemoveEmployee_PortalExecution();
                }


                if (true)       // QUICK FIX
                {
                    employee.status = "inactive";    // TEMPORARILY MOVING OUT

                    //SQL Database Entry
                    Status("Updating Employee database... ");
                    Sql_io sql = new Sql_io();
                    try
                    {
                        sql.Update_DB_Status(employee, admin);
                    }
                    catch
                    {
                        Log("Error in SQL Update_DB_Status() implementation!");
                    }
                }
                Status("Updating user login sheet... ");
                //Log("Not implemented!");
                Excel_io excel = new Excel_io();
                excel.Write_Excel(employee, admin);

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

                xml.UpdateQueue(person);
                Status("Closing driver...");
                _driver.Quit();
                tb_Progress.Value = 0;
                tb_Progress.MarqueeAnimationSpeed = 0;
                employee = null;
                Progress("::DONE::");
                
                return true;
            }
            else
            {
                person.Remove();
                // Cancel
                return false;
            }
        }

        private bool RemoveEmployee_PortalExecution()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Directory.GetCurrentDirectory() + @"\resources\portals.xml");

            if (tv_TermPortals.Nodes[0].Checked && state == true) { state = RemoveEmployee_NNAExecution(); }
            if (tv_TermPortals.Nodes[1].Checked && state == true) { state = RemoveEmployee_DealerConnectExecution(); }
            if (tv_TermPortals.Nodes[2].Checked && state == true) { state = RemoveEmployee_GMGlobalExecution(); }
            if (tv_TermPortals.Nodes[3].Checked && state == true) { state = RemoveEmployee_HyundaiDealerExecution(); }
            if (tv_TermPortals.Nodes[4].Checked && state == true) { state = RemoveEmployee_KDealerExecution(); }
            if (tv_TermPortals.Nodes[5].Checked && state == true) { state = RemoveEmployee_HDNetExecution(); }
            if (tv_TermPortals.Nodes[6].Checked && state == true) { state = RemoveEmployee_VCCExecution(); }
            if (tv_TermPortals.Nodes[7].Checked && state == true) { state = RemoveEmployee_MXConnectExecution(); }
            if (tv_TermPortals.Nodes[8].Checked && state == true) { state = RemoveEmployee_CUDLExecution(); }
            if (tv_TermPortals.Nodes[9].Checked && state == true) { state = RemoveEmployee_Office365Execution(); }

            return state;
        }

        public bool RemoveEmployee_Office365Execution()
        {
            if (state) { state = Office365Login(); }
            if (state) { state = Office365RemoveUser(); }
            if (state) { employee.office365.SignForRemove(admin); }

            return state;
        }

        public bool RemoveEmployee_CUDLExecution()
        {
            if (state) { state = CUDLLogin(); }
            if (state) { state = CUDLRemoveUser(); }
            if (state) { employee.cudl.SignForRemove(admin); }
            return state;
        }

        public bool RemoveEmployee_MXConnectExecution()
        {
            if (state) { state = MXConnectLogin(); }
            if (state) { state = MXConnectRemoveUser(); }
            if (state) { employee.mxconnect.SignForRemove(admin); }

            return state;
        }

        public bool RemoveEmployee_VCCExecution()
        {
            if (state) { state = VCCLogin(); }
            if (state) { state = VCCRemoveUser(); }
            if (state) { employee.vcc.SignForRemove(admin); }

            return state;
        }

        public bool RemoveEmployee_HDNetExecution()
        {
            // Logs in according to employee.store...
            /*
            if (state) { state = HDNetLogin(); }    
            if (state && (employee.hdnet.username != "")) { state = HDNetRemoveUser(employee.hdnet.username); }     // Reno
            if (state && (employee.hdnet1.username != "")) { state = HDNetRemoveUser(employee.hdnet1.username); }   // Yuba
            if (state && (employee.hdnet2.username != "")) { state = HDNetRemoveUser(employee.hdnet2.username); }   // Redwood
            if (state && (employee.hdnet3.username != "")) { state = HDNetRemoveUser(employee.hdnet3.username); }   // Death Valley?
            */

            // Logs in according to sub-node checked boxes...
            if (UserPropertiesRemove.termHD[0] == true) { HDNetLogin(1); HDNetRemoveUser(employee.hdnet.username); }
            if (UserPropertiesRemove.termHD[1] == true) { HDNetLogin(2); HDNetRemoveUser(employee.hdnet1.username); }
            if (UserPropertiesRemove.termHD[2] == true) { HDNetLogin(3); HDNetRemoveUser(employee.hdnet2.username); }


            if (state) { employee.hdnet.SignForRemove(admin); }
            if (state) { employee.hdnet1.SignForRemove(admin); }
            if (state) { employee.hdnet2.SignForRemove(admin); }
            if (state) { employee.hdnet3.SignForRemove(admin); }
            return state;
        }

        public bool RemoveEmployee_KDealerExecution()
        {
            if (state) { state = KDealerLogin(); }
            if (state) { state = KDealerRemoveUser(employee.kdealer.username); }
            if (state) { employee.kdealer.SignForRemove(admin); }

            return state;
        }

        public bool RemoveEmployee_GMGlobalExecution()
        {
            if (state) { state = GMGlobalLogin(); }
            if (state) { state = GMGlobalRemoveUser(); }
            if (state) { employee.gmglobal.SignForRemove(admin); }

            return state;
        }

        public bool RemoveEmployee_HyundaiDealerExecution()
        {
            if (state) { state = HyundaiDealerLogin(); }
            if (state) { state = HyundaiDealerRemoveUser(employee.hyundaidealer.username); }
            if (state) { employee.hyundaidealer.SignForRemove(admin); }

            return state;
        }

        public bool RemoveEmployee_DealerConnectExecution()
        {
            if (state) { state = DealerConnectLogin(); }
            if (state) { state = DealerConnectRemoveUser(employee.dealerconnect.username); }
            if (state) { employee.dealerconnect.SignForRemove(admin); }

            return state;
        }

        public bool RemoveEmployee_NNAExecution()
        {
            if (state) { state = NNANetLogin(); }
            if (state) { state = NNANetRemoveUser(employee.nnanet.username); }
            if (state) { employee.nnanet.SignForRemove(admin); }

            return state;
        }

        public bool RemoveEmployee_ReynoldsExecution()
        {
            Status("Removing Reynolds...");

            if (state) { state = ReynoldsLogin(); }
            if (state) { state = ReynoldsRemoveUser(); }
            if (state) { employee.reynolds.SignForRemove(admin); }

            return state;
        }

        public bool RemoveEmployee_DealerTrackExecution()
        {
            Status("Removing DealerTrack...");

            if (state) { state = DTLogin(); }
            if (state) { state = DTRemoveUser(employee.dealertrack.username); }
            if (state) { employee.dealertrack.SignForRemove(admin); }

            return state;
        }

        public bool RemoveEmployee_EmailExecution()
        {
            Status("Removing Email: " + employee.email.username);

            if (state) { state = EmailLogin(); }
            if (state) { state = EmailRemoveUser(employee.email.username, forwardEmail, input); }
            if (state) { employee.email.SignForRemove(admin); }

            return true;
        }
    }
}
