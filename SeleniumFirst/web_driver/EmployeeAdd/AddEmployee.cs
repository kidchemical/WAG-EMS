/* To Do:
    -Choose XPath OR CssSelector, should have consistency
    -Accurate 'ChromeDriver.exe' termination
    -LaunchReynoldsExecution
    -LaunchDmsExecution
    -LaunchPortalExecution
*/

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Data.SqlClient;

namespace SeleniumFirst
{
    public partial class Form1
    {
        public bool AddEmployee(ListViewItem person)
        {
            InitializeBrowser(_showBrowser);
            if ((cb_storeEmail.Checked || cb_wiseEmail.Checked) && state)
                state = AddEmployee_EmailExecution();
            if (cb_dealerTrack.Checked && state)
                state = AddEmployee_DealerTrackExecution();
           /*
            if (cb_com.Checked && state)
                state = LaunchDmsExecution();
           */
            if (cb_reynolds.Checked && state)
                state = AddEmployee_ReynoldsExecution();
            if (state)
            {
                state = AddEmployee_PortalExecution();
            }
            if (state)
            {
                employee.status = "active";

                //SQL Database Entry
                Status("Submitting to Employee database... ");
                Sql_io sql = new Sql_io();
                sql.Write_DB(employee, admin);
                // SpreadhSheet Login Sheet Creation 
                Status("Creating user login sheet... ");
                Excel_io excel = new Excel_io();
                excel.Write_Excel(employee, admin);
            }

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
            xml.UpdateQueue(person);



            
            TerminateBrowser();
            return state;
        }

        private bool AddEmployee_PortalExecution()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Directory.GetCurrentDirectory() + @"\resources\portals.xml");

            if (tv_portals.Nodes[0].Checked && state == true) { state = AddEmployee_NNAExecution(); }
            if (tv_portals.Nodes[1].Checked && state == true) { state = AddEmployee_DealerConnectExecution(); }
            if (tv_portals.Nodes[2].Checked && state == true) { state = AddEmployee_GMGlobalExecution(); }
            if (tv_portals.Nodes[3].Checked && state == true) { state = AddEmployee_HyundaiDealerExecution(); }
            if (tv_portals.Nodes[4].Checked && state == true) { state = AddEmployee_KDealerExecution(); }
            //if (tv_portals.Nodes[5].Checked && state == true) { state = AddEmployee_HDNetExecution(0); } //ROOT
            if (tv_portals.Nodes[5].Nodes[0].Checked && state == true) { state = AddEmployee_HDNetExecution(1); } // Reno
            if (tv_portals.Nodes[5].Nodes[1].Checked && state == true) { state = AddEmployee_HDNetExecution(2); } // Yuba
            if (tv_portals.Nodes[5].Nodes[2].Checked && state == true) { state = AddEmployee_HDNetExecution(3); } // Redwood
            if (tv_portals.Nodes[5].Nodes[3].Checked && state == true) { state = AddEmployee_HDNetExecution(4); } // Death Valley
            if (tv_portals.Nodes[6].Checked && state == true) { state = AddEmployee_VCCExecution(); }
            if (tv_portals.Nodes[7].Checked && state == true) { state = AddEmployee_MXConnectExecution(); }
            if (tv_portals.Nodes[8].Checked && state == true) { state = AddEmployee_CUDLExecution(); }

            // experimental alert
            if (tv_portals.Nodes[9].Checked && state == true)
            {
                if (MessageBox.Show("WARNING: Office365 creation is experimental and is still being developed. It is recommended that you create office365 manually. " +
            System.Environment.NewLine + " Would you like to continue anyways?", "WARNING!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    state = AddEmployee_Office365Execution();
                }
            }


            return state;
        } 

        public bool AddEmployee_EmailExecution()
        {
            Status("Creating Email: " + employee.email.username + " Password: " + employee.email.password);

            if (state) { state = EmailLogin(); }
            if (state) { state = EmailAddUser(); }
            if (state) { employee.email.SignForAdd(admin); }

            return state;
        }

