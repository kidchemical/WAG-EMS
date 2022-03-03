#region Examples - How to use
/* Example of how to use:
            string results;
            using (forms.MessageBox_dropdown mb_dropdown = new forms.MessageBox_dropdown())
            {
                mb_dropdown.AddItem("AAAA");
                mb_dropdown.AddItem("BBBB");
                if (mb_dropdown.ShowDialog() == DialogResult.OK)
                {
                    results = mb_dropdown.Text;
                    MessageBox.Show(results);
                }
            }
*/



/* An even better example:
//User Settings
            
            IReadOnlyCollection<IWebElement> roles = _driver.FindElements(By.XPath("//select[@id='Title']/option"));
            List<string> strRoles = new List<string>();
            foreach (IWebElement element in roles)
            {
                strRoles.Add(element.Text);
            }

            // DROP DOWN INPUT DIALOG - Job Titles
            forms.MessageBox_dropdown inputBox = new forms.MessageBox_dropdown("Select the role you wish to assign on CUDL", strRoles);
            inputBox.ShowDialog();

            new SelectElement(_driver.FindElement(By.XPath("//select[@id='Title']"))).SelectByValue(inputBox.result);
*/
#endregion

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
    public partial class MessageBox_dropdown : Form
    {
        public string result;


        public MessageBox_dropdown(string str, List<string> options)
        {
            InitializeComponent();
            lbl_message.Text = str;

            foreach( string option in options)
            {
                AddItem(option);
            }
        }




        public void AddItem(string str)
        {
            cb_dropdown.Items.Add(str);
        }




        private void MessageBox_dropdown_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            result = cb_dropdown.SelectedItem.ToString();
            this.Close();
        }
    }
}
