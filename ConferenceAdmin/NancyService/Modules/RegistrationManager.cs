using NancyService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NancyService.Modules
{
    public class RegistrationManager
    {
        public RegistrationManager()
        {

        }

        public bool addRegistration(registration reg, user user)
        {/*int type, string firstname, string lastname, string affiliationName, bool registrationstatus, bool hasapplied, bool acceptancestatus*/
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    user.membershipID = 1;
                    user.addressID = 1;
                    user.registrationStatus = "Accepted";
                    user.hasApplied = true;
                    user.acceptanceStatus = "Accepted";
                    user.phone = "";
                    user.userFax = "";
                    user.isEvaluator = "yes";
                    context.users.Add(user);
                    context.SaveChanges();

                    reg.userID = user.userID;
                    reg.paymentID = 1;
                    reg.byAdmin = true;
                    context.registrations.Add(reg);

                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.Write("AdminManager.addRegistration error " + ex);
                return false;
            }

        }

        public List<RegisteredUser> getRegistrationList()
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    var registrationList = new List<RegisteredUser>();
                    registrationList = context.registrations.Select(reg => new RegisteredUser
                    {
                        registrationID = reg.registrationID,
                        firstname = reg.user.firstName,
                        lastname = reg.user.lastName,
                        usertypeid = reg.user.usertype.userTypeName,
                        date1 = reg.date1,
                        date2 = reg.date2,
                        affiliationName = reg.user.affiliationName
                    }).ToList();

                    return registrationList;
                }
            }
            catch (Exception ex)
            {
                Console.Write("RegistrationManager.getRegistration error " + ex);
                return null;
            }
        }



        public bool deleteRegistration(int id)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    var registration = context.registrations.Where(reg => reg.registrationID == id).FirstOrDefault();
                    context.registrations.Remove(registration);
                    context.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.Write("AdminManager.deleteRegistration error " + ex);
                return false;
            }
        }

        public bool updateRegistration(int id, string firstname)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {

                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.Write("AdminManager.updateRegistration error " + ex);
                return false;
            }
        }


    }
}



public class RegisteredUser
{
    public long registrationID;
    public string firstname;
    public string lastname;
    public string usertypeid;
    public bool? date1;
    public bool? date2;
    public string affiliationName;


    public RegisteredUser() { }

}