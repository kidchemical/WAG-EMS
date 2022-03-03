using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumFirst
{
    public partial class Form1
    {
        public void ToolDTPasswordReset(List<Person> people)
        {
            
            Progress("::TOOL - DT Password Resetter - " + people.Count.ToString() + " accounts targeted...::");
            DTLogin();
            foreach (Person person in people)
            {
                //Status("Resetting person...");
                if (person != null)
                {
                    Status("Resetting account password: " + person.email.username);
                    if (DTCheckUserByEmail(person.email.username) == true)
                    {
                        DTResetPassword(person.email.username);
                    }
                    else
                    {
                        //Log(person.dealertrack.username + " not found");
                    }
                }
                else
                {
                    Log("Person is null!");
                }
            }
            Progress("::TOOL EXECUTION - DealerTrack Password Resetter - Complete!::");
        }


    }
}
