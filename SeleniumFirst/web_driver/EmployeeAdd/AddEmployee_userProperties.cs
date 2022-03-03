using System.IO;
using System.Xml;
using System.Windows.Forms;
using System;

namespace SeleniumFirst.forms
{
    public partial class UserPropertiesAdd : Form
    {
        bool state = true;

        public bool AddEmployee(ListViewItem person)
        {
            mainForm.InitializeBrowser(mainForm._showBrowser);
            if ((cb_storeEmail.Checked || cb_wiseEmail.Checked) && mainForm.state)
                state = mainForm.AddEmployee_EmailExecution();
            if (cb_dealerTrack.Checked && state)
                state = mainForm.AddEmployee_DealerTrackExecution();
            /*
             if (cb_com.Checked && state)
                 state = LaunchDmsExecution();

             if (cb_reynolds.Checked && state)
                 state = mainForm.AddEmployee_ReynoldsExecution();
            */
            if (state)
            {
                state = AddEmployee_PortalExecution();
            }

            if (!state)
            { 
                mainForm.Log("Automation encountered an issue. Submitting to Employee database... ");
                mainForm.employee.notes += "*Automation stopped during account creation*";
            }

            mainForm.employee.status = "active";
            //SQL Database Entry
            mainForm.Status("Submitting to Employee database... ");
            Sql_io sql = new Sql_io();
            sql.Write_DB(mainForm.employee, mainForm.admin);
            // SpreadhSheet Login Sheet Creation 
            mainForm.Status("Creating user login sheet... ");
            Excel_io excel = new Excel_io();
            excel.Write_Excel(mainForm.employee, mainForm.admin);






            // FINISH EXECUTION
            if (state)
            {
                person.Text = "Done";
                person.ImageIndex = 2;
            }
            else
            {
                person.Text = "Failed";
                person.ImageIndex = 3;
            }

            mainForm.xml.UpdateQueue(person);
            mainForm.TerminateBrowser();
            return state;
        }

        private bool AddEmployee_PortalExecution()
        {

            if (QuarterRestrictionOverride() == false)
            {
                return true;
            }
            else
            { 
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Directory.GetCurrentDirectory() + @"\resources\portals.xml");

            if (tv_portals.Nodes[0].Checked && state == true) { state = mainForm.AddEmployee_NNAExecution(); }
            if (tv_portals.Nodes[1].Checked && state == true) { state = mainForm.AddEmployee_DealerConnectExecution(); }
            if (tv_portals.Nodes[2].Checked && state == true) { state = mainForm.AddEmployee_GMGlobalExecution(); }
            if (tv_portals.Nodes[3].Checked && state == true) { state = mainForm.AddEmployee_HyundaiDealerExecution(); }
            if (tv_portals.Nodes[4].Checked && state == true) { state = mainForm.AddEmployee_KDealerExecution(); }
            //if (tv_portals.Nodes[5].Checked && state == true) { state = AddEmployee_HDNetExecution(0); } //ROOT
            if (tv_portals.Nodes[5].Nodes[0].Checked && state == true) { state = mainForm.AddEmployee_HDNetExecution(1); } // Reno
            if (tv_portals.Nodes[5].Nodes[1].Checked && state == true) { state = mainForm.AddEmployee_HDNetExecution(2); } // Yuba
            if (tv_portals.Nodes[5].Nodes[2].Checked && state == true) { state = mainForm.AddEmployee_HDNetExecution(3); } // Redwood
            if (tv_portals.Nodes[5].Nodes[3].Checked && state == true) { state = mainForm.AddEmployee_HDNetExecution(4); } // Death Valley
            if (tv_portals.Nodes[6].Checked && state == true) { state = mainForm.AddEmployee_VCCExecution(); }
            if (tv_portals.Nodes[7].Checked && state == true) { state = mainForm.AddEmployee_MXConnectExecution(); }
            if (tv_portals.Nodes[8].Checked && state == true) { state = mainForm.AddEmployee_CUDLExecution(); }

            // experimental alert
            if (tv_portals.Nodes[9].Checked && state == true)
            {
                if (MessageBox.Show("WARNING: Office365 creation is experimental and is still being developed. It is recommended that you create office365 manually. " +
                    System.Environment.NewLine + " Would you like to continue anyways?", "WARNING!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    state = mainForm.AddEmployee_Office365Execution();
                }
            }

  
            return state;
            }

        }

        private bool QuarterRestrictionOverride()
        {
            // Quarterly Cutoff for Portal Creation - This prevents personnel from creating portals if the date is halfway through the month of the quarter

            int thisMonth = DateTime.Now.Month;
            int thisDay = DateTime.Now.Day;
            if ((thisMonth == 3 || thisMonth == 6 || thisMonth == 9 || thisMonth == 12) && thisDay >= 14)
            {
                mainForm.Log("::ALERT!!!:: No portal additions are allowed during end of the quarter :: Override?");
                if (MessageBox.Show("No portal additions are allowed during end of the quarter. Would you like to override?", "Quarter Portal Restriction", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    mainForm.Log("Overriding portal creation...");
                    return true;
                }
                else
                {
                    mainForm.Log("Continuing without portal creation...");
                    return false;
                }
            }
            else
            {
                // Quarterly Restriction does not apply....
                return true;
            }
        }


    }
}
