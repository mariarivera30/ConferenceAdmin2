using NancyService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NancyService.Modules
{
    public class AdminManager
    {
        public AdminManager()
        {

        }

        public List<AdministratorQuery> getAdministratorList()
        {
            try
            {

                using (conferenceadminContext context = new conferenceadminContext())
                {
                    var administrators = context.claims.Where(admin => admin.deleted != true && admin.privilege.privilegestType !="Evaluator").Select(admin => new AdministratorQuery
                    {
                        userID = (long)admin.userID,
                        firstName= admin.user.firstName,
                        lastName= admin.user.lastName,
                        email = admin.user.membership.email,
                        privilege=admin.privilege.privilegestType,
                        privilegeID = (int)admin.privilegesID

                    }).ToList();

                    return administrators;
                }
            }
            catch (Exception ex)
            {
                Console.Write("AdminManager.getAdministratorList error " + ex);
                return null;
            }
        }

        public List<PrivilegeQuery> getPrivilegesList()
        {
            try
            {

                using (conferenceadminContext context = new conferenceadminContext())
                {
                    var privileges = context.privileges.Where(privilege => privilege.privilegestType != "Evaluator").Select(privilege => new PrivilegeQuery()
                    {
                        privilegeID = privilege.privilegesID,
                        name= privilege.privilegestType,

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

        public bool deleteAdministrator(AdministratorQuery delAdmin)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    var admin = (from s in context.claims
                                 where s.userID == delAdmin.userID && s.privilegesID == delAdmin.privilegeID
                                 select s).First();
                    admin.deleted = true;
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.Write("AdminManager.deleteAdministrator error " + ex);
                return false;
            }
        }

        public AdministratorQuery addAdmin(AdministratorQuery s)
        { 
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    //Get Privilege Name
                    s.privilege = (from privilege in context.privileges
                                   where privilege.privilegesID == s.privilegeID
                                   select privilege.privilegestType).FirstOrDefault();

                    //Check if user is Member
                    s.userID = (from user in context.users
                                        where user.membership.email == s.email
                                        select user.userID).FirstOrDefault();

                    if (s.userID == 0)
                    {
                        //Add member
                        membership newMember = new membership();
                        newMember.email = s.email;
                        newMember.deleted = false;
                        newMember.emailConfirmation = false;
                        newMember.password = "root"; //password generator?
                        context.memberships.Add(newMember);
                        context.SaveChanges();

                        //Add user
                        user newUser = new user();
                        newUser.membershipID = newMember.membershipID;
                        newUser.firstName = s.firstName;
                        newUser.lastName = s.lastName;
                        newUser.acceptanceStatus = "N/A";
                        newUser.userTypeID = 4;
                        newUser.addressID = 1;
                        newUser.affiliationName = "";
                        newUser.deleted = false;
                        newUser.hasApplied = false;
                        newUser.title = "";
                        newUser.phone = "";
                        newUser.userFax = "";
                        context.users.Add(newUser);
                        context.SaveChanges();

                        //Add admin 
                        claim newAdmin = new claim();
                        newAdmin.privilegesID = s.privilegeID;
                        newAdmin.deleted = false;
                        newAdmin.userID = newUser.userID; 
                        context.claims.Add(newAdmin);
                        context.SaveChanges();

                        s.userID= newUser.userID;
                    }

                    else
                    {
                        //Check if newAdmin can add the selected privilege
                        var checkAdmin = (from admin in context.claims
                                          where admin.userID == s.userID && admin.privilege.privilegestType != "Evaluator"
                                          select admin.userID).Count();

                        if (checkAdmin == 0)
                        {
                            //Add admin 
                            claim newAdmin = new claim();
                            newAdmin.privilegesID = s.privilegeID;
                            newAdmin.deleted = false;
                            newAdmin.userID = s.userID;
                            context.claims.Add(newAdmin);
                            context.SaveChanges();
                        }
                        else
                        {
                            return null;
                        }

                    }

                    return s;
                }
            }
            catch (Exception ex)
            {
                Console.Write("AdminManager.addAdmin error " + ex);
                return s;
            }

        }

        public String editAdministrator(AdministratorQuery editAdmin)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                   String privilege = (from p in context.privileges
                                   where p.privilegesID == editAdmin.privilegeID
                                   select p.privilegestType).FirstOrDefault();

                    var admin = (from s in context.claims
                                 where s.userID == editAdmin.userID
                                 select s).First();

                    if (admin != null)
                    {
                        admin.privilegesID = editAdmin.privilegeID;
                        context.SaveChanges();
                        return privilege;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write("AdminManager.editAdministrator error " + ex);
                return null;
            }
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

        public AdministratorQuery()
        {

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