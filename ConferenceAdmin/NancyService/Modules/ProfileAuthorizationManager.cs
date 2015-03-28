using NancyService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NancyService.Modules
{
    public class ProfileAuthorizationManager
    {
        public ProfileAuthorizationManager()
        {
        }

        public List<authorizationtemplate> getTemplates()
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    List<authorizationtemplate> templates = context.authorizationtemplates.Where(t => t.deleted != true).ToList();
                    return templates;
                }
            }
            catch (Exception ex)
            {
                Console.Write("ProfileAuthorizationManager.getTemplates error " + ex);
                return null;
            }
        }

        public List<Authorization> getDocuments(UserInfo user)
        {
            /*try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    minor minor = context.minors.Where(m => m.userID == user.userID).FirstOrDefault();
                    List<authorizationsubmitted> documents = context.authorizationsubmitteds.Where(t => t.minorID == minor.id == minor.minorsID && t.deleted != true).ToList();
                    return documents;
                }
            }
            catch (Exception ex)
            {
                Console.Write("ProfileAuthorizationManager.getDocument error " + ex);
                return null;
            }*/
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    List<Authorization> documents = context.authorizationsubmitteds.Where(d => d.deleted != true && d.minorID == 1).Select(d => new Authorization 
                    { 
                        minor = new MinorUser{ userID = user.userID },
                        authorizationID = d.authorizationSubmittedID,
                        authorizationFile = d.documentFile,
                        authorizationName = d.documentName
                    }).ToList();
                    return documents;
                }
            }
            catch (Exception ex)
            {
                Console.Write("ProfileAuthorizationManager.getTemplates error " + ex);
                return null;
            }
        }

        public bool uploadDocument(Authorization auth, MinorUser minor)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    minor.minorID = context.minors.Where(m => m.userID == minor.userID).FirstOrDefault().minorsID;
                    authorizationsubmitted authorization = new authorizationsubmitted
                    {
                        minorID = minor.minorID,
                        documentName = auth.authorizationName,
                        documentFile = auth.authorizationFile,
                        deleted = false
                    };
                    context.authorizationsubmitteds.Add(authorization);
                    

                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.Write("ProfileAuthorizationManager.uploadDocument error " + ex);
                return false;
            }
        }


        public bool deleteDocument(Authorization auth)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    authorizationsubmitted authorization = context.authorizationsubmitteds.Where(a => a.authorizationSubmittedID == auth.authorizationID).FirstOrDefault();
                    authorization.deleted = true;
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.Write("ProfileAuthorizationManager.deleteDocument error " + ex);
                return false;
            }
        }



    }
}

public class MinorUser
{
    public long minorID;
    public long userID;
    public bool? authorizationStatus;
}

public class Authorization
{
    public MinorUser minor;
    public int authorizationID;
    public string authorizationName;
    public string authorizationFile;
}

public class Template
{
    public MinorUser minor;
    public int templateID;
    public string templateName;
    public string templateFile;
}