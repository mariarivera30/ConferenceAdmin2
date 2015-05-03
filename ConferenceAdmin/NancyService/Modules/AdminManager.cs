using NancyService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;

namespace NancyService.Modules
{
    public class AdminManager
    {
        //ccwicEmail
        string ccwicEmail = "ccwictest@gmail.com";
        string ccwicEmailPass = "ccwic123456789";
        string testEmail = "heidi.negron1@upr.edu";

        public AdminManager()
        {

        }

        public AdminPagingQuery getAdministratorList(int index)
        {
            AdminPagingQuery page = new AdminPagingQuery();

            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    int pageSize = 10;
                    var query = context.claims.Where(admin => admin.deleted != true && admin.privilege.privilegestType != "Master" && admin.privilege.privilegestType != "Evaluator").Select(admin => new AdministratorQuery
                    {
                        userID = (long)admin.userID,
                        firstName = admin.user.firstName,
                        lastName = admin.user.lastName,
                        email = admin.user.membership.email,
                        privilege = admin.privilege.privilegestType == "Admin" ? "Administrator" : admin.privilege.privilegestType == "CommitteEvaluator" ? "Committee Evaluator" : admin.privilege.privilegestType,
                        privilegeID = (int)admin.privilegesID

                    }).OrderBy(x => x.userID);

                    page.rowCount = query.Count();
                    if (page.rowCount > 0)
                    {
                        page.maxIndex = (int)Math.Ceiling(page.rowCount / (double)pageSize);
                        var administrators = query.Skip(pageSize * index).Take(pageSize).ToList(); //Skip past rows and take new elements
                        page.results = administrators;
                    }

                    return page;
                }
            }
            catch (Exception ex)
            {
                Console.Write("AdminManager.getAdministratorList error " + ex);
                return null;
            }
        }

        public bool checkNewAdmin(String email)
        {
            try
            {

                using (conferenceadminContext context = new conferenceadminContext())
                {
                    var result = (from user in context.users
                                  where user.membership.email == email
                                  select user.userID).Count();

                    if (result > 0)
                    {
                        return true;
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.Write("AdminManager.checkNewAdmin error " + ex);
                return false;
            }
        }

        public List<PrivilegeQuery> getPrivilegesList()
        {
            try
            {

                using (conferenceadminContext context = new conferenceadminContext())
                {
                    var privileges = context.privileges.Where(privilege => privilege.privilegestType != "Master" && privilege.privilegestType != "Evaluator").Select(privilege => new PrivilegeQuery()
                    {
                        privilegeID = privilege.privilegesID,
                        name = privilege.privilegestType == "Admin" ? "Administrator" : privilege.privilegestType == "CommitteEvaluator" ? "Committee Evaluator" : privilege.privilegestType,

                    }).ToList();

                    return privileges;
                }
            }
            catch (Exception ex)
            {
                Console.Write("AdminManager.getPrivilegesList error " + ex);
                return null;
            }
        }

        public AdministratorQuery addAdmin(AdministratorQuery s)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    //Get privilege name
                    s.privilege = (from p in context.privileges
                                   where p.privilegesID == s.privilegeID
                                   select p.privilegestType).FirstOrDefault();

                    //Check if user is Member. Note: In the administrator tab the user has already been checked for membership.
                    var userInfo = (from user in context.users
                                    where user.membership.email == s.email
                                    select user).FirstOrDefault();

                    if (userInfo != null && s.privilege != null)
                    {
                        //User exists
                        s.userID = userInfo.userID;
                        s.firstName = userInfo.firstName;
                        s.lastName = userInfo.lastName;
                        s.email = userInfo.membership.email;

                        //Check if newAdmin has already a privilege
                        var checkAdmin = (from admin in context.claims
                                          where admin.userID == s.userID && admin.privilege.privilegestType != "Evaluator" && admin.deleted != true
                                          select admin).FirstOrDefault();

                        if (checkAdmin != null)
                        {
                            return null;
                        }

                        else
                        {
                            //Check if user has had the privilege before
                            var adminPrivilege = (from admin in context.claims
                                                  where admin.userID == s.userID && admin.privilege.privilegestType == s.privilege
                                                  select admin).FirstOrDefault();

                            if (adminPrivilege != null)
                            {
                                adminPrivilege.deleted = false;
                                context.SaveChanges();
                            }

                            else
                            {
                                //Add admin 
                                claim newAdmin = new claim();
                                newAdmin.privilegesID = s.privilegeID;
                                newAdmin.deleted = false;
                                newAdmin.userID = s.userID;
                                context.claims.Add(newAdmin);
                                context.SaveChanges();
                            }

                            try { sendEmailConfirmation(s.email, s.privilege); }
                            catch (Exception ex)
                            {
                                Console.Write("AdminManager.sendnewAdminEmail error " + ex);
                                return null;
                            }

                            if (s.privilege != "Finance")
                            {
                                EvaluatorManager evaluator = new EvaluatorManager();
                                evaluator.addEvaluator(s.email);
                            }
                        }

                        s.privilege = s.privilege == "Admin" ? "Administrator" : s.privilege == "CommitteEvaluator" ? "Committee Evaluator" : s.privilege;

                        return s;
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.Write("AdminManager.addAdmin error " + ex);
                return null;
            }

        }

        public String editAdministrator(AdministratorQuery editAdmin)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    //Get privilege name
                    String privilege = (from p in context.privileges
                                        where p.privilegesID == editAdmin.privilegeID
                                        select p.privilegestType).FirstOrDefault();

                    //Check if admin had the privilege before
                    var admin = (from s in context.claims
                                 where s.userID == editAdmin.userID && s.privilegesID == editAdmin.privilegeID
                                 select s).FirstOrDefault();

                    //Get current--soon to be old privilege--- information
                    var oldAdmin = (from s in context.claims
                                    where s.userID == editAdmin.userID && s.privilegesID == editAdmin.oldPrivilegeID && s.deleted != true
                                    select s).FirstOrDefault();

                    if (oldAdmin != null && privilege != null)
                    {
                        if (admin != null && admin.claimsID != oldAdmin.claimsID)
                        {
                            admin.deleted = false;
                            oldAdmin.deleted = true;
                            
                            if (privilege != "Finance")
                            {
                                EvaluatorManager evaluator = new EvaluatorManager();
                                evaluator.addEvaluator(admin.user.membership.email);
                            }
                        }

                        else
                        {
                            oldAdmin.privilegesID = editAdmin.privilegeID;

                            if (privilege != "Finance")
                            {
                                EvaluatorManager evaluator = new EvaluatorManager();
                                evaluator.addEvaluator(oldAdmin.user.membership.email);
                            }
                        }

                        try {sendEmailEditAdminConfirmation(oldAdmin.user.membership.email, privilege);}
                        catch (Exception ex)
                        {
                            Console.Write("AdminManager.sendeditAdminEmail error " + ex);
                            return null;
                        }

                        context.SaveChanges();
                        return privilege == "Admin" ? "Administrator" : privilege == "CommitteEvaluator" ? "Committee Evaluator" : privilege;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.Write("AdminManager.editAdministrator error " + ex);
                return null;
            }
        }

        public bool deleteAdministrator(AdministratorQuery delAdmin)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    var admin = (from s in context.claims
                                 where s.userID == delAdmin.userID && s.privilegesID == delAdmin.privilegeID && s.deleted != true
                                 select s).FirstOrDefault();
                    if (admin != null)
                    {
                        admin.deleted = true;
                    }
                    context.SaveChanges();

                    if (delAdmin.privilegeID != 2)//Finance Privilege
                    {
                        EvaluatorQuery evaluator = new EvaluatorQuery();
                        evaluator.userID = delAdmin.userID;
                        evaluator.acceptanceStatus = "Rejected";
                        EvaluatorManager manager = new EvaluatorManager();
                        manager.updateAcceptanceStatus(evaluator);
                    }
                    
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.Write("AdminManager.deleteAdministrator error " + ex);
                return false;
            }
        }

        public AdminPagingQuery searchAdministrators(int index, String criteria)
        {
            AdminPagingQuery page = new AdminPagingQuery();

            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    int pageSize = 10;
                    var query = context.claims.Where(admin => (admin.deleted != true && admin.privilege.privilegestType != "Master" && admin.privilege.privilegestType != "Evaluator") && ((admin.user.firstName.ToLower() + " " + admin.user.lastName.ToLower()).Contains(criteria.ToLower()) || admin.user.membership.email.ToLower().Contains(criteria.ToLower()))).Select(admin => new AdministratorQuery
                    {
                        userID = (long)admin.userID,
                        firstName = admin.user.firstName,
                        lastName = admin.user.lastName,
                        email = admin.user.membership.email,
                        privilege = admin.privilege.privilegestType == "Admin" ? "Administrator" : admin.privilege.privilegestType == "CommitteEvaluator" ? "Committee Evaluator" : admin.privilege.privilegestType,
                        privilegeID = (int)admin.privilegesID

                    }).OrderBy(x => x.userID);

                    page.rowCount = query.Count();
                    if (page.rowCount > 0)
                    {
                        page.maxIndex = (int)Math.Ceiling(page.rowCount / (double)pageSize);
                        var administrators = query.Skip(pageSize * index).Take(pageSize).ToList(); //Skip past rows and take new elements
                        page.results = administrators;
                    }

                    return page;
                }
            }
            catch (Exception ex)
            {
                Console.Write("AdminManager.searchAdministrators error " + ex);
                return null;
            }
        }

        private void sendEmailConfirmation(string email, String p)
        {
            if (p == "Admin")
            {
                p = "Administrator";
            }
            else if (p == "CommitteEvaluator")
            {
                p = "Committee Manager";
            }
            else if (p == "Finance")
            {
                p = "Finance Manager";
            }

            MailAddress ccwic = new MailAddress(ccwicEmail);
            MailAddress user = new MailAddress(testEmail);
            MailMessage mail = new System.Net.Mail.MailMessage(ccwic, user);

            String closing = " \r\nThank you.\r\nCCWiC Administration";

            mail.Subject = "Caribbean Celebration of Women in Computing- Administrator Information";
            mail.Body = "Greetings,\r\n \r\nYou have been given a privilege within our system: " + p + ". You can now access Administrator Settings by login in ConferenceAdmin.\r\n"+closing;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;

            smtp.Credentials = new NetworkCredential(
                ccwicEmail, ccwicEmailPass);
            smtp.EnableSsl = true;

            smtp.Send(mail);
        }

        private void sendEmailEditAdminConfirmation(string email, String p)
        {
            if (p == "Admin")
            {
                p = "Administrator";
            }
            else if (p == "CommitteEvaluator")
            {
                p = "Committee Manager";
            }
            else if (p == "Finance")
            {
                p = "Finance Manager";
            }
            
            MailAddress ccwic = new MailAddress(ccwicEmail);
            MailAddress user = new MailAddress(testEmail);
            MailMessage mail = new System.Net.Mail.MailMessage(ccwic, user);

            String closing = " \r\nThank you.\r\nCCWiC Administration";

            mail.Subject = "Caribbean Celebration of Women in Computing- Administrator Information";
            mail.Body = "Greetings,\r\n \r\nYour privilege within our system has changed. You are now: " + p + ". Remember: You can access Administrator Settings by login in ConferenceAdmin.\r\n"+closing;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;

            smtp.Credentials = new NetworkCredential(
                ccwicEmail, ccwicEmailPass);
            smtp.EnableSsl = true;

            smtp.Send(mail);
        }

    }

    public class AdministratorQuery
    {
        public long userID;
        public String firstName;
        public String lastName;
        public String email;
        public String privilege;
        public int privilegeID;
        public int oldPrivilegeID;

        public AdministratorQuery()
        {

        }
    }

    public class AdminPagingQuery
    {
        public int indexPage;
        public int maxIndex;
        public int rowCount;
        public List<AdministratorQuery> results;

        public AdminPagingQuery()
        {
            results = new List<AdministratorQuery>();
        }
    }

    public class PrivilegeQuery
    {
        public int privilegeID;
        public String name;

        public PrivilegeQuery()
        {

        }
    }
}