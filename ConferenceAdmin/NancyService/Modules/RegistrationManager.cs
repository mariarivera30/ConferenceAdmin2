
﻿using NancyService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Net;
using System.Net.Mail;

namespace NancyService.Modules
{
    public class RegistrationManager
    {
        /* [Randy] Empty Constructor */
        public RegistrationManager(){}

        /* [Randy] Add new registration entry */
        public string addRegistration(registration reg, user user, membership mem)
        {/*int type, string firstname, string lastname, string affiliationName, bool registrationstatus, bool hasapplied, bool acceptancestatus*/
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {                    
                    address address = new address();
                    context.addresses.Add(address); 
                   
                    //encryption
                    var userPassword = mem.password;
                    
                    mem.password = Security.GetSHA1HashData(mem.password);
                    
                    //end encryption                  
                    mem.emailConfirmation = true;
                    mem.deleted = false;
                    context.memberships.Add(mem);

                    context.SaveChanges();

                    user.addressID = address.addressID;
                    user.membershipID = mem.membershipID;
                    user.registrationStatus = "Accepted";
                    user.hasApplied = true;
                    user.acceptanceStatus = "Accepted";
                    user.title = "";
                    user.phone = "";
                    user.userFax = "";
                    user.deleted = false;
                    context.users.Add(user);
                    context.SaveChanges();

                    reg.userID = user.userID;
                    reg.paymentID = 1;
                    reg.byAdmin = true;
                    reg.deleted = false;
                    context.registrations.Add(reg);

                    context.SaveChanges();

                    try { sendEmailConfirmation(mem.email, userPassword); }

                    catch (Exception ex)
                    {
                        Console.Write("AdminManager.ConfirmationEmail error " + ex);
                        return null;
                    }

                    return reg.registrationID + "," + user.userTypeID;
                }
            }
            catch (Exception ex)
            {
                Console.Write("AdminManager.addRegistration error " + ex);
                return null;
            }

        }

        /* [Randy] Send an email to the user */
        private void sendEmailConfirmation(string email, string pass)
        {
            string ccwicEmail = "ccwictest@gmail.com";
            string ccwicEmailPass = "ccwic123456789";
            MailAddress ccwic = new MailAddress(ccwicEmail);
            MailAddress user = new MailAddress(email);
            MailMessage mail = new System.Net.Mail.MailMessage(ccwic, user);


            mail.Subject = "Caribbean Celebration of Women in Computing Account Confirmation!";
            mail.Body = "Your email has been used to create an account for the CCWiC Conference. You can login using your temporary password: " + pass + " . \n You can change your password by visiting this link http://136.145.116.238/#/ChangePassword.";
            

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;

            smtp.Credentials = new NetworkCredential(
                ccwicEmail, ccwicEmailPass);
            smtp.EnableSsl = true;

            smtp.Send(mail);
        }

        /* [Randy] Get list of all registration entries */
        public RegistrationPagingQuery getRegistrationList(int index)
        {
            RegistrationPagingQuery page = new RegistrationPagingQuery();
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    int pageSize = 10;
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
                        notes = reg.note,
                        usertype = new UserTypeName { userTypeID = reg.user.usertype.userTypeID, userTypeName = reg.user.usertype.userTypeName }
                    }).OrderBy(f => f.firstname).ToList();

                    page.rowCount = registrationList.Count();
                    if (page.rowCount > 0)
                    {
                        page.maxIndex = (int)Math.Ceiling(page.rowCount / (double)pageSize);
                        List<RegisteredUser> registrationPage = registrationList.Skip(pageSize * index).Take(pageSize).ToList(); //Skip past rows and take new elements
                        page.results = registrationPage;
                    }

                    return page;
                }
            }
            catch (Exception ex)
            {
                Console.Write("RegistrationManager.getRegistration error " + ex);
                return null;
            }
        }

        /* [Randy] Search within the list with a certain criteria */
        public RegistrationPagingQuery searchRegistration(int index, string criteria)
        {
            RegistrationPagingQuery page = new RegistrationPagingQuery();
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    int pageSize = 10;
                    var registrationList = new List<RegisteredUser>();
                    registrationList = context.registrations.Where(reg => ((reg.user.firstName + " " + reg.user.lastName).ToLower().Contains(criteria.ToLower()) || reg.user.usertype.userTypeName.ToLower().Contains(criteria.ToLower()) || reg.user.affiliationName.ToLower().Contains(criteria.ToLower())) && reg.deleted == false).Select(reg => new RegisteredUser
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
                        notes = reg.note,
                        usertype = new UserTypeName { userTypeID = reg.user.usertype.userTypeID, userTypeName = reg.user.usertype.userTypeName }
                    }).OrderBy(f => f.firstname).ToList();

                    page.rowCount = registrationList.Count();
                    if (page.rowCount > 0)
                    {
                        page.maxIndex = (int)Math.Ceiling(page.rowCount / (double)pageSize);
                        List<RegisteredUser> registrationPage = registrationList.Skip(pageSize * index).Take(pageSize).ToList(); //Skip past rows and take new elements
                        page.results = registrationPage;
                    }

                    return page;
                }
            }
            catch (Exception ex)
            {
                Console.Write("RegistrationManager.getRegistration error " + ex);
                return null;
            }
        }

        /* [Randy] Get list of all types of users */
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

        /* [Randy] Delete a specific registration entry */
        public bool deleteRegistration(int id)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    var registration = context.registrations.Where(reg => reg.registrationID == id).FirstOrDefault();
                    registration.deleted = true;
                    
                    context.users.Where(u => u.userID == registration.userID).FirstOrDefault().deleted = true;

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

        /* [Randy] Edit a specific registration entry */
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
                    registration.note = registeredUser.notes;
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

        /* [Randy] Get dates of the conference */
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

public class RegistrationPagingQuery
{
    public int indexPage;
    public int maxIndex;
    public int rowCount;
    public List<RegisteredUser> results;
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