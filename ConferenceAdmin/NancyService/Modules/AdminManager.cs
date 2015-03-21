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

        public List<AdministratorPrivilege> getAdministratorList()
        {/*
            try
            {

                using (conferenceadminContext context = new conferenceadminContext())
                {
                    var administrators = context.administrators.Where(admin => admin.enabled == true).Select(admin => new AdministratorPrivilege
                    {
                        membershipID = (long)admin.membershipID,
                        email = admin.membership.email,
                        privilege = admin.priviledge.priviledgestType

                    }).ToList();

                    return administrators;
                }
            }
            catch (Exception ex)
            {
                Console.Write("AdministratorsManager.getAdministratorList error " + ex);
                return null;
            }*/return null;
        }

        public bool deleteAdministrator(long id)
        {
            /*
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    var admin = (from s in context.administrators
                                 where s.membershipID == id
                                 select s).First();
                    admin.enabled = false;
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.Write("AdministratorsManager.deleteAdministrator error " + ex);
                return false;
            }*/
            return false;
        }

        public AdministratorPrivilege addAdmin(AdministratorPrivilege s)
        { /*
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    //Check if user is member
                    var membershipID = (from member in context.memberships
                                        where member.email == s.email
                                        select member.membershipID).FirstOrDefault();

                    if (membershipID == 0)
                    {
                        //Add member
                        membership newMember = new membership();
                        newMember.email = s.email;
                        newMember.emailConfirmation = false;
                        newMember.password = "root"; //password generator?
                        newMember.membershipTypeID = 1;
                        context.memberships.Add(newMember);
                        context.SaveChanges();
                        membershipID = newMember.membershipID;

                        //Add admin 
                        administrator newAdmin = new administrator();
                        newAdmin.priviledgesID = s.privilegeID;
                        newAdmin.enabled = true;
                        newAdmin.membershipID = membershipID;
                        context.administrators.Add(newAdmin);
                        context.SaveChanges();
                    }

                    else
                    {
                        //Check if newAdmin is not already in the table
                        var checkAdmin = (from admin in context.administrators
                                          where admin.membershipID == membershipID && admin.enabled == true
                                          select admin.membershipID).Count();

                        if (checkAdmin == 0)
                        {
                            //Add admin if it is not already on the list
                            administrator newAdmin = new administrator();
                            newAdmin.priviledgesID = s.privilegeID;
                            newAdmin.enabled = true;
                            newAdmin.membershipID = membershipID;
                            context.administrators.Add(newAdmin);
                            context.SaveChanges();
                        }
                        else
                        {
                            return null;
                        }

                    }

                    //For returning the administrator object
                    s.membershipID = membershipID;

                    return s;
                }
            }
            catch (Exception ex)
            {
                Console.Write("AdministratorsManager.addAdmin error " + ex);
                return s;
            }*/return null;

        }

        public bool editAdministrator(AdministratorPrivilege editAdmin)
        {/*
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    var admin = (from s in context.administrators
                                 where s.membershipID == editAdmin.membershipID
                                 select s).First();
                    admin.priviledgesID = editAdmin.privilegeID;
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.Write("AdministratorsManager.editAdministrator error " + ex);
                return false;
            }*/ return false;
        }
           
    }

    public class AdministratorPrivilege
    {
        public long membershipID;
        public String email;
        public String privilege;
        public int privilegeID;

        public AdministratorPrivilege()
        {

        }
    }
}