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

        public bool uploadDocument(Authorization auth, MinorUser minor)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    minor.minorID = context.minors.Where(m => m.userID == minor.userID).FirstOrDefault().minorsID;
                    authorizationsubmitted authorization = context.authorizationsubmitteds.Where(a => a.minorID == minor.minorID).FirstOrDefault();

                    // ADD
                    if (authorization == null)
                    {
                        authorization = new authorizationsubmitted
                        {
                            minorID = minor.minorID,
                            documentName = auth.authorizationName,
                            documentFile = auth.authorizationFile,
                            deleted = false
                        };
                        context.authorizationsubmitteds.Add(authorization);
                    }
                    
                    // EDIT
                    else
                    {
                        authorization.documentName = auth.authorizationName;
                        authorization.documentFile = auth.authorizationFile;
                        authorization.minorID = auth.minor.minorID;
                    }

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