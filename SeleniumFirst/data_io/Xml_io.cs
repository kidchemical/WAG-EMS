using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Forms;

namespace SeleniumFirst
{
    public class Xml_io
    {
        // Settings.XML
        public string settingsPath = Directory.GetCurrentDirectory() + @"\resources\state.xml";
        public string settingsPath_default = Directory.GetCurrentDirectory() + @"\resources\state_default.xml";



        Form1 main;
        public Xml_io(Form1 form)
        {
            main = form;
        }

        public void SaveState()
        {
            XmlDocument xmlDoc = new XmlDocument();
            if (File.Exists(settingsPath))
            {
                xmlDoc.Load(settingsPath);
            }
            else
            {
                xmlDoc.Load(settingsPath_default);
            }

            xmlDoc.SelectSingleNode("//state/settings/showBrowser/@value").InnerText = XmlConvert.ToString(main._showBrowser);
            xmlDoc.SelectSingleNode("//state/settings/showConsole/@value").InnerText = XmlConvert.ToString(main._showConsole);
            xmlDoc.SelectSingleNode("//state/settings/timeout/@value").InnerText = XmlConvert.ToString(main._timeout);

            xmlDoc.SelectSingleNode("//state/settings/admin/name/@value").InnerText = main.admin.fullname;
            xmlDoc.SelectSingleNode("//state/settings/admin/dealertrack/@username").InnerText = main.admin.dealertrack.username;
            xmlDoc.SelectSingleNode("//state/settings/admin/dealertrack/@password").InnerText = main.admin.dealertrack.password;
            xmlDoc.SelectSingleNode("//state/settings/admin/reynolds/@username").InnerText = main.admin.reynolds.username;
            xmlDoc.SelectSingleNode("//state/settings/admin/reynolds/@password").InnerText = main.admin.reynolds.password;
            xmlDoc.SelectSingleNode("//state/settings/admin/talones/@username").InnerText = main.admin.talones.username;
            xmlDoc.SelectSingleNode("//state/settings/admin/talones/@password").InnerText = main.admin.talones.password;
            xmlDoc.SelectSingleNode("//state/settings/admin/nnanet/@username").InnerText = main.admin.nnanet.username;
            xmlDoc.SelectSingleNode("//state/settings/admin/nnanet/@password").InnerText = main.admin.nnanet.password;
            xmlDoc.SelectSingleNode("//state/settings/admin/hyundaidealer/@username").InnerText = main.admin.hyundaidealer.username;
            xmlDoc.SelectSingleNode("//state/settings/admin/hyundaidealer/@password").InnerText = main.admin.hyundaidealer.password;
            xmlDoc.SelectSingleNode("//state/settings/admin/mxconnect/@username").InnerText = main.admin.mxconnect.username;
            xmlDoc.SelectSingleNode("//state/settings/admin/mxconnect/@password").InnerText = main.admin.mxconnect.password;
            xmlDoc.SelectSingleNode("//state/settings/admin/kdealer/@username").InnerText = main.admin.kdealer.username;
            xmlDoc.SelectSingleNode("//state/settings/admin/kdealer/@password").InnerText = main.admin.kdealer.password;
            xmlDoc.SelectSingleNode("//state/settings/admin/gmglobal/@username").InnerText = main.admin.gmglobal.username;
            xmlDoc.SelectSingleNode("//state/settings/admin/gmglobal/@password").InnerText = main.admin.gmglobal.password;
            xmlDoc.SelectSingleNode("//state/settings/admin/dealerconnect/@username").InnerText = main.admin.dealerconnect.username;
            xmlDoc.SelectSingleNode("//state/settings/admin/dealerconnect/@password").InnerText = main.admin.dealerconnect.password;
            xmlDoc.SelectSingleNode("//state/settings/admin/hdnet/@username").InnerText = main.admin.hdnet.username;
            xmlDoc.SelectSingleNode("//state/settings/admin/hdnet/@password").InnerText = main.admin.hdnet.password;
            xmlDoc.SelectSingleNode("//state/settings/admin/hdnet1/@username").InnerText = main.admin.hdnet1.username;
            xmlDoc.SelectSingleNode("//state/settings/admin/hdnet1/@password").InnerText = main.admin.hdnet1.password;
            xmlDoc.SelectSingleNode("//state/settings/admin/hdnet2/@username").InnerText = main.admin.hdnet2.username;
            xmlDoc.SelectSingleNode("//state/settings/admin/hdnet2/@password").InnerText = main.admin.hdnet2.password;
            xmlDoc.SelectSingleNode("//state/settings/admin/hdnet3/@username").InnerText = main.admin.hdnet3.username;
            xmlDoc.SelectSingleNode("//state/settings/admin/hdnet3/@password").InnerText = main.admin.hdnet3.password;
            xmlDoc.SelectSingleNode("//state/settings/admin/hdnet4/@username").InnerText = main.admin.hdnet4.username;
            xmlDoc.SelectSingleNode("//state/settings/admin/hdnet4/@password").InnerText = main.admin.hdnet4.password;
            xmlDoc.SelectSingleNode("//state/settings/admin/hdnet5/@username").InnerText = main.admin.hdnet5.username;
            xmlDoc.SelectSingleNode("//state/settings/admin/hdnet5/@password").InnerText = main.admin.hdnet5.password;
            xmlDoc.SelectSingleNode("//state/settings/admin/vcc/@username").InnerText = main.admin.vcc.username;
            xmlDoc.SelectSingleNode("//state/settings/admin/vcc/@password").InnerText = main.admin.vcc.password;
            xmlDoc.SelectSingleNode("//state/settings/admin/cudl/@username").InnerText = main.admin.cudl.username;
            xmlDoc.SelectSingleNode("//state/settings/admin/cudl/@password").InnerText = main.admin.cudl.password;
            xmlDoc.SelectSingleNode("//state/settings/admin/office365/@username").InnerText = main.admin.office365.username;
            xmlDoc.SelectSingleNode("//state/settings/admin/office365/@password").InnerText = main.admin.office365.password;

            xmlDoc.Save(settingsPath);
            System.Windows.Forms.MessageBox.Show("Settings have been saved!");
        }

