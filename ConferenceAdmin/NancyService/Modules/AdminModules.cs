using Nancy;
using Nancy.Responses;
using NancyService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy.ModelBinding;
using Nancy.Authentication.Token;
using Nancy.Security;

namespace NancyService.Modules
{
    public class AdminModules : NancyModule
    {
        public AdminModules(ITokenizer tokenizer)
            : base("/admin")
        {
            WebManager webManager = new WebManager();
            ReportManager reportManager = new ReportManager();
            AdminManager adminManager = new AdminManager();
            EvaluatorManager evaluatorManager = new EvaluatorManager();
            TopicManager topicManager = new TopicManager();
            SponsorManager sponsorManager = new SponsorManager();
            List<sponsor> sponsorList = new List<sponsor>();
            RegistrationManager registration = new RegistrationManager();
            GuestManager guest = new GuestManager();
            TemplateManager templateManager = new TemplateManager();
            AuthTemplateManager authTemplateManager = new AuthTemplateManager();
            SubmissionManager submissionManager = new SubmissionManager();
            BannerManager bannerManager = new BannerManager();


            /* ----- Template -----*/


            Post["/addTemplate"] = parameters =>
            {
                var temp = this.Bind<TemplateManager.templateQuery>();
                TemplateManager.templateQuery result = templateManager.addTemplate(temp);
                if (result != null)
                {
                    return Response.AsJson(result);
                }

                else
                {
                    return HttpStatusCode.Conflict;
                }

            };

            Get["/getTemplatesAdmin"] = parameters =>
            {

                return Response.AsJson(templateManager.getTemplates());
            };

            Get["/getTemplatesAdminListIndex/{index:int}"] = parameters =>
            {
                int index = parameters.index;
                return Response.AsJson(templateManager.getTemplates(index));
            };

            Put["/deleteTemplate"] = parameters =>
            {
                var id = this.Bind<long>();
                if (templateManager.deleteTemplate(id))
                {
                    return HttpStatusCode.OK;
                }

                else
                {
                    return HttpStatusCode.Conflict;
                }
            };

            Put["/updateTemplate"] = parameters =>
            {
                var template = this.Bind<template>();

                if (templateManager.updateTemplate(template))
                {
                    return HttpStatusCode.OK;
                }

                else
                {
                    return HttpStatusCode.Conflict;
                }
            };
            /* ----- Auth Template -----*/


            Post["/addAuthTemplate"] = parameters =>
            {
                var temp = this.Bind<AuthTemplateManager.templateQuery>();
                AuthTemplateManager.templateQuery result = authTemplateManager.addTemplate(temp);
                if (result != null)
                {
                    return Response.AsJson(result);
                }

                else
                {
                    return HttpStatusCode.Conflict;
                }

            };

            Get["/getAuthTemplatesAdmin"] = parameters =>
            {

                return Response.AsJson(authTemplateManager.getTemplates());
            };

            Get["/getAuthTemplatesAdminListIndex/{index:int}"] = parameters =>
            {
                int index = parameters.index;
                return Response.AsJson(authTemplateManager.getTemplates(index));
            };

            Put["/deleteAuthTemplate"] = parameters =>
            {
                var id = this.Bind<int>();
                if (authTemplateManager.deleteTemplate(id))
                {
                    return HttpStatusCode.OK;
                }

                else
                {
                    return HttpStatusCode.Conflict;
                }
            };

            Put["/updateAuthTemplate"] = parameters =>
            {
                var template = this.Bind<authorizationtemplate>();

                if (authTemplateManager.updateTemplate(template))
                {
                    return HttpStatusCode.OK;
                }

                else
                {
                    return HttpStatusCode.Conflict;
                }
            };

            /* ----- Sponsor Complementary-----*/
            Post["/addSponsorComplementaryKeys"] = parameters =>
            {

                var obj = this.Bind<NancyService.Modules.SponsorManager.addComplementary>();
                return Response.AsJson(sponsorManager.addKeysTo(obj));

            };
            Put["/deleteComplementaryKey"] = parameters =>
            {
                var id = this.Bind<long>();
                if (sponsorManager.deleteComplementary(id))
                {
                    return HttpStatusCode.OK;
                }

                else
                {
                    return HttpStatusCode.Conflict;
                }
            };
            Put["/deleteSponsorComplementaryKey"] = parameters =>
            {
                var id = this.Bind<long>();
                return Response.AsJson(sponsorManager.deleteComplementarySponsor(id));
            };
            Get["/getComplementaryKeys"] = parameters =>
            {
                try
                {
                    // this.RequiresAuthentication();
                    // this.RequiresClaims(new[] { "minor" });
                    return Response.AsJson(sponsorManager.getComplementaryList());
                }
                catch { return null; }
            };
            Get["/getSponsorComplementaryKeys/{id:long}"] = parameters =>
            {
                try
                {
                    long id = parameters.id;
                    return Response.AsJson(sponsorManager.getSponsorComplentaryList(id));
                }
                catch { return null; }
            };

            Get["/getSponsorComplementaryKeysFromIndex/{index:int}/{id:long}"] = parameters =>
            {
                try
                {
                    NancyService.Modules.SponsorManager.ComplimentaryPagingQuery info = new NancyService.Modules.SponsorManager.ComplimentaryPagingQuery();
                    info.sponsorID = parameters.id;
                    info.index = parameters.index;
                    return Response.AsJson(sponsorManager.getSponsorComplentaryList(info));
                }
                catch { return null; }
            };

            //--------------------------------------------Sponsor----------------------------
            Post["/addsponsor"] = parameters =>
            {

                var sponsor = this.Bind<NancyService.Modules.SponsorManager.SponsorQuery>();
                SponsorManager.SponsorQuery added = sponsorManager.addSponsor(sponsor);
                if (added != null)
                {
                    return Response.AsJson(added);
                }

                else
                {
                    return HttpStatusCode.Conflict;
                }
            };

            Get["/getSponsor"] = parameters =>
            {
                try
                {
                    // this.RequiresAuthentication();
                    // this.RequiresClaims(new[] { "minor" });
                    return Response.AsJson(sponsorManager.getSponsorList());
                }
                catch { return null; }
            };

            Get["/getSponsorListIndex/{index:int}"] = parameters =>
            {
                int index = parameters.index;
                return Response.AsJson(sponsorManager.getSponsorList(index));
            };

            Get["/getSponsorbyID/{id:long}"] = parameters =>
            {
                try
                {
                    // this.RequiresAuthentication();
                    // this.RequiresClaims(new[] { "minor" });
                    long id = parameters.id;
                    return Response.AsJson(sponsorManager.getSponsorbyID(id));
                }
                catch { return null; }
            };


            Put["/updateSponsor"] = parameters =>
            {
                var sponsor = this.Bind<NancyService.Modules.SponsorManager.SponsorQuery>();
                SponsorManager.SponsorQuery s = sponsorManager.updateSponsor(sponsor);
                if (s != null)
                {
                    return Response.AsJson(s);
                }

                else
                {
                    return HttpStatusCode.Conflict;
                }
            };

            Get["/getSponsorTypesList"] = parameters =>
            {
                try
                {
                    //this.RequiresAuthentication();
                    //this.RequiresClaims(new[] { "admin" });
                    return Response.AsJson(sponsorManager.getSponsorTypesList());
                }
                catch { return null; }
            };


            Put["/deleteSponsor"] = parameters =>
            {
                var id = this.Bind<long>();
                if (sponsorManager.deleteSponsor(id))
                {
                    return HttpStatusCode.OK;
                }

                else
                {
                    return HttpStatusCode.Conflict;
                }
            };
            /* ----- Topic -----*/

            Get["/getTopic"] = parameters =>
            {

                return Response.AsJson(topicManager.getTopicList());
            };

            Post["/addTopic"] = parameters =>
            {
                var topic = this.Bind<topiccategory>();
                return Response.AsJson(topicManager.addTopic(topic));
            };

            Put["/updateTopic"] = parameters =>
            {
                var topic = this.Bind<topiccategory>();

                if (topicManager.updateTopic(topic))
                {
                    return HttpStatusCode.OK;
                }

                else
                {
                    return HttpStatusCode.Conflict;
                }
            };

            Put["/deleteTopic/{topiccategoryID:int}"] = parameters =>
            {
                return topicManager.deleteTopic(parameters.topiccategoryID);
            };

            /* ----- Administrators -----*/

            Get["/getNewAdmin/{email}"] = parameters =>
            {
                return adminManager.checkNewAdmin(parameters.email);
            };

            Get["/getAdministrators"] = parameters =>
            {
                try
                {
                    //this.RequiresAuthentication();
                    //this.RequiresClaims(new[] { "admin" });
                    return Response.AsJson(adminManager.getAdministratorList());
                }
                catch { return null; }
            };

            Get["/getPrivilegesList"] = parameters =>
            {
                try
                {
                    //this.RequiresAuthentication();
                    //this.RequiresClaims(new[] { "admin" });
                    return Response.AsJson(adminManager.getPrivilegesList());
                }
                catch { return null; }
            };

            Post["/addAdmin"] = parameters =>
            {
                var newAdmin = this.Bind<AdministratorQuery>();
                return Response.AsJson(adminManager.addAdmin(newAdmin));
            };

            Put["/editAdmin"] = parameters =>
            {
                var editAdmin = this.Bind<AdministratorQuery>();
                return Response.AsJson(adminManager.editAdministrator(editAdmin));
            };

            Put["/deleteAdmin"] = parameters =>
            {
                var delAdmin = this.Bind<AdministratorQuery>();
                return adminManager.deleteAdministrator(delAdmin);
            };

            /*------ Evaluators -----*/

            Get["/getEvaluatorListFromIndex/{index:int}"] = parameters =>
            {
                int index = parameters.index;
                return Response.AsJson(evaluatorManager.getEvaluatorList(index));
            };

            Get["/getPendingListFromIndex/{index:int}"] = parameters =>
            {
                int index = parameters.index;
                return Response.AsJson(evaluatorManager.getPendingList(index));
            };

            Get["/getNewEvaluator/{email}"] = parameters =>
            {
                return evaluatorManager.checkNewEvaluator(parameters.email);
            };

            Post["/addEvaluator/{email}"] = parameters =>
            {
                return evaluatorManager.addEvaluator(parameters.email);
            };

            Put["/updateEvaluatorAcceptanceStatus"] = parameters =>
            {
                var updateEvaluator = this.Bind<EvaluatorQuery>();
                if (evaluatorManager.updateAcceptanceStatus(updateEvaluator))
                {
                    return HttpStatusCode.OK;
                }
                else
                {
                    return HttpStatusCode.Conflict;
                }
            };

            /* ----- Registration -----*/

            Get["/getRegistrations"] = parameters =>
            {
                List<RegisteredUser> list = registration.getRegistrationList();
                return Response.AsJson(list);
            };

            Get["/getUserTypes"] = parameters =>
            {
                List<UserTypeName> list = registration.getUserTypesList();
                return Response.AsJson(list);
            };

            Put["/updateRegistration"] = parameters =>
            {
                var registeredUser = this.Bind<RegisteredUser>();
                if (registration.updateRegistration(registeredUser))
                    return HttpStatusCode.OK;

                else
                    return HttpStatusCode.Conflict;
            };

            Delete["/deleteRegistration/{registrationID:int}"] = parameters =>
            {
                if (registration.deleteRegistration(parameters.registrationID))
                    return HttpStatusCode.OK;

                else
                    return HttpStatusCode.Conflict;
            };

            Post["/addRegistration"] = parameters =>
            {
                var user = this.Bind<user>();
                var reg = this.Bind<registration>();
                return Response.AsJson(registration.addRegistration(reg: reg, user: user));
            };

            Get["/getDates"] = parameters =>
            {
                List<string> list = registration.getDates();
                return Response.AsJson(list);
            };

            //-------------------------------------GUESTS---------------------------------------------
            //Guest list for admins
            Get["/getGuestList"] = parameters =>
            {
                List<GuestList> guestList = guest.getListOfGuests();

                if (guestList == null)
                {
                    guestList = new List<GuestList>();
                }
                return Response.AsJson(guestList);
            };

            //update acceptance status of guest
            Put["/updateAcceptanceStatus"] = parameters =>
            {
                var update = this.Bind<AcceptanceStatusInfo>();
                int guestID = update.id;
                String acceptanceStatus = update.status;

                if (guest.updateAcceptanceStatus(guestID, acceptanceStatus)) return HttpStatusCode.OK;
                else return HttpStatusCode.Conflict;
            };

            //set registration status of guest to Rejected.
            Put["/rejectRegisteredGuest/{id}"] = parameters =>
            {
                int id = parameters.id;

                if (guest.rejectRegisteredGuest(id)) return HttpStatusCode.OK;
                else return HttpStatusCode.Conflict;
            };

            //get minor's authorizations
            Get["/displayAuthorizations/{id}"] = parameters =>
            {
                int id = parameters.id;
                List<MinorAuthorizations> authorizations = guest.getMinorAuthorizations(id);
                if (authorizations == null)
                {
                    authorizations = new List<MinorAuthorizations>();
                }
                return Response.AsJson(authorizations);
            };

            //-----------------------------------------WEBSITE CONTENT ----------------------------------------

            Get["/getHome"] = parameters =>
            {
                return Response.AsJson(webManager.getHome());
            };

            Get["/getHomeImage"] = parameters =>
            {
                return Response.AsJson(webManager.getHomeImage());
            };

            Put["/saveHome"] = parameters =>
            {
                var home = this.Bind<HomeQuery>();
                return webManager.saveHome(home);
            };

            Put["/removeFile/{data}"] = parameters =>
            {
                return webManager.removeFile(parameters.data);
            };

            Get["/getVenue"] = parameters =>
            {
                return Response.AsJson(webManager.getVenue());
            };

            Put["/saveVenue"] = parameters =>
            {
                var venue = this.Bind<VenueQuery>();
                return webManager.saveVenue(venue);
            };

            Get["/getContact"] = parameters =>
            {
                return Response.AsJson(webManager.getContact());
            };

            Put["/saveContact"] = parameters =>
            {
                var contact = this.Bind<ContactQuery>();
                return webManager.saveContact(contact);
            };

            Get["/getParticipation"] = parameters =>
            {
                return Response.AsJson(webManager.getParticipation());
            };

            Put["/saveParticipation"] = parameters =>
            {
                var participation = this.Bind<ParticipationQuery>();
                return webManager.saveParticipation(participation);
            };

            Get["/getRegistrationInfo"] = parameters =>
            {
                return Response.AsJson(webManager.getRegistrationInfo());
            };

            Put["/saveRegistrationInfo"] = parameters =>
            {
                var registrationInfo = this.Bind<RegistrationQuery>();
                return webManager.saveRegistrationInfo(registrationInfo);
            };

            Get["/getDeadlines"] = parameters =>
            {
                return Response.AsJson(webManager.getDeadlines());
            };

            Put["/saveDeadlines"] = parameters =>
            {
                var deadlines = this.Bind<DeadlinesQuery>();
                return webManager.saveDeadlines(deadlines);
            };

            Get["/getPlanningCommittee"] = parameters =>
            {
                return Response.AsJson(webManager.getPlanningCommittee());
            };

            Post["/addNewCommittee"] = parameters =>
            {
                var committee = this.Bind<PlanningCommitteeQuery>();
                return Response.AsJson(webManager.addCommittee(committee));
            };

            Put["/editCommittee"] = parameters =>
            {
                var committee = this.Bind<PlanningCommitteeQuery>();
                return webManager.editCommittee(committee);
            };

            Put["/deleteCommittee"] = parameters =>
            {
                var committee = this.Bind<PlanningCommitteeQuery>();
                return webManager.deleteCommittee(committee);
            };

            Get["/getCommitteeInterface"] = parameters =>
            {
                return Response.AsJson(webManager.getCommitteeInterface());
            };

            Get["/getAdminSponsorBenefits/{data}"] = parameters =>
            {
                return webManager.getAdminSponsorBenefits(parameters.data);
            };

            Put["/saveAdminSponsorBenefits"] = parameters =>
            {
                var sponsor = this.Bind<SaveSponsorQuery>();
                return webManager.saveSponsorBenefits(sponsor);
            };

            Put["/saveInstructions"] = parameters =>
            {
                return webManager.saveInstructions("");
            };

            Put["/saveInstructions/{data}"] = parameters =>
            {
                return webManager.saveInstructions(parameters.data);
            };

            Get["/getSponsorInstructions"] = parameters =>
            {
                return Response.AsJson(webManager.getInstructions());
            };

            Get["/getAllSponsorBenefits"] = parameters =>
            {
                return Response.AsJson(webManager.getAllSponsorBenefits());
            };

            Get["/getGeneralInfo"] = parameters =>
            {
                return Response.AsJson(webManager.getGeneralInfo());
            };

            Put["/saveGeneralInfo"] = parameters =>
            {
                var info = this.Bind<GeneralInfoQuery>();
                return webManager.saveGeneralInfo(info);
            };

            Get["/getProgram"] = parameters =>
            {
                return Response.AsJson(webManager.getProgram());
            };

            Get["/getAbstractDocument"] = parameters =>
            {
                return Response.AsJson(webManager.getAbstractDocument());
            };

            Get["/getProgramDocument"] = parameters =>
            {
                return Response.AsJson(webManager.getProgramDocument());
            };

            Put["/saveProgram"] = parameters =>
            {
                var info = this.Bind<ProgramQuery>();
                return webManager.saveProgram(info);
            };

            Get["/getBillReport/{index:int}"] = parameters =>
            {
                int index = parameters.index;
                return Response.AsJson(reportManager.getBillReportList(index));
            };

            Get["/getRegistrationPayments/{index:int}"] = parameters =>
            {
                int index = parameters.index;
                return Response.AsJson(reportManager.getRegistrationPayments(index));
            };

            Get["/getSponsorPayments/{index:int}"] = parameters =>
            {
                int index = parameters.index;
                return Response.AsJson(reportManager.getSponsorPayments(index));
            };

            //Gets all submissions in the system that have not been deleted
            Get["/getAllSubmissions"] = parameters =>
                {
                    return Response.AsJson(submissionManager.getAllSubmissions());
                };
            //gets the evaluation for a submission
            Get["/getEvaluationsForSubmission/{submissionID}"] = parameters =>
                {
                    long submissionID = parameters.submissionID;
                    var evaluations = submissionManager.getSubmissionEvaluations(submissionID);

                    return Response.AsJson(evaluations);
                };
            //gets all approved evaluators so as to assign them submissions to evaluate
            Get["/getAllEvaluators"] = parameters =>
                {
                    return Response.AsJson(submissionManager.getAcceptedEvaluators());
                };
            //Assigns an evaluator to a submission
            Post["/assignEvaluator/{submissionID:long}/{evaluatorID:long}"] = parameters =>
                {
                    long submissionID = parameters.submissionID;
                    long evaluatorID = parameters.evaluatorID;

                    Evaluation evList = submissionManager.assignEvaluator(submissionID, evaluatorID);

                    return Response.AsJson(evList);
                };
            //Assigns a template to a submission
            Post["/assignTemplate/{submissionID:long}/{templateID:long}"] = parameters =>
            {
                long submissionID = parameters.submissionID;
                long templateID = parameters.templateID;
                if (submissionManager.assignTemplate(submissionID, templateID)) return HttpStatusCode.OK;
                else return HttpStatusCode.Conflict;
            };
            //Get the info of an evaluation
            Get["/getEvaluationDetails/{submissionID:long}/{evaluatorID:long}"] = parameters =>
            {
                long submissionID = parameters.submissionID;
                long evaluatorID = parameters.evaluatorID;
                Evaluation sub = submissionManager.getEvaluationDetails(submissionID, evaluatorID);
                if (sub == null)
                {
                    sub = new Evaluation();
                }
                return Response.AsJson(sub);
            };
            //Remove evaluator submission relation
            Put["/removeEvaluatorSubmission/{evaluatorSubmissionID}"] = parameters =>
                {
                    long evaluatorSubmissionID = parameters.evaluatorSubmissionID;
                    long es = submissionManager.removeEvaluatorSubmission(evaluatorSubmissionID);
                    return Response.AsJson(es);
                };
            //Change submission status
            Put["/changeSubmissionStatus/{status}/{submissionID}"] = parameters =>
            {
                String newStatus = parameters.status;
                long submissionID = parameters.submissionID;
                Submission sub = submissionManager.changeSubmissionStatus(submissionID, newStatus);

                return Response.AsJson(sub);
            };
            //admin adds a submission
            Post["/postAdminSubmission"] = parameters =>
            {
                panel pannelToAdd = null;
                workshop workshopToAdd = null;
                submission submissionToAdd = this.Bind<submission>();
                usersubmission usersubTA = this.Bind<usersubmission>();

                int submissionTypeID = submissionToAdd.submissionTypeID;
                if (submissionTypeID == 3)
                {
                    pannelToAdd = this.Bind<panel>();
                }
                else if (submissionTypeID == 5)
                {
                    workshopToAdd = this.Bind<workshop>();
                }
                Submission newSubmission =
                    submissionManager.addSubmissionByAdmin(usersubTA, submissionToAdd, pannelToAdd, workshopToAdd);
                return Response.AsJson(newSubmission);
            };
            //post final version of evaluation submitted by admin
            Post["/postAdminFinalSubmission"] = parameters =>
            {
                panel pannelToAdd = null;
                workshop workshopToAdd = null;
                submission submissionToAdd = this.Bind<submission>();
                documentssubmitted submissionDocuments = this.Bind<documentssubmitted>();
                usersubmission usersubTA = this.Bind<usersubmission>();

                int submissionTypeID = submissionToAdd.submissionTypeID;
                if (submissionDocuments.document == null && submissionDocuments.documentName == null)
                {
                    submissionDocuments = null;
                }
                if (submissionTypeID == 3)
                {
                    pannelToAdd = this.Bind<panel>();
                }
                else if (submissionTypeID == 5)
                {
                    workshopToAdd = this.Bind<workshop>();
                }
                Submission newSubmission =
                    submissionManager.postAdminFinalSubmission(usersubTA, submissionToAdd, submissionDocuments, pannelToAdd, workshopToAdd);
                return Response.AsJson(newSubmission);
            };
            //gets all deleted submissions
            Get["/getDeletedSubmissions"] = parameters =>
                {
                    return Response.AsJson(submissionManager.getDeletedSubmissions());
                };
            //gets the details of a deleted submission
            Get["/getADeletedSubmission/{submissionID:long}"] = parameters =>
                {
                    long submissionID = parameters.submissionID;
                    return Response.AsJson(submissionManager.getADeletedSubmission(submissionID));
                };
            //gets the list of all users
            Get["/getListOfUsers"] = parameters =>
                {
                    return Response.AsJson(submissionManager.getListOfUsers());
                };
            //returns true is the currently logged in user is the master
            Get["/isMaster/{userID:long}"] = parameters =>
                {
                    long userID = parameters.userID;
                    bool isMaster = submissionManager.isMaster(userID);
                    return isMaster;
                };

            //------------------------------------Banner---------------------------------------------
            Get["/getBanners"] = parameters =>
            {
                return Response.AsJson(bannerManager.getBannerList());
            };

        }
    }
    public class AcceptanceStatusInfo
    {
        public int id { get; set; }
        public String status { get; set; }
    }
}