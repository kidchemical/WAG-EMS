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
    public partial class DebugPanel : Form
    {
        Form1 mainForm; 

        public DebugPanel(Form1 main)
        {
            InitializeComponent();
            mainForm = main;
            
        }


        private void DebugPanel_Load(object sender, EventArgs e)
        {

            UpdateValues();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateValues();
        }

        private void UpdateValues()
        {
            label2.Text = mainForm.employee.ToString();
            label5.Text = mainForm.employee.firstname;
            label6.Text = mainForm.employee.lastname;
            label11.Text = mainForm.employee.fullname;

            label8.Text = mainForm.employee.store;
            label7.Text = mainForm.employee.role;
            label12.Text = mainForm._domain;
            label16.Text = mainForm._domainLogin;
        }
    }
}