        public List<ListViewItem> LoadState()
        {
            XmlDocument xmlDoc = new XmlDocument();

            if (File.Exists(settingsPath))
            {
                xmlDoc.Load(settingsPath);
            }
            else
            {
                MessageBox.Show("Your account settings must be configured in order to use this software. Please fill out the account credential fields in the settings dialog.", "Setup Notice", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                xmlDoc.Load(settingsPath_default);

            }

            // Settings
            main._showBrowser = XmlConvert.ToBoolean(xmlDoc.SelectSingleNode("//state/settings/showBrowser/@value").InnerText);
            main._showConsole = XmlConvert.ToBoolean(xmlDoc.SelectSingleNode("//state/settings/showConsole/@value").InnerText);
            main._timeout = XmlConvert.ToInt16(xmlDoc.SelectSingleNode("//state/settings/timeout/@value").InnerText);
            main.admin.fullname = xmlDoc.SelectSingleNode("//state/settings/admin/name/@value").InnerText;
            main.admin.dealertrack.username = xmlDoc.SelectSingleNode("//state/settings/admin/dealertrack/@username").InnerText;
            main.admin.dealertrack.password = xmlDoc.SelectSingleNode("//state/settings/admin/dealertrack/@password").InnerText;
            main.admin.reynolds.username = xmlDoc.SelectSingleNode("//state/settings/admin/reynolds/@username").InnerText;
            main.admin.reynolds.password = xmlDoc.SelectSingleNode("//state/settings/admin/reynolds/@password").InnerText;
            main.admin.talones.username = xmlDoc.SelectSingleNode("//state/settings/admin/talones/@username").InnerText;
            main.admin.talones.password = xmlDoc.SelectSingleNode("//state/settings/admin/talones/@password").InnerText;
            main.admin.nnanet.username = xmlDoc.SelectSingleNode("//state/settings/admin/nnanet/@username").InnerText;
            main.admin.nnanet.password = xmlDoc.SelectSingleNode("//state/settings/admin/nnanet/@password").InnerText;
            main.admin.hyundaidealer.username = xmlDoc.SelectSingleNode("//state/settings/admin/hyundaidealer/@username").InnerText;
            main.admin.hyundaidealer.password = xmlDoc.SelectSingleNode("//state/settings/admin/hyundaidealer/@password").InnerText;
            main.admin.mxconnect.username = xmlDoc.SelectSingleNode("//state/settings/admin/mxconnect/@username").InnerText;
            main.admin.mxconnect.password = xmlDoc.SelectSingleNode("//state/settings/admin/mxconnect/@password").InnerText;
            main.admin.kdealer.username = xmlDoc.SelectSingleNode("//state/settings/admin/kdealer/@username").InnerText;
            main.admin.kdealer.password = xmlDoc.SelectSingleNode("//state/settings/admin/kdealer/@password").InnerText;
            main.admin.gmglobal.username = xmlDoc.SelectSingleNode("//state/settings/admin/gmglobal/@username").InnerText;
            main.admin.gmglobal.password = xmlDoc.SelectSingleNode("//state/settings/admin/gmglobal/@password").InnerText;
            main.admin.dealerconnect.username = xmlDoc.SelectSingleNode("//state/settings/admin/dealerconnect/@username").InnerText;
            main.admin.dealerconnect.password = xmlDoc.SelectSingleNode("//state/settings/admin/dealerconnect/@password").InnerText;
            main.admin.hdnet.username = xmlDoc.SelectSingleNode("//state/settings/admin/hdnet/@username").InnerText;
            main.admin.hdnet.password = xmlDoc.SelectSingleNode("//state/settings/admin/hdnet/@password").InnerText;
            main.admin.hdnet1.username = xmlDoc.SelectSingleNode("//state/settings/admin/hdnet1/@username").InnerText;
            main.admin.hdnet1.password = xmlDoc.SelectSingleNode("//state/settings/admin/hdnet1/@password").InnerText;
            main.admin.hdnet2.username = xmlDoc.SelectSingleNode("//state/settings/admin/hdnet2/@username").InnerText;
            main.admin.hdnet2.password = xmlDoc.SelectSingleNode("//state/settings/admin/hdnet2/@password").InnerText;
            main.admin.hdnet3.username = xmlDoc.SelectSingleNode("//state/settings/admin/hdnet3/@username").InnerText;
            main.admin.hdnet3.password = xmlDoc.SelectSingleNode("//state/settings/admin/hdnet3/@password").InnerText;

            #region Node Handler for HDNet4
            XmlElement element = (XmlElement)xmlDoc.SelectSingleNode("//state/settings/admin");
            if (element.SelectSingleNode("hdnet4") != null)
            {
                main.admin.hdnet4.username = xmlDoc.SelectSingleNode("//state/settings/admin/hdnet4/@username").InnerText;
                main.admin.hdnet4.password = xmlDoc.SelectSingleNode("//state/settings/admin/hdnet4/@password").InnerText;
            }
            else
            {
                XmlElement node = xmlDoc.CreateElement("hdnet4");

                node.SetAttribute("username", "");
                node.SetAttribute("password", "");

                xmlDoc.SelectSingleNode("//state/settings/admin").AppendChild(node);
                //xmlDoc.DocumentElement.AppendChild(node);
                xmlDoc.Save(settingsPath);
                main.Log("settings XML has been updated!");
            }
            #endregion

            #region Node Handler for HDNet5
            element = (XmlElement)xmlDoc.SelectSingleNode("//state/settings/admin");
            if (element.SelectSingleNode("hdnet5") != null)
            {
                main.admin.hdnet5.username = xmlDoc.SelectSingleNode("//state/settings/admin/hdnet5/@username").InnerText;
                main.admin.hdnet5.password = xmlDoc.SelectSingleNode("//state/settings/admin/hdnet5/@password").InnerText;
            }
            else
            {
                XmlElement node = xmlDoc.CreateElement("hdnet5");

                node.SetAttribute("username", "");
                node.SetAttribute("password", "");

                xmlDoc.SelectSingleNode("//state/settings/admin").AppendChild(node);
                //xmlDoc.DocumentElement.AppendChild(node);
                xmlDoc.Save(settingsPath);
                main.Log("settings XML has been updated!");
            }
            #endregion




            main.admin.vcc.username = xmlDoc.SelectSingleNode("//state/settings/admin/vcc/@username").InnerText;
            main.admin.vcc.password = xmlDoc.SelectSingleNode("//state/settings/admin/vcc/@password").InnerText;
            main.admin.cudl.username = xmlDoc.SelectSingleNode("//state/settings/admin/cudl/@username").InnerText;
            main.admin.cudl.password = xmlDoc.SelectSingleNode("//state/settings/admin/cudl/@password").InnerText;
            main.admin.office365.username = xmlDoc.SelectSingleNode("//state/settings/admin/office365/@username").InnerText;
            main.admin.office365.password = xmlDoc.SelectSingleNode("//state/settings/admin/office365/@password").InnerText;
            



            // Field
            /* Input field loading here */

            // Queue
            List<ListViewItem> personList = new List<ListViewItem>();
            foreach (XmlNode node_person in xmlDoc.SelectNodes("//state/queue/person"))
            {
                ListViewItem person = new ListViewItem();
                //person.UseItemStyleForSubItems = false;
                person.ForeColor = System.Drawing.Color.Gray;
                person.Text = node_person.Attributes.GetNamedItem("status").InnerText;
                person.SubItems.Add(node_person.Attributes.GetNamedItem("name").InnerText);
                person.SubItems.Add(node_person.Attributes.GetNamedItem("email").InnerText);
                person.SubItems.Add(node_person.Attributes.GetNamedItem("type").InnerText);
                person.SubItems.Add(node_person.Attributes.GetNamedItem("date").InnerText);
                personList.Add(person);
                switch (person.Text)
                {
                    case "In Progress": person.ImageIndex = 1; break;
                    case "Done": person.ImageIndex = 2; break;
                    case "Failed": person.ImageIndex = 4; break;
                    case "DEBUG": person.ImageIndex = 5; break;
                    default: person.ImageIndex = 0; break;
                }
                //MessageBox.Show(node_person.Attributes.GetNamedItem("name").InnerText);
            }

            return personList;
        }

        public ListViewItem AddToQueue(string status, string fullname, string email, string type, int imageIndex, string tableID)
        {
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load(settingsPath);
            }

            catch(FileNotFoundException)
            {
                main.Error("Your settings must be configured to continue!");
                return null;
            }


            ListViewItem person = new ListViewItem();
            person.UseItemStyleForSubItems = false;
            person.Text = status;
            person.ImageIndex = imageIndex;
            person.SubItems.Add(fullname);
            person.SubItems.Add(email);
            person.SubItems.Add(type).ForeColor = System.Drawing.Color.Green;
            person.SubItems.Add(DateTime.Now.ToString());
            person.SubItems.Add(tableID);

            //XmlElement element = xmlDoc.("//state/queue/person[@name='" + fullname + "']");
            XmlNode node = xmlDoc.SelectSingleNode("//state/queue/person[@name='" + fullname + "']");

            if (node == null)  // Create instance in XML queue...
            {
                XmlElement a = xmlDoc.CreateElement("person");
                a.SetAttribute("name", fullname);
                a.SetAttribute("email", email);
                a.SetAttribute("date", DateTime.Now.ToString());
                a.SetAttribute("type", type);
                a.SetAttribute("status", status);
                xmlDoc.SelectSingleNode("//state/queue").AppendChild(a);
            }
            else // Instance must already exist, update it...
            {
                node.Attributes[4].Value = status;
            }
            

            xmlDoc.Save(settingsPath);
            return person;
        }

        public void UpdateQueue(ListViewItem person)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(settingsPath);
            XmlNode node = xmlDoc.SelectSingleNode("//state/queue/person[@name='" + person.SubItems[1].Text + "']");

            if (node == null)  // Create instance in XML queue...
            {
                XmlElement a = xmlDoc.CreateElement("person");
                a.SetAttribute("name", person.SubItems[1].Text);
                a.SetAttribute("email", person.SubItems[2].Text);
                a.SetAttribute("date", person.SubItems[3].Text);
                a.SetAttribute("type", person.SubItems[4].Text);
                a.SetAttribute("status", person.Text);
                xmlDoc.SelectSingleNode("//state/queue").AppendChild(a);
            }
            else // Instance must already exist, update it...
            {
                node.Attributes[4].Value = person.Text;
            }
            xmlDoc.Save(settingsPath);
        }

        public void ClearQueue()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(settingsPath);
            xmlDoc.SelectSingleNode("//state/queue").RemoveAll();
            //XmlNodeList nodes = xmlDoc.SelectNodes("//state/queue");


            xmlDoc.Save(settingsPath);

        }

    }
}
