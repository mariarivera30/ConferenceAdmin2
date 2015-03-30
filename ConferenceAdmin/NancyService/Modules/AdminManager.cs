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
                    //ANADIR CONDICION PARA QUE NO SALGA EL MISMO ADMINISTRADOR EN LA LISTA QUE DEVUELVE
                    var administrators = context.claims.Where(admin => admin.deleted != true && admin.privilege.privilegestType != "Evaluator").Select(admin => new AdministratorQuery
                    {
                        userID = (long)admin.userID,
                        firstName = admin.user.firstName,
                        lastName = admin.user.lastName,
                        email = admin.user.membership.email,
                        privilege = admin.privilege.privilegestType,
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
                    var privileges = context.privileges.Where(privilege => privilege.privilegestType != "Evaluator").Select(privilege => new PrivilegeQuery()
                    {
                        privilegeID = privilege.privilegesID,
                        name = privilege.privilegestType,

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
                        }

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
                        }

                        else
                        {
                            oldAdmin.privilegesID = editAdmin.privilegeID;
                        }

                        context.SaveChanges();
                        return privilege;
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
                    //Check que otro admin no pueda quitar privilegio a Nayda
                    var admin = (from s in context.claims
                                 where s.userID == delAdmin.userID && s.privilegesID == delAdmin.privilegeID && s.deleted != true
                                 select s).FirstOrDefault();
                    if (admin != null)
                    {
                        admin.deleted = true;
                    }
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

    public class PrivilegeQuery
    {
        public int privilegeID;
        public String name;

        public PrivilegeQuery()
        {

        }
    }
}