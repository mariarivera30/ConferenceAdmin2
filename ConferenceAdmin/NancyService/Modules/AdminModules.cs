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
            AdminManager adminManager = new AdminManager();
            EvaluatorManager evaluatorManager = new EvaluatorManager();
            TopicManager topicManager = new TopicManager();
            SponsorManager sponsorManager = new SponsorManager();
            List<sponsor> sponsorList = new List<sponsor>();
            RegistrationManager registration = new RegistrationManager();
            GuestManager guest = new GuestManager();
            TemplateManager templateManager = new TemplateManager();
            AuthTemplateManager authTemplateManager = new AuthTemplateManager();


            /* ----- Template -----*/


            Post["/addTemplate"] = parameters =>
            {
                var temp = this.Bind<TemplateManager.templateQuery>();
                return Response.AsJson(templateManager.addTemplate(temp));
            };

            Get["/getTemplatesAdmin"] = parameters =>
            {

                return Response.AsJson(templateManager.getTemplates());
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
                return Response.AsJson(authTemplateManager.addTemplate(temp));
            };

            Get["/getAuthTemplatesAdmin"] = parameters =>
            {

                return Response.AsJson(authTemplateManager.getTemplates());
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
                List<SponsorManager.ComplementaryQuery> list = sponsorManager.addKeysTo(obj);
                if (list != null)
                {
                    return Response.AsJson(list);
                }

                else
                {
                    return HttpStatusCode.Conflict;
                }
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
                List<SponsorManager.ComplementaryQuery> list = sponsorManager.deleteComplementarySponsor(id);
                if (list != null)
                {
                    return list;
                }

                else
                {
                    return HttpStatusCode.Conflict;
                }
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

                if (sponsorManager.updateSponsor(sponsor))
                {
                    return HttpStatusCode.OK;
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

            Get["/getEvaluatorList"] = parameters =>
            {
                return Response.AsJson(evaluatorManager.getEvaluatorList());
            };

            Get["/getPendingList"] = parameters =>
            {
                return Response.AsJson(evaluatorManager.getPendingList());
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

            Put["/saveHome"] = parameters =>
            {
                var home = this.Bind<HomeQuery>();
                return webManager.saveHome(home);
            };

            Put["/removeImage/{data}"] = parameters =>
            {
                return webManager.removeImage(parameters.data);
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

        }
    }
    public class AcceptanceStatusInfo
    {
        public int id { get; set; }
        public String status { get; set; }
    }
}