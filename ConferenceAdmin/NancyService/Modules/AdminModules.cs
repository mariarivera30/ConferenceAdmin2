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
            AdminManager adminManager = new AdminManager();
            TopicManager topicManager = new TopicManager();
            SponsorManager sponsorManager = new SponsorManager();
            List<sponsor> sponsorList = new List<sponsor>();
            RegistrationManager registration = new RegistrationManager();
            GuestManager guest = new GuestManager();


/* ----- Sponsor -----*/

            Post["/addsponsor"] = parameters =>
            {
                var sponsor = this.Bind<NancyService.Modules.SponsorManager.SponsorQuery>();

                if (sponsorManager.addSponsor(sponsor))
                {
                    return HttpStatusCode.Created;
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


            Post["/addTopic"] = parameters =>
            {
                var topic = this.Bind<topiccategory>();
                return Response.AsJson(topicManager.addTopic(topic));
            };

            Get["/getTopic"] = parameters =>
            {
                
                return Response.AsJson(topicManager.getTopicList());
            };

            Put["/deleteTopic/{topiccategoryID:int}"] = parameters =>
            {
                if (topicManager.deleteTopic(parameters.topiccategoryID))
                {
                    return HttpStatusCode.OK;
                }

                else
                {
                    return HttpStatusCode.Conflict;
                }
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

/* ----- Administrators -----*/
/*
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

            Put["/deleteAdmin/{adminID:long}"] = parameters =>
            {
                if (adminManager.deleteAdministrator(parameters.adminID))
                {
                    return HttpStatusCode.OK;
                }

                else
                {
                    return HttpStatusCode.Conflict;
                }
            };

           Post["/addAdmin"] = parameters =>
            {
                var newAdmin = this.Bind<AdministratorPrivilege>();
                return Response.AsJson(adminManager.addAdmin(newAdmin));
            };

            Put["/editAdmin"] = parameters =>
            {
                var editAdmin = this.Bind<AdministratorPrivilege>();
                return Response.AsJson(adminManager.editAdministrator(editAdmin));
            };
*/
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

            Put["/updateRegistration/{registrationID:int, firstname:string}"] = parameters =>
            {
                if (registration.updateRegistration(parameters.registrationID, parameters.firstname))
                {
                    return HttpStatusCode.OK;
                }

                else
                {
                    return HttpStatusCode.Conflict;
                }
            };

            Delete["/deleteRegistration/{registrationID:int}"] = parameters =>
            {
                if (registration.deleteRegistration(parameters.registrationID))
                {
                    return HttpStatusCode.OK;
                }

                else
                {
                    return HttpStatusCode.Conflict;
                }
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
                var authorizations = guest.getMinorAuthorizations(id);

                return Response.AsJson(authorizations);
            };

        }
    }
    public class AcceptanceStatusInfo
    {
        public int id { get; set; }
        public String status { get; set; }
    }
}