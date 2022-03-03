using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;

namespace SeleniumFirst.forms
{
    public partial class DTPassResetter : Form
    {


        Form1 mainForm;
        public DTPassResetter(Form1 main)
        {
            mainForm = main;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            mainForm.InitializeBrowser(true);



            List<Person> people = new List<Person>();

            //split string by line breaks
            string raw = tb_userList.Text;
            string[] lines = raw.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            //MessageBox.Show(raw);

            foreach (string line in lines)
            {
                Person employee = new Person();
                employee.email.username = line;
                people.Add(employee);
            }


            mainForm.ToolDTPasswordReset(people);
            mainForm.TerminateBrowser();
        }
    }
}
