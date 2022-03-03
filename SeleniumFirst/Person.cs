using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SeleniumFirst
{
    public class Person
    {

        public string firstname, lastname, fullname;
        public string role;
        public string store;
        public string ssn;
        public string employeenumber;
        public string status; // active or inactive
        public string notes = "";
        public string department;
        public string tableID;  // SQL Table ID

        // IT Dept.
         ///public List<Credentials> credentials = new List<Credentials>();
        public Credentials email = new Credentials();
        public Credentials dealertrack = new Credentials();
        public Credentials reynolds = new Credentials();
        public Credentials talones = new Credentials();
        public Credentials nnanet = new Credentials();
        public Credentials dealerconnect = new Credentials();
        public Credentials gmglobal = new Credentials();
        public Credentials hyundaidealer = new Credentials();
        public Credentials kdealer = new Credentials();
        public Credentials hdnet = new Credentials();   // Reno
        public Credentials hdnet1 = new Credentials();  // Yuba
        public Credentials hdnet2 = new Credentials();  // Redwood
        public Credentials hdnet3 = new Credentials();  // Death Valley
        public Credentials hdnet4 = new Credentials();  // Coronado Beach
        public Credentials hdnet5 = new Credentials();  // Orange County
        public Credentials vcc = new Credentials();
        public Credentials mxconnect = new Credentials();
        public Credentials cudl = new Credentials();
        public Credentials office365 = new Credentials();

        // Chris's Dept.
        public Credentials fiexpress = new Credentials();
        public Credentials dmvdesk = new Credentials();
        public Credentials vinsolutions = new Credentials();
        public Credentials carwars = new Credentials();

        // Used Car Dept.
        public Credentials rapidrecon = new Credentials();
        public Credentials vauto = new Credentials();


        public string addBy = "", removeBy = "", modifyBy = "";
        public string AddDate = "";
        public string RemoveDate = "";
        public string ModifyDate = "";
        #region Extremely Sloppy date parser to correct sql null db values -> excel
        public string addDate
        {
            get
            {
                int year = 0;
                if (AddDate.Contains(' '))
                {
                    try
                    {
                        year = DateTime.Parse(AddDate.Remove(AddDate.IndexOf(' '))).Year;
                    }
                    catch { }
                }
                else
                {
                    //MessageBox.Show(AddDate);
                    try
                    {
                        year = DateTime.Parse(AddDate).Year;
                    }
                    catch { }
                }

                //MessageBox.Show(year.ToString());

                if (year > 1990)
                {
                    return AddDate;
                }
                else
                {
                    return "";
                }
            }
            set
            {
                this.AddDate = value;
            }
        }

        public string removeDate

        {
            get
            {
                int year = 0;
                if (RemoveDate.Contains(' '))
                {
                    try
                    {
                        year = DateTime.Parse(RemoveDate.Remove(RemoveDate.IndexOf(' '))).Year;
                    }
                    catch { }
                }
                else
                {
                    try
                    {
                        year = DateTime.Parse(RemoveDate).Year;
                    }
                    catch { }
                }


                if (year > 1990)
                {
                    return this.RemoveDate;
                }
                else
                {
                    return "";
                }
            }
            set
            {
                this.RemoveDate = value;
            }
        }

        public string modifyDate

        {
            get
            {
                int year = 0;
                if (ModifyDate.Contains(' '))
                {
                    try
                    {
                        year = DateTime.Parse(ModifyDate.Remove(ModifyDate.IndexOf(' '))).Year;
                    }
                    catch { }
                }
                else
                {
                    try
                    {
                        year = DateTime.Parse(ModifyDate).Year;
                    }
                    catch { }
                }


                if (year > 1990)
                {
                    return this.ModifyDate;
                }
                else
                {
                    return "";
                }
            }
            set
            {
                this.ModifyDate = value;
            }
        }

        #endregion





        public class Credentials
        {
            public string username = "";
                public string password = "";

            public string addBy = "", removeBy = "";
            public string AddDate = "", RemoveDate = "";

            #region Extremely Sloppy date parser to correct sql null db values -> excel
            public string addDate
            {
                get
                {
                    if (username == "")
                    {
                        //return "";
                    }


                    int year = 0;
                    if (AddDate.Contains(' '))
                    {
                        try
                        {
                            year = DateTime.Parse(AddDate.Remove(AddDate.IndexOf(' '))).Year;
                        }
                        catch { }
                    }
                    else
                    {
                        //MessageBox.Show(AddDate);
                        try
                        {
                            year = DateTime.Parse(AddDate).Year;
                        }
                        catch { }
                    }

                    //MessageBox.Show(year.ToString());

                    if (year > 1990)
                    {
                        return AddDate;
                    }
                    else
                    {
                        return "";
                    }
                }
                set
                {
                    this.AddDate = value;
                }
            }

            public string removeDate

            {
                get
                {
                    if (username == "")
                    {
                        //return "";
                    }

                    int year = 0;
                    if (RemoveDate.Contains(' '))
                    {
                        try
                        {
                            year = DateTime.Parse(RemoveDate.Remove(RemoveDate.IndexOf(' '))).Year;
                        }
                        catch { }
                    }
                    else
                    {
                        try
                        {
                            year = DateTime.Parse(RemoveDate).Year;
                        }
                        catch { } 
                    }


                    if (year > 1990)
                    {
                        return DateTime.Parse(this.RemoveDate.ToString()).ToString("MM/dd/yyyy");
                    }
                    else
                    {
                        return "";
                    }
                }
                set
                {
                    this.RemoveDate = value;
                }
            }

            #endregion


            

            public string roleID, storeID;  // XML value for portal / role association
            public string managerID, departmentID;  // Used only for HDNet

            public void SignForAdd(Person admin)
            {
                addDate = DateTime.Now.ToShortDateString();
                addBy = admin.fullname;
            }

            public void SignForRemove(Person admin)
            {
                removeDate = DateTime.Now.ToShortDateString();
                removeBy = admin.fullname;
            }

        }

        public string GuessLastName()
        {

            string lastname = fullname.Remove(0, fullname.IndexOf(" "));
            while (lastname.Contains(" "))
            {
                lastname = lastname.Remove(0, lastname.IndexOf(" ") + 1);
            }
            
            return lastname;
        }

        public bool GetFirstLastName()
        {
            string input = fullname;

            if (InputBoxClass.InputBox("First Name", "Enter employee's first name", ref input) == System.Windows.Forms.DialogResult.OK)
            {
                firstname = input;
                if (InputBoxClass.InputBox("Last Name", "Enter employee's last name", ref input) == System.Windows.Forms.DialogResult.OK)
                {
                    lastname = input;
                    return true;
                }
            }

            return false;

        }

        public void InitializeCredentials()
        {
            //credentials.Add(email);
        }

    }
}
