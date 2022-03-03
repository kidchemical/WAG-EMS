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
    public partial class UserPropertiesEdit : Form
    {
        Form1 mainForm;
        Person employee; // Might be safer to reference directly to mainForm.employeee rather than this instance of employee..

        public UserPropertiesEdit(Form1 mainForm)
        {
            this.mainForm = mainForm;
            this.employee = mainForm.employee;
            InitializeComponent();
            PopulateInputField();   // Fill in combo-dropdown boxes
        }



        private void UserProperties_Load(object sender, EventArgs e)
        {
            ClearInputField();
            //SearchPerson(employee.fullname);
            PopulateUsingEmployeeState(mainForm.employee);
        }



        // FORM INPUT MANAGEMENT
        private void PopulateUsingEmployeeState(Person person)
        {
            this.employee = person;


            //tb_name.Text = employee.fullname;
            tb_editName.Text = employee.fullname;
            cb_Role.Text = employee.role;
            tb_employeeNumber.Text = employee.employeenumber;
            tb_notes.Text = employee.notes;
            cb_Location.Text = employee.store;
            cb_Role.Text = employee.role;
            //tb_ssn.Text = employee.ssn;
            if (employee.status != null) { cb_status.Text = employee.status.ToUpper(); }
            if (employee.status == "active") cb_status.BackColor = Color.SeaGreen;
            else cb_status.BackColor = Color.IndianRed;

            //Change Details - Dates and Modifications
            DateTime.TryParse(employee.addDate, out DateTime result1);
            DateTime.TryParse(employee.modifyDate, out DateTime result2);
            DateTime.TryParse(employee.removeDate, out DateTime result3);

            if (result1.Year > 1950)
            {
                string resultA = DateTime.Parse(result1.ToString()).ToString("MM/dd/yyyy");
                lbl_created.Text = resultA.ToString() + " [" + employee.email.addBy + "]";
            }

            if (result2.Year > 1950)
            {
                string resultB = DateTime.Parse(result2.ToString()).ToString("MM/dd/yyyy");
                //lbl_modified.Text = resultB.ToString() + " [" + employee.modifyBy + "]";
                lbl_modified.Text = resultB.ToString();
            }

            if (result3.Year > 1950)
            {
                string resultC = DateTime.Parse(result3.ToString()).ToString("MM/dd/yyyy");
                lbl_removed.Text = resultC.ToString() + " [" + employee.email.removeBy + "]";
            }






            //Email
            tb_email_username.Text = employee.email.username;
            tb_email_password.Text = employee.email.password;
            //DealerTrack
            tb_dealertrack_username.Text = employee.dealertrack.username;
            tb_dealertrack_password.Text = employee.dealertrack.password;
            //Reynolds
            tb_reynolds_username.Text = employee.reynolds.username;
            tb_reynolds_password.Text = employee.reynolds.password;
            //TALONes
            tb_talones_username.Text = employee.talones.username;
            tb_talones_password.Text = employee.talones.password;

            //NNANet
            tb_nnanet_username.Text = employee.nnanet.username;
            tb_nnanet_password.Text = employee.nnanet.password;
            //DealerConnect
            tb_dealerconnect_username.Text = employee.dealerconnect.username;
            tb_dealerconnect_password.Text = employee.dealerconnect.password;
            //GMGlobal
            tb_gmglobal_username.Text = employee.gmglobal.username;
            tb_gmglobal_password.Text = employee.gmglobal.password;
            //HyundaiDealer
            tb_hyundaidealer_username.Text = employee.hyundaidealer.username;
            tb_hyundaidealer_password.Text = employee.hyundaidealer.password;
            //KDealer
            tb_kdealer_username.Text = employee.kdealer.username;
            tb_kdealer_password.Text = employee.kdealer.password;
            //HDNet Reno
            tb_hdnet_username.Text = employee.hdnet.username;
            tb_hdnet_password.Text = employee.hdnet.password;
            //HDNet1 Yuba
            tb_hdnet1_username.Text = employee.hdnet1.username;
            tb_hdnet1_password.Text = employee.hdnet1.password;
            //HDNet2 Redwood
            tb_hdnet2_username.Text = employee.hdnet2.username;
            tb_hdnet2_password.Text = employee.hdnet2.password;
            //HDNet3 DeathValley
            tb_hdnet3_username.Text = employee.hdnet3.username;
            tb_hdnet3_password.Text = employee.hdnet3.password;
            //HDNet4 Coronado Beach
            tb_hdnet4_username.Text = employee.hdnet4.username;
            tb_hdnet4_password.Text = employee.hdnet4.password;
            //HDNet5 Orange County
            tb_hdnet5_username.Text = employee.hdnet5.username;
            tb_hdnet5_password.Text = employee.hdnet5.password;
            /* Talones
            //HDNet3
            tb_email_username.Text = employee.email.username;
            tb_email_password.Text = employee.email.password;  */
            //
            tb_gmglobal_username.Text = employee.gmglobal.username;
            tb_gmglobal_password.Text = employee.gmglobal.password;
            //VCC
            tb_vcc_username.Text = employee.vcc.username;
            tb_vcc_password.Text = employee.vcc.password;
            //MXConnect
            tb_mxconnect_username.Text = employee.mxconnect.username;
            tb_mxconnect_password.Text = employee.mxconnect.password;
            //CUDL
            tb_cudl_username.Text = employee.cudl.username;
            tb_cudl_password.Text = employee.cudl.password;
            //Office365
            tb_office365_username.Text = employee.office365.username;
            tb_office365_password.Text = employee.office365.password;

            //fiexpress
            tb_fiexpress_username.Text = employee.fiexpress.username;
            tb_fiexpress_password.Text = employee.fiexpress.password;
            //dmvdesk
            tb_dmvdesk_username.Text = employee.dmvdesk.username;
            tb_dmvdesk_password.Text = employee.dmvdesk.password;
            //vinsolutions
            tb_vinsolutions_username.Text = employee.vinsolutions.username;
            tb_vinsolutions_password.Text = employee.vinsolutions.password;
            //carwars
            tb_carwars_username.Text = employee.carwars.username;
            tb_carwars_password.Text = employee.carwars.password;

            //rapidrecon
            tb_rapidrecon_username.Text = employee.rapidrecon.username;
            tb_rapidrecon_password.Text = employee.rapidrecon.password;
            //vauto
            tb_vauto_username.Text = employee.vauto.username;
            tb_vauto_password.Text = employee.vauto.password;




            //figure domain
            if (employee.email.username.Contains("@wiseautogroup.com")) { cb_wiseEmail.Checked = true; cb_storeEmail.Checked = false; }
            else
            {
                if (employee.email.username.Contains("@")) { cb_storeEmail.Checked = true; cb_wiseEmail.Checked = false; }
            }

            //figure portals
            if (employee.dealertrack.username != "") { cb_TransferDealerTrack.Checked = true; }
            if (employee.reynolds.username != "") { cb_TransferReynolds.Checked = true; }
            if (employee.nnanet.username != "") { tv_Portals.Nodes[0].Checked = true; }
            if (employee.dealerconnect.username != "") { tv_Portals.Nodes[1].Checked = true; }
            if (employee.gmglobal.username != "") { tv_Portals.Nodes[2].Checked = true; }
            if (employee.hyundaidealer.username != "") { tv_Portals.Nodes[3].Checked = true; }
            if (employee.kdealer.username != "") { tv_Portals.Nodes[4].Checked = true; }

            if (employee.hdnet.username != "") { tv_Portals.Nodes[5].Checked = true; }
            if (employee.hdnet1.username != "") { tv_Portals.Nodes[5].Checked = true; }
            if (employee.hdnet2.username != "") { tv_Portals.Nodes[5].Checked = true; }
            if (employee.hdnet3.username != "") { tv_Portals.Nodes[5].Checked = true; }

            if (employee.vcc.username != "") { tv_Portals.Nodes[6].Checked = true; }
            if (employee.mxconnect.username != "") { tv_Portals.Nodes[7].Checked = true; }
            if (employee.cudl.username != "") { tv_Portals.Nodes[8].Checked = true; }
            if (employee.office365.username != "") { tv_Portals.Nodes[9].Checked = true; }




        }

        private void PopulateInputField()
        {
            //mainForm.Status("Populating...");
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
        }

        private void ClearInputField()
        {
            mainForm.Status("Clearing input fields... [UserPropertiesEdit.cs]");
            this.employee = null;
            // treeview Portals reset
            foreach (TreeNode node in tv_Portals.Nodes)
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

            tb_email_username.Clear();
            cb_storeEmail.Checked = false;
            cb_wiseEmail.Checked = false;
            cb_TransferDealerTrack.Checked = false;
            //cb_TransferCom.Checked = false;
            cb_TransferReynolds.Checked = false;

            tb_editName.ResetText();
            cb_Location.ResetText();
            cb_Role.Text = "";
            tb_employeeNumber.ResetText();
            cb_status.ResetText();



            lbl_created.Text = "";
            lbl_modified.Text = "";
            lbl_removed.Text = "";
        }


        #region Code to Delete ***
        // ???
        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_Commit_Click_1(object sender, EventArgs e)
        {
            SaveDialogChanges();
            this.Close();

        }

        private void SaveDialogChanges()
        {
            mainForm.Log("Saving user changes...");

            // Convert this.employee to the state employee
            SetEmployeeParameters();
            mainForm.employee = this.employee;

            this.DialogResult = DialogResult.OK;

        }

        private void btn_Process_Click_1(object sender, EventArgs e)
        {

        }

        private void btn_Commit_Click(object sender, EventArgs e)
        {

        }

        private void btn_Process_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void tb_EmployeeNumber_KeyDown(object sender, KeyEventArgs e)
        {

        }
        #endregion

        private void button1_Click_1(object sender, EventArgs e)    // Export Spreadsheet BUTTON - Login sheet xlsx
        {
            if (mainForm.employee.fullname != null)
            {
                if (mainForm.employee.fullname.Trim() != "")
                {
                    if (MessageBox.Show("Save changes made to " + employee.fullname + "?", "SAVE", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {

                        try
                        {
                            mainForm.sql.Update_DB(mainForm.employee, mainForm.admin);
                            mainForm.Status("Updated the properties of " + employee.fullname + " (" + employee.status + ")");
                            //mainForm.DatabaseSearch(ts_tb_search.Text);     //Refresh the search to update any status changes...

                            SaveDialogChanges();
                            mainForm.excel.Write_Excel(mainForm.employee, mainForm.admin);
                            return;
                        }
                        catch (Exception x)
                        {
                            mainForm.Error("SQL DB Error - Contact administraton [0xBighorn]");
                            MessageBox.Show(x.ToString());
                        }
                    }
                    else
                    {
                        mainForm.Error("Export spreadsheet aborted!");
                    }

                }
            }

            // if we reached this point, we are in a blank state...
            MessageBox.Show("You must submit user changes before exporting as speadsheet!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void SetEmployeeParameters()
        {
            //var employee = mainForm.employee;
            employee.fullname = tb_editName.Text;
            //employee.firstname = employee.GuessFirstName();
            //employee.lastname = employee.GuessLastName();


            // this isnt setting for some reason....
            employee.role = cb_Role.Text;

            employee.department = "";
            employee.store = cb_Location.Text;
            employee.notes = tb_notes.Text;
            employee.email.password = tb_email_password.Text;
            //employee.ssn = tb_SSN.Text;
            employee.employeenumber = tb_employeeNumber.Text;
            employee.status = cb_status.Text.ToLower();


            #region Usernames
            employee.email.username = tb_email_username.Text;
            employee.dealertrack.username = tb_dealertrack_username.Text;
            employee.reynolds.username = tb_reynolds_username.Text;
            employee.talones.username = tb_talones_username.Text;
            employee.nnanet.username = tb_nnanet_username.Text;
            employee.dealerconnect.username = tb_dealerconnect_username.Text;
            employee.gmglobal.username = tb_gmglobal_username.Text;
            employee.hyundaidealer.username = tb_hyundaidealer_username.Text;
            employee.kdealer.username = tb_kdealer_username.Text;
            employee.hdnet.username = tb_hdnet_username.Text;
            employee.hdnet1.username = tb_hdnet1_username.Text;
            employee.hdnet2.username = tb_hdnet2_username.Text;
            employee.hdnet3.username = tb_hdnet3_username.Text; //Death Valley
            employee.hdnet4.username = tb_hdnet4_username.Text; //Coronado Beach
            employee.hdnet5.username = tb_hdnet5_username.Text; //Orange County
            employee.vcc.username = tb_vcc_username.Text;
            employee.mxconnect.username = tb_mxconnect_username.Text;
            employee.cudl.username = tb_cudl_username.Text;
            employee.office365.username = tb_office365_username.Text;

            employee.fiexpress.username = tb_fiexpress_username.Text;
            employee.dmvdesk.username = tb_dmvdesk_username.Text;
            employee.vinsolutions.username = tb_vinsolutions_username.Text;
            employee.carwars.username = tb_carwars_username.Text;

            employee.rapidrecon.username = tb_rapidrecon_username.Text;
            employee.vauto.username = tb_vauto_username.Text;
            #endregion

            #region Passwords
            employee.email.password = tb_email_password.Text;
            employee.dealertrack.password = tb_dealertrack_password.Text;
            employee.reynolds.password = tb_reynolds_password.Text;
            employee.talones.password = tb_talones_password.Text;
            employee.nnanet.password = tb_nnanet_password.Text;
            employee.dealerconnect.password = tb_dealerconnect_password.Text;
            employee.gmglobal.password = tb_gmglobal_password.Text;
            employee.hyundaidealer.password = tb_hyundaidealer_password.Text;
            employee.kdealer.password = tb_kdealer_password.Text;
            employee.hdnet.password = tb_hdnet_password.Text;
            employee.hdnet1.password = tb_hdnet1_password.Text;
            employee.hdnet2.password = tb_hdnet2_password.Text;
            employee.hdnet3.password = tb_hdnet3_password.Text; //Death Valley
            employee.hdnet4.password = tb_hdnet4_password.Text; //Coronado Beach
            employee.hdnet5.password = tb_hdnet5_password.Text; //Orange County
            employee.vcc.password = tb_vcc_password.Text;
            employee.mxconnect.password = tb_mxconnect_password.Text;
            employee.cudl.password = tb_cudl_password.Text;
            employee.office365.password = tb_office365_password.Text;


            employee.fiexpress.password = tb_fiexpress_password.Text;
            employee.dmvdesk.password = tb_dmvdesk_password.Text;
            employee.vinsolutions.password = tb_vinsolutions_password.Text;
            employee.carwars.password = tb_carwars_password.Text;

            employee.rapidrecon.password = tb_rapidrecon_password.Text;
            employee.vauto.password = tb_vauto_password.Text;
            #endregion

            #region Timestamp and Signatures

            if (employee.email.username != "" && employee.email.addDate == "") { employee.email.SignForAdd(mainForm.admin); }
            if (employee.dealertrack.username != "" && employee.dealertrack.addDate == "") { employee.dealertrack.SignForAdd(mainForm.admin); }
            if (employee.reynolds.username != "" && employee.reynolds.addDate == "") { employee.reynolds.SignForAdd(mainForm.admin); }
            if (employee.talones.username != "" && employee.talones.addDate == "") { employee.talones.SignForAdd(mainForm.admin); }
            if (employee.nnanet.username != "" && employee.nnanet.addDate == "") { employee.nnanet.SignForAdd(mainForm.admin); }
            if (employee.dealerconnect.username != "" && employee.email.addDate == "") { employee.email.SignForAdd(mainForm.admin); }
            if (employee.gmglobal.username != "" && employee.gmglobal.addDate == "") { employee.gmglobal.SignForAdd(mainForm.admin); }
            if (employee.hyundaidealer.username != "" && employee.hyundaidealer.addDate == "") { employee.hyundaidealer.SignForAdd(mainForm.admin); }
            if (employee.kdealer.username != "" && employee.kdealer.addDate == "") { employee.kdealer.SignForAdd(mainForm.admin); }
            if (employee.hdnet.username != "" && employee.hdnet.addDate == "") { employee.hdnet.SignForAdd(mainForm.admin); }
            if (employee.hdnet1.username != "" && employee.hdnet1.addDate == "") { employee.hdnet1.SignForAdd(mainForm.admin); }
            if (employee.hdnet2.username != "" && employee.hdnet2.addDate == "") { employee.hdnet2.SignForAdd(mainForm.admin); }
            if (employee.hdnet3.username != "" && employee.hdnet3.addDate == "") { employee.hdnet3.SignForAdd(mainForm.admin); }
            if (employee.hdnet4.username != "" && employee.hdnet4.addDate == "") { employee.hdnet4.SignForAdd(mainForm.admin); }
            if (employee.hdnet5.username != "" && employee.hdnet5.addDate == "") { employee.hdnet5.SignForAdd(mainForm.admin); }
            if (employee.vcc.username != "" && employee.vcc.addDate == "") { employee.vcc.SignForAdd(mainForm.admin); }
            if (employee.mxconnect.username != "" && employee.mxconnect.addDate == "") { employee.mxconnect.SignForAdd(mainForm.admin); }
            if (employee.cudl.username != "" && employee.cudl.addDate == "") { employee.cudl.SignForAdd(mainForm.admin); }
            if (employee.office365.username != "" && employee.office365.addDate == "") { employee.office365.SignForAdd(mainForm.admin); }

            if (employee.fiexpress.username != "" && employee.fiexpress.addDate == "") { employee.fiexpress.SignForAdd(mainForm.admin); }
            if (employee.dmvdesk.username != "" && employee.dmvdesk.addDate == "") { employee.dmvdesk.SignForAdd(mainForm.admin); }
            if (employee.vinsolutions.username != "" && employee.vinsolutions.addDate == "") { employee.vinsolutions.SignForAdd(mainForm.admin); }
            if (employee.carwars.username != "" && employee.carwars.addDate == "") { employee.carwars.SignForAdd(mainForm.admin); }

            if (employee.rapidrecon.username != "" && employee.rapidrecon.addDate == "") { employee.rapidrecon.SignForAdd(mainForm.admin); }
            if (employee.vauto.username != "" && employee.vauto.addDate == "") { employee.vauto.SignForAdd(mainForm.admin); }


            // This may not be needed (modify date is set somewhere else.......


            #endregion



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
                    //mainForm.GenerateDomainLogin(employee.store);
                    employee.department = element.SelectSingleNode("./@department").InnerText;
                    break; //there shouldn't be multiple....
                }
            }
        }

        private void button77_Click(object sender, EventArgs e)     // DealerTrack ADD + button 
        {
            #region DealerTrack ADD + Button
            if (MessageBox.Show("To add a DealerTrack account for " + employee.fullname + " using automation, you must save any changes."
                + System.Environment.NewLine + "Save and continue?", "Save Changes", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                QuickStart();
                mainForm.AddEmployee_DealerTrackExecution();
                QuickEnd();
            }

            #endregion
        }



        private void groupBox24_Enter(object sender, EventArgs e)
        {
            // delete me
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // do nothing
        }

        private void button7_Click(object sender, EventArgs e) // NNA ADD +
        {
            #region NNANet ADD + Button
            if (MessageBox.Show("To add a NNANet account for " + employee.fullname + " using automation, you must save any changes."
                + System.Environment.NewLine + "Save and continue?", "Save Changes", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                QuickStart();
                mainForm.AddEmployee_NNAExecution();
                QuickEnd();
            }

            #endregion
        }

        private void button53_Click(object sender, EventArgs e) // HyundaiDealer ADD +
        {
            #region HyundaiDealer ADD + Button
            if (MessageBox.Show("To add a HyundaiDealer account for " + employee.fullname + " using automation, you must save any changes."
             + System.Environment.NewLine + "Save and continue?", "Save Changes", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                QuickStart();
                mainForm.AddEmployee_HyundaiDealerExecution();
                QuickEnd();
            }

            #endregion
        }

        private void button11_Click(object sender, EventArgs e) // KDealer ADD +
        {
            #region KDealer ADD + Button
            if (MessageBox.Show("To add a KDealer account for " + employee.fullname + " using automation, you must save any changes."
                + System.Environment.NewLine + "Save and continue?", "Save Changes", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                QuickStart();
                mainForm.AddEmployee_CUDLExecution();
                QuickEnd();
            }

            #endregion
        }

        private void button29_Click(object sender, EventArgs e)
        {
            #region MXConnect ADD + Button
            if (MessageBox.Show("To add a MXConnect account for " + employee.fullname + " using automation, you must save any changes."
            + System.Environment.NewLine + "Save and continue?", "Save Changes", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                QuickStart();
                mainForm.AddEmployee_MXConnectExecution();
                QuickEnd();
            }

            #endregion
        }

        private void button21_Click(object sender, EventArgs e) // Office365 ADD +
        {
            #region Office365 ADD + Button
            if (MessageBox.Show("To add a Office365 account for " + employee.fullname + " using automation, you must save any changes."
                + System.Environment.NewLine + "Save and continue?", "Save Changes", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                QuickStart();
                mainForm.AddEmployee_Office365Execution();
                QuickEnd();
            }

            #endregion

        }

        private void button3_Click(object sender, EventArgs e)
        {
            #region Email REMOVE - Button
            if (MessageBox.Show("To remove a WebMail account for " + employee.fullname + " using automation, you must save any changes."
                + System.Environment.NewLine + "Save and continue?", "Save Changes", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                SaveDialogChanges();
                if (employee.fullname == "" || employee.fullname == null) { mainForm.Error("Employee cannot be blank or null! [0xAntEater]"); return; }

                //mainForm.employee.GetFirstLastName();
                mainForm.employee.status = "active";
               
                mainForm.GenerateRoles(employee.role, employee.store);
                mainForm.GenerateDomainLogin(employee.store); //this should be part of generateRole..............
                mainForm.SetAdminParameters();
                mainForm.InitializeBrowser(mainForm._showBrowser);
                mainForm.RemoveEmployee_EmailExecution();
                mainForm.TerminateBrowser();


                if (employee.fullname == "" || employee.fullname == null) { mainForm.Error("Employee cannot be blank or null! [0xAntEater]"); return; }
                try
                {
                    // We might have to skip this for now and insert manually....
                    mainForm.employee.status = "active";
                    mainForm.sql.Update_DB(mainForm.employee, mainForm.admin);
                    mainForm.Progress("Employee DB instance updated");
                }
                catch (Exception x)
                {
                    mainForm.Log("An error has occured relating to SQL Updating DB [0xPython]");
                    MessageBox.Show(x.ToString());
                }


                //probably best to reset...
                employee = new Person();
                // SetEmployeeParameters();
                //mainForm.sql.Update_DB()
            }

            #endregion
        }

        private void button2_Click(object sender, EventArgs e)
        {
            #region WemMail ADD + Button

            // Check to see if an existing email exists for this user...
            if (mainForm.employee.email.username.Trim().Length != 0)
            {
                if (MessageBox.Show("This person already has an email address: " + mainForm.employee.email.username
                    + System.Environment.NewLine + "Would you like to create a new email address? " +
                    tb_email_username + System.Environment.NewLine +
                    tb_email_password + System.Environment.NewLine + 
                    cb_Location, "Duplicate email", MessageBoxButtons.YesNo) != DialogResult.Yes)
                {
                    mainForm.Error("Email creation stopped. [0xCricket]");
                    return;
                }

            }
            


            if (MessageBox.Show("To add a WebMail account for " + employee.fullname + " using automation, you must save any changes."
                + System.Environment.NewLine + "Save and continue?", "Save Changes", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                SaveDialogChanges();

                //mainForm.employee.GetFirstLastName();
                mainForm.employee.status = "active";
                mainForm.GenerateRoles(employee.role, employee.store);
                mainForm.SetAdminParameters();
                mainForm.InitializeBrowser(mainForm._showBrowser);
                mainForm.AddEmployee_EmailExecution();
                mainForm.TerminateBrowser();

                if (employee.fullname == "" || employee.fullname == null) { mainForm.Error("Employee cannot be blank or null! [0xAntEater]"); return; }

                try
                {
                    // We might have to skip this for now and insert manually....
                    mainForm.employee.status = "active";
                    mainForm.sql.Update_DB(mainForm.employee, mainForm.admin);
                    mainForm.Progress("Employee DB instance updated");
                }
                catch (Exception x)
                {
                    mainForm.Log("An error has occured relating to SQL Updating DB [0xPython]");
                    MessageBox.Show(x.ToString());
                }


                //probably best to reset...
                employee = new Person();
                // SetEmployeeParameters();
                //mainForm.sql.Update_DB()
            }
            
            #endregion
        }

        private void button76_Click(object sender, EventArgs e)
        {

        }

        // Quick Start and Quick Stop methods for initialization and closing of driver and process
        private bool QuickStart()
        {
            SaveDialogChanges();

            //mainForm.employee.GetFirstLastName();
            mainForm.employee.status = "active";
            mainForm.GenerateRoles(employee.role, employee.store); // should this be mainForm referenced?...
            mainForm.SetAdminParameters();

            ///////////////
            /// Input first and last name
            /// ///////////
            /// 
            string input = "";
            if (InputBoxClass.InputBox("First Name", "Enter employee's first name", ref input) == DialogResult.OK)
            {
                mainForm.employee.firstname = input;
                if (InputBoxClass.InputBox("Last Name", "Enter employee's last name", ref input) == DialogResult.OK)
                {
                    mainForm.employee.lastname = input;
                }
                else
                {
                    mainForm.Log("Name cannot be blank! Stopped.");
                    return false;
                }
            }
            else
            {
                mainForm.Log("Name cannot be blank! Stopped.");
                return false;
            }

            if (employee.firstname == "" || employee.lastname == "")
            {
                mainForm.Log("Name cannot be blank! Stopped.");
                return false ;
            }

            // Get SSN
            if (mainForm.employee.ssn == "" || mainForm.employee.ssn == null)
            {
                if (InputBoxClass.InputBox("Social Security Number", "Enter employee's SSN", ref input) == DialogResult.OK)
                {
                    mainForm.employee.ssn = input;
                }
                else
                {
                    mainForm.Error("SSN is required for some portals!");
                    return false;
                }
            }

            mainForm.InitializeBrowser(mainForm._showBrowser);
            //mainForm.AddEmployee_DealerTrackExecution();

            return true;

        }

        private bool QuickEnd()
        {
            mainForm.TerminateBrowser();


            if (employee.fullname == "" || employee.fullname == null) { mainForm.Error("Employee cannot be blank or null! [0xAntEater]"); return false; }
            try
            {
                // We might have to skip this for now and insert manually....
                mainForm.employee.status = "active";
                mainForm.sql.Update_DB(mainForm.employee, mainForm.admin);
                mainForm.Progress("Employee DB instance updated");
            }
            catch (Exception x)
            {
                mainForm.Log("An error has occured relating to SQL Updating DB [0xPython]");
                MessageBox.Show(x.ToString());
                return false;
            }


            //probably best to reset...
            employee = new Person();
            mainForm.employee = new Person();
            return true;
            
        }

        private void button13_Click(object sender, EventArgs e) //HDNet Reno ADD +
        {
            if (MessageBox.Show("To add a H-DNet (Reno) account for " + employee.fullname + " using automation, you must save any changes."
                + System.Environment.NewLine + "Save and continue?", "Save Changes", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                QuickStart();
                mainForm.AddEmployee_HDNetExecution(1);
                QuickEnd();
            }
        }

        private void button45_Click(object sender, EventArgs e) //HDNet Yuba ADD +
        {
            if (MessageBox.Show("To add a H-DNet (Yuba) account for " + employee.fullname + " using automation, you must save any changes."
                + System.Environment.NewLine + "Save and continue?", "Save Changes", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                QuickStart();
                mainForm.AddEmployee_HDNetExecution(2);
                QuickEnd();
            }
        }

        private void button15_Click(object sender, EventArgs e) //HDNet Redwood ADD +
        {
            if (MessageBox.Show("To add a H-DNet (Redwood) account for " + employee.fullname + " using automation, you must save any changes."
                + System.Environment.NewLine + "Save and continue?", "Save Changes", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                QuickStart();
                mainForm.AddEmployee_HDNetExecution(3);
                QuickEnd();
            }
        }

        private void button37_Click(object sender, EventArgs e) // HDNet Death Valley ADD +
        {
            if (MessageBox.Show("To add a H-DNet (Death Valley) account for " + employee.fullname + " using automation, you must save any changes."
    + System.Environment.NewLine + "Save and continue?", "Save Changes", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                QuickStart();
                mainForm.AddEmployee_HDNetExecution(4);
                QuickEnd();
            }
        }

        private void button93_Click(object sender, EventArgs e) // HDNet Coronado Beach ADD +
        {
            // do nothing
        }

        private void button85_Click(object sender, EventArgs e) //HDNet Orange County ADD +
        {
            // do nothing
        }
    }

}
