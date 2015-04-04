using NancyService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

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
                    user.title = "";
                    user.phone = "";
                    user.userFax = "";
                    //user.isEvaluator = false;
                    context.users.Add(user);
                    context.SaveChanges();

                    reg.userID = user.userID;
                    reg.paymentID = 1;
                    reg.byAdmin = true;
                    reg.deleted = false;
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
                    registrationList = context.registrations.Where(reg => reg.deleted == false).Select(reg => new RegisteredUser
                    {
                        registrationID = reg.registrationID,
                        firstname = reg.user.firstName,
                        lastname = reg.user.lastName,
                        usertypeid = reg.user.usertype.userTypeName,
                        date1 = reg.date1,
                        date2 = reg.date2,
                        date3 = reg.date3,
                        affiliationName = reg.user.affiliationName,
                        byAdmin = reg.byAdmin,
                        usertype = new UserTypeName { userTypeID = reg.user.usertype.userTypeID, userTypeName = reg.user.usertype.userTypeName }
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

        public List<UserTypeName> getUserTypesList()
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    var userTypesList = new List<UserTypeName>();
                    userTypesList = context.usertypes.Select(u => new UserTypeName
                    {
                        userTypeID = u.userTypeID,
                        userTypeName = u.userTypeName,
                        description = u.description,
                        registrationCost = u.registrationCost,
                        registrationLateFee = u.registrationLateFee
                    }).ToList();

                    return userTypesList;
                }
            }
            catch (Exception ex)
            {
                Console.Write("RegistrationManager.getUserTypes error " + ex);
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
                    registration.deleted = true;
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

        public bool updateRegistration(RegisteredUser registeredUser)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    var registration = context.registrations.Where(reg => reg.registrationID == registeredUser.registrationID).FirstOrDefault();
                    var temp = registration.userID;
                    var user = context.users.Where(u => u.userID == registration.userID).FirstOrDefault();
                    user.firstName = registeredUser.firstname;
                    user.lastName = registeredUser.lastname;
                    user.userTypeID = Convert.ToInt32(registeredUser.usertypeid);
                    user.affiliationName = registeredUser.affiliationName;
                    registration.user = user;
                    registration.date1 = registeredUser.date1;
                    registration.date2 = registeredUser.date2;
                    registration.date3 = registeredUser.date3;
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.Write("AdminManager.updateRegistration error " + ex);
                return false;
            }
        }

        public List<string> getDates()
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    List<string> dates = new List<string>();
                    var date1 = context.interfaceinformations.Where(i => i.attribute == "conferenceDay1").FirstOrDefault().content;
                    var date2 = context.interfaceinformations.Where(i => i.attribute == "conferenceDay2").FirstOrDefault().content;
                    var date3 = context.interfaceinformations.Where(i => i.attribute == "conferenceDay3").FirstOrDefault().content;

                    if (date1 != null && date1 != "") {                    
                        DateTime confDate1 = new DateTime( // Constructor (Year, Month, Day)
                            Convert.ToInt32(date1.Split('/')[2]),
                            Convert.ToInt32(date1.Split('/')[0]),
                            Convert.ToInt32(date1.Split('/')[1]));
                        dates.Add(confDate1.DayOfWeek + ", " + confDate1.ToString("MMMM", CultureInfo.InvariantCulture) + " " + confDate1.Day + ", " + confDate1.Year);
                    }

                    if (date2 != null && date2 != "")
                    {
                        DateTime confDate2 = new DateTime( // Constructor (Year, Month, Day)
                            Convert.ToInt32(date2.Split('/')[2]),
                            Convert.ToInt32(date2.Split('/')[0]),
                            Convert.ToInt32(date2.Split('/')[1]));
                        dates.Add(confDate2.DayOfWeek + ", " + confDate2.ToString("MMMM", CultureInfo.InvariantCulture) + " " + confDate2.Day + ", " + confDate2.Year);
                    }

                    if (date3 != null && date3 != "")
                    {
                        DateTime confDate3 = new DateTime( // Constructor (Year, Month, Day)
                            Convert.ToInt32(date3.Split('/')[2]),
                            Convert.ToInt32(date3.Split('/')[0]),
                            Convert.ToInt32(date3.Split('/')[1]));
                        dates.Add(confDate3.DayOfWeek + ", " + confDate3.ToString("MMMM", CultureInfo.InvariantCulture) + " " + confDate3.Day + ", " + confDate3.Year);
                    }                        

                    return dates;
                }
            }
            catch (Exception ex)
            {
                Console.Write("RegistrationManager.getDates error " + ex);
                return null;
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
    public bool? date3;
    public string affiliationName;
    public bool? byAdmin;
    public UserTypeName usertype;
    public string notes;
    public RegisteredUser() { }

}

public class UserTypeName
{
    public int userTypeID;
    public string userTypeName;
    public string description;
    public double? registrationCost;
    public double? registrationLateFee;
}