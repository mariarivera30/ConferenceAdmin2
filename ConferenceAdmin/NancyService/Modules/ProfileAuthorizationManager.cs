using NancyService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NancyService.Modules
{
    public class ProfileAuthorizationManager
    {
        /* [Randy] Empty Constructor */
        public ProfileAuthorizationManager(){}

        /* [Randy] Get list of authorization templates */
        public List<Template> getTemplates()
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    List<Template> templates = context.authorizationtemplates.Where(t => t.deleted != true).
                        Select(n => new Template
                        {
                            templateID = n.authorizationID,
                            templateName = n.authorizationName,
                        }).ToList();
                    return templates;
                }
            }
            catch (Exception ex)
            {
                Console.Write("ProfileAuthorizationManager.getTemplates error " + ex);
                return null;
            }
        }

        /* [Randy] Download authorization template file */
        public Template getTemplateFile(int templateID)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    Template file = context.authorizationtemplates.Where(c => c.authorizationID == templateID && c.deleted == false).
                        Select(d => new Template
                        {
                            templateID = d.authorizationID,
                            templateName = d.authorizationName,
                            templateFile = d.authorizationDocument
                        }).FirstOrDefault();
                    return file;
                }
            }
            catch (Exception ex)
            {
                Console.Write("ProfileAuthorizationManager.getTemplateFile error " + ex);
                return null;
            }
        }

        /* [Randy] Get list of all documents submitted */
        public List<Authorization> getDocuments(UserInfo user)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    minor minor = context.minors.Where(m => m.userID == user.userID).FirstOrDefault();
                    List<Authorization> documents = context.authorizationsubmitteds.Where(d => d.deleted != true && d.minorID == minor.minorsID).Select(d => new Authorization 
                    { 
                        minor = new MinorUser{ userID = user.userID },
                        authorizationID = d.authorizationSubmittedID,
                        //authorizationFile = d.documentFile,
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

        /* [Randy] Download a selected authorization file */
        public Authorization getAuthorizationFile(int authorizationID)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    Authorization file = context.authorizationsubmitteds.Where(c => c.authorizationSubmittedID == authorizationID && c.deleted == false).
                        Select(d => new Authorization
                        {
                            authorizationID = d.authorizationSubmittedID,
                            authorizationName = d.documentName,
                            authorizationFile = d.documentFile
                        }).FirstOrDefault();
                    return file;
                }
            }
            catch (Exception ex)
            {
                Console.Write("ProfileAuthorizationManager.getAuthorizationFile error " + ex);
                return null;
            }
        }

        /* [Randy] Upload an authorization file */
        public int uploadDocument(Authorization auth, MinorUser minor)
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

                    minor.authorizationStatus = true;

                    context.SaveChanges();
                    return authorization.authorizationSubmittedID;
                }
            }
            catch (Exception ex)
            {
                Console.Write("ProfileAuthorizationManager.uploadDocument error " + ex);
                return 0;
            }
        }

        /* [Randy] Delete a specific authorization document */
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

        /* [Randy] Get the submitted companion key */
        public CompanionKey getCompanionKey(UserInfo user)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    companionminor companionminor = context.companionminors.Where(cm => cm.minor.userID == user.userID).FirstOrDefault();
                    if (companionminor != null)
                    {
                        string key = companionminor.companion.companionKey;
                        return new CompanionKey { companionKey = key };
                    }

                    else return new CompanionKey();
                }
            }
            catch (Exception ex)
            {
                Console.Write("ProfileAuthorizationManager.getCompanionKey error " + ex);
                return null;
            }
        }

        /* [Randy] Bind companion with minor */
        public string selectCompanion(UserInfo user, companion companion)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    companion = context.companions.Where(c => c.companionKey == companion.companionKey).FirstOrDefault();
                    minor minor = context.minors.Where(m => m.userID == user.userID).FirstOrDefault();

                    if (companion != null){
                        companionminor companionminor = new companionminor
                        {
                            companionID = companion.companionID,
                            minorID = minor.minorsID,
                            deleted = false
                        };
                        context.companionminors.Add(companionminor);                        
                    }

                    var status = context.users.Where(u => u.userID == companion.userID).FirstOrDefault().registrationStatus;
                    
                    if(status == "Accepted")
                        context.SaveChanges();

                    return status;
                }
            }
            catch (Exception ex)
            {
                Console.Write("ProfileAuthorizationManager.selectCompanion error " + ex);
                return null;
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

public class CompanionKey
{
    public string companionKey;
}