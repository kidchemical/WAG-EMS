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
    public partial class ToolsNNANetCrossEnroller : Form
    {

        Form1 mainForm;
        public ToolsNNANetCrossEnroller(Form1 main)
        {
            mainForm = main;
            InitializeComponent();
            PopulateInputField();
        }



        // GUI
        public void PopulateInputField()
        {
            mainForm.Status("Populating... [ToolsNNANetCrossEnroller.cs]");
            XmlDocument xmlDoc = new XmlDocument();

            // Role Position ComboBox
            xmlDoc.Load(Directory.GetCurrentDirectory() + @"\resources\roles.xml");
            foreach (XmlNode element in xmlDoc.SelectNodes("//allroles/role"))
            {
                string role = element.SelectSingleNode("./@title").InnerText;
                cb_tools_role.Items.Add(role);
            }

        }


        // EZ MAP FUNCTIONS
        private void ToolNNANetCrossEnroller()
        {
            if (tb_tools_username.Text == "" || cb_tools_role.Text == "")
            {
                mainForm.Log("Username and role field must be filled.");
                return;
            }
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Directory.GetCurrentDirectory() + @"\resources\portals.xml");
            mainForm.InitializeBrowser(mainForm._showBrowser);


                            //mainForm.SetEmployeeParameters();



            // override role from designated combo-box...
            mainForm.employee.role = cb_tools_role.Text;
            mainForm.SetAdminParameters();
                //mainForm.GenerateRoles(mainForm.employee.role, mainForm.employee.store);

            mainForm.Status("Enrolling in multiple stores...");

            if (tv_nna.Nodes[0].Nodes[0].Checked) { mainForm.NNAnetMultiStore(0, tb_tools_username.Text, true); }
            if (tv_nna.Nodes[0].Nodes[1].Checked) { mainForm.NNAnetMultiStore(1, tb_tools_username.Text, true); }
            if (tv_nna.Nodes[0].Nodes[2].Checked) { mainForm.NNAnetMultiStore(2, tb_tools_username.Text, true); }
            if (tv_nna.Nodes[0].Nodes[3].Checked) { mainForm.NNAnetMultiStore(3, tb_tools_username.Text, true); }
            if (tv_nna.Nodes[0].Nodes[4].Checked) { mainForm.NNAnetMultiStore(4, tb_tools_username.Text, true); }
            if (tv_nna.Nodes[0].Nodes[5].Checked) { mainForm.NNAnetMultiStore(5, tb_tools_username.Text, true); }
            if (tv_nna.Nodes[0].Nodes[6].Checked) { mainForm.NNAnetMultiStore(6, tb_tools_username.Text, true); }
            if (tv_nna.Nodes[0].Nodes[7].Checked) { mainForm.NNAnetMultiStore(7, tb_tools_username.Text, true); }
            mainForm.TerminateBrowser();
            mainForm.Progress("::DONE::");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            ToolNNANetCrossEnroller();
        }
    }
}
