using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeleniumFirst.forms
{
    public partial class Settings : Form
    {
        Form1 main;
        Xml_io xml;
        public Settings(Form1 form, Xml_io xml_io)
        {
            main = form;
            xml = xml_io;
            InitializeComponent();


            tb_settings_name.Text = main.admin.fullname;
            tb_dealertrack_username.Text = main.admin.dealertrack.username;
            tb_dealertrack_password.Text = main.admin.dealertrack.password;
            tb_reynolds_username.Text = main.admin.reynolds.username;
            tb_reynolds_password.Text = main.admin.reynolds.password;
            tb_talones_username.Text = main.admin.talones.username;
            tb_talones_password.Text = main.admin.talones.password;
            tb_nnanet_username.Text = main.admin.nnanet.username;
            tb_nnanet_password.Text = main.admin.nnanet.password;
            tb_hyundaidealer_username.Text = main.admin.hyundaidealer.username;
            tb_hyundaidealer_password.Text = main.admin.hyundaidealer.password;
            tb_mxconnect_username.Text = main.admin.mxconnect.username;
            tb_mxconnect_password.Text = main.admin.mxconnect.password;
            tb_hdnet_username.Text = main.admin.hdnet.username;
            tb_hdnet_password.Text = main.admin.hdnet.password;
            tb_hdnet1_username.Text = main.admin.hdnet1.username;
            tb_hdnet1_password.Text = main.admin.hdnet1.password;
            tb_hdnet2_username.Text = main.admin.hdnet2.username;
            tb_hdnet2_password.Text = main.admin.hdnet2.password;
            tb_hdnet3_username.Text = main.admin.hdnet3.username;
            tb_hdnet3_password.Text = main.admin.hdnet3.password;
            tb_hdnet4_username.Text = main.admin.hdnet4.username;
            tb_hdnet4_password.Text = main.admin.hdnet4.password;
            tb_hdnet5_username.Text = main.admin.hdnet5.username;
            tb_hdnet5_password.Text = main.admin.hdnet5.password;
            tb_kdealer_username.Text = main.admin.kdealer.username;
            tb_kdealer_password.Text = main.admin.kdealer.password;
            tb_gmglobal_username.Text = main.admin.gmglobal.username;
            tb_gmglobal_password.Text = main.admin.gmglobal.password;
            tb_dealerconnect_username.Text = main.admin.dealerconnect.username;
            tb_dealerconnect_password.Text = main.admin.dealerconnect.password;
            tb_vcc_username.Text = main.admin.vcc.username;
            tb_vcc_password.Text = main.admin.vcc.password;
            tb_cudl_username.Text = main.admin.cudl.username;
            tb_cudl_password.Text = main.admin.cudl.password;
            tb_office365_username.Text = main.admin.office365.username;
            tb_office365_password.Text = main.admin.office365.password;

            cb_showBrowser.Checked = main._showBrowser;
            cb_showConsole.Checked = main._showConsole;
            //cb_showLegacyPanel.Checked = main._showLegacyPanel;
            tb_maxTimeout.Text = main._timeout.ToString();

            tb_chromePath.Text = main.chromePath;
            tb_driverPath.Text = main.driverPath;
        }

        private void button1_Click(object sender, EventArgs e)          //  Save Button
        {
            main.admin.fullname = tb_settings_name.Text;
            main.admin.dealertrack.username = tb_dealertrack_username.Text;
            main.admin.dealertrack.password = tb_dealertrack_password.Text;
            main.admin.reynolds.username = tb_reynolds_username.Text;
            main.admin.reynolds.password = tb_reynolds_password.Text;
            main.admin.talones.username = tb_talones_username.Text;
            main.admin.talones.password = tb_talones_password.Text;
            main.admin.nnanet.username = tb_nnanet_username.Text;
            main.admin.nnanet.password = tb_nnanet_password.Text;
            main.admin.hyundaidealer.username = tb_hyundaidealer_username.Text;
            main.admin.hyundaidealer.password = tb_hyundaidealer_password.Text;
            main.admin.mxconnect.username = tb_mxconnect_username.Text;
            main.admin.mxconnect.password = tb_mxconnect_password.Text;
            main.admin.hdnet.username = tb_hdnet_username.Text;
            main.admin.hdnet.password = tb_hdnet_password.Text;
            main.admin.hdnet1.username = tb_hdnet1_username.Text;
            main.admin.hdnet1.password = tb_hdnet1_password.Text;
            main.admin.hdnet2.username = tb_hdnet2_username.Text;
            main.admin.hdnet2.password = tb_hdnet2_password.Text;
            main.admin.hdnet3.username = tb_hdnet3_username.Text;
            main.admin.hdnet3.password = tb_hdnet3_password.Text;
            main.admin.hdnet4.username = tb_hdnet4_username.Text;
            main.admin.hdnet4.password = tb_hdnet4_password.Text;
            main.admin.hdnet5.username = tb_hdnet5_username.Text;
            main.admin.hdnet5.password = tb_hdnet5_password.Text;
            main.admin.kdealer.username = tb_kdealer_username.Text;
            main.admin.kdealer.password = tb_kdealer_password.Text;
            main.admin.gmglobal.username = tb_gmglobal_username.Text;
            main.admin.gmglobal.password = tb_gmglobal_password.Text;
            main.admin.dealerconnect.username = tb_dealerconnect_username.Text;
            main.admin.dealerconnect.password = tb_dealerconnect_password.Text;
            main.admin.vcc.username = tb_vcc_username.Text;
            main.admin.vcc.password = tb_vcc_password.Text;
            main.admin.cudl.username = tb_cudl_username.Text;
            main.admin.cudl.password = tb_cudl_password.Text;
            main.admin.office365.username = tb_office365_username.Text;
            main.admin.office365.password = tb_office365_password.Text;

            main._showBrowser = cb_showBrowser.Checked;
            main.ShowConsole(cb_showConsole.Checked);
            //main.ShowLegacyPanel(cb_showLegacyPanel.Checked);
            main._timeout = Int32.Parse(tb_maxTimeout.Text);
            xml.SaveState();     // Save the current state to the state.XML file...

            this.Close();
        }

        private void Settings_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            main.WebMailDebugger(); 
        }
    }
}
