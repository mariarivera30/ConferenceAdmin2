using NancyService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;


namespace NancyService.Modules
{
    public class SignUpManager
    {
        public class UserCreation
        {
            public string email { get; set; }
            public string password { get; set; }
            public int userTypeID { get; set; }
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string title { get; set; }
            public string affiliationName { get; set; }
            public string phone { get; set; }
            public long addressID { get; set; }
            public string userFax { get; set; }
            public string line1 { get; set; }
            public string line2 { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public string country { get; set; }
            public string zipcode { get; set; }
            public long membershipID { get; set; }
            public string newPass { get; set; }


        }
       
       
        public bool createUser(user user, membership member, address address)
        {
            try
            {
                string key = generateEmailConfirmationKey();
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    member.deleted = false;
                    member.emailConfirmation = true;
                    member.deleted = false;
                    member.confirmationKey = key;
                    context.memberships.Add(member);
                    context.SaveChanges();
                    context.addresses.Add(address);
                    context.SaveChanges();

                    user.addressID = address.addressID;
                    user.membershipID = member.membershipID;
                    user.acceptanceStatus = "Pending";
                    user.deleted = false;
                    user.hasApplied = false;
                    user.registrationStatus = "Pending";
                    user.evaluatorStatus = user.evaluatorStatus;
                    
                    context.users.Add(user);
                    context.SaveChanges();

                    if (user.userTypeID == 1)
                    {
                        minor minor = new minor();
                        minor.authorizationStatus = false;
                        minor.deleted = false;
                        minor.userID = user.userID;
                        context.minors.Add(minor);
                        context.SaveChanges();

                    }
                    else if (user.userTypeID == 6)
                    {
                        companion companion = new companion();
                        companion.deleted = false;
                        companion.userID = user.userID;
                        context.companions.Add(companion);
                        context.SaveChanges();

                    }

                    sendEmailConfirmation(member.email, member.confirmationKey);

                    return true;


                }

            }
            catch (Exception ex)
            {
                Console.Write("SignUpManager.addSponsor error " + ex);
                return false;
            }

        }

        private string generateEmailConfirmationKey()
        {
            return "CCWIC" + Guid.NewGuid().ToString();
        }

        private void sendEmailConfirmation(string email, string key)
        {
            MailAddress ccwic = new MailAddress("maria.rivera30@upr.edu");
            MailAddress user = new MailAddress(email);
            MailMessage mail = new System.Net.Mail.MailMessage(ccwic, user);


            mail.Subject = "Caribbean Celebration of Women in Computing Account Confirmation!";
            mail.Body = "Please click the link to confirm your account. \n\n " + "http://localhost:12029/#/Validate" + "\n Your key is " + key;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;

            smtp.Credentials = new NetworkCredential(
                "maria.rivera30@upr.edu", "casa7463");
            smtp.EnableSsl = true;

            smtp.Send(mail);
        }
        private void sendTemporaryPassword(string email, string pass)
        {
            MailAddress ccwic = new MailAddress("maria.rivera30@upr.edu");
            MailAddress user = new MailAddress(email);
            MailMessage mail = new System.Net.Mail.MailMessage(ccwic, user);


            mail.Subject = "Caribbean Celebration of Women Temporary Password!";
            mail.Body = "Login using this password " + pass + ".\n Change your password as soon as possible.\n Visit us: http://localhost:12029/#/ChangePassword";

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;

            smtp.Credentials = new NetworkCredential(
                "maria.rivera30@upr.edu", "casa7463");
            smtp.EnableSsl = true;

            smtp.Send(mail);
        }
        public UserCreation confirmAccount(string key)
        {


            using (conferenceadminContext context = new conferenceadminContext())
            {

                try
                {
                    UserCreation user = (from m in context.memberships
                                         from u in context.users
                                         where (m.confirmationKey.Equals(key) && m.membershipID == u.membershipID && m.deleted == false)
                                         select new UserCreation
                                         {
                                             firstName = u.firstName,
                                             lastName = u.lastName,
                                             email = m.email,
                                             membershipID = m.membershipID
                                         }).FirstOrDefault();
                    if (user != null)
                    {
                        context.memberships
                              .Where(s => s.membershipID == user.membershipID && s.deleted == false)
                              .ToList().ForEach(s => { s.emailConfirmation = true; });
                        context.SaveChanges();
                    }
                    return user;
                }
                catch (Exception ex)
                {
                    Console.Write("SponsorManager.getSponsor error " + ex);
                    return null;
                }
            }

        }


        public UserCreation requestPass(string email)
        {
            using (conferenceadminContext context = new conferenceadminContext())
            {

                try
                {
                    string tempPass = generateEmailConfirmationKey().Substring(0, 9); ;
                    var member = (from m in context.memberships
                                  where (m.email.Equals(email) && m.deleted == false)
                                  select m).FirstOrDefault();
                    if (member != null)
                    {
                        member.password = tempPass;
                        UserCreation u = new UserCreation();
                        u.email = member.email;
                        u.membershipID = member.membershipID;
                        context.SaveChanges();
                        sendTemporaryPassword(u.email, tempPass);
                        return u;
                    }

                    else
                    {
                        return null;
                    }


                }
                catch (Exception ex)
                {
                    Console.Write("checkEmail error " + ex);
                    return null;
                }
            }

        }

        public bool checkEmail(string email)
        {


            using (conferenceadminContext context = new conferenceadminContext())
            {

                try
                {
                    var user = (from m in context.memberships
                                where (m.email.Equals(email) && m.deleted == false)
                                select m).FirstOrDefault();
                    if (user != null)
                    {
                        return true;
                    }

                    else
                    {
                        return false;
                    }

                }
                catch (Exception ex)
                {
                    Console.Write("SponsorManager.getSponsor error " + ex);
                    return false;
                }
            }

        }

        public UserCreation changePassword(UserCreation u)
        {
            using (conferenceadminContext context = new conferenceadminContext())
            {

                try
                {
                    
                    var member = (from m in context.memberships
                                  where (m.email.Equals(u.email) && m.password== u.password && m.deleted == false)
                                  select m).FirstOrDefault(); 
                    if (member != null)
                    {
                        member.password = u.newPass;
                        u.membershipID = member.membershipID;
                        context.SaveChanges();
                        return u;
                    }

                    else
                    {
                        return null;
                    }


                }
                catch (Exception ex)
                {
                    Console.Write("checkEmail error " + ex);
                    return null;
                }
            }

        }


    }
}