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

    public partial class UserPropertiesRemove : Form
    {
        Form1 mainForm;
        Person employee;
        public List<bool> termHD = new List<bool>();   // boolean flags for removing Harley Davidson accounts

        public UserPropertiesRemove(Form1 mainForm)
        {
            this.mainForm = mainForm;
            this.employee = mainForm.employee;
            InitializeComponent();

        }



        private void UserProperties_Load(object sender, EventArgs e)
        {
            ClearInputField();
            //SearchPerson(employee.fullname);
            PopulateFields(mainForm.employee);
        }



        // FORM INPUT MANAGEMENT
        private void PopulateFields(Person person)
        {
            this.employee = person;
            //tb_name.Text = employee.fullname;

              //Email
            tb_TermEmail.Text = employee.email.username;
           
            //figure domain
            if (employee.email.username.Contains("@wiseautogroup.com")) { cb_TermWiseEmail.Checked = true; cb_TermStoreEmail.Checked = false; }
            else
            {
                if (employee.email.username.Contains("@")) { cb_TermStoreEmail.Checked = true; cb_TermWiseEmail.Checked = false; }
            }

            //figure portals
            if (employee.dealertrack.username != "") { cb_TermDealerTrack.Checked = true; }
            if (employee.reynolds.username != "") { cb_TermReynolds.Checked = true; }
            if (employee.nnanet.username != "") { tv_TermPortals.Nodes[0].Checked = true; }
            if (employee.dealerconnect.username != "") { tv_TermPortals.Nodes[1].Checked = true; }
            if (employee.gmglobal.username != "") { tv_TermPortals.Nodes[2].Checked = true; }
            if (employee.hyundaidealer.username != "") { tv_TermPortals.Nodes[3].Checked = true; }
            if (employee.kdealer.username != "") { tv_TermPortals.Nodes[4].Checked = true; }

            if (employee.hdnet.username != "") { tv_TermPortals.Nodes[5].Checked = true; }
            if (employee.hdnet1.username != "") { tv_TermPortals.Nodes[5].Checked = true; }
            if (employee.hdnet2.username != "") { tv_TermPortals.Nodes[5].Checked = true; }
            if (employee.hdnet3.username != "") { tv_TermPortals.Nodes[5].Checked = true; }

            if (employee.vcc.username != "") { tv_TermPortals.Nodes[6].Checked = true; }
            if (employee.mxconnect.username != "") { tv_TermPortals.Nodes[7].Checked = true; }
            if (employee.cudl.username != "") { tv_TermPortals.Nodes[8].Checked = true; }
            if (employee.office365.username != "") { tv_TermPortals.Nodes[9].Checked = true; }
        }

        private void ClearInputField()
        {

            // treeview Portals reset
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





        #region Delete these methods ****
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
        }

        #endregion

        private void btn_Terminate_Click(object sender, EventArgs e)
        {
            // Augment the form fields into temporary public memory...
            if (tv_TermPortals.Nodes[5] == null) return;

            termHD.Add(tv_TermPortals.Nodes[5].Nodes[0].Checked);
            termHD.Add(tv_TermPortals.Nodes[5].Nodes[1].Checked);
            termHD.Add(tv_TermPortals.Nodes[5].Nodes[2].Checked);
            termHD.Add(tv_TermPortals.Nodes[5].Nodes[3].Checked);

            this.Close();
        }
    }

}