        public bool AddEmployee_ReynoldsExecution()
        {
            if (state) { state = ReynoldsLogin(); }
            if (state) { state = ReynoldsAddUser(); }
            if (state) { employee.reynolds.SignForAdd(admin); }

            return state;
        }

        public bool AddEmployee_DealerTrackExecution()
        {
            Status("Creating DealerTrack...");

            if (state) { state = DTLogin(); }
            if (state) { state = DTAddUser(); }
            if (state) { employee.dealertrack.SignForAdd(admin); }

            return state;
        }

        // To Do : I need to learn procedure for creating office365
        public bool AddEmployee_Office365Execution()
        {
            if (state) { state = Office365Login(); }
            if (state) { state = Office365AddUser(); }
            if (state) { employee.office365.SignForAdd(admin); }

            return state;
        }



        //  PORTALS
        // To Do : Assign roles (defaults to User Input!)
        //  -Implement fax
        //  -Grab user ID
        //  -Change location
        //  -Change location
        public bool AddEmployee_CUDLExecution()
        {
            if (state) { state = CUDLLogin(); }
            if (state) { state = CUDLAddUser(); }
            if (state) { employee.cudl.SignForAdd(admin); }

            return state;
        }

        public bool AddEmployee_MXConnectExecution()
        {
            if (state) { state = MXConnectLogin(); }
            if (state) { state = MXConnectAddUser(); }
            if (state) { employee.mxconnect.SignForAdd(admin); }

            return state;
        }

        // I need VCC admin to develop this
        public bool AddEmployee_VCCExecution()
        {
            if (state) { state = VCCLogin(); }
            if (state) { state = VCCAddUser(); }
            if (state) { employee.vcc.SignForAdd(admin); }

            return state;
        }

        // To Do : Assign roles, Manager, Department (defaults to cashier, no manager w/user input) 
        public bool AddEmployee_HDNetExecution(int loginID)
        {
            if (state) { state = HDNetLogin(loginID); }
            if (state) { state = HDNetAddUser(loginID); }

            if (state) { employee.hdnet.SignForAdd(admin); }
            if (state) { employee.hdnet1.SignForAdd(admin); }   // Temporary
            if (state) { employee.hdnet2.SignForAdd(admin); }   // Temporary
            if (state) { employee.hdnet3.SignForAdd(admin); }   // Temporary

            return state;
        }

        public bool AddEmployee_KDealerExecution()
        {
            if (state) { state = KDealerLogin(); }
            if (state) { state = KDealerAddUser(); }
            if (state) { employee.kdealer.SignForAdd(admin); }

            return state;
        }

        public bool AddEmployee_HyundaiDealerExecution()
        {
            if (state) { state = HyundaiDealerLogin(); }
            if (state) { state = HyundaiDealerAddUser(); }
            if (state) { employee.hyundaidealer.SignForAdd(admin); }

            return state;
        }

        // I need GMGlobal admin to develop this
        public bool AddEmployee_GMGlobalExecution()
        {
            if (state) { state = GMGlobalLogin(); }
            if (state) { state = GMGlobalAddUser(); }
            if (state) { employee.gmglobal.SignForAdd(admin); }

            return state;
        }

        // I need DealerConnect admin to develop this
        public bool AddEmployee_DealerConnectExecution()
        {
            if (state) { state = DealerConnectLogin(); }
            if (state) { state = DealerConnectAddUser(employee.dealerconnect.username); }
            if (state) { employee.dealerconnect.SignForAdd(admin); }

            return state;
        }

        //  To Do : Ability to enroll in multiple locations (done but needs testing)
        public bool AddEmployee_NNAExecution()
        {
            if (state) { state = NNANetLogin(); }
            if (state) { state = NNANetAddUser(); }
            if (state) { employee.nnanet.SignForAdd(admin); }

            return state;
        }

    }
}
